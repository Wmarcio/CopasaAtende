using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Business.Interfaces
{
    /// <summary>
    /// Interface Rule - Vazamento.
    /// </summary>
    public interface IVazamentoRule
    {
        /// <summary>
        /// Vazamento na rua - SCN4ISVR.
        /// </summary>
        BaseResponse SCN4ISVR(SCN4ISVRSend sCN4ISVRSend);

        /// <summary>
        /// Vazamento no imovel - SCN4ISVI.
        /// </summary>
        BaseResponse SCN4ISVI(SCN4ISVISend sCN4ISVISend);
    }
}
