using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Facade.Interfaces
{
    /// <summary>
    /// Interface para recuperar certidão negativa de débito
    /// </summary>
    public interface ICertidaoNegativaDebitoFacade
    {
        /// <summary>
        /// Retorna os dados da certidão negativa de débito
        /// </summary>
        /// <param name="sCN6ISCNSend"></param>
        /// <returns></returns>
        BaseResponse SCN6ISCN(SCN6ISCNSend sCN6ISCNSend);
    }
}
