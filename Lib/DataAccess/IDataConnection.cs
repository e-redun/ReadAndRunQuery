using System.Collections.Generic;

namespace Lib
{
    /// <summary>
    /// Интерфейс взаимодействия с БД
    /// </summary>
    public interface IDataConnection
    {
        string MakeQueryFromInputTxt(ref List<List<string>> list);
    }
}
