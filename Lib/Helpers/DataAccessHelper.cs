using Lib.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Helpers
{
    /// <summary>
    /// Вспомогательный класс доступа к данным
    /// </summary>
    public static class DataAccessHelper
    {
        /// <summary>
        /// Возвращает полное имя файла
        /// </summary>
        /// <param name="folderAlias">Алиас папки в файле App.config</param>
        /// <param name="fileName">Имя файла с расширением</param>
        /// <returns>Полное имя файла</returns>
        public static string FullFilePath(this string folderAlias, string fileName)
        {
            return ConfigurationManager.AppSettings[folderAlias] + "\\" + fileName;
        }

        /// <summary>
        /// Загружает файл
        /// </summary>
        /// <param name="fullFileName">Полное имя файла</param>
        /// <returns>Список строк файла</returns>
        public static string GetFileContent(this string fullFileName)
        {
            if (File.Exists(fullFileName))
            {
                return File.ReadAllText(fullFileName);
            }

            return "";
        }

        /// <summary>
        /// Возвращает содержимое фала как набор строк
        /// </summary>
        /// <param name="fullFileName">Поное имя файла</param>
        /// <returns>Содержимое фала как набор строк</returns>
        public static List<string> GetFileLines(this string fullFileName)
        {
            if (File.Exists(fullFileName))
            {
                return File.ReadAllLines(fullFileName).ToList();
            }
            else
            {
                return new List<string>();
            }
        }

        /// <summary>
        /// Сохраняет результат запроса в файл
        /// </summary>
        /// <param name="result">Результат запроса</param>
        /// <param name="fileName">Имя фала</param>
        public static void SaveResult(this List<List<string>> result, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (List<string> row in result)
            {
                string line = row.ConvertRowToLine();

                lines.Add(line);
            }

            File.WriteAllLines("textData".FullFilePath(fileName), lines);
        }

        /// <summary>
        /// Получает данные из ответа SQLSERVER и
        /// возвращает в виде коллекции строк данных
        /// </summary>
        /// <param name="dataReader">Объект SqlDataReader для возврата результатов запроса</param>
        /// <returns>Коллекция List строк данных</returns>
        public static List<List<string>> GetData(this SqlDataReader dataReader)
        {
            List<List<string>> output = new List<List<string>>();

            if (dataReader.HasRows) // если есть данные
            {
                // формирование списка полей ("шапки")
                List<string> fields = new List<string>();

                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    fields.Add(dataReader.GetName(i));
                }

                output.Add(fields);

                while (dataReader.Read()) // построчно считываем данные
                {
                    List<string> record = new List<string>();

                    for (int i = 0; i < fields.Count; i++)
                    {
                        record.Add(dataReader.GetValue(i).ToString());
                    }

                    output.Add(record);
                }
            }

            return output;
        }

        /// <summary>
        /// Конвертирует коллекцию данных в строку для поледующего сохранения в формате csv
        /// </summary>
        /// <param name="row">Ряд данных</param>
        /// <returns>Cтроку для сохранения в формате csv</returns>
        public static string ConvertRowToLine(this List<string> row)
        {
            string output = "";

            foreach (string item in row)
            {
                output += item + ",";
            }

            //удаление последнего символа (запятой)
            output = output.Remove(output.Length - 1, 1);
            //output.Last()
            return output;
        }
    }
}