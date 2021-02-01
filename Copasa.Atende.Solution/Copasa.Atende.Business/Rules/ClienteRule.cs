using Copasa.Atende.Business.Core;
using Copasa.Atende.Business.Interfaces;
using Copasa.Atende.Business.Rules.dyn365;
using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Model.Digital;
using Copasa.Atende.Model.Dyn365;
using Copasa.Atende.Repository;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Repository.Repositories;
using Copasa.Atende.Repository.Repositories.Digital;
using Copasa.Atende.Repository.Repositories.Dyn365;
using Copasa.Atende.Util;
using Copasa.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Caching;


namespace Copasa.Atende.Business.Rules
{
    /// <summary>
    /// Rule - Cliente.
    /// </summary>
    public class ClienteRule : BaseRule, IClienteRule
    {
        /// <summary>
        /// Classe de retorno para falta de água
        /// </summary>
        public class InterrupcaoAgua
        {
            /// <summary>
            /// Flag que indica se existe interrupção
            /// </summary>
            public string temInterrupcaoAgua { get; set; }

            /// <summary>
            /// Mensagem de retorno que informa os dados da interrupção
            /// </summary>
            public string mensagemRetorno { get; set; }
        }

        private IBrokerSCN3PCLIRepository _brokerSCN3PCLIRepository;
        private IBrokerSCN6UEFIRepository _brokerSCN6UEFIRepository;
        private IISSCN4ISU1Repository _iSSCN4ISU1Repository;
        private IISSCN4ISU1_IdentificadoresRepository _iSSCN4ISU1_IdentificadoresRepository;
        private IISSCN5ISHCRepository _iISSCN5ISHCRepository;
        private IISSCN4ISFARepository _iISSCN4ISFARepository;
        private IISSCN6ISCNRepository _iISSCN6ISCNRepository;
        private IISSCN6ISCERepository _iISSCN6ISCERepository;
        private IISSCN3ISPSRepository _iISSCN3ISPSRepository;
        private IISSCN3ISMTRepository _iISSCN3ISMTRepository;
        private IISSCN6ISAVRepository _iSSCN6ISAVRepository;
        private IISSCN6ISCCRepository _iSSCN6ISCCRepository;
        private IISSCN6ISQARepository _iSSCN6ISQARepository;
        private IISSCN4ISACRepository _iSSCN4ISACRepository;
        private IFaturaRule _faturaRule;
        private IMensagemRepository _mensagemRepository;
        private IServicoOperacionalRule _servicoOperacionalRule;
        private IAtendimentoRule _atendimentoRule;
        private IInformarLeituraRule _informarLeituraRule;
        //private IAzureRepository _azureRepository;
        private List<TrabInterrupcaoCopagis> _interrupcoesCopaGis;
        private SCN4ISFAReceive _interrupcoesSicom;
        private ILog _log;

        /// <summary>
        /// Construtor Cliente.
        /// </summary>
        /// <param name="brokerSCN3PCLIRepository">IBrokerSCN3PCLIRepository.</param>
        /// <param name="brokerSCN6UEFIRepository">IBrokerSCN6UEFIRepository.</param>
        /// <param name="iSSCN4ISU1Repository">IISSCN4ISU1Repository.</param>
        /// <param name="iSSCN4ISU1_IdentificadoresRepository">IISSCN4ISU1_IdentificadoresRepository.</param>
        /// <param name="iSSCN5ISHCRepository">IISSCN5ISHCRepository.</param>
        /// <param name="iSSCN4ISFARepository">IISSCN4ISFARepository.</param>
        /// <param name="iSSCN6ISCNRepository">IISSCN6ISCNRepository.</param>
        /// <param name="iSSCN6ISCERepository">IISSCN6ISCERepository.</param>
        /// <param name="iSSCN3ISPSRepository">IISSCN3ISPSRepository.</param>
        /// <param name="iSSCN3ISMTRepository">IISSCN3ISMTRepository.</param>
        /// <param name="iSSCN6ISAVRepository">IISSCN6ISAVRepository.</param>
        /// <param name="iSSCN6ISCCRepository">IISSCN6ISCCRepository.</param>
        /// <param name="iSSCN6ISQARepository">IISSCN6ISQARepository.</param>
        /// <param name="iSSCN4ISACRepository">IISSCN4ISACRepository.</param>
        /// <param name="faturaRule">IFaturaRule.</param>
        /// <param name="mensagemRepository">IMensagemRepository.</param>
        /// <param name="servicoOperacionalRule">IServicoOperacionalRule.</param>
        /// <param name="atendimentoRule">IAtendimentoRule.</param>
        /// <param name="informarLeituraRule">IInformarLeituraRule.</param>
        /// <param name="log">ILog</param>
        public ClienteRule(
            IBrokerSCN3PCLIRepository brokerSCN3PCLIRepository,
            IBrokerSCN6UEFIRepository brokerSCN6UEFIRepository,
            IISSCN4ISU1Repository iSSCN4ISU1Repository,
            IISSCN4ISU1_IdentificadoresRepository iSSCN4ISU1_IdentificadoresRepository,
            IISSCN5ISHCRepository iSSCN5ISHCRepository,
            IISSCN4ISFARepository iSSCN4ISFARepository,
            IISSCN6ISCNRepository iSSCN6ISCNRepository,
            IISSCN6ISCERepository iSSCN6ISCERepository,
            IISSCN3ISPSRepository iSSCN3ISPSRepository,
            IISSCN3ISMTRepository iSSCN3ISMTRepository,
            IISSCN6ISAVRepository iSSCN6ISAVRepository,
            IISSCN6ISCCRepository iSSCN6ISCCRepository,
            IISSCN6ISQARepository iSSCN6ISQARepository,
            IISSCN4ISACRepository iSSCN4ISACRepository,
            IFaturaRule faturaRule,
            IMensagemRepository mensagemRepository,
            IServicoOperacionalRule servicoOperacionalRule,
            IAtendimentoRule atendimentoRule,
            IInformarLeituraRule informarLeituraRule,
            ILog log)
        {
            _brokerSCN3PCLIRepository = brokerSCN3PCLIRepository;
            _brokerSCN6UEFIRepository = brokerSCN6UEFIRepository;
            _iSSCN4ISU1Repository = iSSCN4ISU1Repository;
            _iSSCN4ISU1_IdentificadoresRepository = iSSCN4ISU1_IdentificadoresRepository;
            _iISSCN5ISHCRepository = iSSCN5ISHCRepository;
            _iISSCN4ISFARepository = iSSCN4ISFARepository;
            _iISSCN6ISCNRepository = iSSCN6ISCNRepository;
            _iISSCN6ISCERepository = iSSCN6ISCERepository;
            _iISSCN3ISPSRepository = iSSCN3ISPSRepository;
            _iISSCN3ISMTRepository = iSSCN3ISMTRepository;
            _iSSCN6ISAVRepository = iSSCN6ISAVRepository;
            _iSSCN6ISCCRepository = iSSCN6ISCCRepository;
            _iSSCN6ISQARepository = iSSCN6ISQARepository;
            _iSSCN4ISACRepository = iSSCN4ISACRepository;
            _faturaRule = faturaRule;
            _mensagemRepository = mensagemRepository;
            _servicoOperacionalRule = servicoOperacionalRule;
            _atendimentoRule = atendimentoRule;
            _informarLeituraRule = informarLeituraRule;
            _log = log;
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lista clientes de um CPF ou CNPJ ou Identificador.
        /// </summary>
        public BaseResponse SCN3PCLI(SCN3PCLISend sCN3PCLISend)
        {
            string cpfCnpjTratado = retiraCaracteresMascara(sCN3PCLISend.CpfCnpj);
            sCN3PCLISend.CpfCnpjSicom = long.Parse(cpfCnpjTratado);
            sCN3PCLISend.identificadorSicom = 0;
            SCN3PCLIReceive sCN3PCLIReceive = new SCN3PCLIReceive();
            SCN4ISU1_IdentificadoresSend sCN4ISU1_IdentificadoresSend = new SCN4ISU1_IdentificadoresSend();
            sCN4ISU1_IdentificadoresSend.cpfCnpj = sCN3PCLISend.CpfCnpjSicom.ToString();
            sCN4ISU1_IdentificadoresSend.identificadores = new string[0];
            BaseResponse baseResponse = SCN4ISU1Identificadores(sCN4ISU1_IdentificadoresSend);
            SCN4ISU1_IdentificadoresReceive sCN4ISU1_IdentificadoresReceive = (SCN4ISU1_IdentificadoresReceive)baseResponse.Model;
            sCN3PCLIReceive.identificadores = new SCN3PCLIReceiveIdentificador[sCN4ISU1_IdentificadoresReceive.identificadores.Count];
            sCN3PCLIReceive.descricaoRetorno = sCN4ISU1_IdentificadoresReceive.descricaoRetorno;
            int indice = 0;
            foreach (SCN4ISU1ReceiveUsuarios identificador in sCN4ISU1_IdentificadoresReceive.identificadores)
            {
                sCN3PCLIReceive.identificadores[indice] = new SCN3PCLIReceiveIdentificador();
                sCN3PCLIReceive.identificadores[indice].identificador = int.Parse(identificador.identificador);
                sCN3PCLIReceive.identificadores[indice].email = identificador.emailUsuario;
                sCN3PCLIReceive.identificadores[indice].nome = identificador.nomeUsuario;
                sCN3PCLIReceive.identificadores[indice].telefoneCelular = identificador.telefoneCelular;
                sCN3PCLIReceive.identificadores[indice].telefoneComercial = identificador.telefoneComercial;
                sCN3PCLIReceive.identificadores[indice].telefoneResidencia = identificador.telefoneResidencia;

                sCN3PCLIReceive.identificadores[indice].tipoLogradouro = identificador.tipoLogradouroUsuario;
                sCN3PCLIReceive.identificadores[indice].nomeLogradouro = identificador.nomeLogradouro;
                sCN3PCLIReceive.identificadores[indice].numeroImovel = int.Parse(identificador.numeroLogradouroUsuario);
                sCN3PCLIReceive.identificadores[indice].tipoComplementoImovel = identificador.tipoComplementoLogradouroUsuario;
                sCN3PCLIReceive.identificadores[indice].complementoImovel = identificador.complementoLogradouroUsuario;
                sCN3PCLIReceive.identificadores[indice].numeroImovel = int.Parse(identificador.numeroLogradouroUsuario);
                sCN3PCLIReceive.identificadores[indice].bairro = identificador.bairro;
                sCN3PCLIReceive.identificadores[indice].localidade = identificador.localidade;
                sCN3PCLIReceive.identificadores[indice].logradouro = identificador.nomeLogradouro;
                foreach (SCN4ISU1ReceiveMatriculasUsuarios matricula in identificador.matriculas)
                {
                    
                    if (!"".Equals(matricula.nomeLogradouro) && !"".Equals(identificador.nomeLogradouro)
                        && matricula.nomeLogradouro.Equals(identificador.nomeLogradouro))
                    {
                        sCN3PCLIReceive.identificadores[indice].CEP = matricula.CEP;
                        break;
                    }
                }
                indice++;
            }
            baseResponse.Model = sCN3PCLIReceive;
            /*
            BaseResponse baseResponse = _brokerSCN3PCLIRepository.Connect(sCN3PCLISend);
            SCN3PCLIReceive sCN3PCLIReceive = (SCN3PCLIReceive)baseResponse.Model;
            if (!"".Equals(sCN3PCLIReceive.descricaoRetornoSicom))
            {
                if (sCN3PCLISend.Origem.ToUpper().Equals("APP") && sCN3PCLIReceive.descricaoRetornoSicom.Contains("CPF/CNPJ"))
                {
                    sCN3PCLIReceive.descricaoRetornoSicom = sCN3PCLIReceive.descricaoRetornoSicom.Replace("CPF/CNPJ", "CPF");
                }
                sCN3PCLIReceive.descricaoRetorno = sCN3PCLIReceive.descricaoRetornoSicom;
            }
            foreach (SCN3PCLIReceiveIdentificador us in sCN3PCLIReceive.identificadores)
            {
                us.logradouro = (us.tipoLogradouro + " " + us.nomeLogradouro).Trim();
                us.complementoImovel = (us.tipoComplementoImovel + " " + us.complementoImovel).Trim();

                if (us.telefoneCelularSicom > 0)
                {
                    string telCelular = us.telefoneCelularSicom.ToString();
                    int tamCelular = telCelular.Length;
                    if (tamCelular > 4)
                        us.telefoneCelular = "(" + us.DDDCelularSicom + ") " + telCelular.Substring(0, tamCelular - 4) + "-" + telCelular.Substring(tamCelular - 4);
                }
                else
                    us.telefoneCelular = "";
                if (us.telefoneComercialSicom > 0)
                {
                    string telComercial = us.telefoneComercialSicom.ToString();
                    if (telComercial.Length > 4)
                        us.telefoneComercial = "(" + us.DDDComercialSicom + ") " + telComercial.Substring(0, 4) + "-" + telComercial.Substring(4);
                }
                else
                    us.telefoneComercial = "";
                if (us.telefoneResidenciaSicom > 0)
                {
                    string telResidencia = us.telefoneResidenciaSicom.ToString();
                    if (telResidencia.Length > 4)
                        us.telefoneResidencia = "(" + us.DDDResidenciaSicom + ") " + telResidencia.Substring(0, 4) + "-" + telResidencia.Substring(4);
                }
                else
                    us.telefoneResidencia = "";
                us.CEP = us.CEP.Substring(0, 2) + "." + us.CEP.Substring(2, 3) + "-" + us.CEP.Substring(5);
            }
            */
            return baseResponse;
        }

        /// <summary>
        /// Atualiza status para envio conta por email.
        /// </summary>
        public BaseResponse SCN6ISCE(SCN6ISCESend sCN6ISCESend)
        {
            foreach (SCN6ISCESendMatricula matricula in sCN6ISCESend.matriculas)
                _log.AddLog(" Matricula:" + matricula.ToString());
            SCN6ISCEReceive sCN6ISCEReceive = (SCN6ISCEReceive)_iISSCN6ISCERepository.Connect(sCN6ISCESend);
            BaseResponse retorno = new BaseResponse();
            retorno.Model = sCN6ISCEReceive;
            return retorno;
        }

        /// <summary>
        /// Altera email e telefone.
        /// </summary>
        public BaseResponse SCN4ISAC(SCN4ISACSend sCN4ISACSend)
        {
            SCN4ISACReceive sCN4ISACReceive = (SCN4ISACReceive)_iSSCN4ISACRepository.Connect(sCN4ISACSend);
            if (!"".Equals(sCN4ISACReceive.descricaoRetorno))
                sCN4ISACReceive.descricaoRetorno = _mensagemRepository.geraMensagem(sCN4ISACReceive.descricaoRetorno);
            BaseResponse retorno = new BaseResponse();
            retorno.Model = sCN4ISACReceive;
            return retorno;
        }

        /// <summary>
        /// Informa identificador e retorna matriculas.
        /// </summary>
        public BaseResponse SCN6UEFI(long identificador)
        {
            SCN6UEFISend sCN6UEFISend = new SCN6UEFISend();
            sCN6UEFISend.identificador = identificador;
            BaseResponse retorno = _brokerSCN6UEFIRepository.Connect(sCN6UEFISend);
            SCN6UEFIReceive sCN6UEFIReceive = (SCN6UEFIReceive)retorno.Model;
            //sCN6UEFIReceive.matriculas = new List<string>();
            for (int i = 0; i < sCN6UEFIReceive.total; i++)
            {
                if (!"".Equals(sCN6UEFIReceive.matriculaSicom[i]))
                {
                    SCN6UEFIReceiveMatriculas matricula = new SCN6UEFIReceiveMatriculas();
                    matricula.matricula = sCN6UEFIReceive.matriculaSicom[i];
                    matricula.endereco = sCN6UEFIReceive.endereco[i];
                    matricula.inicio = sCN6UEFIReceive.inicio[i];
                    matricula.termino = sCN6UEFIReceive.termino[i];
                    matricula.bairro = sCN6UEFIReceive.bairro[i];
                    matricula.localidade = sCN6UEFIReceive.localidade[i];
                    sCN6UEFIReceive.matriculas.Add(matricula);
                    //sCN6UEFIReceive.matriculas.Add(matSicom);
                }
            }
            return retorno;
        }

        /// <summary>
        /// Verifica se identificador é uma pessoa jurídica
        /// </summary>
        public BaseResponse validaCNPJ(TrabValidaCNPJSend trabValidaCNPJSend)
        {
            TrabValidaCNPJReceive trabValidaCNPJReceive = new TrabValidaCNPJReceive();
            trabValidaCNPJReceive.confirmacao = "";
            trabValidaCNPJReceive.descricaoRetorno = "Identificador não localizado.";
            SCN4ISU1Send sCN4ISU1Send = new SCN4ISU1Send();
            string[] identificadores = new string[1];
            identificadores[0] = trabValidaCNPJSend.identificador;
            sCN4ISU1Send.identificadores = identificadores;
            SCN4ISU1Receive sCN4ISU1Receive = (SCN4ISU1Receive)SCN4ISU1(sCN4ISU1Send).Model;
            foreach (SCN4ISU1ReceiveMatriculas matricula in sCN4ISU1Receive.matriculas)
            {
                if (matricula.cpfCnpj.Length > 11)
                {
                    trabValidaCNPJReceive.confirmacao = "S";
                    trabValidaCNPJReceive.descricaoRetorno = "";
                    trabValidaCNPJReceive.CNPJ = matricula.cpfCnpj;
                }
                else
                {
                    trabValidaCNPJReceive.confirmacao = "N";
                    trabValidaCNPJReceive.descricaoRetorno = "Não é pessoa jurídica.";
                    trabValidaCNPJReceive.CNPJ = "";

                }
                break;
            }
            BaseResponse baseResponse = new BaseResponse();
            baseResponse.Model = trabValidaCNPJReceive;
            return baseResponse;
        }

        /// <summary>
        /// Lista nome de identificadores.
        /// </summary>
        public BaseResponse SCN4ISU1Nomes(SCN4ISU1_NomesSend sCN4ISU1_NomesSend)
        {
            SCN4ISU1_IdentificadoresSend sCN4ISU1_IdentificadoresSend = new SCN4ISU1_IdentificadoresSend();
            sCN4ISU1_IdentificadoresSend.cpfCnpj = sCN4ISU1_NomesSend.cpfCnpj;
            sCN4ISU1_IdentificadoresSend.identificadores = sCN4ISU1_NomesSend.identificadores;
            SCN4ISU1_IdentificadoresReceive sCN4ISU1_IdentificadoresReceive = (SCN4ISU1_IdentificadoresReceive)_iSSCN4ISU1_IdentificadoresRepository.Connect(sCN4ISU1_IdentificadoresSend);
            SCN4ISU1_NomesReceive sCN4ISU1_NomesReceive = new SCN4ISU1_NomesReceive();
            sCN4ISU1_NomesReceive.descricaoRetorno = sCN4ISU1_IdentificadoresReceive.descricaoRetorno;
            foreach (SCN4ISU1ReceiveUsuarios usuario in sCN4ISU1_IdentificadoresReceive.identificadores)
            {
                SCN4ISU1ReceiveUsuariosNome result =
                    sCN4ISU1_NomesReceive.identificadores.Where(x => x.cpfCnpj == usuario.cpfCnpj).FirstOrDefault();
                if (result == null)
                {
                    SCN4ISU1ReceiveUsuariosNome sCN4ISU1ReceiveUsuariosNome = new SCN4ISU1ReceiveUsuariosNome();
                    sCN4ISU1ReceiveUsuariosNome.cpfCnpj = usuario.cpfCnpj;
                    sCN4ISU1ReceiveUsuariosNome.nomeUsuario = usuario.nomeUsuario;
                    sCN4ISU1_NomesReceive.identificadores.Add(sCN4ISU1ReceiveUsuariosNome);
                }
            }
            BaseResponse retorno = new BaseResponse();
            retorno.Model = sCN4ISU1_NomesReceive;
            return retorno;
        }

        /// <summary>
        /// Lista identificadores.
        /// </summary>
        public BaseResponse SCN4ISU1Identificadores(SCN4ISU1_IdentificadoresSend sCN4ISU1_IdentificadoresSend)
        {
            BaseResponse retorno = new BaseResponse();
            SCN4ISU1_IdentificadoresReceive sCN4ISU1_IdentificadoresReceive;
            for(int i=0;i< sCN4ISU1_IdentificadoresSend.identificadores.Length;i++)
            {
                sCN4ISU1_IdentificadoresSend.identificadores[i] = sCN4ISU1_IdentificadoresSend.identificadores[i].Trim();
                string identificador = sCN4ISU1_IdentificadoresSend.identificadores[i];
                long parseLong;
                if (!"".Equals(identificador) && !long.TryParse(identificador.Trim(),out parseLong))
                {
                    sCN4ISU1_IdentificadoresReceive = new SCN4ISU1_IdentificadoresReceive();
                    sCN4ISU1_IdentificadoresReceive.descricaoRetorno = "Identificador informado está incorreto";
                    retorno.Model = sCN4ISU1_IdentificadoresReceive;
                    return retorno;
                }
            }
            sCN4ISU1_IdentificadoresReceive = (SCN4ISU1_IdentificadoresReceive)_iSSCN4ISU1_IdentificadoresRepository.Connect(sCN4ISU1_IdentificadoresSend);
            /*
            List<SCN4ISU1ReceiveUsuarios> identificadoresAdicionais = new List<SCN4ISU1ReceiveUsuarios>();
            foreach (SCN4ISU1ReceiveUsuarios usuario in sCN4ISU1_IdentificadoresReceive.identificadores)
            {
                if (!"".Equals(usuario.cpfCnpj))
                {
                    SCN3PCLISend sCN3PCLISend = new SCN3PCLISend();
                    sCN3PCLISend.CpfCnpj = Util.Util.retiraCaracteresMascara(usuario.cpfCnpj);
                    SCN3PCLIReceive sCN3PCLIReceive = (SCN3PCLIReceive)SCN3PCLI(sCN3PCLISend).Model;
                    if ("".Equals(sCN3PCLIReceive.descricaoRetornoSicom))
                    {
                        foreach (SCN3PCLIReceiveIdentificador identificador in sCN3PCLIReceive.identificadores)
                        {
                            SCN4ISU1ReceiveUsuarios result =
                                sCN4ISU1_IdentificadoresReceive.identificadores.Where(x => x.identificador == identificador.identificador.ToString()).FirstOrDefault();
                            SCN4ISU1ReceiveUsuarios usuarioAtualizar = null;
                            if (result != null)
                            {
                                usuarioAtualizar = usuario;
                                usuarioAtualizar.tipoLogradouroUsuario = identificador.tipoLogradouro;
                                usuarioAtualizar.nomeLogradouro = identificador.nomeLogradouro;
                                usuarioAtualizar.numeroLogradouroUsuario = identificador.numeroImovel.ToString();
                                usuarioAtualizar.tipoComplementoLogradouroUsuario = identificador.tipoComplementoImovel;
                                usuarioAtualizar.complementoLogradouroUsuario = identificador.complementoImovel;
                                usuarioAtualizar.numeroLogradouroUsuario = identificador.numeroImovel.ToString();
                                usuarioAtualizar.bairro = identificador.bairro;
                                usuarioAtualizar.localidade = identificador.localidade;
                            }
                        }
                    }
                }
            }
            foreach (SCN4ISU1ReceiveUsuarios usuario in identificadoresAdicionais)
                sCN4ISU1_IdentificadoresReceive.identificadores.Add(usuario);
            */
            retorno.Model = sCN4ISU1_IdentificadoresReceive;
            return retorno;
        }

        /// <summary>
        /// Lista matriculas e identificadores de um CPF ou CNPJ.
        /// </summary>
        public BaseResponse SCN4ISU1(SCN4ISU1Send sCN4ISU1Send)
        {
            string mensagemErroValidar;
            SCN4ISU1Receive sCN4ISU1Receive = new SCN4ISU1Receive();
            bool temIdentificador = false;
            if (sCN4ISU1Send.validar(out mensagemErroValidar))
            {
                sCN4ISU1Send.cpfCnpj = Util.Util.retiraCaracteresMascara(sCN4ISU1Send.cpfCnpj);
                string cpfCnpjAux = sCN4ISU1Send.cpfCnpj;
                try
                {
                    if (sCN4ISU1Send.cpfCnpj == null || "".Equals(sCN4ISU1Send.cpfCnpj.Trim()))
                    {
                        sCN4ISU1Send.cpfCnpj = "0";
                    }
                    else
                    {
                        long cpfCnpjNum = long.Parse(sCN4ISU1Send.cpfCnpj);
                        if (cpfCnpjNum > 0)
                        {
                            if (cpfCnpjNum > 999999999999)
                            {
                                sCN4ISU1Send.flagTipoUsu = "J";
                            }
                            else
                            {
                                sCN4ISU1Send.flagTipoUsu = "F";
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    sCN4ISU1Receive.descricaoRetorno = "Cpf ou cnpj inválido: " + cpfCnpjAux;
                    BaseResponse response = new BaseResponse();
                    response.Model = sCN4ISU1Receive;
                    return response;
                }

                if (sCN4ISU1Send.identificadores != null)
                {
                    foreach (string identificador in sCN4ISU1Send.identificadores)
                    {
                        if (!"".Equals(identificador) && !"0".Equals(identificador))
                            temIdentificador = true;
                        /*
                        long numLong;
                        if (identificador != null && !"".Equals(identificador.Trim()) && !long.TryParse(identificador, out numLong))
                        {
                            sCN4ISU1Receive.descricaoRetorno = _mensagemRepository.geraMensagem("M0036");
                            BaseResponse response = new BaseResponse();
                            response.Model = sCN4ISU1Receive;
                            return response;
                        }
                        */
                    }
                }
                sCN4ISU1Receive = (SCN4ISU1Receive)_iSSCN4ISU1Repository.Connect(sCN4ISU1Send);
                if (sCN4ISU1Receive.IsValid)
                {
                    var matriculas = sCN4ISU1Receive.matriculas;
                    //var matriculasFiltradas = matriculas.GroupBy(p => p.matricula).Select(g => g.First()).ToList();
                    //sCN4ISU1Receive.matriculas = matriculasFiltradas;
                    if ("".Equals(sCN4ISU1Receive.descricaoRetorno))
                    {
                        foreach (SCN4ISU1ReceiveMatriculas matricula in sCN4ISU1Receive.matriculas)
                        {
                            if (matricula.cpfCnpj.Equals(sCN4ISU1Send.cpfCnpj))
                                matricula.associacao = "SICOM";
                            else
                                matricula.associacao = "CAtende";
                            if (!"".Equals(matricula.cpfCnpj) && !"".Equals(sCN4ISU1Send.cpfCnpj))
                            {
                                if (retiraCaracteresMascara(matricula.cpfCnpj).Equals(sCN4ISU1Send.cpfCnpj))
                                {
                                    matricula.associacao = "SICOM";
                                }
                                else
                                {
                                    matricula.associacao = "C. Atende";
                                }
                            }
                            SCN6ISFDSend sCN6ISFDSend = new SCN6ISFDSend();
                            sCN6ISFDSend.identificador = matricula.identificador;
                            sCN6ISFDSend.matricula = matricula.matricula;
                            BaseResponse retornoSCN6ISFD = _faturaRule.SCN6ISFD(sCN6ISFDSend, false);
                            if (retornoSCN6ISFD.IsValid)
                            {
                                SCN6ISFDReceive sCN6ISFDReceive = (SCN6ISFDReceive)retornoSCN6ISFD.Model;
                                matricula.valorDebito = sCN6ISFDReceive.valorTotalDebito;
                            }
                            else
                                matricula.valorDebito = "0,00";

                            if (!string.IsNullOrWhiteSpace(sCN4ISU1Send.codigoServicoSolicitado))
                            {
                                string codigoServico = sCN4ISU1Send.codigoServicoSolicitado.Substring(0, 5);
                                //Falta de água - 1ª opção
                                //case "15001":
                                //Falta de água - 2ª opção
                                if ("15002".Equals(codigoServico))
                                {
                                    var codigoBairroFormatado = matricula.codigoBairro.Substring(9, 6);
                                    matricula.temInterrupcaoAgua = "N";
                                    //var retornoInterrupcao = VerificarInterrupcaoSicomGis(matricula.matricula, matricula.codigoLocalidade, codigoBairroFormatado);
                                    SCN4ISFASend sCN4ISFASend = new SCN4ISFASend();
                                    sCN4ISFASend.matricula = matricula.matricula;
                                    _interrupcoesSicom = (SCN4ISFAReceive)_iISSCN4ISFARepository.Connect(sCN4ISFASend);
                                    if (_interrupcoesSicom.temInterrupcao == "S")
                                    {
                                        matricula.temInterrupcaoAgua = "S";
                                        matricula.mensagemRetorno = "Estamos efetuando manutenção na rede de abastecimento de água na sua região, " + _interrupcoesSicom.descricaoMotivo + " e a previsão de solução é " + _interrupcoesSicom.dataPrevisao + " até às " + _interrupcoesSicom.horaPrevisao;
                                    }
                                    else
                                    {
                                        //var codigoBairroFormatado = matricula.codigoBairro.Substring(9, 6);
                                        _interrupcoesCopaGis = ObterInterrupcoesCopagis(matricula.matricula, matricula.codigoLocalidade, codigoBairroFormatado);
                                        if (_interrupcoesCopaGis.Count > 0)
                                        {
                                            var dataPrevisao = _interrupcoesCopaGis.Select(x => x.DtFim).FirstOrDefault().ToString();
                                            var dataPrevisaoFormatada = dataPrevisao.Substring(0, 10);
                                            var horaPrevisaoFormtada = dataPrevisao.Substring(11, 8);
                                            matricula.temInterrupcaoAgua = "S";
                                            matricula.mensagemRetorno = "Estamos efetuando manutenção na rede de abastecimento de água na sua região, " + _interrupcoesCopaGis.Select(x => x.Descricao).FirstOrDefault() + " e a previsão de solução é " + dataPrevisaoFormatada + " até às " + horaPrevisaoFormtada;
                                        }
                                    }
                                }
                                else if (Util.Util.isInArray(new string[10] { "FAT28", "11301", "11322", "11307", "11310", "11360", "11501", "11511", "11509", "60301" }, codigoServico))
                                {
                                    if (sCN4ISU1Send.cpfCnpj != matricula.cpfCnpj)
                                    {
                                        sCN4ISU1Receive.matriculas.Remove(matricula);
                                    }
                                }
                            }
                            else
                            {
                                matricula.temInterrupcaoAgua = "N";
                                SCN4ISFASend sCN4ISFASend = new SCN4ISFASend();
                                sCN4ISFASend.matricula = matricula.matricula;
                                _interrupcoesSicom = (SCN4ISFAReceive)_iISSCN4ISFARepository.Connect(sCN4ISFASend);
                                if (_interrupcoesSicom.temInterrupcao == "S")
                                {
                                    matricula.temInterrupcaoAgua = "S";
                                    matricula.mensagemRetorno = "Estamos efetuando manutenção na rede de abastecimento de água na sua região, " + _interrupcoesSicom.descricaoMotivo + " e a previsão de solução é " + _interrupcoesSicom.dataPrevisao + " até às " + _interrupcoesSicom.horaPrevisao;
                                }
                                else
                                {
                                    if (matricula.codigoBairro != null && matricula.codigoBairro.Length >= 15)
                                    {
                                        var codigoBairroFormatado = matricula.codigoBairro.Substring(9, 6);
                                        _interrupcoesCopaGis = ObterInterrupcoesCopagis(matricula.matricula, matricula.codigoLocalidade, codigoBairroFormatado);
                                        if (_interrupcoesCopaGis.Count > 0)
                                        {
                                            var dataPrevisao = _interrupcoesCopaGis.Select(x => x.DtFim).FirstOrDefault().ToString();
                                            var dataPrevisaoFormatada = dataPrevisao.Substring(0, 10);
                                            var horaPrevisaoFormtada = dataPrevisao.Substring(11, 8);
                                            matricula.temInterrupcaoAgua = "S";
                                            matricula.mensagemRetorno = "Estamos efetuando manutenção na rede de abastecimento de água na sua região, " + _interrupcoesCopaGis.Select(x => x.Descricao).FirstOrDefault() + " e a previsão de solução é " + dataPrevisaoFormatada + " até às " + horaPrevisaoFormtada;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (sCN4ISU1Receive.matriculas.Count() == 0)
                {
                    if (temIdentificador)
                    {
                        sCN4ISU1Receive.descricaoRetorno = _mensagemRepository.geraMensagem("M0194");
                    }
                    else
                    {
                        string mensagem = _mensagemRepository.geraMensagem("M0058");
                        if ("F".Equals(sCN4ISU1Send.flagTipoUsu))
                            mensagem = mensagem.Replace("CPF/CNPJ", "CPF");
                        sCN4ISU1Receive.descricaoRetorno = mensagem;
                    }
                }
            }
            else
            {
                sCN4ISU1Receive.descricaoRetorno = mensagemErroValidar;
            }
            BaseResponse baseResponse = new BaseResponse();
            baseResponse.Model = sCN4ISU1Receive;
            return baseResponse;
        }

        /// <summary>
        /// Histórico de consumo.
        /// </summary>
        public BaseResponse SCN5ISHC(SCN5ISHCSend sCN5ISHCSend)
        {
            SCN5ISHCReceive sCN5ISHCReceive = (SCN5ISHCReceive)_iISSCN5ISHCRepository.Connect(sCN5ISHCSend);

            if (!string.IsNullOrEmpty(sCN5ISHCReceive.descricaoRetorno) && !sCN5ISHCReceive.descricaoRetorno.Equals("0000"))
            {
                sCN5ISHCReceive.descricaoRetorno = _mensagemRepository.parseMensagem(sCN5ISHCReceive.descricaoRetorno);
            }
            else
            {
                if (sCN5ISHCReceive.ocorrencias.Count == 0)
                    sCN5ISHCReceive.descricaoRetorno = "Não existem dados para exibir o histórico.";
            }

            BaseResponse retorno = new BaseResponse();
            retorno.Model = sCN5ISHCReceive;
            return retorno;
        }

        /// <summary>
        /// Lista dados de hidrômetro.
        /// </summary>
        public BaseResponse listaDadosHidrometro(SCN5IS01Send sCN5IS01Send)
        {
            SCN4ISCSSend sCN4ISCSSend = new SCN4ISCSSend();
            sCN4ISCSSend.matricula = sCN5IS01Send.matricula;
            sCN4ISCSSend.codigoServico = "6770100";
            SCN4ISCSReceive sCN5ISHCReceive = (SCN4ISCSReceive)_atendimentoRule.SCN4ISCS(sCN4ISCSSend).Model;
            SCN5IS01Receive sCN5IS01Receive = (SCN5IS01Receive)_informarLeituraRule.SCN5IS01(sCN5IS01Send).Model;

            TrabListaDadosHidrometro trabListaDadosHidrometro = new TrabListaDadosHidrometro();
            trabListaDadosHidrometro.setValues(sCN5ISHCReceive);
            trabListaDadosHidrometro.setValues(sCN5IS01Receive);
            trabListaDadosHidrometro.descricaoRetorno = sCN5IS01Receive.descricaoRetorno;
            trabListaDadosHidrometro.medidores = sCN5IS01Receive.medidores;
            BaseResponse retorno = new BaseResponse();
            retorno.Model = trabListaDadosHidrometro;
            return retorno;

        }

        /// <summary>
        /// Obtém as interrupções em uma determinada região na base GEO via WebService.
        /// </summary>
        /// <param name="matricula">Matrícula que será consultada na base GEO.</param>
        /// <param name="codLocalidade">Código da Localidade que pode haver interrupção.</param>
        /// <param name="codBairro">Código do Bairro que pode haver interrupção.</param>
        /// <returns>Lista com as interrupções consultada na base GEO através do WebService.</returns>
        private List<TrabInterrupcaoCopagis> ObterInterrupcoesCopagis(string matricula, string codLocalidade, string codBairro)
        {
            DateTime tempoInicio = DateTime.Now;
            try
            {
                //Como é apenas uma consulta na base GEO, não é mais necessário separar por ambientes.
                string url = ConfigurationUtil.GetAppSetting("UrlArcGisConsultaInterrupcao") + "matricula={0}&localidade={1}&bairro={2}";
                var hostUri = string.Format(url, matricula, codLocalidade, codBairro);
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
                TimeSpan DuracaoConexao = DateTime.Now.Subtract(tempoInicio);
                double duracaoSegundos = Math.Round(DuracaoConexao.TotalSeconds, 2);
                //_log.AddLog(" CopaGis-interrupcao: " + duracaoSegundos);
                //_log.GravaLogAdc("CopaGis-interrupcao", tempoInicio);
                return JsonConvert.DeserializeObject<List<TrabInterrupcaoCopagis>>(content);
            }
            catch (Exception e)
            {
                gravaLog("Erro ao consultar o arcGis:" + e.Message);
                _log.GravaLogAdc("CopaGis-interrupcao", tempoInicio, e.Message);
                return new List<TrabInterrupcaoCopagis>();
            }
        }

        /// <summary>
        /// Obtém as interrupções em uma determinada região na base GEO via WebService.
        /// </summary>
        /// <param name="matricula">Matrícula que será consultada na base GEO.</param>
        /// <returns>Lista com as interrupções consultada na base GEO através do WebService.</returns>
        public List<TrabInterrupcaoCopagis> ObterInterrupcoesCopagis(string matricula)
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
                _log.GravaLogAdc("CopaGis-interrupcao", tempoInicio);
                return JsonConvert.DeserializeObject<List<TrabInterrupcaoCopagis>>(content);
            }
            catch (Exception e)
            {
                gravaLog("Erro ao consultar o arcGis:" + e.Message);
                _log.GravaLogAdc("CopaGis-interrupcao", tempoInicio, e.Message);
                return new List<TrabInterrupcaoCopagis>();
            }
        }

        /// <summary>
        /// Obtém as interrupções em uma determinada região na base GEO via WebService.
        /// </summary>
        /// <param name="codLocalidade">Código da Localidade que pode haver interrupção.</param>
        /// <param name="codBairro">Código do Bairro que pode haver interrupção.</param>
        /// <returns>Lista com as interrupções consultada na base GEO através do WebService.</returns>
        private List<TrabInterrupcaoCopagis> ObterInterrupcoesCopagis(string codLocalidade, string codBairro)
        {
            try
            {
                //Como é apenas uma consulta na base GEO, não é mais necessário separar por ambientes.
                string url = ConfigurationUtil.GetAppSetting("UrlArcGisConsultaInterrupcao") + "localidade={0}&bairro={1}";
                var hostUri = string.Format(url, codLocalidade, codBairro);
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
                return JsonConvert.DeserializeObject<List<TrabInterrupcaoCopagis>>(content);
            }
            catch (Exception e)
            {
                gravaLog("Erro ao consultar o arcGis:" + e.Message);
                return new List<TrabInterrupcaoCopagis>();
            }
        }

        /// <summary>
        /// Certidão negativa de débito de um CPF ou CNPJ.
        /// </summary>
        public BaseResponse SCN6ISCN(SCN6ISCNSend sCN6ISCNSend)
        {
            SCN6ISCNReceive sCN6ISCNReceive = (SCN6ISCNReceive)_iISSCN6ISCNRepository.Connect(sCN6ISCNSend);
            BaseResponse retorno = new BaseResponse();
            retorno.Model = sCN6ISCNReceive;
            return retorno;
        }

        /// <summary>
        /// Lista Pontos serviço.
        /// </summary>
        public BaseResponse SCN3ISPS(SCN3ISPSSend sCN3ISPSSend)
        {
            SCN3ISPSReceive sCN3ISPSReceive = (SCN3ISPSReceive)_iISSCN3ISPSRepository.Connect(sCN3ISPSSend);
            BaseResponse retorno = new BaseResponse();
            retorno.Model = sCN3ISPSReceive;
            return retorno;
        }

        /// <summary>
        /// Busca matrícula pelo endereço.
        /// </summary>
        public BaseResponse SCN3ISMT(SCN3ISMTSend sCN3ISMTSend)
        {
            string mensagemErroValidar;
            if (sCN3ISMTSend.validar(out mensagemErroValidar))
            {

                SCN3ISMTReceive sCN3ISMTReceive = (SCN3ISMTReceive)_iISSCN3ISMTRepository.Connect(sCN3ISMTSend);
                var identificadorDistinto = sCN3ISMTReceive.matriculas.Select(x => x.identificador).Distinct().ToList();
                if (identificadorDistinto.Count > 0)
                {
                    SCN4ISU1Send retornoIdentificadores = new SCN4ISU1Send { };

                    retornoIdentificadores.identificadores = identificadorDistinto.ToArray();
                    BaseResponse retorno = SCN4ISU1(retornoIdentificadores);
                    SCN4ISU1Receive sCN4ISU1Receive = (SCN4ISU1Receive)retorno.Model;
                    List<SCN4ISU1ReceiveMatriculas> matriculasRetorno = new List<SCN4ISU1ReceiveMatriculas>();
                    foreach (SCN4ISU1ReceiveMatriculas matricula in sCN4ISU1Receive.matriculas)
                    {
                        try
                        {
                            long codigoBairroFormatado = long.Parse(matricula.codigoBairro.Substring(9, 6));
                            long codigoLogradouroFormatado = long.Parse(matricula.codigoLogradouro.Substring(15, 6));

                            if (sCN3ISMTSend.codigoLocalidade == matricula.codigoLocalidade &&
                                long.Parse(sCN3ISMTSend.codigoBairro) == codigoBairroFormatado &&
                                long.Parse(sCN3ISMTSend.codigoLogradouro) == codigoLogradouroFormatado &&
                                long.Parse(sCN3ISMTSend.numeroImovel) == long.Parse(matricula.numeroLogradouro))
                            {
                                matriculasRetorno.Add(matricula);
                            }
                        }
                        catch (Exception) { }
                    }
                    sCN4ISU1Receive.matriculas = matriculasRetorno;
                    return retorno;
                }
                else
                {
                    SCN4ISU1Receive sCN4ISU1Receive = new SCN4ISU1Receive();
                    sCN4ISU1Receive.descricaoRetorno = "Não há cadastro no sistema comercial para o endereço informado.";
                    BaseResponse retorno = new BaseResponse();
                    retorno.Model = sCN4ISU1Receive;
                    return retorno;
                }
            }
            else
            {
                SCN4ISU1Receive sCN4ISU1Receive = new SCN4ISU1Receive();
                sCN4ISU1Receive.descricaoRetorno = mensagemErroValidar;
                BaseResponse retorno = new BaseResponse();
                retorno.Model = sCN4ISU1Receive;
                return retorno;
            }
        }

        /// <summary>
        /// Verifica se há falta d'água em um endereço
        /// </summary>
        public BaseResponse getSituacaoMatriculasEndereco(SCN3ISMTSend sCN3ISMTSend)
        {
            BaseResponse retorno = new BaseResponse();
            TrabPesquisaFaltaAguaReceive trabPesquisaFaltaAguaReceive = new TrabPesquisaFaltaAguaReceive();
            trabPesquisaFaltaAguaReceive.descricaoRetorno = "";
            _log.AddLog("  Chamada ao Copagis: " + DateTime.Now
                + " Localidade: " + sCN3ISMTSend.codigoLocalidade
                + " Bairro: " + sCN3ISMTSend.codigoBairro);
            _interrupcoesCopaGis = ObterInterrupcoesCopagis(sCN3ISMTSend.codigoLocalidade, sCN3ISMTSend.codigoBairro);
            if (_interrupcoesCopaGis.Count > 0)
            {
                var dataPrevisao = _interrupcoesCopaGis.Select(x => x.DtFim).FirstOrDefault().ToString();
                var dataPrevisaoFormatada = dataPrevisao.Substring(0, 10);
                var horaPrevisaoFormtada = dataPrevisao.Substring(11, 8);
                trabPesquisaFaltaAguaReceive.descricaoRetorno = "Estamos efetuando manutenção na rede de abastecimento de água na sua região, " + _interrupcoesCopaGis.Select(x => x.Descricao).FirstOrDefault() + " e a previsão de solução é " + dataPrevisaoFormatada + " até às " + horaPrevisaoFormtada;
                _log.AddLog("  Retorno Copagis: " + DateTime.Now + " " + trabPesquisaFaltaAguaReceive.descricaoRetorno);
            }
            else
            {
                trabPesquisaFaltaAguaReceive.descricaoSituacao = _mensagemRepository.geraMensagem("M0008");
                _log.AddLog("  Retorno Copagis: " + DateTime.Now + " sem interrupção");
            }
            retorno.Model = trabPesquisaFaltaAguaReceive;
            return retorno;
            /*
            SCN4ISU1Receive sCN4ISU1Receive = (SCN4ISU1Receive)SCN3ISMT(sCN3ISMTSend).Model;
            BaseResponse retorno = null;
            TrabPesquisaFaltaAguaReceive trabPesquisaFaltaAguaReceive = null;
            foreach (SCN4ISU1ReceiveMatriculas matricula in sCN4ISU1Receive.matriculas)
            {
                TrabPesquisaFaltaAguaSend situacaoMatriculaSend = new TrabPesquisaFaltaAguaSend();
                situacaoMatriculaSend.matricula = matricula.matricula;
                situacaoMatriculaSend.identificador = matricula.identificador;
                retorno = getSituacaoMatriculas(situacaoMatriculaSend);
                trabPesquisaFaltaAguaReceive = (TrabPesquisaFaltaAguaReceive)retorno.Model;
                if (trabPesquisaFaltaAguaReceive.descricaoSituacao != null && !"".Equals(trabPesquisaFaltaAguaReceive.descricaoSituacao))
                    break;
            }
            if (retorno == null)
            {
                retorno = new BaseResponse();
                trabPesquisaFaltaAguaReceive = new TrabPesquisaFaltaAguaReceive();
                retorno.Model = trabPesquisaFaltaAguaReceive;
            }
            if (trabPesquisaFaltaAguaReceive.descricaoSituacao == null || "".Equals(trabPesquisaFaltaAguaReceive.descricaoSituacao))
            {
                trabPesquisaFaltaAguaReceive.descricaoSituacao = _mensagemRepository.geraMensagem("M0008");
            }
            return retorno;
            */
        }

        /// <summary>
        /// Altera vencimento fatura.
        /// </summary>
        public BaseResponse SCN6ISAV(SCN6ISAVSend sCN6ISAVSend)
        {
            SCN6ISAVReceive sCN6ISAVReceive = (SCN6ISAVReceive)_iSSCN6ISAVRepository.Connect(sCN6ISAVSend);
            if (!"".Equals(sCN6ISAVReceive.descricaoRetorno))
                sCN6ISAVReceive.descricaoRetorno = _mensagemRepository.geraMensagem(sCN6ISAVReceive.descricaoRetorno);
            BaseResponse retorno = new BaseResponse();
            retorno.Model = sCN6ISAVReceive;
            return retorno;
        }

        /// <summary>
        /// Quitação anual de débito.
        /// </summary>
        public BaseResponse SCN6ISQA(SCN6ISQASend sCN6ISQASend)
        {
            SCN6ISQAReceive sCN6ISQAReceive = (SCN6ISQAReceive)_iSSCN6ISQARepository.Connect(sCN6ISQASend);
            if (!"".Equals(sCN6ISQAReceive.descricaoRetorno))
            {
                sCN6ISQAReceive.descricaoRetorno = _mensagemRepository.parseMensagem(sCN6ISQAReceive.descricaoRetorno);
            }
            else
            {
                string endereco = sCN6ISQAReceive.tipoLogradouro + " " + Util.Util.formataTamanhoLetrasNomes(sCN6ISQAReceive.nomeLogradouro) + ", " + sCN6ISQAReceive.numeroImovel +
                    sCN6ISQAReceive.tipoComplementoImovel.Trim() + " " + sCN6ISQAReceive.complementoImovel;
                if ("N".Equals(sCN6ISQAReceive.possuiFaturaEmDebito))
                {
                    sCN6ISQAReceive.deferimento = "";
                    sCN6ISQAReceive.textoCorpo = "Em cumprimento à Lei 12.007, 29 de julho de 2009, a COPASA declara quitados os débitos do " +
                        "ano de "+ sCN6ISQASend.anoPesquisa + ", relativo ao imóvel situado à "+endereco+", "+ sCN6ISQASend .matricula+ ", isentando demais " +
                        "comprovações para o período citado.";
                    sCN6ISQAReceive.textoConsideracoes = "ESTA DECLARAÇÃO NÃO CONTEMPLA EVENTUAIS DÉBITOS POSTERIORMENTE APURADOS EM " +
                        "FUNÇÃO DE IDENTIFICAÇÃO DE IRREGULARIDADE(S) OU REVISÃO DE FATURAMENTO, QUE " +
                        "ABRANJAM O PERÍODO EM QUESTÃO.";
                    sCN6ISQAReceive.textoDebito = "";
                    
                }
                else
                {
                    sCN6ISQAReceive.deferimento = "PEDIDO INDEFERIDO";
                    sCN6ISQAReceive.textoCorpo = "Informamos que a solicitação de 'Declaração de Quitação Anual de Débito', foi indefirida em virtude de existir, até a presente dada, débito(s) vencidos(s) em aberto referente ao " +
                        "exercício de "+ sCN6ISQASend.anoPesquisa + ", para o imóvel situado a "+ endereco + ", sob a matrícula:"+ sCN6ISQASend.matricula + ".";
                    sCN6ISQAReceive.textoConsideracoes = "";
                    sCN6ISQAReceive.textoDebito = "DÉBITO(S) EM ABERTO";
                }

                string localidade = "Belo Horizonte";
                if (sCN6ISQAReceive.nomeLocalidade != null && !"".Equals(sCN6ISQAReceive.nomeLocalidade))
                    localidade = Util.Util.formataTamanhoLetrasNomes(sCN6ISQAReceive.nomeLocalidade);
                sCN6ISQAReceive.local = string.Format(localidade+", {0:dd} de {0:MMMM} de {0:yyyy}", DateTime.Now);
            }
            BaseResponse retorno = new BaseResponse();
            retorno.Model = sCN6ISQAReceive;
            return retorno;
        }

        /// <summary>
        /// Consiste matrícula centralizadora.
        /// </summary>
        public BaseResponse SCN6ISCC(SCN6ISCCSend sCN6ISCCSend)
        {
            SCN6ISCCReceive sCN6ISCCReceive = (SCN6ISCCReceive)_iSSCN6ISCCRepository.Connect(sCN6ISCCSend);
            if (!"".Equals(sCN6ISCCReceive.descricaoRetorno))
                sCN6ISCCReceive.descricaoRetorno = _mensagemRepository.parseMensagem(sCN6ISCCReceive.descricaoRetorno);

            BaseResponse retorno = new BaseResponse();
            retorno.Model = sCN6ISCCReceive;
            return retorno;
        }

        /// <summary>
        /// .
        /// </summary>
        public BaseResponse getSituacaoMatriculas(TrabPesquisaFaltaAguaSend situacaoMatriculaSend)
        {
            SCN4ISU1Send sCN4ISU1Send = new SCN4ISU1Send();
            string[] identificadores = new string[1];
            identificadores[0] = situacaoMatriculaSend.identificador;
            sCN4ISU1Send.identificadores = identificadores;
            BaseResponse brSCN4ISU1 = SCN4ISU1(sCN4ISU1Send);
            SCN4ISU1Receive sCN4ISU1Receive = (SCN4ISU1Receive)brSCN4ISU1.Model;
            TrabPesquisaFaltaAguaReceive situacaoMatriculaReceive = new TrabPesquisaFaltaAguaReceive();
            situacaoMatriculaReceive.descricaoRetorno = "";
            bool existeMatricula = false;
            if (brSCN4ISU1.IsValid)
            {
                foreach (SCN4ISU1ReceiveMatriculas matricula in sCN4ISU1Receive.matriculas)
                {
                    if (matricula.matricula.Equals(situacaoMatriculaSend.matricula))
                    {
                        //TrabSituacaoMatriculaReceiveMatriculas matriculaRetorno = new TrabSituacaoMatriculaReceiveMatriculas();
                        //matriculaRetorno.matricula = matricula.matricula;
                        existeMatricula = true;
                        if ("RA".Equals(matricula.situacaoAgua))
                        {
                            if ("S".Equals(matricula.temInterrupcaoAgua))
                            {
                                situacaoMatriculaReceive.descricaoSituacao = _mensagemRepository.geraMensagem("M0007");
                                /*
                                if (_interrupcoesCopaGis.Count > 0)
                                {
                                foreach (TrabInterrupcaoCopagis interrupcaoCopagis in _interrupcoesCopaGis)
                                {
                                    if (interrupcaoCopagis.Objetivo.Equals(_servicoOperacionalRule.CodigoOSFaldaDAgua))
                                    {
                                    }
                                }
                            }
                            else
                                {
                                    situacaoMatriculaReceive.descricaoSituacao = _interrupcoesSicom.descricaoMotivo
                                            + ". Previsão normalização: " + _interrupcoesSicom.dataPrevisao;
                                }
                                */
                            }
                            else
                            {
                                // Verificar se tem SS
                                SCN4ISSSSend sCN4ISSSSend = new SCN4ISSSSend();
                                sCN4ISSSSend.matriculaImovel = matricula.matricula;
                                SCN4ISSSReceive sCN4ISSSReceive = (SCN4ISSSReceive)_servicoOperacionalRule.SCN4ISSS(sCN4ISSSSend).Model;
                                foreach (SCN4ISSSReceiveSolicitacaoServico ss in sCN4ISSSReceive.solicitacoesServico)
                                {
                                    if ("110".Equals(ss.codigoServicoInsumo))
                                    {
                                        situacaoMatriculaReceive.descricaoSituacao = _mensagemRepository.geraMensagem("M0003");
                                    }
                                    else if ("111".Equals(ss.codigoServicoInsumo))
                                    {
                                        situacaoMatriculaReceive.descricaoSituacao = _mensagemRepository.geraMensagem("M0004");
                                    }
                                    else if ("109".Equals(ss.codigoServicoInsumo))
                                    {
                                        situacaoMatriculaReceive.descricaoSituacao = _mensagemRepository.geraMensagem("M0005");
                                    }
                                }
                            }
                        }
                        else
                        {
                            SCN3ISPSSend sCN3ISPSSend = new SCN3ISPSSend();
                            sCN3ISPSSend.matricula = matricula.matricula;
                            SCN3ISPS(sCN3ISPSSend);
                            BaseResponse brSCN3ISPS = SCN3ISPS(sCN3ISPSSend);
                            SCN3ISPSReceive sCN3ISPSReceive = (SCN3ISPSReceive)brSCN3ISPS.Model;
                            foreach (SCN3ISPSReceivePSAgua psAgua in sCN3ISPSReceive.pontosServicoAgua)
                            {
                                if (matricula.situacaoAgua.Equals(psAgua.descricaoSituacao))
                                {
                                    situacaoMatriculaReceive.descricaoSituacao = psAgua.descricaoMotivo;
                                    situacaoMatriculaReceive.descricaoSituacao = _mensagemRepository.geraMensagem("M0006");
                                }
                            }
                        }
                        //situacaoMatriculaReceive.matriculas.Add(matriculaRetorno);
                    }
                }
            }
            if (!existeMatricula)
                situacaoMatriculaReceive.descricaoRetorno = "Não há matrículas para este identificador.";
            BaseResponse retorno = new BaseResponse();
            retorno.Model = situacaoMatriculaReceive;
            return retorno;

        }

        private string retiraCaracteresMascara(string cpfCnpj)
        {
            char[] chars = cpfCnpj.ToCharArray();
            List<char> retorno = new List<char>();
            foreach (char cr in chars)
            {
                long r;
                if (long.TryParse(cr.ToString(), out r))
                    retorno.Add(cr);
            }
            return new string(retorno.ToArray());
        }

        private InterrupcaoAgua VerificarInterrupcaoSicomGis(string matricula, string codigoLocalidade, string codigoBairro)
        {
            var listaInterrupcoes = new InterrupcaoAgua();
            SCN4ISFASend sCN4ISFASend = new SCN4ISFASend();
            sCN4ISFASend.matricula = matricula;
            _interrupcoesSicom = (SCN4ISFAReceive)_iISSCN4ISFARepository.Connect(sCN4ISFASend);
            if (_interrupcoesSicom.temInterrupcao == "S")
            {
                listaInterrupcoes.temInterrupcaoAgua = "S";
                listaInterrupcoes.mensagemRetorno = "Estamos efetuando manutenção na rede de abastecimento de água na sua região, " + _interrupcoesSicom.descricaoMotivo + " e a previsão de solução é " + _interrupcoesSicom.dataPrevisao + " até às " + _interrupcoesSicom.horaPrevisao;

                return listaInterrupcoes;
            }
            else
            {
                var codigoBairroFormatado = codigoBairro.Substring(9, 6);
                _interrupcoesCopaGis = ObterInterrupcoesCopagis(matricula, codigoLocalidade, codigoBairroFormatado);
                if (_interrupcoesCopaGis.Count > 0)
                {
                    var dataPrevisao = _interrupcoesCopaGis.Select(x => x.DtFim).FirstOrDefault().ToString();
                    var dataPrevisaoFormatada = dataPrevisao.Substring(0, 10);
                    var horaPrevisaoFormtada = dataPrevisao.Substring(11, 8);
                    listaInterrupcoes.temInterrupcaoAgua = "S";
                    listaInterrupcoes.mensagemRetorno = "Estamos efetuando manutenção na rede de abastecimento de água na sua região, " + _interrupcoesCopaGis.Select(x => x.Descricao).FirstOrDefault() + " e a previsão de solução é " + dataPrevisaoFormatada + " até às " + horaPrevisaoFormtada;

                    return listaInterrupcoes;
                }
            }

            return listaInterrupcoes;
        }

        /// <summary>
        /// Valida Usuário
        /// </summary>
        public BaseResponse validaUsuario(TrabValidaUsuarioSend trabValidaUsuarioSend)
        {
            var trabValidaUsuarioReceive = new TrabValidaUsuarioReceive();
            string cpfCnpjTratado = "";
            if (!"".Equals(trabValidaUsuarioSend.identificador) && !"".Equals(trabValidaUsuarioSend.CpfCnpj))
            {
                cpfCnpjTratado = retiraCaracteresMascara(trabValidaUsuarioSend.CpfCnpj);

                SCN4ISU1Send sCN4ISU1Send = new SCN4ISU1Send();
                sCN4ISU1Send.cpfCnpj = cpfCnpjTratado;
                SCN4ISU1Receive sCN4ISU1Receive = (SCN4ISU1Receive)_iSSCN4ISU1Repository.Connect(sCN4ISU1Send);
                if (sCN4ISU1Receive.usuariosIS.Length == 0)
                {
                    trabValidaUsuarioReceive.codigoRetorno = "75";
                }
                else
                {
                    string identificadorTratado = retiraCaracteresMascara(trabValidaUsuarioSend.identificador);
                    long identificadorLong;
                    if (long.TryParse(identificadorTratado, out identificadorLong))
                    {
                        trabValidaUsuarioSend.identificador = identificadorLong.ToString();
                        SCN4ISU1ReceiveUsuarios identificador = sCN4ISU1Receive.usuariosIS.Where(x => x.identificador == trabValidaUsuarioSend.identificador).FirstOrDefault();
                        if (identificador != null)
                        {
                            trabValidaUsuarioReceive.codigoRetorno = "0";
                            trabValidaUsuarioReceive.CpfCnpj = cpfCnpjTratado;
                            trabValidaUsuarioReceive.identificador = trabValidaUsuarioSend.identificador;
                            trabValidaUsuarioReceive.descricaoRetorno = "";
                        }
                        else
                        {
                            trabValidaUsuarioReceive.codigoRetorno = "74";
                        }
                    }
                }
            }
            else
            {
                trabValidaUsuarioReceive.codigoRetorno = "74";
            }
            if (!String.IsNullOrEmpty(trabValidaUsuarioReceive.codigoRetorno) && trabValidaUsuarioReceive.codigoRetorno != "0")
            {
                if (trabValidaUsuarioReceive.codigoRetorno == "75")
                {
                    if (cpfCnpjTratado.Length == 14)
                        trabValidaUsuarioReceive.descricaoRetorno = "CNPJ não encontrado.";
                    else
                    {
                        trabValidaUsuarioReceive.descricaoRetorno = "CPF não encontrado.";
                    }
                }
                else
                {
                    trabValidaUsuarioReceive.descricaoRetorno = _mensagemRepository.geraMensagem(string.Format("M00{0}", trabValidaUsuarioReceive.codigoRetorno));
                }
            }

            var response = new BaseResponse();
            response.Model = trabValidaUsuarioReceive;

            return response;
        }

        /// <summary>
        /// Cria um Contato Novo no Microsoft Dynamics 365.
        /// </summary>
        public BaseResponse CadastraUsuario(Dyn365CreatePortalUserSend createDyn365PortalUserSend)
        {
            var baseResponse = new BaseResponse();

            if (!createDyn365PortalUserSend.EmailAddress1.IsNullOrEmpty()
                && (createDyn365PortalUserSend.EmailAddress1.Contains("@copasa.com.br")
                || createDyn365PortalUserSend.EmailAddress1.Contains("@parceiro.copasa.com.br")
                || createDyn365PortalUserSend.EmailAddress1.Contains("@copanor.com.br")
                || createDyn365PortalUserSend.EmailAddress1.Contains("@parceiro.copanor.com.br")))
            {
                BaseModelAzureCopaUserReceive baseModelAzureCopaUserReceive = new BaseModelAzureCopaUserReceive()
                {
                    Id = null,
                    Records = null,
                    Status = null,
                    Message = "Não é permitido informar e-mail corporativo. Favor efetuar o cadastro com seu e-mail particular.",
                    Protocol = null
                };
                baseResponse.Model = baseModelAzureCopaUserReceive;
            }
            else
                baseResponse.Model = ConnectDyn365("CreateDyn365PortalUser", createDyn365PortalUserSend);
                
            return baseResponse;
        }

        /// <summary>
        /// Autentica um Contato no Microsoft Dynamics 365. - 
        /// </summary>
        public BaseResponse AutenticaUsuario(Dyn365AuthenticateUserSend authenticateDyn365UserSend)
        {
            return new BaseResponse
            {
                Model = ConnectDyn365("AuthenticateDyn365User", authenticateDyn365UserSend)
            };
        }

        /// <summary>
        /// Altera a Senha de um Contato no Microsoft Dynamics 365  enviando Id ao Dynamics.
        /// </summary>
        public BaseResponse AltualizaSenha(Dyn365ChangeUserPasswordSend changeDyn365UserPasswordSend)
        {            
           return new BaseResponse
           {
              Model = ConnectDyn365("ChangeDyn365UserPassword", changeDyn365UserPasswordSend)
           };
        }

        /// <summary>
        /// Altera a Senha via APP por passando CPF. Recupera Id e Executa metodo modifica senha
        /// </summary>
        public BaseResponse AlteraSenha(Dyn365ChangeUserPasswordCpfSend changeDyn365UserPasswordCpfSend)
        {
            BaseResponse retorno = new BaseResponse();

            // autentica com senha atual para na alteração e retorna o Id do Usuário
            Dyn365AuthenticateUserSend dyn365AuthenticateUserSend = new Dyn365AuthenticateUserSend();
            dyn365AuthenticateUserSend.ADXPassword = changeDyn365UserPasswordCpfSend.CurrentPassword;
            dyn365AuthenticateUserSend.ADXUsername = changeDyn365UserPasswordCpfSend.CpfCnpj;
            dyn365AuthenticateUserSend.Password = changeDyn365UserPasswordCpfSend.Password;
            dyn365AuthenticateUserSend.Username = changeDyn365UserPasswordCpfSend.Username;

            retorno = DAutenticaUsuario(dyn365AuthenticateUserSend);

            if (retorno.Model.getValor("Message").IsNullOrEmpty())
            {
                // cria os parametros para a classe Atualiza 
                Dyn365ChangeUserPasswordSend changeDyn365UserPasswordSend = new Dyn365ChangeUserPasswordSend();
                changeDyn365UserPasswordSend.ContactId  = retorno.Model.getValor("Id").ToString();
                changeDyn365UserPasswordSend.NewPassword = changeDyn365UserPasswordCpfSend.NewPassword;
                changeDyn365UserPasswordSend.Password = changeDyn365UserPasswordCpfSend.Password;
                changeDyn365UserPasswordSend.Username = changeDyn365UserPasswordCpfSend.Username;

                retorno = AltualizaSenha(changeDyn365UserPasswordSend);
            }

            return retorno;
        }

        /// <summary>
        /// Gera uma Nova Senha para um Contato no Microsoft Dynamics 365.
        /// </summary>
        public BaseResponse RecuperaSenha(Dyn365RecoveryUserPasswordSend recoveryDyn365UserPasswordSend)
        {
            var baseResponse = new BaseResponse();

            baseResponse.Model = ConnectDyn365("RecoveryDyn365UserPassword", recoveryDyn365UserPasswordSend);

            if (!baseResponse.Model.getValor("Message").IsNullOrEmpty() && recoveryDyn365UserPasswordSend.Origem.ToUpper().Equals("APP")
                && baseResponse.Model.getValor("Message").Contains("CPF/CNPJ"))
            {
                baseResponse.Model.setValue("Message", baseResponse.Model.getValor("Message").Replace("CPF/CNPJ", "CPF"));
            }

            return baseResponse;
        }

        /// <summary>
        /// Atualiza um Contato no Microsoft Dynamics 365.
        /// </summary>
        public BaseResponse AtualizaUsuario(Dyn365UpdatePortalUserSend updateDyn365PortalUserSend)
        {
            //caso de imagem vazia, atualiza com uma imagem padrão
            if (string.IsNullOrEmpty(updateDyn365PortalUserSend.EntityImage))
            {
                updateDyn365PortalUserSend.EntityImage = "iVBORw0KGgoAAAANSUhEUgAAAD4AAAA+CAYAAABzwahEAAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAOnAAADpwBB5RT3QAAABx0RVh0U29mdHdhcmUAQWRvYmUgRmlyZXdvcmtzIENTNui8sowAAARcSURBVGiB7ZpbSyptGIbv0dVyGjejloM7wkgSC1REpIOgOvc39ieKOqkIg7AOMtpgolBDtKHM0jBHRec7iO/jq1zOqM+4KrtO553b5+LVdyuzvLwsYwjR/e0C/hY/4sPGj/iw8SM+bPwa1AdxHAev1wtBEGC321Gv11Eul1GpVJDP51GpVAZVCoABiNtsNoTDYUxOTn545nK5AADRaBR3d3fY39/H/f291iUB0Fh8dnYWsVgMOp3yL0oQBCQSCWSzWaRSKbRaLS1L0058bm4OwWCw6/cCgQB4nsfm5ibq9boGlb2iyeAWjUZ7kv4Xp9OJxcVFMAxDWNVbyMUFQUAoFOo7x+PxIBKJEFTUHlJxhmEwPz9P1lOhUAgWi4Uk6z2k4j6fDzzPk+XpdDrEYjGyvDfZlGEzMzOUcQCAiYkJmM1m8lwycaPRCEEQqOL+g2GYtmuAfiETdzgcVFEDySYTt9vtVFEfsNls5Jlk4hzHUUV9wGw2Q6/Xk2aSiVMX9h6DwUCaRybebDapotpCvYojE395eaGKakuj0SDNIxN/fHykivpArVYj37CQiReLRaqogWSTiZdKJTw/P1PFveH6+po8k3TJms/nKeMAALIsI5fLkeeSimcyGfJB6OLiAtVqlTQTIBav1Wo4Ojoiy2u1Wkin02R5/4f8IOL4+Bg3NzckWQcHByiVSiRZ7yEXl2UZ29vbKJfLfeWcn5/j5OSEqKqPaHLmJkkS1tbWej4qFkURyWQSsqzd1b1mNynVahXr6+vIZDKqBZrNJvb29rC1tfV1j5eBV5FUKoWzszOEQiF4vd62mw1JkpDL5XB6eqrJCN6OvsUtFgv8fj/cbjd4nkepVEI2m30z9z49PSGZTIJhGNjtdphMJrAsi3q9jkKh0HHh4/P5MD09DUEQUC6XUSgUkM1m8fDw0FfdTK//gXG5XIhEInA6nW2fi6KI3d1dSJLUU2E6nQ7xePyP5/PFYhGHh4cQRbGn/K7FWZZFPB7H1NSUYttqtYqdnR1cXV11VZTNZsPCwoKqk5disYhkMtn1JqkrcY7jkEgkYDQau/qQXC6HdDqteCPKsizC4TCCwWBX++9Go4GNjQ3c3t6qfke1uMFgQCKR6PmAv9ls4vLyEqIoolQqoVqtQpZlcBwHh8MBl8sFj8eDkZGRnvIlScLq6qrqjZLqwS0ajfZ1q6HX6+Hz+eDz+XrO6ATLslhaWsLKyoqq6VPVPG61WhEIBPouTmvGxsbg9/tVtVUlHggENL25pERtBymKMwyjagT/LDgcDlWDr6I4z/PkR7ta43a7Fdsoimtxi6E14+Pjim2+pbjValVsoyhuMplIihkkaqbdbynOcZzilda3FAegOLJ3FGcYRtNbUC1Rmok6iptMpi+zcHnP79+/Oz7vKP5Vext4Xbt3oqN4t9vPz0RfX/XR0VHSYgbJT4//gW/b40MrPrSjel/iQ9njer1e8eXPTM/iX3lEB5QHt38Aq5lSwzdflWcAAAAASUVORK5CYII=";
            }

            var baseResponse = new BaseResponse();

            if (!updateDyn365PortalUserSend.EmailAddress1.IsNullOrEmpty()
                && (updateDyn365PortalUserSend.EmailAddress1.Contains("@copasa.com.br")
                || updateDyn365PortalUserSend.EmailAddress1.Contains("@parceiro.copasa.com.br")
                || updateDyn365PortalUserSend.EmailAddress1.Contains("@copanor.com.br")
                || updateDyn365PortalUserSend.EmailAddress1.Contains("@parceiro.copanor.com.br")))
            {
                BaseModelAzureCopaUserReceive baseModelAzureCopaUserReceive = new BaseModelAzureCopaUserReceive()
                {
                    Id = null,
                    Records = null,
                    Status = null,
                    Message = "Não é permitido informar e-mail corporativo. Favor efetuar o cadastro com seu e-mail particular.",
                    Protocol = null
                };
                baseResponse.Model = baseModelAzureCopaUserReceive;
            }
            else
                baseResponse.Model = ConnectDyn365("UpdateDyn365PortalUser", updateDyn365PortalUserSend);

            return baseResponse;
        }

        /// <summary>
        /// Associa um Identificador com um Contato no Microsoft Dynamics 365.
        /// </summary>
        public BaseResponse AssociaIdentificador(Dyn365AssociateIdentifierXUserSend associateDyn365IdentifierXUserSend)
        {
            return new BaseResponse
            {
                Model = ConnectDyn365("AssociateDyn365IdentifierXUser", associateDyn365IdentifierXUserSend)
            };
        }

        /// <summary>
        /// Altera o Status do [Identificador do Contato] no Microsoft Dynamics 365.
        /// </summary>
        public BaseResponse AtualizaStatusIdentificador(Dyn365ChangeControllerIdentifierStatusSend changeDyn365ControllerIdentifierStatusSend)
        {
            return new BaseResponse
            {
                Model = ConnectDyn365("ChangeDyn365ControllerIdentifierStatus", changeDyn365ControllerIdentifierStatusSend)
            };
        }

        /// <summary>
        /// Faz chamadas via post para um serviço da tabela CopasaUser no Dynalmics 365
        /// </summary>
        private BaseModelAzureCopaUserReceive ConnectDyn365(string nomeServico, BaseModel baseModelSend)
        {
            Dyn365User dyn365User = new Dyn365User();
            dyn365User.setValues(baseModelSend);
            Dyn365AutenticaoUsuarioRepository dyn365AutenticaUsuarioRepository = new Dyn365AutenticaoUsuarioRepository();
            BaseModelAzureCopaUserReceive baseModelAzureCopaUserReceive = (BaseModelAzureCopaUserReceive)dyn365AutenticaUsuarioRepository.ExecutarServico(nomeServico, dyn365User, typeof(BaseModelAzureCopaUserReceive));
            return baseModelAzureCopaUserReceive;
        }

        /// <summary>
        /// Busca Mensagem Informativa
        /// </summary>
        /// <returns></returns>
        public BaseResponse ObterInformativo(TrabParametroSend entrada)
        {
            var baseResponse = new BaseResponse();
            TrabParametroReceive trabParametroReceive = new TrabParametroReceive();

            try
            {
                trabParametroReceive.descricaoRetorno = "Mensagem Indisponível.";

                if (entrada.Origem.ToUpper().Equals("APP"))
                {
                    trabParametroReceive.descricaoRetorno = "";
                    using (var unitOfWork = RepositoryFactory.UnitOfWork)
                    {
                        var parametros = unitOfWork.ParametroRepository.GetAll();

                        if (parametros.Count > 0)
                        {
                            ParametroModel parametroModel = parametros.Where(x => x.DataInicio <= DateTime.Now && x.DataFim >= DateTime.Now).FirstOrDefault<ParametroModel>();

                            if (parametroModel != null)
                            {
                                trabParametroReceive = new TrabParametroReceive()
                                {
                                    descricaoRetorno = "",
                                    DataInicio = parametroModel.DataInicio.ToString("dd/MM/yyyy"),
                                    DataFim = parametroModel.DataFim.ToString("dd/MM/yyyy"),
                                    DescricaoMensagem = parametroModel.DescricaoMensagem,
                                    FlagPermissao = parametroModel.FlagPermissao.ToString(),
                                    Versao = parametroModel.Versao,
                                    SistemaOperacional = parametroModel.SistemaOperacional
                                };
                            }
                        }
                    };
                }
                baseResponse.Model = trabParametroReceive;

                return baseResponse;
            }
            catch (Exception)
            {
                trabParametroReceive.descricaoRetorno = "";
                baseResponse.Model = trabParametroReceive;
                return baseResponse;
            }
        }


        ///// <summary>
        ///// Autentica um Contato no Microsoft Dynamics 365.
        ///// </summary>
        //public BaseResponse DAutenticaUsuario(Dyn365AuthenticateUserSend authenticateDyn365UserSend)
        //{
        //    BaseResponse retorno = new BaseResponse();
        //    Dyn365User dyn365User = new Dyn365User();
        //    dyn365User.setValues(authenticateDyn365UserSend);
        //    Dyn365AutenticaoUsuarioRepository dyn365AutenticaUsuarioRepository = new Dyn365AutenticaoUsuarioRepository();

        //    //Autentiar com cpf e senha informados pelo usuário no Dynamics
        //    BaseModelAzureCopaUserReceive azure = (BaseModelAzureCopaUserReceive)dyn365AutenticaUsuarioRepository.ExecutarServico("AuthenticateDyn365User", dyn365User, typeof(BaseModelAzureCopaUserReceive));


        //    BaseModelAzureCopaUserReceive migrado = new BaseModelAzureCopaUserReceive();
        //    DValidaCpfCnpjReceive cpfCadastrado = new DValidaCpfCnpjReceive();
        //    bool retornoaut = false;

        //    if (string.IsNullOrEmpty(azure.Id))
        //    {
        //        //Caso não consiga logar tentar com a senha padrão
        //        //para verificar se foi migrado automáticamente
        //        string senhaoriginal = authenticateDyn365UserSend.ADXPassword;
        //        authenticateDyn365UserSend.ADXPassword = "IwlHyJcR6RrtG/O5fdR49XMwgZauBu2I2B5tsoZr/5o=";
        //        Dyn365User dyn365User2 = new Dyn365User();

        //        dyn365User2.setValues(authenticateDyn365UserSend);
        //        dyn365AutenticaUsuarioRepository = new Dyn365AutenticaoUsuarioRepository();
        //        migrado = (BaseModelAzureCopaUserReceive)dyn365AutenticaUsuarioRepository.ExecutarServico("AuthenticateDyn365User", dyn365User2, typeof(BaseModelAzureCopaUserReceive));

        //        retornoaut = AutenticarDigital(authenticateDyn365UserSend.ADXUsername, Criptografia.Decrypt(senhaoriginal));

        //        //Se usuário foi migrado //se ele já havia sido migrado, agora ele está com a senha padrão
        //        if (!string.IsNullOrEmpty(migrado.Id))
        //        {
        //            //e senha foi autenticada no banco do Copasa Digital
        //            if (retornoaut)
        //            {
        //                Dyn365ChangeUserPasswordSend changeDyn365UserPasswordSend = new Dyn365ChangeUserPasswordSend()
        //                {
        //                    CpfCnpj = authenticateDyn365UserSend.ADXUsername,
        //                    CurrentPassword = authenticateDyn365UserSend.ADXPassword,
        //                    NewPassword = senhaoriginal,
        //                    Username = "servico.app@copasa.com.br",
        //                    Password = "CYiEjHlatF9W33G56MjcmrfEh1nRXazWw+sYuVM/QGc="
        //                };
        //                //Atualizar o banco novo com a senha
        //                AltualizaSenha(changeDyn365UserPasswordSend);

        //                azure = migrado;
        //                // e repassa as informações obtidas na consulta valida feita com a senha padrão.
        //                //retorno = new BaseResponse { Model = baseModelAzureCopaUserReceive, IsValid = true };

        //                azure.Status = "101";  //---- 
        //            }
        //        }
        //        else
        //        {
        //            //Importar todos os dados do cpf.
        //            if (retornoaut)
        //            {
        //                using (var unitOfWork = RepositoryFactory.UnitOfWork)
        //                {
        //                    var cpfCnpj = Convert.ToInt64(authenticateDyn365UserSend.ADXUsername);
        //                    string validado = "N";

        //                    ClienteModel cliente = unitOfWork.ClienteRepository.Get(x => x.CpfCnpj.Equals(cpfCnpj) & x.Validado == validado & x.TipoCliente == "F"); // sugerencia retirar validado

        //                    if (Migracliente(cliente, senhaoriginal))
        //                    {
        //                        cliente.Validado = "S";
        //                        unitOfWork.ClienteRepository.Update(cliente);
        //                        unitOfWork.DCommit();
        //                    }

        //                    dyn365AutenticaUsuarioRepository = new Dyn365AutenticaoUsuarioRepository();
        //                    //tenta fazer o login novamente.
        //                    migrado = (BaseModelAzureCopaUserReceive)dyn365AutenticaUsuarioRepository.ExecutarServico("AuthenticateDyn365User", dyn365User, typeof(BaseModelAzureCopaUserReceive));

        //                    if (!string.IsNullOrEmpty(migrado.Id))
        //                    {
        //                        azure = migrado;

        //                    }
        //                    else
        //                    {
        //                        string cpf = cpfCnpj.ToString().PadLeft(11, '0');

        //                        DValidaCpfCnpjSend dyn365ValidaCpfCnpjSend = new DValidaCpfCnpjSend()
        //                        {
        //                            CpfCnpj = cpf
        //                        };
        //                        cpfCadastrado = (DValidaCpfCnpjReceive)ValidaCpfCnpjDyn365(dyn365ValidaCpfCnpjSend).Model;
        //                    }

        //                    azure.Status = "101";  //---- 

        //                }
        //            }
        //        }

        //    }

        //    if ((migrado.Message == "Usuário/Senha incorreta." && !retornoaut)
        //        || (!string.IsNullOrEmpty(migrado.Id) && !retornoaut)
        //        || (migrado.Message == "Usuário/Senha incorreta."
        //             && retornoaut && !string.IsNullOrEmpty(cpfCadastrado.CpfCnpjId))
        //        || (migrado.Message == "Usuário/Senha incorreta."
        //             && !string.IsNullOrEmpty(cpfCadastrado.CpfCnpjId)))
        //    {
        //        azure.Message = "CPF e/ou senha incorretos.";
        //    }
        //    else if (migrado.Message == "Usuário/Senha incorreta."
        //             && string.IsNullOrEmpty(cpfCadastrado.CpfCnpjId))
        //    {
        //        azure = new BaseModelAzureCopaUserReceive();
        //        azure.Message = "Não foi possível realizar a migração dos dados. E-mail já está sendo utilizado.";

        //    }

        //    retorno = new BaseResponse { Model = azure, IsValid = true };
        //    return retorno;

        //}

        /// <summary>
        /// Autentica um Contato no Microsoft Dynamics 365.
        /// </summary>
        public BaseResponse DAutenticaUsuario(Dyn365AuthenticateUserSend authenticateDyn365UserSend)
        {
            BaseResponse retorno = new BaseResponse();
            Dyn365User dyn365User = new Dyn365User();
            dyn365User.setValues(authenticateDyn365UserSend);
            Dyn365AutenticaoUsuarioRepository dyn365AutenticaUsuarioRepository = new Dyn365AutenticaoUsuarioRepository();

            //Autenticar com cpf e senha informados pelo usuário no Dynamics
            BaseModelAzureCopaUserReceive azure = (BaseModelAzureCopaUserReceive)dyn365AutenticaUsuarioRepository.ExecutarServico("AuthenticateDyn365User", dyn365User, typeof(BaseModelAzureCopaUserReceive));
            //    string autenticadoAntigo = "";
            BaseModelAzureCopaUserReceive autenticadoAntigo = new BaseModelAzureCopaUserReceive();

            string condicao = $"$filter=copasa_cpf_cnpj eq '{Util.Util.InsereMascaraCpfCnpj(authenticateDyn365UserSend.ADXUsername)}'";

            if (string.IsNullOrEmpty(azure.Id))
            {
                DRepository dRepository = new DRepository("contacts", "Dyn365Host");
                //
                BaseResponse response = new BaseResponse() { Model = ((DConsultaUsuarioReceive)dRepository.DPesquisarObjeto(condicao, typeof(DConsultaUsuarioReceive))) };
               
                if (response.Model == null)
                {
                    //Autenticar no sistema antigo, precisa descriptografar a senha pois a recebida do Dynamics utiliza outro tipo de crypto
                    autenticadoAntigo = AutenticarDigital(authenticateDyn365UserSend.ADXUsername, Criptografia.Decrypt(authenticateDyn365UserSend.ADXPassword), authenticateDyn365UserSend.ADXPassword);
                    azure.Message = autenticadoAntigo.Message;
                    azure.Status = autenticadoAntigo.Status;
                    azure.Id = autenticadoAntigo.Id;
                }
                else
                {
                    // tratamento feito para cliente especial - TASK 832
                    // Usuários com conta especial, logar-se no APP.
                    // Código 103 - retorna quando senha e email forem nulos na base de dados
                    // Código 104 - retorna quando existe email e senha nula
                    DValidaCpfCnpjReceive cpfCadastrado = new DValidaCpfCnpjReceive();
                    string cpf = authenticateDyn365UserSend.ADXUsername.ToString().PadLeft(11, '0');
                    DValidaCpfCnpjSend dyn365ValidaCpfCnpjSend = new DValidaCpfCnpjSend()
                    {
                      CpfCnpj = cpf
                    };
                    cpfCadastrado = (DValidaCpfCnpjReceive)ValidaCpfCnpjDyn365(dyn365ValidaCpfCnpjSend).Model;

                    DConsultaUsuarioReceive dados = (DConsultaUsuarioReceive)response.Model;
                    if (dados.emailaddress1.IsNullOrEmpty() && authenticateDyn365UserSend.ADXPassword.IsNullOrEmpty())
                    {
                        azure.Status = "103";
                        azure.Message = "";
                        azure.Id = cpfCadastrado.CpfCnpjId;
                    }
                    else
                    if (authenticateDyn365UserSend.ADXPassword.IsNullOrEmpty())
                    {
                        azure.Status = "104"; 
                        azure.Message = "Usuário/Senha incorreto";
                        azure.Id = cpfCadastrado.CpfCnpjId;
                    }
                }
            }
            else
            {
                DRepository dRepository = new DRepository("contacts", "Dyn365Host");
                BaseResponse response = new BaseResponse() { Model = ((DConsultaUsuarioReceive)dRepository.DPesquisarObjeto(condicao, typeof(DConsultaUsuarioReceive))) };
                
                DConsultaUsuarioReceive dados = (DConsultaUsuarioReceive)response.Model;
                if (response.Model != null && (dados.emailaddress1 == dados.username+"@meuemail.com.br"))
                {
                   azure.Status = "102";
                }
            }
            retorno = new BaseResponse { Model = azure, IsValid = true };
            return retorno;
        }

        /// <summary>
        /// Recebe usuario e senha descriptografada para criptografar com SHA para validar no sistema antigo
        /// </summary>
        /// <param name = "loginUsuario" ></param >
        /// <param name="senhaUsuario"></param>
        /// <param name="senhaOriginal"></param>
        /// <returns></returns>
        public BaseModelAzureCopaUserReceive AutenticarDigital(string loginUsuario, string senhaUsuario, string senhaOriginal)
        {
            BaseModelAzureCopaUserReceive retorno = new BaseModelAzureCopaUserReceive();

            var senhacriptografada = CriptografarSHA(senhaUsuario);
            try
            {   
                var cpfCnpj = Convert.ToInt64(loginUsuario);

                var cliente = RepositoryFactory.UnitOfWork.ClienteRepository.Get(x => x.CpfCnpj.Equals(cpfCnpj));

                Boolean migrado = false;

                if (cliente == null || !cliente.Senha.Equals(senhacriptografada))
                {
                    retorno.Message = "";
                    retorno.Status = "500";
                    retorno.Id = Guid.Empty.ToString();
                }
                if (cliente.Id.ToString().IsNullOrEmpty())
                {
                    migrado = Migracliente(cliente, senhaOriginal);
                    if (migrado)
                    {
                        retorno.Message = "";
                        retorno.Status = "101";
                        retorno.Id = cliente.Id.ToString();
                    }
                    else if (!migrado)
                    {
                        cliente.Email = cliente.CpfCnpj+"@meuemail.com.br";
                        migrado = Migracliente(cliente, senhaOriginal);
                        if(migrado)
                        {
                            retorno.Message = "";
                            retorno.Status = "102";
                            retorno.Id = cliente.Id.ToString();
                        }
                    }
                }
                return retorno;
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string CriptografarSHA(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {

                var sha = new SHA1CryptoServiceProvider();
                var data = Encoding.ASCII.GetBytes(input);
                var hash = sha.ComputeHash(data);

                var sb = new StringBuilder();

                for (var i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }

                return sb.ToString();
            }

            return string.Empty;
        }

        //
        /// <summary>
        /// 
        /// </summary>
        public bool Migracliente(ClienteModel cliente,  string senhaoriginal)
        {
            IEnumerable<TelefoneModel> listacelular, listatelefones;
            string cpf, cpfformatado, numerocelular, telefonea, telefoneb, primeiro_nome, sobrenome, politicaprivacidade, termoaceite;
            string[] nome;
            DRepository dRepository;
            bool retorno = false;

            try
            {
                //ANTES DE TUDO, VERIFICAR SE JÁ EXISTE NO BANCO ATUAL, PELO CPF
                //transforma o cpf em string
                cpf = cliente.CpfCnpj.ToString().PadLeft(11, '0');

                DValidaCpfCnpjSend dyn365ValidaCpfCnpjSend = new DValidaCpfCnpjSend()
                {
                    CpfCnpj = cpf
                };
                
                DValidaCpfCnpjReceive dyn365ValidaCpfCnpjReceive = (DValidaCpfCnpjReceive)ValidaCpfCnpjDyn365(dyn365ValidaCpfCnpjSend).Model;

                //caso nao tenha, cadastrar
                if (string.IsNullOrEmpty(dyn365ValidaCpfCnpjReceive.CpfCnpjId))
                {
                    //SE NAO EXISTIR CONTINUAR O PROCESSO DE MIGRAÇÃO
                    //ter certeza de que as variaveis estão limpas e separadas, por isso, evitei declará-las durante a função.

                    listacelular = listatelefones = null;
                    nome = new string[3];
                    primeiro_nome = sobrenome = numerocelular = telefonea = telefoneb = cpfformatado = politicaprivacidade = termoaceite = "";

                    //formtata cpf
                    cpfformatado = $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}." +
                            $"{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";

                    //usuarios sem nome e email nao tem como cadastrar.
                    if (!string.IsNullOrEmpty(cliente.Nome))
                    {
                        //separar o nome do sobrenome se possivel
                        nome = cliente.Nome.Split(new char[] { ' ' }, 2);
                        primeiro_nome = cliente.Nome.Split(new char[] { ' ' }, 2)[0]; //.ToString().Split(' ');
                        sobrenome = "";
                        if (nome.Length > 1)
                        {
                            sobrenome = cliente.Nome.Split(new char[] { ' ' }, 2)[1];
                        }

                        //separar o telefone celular
                        //telefone celular
                        listacelular = cliente.Telefones.Where(x => x.IdTipoTelefone == (TipoTelefoneEnum.CELULAR));

                        if (listacelular.Count() > 0)
                            numerocelular = listacelular.Select(x => x.NumeroTelefone).First().ToString();

                        //caso tenha outros, cadastrar nos telefones a e b
                        listatelefones = cliente.Telefones.Where(x => x.IdTipoTelefone != (TipoTelefoneEnum.CELULAR));

                        if (listatelefones.Count() > 0) telefonea = listatelefones.Select(x => x.NumeroTelefone).First().ToString();
                        if (listatelefones.Count() > 1) telefoneb = listatelefones.Select(x => x.NumeroTelefone).Last().ToString();
                        string imagem = "";
                        //capturar a imagem e convertê-la para uma imagem base64 válida se possivel

                        if (cliente.ImagemPerfil != null)
                            imagem = cliente.ImagemPerfil.ByteArrayToBase64();

                        politicaprivacidade = (cliente.FlagPoliticaPrivacidade ? "1" : "0");
                        termoaceite = (cliente.FlagTermoAceite ? "1" : "0");

                        //caso tenha localidade
                        //var listaIdenticadores  = cliente.Identificadores.Where(w => w.CpfCnpj == (cliente.CpfCnpj));
                       
                        //objeto de inclusão
                        DCadastraUsuarioSend dCadastraUsuario = new DCadastraUsuarioSend()
                        {
                            Username = "servico.app@copasa.com.br",
                            Password = "CYiEjHlatF9W33G56MjcmrfEh1nRXazWw+sYuVM/QGc=",
                            CpfCnpj = cpfformatado,
                            PortalUsername = cpf,
                            PortaluserPasswordBtoa = "false",
                            PortalUserpassword = senhaoriginal/*"Hh00u0sLyUNK8nnday23KXjnR2zMYknlSHKceorPJ6c="*/,
                            Firstname = primeiro_nome,
                            Lastname = sobrenome,
                            Phonetype = "176410000",
                            Mobilephone = numerocelular,
                            Telephone1 = telefonea,
                            Telephone2 = telefoneb,
                            DoNotEmail = "false",
                            EmailAddress1 = cliente.Email.ToString(),
                            CopasaTipoCliente = "176410000",
                            CopasaValidacaoEmail = "176410000",
                            EntityImage = imagem,
                            CopasaPoliticaPrivacidade = politicaprivacidade,
                            CopasaTermoAceite = termoaceite,
                            Locality = null,
                            Neighborhood = null
                        };
                        dRepository = new DRepository("CopasaUser", "Dyn365HostAuthenticate");

                        BaseModelAzureCopaUserReceive CopaUserReceive = (BaseModelAzureCopaUserReceive)dRepository.DExecutarServico("CreateDyn365PortalUser", dCadastraUsuario, typeof(BaseModelAzureCopaUserReceive));
                        if (CopaUserReceive != null)
                        {
                            //caso de erro no cadastro
                            if (string.IsNullOrEmpty(CopaUserReceive.Id))
                            {
                                return false;
                            }
                            else
                            {
                                cliente.Id = Guid.Parse(CopaUserReceive.Id);
                                retorno = true;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }


            return retorno;
        }

        /// <summary>
        /// Valida cpfCnpjDyn365
        /// </summary>
        public BaseResponse ValidaCpfCnpjDyn365(DValidaCpfCnpjSend dyn365ValidaCpfCnpjSend)
        {
            DValidaCpfCnpjReceive dyn365ValidaCpfCnpjReceive = new DValidaCpfCnpjReceive();
            var response = new BaseResponse();
            //insere máscara no cpf ou cnpj
            if (dyn365ValidaCpfCnpjSend.CpfCnpj.Length == 11)
            {
                dyn365ValidaCpfCnpjSend.CpfCnpj = $"{dyn365ValidaCpfCnpjSend.CpfCnpj.Substring(0, 3)}.{dyn365ValidaCpfCnpjSend.CpfCnpj.Substring(3, 3)}." +
                    $"{dyn365ValidaCpfCnpjSend.CpfCnpj.Substring(6, 3)}-{dyn365ValidaCpfCnpjSend.CpfCnpj.Substring(9, 2)}";
            }
            else if (dyn365ValidaCpfCnpjSend.CpfCnpj.Length == 14)
            {
                dyn365ValidaCpfCnpjSend.CpfCnpj = $"{dyn365ValidaCpfCnpjSend.CpfCnpj.Substring(0, 2)}.{dyn365ValidaCpfCnpjSend.CpfCnpj.Substring(2, 3)}.{dyn365ValidaCpfCnpjSend.CpfCnpj.Substring(5, 3)}" +
                    $"/{dyn365ValidaCpfCnpjSend.CpfCnpj.Substring(8, 4)}-{dyn365ValidaCpfCnpjSend.CpfCnpj.Substring(12, 2)}";
            }
            else
            {
                dyn365ValidaCpfCnpjReceive.IsValid = false;
                dyn365ValidaCpfCnpjReceive.descricaoRetorno = "Informação inválida.";
                response.Model = dyn365ValidaCpfCnpjReceive;
                return response;
            }

            //List<DUsuario>
            string condicao = $"$filter=copasa_cpf_cnpj eq '{ dyn365ValidaCpfCnpjSend.CpfCnpj}'";
            DRepository dRepository = new DRepository("contacts", "Dyn365Host");
            DValidaCpfCnpjReceive Retorno = (DValidaCpfCnpjReceive)dRepository.DPesquisarObjeto(condicao, dyn365ValidaCpfCnpjReceive.GetType());

            if (Retorno != null)
            {
                dyn365ValidaCpfCnpjReceive.CpfCnpjId = Retorno.CpfCnpjId;
            }
            else
            {
                dyn365ValidaCpfCnpjReceive.IsValid = false;
                if (dyn365ValidaCpfCnpjSend.CpfCnpj.Length == 18)  // cnpj com mascara
                    dyn365ValidaCpfCnpjReceive.descricaoRetorno = "CNPJ não encontrado.";
                else
                {
                    dyn365ValidaCpfCnpjReceive.descricaoRetorno = "CPF não encontrado.";
                }
            }
            response = new BaseResponse();
            response.Model = dyn365ValidaCpfCnpjReceive;
            return response;
        }

        /// <summary>
        /// Rotina para corrigir registros de protocolos no Dynamics365 que tenham algum erro
        /// </summary>
        public BaseResponse corrigirProtocolos()
        {
            BaseResponse response = new BaseResponse();
            StringBuilder protocolosAtualizados = new StringBuilder();
            string virgula = "";
            IDyn365ProtocoloRepository dyn365ProtocoloRepository = new Dyn365ProtocoloRepository();
            IDyn365LocalidadeRepository dyn365LocalidadeRepository = new Dyn365LocalidadeRepository();
            _log.IsBatch();
            string condicao = $"$filter=copasa_codigoservico ne null and statuscode eq 1 and copasa_matriculaintegracao ne null and copasa_identificadorintegracao ne null and _copasa_localidadeid_value eq null";
            List<BaseModel> listaRetorno = dyn365ProtocoloRepository.DPesquisarLista(condicao, typeof(Dyn365Protocolo));
            int cont = 0;
            foreach(BaseModel baseModel in listaRetorno)
            {
                
                //if (cont == 100)
                //    break;
                Dyn365Protocolo protocolo = (Dyn365Protocolo)baseModel;
                SCN4ISU1Send sCN4ISU1Send = new SCN4ISU1Send();
                sCN4ISU1Send.identificadores = new string[1];
                sCN4ISU1Send.identificadores[0] = protocolo.identificadorIntegracao;
                SCN4ISU1Receive sCN4ISU1Receive = (SCN4ISU1Receive)SCN4ISU1(sCN4ISU1Send).Model;
                
                foreach (SCN4ISU1ReceiveMatriculas matricula in sCN4ISU1Receive.matriculas)
                {
                    if (matricula.matricula.Equals(protocolo.matricula) && matricula.identificador.Equals(protocolo.identificadorIntegracao))
                    {
                        string localidade = matricula.codigoLocalidade;
                        List<Dyn365Localidade> listRetorno = dyn365LocalidadeRepository.Pesquisar("copasa_codigo", localidade);
                        if (listRetorno.Count > 0)
                        {
                            Dyn365Protocolo protocoloAterar = new Dyn365Protocolo();

                            protocoloAterar.idLocalidade = listRetorno[0].id;
                            protocoloAterar.incidentid = protocolo.incidentid;
                            if (dyn365ProtocoloRepository.Atualizar(protocoloAterar))
                            {
                                cont++;
                                protocolosAtualizados.Append(virgula + protocolo.numeroProtocolo);
                                virgula = ", ";

                            }
                        }
                        break;
                    }
                }
            }
            if (cont > 0)
                response.Message = "Foram corrigidos   " + cont + " protocolos no Dynamics: " + protocolosAtualizados.ToString();
            else
                response.Message = "";
            return response;
        }

    }

}
