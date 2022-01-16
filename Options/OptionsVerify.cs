using CommandLine;

namespace FileSigner
{
    [Verb("verify")]
    public class OptionsVerify
    {
        [Option('f', "file", Required = true, HelpText = "Имя подписанного файла")]
        public string FileName { get; set; }

        [Option('s', "signature", Required = true, HelpText = "Отсоединенный файл подписи")]
        public string SignatureFileName { get; set; }
    }
}
