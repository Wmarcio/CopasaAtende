using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Facade.Interfaces
{
    /// <summary>
    /// Interface Facade - Vazamento.
    /// </summary>
    public interface IVazamentoFacade
    {
        /// <summary>
        /// Vazamento na rua - SCN4ISVR
        /// </summary>
        BaseResponse SCN4ISVR(SCN4ISVRSend _sCN4ISVRSend);

        /// <summary>
        /// Vazamento no imovel - SCN4ISVI.
        /// </summary>
        BaseResponse SCN4ISVI(SCN4ISVISend sCN4ISVISend);
    }
}
