using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;

namespace FileSigner
{
    public class OptionsHelp
    {
        public static void DisplayHelp<T>(ParserResult<T> result, IEnumerable<Error> errs)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var helpText = HelpText.AutoBuild(result, h =>
            {
                h.AdditionalNewLineAfterOption = false; //remove the extra newline between options
                h.Heading = "FileSigner 1.0.0.0"; //change header
                h.Copyright = "Copyright (c) 2021 LLC Russalt"; //change copyrigt text
                return HelpText.DefaultParsingErrorsHandler(result, h);
            }, e => e);
            Console.WriteLine(helpText);
            Console.WriteLine("Программа FileSigner используется для подписания файла ЭЦП в формате PKS#7");
            Console.WriteLine("Для подписания укажите ключевое слово sign. В результате создается отсоединенная электронная подпись с именем подписываемого файла и расширением .sig");
            Console.WriteLine("Для проверки напишите ключевое слово verify. Проверяется электронная подпись по указанному документу");
            Console.WriteLine("Параметры подписания (sign)");
            Console.WriteLine(" -f, --file <путь_к_файлу> Полное имя файла");
            Console.WriteLine("");
            Console.WriteLine(" -l, --location Расположение хранилища сертификата. Возможные значения:");
            Console.WriteLine("  CurrentUser - Хранилище сертификатов текущего пользователя");
            Console.WriteLine("  LocalMachine - Хранилище сертификатов локального компьютера");
            Console.WriteLine("");
            Console.WriteLine(" -s, --store Имя хранилища. Возможные значения:");
            Console.WriteLine("  AddressBook - Другие пользователи");
            Console.WriteLine("  AuthRoot - Сторонние центры сертификации");
            Console.WriteLine("  CertificateAuthority - Промежуточные центры сертификации");
            Console.WriteLine("  Disallowed - Отозванные сертификаты");
            Console.WriteLine("  My - Личные");
            Console.WriteLine("  Root - Доверенные корневые центры сертификации");
            Console.WriteLine("  TrustedPeople - Доверенные лица");
            Console.WriteLine("  TrustedPublisher - Доверенные издатели");
            Console.WriteLine("");
            Console.WriteLine(" -n, --number Серийный номер сертификата");
            Console.WriteLine("");
            Console.WriteLine("Параметры проверки (verify)");
            Console.WriteLine(" -f, --file <путь_к_файлу> Полное имя подписанного файла");
            Console.WriteLine(" -s, --signature <путь_к_файлу_подписи> Отсоединенный файл подписи");
        }
    }
}
