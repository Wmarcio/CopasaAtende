using Copasa.Atende.Business.Core;
using Copasa.Atende.Business.Interfaces;
using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;
using System;

namespace Copasa.Atende.Business.Rules
{
    /// <summary>
    /// Rule - Religação.
    /// </summary>
    public class ReligacaoRule : BaseRule, IReligacaoRule
    {
        private IISSCN4ISRLRepository _isSCN4ISRLRepository;
        private IISSCN4ISCPRepository _iSSCN4ISCPRepository;
        private IISSCN4ISRERepository _iSSCN4ISRERepository;
        private IISSCN6ISFDRepository _iSSCN6ISFDRepository;
        private IISSCN6ISCBRepository _iSSCN6ISCBRepository;
        private IMensagemRepository _mensagemRepository;
        private IServicoOperacionalRule _servicoOperacionalRule;
        private ILog _log;

        /// <summary>
        /// Construtor LoteOrdemServicoRule.
        /// </summary>
        /// <param name="isSCN4ISRLRepository">IISSCN4ISRLRepository.</param>
        /// <param name="iSSCN4ISCPRepository">IISSCN4ISCPRepository</param>
        /// <param name="iSSCN4ISRERepository">IISSCN4ISRERepository</param>
        /// <param name="iSSCN6ISFDRepository">IISSCN6ISFDRepository</param>
        /// <param name="iSSCN6ISCBRepository">IISSCN6ISCBRepository</param>
        /// <param name="mensagemRepository">IMensagemRepository</param>
        /// <param name="servicoOperacionalRule">IServicoOperacionalRule.</param>
        /// <param name="log">ILog</param>
        public ReligacaoRule(
            IISSCN4ISRLRepository isSCN4ISRLRepository,
            IISSCN4ISCPRepository iSSCN4ISCPRepository,
            IISSCN4ISRERepository iSSCN4ISRERepository,
            IISSCN6ISFDRepository iSSCN6ISFDRepository,
            IISSCN6ISCBRepository iSSCN6ISCBRepository,
            IMensagemRepository mensagemRepository,
            IServicoOperacionalRule servicoOperacionalRule,
            ILog log)
        {
            _isSCN4ISRLRepository = isSCN4ISRLRepository;
            _iSSCN4ISCPRepository = iSSCN4ISCPRepository;
            _iSSCN4ISRERepository = iSSCN4ISRERepository;
            _iSSCN6ISFDRepository = iSSCN6ISFDRepository;
            _iSSCN6ISCBRepository = iSSCN6ISCBRepository;
            _mensagemRepository = mensagemRepository;
            _servicoOperacionalRule = servicoOperacionalRule;
            _log = log;
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS religação";
        }

        /// <summary>
        /// Religação - Busca dados para religação
        /// </summary>
        public BaseResponse SCN4ISRL(SCN4ISRLSend _sCN4ISRLSend)
        {
            SCN4ISRLReceive sCN4ISRLReceive = (SCN4ISRLReceive)_isSCN4ISRLRepository.Connect(_sCN4ISRLSend);
            _log.AddLog("  Retorno SCN4ISRL:" + sCN4ISRLReceive.ToString());

            int codigoRetorno = 0;
            try
            {
                if (!"".Equals(sCN4ISRLReceive.descricaoRetorno))
                    codigoRetorno = int.Parse(sCN4ISRLReceive.descricaoRetorno.Trim());
            }
            catch (Exception) { }

            // Chamar programa SCN4CRAL para gerar evento de prioridade para esta SS
            if (codigoRetorno == 0038 && !_sCN4ISRLSend.protocolo.Equals(sCN4ISRLReceive.protocoloSS)
                && !"".Equals(_sCN4ISRLSend.protocolo) && !"".Equals(sCN4ISRLReceive.protocoloSS)
                && !"".Equals(sCN4ISRLReceive.dataPrevisaoSS) && !"0".Equals(sCN4ISRLReceive.dataPrevisaoSS))
            {
                _servicoOperacionalRule.executaReiteracao(sCN4ISRLReceive.protocoloSS, _sCN4ISRLSend.protocolo, _sCN4ISRLSend.Origem, sCN4ISRLReceive.dataPrevisaoSS);
            }

            if (sCN4ISRLReceive.descricaoRetorno != "0" && sCN4ISRLReceive.descricaoRetorno != "")
            {
                sCN4ISRLReceive.descricaoRetorno = _mensagemRepository.geraMensagem(string.Format("M00{0}", sCN4ISRLReceive.descricaoRetorno));
            }

            BaseResponse response = new BaseResponse();
            response.Model = sCN4ISRLReceive;
            return response;
        }

        /// <summary>
        /// Busca dados parcelamento religação
        /// </summary>
        /// <param name="_sCN4ISCPSend"></param>
        /// <returns></returns>
        public BaseResponse SCN4ISCP(SCN4ISCPSend _sCN4ISCPSend)
        {
            SCN4ISCPReceive sCN4ISCPReceive = (SCN4ISCPReceive)_iSSCN4ISCPRepository.Connect(_sCN4ISCPSend);

            if (sCN4ISCPReceive.codigoRetornoSicom != "0")
            {
                sCN4ISCPReceive.descricaoRetorno = _mensagemRepository.geraMensagem(string.Format("M00{0}", sCN4ISCPReceive.codigoRetornoSicom));
            }

            BaseResponse response = new BaseResponse();
            response.Model = sCN4ISCPReceive;
            return response;
        }

        /// <summary>
        /// Salva dados religação
        /// </summary>
        /// <param name="_sCN4ISRESend"></param>
        /// <returns></returns>
        public BaseResponse SCN4ISRE(SCN4ISRESend _sCN4ISRESend)
        {
            if (_sCN4ISRESend.contas != null)
            {
                foreach (SCN4ISRESendContas conta in _sCN4ISRESend.contas)
                {
                    _log.AddLog("  " + conta.ToString());
                }
            }
            BaseResponse response = new BaseResponse();
            SCN4ISREReceive sCN4ISREReceive = (SCN4ISREReceive)_iSSCN4ISRERepository.Connect(_sCN4ISRESend);
            _log.AddLog("  Retorno SCN4ISRE:" + sCN4ISREReceive.ToString());

            int codigoRetorno = 0;
            try
            {
                if (!"".Equals(sCN4ISREReceive.descricaoRetorno))
                    codigoRetorno = int.Parse(sCN4ISREReceive.descricaoRetorno.Trim());
            }
            catch (Exception) { }

            // Chamar programa SCN4CRAL para gerar evento de prioridade para esta SS
            if (codigoRetorno == 0038 && !_sCN4ISRESend.numeroProtocoloCrm.Equals(sCN4ISREReceive.protocoloSS)
                && !"".Equals(sCN4ISREReceive.dataPrevisaoSS) && !"0".Equals(sCN4ISREReceive.dataPrevisaoSS))
            {
                _servicoOperacionalRule.executaReiteracao(sCN4ISREReceive.protocoloSS, _sCN4ISRESend.numeroProtocoloCrm, _sCN4ISRESend.origem, sCN4ISREReceive.dataPrevisaoSS);
            }

            if (!"".Equals(sCN4ISREReceive.descricaoRetorno))
            {
                if (sCN4ISREReceive.descricaoRetorno.Length == 1)
                    sCN4ISREReceive.descricaoRetorno = _mensagemRepository.GeraMensagemCustom(sCN4ISREReceive.descricaoRetorno);
                else
                    sCN4ISREReceive.descricaoRetorno = _mensagemRepository.parseMensagem(sCN4ISREReceive.descricaoRetorno);
            }
            response.Model = sCN4ISREReceive;
            return response;
        }

        /// <summary>
        /// Busca todos os dados sobre religação de água
        /// </summary>
        /// <param name="_buscaReligacaoSend"></param>
        /// <returns></returns>
        public BaseResponse BuscaReligacao(TrabBuscaReligacaoSend _buscaReligacaoSend)
        {
            BaseResponse response;
            var buscaReligacaoReceive = new TrabBuscaReligacaoReceive();

            //busca dados da religação
            var sCN4ISRLSend = new SCN4ISRLSend();
            sCN4ISRLSend.setValues(_buscaReligacaoSend);
            //numeroMatricula = _buscaReligacaoSend.numeroMatricula

            buscaReligacaoReceive.dados = (SCN4ISRLReceive)SCN4ISRL(sCN4ISRLSend).Model;
            buscaReligacaoReceive.descricaoRetorno = buscaReligacaoReceive.dados.descricaoRetorno;

            if (!"".Equals(_buscaReligacaoSend.numeroMatricula) && !"".Equals(_buscaReligacaoSend.identificador))
            {
                var sCN6ISFDSend = new SCN6ISFDSend
                {
                    matricula = _buscaReligacaoSend.numeroMatricula,
                    identificador = _buscaReligacaoSend.identificador,
                };
                buscaReligacaoReceive.faturasEmDebito = (SCN6ISFDReceive)_iSSCN6ISFDRepository.Connect(sCN6ISFDSend);
                foreach(SCN6ISFDReceiveFaturas fatura in buscaReligacaoReceive.faturasEmDebito.faturas)
                {
                    SCN6ISCBSend sCN6ISCBSend = new SCN6ISCBSend();
                    sCN6ISCBSend.numeroFatura = fatura.numeroFatura;
                    SCN6ISCBReceive sCN6ISCBReceive = (SCN6ISCBReceive)_iSSCN6ISCBRepository.Connect(sCN6ISCBSend);
                    if (sCN6ISCBReceive.IsValid)
                    {
                        fatura.numeroCodigoBarras = sCN6ISCBReceive.codigoBarrasFormatado;
                        fatura.numeroCodigoBarrasFormatado = sCN6ISCBReceive.codigoBarras;
                    }
                }
                
            }

            //verifica se ocorreu erro e gera mensagem
            if (buscaReligacaoReceive.faturasEmDebito != null && buscaReligacaoReceive.faturasEmDebito.descricaoRetorno != "")
            {
                buscaReligacaoReceive.faturasEmDebito.descricaoRetorno = _mensagemRepository.geraMensagem(string.Format("M00{0}", buscaReligacaoReceive.faturasEmDebito.codigoRetorno));
            }


            if (buscaReligacaoReceive.dados.descricaoRetorno == "" && "S".Equals(buscaReligacaoReceive.dados.financiaServico))
            {
                //busca parcelas da religação
                var sCN4ISCPSend = new SCN4ISCPSend
                {
                    numeroMatricula = _buscaReligacaoSend.numeroMatricula, //(BuscaReligacaoSend)
                    identificadorUnidade = _buscaReligacaoSend.identificadorUnidade, //(BuscaReligacaoSend)
                    servicoSS = buscaReligacaoReceive.dados.servicoSS, //(SCN4ISRLReceive)
                    valorReligacao = buscaReligacaoReceive.dados.valorReligacao, //(SCN4ISRLReceive)
                    valorEntradaParcelamento = null
                };

                buscaReligacaoReceive.dadosParcelamento = (SCN4ISCPReceive)SCN4ISCP(sCN4ISCPSend).Model;
            }

            response = new BaseResponse
            {
                Model = buscaReligacaoReceive
            };

            if (buscaReligacaoReceive.dadosParcelamento == null)
            {
                buscaReligacaoReceive.dadosParcelamento = new SCN4ISCPReceive();
                buscaReligacaoReceive.dadosParcelamento.descricaoRetorno = "Valor do serviço não poderá ser parcelado.";
            }
            if (buscaReligacaoReceive.faturasEmDebito == null || buscaReligacaoReceive.faturasEmDebito.descricaoRetorno == "00")
            {
                buscaReligacaoReceive.faturasEmDebito = new SCN6ISFDReceive();
                buscaReligacaoReceive.faturasEmDebito.descricaoRetorno = "Não existem faturas em débito.";
            }

            return response;
        }
    }
}