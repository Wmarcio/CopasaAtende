using Copasa.Atende.Model.Enumerador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Repository.Infrastructure
{
    /// <summary>
    /// Interface IBrokerRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDBrokerRepository<T> where T : class
    {
        /// <summary>
        /// Donwload dados por parametro
        /// </summary>
        /// <param name="parametros"></param>
        /// <param name="ambiente"></param>
        /// <returns></returns>
        string Download(string parametros, EnvironmentEnum ambiente);
    }
}
