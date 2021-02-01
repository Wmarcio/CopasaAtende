using Copasa.Atende.Business.Interfaces;
using Copasa.Atende.Facade.Interfaces;
using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using System;

namespace Copasa.Atende.Facade.Facades
{
    /// <summary>
    /// Construtor AtendimentoFacade.
    /// </summary>
    public class AtendimentoFacade : IAtendimentoFacade
    {
        private IAtendimentoRule _atendimentoRule;

        /// <summary>
        /// Construtor InformarLeituraFacade.
        /// </summary>
        /// <param name="atendimentoRule">IClienteRule.</param>
        public AtendimentoFacade(IAtendimentoRule atendimentoRule)
        {
            _atendimentoRule = atendimentoRule;
        }

        /// <summary>
        /// Lista agências atendimento
        /// </summary>
        /// <param name="_sCN6ISAASend"></param>
        /// <returns></returns>
        public BaseResponse SCN6ISAA(SCN6ISAASend _sCN6ISAASend)
        {
            return _atendimentoRule.SCN6ISAA(_sCN6ISAASend);
        }

        /// <summary>
        /// Busca informações de matrícula
        /// </summary>
        /// <param name="sCN4ISCSSend"></param>
        /// <returns></returns>
        public BaseResponse SCN4ISCS(SCN4ISCSSend sCN4ISCSSend)
        {
            return _atendimentoRule.SCN4ISCS(sCN4ISCSSend);
        }

        /// <summary>
        /// Calendário faturamento
        /// </summary>
        /// <param name="sCN6ISDLSend"></param>
        public BaseResponse SCN6ISDL(SCN6ISDLSend sCN6ISDLSend)
        {
            return _atendimentoRule.SCN6ISDL(sCN6ISDLSend);
        }

        /// <summary>
        /// Onde pagar a conta
        /// </summary>
        /// <param name="sCN7ISOPSend"></param>
        public BaseResponse SCN7ISOP(SCN7ISOPSend sCN7ISOPSend)
        {
            return _atendimentoRule.SCN7ISOP(sCN7ISOPSend);
        }

        /// <summary>
        /// Lista hidrômetros de uma matrícula
        /// </summary>
        /// <param name="sCNISPS1Send"></param>
        /// <returns></returns>
        public BaseResponse SCNISPS1(SCNISPS1Send sCNISPS1Send)
        {
            return _atendimentoRule.SCNISPS1(sCNISPS1Send);
        }

        /// <summary>
        /// Unidade de Destino
        /// </summary>
        /// <param name="sCN4CRUNSend"></param>
        /// <returns></returns>
        public BaseResponse SCN4CRUN(SCN4CRUNSend sCN4CRUNSend)
        {
            return _atendimentoRule.SCN4CRUN(sCN4CRUNSend);
        }
    }
}
