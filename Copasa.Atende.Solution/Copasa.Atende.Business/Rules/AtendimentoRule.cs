using Copasa.Atende.Business.Core;
using Copasa.Atende.Business.Interfaces;
using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Business.Rules
{
    /// <summary>
    /// Rule - Atendimento
    /// </summary>
    public class AtendimentoRule : BaseRule, IAtendimentoRule
    {
        private IISSCN6ISAARepository _iSSCN6ISAARepository;
        private IISSCN4ISCSRepository _iSSCN4ISCSRepository;
        private IISSCN6ISDLRepository _iSSCN6ISDLRepository;
        private IISSCN7ISOPRepository _iSSCN7ISOPRepository;
        private IISSCNISPS1Repository _iSSCNISPS1Repository;
        private IISSCN4CRUNRepository _iSSCN4CRUNRepository;
        private IServicoOperacionalRule _servicoOperacionalRule;
        private IMensagemRepository _mensagemRepository;
        private ILog _log;

        /// <summary>
        /// Construtor AtendimentoRule.
        /// </summary>
        /// <param name="iSSCN6ISAARepository">IISSCN6ISAARepository.</param>
        /// <param name="iSSCN4ISCSRepository">IISSCN4ISCSRepository.</param>
        /// <param name="iSSCN6ISDLRepository">IISSCN6ISDLRepository.</param>
        /// <param name="iSSCN7ISOPRepository">IISSCN7ISOPRepository.</param>
        /// <param name="iSSCNISPS1Repository">IISSCNISPS1Repository.</param>
        /// <param name="iSSCN4CRUNRepository">IISSCN4CRUNRepository.</param>
        /// <param name="servicoOperacionalRule">IServicoOperacionalRule.</param>
        /// <param name="mensagemRepository">IMensagemRepository</param>
        /// <param name="log">ILog</param>
        public AtendimentoRule(
            IISSCN6ISAARepository iSSCN6ISAARepository,
            IISSCN4ISCSRepository iSSCN4ISCSRepository,
            IISSCN6ISDLRepository iSSCN6ISDLRepository,
            IISSCN7ISOPRepository iSSCN7ISOPRepository,
            IISSCNISPS1Repository iSSCNISPS1Repository,
            IISSCN4CRUNRepository iSSCN4CRUNRepository,
            IServicoOperacionalRule servicoOperacionalRule,
            IMensagemRepository mensagemRepository,
            ILog log)
        {
            _iSSCN6ISAARepository = iSSCN6ISAARepository;
            _iSSCN4ISCSRepository = iSSCN4ISCSRepository;
            _iSSCN6ISDLRepository = iSSCN6ISDLRepository;
            _iSSCN7ISOPRepository = iSSCN7ISOPRepository;
            _iSSCNISPS1Repository = iSSCNISPS1Repository;
            _iSSCN4CRUNRepository = iSSCN4CRUNRepository;
            _servicoOperacionalRule = servicoOperacionalRule;
            _mensagemRepository = mensagemRepository;
            _log = log;
        }
        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Lista agências atendimento
        /// </summary>
        /// <param name="_sCN6ISAASend"></param>
        /// <returns></returns>
        public BaseResponse SCN6ISAA(SCN6ISAASend _sCN6ISAASend)
        {
            SCN6ISAAReceive sCN6ISAAReceive = (SCN6ISAAReceive)_iSSCN6ISAARepository.Connect(_sCN6ISAASend);

            if (sCN6ISAAReceive.codigoErro != "0")
            {
                sCN6ISAAReceive.descricaoRetorno = _mensagemRepository.geraMensagem(string.Format("M00{0}", sCN6ISAAReceive.codigoErro));
            }

            BaseResponse response = new BaseResponse();
            response.Model = sCN6ISAAReceive;
            return response;
        }

        /// <summary>
        /// Busca informações de matrícula
        /// </summary>
        /// <param name="sCN4ISCSSend"></param>
        /// <returns></returns>
        public BaseResponse SCN4ISCS(SCN4ISCSSend sCN4ISCSSend)
        {
            SCN4ISCSReceive sCN4ISCSReceive = (SCN4ISCSReceive)_iSSCN4ISCSRepository.Connect(sCN4ISCSSend);
            BaseResponse response = new BaseResponse();
            response.Model = sCN4ISCSReceive;
            return response;
        }

        /// <summary>
        /// Lista hidrômetros de uma matrícula
        /// </summary>
        /// <param name="sCNISPS1Send"></param>
        /// <returns></returns>
        public BaseResponse SCNISPS1(SCNISPS1Send sCNISPS1Send)
        {
            SCNISPS1Receive sCNISPS1Receive = (SCNISPS1Receive)_iSSCNISPS1Repository.Connect(sCNISPS1Send);
            SCN4ISCSSend sCN4ISCSSend = new SCN4ISCSSend();
            sCN4ISCSSend.matricula = sCNISPS1Send.matricula;
            sCN4ISCSSend.codigoServico = "6770100";
            SCN4ISCSReceive sCN4ISAEReceive = (SCN4ISCSReceive)SCN4ISCS(sCN4ISCSSend).Model;
            sCNISPS1Receive.setValues(sCN4ISAEReceive);
            BaseResponse response = new BaseResponse();
            response.Model = sCNISPS1Receive;
            return response;
        }

        /// <summary>
        /// Calendário faturamento
        /// </summary>
        /// <param name="sCN6ISDLSend"></param>
        /// <returns></returns>
        public BaseResponse SCN6ISDL(SCN6ISDLSend sCN6ISDLSend)
        {
            SCN6ISDLReceive sCN4ISAEReceive = (SCN6ISDLReceive)_iSSCN6ISDLRepository.Connect(sCN6ISDLSend);
            BaseResponse response = new BaseResponse();
            response.Model = sCN4ISAEReceive;
            return response;
        }

        /// <summary>
        /// Onde pagar a conta
        /// </summary>
        /// <param name="sCN7ISOPSend"></param>
        /// <returns></returns>
        public BaseResponse SCN7ISOP(SCN7ISOPSend sCN7ISOPSend)
        {
            SCN7ISOPReceive sCN7ISOPReceive = (SCN7ISOPReceive)_iSSCN7ISOPRepository.Connect(sCN7ISOPSend);
            BaseResponse response = new BaseResponse();
            response.Model = sCN7ISOPReceive;
            return response;
        }

        /// <summary>
        /// Unidade de Destino
        /// </summary>
        /// <param name="sCN4CRUNSend"></param>
        /// <returns></returns>
        public BaseResponse SCN4CRUN(SCN4CRUNSend sCN4CRUNSend)
        {
            SCN4CRUNReceive sCN4CRUNReceive = (SCN4CRUNReceive)_iSSCN4CRUNRepository.Connect(sCN4CRUNSend);
            BaseResponse response = new BaseResponse();
            response.Model = sCN4CRUNReceive;
            return response;
        }
    }
}