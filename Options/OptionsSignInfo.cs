using CommandLine;
using System.Security.Cryptography.X509Certificates;

namespace FileSigner
{
    [Verb("info")]
    public class OptionsSignInfo
    {
        [Option('s', "signature", Required = true, HelpText = "Отсоединенный файл подписи")]
        public string SignatureFileName { get; set; }
    }
}
