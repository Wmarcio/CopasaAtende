using Copasa.Atende.Business.Interfaces;
using Copasa.Atende.Facade.Interfaces;
using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Facade.Facades
{
    /// <summary>
    /// Facade - Serviços operacionais.
    /// </summary>
    public class ServicoOperacionalFacade : IServicoOperacionalFacade
    {
        private IServicoOperacionalRule _servicoOperacionalRule;

        /// <summary>
        /// Construtor ServicoOperacionalFacade.
        /// </summary>
        /// <param name="servicoOperacionalRule">IClienteRule.</param>
        public ServicoOperacionalFacade(IServicoOperacionalRule servicoOperacionalRule)
        {
            _servicoOperacionalRule = servicoOperacionalRule;
        }

        /// <summary>
        /// Gera eventos prioridade.
        /// </summary>
        public BaseResponse SCN4CRAL(SCN4CRALSend sCN4CRALSend)
        {
            return _servicoOperacionalRule.SCN4CRAL(sCN4CRALSend);
        }

        /// <summary>
        /// Busca baixas de OS no Sicom para atualizar o Dynamics 365.
        /// </summary>
        public BaseResponse AtualizaDynamicsBaxiasOS()
        {
            return _servicoOperacionalRule.AtualizaDynamicsBaxiasOS();
        }

        /// <summary>
        /// Busca serviço gerados no Sicom para atualizar o Dynamics 365 .
        /// </summary>
        public BaseResponse AtualizaDynamicsOSGeradas()
        {
            return _servicoOperacionalRule.AtualizaDynamicsOSGeradas();
        }

        /// <summary>
        /// Cria solicitação de serviço.
        /// </summary>
        public BaseResponse SCN4CRSS(SCN4CRSSSend sCN4CRSSSend)
        {
            return _servicoOperacionalRule.SCN4CRSS(sCN4CRSSSend);
        }

        /// <summary>
        /// Busca OS's de uma solicitação de serviço.
        /// </summary>
        public BaseResponse SCN4ISOR(SCN4ISORSend sCN4ISORSend)
        {
            return _servicoOperacionalRule.SCN4ISOR(sCN4ISORSend);
        }

        /// <summary>
        /// Busca solicitações de serviços de uma matrícula.
        /// </summary>
        public BaseResponse SCN4ISSS(SCN4ISSSSend sCN4ISSSSend)
        {
            return _servicoOperacionalRule.SCN4ISSS(sCN4ISSSSend);
        }

        /// <summary>
        /// Cancela Solicitação de serviço
        /// </summary>
        /// <param name="sCN4CASSSend"></param>
        /// <returns></returns>
        public BaseResponse SCN4CASS(SCN4CASSSend sCN4CASSSend)
        {
            return _servicoOperacionalRule.SCN4CASS(sCN4CASSSend);
        }

        /// <summary>
        /// Gera OS extra para SS existente
        /// </summary>
        /// <param name="sCN4CREXSend"></param>
        public BaseResponse SCN4CREX(SCN4CREXSend sCN4CREXSend)
        {
            return _servicoOperacionalRule.SCN4CREX(sCN4CREXSend);
        }

        /// <summary>
        /// Gera alteração de economias
        /// </summary>
        /// <param name="sCN4ISAESend"></param>
        /// <returns></returns>
        public BaseResponse SCN4ISAE(SCN4ISAESend sCN4ISAESend)
        {
            return _servicoOperacionalRule.SCN4ISAE(sCN4ISAESend);
        }

    }
}
