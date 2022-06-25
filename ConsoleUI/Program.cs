using Lib;
using Lib.Helpers;
using System;
using System.Collections.Generic;

namespace ReadAndRunQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            // инициализация соединения
            GlobalConfig.InitializeConnections(DatabaseAccessType.Sql);

            // обработка input.txt
            ProcessInputTxt();
        }

        private static void ProcessInputTxt()
        {
            // предложение нажать клавишу Y/N
            string prompt = "";
            prompt += "Нажмите одну из клавиш Y или N" + "\n";
            prompt += "[Y|y] - cчитать запрос из файла Input.txt" + "\n";
            prompt += "[N|n] - закончить работу с программой" + "\n";

            // статус выполнения запроса 
            string state = "";

            while (true)
            {
                Console.Clear();

                if (!string.IsNullOrEmpty(state))
                {
                    // вывод статуса выполнения запроса на экран
                    Console.WriteLine(state);
                    Console.WriteLine();

                    // вывод содержимого файла output.txt на экран
                    string output = "textData".FullFilePath("output.txt").GetFileContent();
                    Console.WriteLine(output);

                    Console.WriteLine();
                }

                // предложение нажать клавишу
                Console.WriteLine(prompt);

                // считывание нажатой клавиши
                var key = Console.ReadKey();

                // если нажата N
                if (key.Key == ConsoleKey.N)
                {
                    // выход из программы
                    break;
                }

                // если нажата Y
                if (key.Key == ConsoleKey.Y)
                {
                    // результат запоса
                    List<List<string>> result = new List<List<string>>();

                    // сделать запрос из файла input.txt
                    state = GlobalConfig.Connection.MakeQueryFromInputTxt(ref result);

                    // сохранение результата запроса в файл output.txt
                    result.SaveResult("output.txt");
                }

            }
            Console.Clear();
            Console.WriteLine("Конец программы");
        }
    }
}
