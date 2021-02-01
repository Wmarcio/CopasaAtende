using Copasa.Atende.Model.Core;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Infrastructure
{
    /// <summary>
    /// Interface IBrokerRepository
    /// </summary>
    public interface IBrokerRepository<T> where T : class
    {
        /// <summary>
        /// Conectar com o Sicom via Broker
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="log"></param>
        /// <returns>T</returns>
        BaseResponse Connect(BaseModel obj,ILog log);


        /// <summary>
        /// Conectar com o Sicom via Broker
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>T</returns>
        BaseResponse Connect(BaseModel obj);

        /// <summary>
        /// Conectar com o Sicom via Broker
        /// </summary>
        /// <returns>T</returns>
        BaseResponse Connect();
    }
}
