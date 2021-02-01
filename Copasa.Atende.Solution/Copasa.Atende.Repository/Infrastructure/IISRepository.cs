using Copasa.Atende.Model.Core;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Infrastructure
{
    /// <summary>
    /// Interface IISRepository
    /// </summary>
    public interface IISRepository<T> where T : class
    {
        /// <summary>
        /// Conectar com o Sicom via Broker
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>T</returns>
        BaseModel Connect(BaseModel obj);

        /// <summary>
        /// Conectar com o Sicom via Broker
        /// </summary>
        /// <returns>T</returns>
        BaseModel Connect();

        /// <summary>
        /// Conectar com o Sicom via Broker
        /// </summary>
        /// <returns>T</returns>
        string GetEnvio();
    }
}
