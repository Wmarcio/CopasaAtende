using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Facade.Interfaces
{
    /// <summary>
    /// Interface Facade - Atendimento
    /// </summary>
    public interface IAtendimentoFacade
    {
        /// <summary>
        /// Lista agências atendimento
        /// </summary>
        /// <param name="_sCN6ISAASend"></param>
        /// <returns></returns>
        BaseResponse SCN6ISAA(SCN6ISAASend _sCN6ISAASend);

        /// <summary>
        /// Busca informações de matrícula
        /// </summary>
        /// <param name="sCN4ISCSSend"></param>
        /// <returns></returns>
        BaseResponse SCN4ISCS(SCN4ISCSSend sCN4ISCSSend);

        /// <summary>
        /// Calendário faturamento
        /// </summary>
        /// <param name="sCN6ISDLSend"></param>
        /// <returns></returns>
        BaseResponse SCN6ISDL(SCN6ISDLSend sCN6ISDLSend);

        /// <summary>
        /// Onde pagar a conta
        /// </summary>
        /// <param name="sCN7ISOPSend"></param>
        /// <returns></returns>
        BaseResponse SCN7ISOP(SCN7ISOPSend sCN7ISOPSend);

        /// <summary>
        /// Lista hidrômetros de uma matrícula
        /// </summary>
        /// <param name="sCNISPS1Send"></param>
        /// <returns></returns>
        BaseResponse SCNISPS1(SCNISPS1Send sCNISPS1Send);

        /// <summary>
        /// Unidade de Destino
        /// </summary>
        /// <param name="sCN4CRUNSend"></param>
        /// <returns></returns>
        BaseResponse SCN4CRUN(SCN4CRUNSend sCN4CRUNSend);
    }
}
