using Copasa.Atende.Business.Core;
using Copasa.Atende.Business.Interfaces;
using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Repository.Repositories;
using Copasa.Atende.Util;
using Copasa.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Copasa.Atende.Business.Rules
{
    /// <summary>
    /// Rule - Serviços operacionais.
    /// </summary>
    public class ServicoOperacionalRule : BaseRule, IServicoOperacionalRule
    {
        private IISSCN4CRALRepository _iSSCN4CRALRepository;
        private IISSCN4CRATRepository _iSSCN4CRATRepository;
        private IISSCN4CRBXRepository _iSSCN4CRBXRepository;
        private IISSCN4CRSSRepository _iSSCN4CRSSRepository;
        private IISSCN4ISORRepository _iSSCN4ISORRepository;
        private IISSCN4ISSSRepository _iSSCN4ISSSRepository;
        private IISSCN4CASSRepository _iSSCN4CASSRepository;
        private IISSCN4CREXRepository _iSSCN4CREXRepository;
        private IISSCN4ISAERepository _iSSCN4ISAERepository;
        private IISSCN4ISFARepository _iISSCN4ISFARepository;
        private IMensagemRepository _mensagemRepository;
        private IORAEmpregadoRepository _oraEmpregadoRepository;
        private ILog _log;

        /// <summary>
        /// Construtor Serviços operacionais.
        /// </summary>
        /// <param name="iSSCN4CRALRepository">IISSCN4CRALRepository.</param>
        /// <param name="iSSCN4CRATRepository">IISSCN4CRATRepository.</param>
        /// <param name="iSSCN4CRBXRepository">IISSCN4CRBXRepository.</param>
        /// <param name="iSSCN4CRSSRepository">IISSCN4CRSSRepository.</param>
        /// <param name="iSSCN4ISORRepository">IISSCN4ISORRepository.</param>
        /// <param name="iSSCN4ISSSRepository">IISSCN4ISSSRepository.</param>
        /// <param name="iSSCN4CASSRepository">IISSCN4CASSRepository</param>
        /// <param name="iSSCN4CREXRepository">IISSCN4CREXRepository</param>
        /// <param name="iSSCN4ISAERepository">IISSCN4ISAERepository.</param>
        /// <param name="iSSCN4ISFARepository">IISSCN4ISFARepository.</param>
        /// <param name="mensagemRepository">IMensagemRepository</param>
        /// <param name="oraEmpregadoRepository">IORAEmpregadoRepository</param>
        /// <param name="log">ILog</param>
        public ServicoOperacionalRule(
            IISSCN4CRALRepository iSSCN4CRALRepository,
            IISSCN4CRATRepository iSSCN4CRATRepository,
            IISSCN4CRBXRepository iSSCN4CRBXRepository,
            IISSCN4CRSSRepository iSSCN4CRSSRepository,
            IISSCN4ISORRepository iSSCN4ISORRepository,
            IISSCN4ISSSRepository iSSCN4ISSSRepository,
            IISSCN4CASSRepository iSSCN4CASSRepository,
            IISSCN4CREXRepository iSSCN4CREXRepository,
            IISSCN4ISAERepository iSSCN4ISAERepository,
            IISSCN4ISFARepository iSSCN4ISFARepository,
            IMensagemRepository mensagemRepository,
            IORAEmpregadoRepository oraEmpregadoRepository,
            ILog log
            )
        {
            _iSSCN4CRALRepository = iSSCN4CRALRepository;
            _iSSCN4CRATRepository = iSSCN4CRATRepository;
            _iSSCN4CRBXRepository = iSSCN4CRBXRepository;
            _iSSCN4CRSSRepository = iSSCN4CRSSRepository;
            _iSSCN4ISORRepository = iSSCN4ISORRepository;
            _iSSCN4ISSSRepository = iSSCN4ISSSRepository;
            _iSSCN4CASSRepository = iSSCN4CASSRepository;
            _iSSCN4CREXRepository = iSSCN4CREXRepository;
            _iSSCN4ISAERepository = iSSCN4ISAERepository;
            _iISSCN4ISFARepository = iSSCN4ISFARepository;
            _mensagemRepository = mensagemRepository;
            _oraEmpregadoRepository = oraEmpregadoRepository;
            _log = log;

            CodigoOSFaldaDAgua = "1500100";
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gera eventos prioridade.
        /// </summary>
        public BaseResponse SCN4CRAL(SCN4CRALSend sCN4CRALSend)
        {
            if ("servico.app".Equals(sCN4CRALSend.Origem))
                sCN4CRALSend.Origem = "APP";
            else
                sCN4CRALSend.Origem = "CRM";

            if ("".Equals(sCN4CRALSend.evento) && sCN4CRALSend.dataPrevisao != null && !"".Equals(sCN4CRALSend.dataPrevisao))
            {
                if (!"".Equals(sCN4CRALSend.dataPrevisao))
                {
                    int pos = sCN4CRALSend.dataPrevisao.IndexOf(' ');
                    if (pos > 0)
                        sCN4CRALSend.dataPrevisao = sCN4CRALSend.dataPrevisao.Substring(0, pos);
                    DateTime dataPrevisao = sCN4CRALSend.dataPrevisao.ToDateTime("M/d/yyyy");
                    sCN4CRALSend.dataPrevisao = dataPrevisao.ToString("yyyyMMdd");
                    if (dataPrevisao >= DateTime.Today)
                        sCN4CRALSend.evento = "RE";
                    else
                        sCN4CRALSend.evento = "RT";
                }
            }
               
            BaseResponse response = new BaseResponse();
            _log.AddLog("  Envio SCN4CRAL:" + sCN4CRALSend.ToString().Replace(";", "\r\n    "));
            SCN4CRALReceive sCN4CRALReceive = (SCN4CRALReceive)_iSSCN4CRALRepository.Connect(sCN4CRALSend);
            _log.AddLog("  Retorno SCN4CRAL:" + sCN4CRALReceive.ToString().Replace(";", "\r\n    "));
            response.Model = sCN4CRALReceive;
            return response;
        }

        /// <summary>
        /// Busca serviço gerados no Sicom para atualizar o Dynamics 365 .
        /// </summary>
        public BaseResponse AtualizaDynamicsOSGeradas()
        {
            BaseResponse response = new BaseResponse();
            StringBuilder mensagensRetorno = new StringBuilder();
            try
            {
                _log.IsBatch();
                SCN4CRATReceive sCN4CRATReceive = (SCN4CRATReceive)_iSSCN4CRATRepository.Connect();
                IDyn365ProtocoloRepository dyn365ProtocoloRepository = new Dyn365ProtocoloRepository();
                IDyn365ProtocoloEncerradoRepository dyn365ProtocoloEncerradoRepository = new Dyn365ProtocoloEncerradoRepository();
                int totalAtualizacoes = 0;
                StringBuilder osAtualizadas = new StringBuilder();
                string virgula = "";
                bool printouTitulo = false;
                foreach (SCN4CRATReceiveServicos os in sCN4CRATReceive.loteServicos)
                {
                    if (os.numeroProtocoloSS == null || "".Equals(os.numeroProtocoloSS) || "0".Equals(os.numeroProtocoloSS)
                        && (os.numeroProtocoloOS != null && !"".Equals(os.numeroProtocoloOS) && !"0".Equals(os.numeroProtocoloOS)))
                    {
                        if (!printouTitulo)
                        {
                            _log.AddLog("SolicitacaoServicoController AtualizaDynamicsOSGeradas");
                            printouTitulo = true;
                        }
                        _log.AddLog("     Erro SS zerada para a OS"+os.numeroOrdemServico);
                    }
                    if (os.numeroProtocoloSS != null && !"".Equals(os.numeroProtocoloSS) && !"0".Equals(os.numeroProtocoloSS))
                    {
                        if (!printouTitulo)
                        {
                            _log.IsBatch();
                            _log.AddLog("SolicitacaoServicoController AtualizaDynamicsOSGeradas");
                            printouTitulo = true;
                        }

                        Dyn365Protocolo dyn365Protocolo = new Dyn365Protocolo();
                        dyn365Protocolo.setValues(os);
                        dyn365Protocolo.descricaoSituacaoSS = GetdescricaoStatusOS(dyn365Protocolo.situacaoSS);
                        if (!"".Equals(os.numeroProtocoloSS) && !"0".Equals(os.numeroProtocoloSS))
                        {
                            List<Dyn365Protocolo> listRetorno = dyn365ProtocoloRepository.Pesquisar("copasa_protocolo", os.numeroProtocoloSS);
                            if (listRetorno.Count == 1)
                            {
                                dyn365Protocolo.incidentid = listRetorno.ToArray()[0].incidentid;
                                dyn365Protocolo.descricaoServicoSS = dyn365Protocolo.descricaoServicoSS.Replace('"', ' ');
                                bool sucesso = dyn365ProtocoloRepository.Atualizar(dyn365Protocolo, _log);
                                if (sucesso)
                                {
                                    Dyn365OrdemServico dyn365OrdemServico = new Dyn365OrdemServico();
                                    dyn365OrdemServico.setValues(os);
                                    if (!"".Equals(dyn365OrdemServico.situacaoOS))
                                        dyn365OrdemServico.descricaoSituacaoOS = GetdescricaoStatusOS(dyn365OrdemServico.situacaoOS);

                                    IDyn365OrdemServicoRepository dyn365OrdemServicoRepository = new Dyn365OrdemServicoRepository();
                                    if (os.numeroProtocoloOS != null && !"".Equals(os.numeroProtocoloOS) && !"0".Equals(os.numeroProtocoloOS))
                                    {
                                        List<Dyn365OrdemServico> listRetornoOS = dyn365OrdemServicoRepository.Pesquisar("copasa_numerodaos", dyn365OrdemServico.numeroOrdemServico);
                                        if (listRetornoOS.Count > 0)
                                        {
                                            dyn365OrdemServico.setValues(os);
                                            dyn365OrdemServico.idDyn365 = listRetornoOS.ToArray()[0].idDyn365;
                                            dyn365OrdemServico.descricaoServicoOS = dyn365OrdemServico.descricaoServicoOS.Replace('"', ' ');
                                            sucesso = dyn365OrdemServicoRepository.Atualizar(dyn365OrdemServico, _log);
                                        }
                                        else
                                        {
                                            dyn365OrdemServico.idSolicitacaoBind = dyn365Protocolo.incidentid;
                                            dyn365OrdemServico.descricaoServicoOS = dyn365OrdemServico.descricaoServicoOS.Replace('"', ' ');
                                            sucesso = dyn365OrdemServicoRepository.Incluir(dyn365OrdemServico, _log);
                                        }
                                        if (sucesso)
                                        {
                                            totalAtualizacoes++;
                                            osAtualizadas.Append(virgula + os.numeroProtocoloSS);
                                            virgula = ", ";
                                        }
                                        else
                                        {
                                            _log.AddLog("    " + "Erro na atualização de OS " + dyn365OrdemServicoRepository.getDadosRetorno());
                                            mensagensRetorno.Append("Erro na atualização de OS " + dyn365OrdemServicoRepository.getDadosRetorno() + "\r\n");
                                        }
                                    }
                                }
                                else
                                {
                                    _log.AddLog("    " + "Erro na atualização de SS " + dyn365ProtocoloRepository.getDadosRetorno());
                                    mensagensRetorno.Append("Erro na atualização de SS " + dyn365ProtocoloRepository.getDadosRetorno() + "\r\n");
                                }
                            }
                            else
                            {
                                _log.AddLog("    " + "Protocolo " + os.numeroProtocoloSS + "inexistente no Dynamicas");
                                mensagensRetorno.Append("Protocolo " + os.numeroProtocoloSS + "inexistente no Dynamicas\r\n");
                            }
                        }
                    }
                }
                if (totalAtualizacoes > 0)
                {
                    _log.AddLog("    " + "Foram enviadas  " + totalAtualizacoes + " SS's para atualização no Dynamics: " + osAtualizadas.ToString());
                    mensagensRetorno.Append("Foram enviadas  " + totalAtualizacoes + " SS's para atualização no Dynamics: " + osAtualizadas.ToString());
                }
                else
                {
                    /*
                    _log.AddLog("SolicitacaoServicoController AtualizaDynamicsOSGeradas");
                    _log.AddLog("    Não há SS's para serem atualizadas");
                    mensagensRetorno.Append("Não há SS's para serem atualizadas");
                    */
                    mensagensRetorno.Append("");
                }

            }
            catch (Exception e)
            {
                _log.AddLog("    " + "Erro na atualização do Dynamics " + e.Message);
                mensagensRetorno.Append("Erro na atualização do Dynamics " + e.Message + "\r\n");
            }
            response.Message = mensagensRetorno.ToString();
            return response;
        }

        /// <summary>
        /// Busca baixas de OS no Sicom para atualizar o Dynamics 365.
        /// </summary>
        public BaseResponse AtualizaDynamicsBaxiasOS()
        {
            BaseResponse response = new BaseResponse();
            StringBuilder mensagensRetorno = new StringBuilder();
            try
            {
                _log.IsBatch();
                SCN4CRBXReceive sCN4CRBXReceive = (SCN4CRBXReceive)_iSSCN4CRBXRepository.Connect();
                if (!sCN4CRBXReceive.quantidadeOSEnviada.Equals("0"))
                {
                    _log.AddLog(" Retorno Sicom SCN4CRBX:");
                    _log.AddLog("  " + sCN4CRBXReceive.ToString().Replace(";", "\r\n "));
                    foreach (SCN4CRBXReceiveServicos ss in sCN4CRBXReceive.loteServicos)
                    {
                        _log.AddLog("   " + ss.ToString().Replace(";", "\r\n  "));
                    }
                }

                IDyn365ProtocoloRepository dyn365ProtocoloRepository = new Dyn365ProtocoloRepository();
                IDyn365ProtocoloEncerradoRepository dyn365ProtocoloEncerradoRepository = new Dyn365ProtocoloEncerradoRepository();
                int totalAtualizacoes = 0;
                StringBuilder osAtualizadas = new StringBuilder();
                string virgula = "";
                bool printouTitulo = false;
                foreach (SCN4CRBXReceiveServicos os in sCN4CRBXReceive.loteServicos)
                {
                    if (os.numeroProtocoloSS != null && !"".Equals(os.numeroProtocoloSS) && !"0".Equals(os.numeroProtocoloSS))
                    {
                        if (!printouTitulo)
                        {
                            _log.AddLog("SolicitacaoServicoController AtualizaDynamicsBaxiasOS");
                            printouTitulo = true;
                        }
                        Dyn365Protocolo dyn365Protocolo = new Dyn365Protocolo();
                        //Temporario
                        os.horaBaixaSS = os.horaBaixaOS;
                        dyn365Protocolo.setValues(os);
                        List<Dyn365Protocolo> listRetorno = dyn365ProtocoloRepository.Pesquisar("copasa_protocolo", os.numeroProtocoloSS);
                        if (listRetorno.Count == 1)
                        {
                            bool sucesso = false;
                            Dyn365OrdemServico dyn365OrdemServico = new Dyn365OrdemServico();
                            dyn365OrdemServico.setValues(os);
                            if (!"".Equals(dyn365OrdemServico.situacaoOS))
                                dyn365OrdemServico.descricaoSituacaoOS = GetdescricaoStatusOS(dyn365OrdemServico.situacaoOS);

                            IDyn365OrdemServicoRepository dyn365OrdemServicoRepository = new Dyn365OrdemServicoRepository();
                            List<Dyn365OrdemServico> listRetornoOS = dyn365OrdemServicoRepository.Pesquisar("copasa_numerodaos", dyn365OrdemServico.numeroOrdemServico);
                            if (listRetornoOS.Count > 0)
                            {
                                dyn365OrdemServico.idDyn365 = listRetornoOS.ToArray()[0].idDyn365;
                                dyn365OrdemServico.descricaoServicoOS = dyn365OrdemServico.descricaoServicoOS.Replace('"', ' ');
                                sucesso = dyn365OrdemServicoRepository.Atualizar(dyn365OrdemServico, _log);
                            }
                            else
                            {
                                dyn365OrdemServico.idSolicitacaoBind = listRetorno.ToArray()[0].incidentid;
                                dyn365OrdemServico.descricaoServicoOS = dyn365OrdemServico.descricaoServicoOS.Replace('"', ' ');
                                sucesso = dyn365OrdemServicoRepository.Incluir(dyn365OrdemServico, _log);
                            }
                            if (!sucesso)
                            {
                                _log.AddLog("    Erro na baixa de OS " + dyn365OrdemServicoRepository.getDadosRetorno());
                                _log.AddLog("     Dados envidados: " + dyn365OrdemServicoRepository.getDadosEnvio());
                                _log.AddLog("     Endereco conexao: " + dyn365OrdemServicoRepository.getEnderecoConexao());
                                _log.AddLog("     Dados retornados: " + dyn365OrdemServicoRepository.getDadosRetorno());
                                mensagensRetorno.Append("Erro na baixa de OS " + dyn365OrdemServicoRepository.getDadosRetorno() + "\r\n");
                            }
                            dyn365Protocolo.incidentid = listRetorno.ToArray()[0].incidentid;
                            dyn365Protocolo.situacaoSS = listRetorno.ToArray()[0].situacaoSS;
                            dyn365Protocolo.descricaoSituacaoSS = GetdescricaoStatusOS(dyn365Protocolo.situacaoSS);
                            if ("C".Equals(dyn365Protocolo.situacaoSS))
                            {
                                // Solicitação Cancelada
                                dyn365Protocolo.situacaoSolicitacao = 6;
                                dyn365Protocolo.situacaoProtocolo = 2;
                            }
                            dyn365Protocolo.descricaoServicoSS = dyn365Protocolo.descricaoServicoSS.Replace('"', ' ');
                            sucesso = dyn365ProtocoloRepository.Atualizar(dyn365Protocolo,_log);

                            if (sucesso)
                            {
                                totalAtualizacoes++;
                                osAtualizadas.Append(virgula + os.numeroProtocoloSS);
                                virgula = ", ";
                            }
                            else
                            {
                                _log.AddLog("    Erro na baixa de SS " + dyn365ProtocoloRepository.getDadosRetorno());
                                _log.AddLog("     Dados envidados: " + dyn365ProtocoloRepository.getDadosEnvio());
                                _log.AddLog("     Endereco conexao: " + dyn365ProtocoloRepository.getEnderecoConexao());
                                _log.AddLog("     Dados retornados: " + dyn365ProtocoloRepository.getDadosRetorno());

                                mensagensRetorno.Append("Erro na baixa de SS " + dyn365ProtocoloRepository.getDadosRetorno() + "\r\n");
                            }
                            if ("D".Equals(dyn365Protocolo.situacaoSS) ||
                                "E".Equals(dyn365Protocolo.situacaoSS) ||
                                "L".Equals(dyn365Protocolo.situacaoSS) ||
                                "M".Equals(dyn365Protocolo.situacaoSS) ||
                                "N".Equals(dyn365Protocolo.situacaoSS) ||
                                "T".Equals(dyn365Protocolo.situacaoSS) ||
                                "U".Equals(dyn365Protocolo.situacaoSS))
                            {
                                // Solicitação resolvida
                                Dyn365ProtocoloEncerrado dyn365ProtocoloEncerrado = new Dyn365ProtocoloEncerrado();
                                dyn365ProtocoloEncerrado.status = -1;
                                dyn365ProtocoloEncerrado.resolucao = new Dyn365ProtocoloEncerradoResolucao();
                                dyn365ProtocoloEncerrado.resolucao.sujeito = dyn365Protocolo.numeroProtocolo;
                                dyn365ProtocoloEncerrado.resolucao.descricao = dyn365Protocolo.descricaoSituacaoSS;
                                dyn365ProtocoloEncerrado.resolucao.idProtocoloRelacionadoBind = listRetorno.ToArray()[0].incidentid;
                                dyn365ProtocoloEncerrado.resolucao.tempoGasto = 60;
                                dyn365ProtocoloEncerrado.status = -1;
                                if (!dyn365ProtocoloEncerradoRepository.Incluir(dyn365ProtocoloEncerrado))
                                {
                                    _log.AddLog("    Não atualizou ProtocoloEncerrado");
                                    _log.AddLog("     Dados envidados: " + dyn365ProtocoloEncerradoRepository.getDadosEnvio());
                                    _log.AddLog("     Endereco conexao: " + dyn365ProtocoloEncerradoRepository.getEnderecoConexao());
                                    _log.AddLog("     Dados retornados: " + dyn365ProtocoloEncerradoRepository.getDadosRetorno());
                                }
                            }
                        }
                        else
                        {
                            _log.AddLog("    " + "Protocolo " + os.numeroProtocoloSS + "inexistente no Dynamicas");
                            mensagensRetorno.Append("Protocolo " + os.numeroProtocoloSS + "inexistente no Dynamicas\r\n");
                        }
                    }
                }
                if (totalAtualizacoes > 0)
                {
                    _log.AddLog("    " + "Foram enviadas  " + totalAtualizacoes + " SS's para baixa no Dynamics: " + osAtualizadas.ToString());
                    mensagensRetorno.Append("Foram enviadas  " + totalAtualizacoes + " SS's para baixa no Dynamics: " + osAtualizadas.ToString());
                }
                else
                {
                    /*
                    _log.AddLog("SolicitacaoServicoController AtualizaDynamicsBaxiasOS");
                    _log.AddLog("    Não há SS's para ser baixada");
                    mensagensRetorno.Append("Não há SS's para ser baixada");
                    */
                    mensagensRetorno.Append("");
                }

            }
            catch (Exception e)
            {
                _log.AddLog("    " + "Erro na atualização do Dynamics " + e.Message);
                mensagensRetorno.Append("Erro na atualização do Dynamics " + e.Message+"\r\n");
            }
            response.Message = mensagensRetorno.ToString();
            return response;
        }

        /// <summary>
        /// Cria solicitação de serviço.
        /// </summary>
        public BaseResponse SCN4CRSS(SCN4CRSSSend sCN4CRSSSend)
        {
            //_log.GravaLog("     " + sCN4CRSSSend.ToString().Replace(";", "\r\n    "));
            string mensagemErroValidar;

            BaseResponse response = new BaseResponse();
            SCN4CRSSReceive sCN4CRSSReceive = null;
            if (sCN4CRSSSend.validar(out mensagemErroValidar))
            {
                bool consistiu = true;
                string descricaoRetorno = "";
                if ("".Equals(sCN4CRSSSend.nomeSolicitante))
                    descricaoRetorno = _mensagemRepository.geraMensagem("M0147");
                if (!"".Equals(descricaoRetorno))
                {
                    sCN4CRSSReceive = new SCN4CRSSReceive();
                    sCN4CRSSReceive.descricaoRetorno = descricaoRetorno;
                    consistiu = false;
                }
                if (consistiu)
                {
                    bool temInterrupcao = false;
                    string prefixoCodigoServico = sCN4CRSSSend.codigoServicoSolicitado.Substring(0, 5);
                    if ("15001".Equals(prefixoCodigoServico) || "15002".Equals(prefixoCodigoServico))
                    {
                        SCN4ISFASend sCN4ISFASend = new SCN4ISFASend();
                        sCN4ISFASend.matricula = sCN4CRSSSend.matricula;
                        SCN4ISFAReceive _interrupcoesSicom = (SCN4ISFAReceive)_iISSCN4ISFARepository.Connect(sCN4ISFASend);
                        if (_interrupcoesSicom.temInterrupcao == "S")
                        {
                            try
                            {
                                DateTime dataPrevisao = DateTime.ParseExact(_interrupcoesSicom.dataPrevisao + _interrupcoesSicom.horaPrevisao, "dd/MM/yyyyHH:mm", null);
                                if (dataPrevisao > DateTime.Now)
                                {
                                    temInterrupcao = true;
                                    sCN4CRSSReceive = new SCN4CRSSReceive();
                                    sCN4CRSSReceive.descricaoRetorno = "Já existe manutenção na rede de abastecimento de água nesta região, por motivo de " + _interrupcoesSicom.descricaoMotivo + " e a previsão de solução é " + _interrupcoesSicom.dataPrevisao + " até às " + _interrupcoesSicom.horaPrevisao;
                                }
                            }
                            catch (Exception) { }
                        }
                        if (!temInterrupcao)
                        {
                            TrabInterrupcaoCopagis _interrupcoesCopaGis = null;
                            if (!"".Equals(sCN4CRSSSend.matricula))
                                _interrupcoesCopaGis = ObterInterrupcoesCopagis(sCN4CRSSSend.matricula);
                            else if (!"".Equals(sCN4CRSSSend.codigoBairro) && !"".Equals(sCN4CRSSSend.codigoLocalidade))
                                _interrupcoesCopaGis = ObterInterrupcoesCopagis(sCN4CRSSSend.codigoLocalidade, sCN4CRSSSend.codigoBairro);
                            if (_interrupcoesCopaGis != null)
                            {
                                DateTime dataPrevisao = _interrupcoesCopaGis.DtFim;
                                var dataPrevisaoFormatada = dataPrevisao.ToString("dd/MM/yyyy");
                                var horaPrevisaoFormtada = dataPrevisao.ToString("HH:mm");
                                temInterrupcao = true;
                                sCN4CRSSReceive = new SCN4CRSSReceive();
                                sCN4CRSSReceive.descricaoRetorno = "Já existe manutenção na rede de abastecimento de água nesta região, por motivo de " + _interrupcoesCopaGis.Descricao + " e a previsão de solução é " + dataPrevisaoFormatada + " até às " + horaPrevisaoFormtada;
                            }
                        }
                    }
                    if (!temInterrupcao)
                    {
                        sCN4CRSSSend.telefoneSolicitante = getNumeros(sCN4CRSSSend.telefoneSolicitante);
                        StringBuilder valores = new StringBuilder();
                        TrabUsuario trabUsuario = new TrabUsuario();
                        trabUsuario.codigoUsuario = sCN4CRSSSend.codigoUsuario;
                        trabUsuario.nomeUsuario = sCN4CRSSSend.nomeUsuario;
                        trabUsuario.agenciaUsuario = sCN4CRSSSend.agenciaUsuario;
                        sCN4CRSSSend.usuarioInterno = _oraEmpregadoRepository.preencheDadosUsuario(trabUsuario);
                        sCN4CRSSSend.codigoUsuario = trabUsuario.codigoUsuario;
                        sCN4CRSSSend.nomeUsuario = trabUsuario.nomeUsuario;
                        sCN4CRSSSend.agenciaUsuario = trabUsuario.agenciaUsuario;
                        sCN4CRSSSend.observacaoSicom = ConverteTextoParaArrayIBM(sCN4CRSSSend.observacao,70);
                        sCN4CRSSSend.referenciaEndereco = Util.Util.trataTextoIBM(sCN4CRSSSend.referenciaEndereco);
                        if ("".Equals(sCN4CRSSSend.telefoneSolicitante))
                            sCN4CRSSSend.telefoneSolicitante = "3131";
                        if ("".Equals(sCN4CRSSSend.referenciaEndereco))
                            sCN4CRSSSend.referenciaEndereco = "SEM REFERENCIA";
                        if ("".Equals(sCN4CRSSSend.observacao))
                        {
                            string[] observacaoSicom = new string[1];
                            observacaoSicom[0] = "SEM OBSERVACAO";
                            sCN4CRSSSend.observacaoSicom = observacaoSicom;
                        }

                        sCN4CRSSReceive = (SCN4CRSSReceive)_iSSCN4CRSSRepository.Connect(sCN4CRSSSend);
                        int codigoRetorno = 0;
                        bool usuarioInterno = ((SCN4CRSSSend)sCN4CRSSSend).usuarioInterno;
                        try
                        {
                            if (!"".Equals(sCN4CRSSReceive.codigoRetornoSicom))
                                codigoRetorno = int.Parse(sCN4CRSSReceive.codigoRetornoSicom);
                            else if (!"".Equals(sCN4CRSSReceive.descricaoRetornoSicom))
                            {
                                if (sCN4CRSSReceive.descricaoRetornoSicom.Length > 4 && int.TryParse(sCN4CRSSReceive.descricaoRetornoSicom.Substring(0, 4).Trim(), out codigoRetorno))
                                { }
                            }
                        }
                        catch (Exception) { }

                        string dataPrevisaoSS = "";
                        if (!"".Equals(sCN4CRSSReceive.dataPrevisaoSS) && !"0".Equals(sCN4CRSSReceive.dataPrevisaoSS))
                        {
                            dataPrevisaoSS = sCN4CRSSReceive.dataPrevisaoSS.ToDateTime("yyyyMMdd").ToString("dd/MM/yyyy");
                        }
                        if ("0".Equals(sCN4CRSSReceive.codigoServicoOS))
                            sCN4CRSSReceive.codigoServicoOS = "";
                        if ("0".Equals(sCN4CRSSReceive.codigoServicoSS))
                            sCN4CRSSReceive.codigoServicoSS = "";

                        if (!"".Equals(sCN4CRSSReceive.numeroProtocoloSS.Trim()) && !"".Equals(sCN4CRSSReceive.dataPrevisaoSS.Trim()))
                        {
                            Dyn365Protocolo dyn365Protocolo = null;
                            Dyn365OrdemServico dyn365OrdemServico = null;

                            if (codigoRetorno != 0125 && codigoRetorno != 1258 && codigoRetorno != 1207 && codigoRetorno != 2483 && codigoRetorno != 2565 &&
                                !"".Equals(sCN4CRSSSend.numeroProtocolo)
                                && !"".Equals(sCN4CRSSReceive.numeroSolicitacaoServico)
                                && !"0".Equals(sCN4CRSSReceive.numeroSolicitacaoServico))
                            {
                                dyn365Protocolo = new Dyn365Protocolo();
                                dyn365Protocolo.numeroProtocolo = sCN4CRSSSend.numeroProtocolo;
                                dyn365Protocolo.setValues(sCN4CRSSReceive);
                                dyn365Protocolo.numeroProtocolo = sCN4CRSSReceive.numeroProtocoloSS;
                                dyn365Protocolo.codigoServico = sCN4CRSSReceive.codigoServicoSS;
                                if (!"".Equals(dyn365Protocolo.situacaoSS))
                                    dyn365Protocolo.descricaoSituacaoSS = GetdescricaoStatusOS(dyn365Protocolo.situacaoSS);
                                if (!"".Equals(sCN4CRSSReceive.numeroOrdemServico) && !"0".Equals(sCN4CRSSReceive.numeroOrdemServico))
                                {
                                    dyn365OrdemServico = new Dyn365OrdemServico();
                                    dyn365OrdemServico.setValues(sCN4CRSSReceive);
                                    if (!"".Equals(dyn365OrdemServico.situacaoOS))
                                        dyn365OrdemServico.descricaoSituacaoOS = GetdescricaoStatusOS(dyn365OrdemServico.situacaoOS);
                                }
                            }

                            // Chamar programa SCN4CRAL para gerar evento de prioridade para esta SS
                            if ((codigoRetorno == 0125 || codigoRetorno == 1258 || codigoRetorno == 1207 || codigoRetorno == 2483 || codigoRetorno == 2565
                            || ("S".Equals(sCN4CRSSSend.emergenciaRisco) && !"".Equals(sCN4CRSSSend.justificativaEmergenciaRisco)))
                                && !sCN4CRSSSend.numeroProtocolo.Equals(sCN4CRSSReceive.numeroProtocoloSS))
                            {
                                SCN4CRALSend sCN4CRALSend = new SCN4CRALSend();
                                sCN4CRALSend.protocoloSS = sCN4CRSSReceive.numeroProtocoloSS;
                                sCN4CRALSend.protocoloAtendimento = sCN4CRSSSend.numeroProtocolo;
                                sCN4CRALSend.Origem = sCN4CRSSSend.codigoUsuario;
                                if ("S".Equals(sCN4CRSSSend.emergenciaRisco) && !"".Equals(sCN4CRSSSend.justificativaEmergenciaRisco))
                                    sCN4CRALSend.evento = "PR";
                                sCN4CRALSend.dataPrevisao = sCN4CRSSReceive.dataPrevisaoSS.ToDateTime("yyyyMMdd").ToString("M/d/yyyy");
                                sCN4CRALSend.observacaoEvento = sCN4CRSSSend.justificativaEmergenciaRisco;
                                try
                                {
                                    SCN4CRALReceive sCN4CRALReceive = (SCN4CRALReceive)SCN4CRAL(sCN4CRALSend).Model;
                                    // Atualizar dados do protocolo relacionado no Dynamics
                                    if (!"".Equals(sCN4CRSSSend.numeroProtocolo) && !"0".Equals(sCN4CRSSSend.numeroProtocolo))
                                    {
                                        dyn365Protocolo = new Dyn365Protocolo();
                                        dyn365Protocolo.numeroProtocolo = sCN4CRSSSend.numeroProtocolo;
                                        dyn365Protocolo.numeroPriorizacao = sCN4CRALReceive.quantidadePR;
                                        dyn365Protocolo.emailUnidadeResponsavel = sCN4CRALReceive.EmailUnidadeResponsavel;
                                        IDyn365ProtocoloRepository dyn365ProtocoloRepositoryRelacionado = new Dyn365ProtocoloRepository();
                                        List<Dyn365Protocolo> listRetornoProtocoloRelacionado = dyn365ProtocoloRepositoryRelacionado.Pesquisar("copasa_protocolo", sCN4CRSSReceive.numeroProtocoloSS);
                                        if (listRetornoProtocoloRelacionado.Count == 1)
                                        {
                                            dyn365Protocolo.idSolicitacaoRelacionadaBind = listRetornoProtocoloRelacionado.ToArray()[0].incidentid;
                                        }
                                        IDyn365SuperintendenciaRepository dyn365SuperintendenciaRepository = new Dyn365SuperintendenciaRepository();
                                        List<Dyn365Superintendencia> listSuperintendencia = dyn365SuperintendenciaRepository.Pesquisar("copasa_name", sCN4CRALReceive.superintendenciaResponsavel);
                                        if (listSuperintendencia.Count == 1)
                                        {
                                            dyn365Protocolo.ideSuperintendenciaBind = listSuperintendencia.ToArray()[0].id;
                                        }
                                        else
                                        {
                                            sCN4CRALReceive.unidadeResponsavel = sCN4CRALReceive.superintendenciaResponsavel;
                                        }
                                        IDyn365UnidadeRepository dyn365UnidadeRepository = new Dyn365UnidadeRepository();
                                        List<Dyn365Unidade> listUnidades = dyn365UnidadeRepository.Pesquisar("copasa_name", sCN4CRALReceive.unidadeResponsavel);
                                        if (listUnidades.Count == 1)
                                        {
                                            dyn365Protocolo.idUnidadeEnvolvidaBind = listUnidades.ToArray()[0].id;
                                        }
                                        else
                                        {
                                            IDyn365EscritorioLocalRepository dyn365EscritorioLocalRepository = new Dyn365EscritorioLocalRepository();
                                            List<Dyn365EscritorioLocal> listEscritorioLocal = dyn365EscritorioLocalRepository.Pesquisar("copasa_name", sCN4CRALReceive.unidadeResponsavel);
                                            if (listUnidades.Count == 1)
                                            {
                                                dyn365Protocolo.idUnidadeEnvolvidaBind = listEscritorioLocal.ToArray()[0].id;
                                            }
                                        }
                                    }
                                }
                                catch (Exception) { }
                                if (!"".Equals(dyn365Protocolo.numeroPriorizacao) && !"0".Equals(dyn365Protocolo.numeroPriorizacao))
                                {
                                    sCN4CRSSReceive.descricaoRetorno = _mensagemRepository.geraMensagem("M0172");
                                    if (!"".Equals(dataPrevisaoSS) && !"0".Equals(dataPrevisaoSS))
                                        sCN4CRSSReceive.descricaoRetorno = sCN4CRSSReceive.descricaoRetorno + " A previsão de solução é dia " + dataPrevisaoSS.ToDateTime("yyyyMMdd").ToString("dd/MM/yyyy");
                                }
                            }

                            //Atualizar dados do protocolo criado no Dynamics
                            if (dyn365Protocolo != null)
                            {
                                IDyn365ProtocoloRepository dyn365ProtocoloRepository = new Dyn365ProtocoloRepository();
                                try
                                {
                                    List<Dyn365Protocolo> listRetorno = dyn365ProtocoloRepository.Pesquisar("copasa_protocolo", dyn365Protocolo.numeroProtocolo);
                                    if (listRetorno.Count == 1)
                                    {
                                        dyn365Protocolo.incidentid = listRetorno.ToArray()[0].incidentid;
                                        dyn365ProtocoloRepository.AtualizarAsync(dyn365Protocolo, _log);
                                        if (dyn365OrdemServico != null)
                                        {
                                            dyn365OrdemServico.idSolicitacaoBind = dyn365Protocolo.incidentid;
                                            IDyn365OrdemServicoRepository dyn365OrdemServicoRepository = new Dyn365OrdemServicoRepository();
                                            dyn365OrdemServicoRepository.AtualizarAsync(dyn365OrdemServico, _log);
                                        }
                                    }
                                    else
                                    {
                                        _log.AddLog("    Não foi possível atualizar o Dynamics, protocolo " + sCN4CRSSSend.numeroProtocolo + " não encontrado");
                                    }
                                }
                                catch (Exception e)
                                {
                                    _log.AddLog("    Erro" + e.Message, true);
                                }
                            }
                        }
                    }
                }
                else
                {
                    _log.AddLog("    Erro na consistência:" + descricaoRetorno);
                }
            }
            else
            {
                sCN4CRSSReceive = new SCN4CRSSReceive();
                sCN4CRSSReceive.descricaoRetorno = mensagemErroValidar;
            }
            Dyn365SomenteMensagem retornoDyn365 = new Dyn365SomenteMensagem();
            retornoDyn365.descricaoRetorno = sCN4CRSSReceive.descricaoRetorno;
            retornoDyn365.codigoRetorno = sCN4CRSSReceive.codigoRetorno;
            retornoDyn365.IsValid = sCN4CRSSReceive.IsValid;
            response.Model = retornoDyn365;
            return response;
        }

        /// <summary>
        /// Busca OS's de uma solicitação de serviço.
        /// </summary>
        public BaseResponse SCN4ISOR(SCN4ISORSend sCN4ISORSend)
        {
            SCN4ISORReceive sCN4ISORReceive = (SCN4ISORReceive)_iSSCN4ISORRepository.Connect(sCN4ISORSend);
            if (sCN4ISORReceive.ordensServico.Count == 0)
                sCN4ISORReceive.descricaoRetorno = _mensagemRepository.geraMensagem("M0173");
            BaseResponse response = new BaseResponse();
            response.Model = sCN4ISORReceive;
            return response;
        }

        /// <summary>
        /// Busca solicitações de serviços de uma matrícula.
        /// </summary>
        public BaseResponse SCN4ISSS(SCN4ISSSSend sCN4ISSSSend)
        {
            SCN4ISSSReceive sCN4ISSSReceive = (SCN4ISSSReceive)_iSSCN4ISSSRepository.Connect(sCN4ISSSSend);
            if (sCN4ISSSReceive.solicitacoesServico.Count == 0)
                sCN4ISSSReceive.descricaoRetorno = _mensagemRepository.geraMensagem("M0057");
            BaseResponse response = new BaseResponse();
            response.Model = sCN4ISSSReceive;
            return response;
        }

        /// <summary>
        /// Cancela uma solicitação de serviço
        /// </summary>
        /// <param name="sCN4CASSSend"></param>
        /// <returns></returns>
        public BaseResponse SCN4CASS(SCN4CASSSend sCN4CASSSend)
        {
            // Gambiarra para concertar erro da AX4B, estão enviando os campos invertidos
            string numeroProtocoloCancelamento = sCN4CASSSend.numeroProtocoloCancelamento;
            sCN4CASSSend.numeroProtocoloCancelamento = sCN4CASSSend.numeroProtocoloSS;
            sCN4CASSSend.numeroProtocoloSS = numeroProtocoloCancelamento;

            SCN4CASSReceive sCN4CASSReceive = (SCN4CASSReceive)_iSSCN4CASSRepository.Connect(sCN4CASSSend);
            _log.AddLog(" Retorno SCN4CASS:" + sCN4CASSReceive.getJson().Replace(",", "\r\n "));
            BaseResponse response = new BaseResponse();

            //sCN4CASSReceive.descricaoErro = MontarMensagemRetornoCancelaSS(sCN4CASSReceive);
            sCN4CASSReceive.codigoErro = "";
            sCN4CASSReceive.codigoRetorno = "";
            sCN4CASSReceive.descricaoErro = "";
            sCN4CASSReceive.descricaoErroIS = "";
            sCN4CASSReceive.descricaoRetorno = "";

            response.Model = sCN4CASSReceive;
            return response;
        }

        /// <summary>
        /// Gera OS extra para SS existente
        /// </summary>
        /// <param name="sCN4CREXSend"></param>
        /// <returns></returns>
        public BaseResponse SCN4CREX(SCN4CREXSend sCN4CREXSend)
        {
            SCN4CREXReceive sCN4CREXReceive = (SCN4CREXReceive)_iSSCN4CREXRepository.Connect(sCN4CREXSend);
            BaseResponse response = new BaseResponse();
            response.Model = sCN4CREXReceive;
            return response;
        }

        /// <summary>
        /// Gera alteração de economias
        /// </summary>
        /// <param name="sCN4ISAESend"></param>
        /// <returns></returns>
        public BaseResponse SCN4ISAE(SCN4ISAESend sCN4ISAESend)
        {
            string mensagemErroValidar;

            BaseResponse response = new BaseResponse();
            SCN4ISAEReceive sCN4ISAEReceive = null;
            if (sCN4ISAESend.validar(out mensagemErroValidar))
            {
                sCN4ISAESend.descricaoSSSicom = ConverteTextoParaArrayIBM(sCN4ISAESend.descricaoSS, 65);
                _log.AddLog(" Chamada SCN4ISAE:");
                _log.AddLog(" " + sCN4ISAESend.getJson().Replace(",", "\r\n "));
                sCN4ISAEReceive = (SCN4ISAEReceive)_iSSCN4ISAERepository.Connect(sCN4ISAESend);
                _log.AddLog(" Retorno SCN4ISAE:");
                _log.AddLog(" " + sCN4ISAEReceive.getJson().Replace(",", "\r\n "));
                int codigoRetorno = 0;
                try
                {
                    if (!"".Equals(sCN4ISAEReceive.descricaoRetorno))
                        codigoRetorno = int.Parse(sCN4ISAEReceive.descricaoRetorno.Trim());
                }
                catch (Exception) { }

                if (codigoRetorno > 0)
                    sCN4ISAEReceive.descricaoRetorno = _mensagemRepository.geraMensagem(codigoRetorno.ToString());
                else
                    sCN4ISAEReceive.descricaoRetorno = _mensagemRepository.geraMensagemComDataPrazo(2, sCN4ISAEReceive.dataPrevisaoSS, sCN4ISAEReceive);

                if (!"".Equals(sCN4ISAESend.numeroProtocolo))
                {
                    Dyn365Protocolo dyn365Protocolo = new Dyn365Protocolo();
                    dyn365Protocolo.setValues(sCN4ISAEReceive);
                    dyn365Protocolo.numeroProtocolo = sCN4ISAESend.numeroProtocolo;

                    // Chamar programa SCN4CRAL para gerar evento de prioridade para esta SS
                    if (codigoRetorno == 0038  && !sCN4ISAESend.numeroProtocolo.Equals(sCN4ISAEReceive.protocoloSS)
                        && !"".Equals(sCN4ISAEReceive.dataPrevisaoSS) && !"0".Equals(sCN4ISAEReceive.dataPrevisaoSS))
                    {
                        // Atualizar dados do protocolo relacionado no Dynamics
                        if (!"".Equals(sCN4ISAESend.numeroProtocolo) && !"0".Equals(sCN4ISAESend.numeroProtocolo))
                        {
                            SCN4CRALReceive sCN4CRALReceive = executaReiteracao(sCN4ISAEReceive.protocoloSS, sCN4ISAESend.numeroProtocolo, sCN4ISAESend.Origem, sCN4ISAEReceive.dataPrevisaoSS);
                            dyn365Protocolo = new Dyn365Protocolo();
                            dyn365Protocolo.numeroProtocolo = sCN4ISAESend.numeroProtocolo;
                            dyn365Protocolo.numeroPriorizacao = sCN4CRALReceive.quantidadePR;
                            dyn365Protocolo.emailUnidadeResponsavel = sCN4CRALReceive.EmailUnidadeResponsavel;
                            IDyn365ProtocoloRepository dyn365ProtocoloRepositoryRelacionado = new Dyn365ProtocoloRepository();
                            List<Dyn365Protocolo> listRetornoProtocoloRelacionado = dyn365ProtocoloRepositoryRelacionado.Pesquisar("copasa_protocolo", sCN4ISAEReceive.protocoloSS);
                            if (listRetornoProtocoloRelacionado.Count == 1)
                            {
                                dyn365Protocolo.idSolicitacaoRelacionadaBind = listRetornoProtocoloRelacionado.ToArray()[0].incidentid;
                            }
                            IDyn365SuperintendenciaRepository dyn365SuperintendenciaRepository = new Dyn365SuperintendenciaRepository();
                            List<Dyn365Superintendencia> listSuperintendencia = dyn365SuperintendenciaRepository.Pesquisar("copasa_name", sCN4CRALReceive.superintendenciaResponsavel);
                            if (listSuperintendencia.Count == 1)
                            {
                                dyn365Protocolo.ideSuperintendenciaBind = listSuperintendencia.ToArray()[0].id;
                            }
                            else
                            {
                                sCN4CRALReceive.unidadeResponsavel = sCN4CRALReceive.superintendenciaResponsavel;
                            }
                            IDyn365UnidadeRepository dyn365UnidadeRepository = new Dyn365UnidadeRepository();
                            List<Dyn365Unidade> listUnidades = dyn365UnidadeRepository.Pesquisar("copasa_name", sCN4CRALReceive.unidadeResponsavel);
                            if (listUnidades.Count == 1)
                            {
                                dyn365Protocolo.idUnidadeEnvolvidaBind = listUnidades.ToArray()[0].id;
                            }
                            else
                            {
                                IDyn365EscritorioLocalRepository dyn365EscritorioLocalRepository = new Dyn365EscritorioLocalRepository();
                                List<Dyn365EscritorioLocal> listEscritorioLocal = dyn365EscritorioLocalRepository.Pesquisar("copasa_name", sCN4CRALReceive.unidadeResponsavel);
                                if (listUnidades.Count == 1)
                                {
                                    dyn365Protocolo.idUnidadeEnvolvidaBind = listEscritorioLocal.ToArray()[0].id;
                                }
                            }
                        }

                    }


                    IDyn365ProtocoloRepository dyn365ProtocoloRepository = new Dyn365ProtocoloRepository();
                    try
                    {
                        List<Dyn365Protocolo> listRetorno = dyn365ProtocoloRepository.Pesquisar("copasa_protocolo", dyn365Protocolo.numeroProtocolo);
                        if (listRetorno.Count == 1)
                        {
                            SCN4ISSSSend sCN4ISSSSend = new SCN4ISSSSend();
                            sCN4ISSSSend.matriculaImovel = sCN4ISAESend.matricula;
                            SCN4ISSSReceive sCN4ISSSReceive = (SCN4ISSSReceive)_iSSCN4ISSSRepository.Connect(sCN4ISSSSend);
                            foreach (SCN4ISSSReceiveSolicitacaoServico ss in sCN4ISSSReceive.solicitacoesServico)
                            {
                                if (sCN4ISAEReceive.numeroSS.Equals(ss.numeroSolicitacaoServico))
                                {
                                    dyn365Protocolo.incidentid = listRetorno.ToArray()[0].incidentid;
                                    dyn365Protocolo.situacaoSS = ss.codigoSituacao;
                                    dyn365Protocolo.dataPrevisaoSS = ss.dataPrevisaoAtendimento;
                                    dyn365Protocolo.horaPrevisaoSS = ss.horaPrevisaoAtendimento;
                                    dyn365Protocolo.descricaoSituacaoSS = ss.descricaoSituacao;
                                    _log.AddLog(" Atualizacao protocolo dynamics:");
                                    _log.AddLog(" " + dyn365Protocolo.getJson().Replace(",", "\r\n "));
                                    dyn365ProtocoloRepository.AtualizarAsync(dyn365Protocolo, _log);
                                    SCN4ISORSend sCN4ISORSend = new SCN4ISORSend();
                                    sCN4ISORSend.numeroSolicitacaoServico = ss.numeroSolicitacaoServico;
                                    SCN4ISORReceive sCN4ISORReceive = (SCN4ISORReceive)_iSSCN4ISORRepository.Connect(sCN4ISORSend);
                                    foreach (SCN4ISORReceiveOrdemServico os in sCN4ISORReceive.ordensServico)
                                    {
                                        Dyn365OrdemServico dyn365OrdemServico = new Dyn365OrdemServico();
                                        dyn365OrdemServico.situacaoOS = os.codigoSituacao;
                                        dyn365OrdemServico.descricaoSituacaoOS = os.descricaoSituacao;
                                        dyn365OrdemServico.idSolicitacaoBind = dyn365Protocolo.incidentid;
                                        dyn365OrdemServico.numeroOrdemServico = os.numeroOrdemServico;
                                        dyn365OrdemServico.dataPrevisaoOS = os.dataPrevisaoAtendimento;
                                        dyn365OrdemServico.horaPrevisaoOS = os.horaPrevisaoAtendimento;

                                        IDyn365OrdemServicoRepository dyn365OrdemServicoRepository = new Dyn365OrdemServicoRepository();
                                        dyn365OrdemServicoRepository.AtualizarAsync(dyn365OrdemServico, _log);
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            _log.AddLog("    Não foi possível atualizar o Dynamics, protocolo " + sCN4ISAESend.numeroProtocolo + " não encontrado");
                        }
                    }
                    catch (Exception e)
                    {
                        _log.AddLog("    Erro" + e.Message, true);
                    }
                }
            }
            else
            {
                sCN4ISAEReceive = new SCN4ISAEReceive();
                sCN4ISAEReceive.descricaoRetorno = mensagemErroValidar;
            }
            response.Model = sCN4ISAEReceive;
            return response;
        }


        /// <summary>
        /// Método para tratar e transportar um texto para um array de string para o IBM
        /// </summary>
        private string[] ConverteTextoParaArrayIBM(string textoEntrada,int tamanhoString)
        {
            try
            {
                textoEntrada = Util.Util.trataTextoIBM(textoEntrada.Trim());
                int tamanhoTotalObservacao = textoEntrada.Length;
                int quantidadeMaximaCaracteres = tamanhoString;
                int quantidadeOcorrencia = tamanhoTotalObservacao / quantidadeMaximaCaracteres;
                quantidadeOcorrencia++;
                if (quantidadeOcorrencia > 10)
                    quantidadeOcorrencia = 10;

                string[] observacao = new string[quantidadeOcorrencia];
                int posicao = 0;
                for (int i = 0; i < quantidadeOcorrencia; i++)
                {
                    if (posicao < textoEntrada.Length)
                    {
                        string texto = "";
                        if (posicao + quantidadeMaximaCaracteres < textoEntrada.Length)
                            texto = textoEntrada.Substring(posicao, quantidadeMaximaCaracteres);
                        else
                            texto = textoEntrada.Substring(posicao);
                        posicao += tamanhoString;
                        //texto = Util.Util.trataTextoIBM(texto);
                        observacao[i] = texto;
                    }
                    else
                        observacao[i] = "";
                }
                return observacao;
            }
            catch (Exception e)
            {
                string erro = e.Message;
                return new string[0];
            }
        }

        private string MontarMensagemRetornoCancelaSS(SCN4CASSReceive sCN4CASSReceive)
        {
            switch (sCN4CASSReceive.codigoErro)
            {
                case ("0"):
                    sCN4CASSReceive.descricaoRetorno = _mensagemRepository.parseMensagem("M0200");
                    break;
                case ("1"):
                    sCN4CASSReceive.descricaoRetorno = _mensagemRepository.parseMensagem("M0201");
                    break;
                case ("2"):
                    sCN4CASSReceive.descricaoRetorno = _mensagemRepository.parseMensagem("M0202");
                    break;
                case ("4"):
                    sCN4CASSReceive.descricaoRetorno = _mensagemRepository.parseMensagem("M0203");
                    break;
                case ("5"):
                    sCN4CASSReceive.descricaoRetorno = _mensagemRepository.parseMensagem("M0204");
                    break;
                case ("101"):
                    sCN4CASSReceive.descricaoRetorno = _mensagemRepository.parseMensagem("M0205");
                    break;
                case ("102"):
                    sCN4CASSReceive.descricaoRetorno = _mensagemRepository.parseMensagem("M0206");
                    break;
                case ("103"):
                    sCN4CASSReceive.descricaoRetorno = _mensagemRepository.parseMensagem("M0207");
                    break;
                case ("530"):
                    sCN4CASSReceive.descricaoRetorno = _mensagemRepository.parseMensagem("M0208");
                    break;
                case ("1388"):
                    sCN4CASSReceive.descricaoRetorno = _mensagemRepository.parseMensagem("M0209");
                    break;
                case ("1398"):
                    sCN4CASSReceive.descricaoRetorno = _mensagemRepository.parseMensagem("M0210");
                    break;
                case ("1399"):
                    sCN4CASSReceive.descricaoRetorno = _mensagemRepository.parseMensagem("M0211");
                    break;
            }

            return sCN4CASSReceive.descricaoRetorno;
        }

        /// <summary>
        /// Retorna a descrição do status da OS
        /// </summary>
        private string GetdescricaoStatusOS(string statusOS)
        {
            switch (statusOS)
            {
                case ("A"):
                    return "Serviço com programação automática";
                case ("C"):
                    return "Serviço cancelado";
                case ("D"):
                    return "SS baixada não executada";
                case ("E"):
                    return "Serviço executado";
                case ("G"):
                    return "Serviço gerado";
                case ("I"):
                    return "Serviço com programação imediata";
                case ("L"):
                    return "SS pendente";
                case ("M"):
                    return "Serviço baixado com baixa de materiais pendente";
                case ("N"):
                    return "Serviço não executado(com ida ao local)";
                case ("P"):
                    return "Serviço programado";
                case ("R"):
                    return "Serviço reprogramado";
                case ("S"):
                    return "Serviço selecionado";
                case ("T"):
                    return "SS com baixa temporaria";
                case ("U"):
                    return "Serviço não executado(sem ida ao local)";
                case ("V"):
                    return "Serviço de pesquisa (não é programável)";
                case ("Z"):
                    return "Serviço com programação alterada";
            }
                    return "";
        }

        /// <summary>
        /// Obtém as interrupções em uma determinada região na base GEO via WebService.
        /// </summary>
        /// <param name="matricula">Matrícula que será consultada na base GEO.</param>
        /// <returns>Lista com as interrupções consultada na base GEO através do WebService.</returns>
        private TrabInterrupcaoCopagis ObterInterrupcoesCopagis(string matricula)
        {
            DateTime tempoInicio = DateTime.Now;
            try
            {
                //Como é apenas uma consulta na base GEO, não é mais necessário separar por ambientes.
                string url = ConfigurationUtil.GetAppSetting("UrlArcGisConsultaInterrupcaoPorMatricula") + "matricula={0}";
                var hostUri = string.Format(url, matricula);
                var request = (HttpWebRequest)WebRequest.Create(hostUri);
                request.Method = "GET";
                var response = (HttpWebResponse)request.GetResponse();
                string content;
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }
                content = content.Replace("[\"", "");
                content = content.Replace("\"]", "");
                content = content.Replace("\",\"", ";");
                string[] retorno = content.Split(';');
                TrabInterrupcaoCopagis trabInterrupcaoCopagis = null;
                if (retorno.Length > 0 && retorno[0].Length > 0)
                {
                    trabInterrupcaoCopagis = new TrabInterrupcaoCopagis();
                    trabInterrupcaoCopagis.Descricao = retorno[0].Substring(0, 50).Trim();
                    trabInterrupcaoCopagis.DtInicio = DateTime.ParseExact(retorno[0].Substring(52, 12), "ddMMyyyyHHmm", null);
                    trabInterrupcaoCopagis.DtFim = DateTime.ParseExact(retorno[0].Substring(64, 12), "ddMMyyyyHHmm", null);
                }

                return trabInterrupcaoCopagis;
            }
            catch (Exception e)
            {
                gravaLog("Erro ao consultar o arcGis:" + e.Message);
                _log.GravaLogAdc("CopaGis-interrupcao", tempoInicio, e.Message);
                return null;
            }
        }


        /// <summary>
        /// Obtém as interrupções em uma determinada região na base GEO via WebService.
        /// </summary>
        /// <param name="codLocalidade">Código da Localidade que pode haver interrupção.</param>
        /// <param name="codBairro">Código do Bairro que pode haver interrupção.</param>
        /// <returns>Lista com as interrupções consultada na base GEO através do WebService.</returns>
        private TrabInterrupcaoCopagis ObterInterrupcoesCopagis(string codLocalidade, string codBairro)
        {
            try
            {
                //Como é apenas uma consulta na base GEO, não é mais necessário separar por ambientes.
                string url = ConfigurationUtil.GetAppSetting("UrlArcGisConsultaInterrupcaoPorBairro") + "localidade={0}&bairro={1}";
                var hostUri = string.Format(url, codLocalidade, codBairro);
                _log.AddLog("   url interrupcao: " + hostUri);
                var request = (HttpWebRequest)WebRequest.Create(hostUri);
                request.Method = "GET";
                var response = (HttpWebResponse)request.GetResponse();
                string content;
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }
                content = content.Replace("[\"", "");
                content = content.Replace("\"]", "");
                content = content.Replace("\",\"", ";");
                string[] retorno = content.Split(';');
                TrabInterrupcaoCopagis trabInterrupcaoCopagis = null;
                if (retorno.Length > 0 && retorno[0].Length > 0)
                {
                    trabInterrupcaoCopagis = new TrabInterrupcaoCopagis();
                    trabInterrupcaoCopagis.Descricao = retorno[0].Substring(0, 50).Trim();
                    trabInterrupcaoCopagis.DtInicio = DateTime.ParseExact(retorno[0].Substring(52, 12), "ddMMyyyyHHmm", null);
                    trabInterrupcaoCopagis.DtFim = DateTime.ParseExact(retorno[0].Substring(64, 12), "ddMMyyyyHHmm", null);
                }

                return trabInterrupcaoCopagis;
            }
            catch (Exception e)
            {
                gravaLog("Erro ao consultar o arcGis:" + e.Message);
                return null;
            }
        }

        /// <summary>
        /// CodigoOSFaldaDAgua.
        /// </summary>
        //public const string CodigoOSFaldaDAgua = "1500100";
        public string CodigoOSFaldaDAgua { get; }


        /// <summary>
        /// Rotina que executa reiteração de uma SS.
        /// </summary>
        public SCN4CRALReceive executaReiteracao(string protocoloSS, string protocoloAtendimento,string origem,string dataPrevisao)
        {
            _log.AddLog("  Reiteracao protocoloSS:" + protocoloSS + " protocoloAtendimento:"+ protocoloAtendimento + " origem" + origem + " dataPrevisao:" + dataPrevisao);

            SCN4CRALSend sCN4CRALSend = new SCN4CRALSend();
            sCN4CRALSend.protocoloSS = protocoloSS;
            sCN4CRALSend.protocoloAtendimento = protocoloAtendimento;
            sCN4CRALSend.Origem = origem;
            sCN4CRALSend.dataPrevisao = dataPrevisao.ToDateTime("yyyyMMdd").ToString("M/d/yyyy");
            SCN4CRALReceive sCN4CRALReceive = null;
            try
            {
                sCN4CRALReceive = (SCN4CRALReceive)SCN4CRAL(sCN4CRALSend).Model;
                _log.AddLog("  Retorno SCN4CRAL:" + sCN4CRALReceive.ToString());
            }
            catch (Exception)
            {
                sCN4CRALReceive = new SCN4CRALReceive();
                sCN4CRALReceive.descricaoRetorno = "ERRO";
            }
            return sCN4CRALReceive;
        }
        //public BaseResponse ListaSolicitacaoServicoDyn(DListaSolicitacaoServicoDynSend dListaSolicitacaoServicoDynSend)
        //{
        //    DListaSolicitacaoServicoDynReceive dynReceive  = new DListaSolicitacaoServicoDynReceive();
        //    DRepository dRepository = new DRepository("copasa_ordemdeservicos", "Dyn365Host");
        //    Dyn365OrdemServico dyn365OrdemServico = new Dyn365OrdemServico();

        //    List<DListaIdentificadorReceive> ListaRetorno = new List<DListaIdentificadorReceive>();

        //    string condicao = $"$filter=copasa_cpf_cnpj eq '{dListaSolicitacaoServicoDynSend.IdCpfCnpj}'";
        //    dynReceive.OrdensDeServico = dRepository.DPesquisarLista(condicao, dyn365OrdemServico.GetType()).Cast<Dyn365OrdemServico>().ToList();

        //    //caso de lista vazia.
        //    if (dynReceive.OrdensDeServico.Count == 0)
        //    {
        //        dynReceive.IsValid = false;
        //        dynReceive.descricaoRetorno = "Protocolos não encontrados.";
        //    }
        //    var response = new BaseResponse();
        //    response.Model = dynReceive;
        //    return response;
        //}

    }
}
