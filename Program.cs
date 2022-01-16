using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using CommandLine;

namespace FileSigner
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            try
            {
                var parser = new CommandLine.Parser(with => with.HelpWriter = null);
                var parserResult = parser.ParseArguments<OptionsSign, OptionsVerify, OptionsSignInfo>(args);
                parserResult.WithNotParsed(errs => OptionsHelp.DisplayHelp(parserResult, errs));
                return parserResult.MapResult(
                    (OptionsSign o) => RunSign(o),
                    (OptionsVerify o) => RunVerify(o),
                    (OptionsSignInfo o) => RunInfo(o),
                    errs => exitProgram(errs));                
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return -1;
            }
        }

        static public int RunSign(OptionsSign options)
        {            
            if(options.ValidateOptionsSign())
            {
                X509Certificate2 certificate = Extensions.FindCertificate(options.CertificateStore, options.CertificateLocation, options.CertificateNumber);

                if(certificate == null)
                {
                    Console.WriteLine($"Сертификат c с параметрами {options.CertificateStore}, {options.CertificateLocation}, {options.CertificateNumber} не найден");
                    return -1;
                }
                Signer signer = new Signer();

                string signatureFileName = System.IO.Path.Combine(  
                                                System.IO.Path.GetTempPath(), 
                                                $"{System.IO.Path.GetFileNameWithoutExtension(options.FileName)}.sig");

                byte[] sign = signer.Sign(
                                    options.FileName,
                                    certificate);

                System.IO.File.WriteAllBytes(signatureFileName, sign);
                Console.WriteLine(signatureFileName);
                return 0;
            }
            return -1;
        }

        static public int RunVerify(OptionsVerify options)
        {
            if(options.ValidateOptionsVerify())
            {
                Signer signer = new Signer();
                bool result = signer.Verify(options.FileName, options.SignatureFileName);

                if (result)
                {
                    Console.WriteLine("Подпись проверена");
                    return 0;
                }
                else
                {
                    Console.WriteLine("Подпись недействительна");
                    return 1;
                }
            }
            return -1;
        }

        static public int RunInfo(OptionsSignInfo options)
        {
            if (options.ValidateOptionsVerify())
            {
                string[] info = new Signer().SignInfo(options.SignatureFileName);
                foreach (var item in info)
                {
                    Console.WriteLine(item);
                }
                return 0;
            }
            return -1;
        }

        static public int exitProgram(IEnumerable<Error> errs)
        {
            return -1;
        }
    }
}
