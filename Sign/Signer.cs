using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using GostCryptography.Pkcs;
using System.Security.Cryptography.Pkcs;

namespace FileSigner
{
    internal class Signer
    {      
        internal byte[] Sign(string fileName, X509Certificate2 certificate)
        {
            var file = System.IO.File.ReadAllBytes(fileName);

            var signature = SignMessage(certificate, file);

            return signature;
        }

        internal bool Verify(string fileName, string signature)
        {
            var message = System.IO.File.ReadAllBytes(fileName);
            var detachedSign = System.IO.File.ReadAllBytes(signature);

            return VerifyMessage(message, detachedSign);
        }

        private static byte[] SignMessage(X509Certificate2 certificate, byte[] message)
        {
            // Создание объекта для подписи сообщения
            var signedCms = new GostSignedCms(new ContentInfo(message), true);

            // Создание объект с информацией о подписчике
            var signer = new CmsSigner(certificate);

            // Включение информации только о конечном сертификате (только для теста)
            signer.IncludeOption = X509IncludeOption.EndCertOnly;

            // Создание подписи для сообщения CMS/PKCS#7
            signedCms.ComputeSignature(signer);

            // Создание подписи CMS/PKCS#7
            return signedCms.Encode();
        }

        private static bool VerifyMessage(byte[] message, byte[] detachedSignature)
        {
            // Создание объекта для проверки подписи сообщения
            var signedCms = new GostSignedCms(new ContentInfo(message), true);

            // Чтение подписи CMS/PKCS#7
            signedCms.Decode(detachedSignature);
            try
            {
                // Проверка подписи CMS/PKCS#7
                signedCms.CheckSignature(true);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public string[] SignInfo(string signature)
        {
            byte[] detachedSignature = System.IO.File.ReadAllBytes(signature);

            // Создание объекта для проверки подписи сообщения
            var signedCms = new GostSignedCms();

            // Чтение подписи CMS/PKCS#7
            signedCms.Decode(detachedSignature);

            string[] result = new string[5];

            result[0] = $"SerialNumber[{signedCms.Certificates[0].SerialNumber.Replace("\"", "")}]";
            result[1] = $"Subject[{signedCms.Certificates[0].Subject.Replace("\"", "")}]";
            result[2] = $"Issuer[{signedCms.Certificates[0].Issuer.Replace("\"", "")}]";
            result[3] = $"NotBefore[{signedCms.Certificates[0].NotBefore}]";
            result[4] = $"NotAfter[{signedCms.Certificates[0].NotAfter}]";
           
            return result;
        }
    }
}
