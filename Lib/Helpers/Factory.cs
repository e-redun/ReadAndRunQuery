using Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Helpers
{
    /// <summary>
    /// Класс генерации объектов
    /// </summary>
    public static class Factory
    {
        /// <summary>
        /// Создает и возвращает экземпляр человека PersonModel
        /// </summary>
        /// <param name="personData">Строковый массив данных чаловека</param>
        /// <returns>Экземпляр человека PersonModel</returns>
        public static PersonModel CreatePerson(this string[] personData)
        {
            return new PersonModel()
            {
                Id = int.Parse(personData[0]),
                FirstName = personData[1],
                LastName = personData[2],
                Age = int.Parse(personData[3])
            };
        }
    }
}
