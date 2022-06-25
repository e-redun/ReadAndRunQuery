using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Lib.Helpers;

namespace Lib.DataAccess
{
    public class SqlConnector : IDataConnection 
    {
        /// <summary>
        /// Имя строки подключния в файле App.config
        /// </summary>
        private const string db = "Employees";
        
        /// <summary>
        /// Делает запрос из файла input.txt
        /// </summary>
        public string MakeQueryFromInputTxt(ref List<List<string>> result)
        {
            return TryCatchShell(GlobalConfig.InputFile, ref result);
        }
        

        /// <summary>
        /// Метод-оболочка имплементирующий конструкцию try/catch
        /// </summary>
        /// <param name="queryFileName">Имя файла с SQL-запросом</param>
        /// <param name="result">Результат выполнения запроса</param>
        /// <returns>Статус выполнения запроса</returns>
        public string TryCatchShell(string queryFileName, ref List<List<string>> result)
        {
            string output;
            try
            {
                using (IDbConnection connection = new SqlConnection(GlobalConfig.ConString(db)))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = "textData".FullFilePath(queryFileName)
                                                         .GetFileContent();

                    sqlCommand.Connection = (SqlConnection)connection;

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        result = reader.GetData();
                    }

                    output = "Запрос выполнен успешно\n";
                    
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                output = "Запрос выполнен с ошибками!!!\n";
                output += ex.Message;
            }

            return output;
        }
    }
}
