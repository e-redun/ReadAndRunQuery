using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.Helpers;
using Lib.Models;

namespace Lib.DataAccess
{
    public class SqlConnector : IDataConnection 
    {
        /// <summary>
        /// Имя строки подключния в файле App.config
        /// </summary>
        private const string db = "Employees";

        /// <summary>
        /// Проверяет наличие базы данных и, в случае отсутствия, создает.
        /// </summary>
        public void CreateDatabase()
        {
            TryCatchShell(GlobalConfig.CreateDatabaseSqlFile);
        }

        /// <summary>
        /// Проверяет наличие таблицы Employees и, в случае отсутствия, создает.
        /// </summary>
        public void CreateTable()
        {
            TryCatchShell(GlobalConfig.CreateTableSqlFile);
        }

        /// <summary>
        /// Проверяет наличие хранимой процедуры и, в случае отсутствия, создает.
        /// </summary>
        public void CreatePersonInsertSP()
        {
            TryCatchShell(GlobalConfig.CreatePersonInsertSPFile);
        }


        /// <summary>
        /// Заполняет БД данными из текстового файла
        /// </summary>
        public void FillDbFromPersonDataFile()
        {
            // считывание из файла данных
            List<string> personData = "textData".FullFilePath("personData.csv")
                                                .GetFileLines();

            // перебор строк файла с пропуском шапки
            foreach (string line in personData.Skip(1))
            {
                // получение экземпляра PersonModel
                PersonModel person = line.Split(',').CreatePerson();

                // добавление в БД
                TryCatchShell(GlobalConfig.InsertPersonSqlFile, person);
            }
        }

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
        public void TryCatchShell(string queryFileName)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(GlobalConfig.ConString(db)))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = "sqlQueries".FullFilePath(queryFileName)
                                                         .GetFileContent();
                    sqlCommand.Connection = (SqlConnection)connection;
                    sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Метод-оболочка имплементирующий конструкцию try/catch. Перегрузка
        /// </summary>
        /// <param name="queryFileName">Имя файла с SQL-запросом</param>
        public void TryCatchShell(string queryFileName, PersonModel person)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(GlobalConfig.ConString(db)))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand();
                    // Вариант 1
                    sqlCommand.CommandText = "Use MyWork " +
                                             "INSERT INTO dbo.Employees(FirstName, LastName, Age) " +
                                             "VALUES(@firstName, @lastName, @age)";
                    // Вариант 2
                    //sqlCommand.CommandText = "sqlQueries".FullFilePath(queryFileName)
                    //                                     .GetFileContent();

                    sqlCommand.Connection = (SqlConnection)connection;

                    sqlCommand.Parameters.Add(new SqlParameter("@firstName", person.FirstName));
                    sqlCommand.Parameters.Add(new SqlParameter("@lastName", person.LastName));
                    sqlCommand.Parameters.Add(new SqlParameter("@age", person.Age));

                    sqlCommand.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Метод-оболочка имплементирующий конструкцию try/catch. Перегрузка.
        /// </summary>
        /// <param name="queryFileName">Имя файла с SQL-запросом</param>
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
