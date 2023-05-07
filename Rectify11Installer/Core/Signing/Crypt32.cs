using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Rectify11Installer.Core.Signing
{
    //Note: this code is a big mess. Use the HandleSignCommand and HandleCreateCommand to use.
    //The code is based off of signer.exe by Jeffrey Bush. https://www.coderforlife.com/projects/win7boot/extras/
    public unsafe class Crypt32
    {
        private const int CERT_STORE_PROV_SYSTEM_W = 10;
        private const int CERT_SYSTEM_STORE_LOCAL_MACHINE = (2 << 16);
        private const int CERT_SYSTEM_STORE_CURRENT_USER = (1 << 16);
        private const int CRYPT_ACQUIRE_ALLOW_NCRYPT_KEY_FLAG = 0x00010000;
        public const int CERT_NAME_FRIENDLY_DISPLAY_TYPE = 5;
        public const int X509_ASN_ENCODING = 1;
        public const int PKCS_7_ASN_ENCODING = 0x00010000;
        private const int CERT_X500_NAME_STR = 3;
        private const int CERT_NAME_STR_CRLF_FLAG = 0x08000000;
        private const int CERT_NAME_STR_NO_PLUS_FLAG = 0x20000000;
        private const int CERT_NAME_STR_NO_QUOTING_FLAG = 0x10000000;
        public const int CERT_DIGITAL_SIGNATURE_KEY_USAGE = 0x80;
        public const int CERT_CRL_SIGN_KEY_USAGE = 0x02;
        public const int CERT_OFFLINE_CRL_SIGN_KEY_USAGE = 0x02;
        public const int CERT_NON_REPUDIATION_KEY_USAGE = 0x40;
        public const int CERT_KEY_CERT_SIGN_KEY_USAGE = 0x04;
        public const int X509_ENHANCED_KEY_USAGE = 36;
        public const int CERT_CA_SUBJECT_FLAG = 0x80;
        public const int CERT_END_ENTITY_SUBJECT_FLAG = 0x40;
        public const int CERT_STORE_ADD_USE_EXISTING = 2;
        public const int CERT_STORE_ADD_REPLACE_EXISTING = 3;
        public const int PROV_RSA_FULL = 1;
        public const int CRYPT_NEWKEYSET = 0x00000008;
        public const int AT_SIGNATURE = 2;
        public const string szOID_RSA_RSA = "1.2.840.113549.1.1.1";
        public const int CERT_KEY_PROV_INFO_PROP_ID = 2;
        public const int X509_CERT_TO_BE_SIGNED = 2;

        public const string TrustedPublisher = "TrustedPublisher";
        public const string Root = "Root";
        public const string szOID_KEY_USAGE = "2.5.29.15";
        public const string szOID_BASIC_CONSTRAINTS = "2.5.29.10";
        public const string szOID_ENHANCED_KEY_USAGE = "2.5.29.37";
        public const string MS_ENHANCED_PROV = "Microsoft Enhanced Cryptographic Provider v1.0";


        public static IntPtr OpenStore(string name, bool machine)
        {
            return CertOpenStore(CERT_STORE_PROV_SYSTEM_W, 0, IntPtr.Zero, machine ? CERT_SYSTEM_STORE_LOCAL_MACHINE : CERT_SYSTEM_STORE_CURRENT_USER, name);
        }
        public static bool HasPrivateKey(CERT_CONTEXT* cert)
        {
            return CryptFindCertificateKeyProvInfo(cert, CRYPT_ACQUIRE_ALLOW_NCRYPT_KEY_FLAG, IntPtr.Zero).ToInt64() > 0;
        }
        public static byte[] EncodeName(string dn)
        {
            byte[] bData;
            uint cbEncoded = 0;

            CertStrToName(X509_ASN_ENCODING | PKCS_7_ASN_ENCODING, dn, CERT_X500_NAME_STR | CERT_NAME_STR_CRLF_FLAG | CERT_NAME_STR_NO_PLUS_FLAG | CERT_NAME_STR_NO_QUOTING_FLAG, IntPtr.Zero, null, ref cbEncoded, null);

            bData = new byte[cbEncoded];
            CertStrToName(X509_ASN_ENCODING | PKCS_7_ASN_ENCODING, dn, CERT_X500_NAME_STR | CERT_NAME_STR_CRLF_FLAG | CERT_NAME_STR_NO_PLUS_FLAG | CERT_NAME_STR_NO_QUOTING_FLAG, IntPtr.Zero, bData, ref cbEncoded, null);

            return bData;
        }
        public static bool CreateCodeSigningEnKeyUsage(ref CRYPTOAPI_BLOB encoded)
        {
            CERT_ENHKEY_USAGE usage = new CERT_ENHKEY_USAGE();
            var OID = Marshal.StringToHGlobalAnsi("1.3.6.1.5.5.7.3.3");

            usage.rgpszUsageIdentifier = Marshal.AllocHGlobal(Marshal.SizeOf(OID));

            Marshal.WriteIntPtr(usage.rgpszUsageIdentifier, OID);

            usage.cUsageIdentifier = 1;
            if (!CryptEncodeObject(X509_ASN_ENCODING | PKCS_7_ASN_ENCODING, X509_ENHANCED_KEY_USAGE, ref usage, encoded.pbData, ref encoded.cbData))
            {
                int error = (int)Marshal.GetLastWin32Error();

                throw new Win32Exception(error);
            }
            return true;
        }
        public static void CreateBasicContraints(bool ca, bool end, ref CRYPTOAPI_BLOB encoded)
        {
            CERT_BASIC_CONSTRAINTS_INFO bc = new CERT_BASIC_CONSTRAINTS_INFO();
            var type_val = (byte)((ca ? CERT_CA_SUBJECT_FLAG : 0) | (end ? CERT_END_ENTITY_SUBJECT_FLAG : 0));
            bc.SubjectType = new CRYPT_BIT_BLOB();
            bc.SubjectType.cbData = 1;
            bc.SubjectType.cUnusedBits = 6;

            byte[] datas = new byte[] { type_val };
            GCHandle pinnedArray = GCHandle.Alloc(datas, GCHandleType.Pinned);
            IntPtr pointer = pinnedArray.AddrOfPinnedObject();
            bc.SubjectType.pbData = pointer;
            if (!CryptEncodeObject(X509_ASN_ENCODING | PKCS_7_ASN_ENCODING, szOID_BASIC_CONSTRAINTS, ref bc, encoded.pbData, ref encoded.cbData))
            {
                throw new Win32Exception();
            }
            pinnedArray.Free();
        }
        internal static bool EncodeKeyUsage(byte key_usage, ref CRYPTOAPI_BLOB encoded)
        {
            CRYPT_BIT_BLOB usage = new CRYPT_BIT_BLOB();
            usage.cbData = 1;
            usage.cUnusedBits = 0;
            byte[] bytes = new byte[] { key_usage };
            GCHandle pinnedArray = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            IntPtr pointer = pinnedArray.AddrOfPinnedObject();

            usage.pbData = pointer;
            if (CryptEncodeObject(X509_ASN_ENCODING | PKCS_7_ASN_ENCODING, szOID_KEY_USAGE, ref usage, null, ref encoded.cbData))
            {
                byte[] data = new byte[encoded.cbData];
                fixed(byte* dataptr = data)
                {
                    var result = CryptEncodeObject(X509_ASN_ENCODING | PKCS_7_ASN_ENCODING, szOID_KEY_USAGE, ref usage, dataptr, ref encoded.cbData);

                    fixed (byte* ptr = data)
                    {
                        encoded.pbData = ptr;
                    }
                    return result;
                }
            }
            else
            {
                throw new Win32Exception();
            }
        }
        public static IntPtr GetStartTime()
        {
            GetSystemTime(out SYSTEMTIME t);
            var x = Marshal.AllocHGlobal(16);
            Marshal.StructureToPtr(t, x, true);
            return x;
        }
        public static IntPtr GetEndTime()
        {
            GetSystemTime(out SYSTEMTIME t);
            t.wYear += 100;
            var x = Marshal.AllocHGlobal(16);
            Marshal.StructureToPtr(t, x, true);
            return x;
        }

        #region Dll imports
        [DllImport("CRYPT32.DLL", EntryPoint = "CertGetCertificateContextProperty", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CertGetCertificateContextProperty([In] CERT_CONTEXT* pCertContext, [In] int dwPropId, [Out] void* pvData, [In, Out] ref int pcbData);

        [DllImport("CRYPT32.DLL", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CertDeleteCertificateFromStore([In] CERT_CONTEXT* pCertContext);

        [DllImport(@"Advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CryptGenKey(
        [In] IntPtr hProv,
        [In] uint Algid,
        [In] uint dwFlags,
        [Out] out IntPtr phKey
    );
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool CryptGetUserKey(IntPtr hProv, uint dwKeySpec, ref IntPtr hKey);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CryptAcquireContext(ref IntPtr hProv, string pszContainer,
   string pszProvider, uint dwProvType, uint dwFlags);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CryptAcquireContext(ref IntPtr hProv, IntPtr pszContainer,
 IntPtr pszProvider, uint dwProvType, uint dwFlags);
        [DllImport("kernel32.dll")]
        static extern void GetSystemTime(out SYSTEMTIME t);
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool CryptDestroyKey(IntPtr phKey);
        [DllImport("Advapi32.dll", EntryPoint = "CryptReleaseContext", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool CryptReleaseContext(
   IntPtr hProv,
   Int32 dwFlags   // Reserved. Must be 0.
   ); [DllImport("crypt32.dll", SetLastError = true)]
        public static extern bool CryptExportPublicKeyInfoEx(
    IntPtr hProv,
    uint dwKeySpec,
    uint dwCertEncodingType,
    string pxzPublicKeyObjId,
    uint dwFlags,
    IntPtr pvAuxInfo,
    IntPtr pInfo,
    ref uint pcbInfo);
        [DllImport("crypt32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CryptSignAndEncodeCertificate(IntPtr hCryptProvOrNCryptKey,
                                                                            uint dwKeySpec,
                                                                            uint dwCertEncodingType,
                                                                            int lpszStructType,
                                                                            CERT_INFO* pvStructInfo,
                                                                            [In] CRYPT_ALGORITHM_IDENTIFIER* pSignatureAlgorithm,
                                                                            IntPtr pvHashAuxInfo,
                                                                            [Out] byte* pbEncoded,
                                                                            [In, Out] ref uint pcbEncoded);
        [DllImport("crypt32.dll")]
        public static extern bool CertFreeCertificateContext(CERT_CONTEXT* pCertContext);
        [DllImport("CRYPT32.DLL", EntryPoint = "CertEnumCertificatesInStore", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern CERT_CONTEXT* CertEnumCertificatesInStore(IntPtr storeProvider, CERT_CONTEXT* prevCertContext);
        [DllImport("CRYPT32.DLL", EntryPoint = "CertOpenStore", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CertOpenStore(int storeProvider, int encodingType, IntPtr hcryptProv, int flags, string pvPara);
        [DllImport("CRYPT32.DLL", EntryPoint = "CryptFindCertificateKeyProvInfo", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CryptFindCertificateKeyProvInfo(CERT_CONTEXT* cert, int flags, IntPtr reserved);
        [DllImport("CRYPT32.DLL", EntryPoint = "CryptFindCertificateKeyProvInfo", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CryptFindCertificateKeyProvInfo(CERT_CONTEXT cert, int flags, IntPtr reserved);
        [DllImport("crypt32.dll", EntryPoint = "CertGetNameString", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CertGetNameString(CERT_CONTEXT* CertContext, int lType, int lFlags, IntPtr pTypeParameter, StringBuilder str, uint cch);
        [DllImport("CRYPT32.DLL", EntryPoint = "CertAddEncodedCertificateToStore", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CertAddEncodedCertificateToStore(IntPtr certStore, int certEncodingType, byte[] certEncoded, int certEncodedLength, int addDisposition, ref CERT_CONTEXT* certContext);
        [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [ResourceExposure(ResourceScope.None)]
        internal protected extern static
       bool CertAddCertificateContextToStore(
           [In] IntPtr hCertStore,
           [In] CERT_CONTEXT* pCertContext,
           [In] uint dwAddDisposition,
           [In, Out] ref CERT_CONTEXT* ppStoreContext);
        [DllImport("crypt32.dll", EntryPoint = "CertCloseStore", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CertCloseStore(IntPtr CertContext);
        [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [ResourceExposure(ResourceScope.None)]
        public extern static
        CERT_CONTEXT* CertCreateSelfSignCertificate(
            [In] IntPtr hProv,
            [In] IntPtr pSubjectIssuerBlob,
            [In] uint dwFlags,
            [In] IntPtr pKeyProvInfo,
            [In] IntPtr pSignatureAlgorithm,
            [In] IntPtr pStartTime,
            [In] IntPtr pEndTime,
            [In] IntPtr pExtensions);
        [DllImport("crypt32.dll", SetLastError = true)]
        public static extern bool CertStrToName(
        uint dwCertEncodingType,
        string pszX500,
        uint dwStrType,
        IntPtr pvReserved,
        byte[] pbEncoded,
        ref uint pcbEncoded,
        StringBuilder ppszError);
        [DllImport("crypt32.dll", SetLastError = true)]
        internal static extern bool CryptEncodeObject(
        uint dwCertEncodingType,
        string lpszStructType,
        ref CRYPT_BIT_BLOB pvStructInfo,
       [Out] byte* pbEncoded,
        ref uint pcbEncoded);
        [DllImport("crypt32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        internal static extern bool CryptEncodeObject(
       uint dwCertEncodingType,
       int lpszStructType,
       ref CERT_ENHKEY_USAGE pvStructInfo,
       byte* pbEncoded,
       ref uint pcbEncoded);
        [DllImport("crypt32.dll", SetLastError = true)]
        internal static extern bool CryptEncodeObject(
   uint dwCertEncodingType,
   string lpszStructType,
   ref CERT_BASIC_CONSTRAINTS_INFO pvStructInfo,
  [Out] byte* pbEncoded,
   ref uint pcbEncoded);
        [DllImport("kernel32.dll")]
        public static extern bool SystemTimeToFileTime([In] IntPtr lpSystemTime,
   out FILETIME lpFileTime);
        [DllImport("Mssign32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int SignerFreeSignerContext(IntPtr SIGNER_CONTEXT);
        [DllImport("Mssign32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int SignerSignEx(
            uint flags,
                   IntPtr pSubjectInfo,        // SIGNER_SUBJECT_INFO
                   IntPtr pSignerCert,         // SIGNER_CERT
                   IntPtr pSignatureInfo,      // SIGNER_SIGNATURE_INFO
                   IntPtr pProviderInfo,       // SIGNER_PROVIDER_INFO
                   string pwszHttpTimeStamp,   // LPCWSTR
                   IntPtr psRequest,           // PCRYPT_ATTRIBUTES
                   IntPtr pSipData,            // LPVOID 
            IntPtr SIGNER_CONTEXT
                   );
        [DllImport("Mssign32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int SignerTimeStampEx(
        uint flags, IntPtr pSubjectInfo,        // SIGNER_SUBJECT_INFO
        string pwszHttpTimeStamp,   // LPCWSTR
        IntPtr psRequest,           // PCRYPT_ATTRIBUTES
        IntPtr pSipData, IntPtr ppSignerContext            // LPVOID 
        );

        [DllImport("Crypt32.DLL", EntryPoint = "CertCreateCertificateContext", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CertCreateCertificateContext(
            int dwCertEncodingType,
            byte[] pbCertEncoded,
            int cbCertEncoded);
        #endregion
        #region Structures
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public short wYear;
            public short wMonth;
            public short wDayOfWeek;
            public short wDay;
            public short wHour;
            public short wMinute;
            public short wSecond;
            public short wMilliseconds;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct CERT_ENHKEY_USAGE
        {
            public int cUsageIdentifier;
            public IntPtr rgpszUsageIdentifier;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct CRYPT_BIT_BLOB
        {
            public uint cbData;
            public IntPtr pbData;
            public uint cUnusedBits;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct CRYPT_DATA_BLOB
        {
            public uint cbData;
            [MarshalAs(UnmanagedType.LPArray)] public byte[] pbData;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct CRYPTOAPI_BLOB
        {
            [MarshalAs(UnmanagedType.I4)] public uint cbData;
            public byte* pbData;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct CERT_EXTENSION
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pszObjId;
            public bool fCritical;
            public CRYPTOAPI_BLOB Value;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct CERT_EXTENSIONS
        {
            public UInt32 cExtension;
            public IntPtr rgExtension;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CERT_BASIC_CONSTRAINTS_INFO
        {
            internal CRYPT_BIT_BLOB SubjectType;
            internal bool fPathLenConstraint;
            internal uint dwPathLenConstraint;
            internal uint cSubtreesConstraint;
            internal IntPtr rgSubtreesConstraint; // PCERT_NAME_BLOB
        };
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct CERT_ALT_NAME_INFO
        {
            public UInt32 cAltEntry;
            public IntPtr rgAltEntry;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct CERT_ALT_NAME_ENTRY
        {
            public UInt32 dwAltNameChoice;
            // since there is no direct translation from C-like unions in C#
            // make additional struct to represent union options.
            public CERT_ALT_NAME_UNION Value;
        }
        // create mapping to dwAltNameChoice
        public const UInt32 CERT_ALT_NAME_OTHER_NAME = 1;
        public const UInt32 CERT_ALT_NAME_RFC822_NAME = 2;
        public const UInt32 CERT_ALT_NAME_DNS_NAME = 3;
        public const UInt32 CERT_ALT_NAME_X400_ADDRESS = 4;
        public const UInt32 CERT_ALT_NAME_DIRECTORY_NAME = 5;
        public const UInt32 CERT_ALT_NAME_EDI_PARTY_NAME = 6;
        public const UInt32 CERT_ALT_NAME_URL = 7;
        public const UInt32 CERT_ALT_NAME_IP_ADDRESS = 8;
        public const UInt32 CERT_ALT_NAME_REGISTERED_ID = 9;

        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Auto)]
        public struct CERT_ALT_NAME_UNION
        {
            [FieldOffset(0)]
            public IntPtr pOtherName;
            [FieldOffset(0)]
            public IntPtr pwszRfc822Name;
            [FieldOffset(0)]
            public IntPtr pwszDNSName;
            [FieldOffset(0)]
            public CRYPTOAPI_BLOB DirectoryName;
            [FieldOffset(0)]
            public IntPtr pwszURL;
            [FieldOffset(0)]
            public IntPtr IPAddress;
            [FieldOffset(0)]
            public IntPtr pszRegisteredID;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct CRYPT_OBJID_BLOB
        {
            public uint cbData;
            public IntPtr pbData;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct CRYPT_ALGORITHM_IDENTIFIER
        {
            public IntPtr pszObjId;
            public CRYPT_OBJID_BLOB Parameters;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct CERT_PUBLIC_KEY_INFO
        {
            public CRYPT_ALGORITHM_IDENTIFIER Algorithm;
            public CRYPT_BIT_BLOB PublicKey;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct FILETIME
        {
            public uint DateTimeLow;
            public uint DateTimeHigh;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct CRYPT_INTEGER_BLOB
        {
            public UInt32 cbData;
            public byte* pbData;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct CERT_INFO
        {
            public uint dwVersion;
            public CRYPT_INTEGER_BLOB SerialNumber;
            public CRYPT_ALGORITHM_IDENTIFIER SignatureAlgorithm;
            public CERT_NAME_BLOB Issuer;
            public FILETIME NotBefore;
            public FILETIME NotAfter;
            public CERT_NAME_BLOB Subject;
            public CERT_PUBLIC_KEY_INFO SubjectPublicKeyInfo;
            public CRYPT_BIT_BLOB IssuerUniqueId;
            public CRYPT_BIT_BLOB SubjectUniqueId;
            public uint cExtension;
            public IntPtr rgExtension;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct CERT_CONTEXT
        {
            public uint dwCertEncodingType;
            public IntPtr pbCertEncoded;
            public uint cbCertEncoded;
            public CERT_INFO* pCertInfo;
            public IntPtr hCertStore;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct CRYPT_KEY_PROV_INFO
        {
            //[MarshalAs(UnmanagedType.LPWStr)]
            public IntPtr pwszContainerName;
            // [MarshalAs(UnmanagedType.LPWStr)]
            public IntPtr pwszProvName;
            public int dwProvType;
            public int dwFlags;
            public int cProvParam;
            public CRYPT_KEY_PROV_PARAM rgProvParam;
            public int dwKeySpec;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct CRYPT_KEY_PROV_PARAM
        {
            public int dwParam;
            public IntPtr pbData;
            public int cbData;
            public int dwFlags;
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct SIGNER_FILE_INFO
        {
            /// DWORD->unsigned int
            public uint cbSize;

            /// LPCWSTR->WCHAR*
            public IntPtr pwszFileName;

            /// HANDLE->void*
            public System.IntPtr hFile;
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        internal struct SIGNER_SUBJECT_INFO
        {
            /// DWORD->unsigned int
            public uint cbSize;

            /// DWORD*
            public System.IntPtr pdwIndex;

            /// DWORD->unsigned int
            public uint dwSubjectChoice;

            /// SubjectChoiceUnion
            public IntPtr Union1;
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct SIGNER_CERT_STORE_INFO
        {
            public uint cbSize;
            public CERT_CONTEXT* pSigningCert;
            public uint dwCertPolicy;
            public IntPtr hCertStore;
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct SIGNER_SIGNATURE_INFO
        {
            public uint cbSize;
            public uint algidHash; // ALG_ID
            public uint dwAttrChoice;
            public IntPtr pAttrAuthCode;
            public IntPtr psAuthenticated; // PCRYPT_ATTRIBUTES
            public IntPtr psUnauthenticated; // PCRYPT_ATTRIBUTES
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct SIGNER_CERT
        {
            public uint cbSize;
            public uint dwCertChoice;
            public IntPtr pCertStoreInfo;
            public IntPtr hwnd;
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct SIGNER_CONTEXT
        {
            public uint cbSize;
            public uint cbBlob;
            public IntPtr Blob;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct CERT_NAME_BLOB
        {
            public uint cbData;
            public IntPtr pbData;
        }
        #endregion
    }
}
