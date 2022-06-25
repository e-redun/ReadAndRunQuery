using Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    /// <summary>
    /// Интерфейс взаимодействия с БД
    /// </summary>
    public interface IDataConnection
    {
        void CreateDatabase();

        void CreateTable();

        void CreatePersonInsertSP();

        void FillDbFromPersonDataFile();

        string MakeQueryFromInputTxt(ref List<List<string>> list);

    }
}
