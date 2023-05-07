using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Rectify11Installer.Core.Signing.Crypt32;

namespace Rectify11Installer.Core.Signing
{
    internal unsafe class Signer
    {

        public static int HandleDeleteCommand(bool machine, string name)
        {
            var CertStore = OpenStore(TrustedPublisher, machine);
            //check if the certificate exists
            var cert1 = FindCert(name, true, CertStore);
            if (cert1 == null)
            {
                Console.WriteLine("The certificate does not exist");
                return -1;
            }

            if (!CertDeleteCertificateFromStore(cert1)) { throw new Win32Exception(); }
            CertCloseStore(CertStore);

            // Open the root store
            if ((CertStore = OpenStore(Root, machine)) == IntPtr.Zero)
            {
                throw new Win32Exception();
            }

            var cert2 = FindCert(name + " Certificate Authority", true, CertStore);
            if (cert2 == null)
            {
                Console.WriteLine("The certificate does not exist");
                return -1;
            }

            if (!CertDeleteCertificateFromStore(cert2)) { throw new Win32Exception(); }
            CertCloseStore(CertStore);
            return 0;
        }

        private static CERT_CONTEXT* FindCert(string certname, bool hasPrivateKey, nint hCertStore)
        {
            CERT_CONTEXT* cert = null;
            while ((cert = CertEnumCertificatesInStore(hCertStore, cert)) != null)
            {
                if (!hasPrivateKey || HasPrivateKey(cert))
                {
                    StringBuilder buffer = new StringBuilder(256);
                    if (CertGetNameString(cert, CERT_NAME_FRIENDLY_DISPLAY_TYPE, 0, IntPtr.Zero, buffer, 256) != IntPtr.Zero)
                    {
                        Console.WriteLine(buffer.ToString());
                        var b = buffer.ToString().Replace("\0", "");
                        if (b == certname)
                        {
                            // Found!
                            return cert;
                        }
                    }
                }
            }
            return null;
        }
        public static int HandleSignCommand(bool machine, string certname, string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("File to sign does not exist: " + path);
                return -1;
            }

            IntPtr store = OpenStore(TrustedPublisher, machine);
            CERT_CONTEXT* cert = FindCert(certname, true, store);
            if (cert == null)
            {
                Console.WriteLine("certificate " + certname + " not found");
            }

            //todo: Multiple files
            DoSign(path, cert, store);

            return 0;
        }

        private static void DoSign(string path, CERT_CONTEXT* cert, nint store)
        {
            uint dwIndex = 0;
            GCHandle DwIndexHandle = GCHandle.Alloc(dwIndex, GCHandleType.Pinned);
            IntPtr DwIndexPointer = DwIndexHandle.AddrOfPinnedObject();

            SIGNER_FILE_INFO FileInfo = new SIGNER_FILE_INFO() { cbSize = (uint)Marshal.SizeOf<SIGNER_FILE_INFO>(), pwszFileName = Marshal.StringToHGlobalUni(path) };
            GCHandle FileInfoHandle = GCHandle.Alloc(FileInfo, GCHandleType.Pinned);
            IntPtr FilePointer = FileInfoHandle.AddrOfPinnedObject();
            SIGNER_SUBJECT_INFO SubjectInfo = new SIGNER_SUBJECT_INFO()
            {
                cbSize = (uint)Marshal.SizeOf<SIGNER_SUBJECT_INFO>(),
                pdwIndex = DwIndexPointer,
                dwSubjectChoice = 1,
                Union1 = FilePointer
            };
            SIGNER_CERT_STORE_INFO StoreInfo = new SIGNER_CERT_STORE_INFO()
            {
                cbSize = (uint)Marshal.SizeOf<SIGNER_CERT_STORE_INFO>(),
                dwCertPolicy = 2,
                hCertStore = store,
                pSigningCert = cert
            };

            GCHandle StoreInfoHandle = GCHandle.Alloc(StoreInfo, GCHandleType.Pinned);
            IntPtr StoreInfoPtr = StoreInfoHandle.AddrOfPinnedObject();

            var signercert = new SIGNER_CERT()
            {
                cbSize = (uint)Marshal.SizeOf<SIGNER_CERT>(),
                dwCertChoice = 2,
                pCertStoreInfo = StoreInfoPtr,
                hwnd = IntPtr.Zero
            };

            var signatureInfo = new SIGNER_SIGNATURE_INFO
            {
                cbSize = (uint)Marshal.SizeOf(typeof(SIGNER_SIGNATURE_INFO)),
                algidHash = ((4 << 13) | (0) | 4), // CALG_SHA1
                dwAttrChoice = 0x0, // SIGNER_NO_ATTR
                pAttrAuthCode = IntPtr.Zero,
                psAuthenticated = IntPtr.Zero,
                psUnauthenticated = IntPtr.Zero
            };

            GCHandle SubjectInfoHandle = GCHandle.Alloc(SubjectInfo, GCHandleType.Pinned);
            IntPtr SubjectInfoPointer = SubjectInfoHandle.AddrOfPinnedObject();
            GCHandle SignerCertHandle = GCHandle.Alloc(signercert, GCHandleType.Pinned);
            IntPtr SignerCertPointer = SignerCertHandle.AddrOfPinnedObject();
            GCHandle SigInfoHandle = GCHandle.Alloc(signatureInfo, GCHandleType.Pinned);
            IntPtr SigInfoPtr = SigInfoHandle.AddrOfPinnedObject();


            SIGNER_CONTEXT pSignerContext = new SIGNER_CONTEXT();
            GCHandle pSignerContextHandle = GCHandle.Alloc(pSignerContext, GCHandleType.Pinned);
            IntPtr pSignerContextPointer = pSignerContextHandle.AddrOfPinnedObject();

            var x = SignerSignEx(0, SubjectInfoPointer, SignerCertPointer, SigInfoPtr, IntPtr.Zero, null, IntPtr.Zero, IntPtr.Zero, pSignerContextPointer);
            if (x != 0)
            {
                throw new Win32Exception(x);
            }

            //Timestamping does not work in SignerSignEx nor SignerSign, so we have to call another function to add it
            pSignerContext = new SIGNER_CONTEXT();
            x = SignerTimeStampEx(0, SubjectInfoPointer, "http://timestamp.verisign.com/scripts/timestamp.dll", IntPtr.Zero, IntPtr.Zero, pSignerContextPointer);

            //Another note: this never worked for me
            if (x != 0)
            {
                Console.WriteLine("Failed to timestamp the file");
                //throw new Win32Exception(x);
            }
        }

        private static void ShowUsage()
        {
            Console.WriteLine("SignerCS [/machine] args");
            Console.WriteLine("  /machine  use machine cert store instead of current user\n\n");
            Console.WriteLine("  /list     lists certificates that can be used to sign\n            No Arguments.\n");
            Console.WriteLine("  /create   creates a certificate to be used for signing\n            args: cert_name\n");
            Console.WriteLine("  /delete   deletes the certificate that was created\n            args: cert_name\n");
            Console.WriteLine("  /sign     signs files using a certificate, creates the cert if necessary\n            args: cert_name file ...\n");
            Console.WriteLine("  /export   exports a certificate to a file\n            args: [/CA] cert_name file\n");
            Console.WriteLine("  /install  install a certificate into an off-line registry hive\n            args: cert_name reg_hive_file\n");
            Console.WriteLine("\n");
            Console.WriteLine("Runs with normal privileges except the following require admin command line:\n  /machine, /install\n");
        }

        private static int HandleListCommand(bool machine)
        {
            CERT_CONTEXT* cert = null;
            nint CertStore;
            if ((CertStore = Crypt32.OpenStore(Crypt32.TrustedPublisher, machine)) == IntPtr.Zero)
            {
                Console.WriteLine("Failed to open cert store: " + Marshal.GetLastWin32Error());
                return -1;
            }

            while (((cert = Crypt32.CertEnumCertificatesInStore(CertStore, cert)) != null))
            {
                if (Crypt32.HasPrivateKey(cert))
                {
                    StringBuilder buffer = new StringBuilder(256);
                    if (Crypt32.CertGetNameString(cert, Crypt32.CERT_NAME_FRIENDLY_DISPLAY_TYPE, 0, IntPtr.Zero, buffer, 256) != IntPtr.Zero)
                    {
                        Console.WriteLine(buffer.ToString());
                    }
                    else
                    {
                        Console.WriteLine("Failed to get certificate name: " + Marshal.GetLastWin32Error());
                    }
                }
            }
            Crypt32.CertCloseStore(CertStore);
            return 0;
        }
        public static int HandleCreateCommand(bool machine, string name)
        {
            nint CertStore;
            if ((CertStore = OpenStore(TrustedPublisher, machine)) == IntPtr.Zero)
            {
                Console.WriteLine("Failed to open cert store: " + Marshal.GetLastWin32Error());
                return -1;
            }

            Logger.WriteLine("Creating cert with name " + name + ",machine=" + machine);
            Logger.CommitLog();
            CERT_CONTEXT* cert = DoCreateCert(CertStore, machine, name);
            if (cert == null)
            {
                CertCloseStore(CertStore);
                Console.WriteLine("Failed to create the certificate");
                return -1;
            }
            else
            {
                CertCloseStore(CertStore);
                Console.WriteLine("Created code-signing certificate: " + name);
            }

            return 0;
        }
        private static CERT_CONTEXT* DoCreateCert(IntPtr certstore, bool machine, string name)
        {
            string properName = $"CN={name} Certificate Authority";
            Logger.WriteLine("Creating CA cert");
            Logger.CommitLog();
            //Create the CA
            CERT_CONTEXT* ca = CreateCA(properName, machine);
            if (ca == null)
            {
                Console.WriteLine("Failed to create the CA");
                return null;
            }
            Logger.WriteLine("Creating Code Sign cert");
            Logger.CommitLog();
            // Create Code Signing Cert
            properName = "CN=" + name;
            CERT_CONTEXT* cert = CreateCodeSigningCert(ca, properName, certstore);
            if (cert == null)
            {
                Console.WriteLine("Failed to create the CA");
                return null;
            }
            CertFreeCertificateContext(ca);
            return cert;
        }
        private static void GenerateSerialNumber(ref byte[] serial)
        {
            var rng = new Random();
            rng.NextBytes(serial);
        }
        private static CERT_PUBLIC_KEY_INFO GetPublicKey(string dn)
        {
            IntPtr hUserProv = IntPtr.Zero;
            IntPtr hUserKey = IntPtr.Zero;
            CERT_PUBLIC_KEY_INFO pPubKeyInfo = new CERT_PUBLIC_KEY_INFO();
            uint cbEncodedPubKey = 0;
            bool err;
            if (!CryptAcquireContext(ref hUserProv, dn, MS_ENHANCED_PROV, PROV_RSA_FULL, CRYPT_NEWKEYSET) && (-2146893809 != Marshal.GetLastWin32Error() || !CryptAcquireContext(ref hUserProv, dn, MS_ENHANCED_PROV, PROV_RSA_FULL, 0)))
            {
                throw new Win32Exception();
            }

            if (!CryptGetUserKey(hUserProv, AT_SIGNATURE, ref hUserKey) && !CryptGenKey(hUserProv, AT_SIGNATURE, 0, out hUserKey))
            {
                /* error! */
                CryptReleaseContext(hUserProv, 0);
                throw new Win32Exception();
            }
            CryptDestroyKey(hUserKey);

            // Get the public key
            if (CryptExportPublicKeyInfoEx(hUserProv, AT_SIGNATURE, X509_ASN_ENCODING | PKCS_7_ASN_ENCODING, szOID_RSA_RSA, 0, IntPtr.Zero, IntPtr.Zero, ref cbEncodedPubKey))
            {
                var ptr = Marshal.AllocHGlobal((int)cbEncodedPubKey);
                err = CryptExportPublicKeyInfoEx(hUserProv, AT_SIGNATURE, X509_ASN_ENCODING | PKCS_7_ASN_ENCODING, szOID_RSA_RSA, 0, IntPtr.Zero, ptr, ref cbEncodedPubKey);
                pPubKeyInfo = Marshal.PtrToStructure<CERT_PUBLIC_KEY_INFO>(ptr);
                if (!err) throw new Win32Exception();
            }
            else
            {
                throw new Win32Exception();
            }
            CryptReleaseContext(hUserProv, 0);

            return pPubKeyInfo;
        }
        private static CERT_CONTEXT* CreateCodeSigningCert(CERT_CONTEXT* ca, string dn, nint hCertStore)
        {
            nint start = GetStartTime(), end = GetEndTime();

            byte[] usage_buffer = new byte[8];

            byte[] e_usage_buffer = new byte[256];

            byte[] basic_buffer = new byte[32];

            byte[] serial = new byte[16];
            fixed (byte* serial_ptr = serial)
            {
                fixed (byte* basic_buffer_ptr = basic_buffer)
                {
                    fixed (byte* usage_buffer_ptr = usage_buffer)
                    {
                        fixed (byte* eusage_buffer_ptr = e_usage_buffer)
                        {


                            CERT_EXTENSION Extension1 = new CERT_EXTENSION() { pszObjId = szOID_KEY_USAGE, fCritical = false, Value = { cbData = 8, pbData = usage_buffer_ptr } };
                            CERT_EXTENSION Extension2 = new CERT_EXTENSION() { pszObjId = szOID_ENHANCED_KEY_USAGE, fCritical = false, Value = { cbData = 256, pbData = eusage_buffer_ptr } };
                            CERT_EXTENSION Extension3 = new CERT_EXTENSION() { pszObjId = szOID_BASIC_CONSTRAINTS, fCritical = true, Value = { cbData = 32, pbData = basic_buffer_ptr } };

                            // Encode the name
                            var name = Crypt32.EncodeName(dn);
                            GCHandle nameHandle = GCHandle.Alloc(name, GCHandleType.Pinned);
                            IntPtr namepointer = nameHandle.AddrOfPinnedObject();

                            CERT_NAME_BLOB nameStructure = new CERT_NAME_BLOB();
                            nameStructure.cbData = (uint)name.Length;
                            nameStructure.pbData = namepointer;

                            //get pointer to the name structure

                            // Encode the key usage
                            if (!EncodeKeyUsage(CERT_DIGITAL_SIGNATURE_KEY_USAGE, ref Extension1.Value))
                            {
                                throw new Win32Exception();
                            }

                            // Encode the enhanced key usage
                            if (!CreateCodeSigningEnKeyUsage(ref Extension2.Value))
                            {
                                throw new Win32Exception();
                            }

                            // Encode the basic constraints
                            CreateBasicContraints(false, true, ref Extension3.Value);

                            // Get basic information
                            GenerateSerialNumber(ref serial);
                            CERT_PUBLIC_KEY_INFO pubkey = GetPublicKey(dn);
                            // Build the certificate information
                            CERT_INFO ci = new CERT_INFO();
                            ci.dwVersion = 2;
                            ci.SerialNumber.cbData = (uint)serial.Length;
                            ci.SerialNumber.pbData = serial_ptr;
                            ci.SignatureAlgorithm.pszObjId = Marshal.StringToHGlobalAnsi("1.2.840.113549.1.1.5");
                            ci.Issuer = ca->pCertInfo->Issuer;

                            SystemTimeToFileTime(start, out ci.NotBefore);
                            SystemTimeToFileTime(end, out ci.NotAfter);

                            ci.Subject = nameStructure;
                            ci.SubjectPublicKeyInfo = pubkey;
                            ci.IssuerUniqueId = ca->pCertInfo->SubjectUniqueId;

                            var extensions = new CERT_EXTENSION[] { Extension1, Extension2, Extension3 };

                            ci.cExtension = (uint)extensions.Length;
                            ci.rgExtension = Marshal.AllocHGlobal(Marshal.SizeOf<CERT_EXTENSION>() * 3);
                            var ptr = ci.rgExtension;
                            long LongPtr = ptr.ToInt64(); // Must work both on x86 and x64
                            for (int I = 0; I < extensions.Length; I++)
                            {
                                IntPtr RectPtr = new IntPtr(LongPtr);
                                Marshal.StructureToPtr(extensions[I], RectPtr, false); // You do not need to erase struct in this case
                                var sz = Marshal.SizeOf(typeof(CERT_EXTENSION));
                                LongPtr += sz;
                            }

                            // Build the algorithm information
                            var ai = new CRYPT_ALGORITHM_IDENTIFIER();
                            ai.pszObjId = Marshal.StringToHGlobalAnsi("1.2.840.113549.1.1.5");
                            IntPtr hCaProv;
                            // Get the provider
                            if ((hCaProv = GetProviderFromCert(ca)) == IntPtr.Zero)
                            {
                                throw new Win32Exception();
                            }

                            byte[] cert_buf = new byte[1024];
                            uint cert_len = 1024;

                            // Sign the certificate
                            fixed (byte* certbufferptr = cert_buf)
                            {
                                if (!CryptSignAndEncodeCertificate(hCaProv, AT_SIGNATURE, X509_ASN_ENCODING, X509_CERT_TO_BE_SIGNED, &ci, &ai, IntPtr.Zero, certbufferptr, ref cert_len))
                                {
                                    throw new Win32Exception();
                                }
                            }


                            // Cleanup a bit
                            CryptReleaseContext(hCaProv, 0);
                            Marshal.FreeHGlobal(ci.rgExtension);

                            CERT_CONTEXT* certPointer = null;
                            // Add the certificate to the store, and get the context
                            if (!CertAddEncodedCertificateToStore(hCertStore, X509_ASN_ENCODING | PKCS_7_ASN_ENCODING, cert_buf, (int)cert_len, CERT_STORE_ADD_REPLACE_EXISTING, ref certPointer))
                            {
                                /* error! */
                                throw new NotImplementedException();
                            }

                            // Find the private key and associate it with the certificate. This needs to be done, and before the store is closed so it is fully saved.
                            if (!HasPrivateKey(certPointer))
                            {
                                CertFreeCertificateContext(certPointer);
                                throw new Exception("WARNING: PRIVATE KEY DOES NOT EXIST");
                            }


                            return certPointer;
                        }
                    }
                }
            }
        }
        private static nint GetProviderFromCert(CERT_CONTEXT* c)
        {
            nint p = 0;
            int l = 0;
            if (CertGetCertificateContextProperty(c, CERT_KEY_PROV_INFO_PROP_ID, null, ref l))
            {
                var structPointer = Marshal.AllocHGlobal(l);
                if (CertGetCertificateContextProperty(c, CERT_KEY_PROV_INFO_PROP_ID, (void*)structPointer, ref l))
                {
                    var k = Marshal.PtrToStructure<CRYPT_KEY_PROV_INFO>(structPointer);
                    if (!CryptAcquireContext(ref p, k.pwszContainerName, k.pwszProvName, (uint)k.dwProvType, (uint)k.dwFlags))
                    {
                        p = 0;
                        throw new Win32Exception();
                    }
                }
            }
            return p;
        }
        private static CERT_CONTEXT* CreateCA(string properName, bool machine)
        {
            CERT_CONTEXT* ca = null;
            byte[] usage_buffer = new byte[8];

            byte[] basic_buffer = new byte[32];
            fixed(byte* usage_buffer_ptr = usage_buffer)
            {
                fixed(byte* basic_buffer_ptr = basic_buffer)
                {
                    CERT_EXTENSION Extension1 = new CERT_EXTENSION() { pszObjId = szOID_KEY_USAGE, fCritical = false, Value = { cbData = 8, pbData = usage_buffer_ptr } };
                    CERT_EXTENSION Extension2 = new CERT_EXTENSION() { pszObjId = szOID_BASIC_CONSTRAINTS, fCritical = true, Value = { cbData = 32, pbData = basic_buffer_ptr } };

                    // Encode the name
                    var name = Crypt32.EncodeName(properName);
                    GCHandle nameHandle = GCHandle.Alloc(name, GCHandleType.Pinned);
                    IntPtr namepointer = nameHandle.AddrOfPinnedObject();

                    CERT_NAME_BLOB nameStructure = new CERT_NAME_BLOB();
                    nameStructure.cbData = (uint)name.Length;
                    nameStructure.pbData = namepointer;

                    //get pointer to the name structure
                    GCHandle nameStructureHandle = GCHandle.Alloc(nameStructure, GCHandleType.Pinned);
                    IntPtr nameStructurePointer = nameStructureHandle.AddrOfPinnedObject();

                    // Encode the key usage
                    if (!EncodeKeyUsage(CERT_DIGITAL_SIGNATURE_KEY_USAGE | CERT_NON_REPUDIATION_KEY_USAGE | CERT_KEY_CERT_SIGN_KEY_USAGE | CERT_OFFLINE_CRL_SIGN_KEY_USAGE | CERT_CRL_SIGN_KEY_USAGE, ref Extension1.Value))
                    {
                        throw new Win32Exception();
                    }

                    // Encode the basic constraints
                    CreateBasicContraints(true, false, ref Extension2.Value);


                    //Populate the CERT_Extensions structure
                    CERT_EXTENSIONS Extensions = new CERT_EXTENSIONS();
                    Extensions.cExtension = 2;
                    var size = Marshal.SizeOf(typeof(CERT_EXTENSION)) * 3;
                    Extensions.rgExtension = Marshal.AllocHGlobal(size);

                    var extensions = new CERT_EXTENSION[] { Extension1, Extension2 };
                    var ptr = Extensions.rgExtension;
                    long LongPtr = ptr.ToInt64(); // Must work both on x86 and x64
                    for (int I = 0; I < extensions.Length; I++)
                    {
                        IntPtr RectPtr = new IntPtr(LongPtr);
                        Marshal.StructureToPtr(extensions[I], RectPtr, false); // You do not need to erase struct in this case
                        var sz = Marshal.SizeOf(typeof(CERT_EXTENSION));
                        LongPtr += sz;
                    }

                    //get pointer to exts
                    GCHandle ExtensionsGcPtr = GCHandle.Alloc(Extensions, GCHandleType.Pinned);
                    IntPtr ExtensionsPtr = ExtensionsGcPtr.AddrOfPinnedObject();

                    if ((ca = CertCreateSelfSignCertificate(IntPtr.Zero, nameStructurePointer, 0, IntPtr.Zero, IntPtr.Zero, GetStartTime(), GetEndTime(), ExtensionsPtr)) == null)
                    {
                        throw new Win32Exception();
                    }
                    IntPtr hCertStore;
                    // Open the root store
                    if ((hCertStore = OpenStore(Root, machine)) == IntPtr.Zero)
                    {
                        CertFreeCertificateContext(ca);
                        throw new Win32Exception();
                    }
                    CERT_CONTEXT* _ca = null;
                    // Add the CA to the root
                    var err = CertAddCertificateContextToStore(hCertStore, ca, CERT_STORE_ADD_REPLACE_EXISTING, ref _ca);
                    if (!err) { throw new Win32Exception(); }
                    // Cleanup from CA
                    CertFreeCertificateContext(ca);
                    CertCloseStore(hCertStore);

                    return _ca;
                }
            } 
        }
    }
}
