using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace FileSigner
{
    public static class Extensions
    {
        private static bool CheckFile(string filename)
        {
            bool ret = true;

            if (!File.Exists(filename))
            {
                System.Console.WriteLine($"Файл {filename} не существует или недостаточно прав для его чтения");
                ret = false;
            }

            return ret;
        }
        
        public static bool ValidateOptionsSign(this OptionsSign options)
        {
            return CheckFile(options.FileName);
        }

        public static bool ValidateOptionsVerify(this OptionsVerify options)
        {
            bool ret = true;
            ret = CheckFile(options.FileName) && ret;
            ret = CheckFile(options.SignatureFileName) && ret;
            return ret;
        }

        public static bool ValidateOptionsVerify(this OptionsSignInfo options)
        {
            return CheckFile(options.SignatureFileName);
        }

        public static X509Certificate2 FindCertificate(StoreName certificateStore, StoreLocation certificateLocation, string certificateSerialNumber)
        {
            X509Store x509CertificateStore = new X509Store(certificateStore, certificateLocation);
            x509CertificateStore.Open(OpenFlags.ReadOnly);
            
            X509Certificate2Collection x509Certificate2Collection = x509CertificateStore.Certificates.Find(X509FindType.FindBySerialNumber, certificateSerialNumber, false);

            X509Certificate2 certificate = null;
            if (x509Certificate2Collection.Count == 1)
            {
                certificate = new X509Certificate2(x509Certificate2Collection[0]);
            }
            x509CertificateStore.Close();
            
            return certificate;
        }
    }
}
