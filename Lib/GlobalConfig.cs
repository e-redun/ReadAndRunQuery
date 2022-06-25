using Lib.DataAccess;
using System.Configuration;

namespace Lib
{
    /// <summary>
    /// Класс глобальных настроек
    /// </summary>
    public static class GlobalConfig
    {
        public const string InputFile = "input.txt";
        public const string OutputFile = "output.csv";
        public const string CreateDatabaseSqlFile = "CreateDatabase.sql";
        public const string CreateTableSqlFile = "CreateTable.sql";
        public const string CreatePersonInsertSPFile = "CreatePersonInsertSP.sql";
        public const string InsertPersonSqlFile = "InsertPerson.sql";


        public static IDataConnection Connection { get; private set; }

        /// <summary>
        /// Инициализирует соединение
        /// </summary>
        /// <param name="dbType">Тип соединения</param>
        public static void InitializeConnections(DatabaseAccessType dbType)
        {
            switch (dbType)
            {
                case DatabaseAccessType.Sql:
                    Connection = new SqlConnector();
                    break;
            }
        }

        /// <summary>
        /// Возвращает строку подключения
        /// </summary>
        /// <param name="name">Имя подключения</param>
        /// <returns>Строка подключения</returns>
        public static string ConString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
