using CommandLine;
using System.Security.Cryptography.X509Certificates;

namespace FileSigner
{
    [Verb("sign")]
    public class OptionsSign
    {
        [Option('f', "file", Required = true, HelpText = "Имя файла для подписи")]
        public string FileName { get; set; }

        [Option('n', "number", Required = true, HelpText = "Серийный номер сертификата")]
        public string CertificateNumber { get; set; }

        [Option('s', "store", Required = true, HelpText = "Имя хранилища сертификата")]
        public StoreName CertificateStore { get; set; }

        [Option('l', "location", Required = true, HelpText = "Расположение хранилища сертификата")]
        public StoreLocation CertificateLocation { get; set; }

    }
}
