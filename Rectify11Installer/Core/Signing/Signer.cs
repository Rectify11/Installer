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
    internal class Signer
    {

        public static int HandleDeleteCommand(bool machine, string name)
        {
            var CertStore = OpenStore(TrustedPublisher, machine);
            //check if the certificate exists
            var cert1 = FindCert(name, true, CertStore);
            if (cert1 == IntPtr.Zero)
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
            if (cert2 == IntPtr.Zero)
            {
                Console.WriteLine("The certificate does not exist");
                return -1;
            }

            if (!CertDeleteCertificateFromStore(cert2)) { throw new Win32Exception(); }
            CertCloseStore(CertStore);
            return 0;
        }

        private static nint FindCert(string certname, bool hasPrivateKey, nint hCertStore)
        {
            IntPtr cert = IntPtr.Zero;
            while ((cert = CertEnumCertificatesInStore(hCertStore, cert)) != IntPtr.Zero)
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
            return IntPtr.Zero;
        }
        public static int HandleSignCommand(bool machine, string certname, string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("File to sign does not exist: " + path);
                return -1;
            }

            IntPtr store = OpenStore(TrustedPublisher, machine);
            IntPtr cert = FindCert(certname, true, store);
            if (cert == IntPtr.Zero)
            {
                Console.WriteLine("certificate " + certname + " not found");
            }

            //todo: Multiple files
            DoSign(path, cert, store);

            return 0;
        }

        private static void DoSign(string path, nint cert, nint store)
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
            IntPtr cert = IntPtr.Zero;
            nint CertStore;
            if ((CertStore = Crypt32.OpenStore(Crypt32.TrustedPublisher, machine)) == IntPtr.Zero)
            {
                Console.WriteLine("Failed to open cert store: " + Marshal.GetLastWin32Error());
                return -1;
            }

            while (((cert = Crypt32.CertEnumCertificatesInStore(CertStore, cert)) != IntPtr.Zero))
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


            IntPtr cert = DoCreateCert(CertStore, machine, name);
            if (cert == IntPtr.Zero)
            {
                Console.WriteLine("Failed to create the certificate");
            }
            else
            {
                Console.WriteLine("Created code-signing certificate: " + name);
            }

            CertCloseStore(CertStore);

            return 0;
        }
        private static IntPtr DoCreateCert(IntPtr certstore, bool machine, string name)
        {
            string properName = $"CN={name} Certificate Authority";

            //Create the CA
            IntPtr ca = CreateCA(properName, machine);
            if (ca == IntPtr.Zero)
            {
                Console.WriteLine("Failed to create the CA");
                return IntPtr.Zero;
            }

            // Create Code Signing Cert
            properName = "CN=" + name;
            IntPtr cert = CreateCodeSigningCert(ca, properName, certstore);
            if (cert == IntPtr.Zero)
            {
                Console.WriteLine("Failed to create the CA");
                return IntPtr.Zero;
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
        private static nint CreateCodeSigningCert(nint caPtr, string dn, nint hCertStore)
        {
            var ca = Marshal.PtrToStructure<CERT_CONTEXT>(caPtr);
            var certInfo = Marshal.PtrToStructure<CERT_INFO>(ca.pCertInfo);

            nint start = GetStartTime(), end = GetEndTime();

            byte[] usage_buffer = new byte[8];
            GCHandle pinnedArray = GCHandle.Alloc(usage_buffer, GCHandleType.Pinned);
            IntPtr usage_buffer_pointer = pinnedArray.AddrOfPinnedObject();

            byte[] e_usage_buffer = new byte[256];
            GCHandle eusagebufferhandle = GCHandle.Alloc(e_usage_buffer, GCHandleType.Pinned);
            IntPtr eusagebufferpointer = eusagebufferhandle.AddrOfPinnedObject();

            byte[] basic_buffer = new byte[32];
            GCHandle basicbufferhandle = GCHandle.Alloc(basic_buffer, GCHandleType.Pinned);
            IntPtr basicbufferpointer = basicbufferhandle.AddrOfPinnedObject();

            byte[] serial = new byte[16];

            CERT_EXTENSION Extension1 = new CERT_EXTENSION() { pszObjId = szOID_KEY_USAGE, fCritical = false, Value = { cbData = 8, pbData = usage_buffer_pointer } };
            CERT_EXTENSION Extension2 = new CERT_EXTENSION() { pszObjId = szOID_ENHANCED_KEY_USAGE, fCritical = false, Value = { cbData = 256, pbData = eusagebufferpointer } };
            CERT_EXTENSION Extension3 = new CERT_EXTENSION() { pszObjId = szOID_BASIC_CONSTRAINTS, fCritical = true, Value = { cbData = 32, pbData = basicbufferpointer } };

            // Encode the name
            var name = Crypt32.EncodeName(dn);
            GCHandle nameHandle = GCHandle.Alloc(name, GCHandleType.Pinned);
            IntPtr namepointer = nameHandle.AddrOfPinnedObject();

            CERT_NAME_BLOB nameStructure = new CERT_NAME_BLOB();
            nameStructure.cbData = (uint)name.Length;
            nameStructure.pbData = namepointer;

            //get pointer to the name structure
            GCHandle nameStructureHandle = GCHandle.Alloc(nameStructure, GCHandleType.Pinned);
            IntPtr nameStructurePointer = nameStructureHandle.AddrOfPinnedObject();

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
            GCHandle serialHandle = GCHandle.Alloc(serial, GCHandleType.Pinned);
            IntPtr serialpointer = serialHandle.AddrOfPinnedObject();
            CERT_PUBLIC_KEY_INFO pubkey = GetPublicKey(dn);

            // Build the certificate information
            CERT_INFO ci = new CERT_INFO();
            ci.dwVersion = 2;
            ci.SerialNumber.cbData = (uint)serial.Length;
            ci.SerialNumber.pbData = serialpointer;
            ci.SignatureAlgorithm.pszObjId = Marshal.StringToHGlobalAnsi("1.2.840.113549.1.1.5");
            ci.Issuer = certInfo.Issuer;

            SystemTimeToFileTime(start, out ci.NotBefore);
            SystemTimeToFileTime(end, out ci.NotAfter);

            ci.Subject = nameStructure;
            ci.SubjectPublicKeyInfo = pubkey;
            ci.IssuerUniqueId = certInfo.SubjectUniqueId;

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
            if ((hCaProv = GetProviderFromCert(caPtr)) == IntPtr.Zero)
            {
                throw new Win32Exception();
            }
            GCHandle ciHandle = GCHandle.Alloc(ci, GCHandleType.Pinned);
            IntPtr ciPointer = ciHandle.AddrOfPinnedObject();
            GCHandle aiHandle = GCHandle.Alloc(ai, GCHandleType.Pinned);
            IntPtr aiPointer = aiHandle.AddrOfPinnedObject();
            byte[] cert_buf = new byte[1024];

            GCHandle certbufHandle = GCHandle.Alloc(cert_buf, GCHandleType.Pinned);
            IntPtr certbufPointer = certbufHandle.AddrOfPinnedObject();
            uint cert_len = 1024;

            // Sign the certificate
            if (!CryptSignAndEncodeCertificate(hCaProv, AT_SIGNATURE, X509_ASN_ENCODING, X509_CERT_TO_BE_SIGNED, ciPointer, aiPointer, IntPtr.Zero, certbufPointer, ref cert_len))
            {
                throw new Win32Exception();
            }

            // Cleanup a bit
            CryptReleaseContext(hCaProv, 0);
            aiHandle.Free();
            ciHandle.Free();
            serialHandle.Free();
            Marshal.FreeHGlobal(ci.rgExtension);

            var certPointer = IntPtr.Zero;//Marshal.AllocHGlobal(Marshal.SizeOf<CERT_CONTEXT>());
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
        private static nint GetProviderFromCert(nint c)
        {
            nint p = 0;
            int l = 0;
            IntPtr pa = IntPtr.Zero;
            if (CertGetCertificateContextProperty(c, CERT_KEY_PROV_INFO_PROP_ID, pa, ref l))
            {
                var structPointer = Marshal.AllocHGlobal(l);
                if (CertGetCertificateContextProperty(c, CERT_KEY_PROV_INFO_PROP_ID, structPointer, ref l))
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
        private static nint CreateCA(string properName, bool machine)
        {
            IntPtr ca = IntPtr.Zero;
            byte[] usage_buffer = new byte[8];
            GCHandle pinnedArray = GCHandle.Alloc(usage_buffer, GCHandleType.Pinned);
            IntPtr usage_buffer_pointer = pinnedArray.AddrOfPinnedObject();

            byte[] basic_buffer = new byte[32];
            GCHandle pinnedArray2 = GCHandle.Alloc(basic_buffer, GCHandleType.Pinned);
            IntPtr basic_buffer_pointer = pinnedArray2.AddrOfPinnedObject();

            CERT_EXTENSION Extension1 = new CERT_EXTENSION() { pszObjId = szOID_KEY_USAGE, fCritical = false, Value = { cbData = 8, pbData = usage_buffer_pointer } };
            CERT_EXTENSION Extension2 = new CERT_EXTENSION() { pszObjId = szOID_BASIC_CONSTRAINTS, fCritical = true, Value = { cbData = 32, pbData = basic_buffer_pointer } };

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

            if ((ca = CertCreateSelfSignCertificate(IntPtr.Zero, nameStructurePointer, 0, IntPtr.Zero, IntPtr.Zero, GetStartTime(), GetEndTime(), ExtensionsPtr)) == IntPtr.Zero)
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
            IntPtr _ca = IntPtr.Zero;
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
