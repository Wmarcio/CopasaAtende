using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Business.Interfaces
{
    /// <summary>
    /// Interface Rule - CertidaoNegativaDebito.
    /// </summary>
    public interface ICertidaoNegativaDebitoRule
    {
        /// <summary>
        /// Busca os dados da certidão negativa de débito
        /// </summary>
        /// <param name="sCN6ISCNSend"></param>
        /// <returns></returns>
        BaseResponse SCN6ISCN(SCN6ISCNSend sCN6ISCNSend);
    }
}
