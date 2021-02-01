using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Copasa.Atende.Business.Core;
using Copasa.Atende.Business.Interfaces;
using Copasa.Atende.Model;
using Copasa.Atende.Model.Broker;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Model.Digital;
using Copasa.Atende.Model.Dyn365;
using Copasa.Atende.Model.Dyn365.Localidade;
using Copasa.Atende.Model.Dyn365.Protocolo;
using Copasa.Atende.Model.Enumerador;
using Copasa.Atende.Repository.Interfaces.DBrokerRepository;
using Copasa.Atende.Repository.Repositories.DBrokerRepository;
using Copasa.Atende.Repository.Repositories.Digital;
using Copasa.Atende.Repository.Repositories.Dyn365;
using Copasa.Atende.Repository.UnitOfWork;
using Copasa.Util;

namespace Copasa.Atende.Business.Rules
{

    /// <summary>
    /// Rule - Dynamics 365 Cliente.
    /// </summary>
    public class DClienteRule : BaseRule, IDClienteRule
    {
        private readonly IClienteRule _clienteRule;
        private readonly IFaturaRule _faturaRule;
        private readonly ICertidaoNegativaDebitoRule _certidaoNegativaDebitoRule;
        private readonly IServicoOperacionalRule _servicoOperacionalRule;
        private DRepository dRepository;

        /// <summary>
        /// Para chamar a função diretamente
        /// </summary>
        private static IUsuarioCopasaRepository UsuarioCopasaRepository
        {
            get { return new UsuarioCopasaRepository(); }
        }

        /// <summary>
        /// Construtor InformarLeituraFacade.
        /// </summary>
        /// <param name="clienteRule">IClienteRule.</param>
        /// <param name="faturaRule">IFaturaRule.</param>
        /// <param name="servicoOperacionalRule">IFaturaRule.</param>
        /// /// <param name="certidaoNegativaDebitoRule">IServicoOperacionalRule.</param>
        public DClienteRule(IClienteRule clienteRule, IFaturaRule faturaRule, ICertidaoNegativaDebitoRule certidaoNegativaDebitoRule, IServicoOperacionalRule servicoOperacionalRule)
        {
            _clienteRule = clienteRule;
            _faturaRule = faturaRule;
            _certidaoNegativaDebitoRule = certidaoNegativaDebitoRule;
            _servicoOperacionalRule = servicoOperacionalRule;
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            throw new NotImplementedException();
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
            dRepository = new DRepository("contacts", "Dyn365Host");
            DValidaCpfCnpjReceive Retorno = (DValidaCpfCnpjReceive)dRepository.DPesquisarObjeto(condicao, dyn365ValidaCpfCnpjReceive.GetType());

            if (Retorno != null)
            {
                dyn365ValidaCpfCnpjReceive.CpfCnpjId = Retorno.CpfCnpjId;
            }
            else
            {
                dyn365ValidaCpfCnpjReceive.IsValid = false;
                if (dyn365ValidaCpfCnpjSend.CpfCnpj.Length == 18)
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
        /// criação de protocolo.
        /// </summary>
        /// <param name="criaProtocoloSend"></param>
        /// <returns></returns>
        public BaseResponse CriaProtocolo(DCriaProtocoloSend criaProtocoloSend)
        {
            DCriaProtocoloReceive dyn365CriaProtocoloReceive = new DCriaProtocoloReceive();

            var empresaCod = (criaProtocoloSend.Empresa == "COPASA" ? 1 : 2).ToInt();

            //string cpfcnpjform = "";
            string agencia = TrataOrigem(criaProtocoloSend.Origem);
            if (agencia != null)
            {
                DCriaProtocolo dyn365CriaProtocolo = new DCriaProtocolo()
                {
                    //CpfCnpj = InsereMascaraCpfCnpj(criaProtocoloSend.CpfCnpj), // cpfCpj já chegava com mascara - desnecessário
                    CpfCnpj = criaProtocoloSend.CpfCnpj,
                    Title = $"{criaProtocoloSend.Origem}-CriaProtocolo",
                    IdCpfCnpj = string.Format("/contacts({0})", criaProtocoloSend.IdCpfCnpj),
                    TipoProtocolo = criaProtocoloSend.TipoProtocolo,
                    Empresa = empresaCod.ToString(),
                    //recupera o id da agencia para a inclusão
                    Agencia = string.Format("/copasa_agenciafisicas({0})", agencia),
                    Nome = criaProtocoloSend.Nome,
                    Telefone1 = criaProtocoloSend.Telefone1,
                    Telefone2 = criaProtocoloSend.Telefone2,
                    Email = criaProtocoloSend.Email
                };

                if ("APP".Equals(criaProtocoloSend.Origem))
                {
                    dyn365CriaProtocolo.Origem = 8;
                }
                else if ("URA".Equals(criaProtocoloSend.Origem))
                {
                    dyn365CriaProtocolo.Origem = 3;
                }
                else
                {
                    dyn365CriaProtocolo.Origem = 4;
                }


                dRepository = new DRepository("incidents", "Dyn365Host");
                dyn365CriaProtocoloReceive = (DCriaProtocoloReceive)dRepository.DExecutarServico(null, dyn365CriaProtocolo, typeof(DCriaProtocoloReceive));
                //dyn365UsuarioRepository.Pesquisar("copasa_cpf_cnpj", dyn365ValidaCpfCnpjSend.cpfCnpj);
                var response = new BaseResponse();
                response.Model = dyn365CriaProtocoloReceive;
                return response;
            }
            else
                return null;
        }

        /// <summary>
        /// consulta um identificador no D365, retornando seu id
        /// </summary>
        /// <param name="consultaIdentificadorSend"></param>
        /// <returns></returns>
        public BaseResponse ConsultaIdentificador(DConsultaIdentificadorSend consultaIdentificadorSend)
        {
            dRepository = new DRepository("copasa_identificadors", "Dyn365Host");
            string condicao = $"$filter=copasa_name eq '{consultaIdentificadorSend.Identificador}'";
            DConsultaIdentificadorReceive retorno = (DConsultaIdentificadorReceive)dRepository.DPesquisarObjeto(condicao, typeof(DConsultaIdentificadorReceive));

            //caso de lista vazia.
            if (retorno == null)
            {
                retorno = new DConsultaIdentificadorReceive();
                retorno.IsValid = false;
                retorno.descricaoRetorno = "Identificador não encontrado.";
            }

            var response = new BaseResponse();
            response.Model = retorno;
            return response;
        }

        /// <summary>
        /// Cria um Novo Identificador no Microsoft Dynamics 365.
        /// </summary>
        public BaseResponse CadastraIdentificador(DCadastraIdentificadorSend dCadastraIdentificadorSend)
        {
            DAssociaIdentificadorReceive dAssociaIdentificadorReceive = new DAssociaIdentificadorReceive();
            var response = new BaseResponse();

            #region verifica se identificador existe no SICOM
            SCN4ISU1Send sCN4ISU1Send = new SCN4ISU1Send()
            {
                codigoServicoSolicitado = "",
                cpfCnpj = "",
                empresa = "",
                flagTipoUsu = "",
                protocolo = "",
                identificadores = new string[] { dCadastraIdentificadorSend.CopasaCodigo }
            };

            SCN4ISU1Receive sCN4ISU1Receive = (SCN4ISU1Receive)_clienteRule.SCN4ISU1(sCN4ISU1Send).Model;

            if (sCN4ISU1Receive.matriculas.Count == 0)
            {
                dAssociaIdentificadorReceive.IsValid = false;
                dAssociaIdentificadorReceive.descricaoRetorno = "Identificador não encontrado no SICOM.";
                response.Model = dAssociaIdentificadorReceive;
                return response;
            }
            #endregion;

            dRepository = new DRepository("CopasaUser", "Dyn365HostAuthenticate");
            return new BaseResponse
            {
                Model = dRepository.ExecutarServico("CreateDyn365Identifier", dCadastraIdentificadorSend, typeof(DCadastraIdentificador))
            };
        }

        /// <summary>
        /// Associa um identificador a um cpf/cnpj
        /// </summary>
        /// <returns></returns>
        public BaseResponse DAssociaIdentificador(DAssociaIdentificadorSend dAssociaIdentificadorSend)
        {
            DAssociaIdentificadorReceive dAssociaIdentificadorReceive = new DAssociaIdentificadorReceive();
            var response = new BaseResponse();

            #region verifica se identificador existe no SICOM
            SCN4ISU1Send sCN4ISU1Send = new SCN4ISU1Send()
            {
                codigoServicoSolicitado = "",
                cpfCnpj = "",
                empresa = "",
                flagTipoUsu = "",
                protocolo = "",
                identificadores = new string[] { dAssociaIdentificadorSend.Identificador }
            };

            SCN4ISU1Receive sCN4ISU1Receive = (SCN4ISU1Receive)_clienteRule.SCN4ISU1(sCN4ISU1Send).Model;

            if (sCN4ISU1Receive.matriculas.Count == 0)
            {
                dAssociaIdentificadorReceive.IsValid = false;
                dAssociaIdentificadorReceive.descricaoRetorno = "Identificador não encontrado.";
                response.Model = dAssociaIdentificadorReceive;
                return response;
            }
            #endregion;

            #region verifica se associação identificadorcnpj/identificador existe no SICOM
            //verifica associação identificador/cnpj no SICOM
            if (!string.IsNullOrEmpty(dAssociaIdentificadorSend.IdentificadorCnpj))
            {
                ////verifica se o cnpj e identificador existem no SICOM
                TrabValidaUsuarioSend trabValidaUsuarioSend = new TrabValidaUsuarioSend()
                {
                    CpfCnpj = dAssociaIdentificadorSend.IdentificadorCnpj,
                    empresa = "",
                    identificador = dAssociaIdentificadorSend.Identificador,
                    protocolo = ""
                };

                TrabValidaUsuarioReceive trabValidaUsuarioReceive = (TrabValidaUsuarioReceive)_clienteRule.validaUsuario(trabValidaUsuarioSend).Model;
                //

                //caso nao exista no SICOM, retornar mensagem de nao valido
                if (string.IsNullOrEmpty(trabValidaUsuarioReceive.CpfCnpj))
                {
                    dAssociaIdentificadorReceive.IsValid = false;
                    dAssociaIdentificadorReceive.descricaoRetorno = "Cnpj do identificador não encontrado no Comercial.";
                    response.Model = dAssociaIdentificadorReceive;
                    return response;
                }
            }
            #endregion

            //recuperar identificadorid antes da verificação associação identificador/cpf/cnpj
            string IdentificadorId;

            #region verificação do identificador no d365
            //verificar cadastro do idenficador >> ConsultaIdentificador

            DConsultaIdentificadorSend dConsultaIdentificadorSend = new DConsultaIdentificadorSend()
            {
                Identificador = dAssociaIdentificadorSend.Identificador
            };

            DConsultaIdentificadorReceive dConsultaIdentificadorReceive = (DConsultaIdentificadorReceive)ConsultaIdentificador(dConsultaIdentificadorSend).Model;

            if (string.IsNullOrEmpty(dConsultaIdentificadorReceive.IdentificadorId))
            {
                //cadastra o identificador no d365
                DCadastraIdentificadorSend dCadastraIdentificadorSend = new DCadastraIdentificadorSend()
                {
                    Username = dAssociaIdentificadorSend.Username,
                    Password = dAssociaIdentificadorSend.Password,
                    CopasaCodigo = dAssociaIdentificadorSend.Identificador
                };

                DCadastraIdentificador dCadastraIdentificador = (DCadastraIdentificador)CadastraIdentificador(dCadastraIdentificadorSend).Model;

                if (string.IsNullOrEmpty(dCadastraIdentificador.IdentificadorId))
                {
                    dAssociaIdentificadorReceive.IsValid = false;
                    dAssociaIdentificadorReceive.descricaoRetorno = "Erro ao tentar cadastrar o número do identificador.";
                    response.Model = dAssociaIdentificadorReceive;
                    return response;
                }
                else
                {
                    IdentificadorId = dCadastraIdentificador.IdentificadorId;
                }
            }
            else
            {
                IdentificadorId = dConsultaIdentificadorReceive.IdentificadorId;
                dAssociaIdentificadorReceive.descricaoRetorno = "O Identificador ja foi associado anteriormente.";
            }
            #endregion

            //verifica associação identificador/cpf/cnpj no d365
            dRepository = new DRepository("copasa_controledeidentificadors", "Dyn365Host");
            DIdentificador dyn365Identificador = new DIdentificador();
            //DListaIdentificadorReceive dyn365ListaIdentificadorReceive = new DListaIdentificadorReceive();

            string condicao = $"$filter=_copasa_contatoid_value eq '{dAssociaIdentificadorSend.IdCpfCnpj}' and _copasa_identificadorid_value eq '{IdentificadorId}'";

            List<BaseModel> listaDConsultaAssocIdentReceive = dRepository.DPesquisarLista(condicao, typeof(DConsultaAssocIdentReceive));

            //dyn365ListaIdentificadorReceive.Identificador = dRepository.DPesquisarLista(condicao, dyn365Identificador.GetType()).Cast<DIdentificador>().ToList();

            //MGAPP-verifica/cadastra usuario
            if (!(dAssociaIdentificadorSend.Origem == Model.Enumerador.TipoOrigem.APP))
            {
                //
                VerificaCadastroCpfCnpj(dAssociaIdentificadorSend.CpfCnpjLogin, dAssociaIdentificadorSend.NomeSolicitante, dAssociaIdentificadorSend.TelefoneSolicitante, dAssociaIdentificadorSend.EmailSolicitante);
            }

            #region associa identificador/cpfcnpj no d365

            if (listaDConsultaAssocIdentReceive.Count > 0)
            {
                foreach (DConsultaAssocIdentReceive identificador in listaDConsultaAssocIdentReceive)
                {

                    //consulta id da associacao
                    condicao = $"$select=copasa_controledeidentificadorid&$filter=_copasa_identificadorid_value eq '{dConsultaIdentificadorReceive.IdentificadorId}' and _copasa_contatoid_value eq '{dAssociaIdentificadorSend.IdCpfCnpj}'";
                    dRepository = new DRepository("copasa_controledeidentificadors", "Dyn365Host");
                    //copasa_controledeidentificadors
                    DConsultaAssocIdentReceive dConsultaAssocIdentReceive = (DConsultaAssocIdentReceive)dRepository.DPesquisarObjeto(condicao, typeof(DConsultaAssocIdentReceive));
                    //
                    DDesassociaIdentificador dDesassociaIdentificador = new DDesassociaIdentificador();
                    dDesassociaIdentificador.ControleIdentificadorIdAtualizar = dConsultaAssocIdentReceive.ControleIdentificadorId;
                    dDesassociaIdentificador.Status = "1"; dDesassociaIdentificador.State = "0";
                    //
                    bool resposta = dRepository.DAtualizar(dDesassociaIdentificador, dDesassociaIdentificador.ControleIdentificadorIdAtualizar);
                    //
                    dAssociaIdentificadorReceive.AssociacaoId = dConsultaAssocIdentReceive.ControleIdentificadorId;
                }
            }
            else
            {
                Dyn365AssociateIdentifierXUserSend associateDyn365IdentifierXUserSend = new Dyn365AssociateIdentifierXUserSend()
                {
                    Username = dAssociaIdentificadorSend.Username,
                    Password = dAssociaIdentificadorSend.Password,
                    CopasaContatoId = dAssociaIdentificadorSend.IdCpfCnpj,
                    CopasaIdentificadorId = IdentificadorId
                };

                BaseModelAzureCopaUserReceive baseModelAzureCopaUserReceive = (BaseModelAzureCopaUserReceive)_clienteRule.AssociaIdentificador(associateDyn365IdentifierXUserSend).Model;

                if (string.IsNullOrEmpty(baseModelAzureCopaUserReceive.Id))
                {
                    dAssociaIdentificadorReceive.IsValid = false;
                    dAssociaIdentificadorReceive.descricaoRetorno = baseModelAzureCopaUserReceive.Message;
                    response.Model = dAssociaIdentificadorReceive;
                    return response;
                }
                else
                {
                    dAssociaIdentificadorReceive.AssociacaoId = baseModelAzureCopaUserReceive.Id;
                }
            }
            #endregion

            #region cria/atualiza protocolo
            //CRIAR PROTOCOLO
            DCriaProtocoloSend criaProtocoloSend = new DCriaProtocoloSend()
            {
                CpfCnpj = dAssociaIdentificadorSend.CpfCnpjLogin,
                IdCpfCnpj = dAssociaIdentificadorSend.IdCpfCnpj,
                TipoProtocolo = "5",
                Empresa = dAssociaIdentificadorSend.Empresa,
                Origem = dAssociaIdentificadorSend.Origem,
            };
            DCriaProtocoloReceive dCriaProtocoloReceive = ((DCriaProtocoloReceive)CriaProtocolo(criaProtocoloSend).Model);
            //--
            //atualiza protocolo
            DAtualizaProtocoloSend dAtualiza = new DAtualizaProtocoloSend()
            {
                Titulo = dAssociaIdentificadorSend.Origem + " CAD01",
                Empresa = dAssociaIdentificadorSend.Empresa,
                Servico = "CAD",
                SubtipoId = "CAD01",
                PavimentacaoId = "",
                ProtocoloId = dCriaProtocoloReceive.ProtocoloId
            };

            AtualizaProtocolo(dAtualiza);
            #endregion

            response.Model = dAssociaIdentificadorReceive;
            return response;
        }

        /// <summary>
        /// Desassocia um identificador a um cpf/cnpj e um identificador
        /// </summary>
        /// <returns></returns>
        public BaseResponse DesassociaIdentificador(DDesassociaIdentificadorSend dDesassociaIdentificadorSend)
        {

            //Consulta id do associado
            DConsultaIdentificadorSend dConsultaIdentificadorSend = new DConsultaIdentificadorSend()
            {
                Identificador = dDesassociaIdentificadorSend.Identificador
            };

            DConsultaIdentificadorReceive dConsultaIdentificadorReceive = (DConsultaIdentificadorReceive)ConsultaIdentificador(dConsultaIdentificadorSend).Model;
            //consulta id da associacao
            string condicao = $"$select=copasa_controledeidentificadorid&$filter=_copasa_identificadorid_value eq '{dConsultaIdentificadorReceive.IdentificadorId}' and _copasa_contatoid_value eq '{dDesassociaIdentificadorSend.CpfCnpjId}'";
            dRepository = new DRepository("copasa_controledeidentificadors", "Dyn365Host");
            //copasa_controledeidentificadors
            List<BaseModel> listaDConsultaAssocIdentReceive = dRepository.DPesquisarLista(condicao, typeof(DConsultaAssocIdentReceive));
            //DConsultaAssocIdentReceive dConsultaAssocIdentReceive = (DConsultaAssocIdentReceive)dRepository.DPesquisarObjeto(condicao, typeof(DConsultaAssocIdentReceive));

            if (listaDConsultaAssocIdentReceive != null)
            {
                bool resposta = false;
                foreach (BaseModel baseModel in listaDConsultaAssocIdentReceive)
                {
                    DConsultaAssocIdentReceive dConsultaAssocIdentReceive = (DConsultaAssocIdentReceive)baseModel;
                    DDesassociaIdentificador dDesassociaIdentificador = new DDesassociaIdentificador();
                    dDesassociaIdentificador.ControleIdentificadorIdAtualizar = dConsultaAssocIdentReceive.ControleIdentificadorId;
                    dDesassociaIdentificador.Status = "2";
                    dDesassociaIdentificador.State = "1";
                    //
                    resposta = dRepository.DAtualizar(dDesassociaIdentificador, dDesassociaIdentificador.ControleIdentificadorIdAtualizar);
                }
                if (resposta)
                {

                    DCriaProtocoloSend criaProtocoloSend = new DCriaProtocoloSend()
                    {
                        CpfCnpj = dDesassociaIdentificadorSend.CpfCnpj,
                        IdCpfCnpj = dDesassociaIdentificadorSend.CpfCnpjId,
                        TipoProtocolo = "5",
                        Empresa = dDesassociaIdentificadorSend.Empresa,
                        Origem = dDesassociaIdentificadorSend.Origem
                    };
                    DCriaProtocoloReceive dCriaProtocoloReceive = ((DCriaProtocoloReceive)CriaProtocolo(criaProtocoloSend).Model);

                    //--
                    //atualiza protocolo
                    DAtualizaProtocoloSend dAtualiza = new DAtualizaProtocoloSend()
                    {
                        Titulo = dDesassociaIdentificadorSend.Origem + " CAD03",
                        Empresa = dDesassociaIdentificadorSend.Empresa,
                        Servico = "CAD",
                        SubtipoId = "CAD03",
                        PavimentacaoId = "",
                        ProtocoloId = dCriaProtocoloReceive.ProtocoloId
                    };

                    return new BaseResponse() { IsValid = true, Model = new DMensagemRetornoReceive() { Mensagem = "Desassociação efetuada." } };
                }
                else
                {
                    return new BaseResponse() { IsValid = false, Model = new DMensagemRetornoReceive() { Mensagem = "Desassociação não efetuada." } };
                }
            }
            else
            {
                return new BaseResponse() { IsValid = false, Model = new DMensagemRetornoReceive() { Mensagem = "Associação não encontrada." } };
            }
        }

        /// <summary>
        /// ws que trata e/ou verifica o cpf ou cnpj no Dyn365 e retorna os identificadores e matriculas vinculados a ele no SICOM
        /// </summary>
        /// <param name="listaUsuarioSend"></param>
        /// <returns></returns>
        public BaseResponse ListaUsuario(DListaUsuarioSend listaUsuarioSend)
        {
            List<string> identificadores = new List<string>();
            DListaUsuarioReceive dyn365ListaUsuarioReceive = new DListaUsuarioReceive();
            var response = new BaseResponse();

            if (listaUsuarioSend.Origem == Model.Enumerador.TipoOrigem.APP)
            {
                //consulta identificadores vinculados ao cpf/cnpj
                DListaIdentificadorReceive dyn365ListaIdentificadorReceive = new DListaIdentificadorReceive();

                DListaIdentificadorSend dyn365ListaIdentificadorSend = new DListaIdentificadorSend()
                {
                    IdCpfCnpj = listaUsuarioSend.IdCpfCnpj
                };

                dyn365ListaIdentificadorReceive = (DListaIdentificadorReceive)ListaIdentificador(dyn365ListaIdentificadorSend).Model;
                //
                foreach (var item in dyn365ListaIdentificadorReceive.Identificador)
                {
                    identificadores.Add(item.Identificador);
                }
            }
            else
            {
                //se n tem idcpfcnpn e nao passou informações para cadastro
                if (string.IsNullOrEmpty(listaUsuarioSend.IdCpfCnpj))
                {
                    if (((string.IsNullOrEmpty(listaUsuarioSend.NomeSolicitante)) && (string.IsNullOrEmpty(listaUsuarioSend.TelefoneSolicitante)) && (string.IsNullOrEmpty(listaUsuarioSend.EmailSolicitante))))
                        return new BaseResponse() { IsValid = false, Message = "Defina um identificador, ou defina o nome,telefone e email do cpf/cnpj" };
                    else
                    {
                        //verifica se CPF ou CNPJ existe no D365, se não existir cadastra
                        listaUsuarioSend.IdCpfCnpj = VerificaCadastroCpfCnpj(listaUsuarioSend.CpfCnpjLogin, listaUsuarioSend.NomeSolicitante, listaUsuarioSend.TelefoneSolicitante, listaUsuarioSend.EmailSolicitante);
                    }
                }

                if (listaUsuarioSend.CpfCnpjLogin.Length > 11)
                {
                    if (string.IsNullOrEmpty(listaUsuarioSend.Identificador))
                    {
                        dyn365ListaUsuarioReceive = new DListaUsuarioReceive();
                        dyn365ListaUsuarioReceive.descricaoRetorno = "Digite um identificador.";
                        response.Model = dyn365ListaUsuarioReceive;
                        return response;
                    }

                    //verifica se cnpj e o identificador existem no sicom
                    TrabValidaUsuarioSend trabValidaUsuarioSend = new TrabValidaUsuarioSend()
                    {
                        CpfCnpj = listaUsuarioSend.IdentificadorCnpj,
                        empresa = "",
                        identificador = listaUsuarioSend.Identificador,
                        protocolo = ""
                    };

                    TrabValidaUsuarioReceive trabValidaUsuarioReceive = (TrabValidaUsuarioReceive)_clienteRule.validaUsuario(trabValidaUsuarioSend).Model;

                    if (string.IsNullOrEmpty(trabValidaUsuarioReceive.CpfCnpj))
                    {
                        dyn365ListaUsuarioReceive = new DListaUsuarioReceive();
                        dyn365ListaUsuarioReceive.descricaoRetorno = "Identificador e/ou CNPJ inválidos.";
                        response.Model = dyn365ListaUsuarioReceive;
                        return response;
                    }
                    else
                    {
                        identificadores.Add(listaUsuarioSend.Identificador);
                    }
                }
            }

            //
            dyn365ListaUsuarioReceive.IdCpfCnpj = listaUsuarioSend.IdCpfCnpj;
            if (listaUsuarioSend.CpfCnpjLogin != null && !"".Equals(listaUsuarioSend.CpfCnpjLogin))
            {
                SCN3PCLISend sCN3PCLISend = new SCN3PCLISend() { CpfCnpj = listaUsuarioSend.CpfCnpjLogin, empresa = listaUsuarioSend.Empresa };
                SCN3PCLIReceive sCN3PCLIReceive = (SCN3PCLIReceive)_clienteRule.SCN3PCLI(sCN3PCLISend).Model;

                if (sCN3PCLIReceive.identificadores != null)
                {
                    foreach (var item in sCN3PCLIReceive.identificadores)
                    {
                        if (!identificadores.Contains(item.identificador.ToString()))
                            identificadores.Add(item.identificador.ToString());
                    }
                }

                //consulta cliente/lista/matriculas
                SCN4ISU1Send sCN4ISU1Send = new SCN4ISU1Send()
                {
                    codigoServicoSolicitado = "",
                    cpfCnpj = listaUsuarioSend.CpfCnpjLogin,
                    empresa = "",
                    flagTipoUsu = "",
                    identificadores = identificadores.ToArray(),
                    protocolo = ""
                };

                SCN4ISU1Receive sCN4ISU1Receive = (SCN4ISU1Receive)_clienteRule.SCN4ISU1(sCN4ISU1Send).Model;

                foreach (SCN4ISU1ReceiveMatriculas item in sCN4ISU1Receive.matriculas)
                {

                    DValidaCpfCnpjSend dyn365ValidaCpfCnpjSend = new DValidaCpfCnpjSend()
                    {
                        CpfCnpj = Util.Util.retiraCaracteresMascara(item.cpfCnpj)
                    };

                    DValidaCpfCnpjReceive dyn365ValidaCpfCnpjReceive = (DValidaCpfCnpjReceive)ValidaCpfCnpjDyn365(dyn365ValidaCpfCnpjSend).Model;

                    DListaUsuario dyn365ListaUsuario = new DListaUsuario()
                    {
                        Identificador = item.identificador,
                        CpfcnpjProprietario = Util.Util.retiraCaracteresMascara(item.cpfCnpj),
                        IdProprietario = dyn365ValidaCpfCnpjReceive.CpfCnpjId,
                        Bairro = item.bairro,
                        Complemento = item.complementoLogradouro,
                        Localidade = item.Localidade,
                        Logradouro = item.logradouro,
                        Matricula = item.matricula,
                        NumeroLogradouro = item.numeroLogradouro,
                        DataInicioVigencia = DateTime.Parse(item.dataInicioVigencia.ToString().Substring(0, 10)),
                        DataFinalVigencia = DateTime.Parse(item.dataFimVigencia.ToDateTime("yyyyMMdd").ToString("dd/MM/yyyy")).AddHours(23).AddMinutes(59).AddSeconds(59)
                    };
                    dyn365ListaUsuarioReceive.ListaIdentificadorMatricula.Add(dyn365ListaUsuario);
                }
            }
            //CRIAR PROTOCOLO
            DCriaProtocoloSend criaProtocoloSend = new DCriaProtocoloSend()
            {
                CpfCnpj = InsereMascaraCpfCnpj(listaUsuarioSend.CpfCnpjLogin),
                IdCpfCnpj = listaUsuarioSend.IdCpfCnpj,
                TipoProtocolo = "5",
                Empresa = listaUsuarioSend.Empresa,
                Origem = listaUsuarioSend.Origem,
                Email = listaUsuarioSend.EmailSolicitante,
                Nome = listaUsuarioSend.NomeSolicitante,
                Telefone1 = listaUsuarioSend.TelefoneSolicitante,
                Telefone2 = ""
            };
            BaseResponse baseResponse = CriaProtocolo(criaProtocoloSend);
            if (baseResponse != null)
            {
                DCriaProtocoloReceive dCriaProtocoloReceive = (DCriaProtocoloReceive)baseResponse.Model;
                dyn365ListaUsuarioReceive.ProtocoloId = dCriaProtocoloReceive.ProtocoloId;
                dyn365ListaUsuarioReceive.Protocolo = dCriaProtocoloReceive.Protocolo;
            }
            response.Model = dyn365ListaUsuarioReceive;
            return response;
        }

        /// <summary>
        /// Lista de Status Fatura Email
        /// </summary>
        /// <param name="dListaStatusFaturaSend"></param>
        /// <returns></returns>
        public BaseResponse ListaStatusFaturaEmail(DListaStatusFaturaSend dListaStatusFaturaSend)
        {
            DListaIdentificadorReceive dyn365ListaIdentificadorReceive = new DListaIdentificadorReceive();
            List<string> identificadores = new List<string>();

            DListaIdentificadorSend dyn365ListaIdentificadorSend = new DListaIdentificadorSend()
            {
                IdCpfCnpj = dListaStatusFaturaSend.IdCpfCnpj
            };

            dyn365ListaIdentificadorReceive = (DListaIdentificadorReceive)ListaIdentificador(dyn365ListaIdentificadorSend).Model;
            //
            foreach (var item in dyn365ListaIdentificadorReceive.Identificador)
            {
                identificadores.Add(item.Identificador);
            }

            DStatusFaturaEmail dStatusFaturaEmail;
            DListaStatusFaturaEmailReceive listaStatusFaturaEmailReceive = new DListaStatusFaturaEmailReceive();

            //CONSULTA DOS IDENTIFICADORES dyn 365
            //consulta cliente/lista/matriculas
            SCN4ISU1Send sCN4ISU1Send = new SCN4ISU1Send()
            {
                codigoServicoSolicitado = "",
                cpfCnpj = dListaStatusFaturaSend.CpfCnpj,
                empresa = "",
                flagTipoUsu = "",
                identificadores = identificadores.ToArray(),
                protocolo = ""
            };

            SCN4ISU1Receive sCN4ISU1Receive = (SCN4ISU1Receive)_clienteRule.SCN4ISU1(sCN4ISU1Send).Model;

            foreach (SCN4ISU1ReceiveMatriculas item in sCN4ISU1Receive.matriculas)
            {
                if (item.situacao.StartsWith("RA"))
                {
                    dStatusFaturaEmail = new DStatusFaturaEmail()
                    {
                        Identificador = item.identificador,
                        Matricula = item.matricula,
                        Email = item.email,
                        FaturaEmail = item.faturaEntreguePorEmail
                    };
                    listaStatusFaturaEmailReceive.StatusFaturasEmail.Add(dStatusFaturaEmail);
                }
            }

            var response = new BaseResponse();
            response.Model = listaStatusFaturaEmailReceive;
            //--
            return response;
        }

        //pesquisar essa lista no lista/matriculas
        //filtrar o retorno
        /// <summary>
        /// Retorna identificadores associados ao cpf/cnpj no D365
        /// </summary>
        public BaseResponse ListaIdentificador(DListaIdentificadorSend dyn365ListaIdentificadorSend)
        {
            DListaIdentificadorReceive dyn365ListaIdentificadorReceive = new DListaIdentificadorReceive();
            dRepository = new DRepository("copasa_controledeidentificadors", "Dyn365Host");
            DIdentificador dyn365Identificador = new DIdentificador();

            List<DListaIdentificadorReceive> ListaRetorno = new List<DListaIdentificadorReceive>();

            string condicao = $"$filter=_copasa_contatoid_value eq '{dyn365ListaIdentificadorSend.IdCpfCnpj}' and statuscode eq 1";

            dyn365ListaIdentificadorReceive.Identificador = dRepository.DPesquisarLista(condicao, dyn365Identificador.GetType()).Cast<DIdentificador>().ToList();

            //caso de lista vazia.
            if (dyn365ListaIdentificadorReceive.Identificador.Count == 0)
            {
                dyn365ListaIdentificadorReceive.IsValid = false;
                dyn365ListaIdentificadorReceive.descricaoRetorno = "Identificadores não encontrados.";
            }
            var response = new BaseResponse();
            response.Model = dyn365ListaIdentificadorReceive;
            return response;
        }

        /// <summary>
        /// Retorna o Historico de faturas pagas do identificador
        /// </summary>
        /// <param name="faturaSend"></param>
        /// <returns></returns>
        public BaseResponse DFaturas(DFaturasSend faturaSend)
        {
            BaseResponse baseModelresp = new BaseResponse();

            //historico de consumo
            if (faturaSend.SubTipo == "FAT19")
            {
                SCN5ISHCSend sCN5ISHCSend = new SCN5ISHCSend()
                {
                    identificador = faturaSend.Identificador,
                    matricula = faturaSend.Matricula,
                    empresa = faturaSend.Empresa,
                    protocolo = ""
                };

                if (faturaSend.Origem == "APP")
                {
                    SCN5ISHCReceive sCN5ISHCReceive = (SCN5ISHCReceive)_clienteRule.SCN5ISHC(sCN5ISHCSend).Model;

                    foreach (SCN5ISHCReceiveDados item in sCN5ISHCReceive.ocorrencias)
                    {
                        if (!string.IsNullOrEmpty(item.valor))
                        {
                            sCN5ISHCReceive.ocorrencias.Remove(item);
                        }
                    }
                }
                else
                {
                    baseModelresp = _clienteRule.SCN5ISHC(sCN5ISHCSend);
                }
            }
            //contas pagas
            else if (faturaSend.SubTipo == "FAT18")
            {
                SCN6ISFPSend sCN6ISFPSend = new SCN6ISFPSend()
                {
                    identificador = faturaSend.Identificador,
                    matricula = faturaSend.Matricula,
                    mesAnoReferencia = "",
                    empresa = faturaSend.Empresa,
                    protocolo = ""
                };

                baseModelresp = _faturaRule.SCN6ISFP(sCN6ISFPSend);
            }
            //em débito (FAT49)
            else
            {
                SCN6ISFDSend sCN6ISFDSend = new SCN6ISFDSend()
                {
                    identificador = faturaSend.Identificador,
                    matricula = faturaSend.Matricula,
                    mesAnoPeriodo = "",
                    empresa = faturaSend.Empresa,
                    protocolo = ""
                };
                baseModelresp = _faturaRule.SCN6ISFD(sCN6ISFDSend, true);
            }

            //faturas pagas
            //--ATUALIZAÇÃO DO PROTOCOLO
            DAtualizaProtocoloSend dAtualiza = new DAtualizaProtocoloSend()
            {
                Titulo = faturaSend.Origem + " " + faturaSend.SubTipo,
                Empresa = faturaSend.Empresa,
                Servico = faturaSend.Servico,
                SubtipoId = faturaSend.SubTipo,
                PavimentacaoId = faturaSend.Pavimentacao,
                ProtocoloId = faturaSend.ProtocoloId,
                Identificador = faturaSend.Identificador,
                Matricula = faturaSend.Matricula,

                // Campos adicionados conforme descrição nos BUGS - 854 / 855 / 856 - Aguardando campos 
                //IdLocalidade = faturaSend.LocalidadeId,
                //IdBairro = faturaSend.BairroId,
                //IdLogradouro = faturaSend.LogradouroId,
                //NumeroImovel = faturaSend.NumeroImovel,
                //Descricao = faturaSend.DescricaoDaSolicitacao,
                //Referencia = faturaSend.Referencia,
                //TipoLogradouro = faturaSend.TipoLogradouro,
                //TipoComplemento = faturaSend.TipoComplemento,
                //Complemento = faturaSend.Complemento
            };
            AtualizaProtocolo(dAtualiza);
            //--
            return baseModelresp;
        }

        /// <summary>
        /// Historico de consumo anexado com o codigo de baeeas do faturas/emdebito
        /// </summary>
        /// <param name="faturaSend"></param>
        /// <returns></returns>
        public BaseResponse DHistoricoConsumoBarra(DFaturasSend faturaSend)
        {
            //consulta de débitos em aberto
            DUltimasFaturasReceive dUltimasFaturasReceive = new DUltimasFaturasReceive();

            Fatura fatura = new Fatura();
            List<Fatura> faturas = new List<Fatura>();
            //
            SCN6ISFDSend sCN6ISFDSend = new SCN6ISFDSend()
            {
                identificador = faturaSend.Identificador,
                matricula = faturaSend.Matricula,
                mesAnoPeriodo = "",
                empresa = faturaSend.Empresa,
                protocolo = ""
            };

            SCN6ISFDReceive sCN6ISFDReceive = (SCN6ISFDReceive)_faturaRule.SCN6ISFD(sCN6ISFDSend, true).Model;
            //

            SCN5ISHCSend sCN5ISHCSend = new SCN5ISHCSend()
            {
                identificador = faturaSend.Identificador,
                matricula = faturaSend.Matricula,
                empresa = faturaSend.Empresa,
                protocolo = ""
            };

            SCN5ISHCReceive sCN5ISHCReceive = (SCN5ISHCReceive)_clienteRule.SCN5ISHC(sCN5ISHCSend).Model;

            DHistoricoConsumoBarra dHistoricoConsumoBarra = new Model.Dyn365.DHistoricoConsumoBarra();
            DHistoricoConsumoBarraItem dHistoricoConsumoBarraItem = new DHistoricoConsumoBarraItem();

            if (sCN5ISHCReceive.ocorrencias.Any())
            {
                foreach (var itemlista in sCN5ISHCReceive.ocorrencias)
                {
                    dHistoricoConsumoBarraItem = new DHistoricoConsumoBarraItem();

                    dHistoricoConsumoBarraItem.dataLeitura = itemlista.dataLeitura;
                    dHistoricoConsumoBarraItem.dataPagamento = itemlista.dataPagamento;
                    dHistoricoConsumoBarraItem.dataVencimento = itemlista.dataVencimento;
                    dHistoricoConsumoBarraItem.leitura = itemlista.leitura;
                    dHistoricoConsumoBarraItem.mediaConsumo = itemlista.mediaConsumo;
                    dHistoricoConsumoBarraItem.referencia = itemlista.referencia;
                    dHistoricoConsumoBarraItem.valor = itemlista.valor;
                    dHistoricoConsumoBarraItem.volume = itemlista.volume;

                    var itembusca = sCN6ISFDReceive.faturas.Find(x => string.Format("{1}/{0}", x.referencia.Substring(0, 4), x.referencia.Substring(4, 2)) == itemlista.referencia);
                    //
                    if (itembusca != null)
                    {
                        dHistoricoConsumoBarraItem.CodigoBarra = ((SCN6ISFDReceiveFaturas)itembusca).numeroCodigoBarras;
                        dHistoricoConsumoBarraItem.CodigoBarraFormatado = ((SCN6ISFDReceiveFaturas)itembusca).numeroCodigoBarrasFormatado;
                    }
                    dHistoricoConsumoBarra.ocorrencias.Add(dHistoricoConsumoBarraItem);
                }
            }
            else
            {
                dHistoricoConsumoBarra.descricaoRetorno = sCN5ISHCReceive.descricaoRetorno;
            }

            try
            {
                //faturas pagas
                //--ATUALIZAÇÃO DO PROTOCOLO
                DAtualizaProtocoloSend dAtualiza = new DAtualizaProtocoloSend()
                {
                    Titulo = faturaSend.Origem + " " + faturaSend.SubTipo,
                    Empresa = faturaSend.Empresa,
                    Servico = faturaSend.Servico,
                    SubtipoId = faturaSend.SubTipo,
                    PavimentacaoId = faturaSend.Pavimentacao,
                    ProtocoloId = faturaSend.ProtocoloId,
                    Identificador = faturaSend.Identificador,
                    Matricula = faturaSend.Matricula
                };
                AtualizaProtocolo(dAtualiza);
                //--
            }
            catch { }

            var response = new BaseResponse();
            response.Model = dHistoricoConsumoBarra;
            //--
            return response;
        }

        /// <summary>
        /// Exibe as 12 ultimas faturas de um cliente
        /// </summary>
        /// <param name="faturaSend"></param>
        /// <returns></returns>
        public BaseResponse DUltimasFaturas(DFaturasSend faturaSend)
        {
            var response = new BaseResponse();

            //consulta de débitos em aberto
            DUltimasFaturasReceive dUltimasFaturasReceive = new DUltimasFaturasReceive();

            Fatura fatura = new Fatura();
            List<Fatura> faturas = new List<Fatura>();
            //
            SCN6ISFDSend sCN6ISFDSend = new SCN6ISFDSend()
            {
                identificador = faturaSend.Identificador,
                matricula = faturaSend.Matricula,
                mesAnoPeriodo = "",
                empresa = faturaSend.Empresa,
                protocolo = ""
            };

            SCN6ISFDReceive sCN6ISFDReceive = (SCN6ISFDReceive)_faturaRule.SCN6ISFD(sCN6ISFDSend, true).Model;

            dUltimasFaturasReceive.Identificador = faturaSend.Identificador;
            dUltimasFaturasReceive.Matricula = faturaSend.Matricula;
            //
            foreach (SCN6ISFDReceiveFaturas sCN6ISFDReceiveFaturas in sCN6ISFDReceive.faturas)
            {
                //peenchimento da lista com os débitos em aberto
                fatura = new Fatura()
                {
                    Referencia = string.Format("{1}/{0}", sCN6ISFDReceiveFaturas.referencia.Substring(0, 4), sCN6ISFDReceiveFaturas.referencia.Substring(4, 2)),
                    NumeroFatura = sCN6ISFDReceiveFaturas.numeroFatura,
                    ValorFatura = sCN6ISFDReceiveFaturas.valorFatura,
                    DataVencimento = Convert.ToDateTime(string.Format("{0}/{1}/{2}", sCN6ISFDReceiveFaturas.dataVencimento.Substring(0, 4), sCN6ISFDReceiveFaturas.dataVencimento.Substring(4, 2), sCN6ISFDReceiveFaturas.dataVencimento.Substring(6, 2))),
                    Emitida = sCN6ISFDReceiveFaturas.emitida,
                    TemCF20 = sCN6ISFDReceiveFaturas.temCF20,
                    NumeroCodigoBarrasFormatado = sCN6ISFDReceiveFaturas.numeroCodigoBarrasFormatado,
                    NumeroCodigoBarras = sCN6ISFDReceiveFaturas.numeroCodigoBarras,
                    CodigoBanco = "",
                    Retificada = "",
                    DataPagamento = ""
                };
                faturas.Add(fatura);
            }

            //consulta os débitos fechados
            SCN6ISFPSend sCN6ISFPSend = new SCN6ISFPSend()
            {
                identificador = faturaSend.Identificador,
                matricula = faturaSend.Matricula,
                mesAnoReferencia = "",
                empresa = faturaSend.Empresa,
                protocolo = ""
            };
            SCN6ISFPReceive sCN6ISFPReceive = (SCN6ISFPReceive)_faturaRule.SCN6ISFP(sCN6ISFPSend).Model;
            //

            foreach (SCN6ISFPReceiveContas sCN6ISFPReceiveContas in sCN6ISFPReceive.contas)
            {
                //peencher até completar 12 faturas
                if (faturas.Count < 12)
                {
                    fatura = new Fatura()
                    {
                        Referencia = sCN6ISFPReceiveContas.referencia,
                        NumeroFatura = sCN6ISFPReceiveContas.numFatura,
                        ValorFatura = sCN6ISFPReceiveContas.valorTotalFatura,
                        DataVencimento = Convert.ToDateTime(sCN6ISFPReceiveContas.dataVencimento),
                        DataPagamento = sCN6ISFPReceiveContas.dataPagamento,
                        CodigoBanco = sCN6ISFPReceiveContas.codigoBanco,
                        Retificada = sCN6ISFPReceiveContas.retificada,
                        TemCF20 = "",
                        Emitida = "S",
                        NumeroCodigoBarrasFormatado = "",
                        NumeroCodigoBarras = ""
                    };
                    faturas.Add(fatura);
                }
                else break;
            }
            dUltimasFaturasReceive.Faturas = faturas.OrderByDescending(x => x.DataVencimento).ToList();

            //faturas pagas
            //--ATUALIZAÇÃO DO PROTOCOLO
            DAtualizaProtocoloSend dAtualiza = new DAtualizaProtocoloSend()
            {
                Titulo = faturaSend.Origem + " " + faturaSend.SubTipo,
                Empresa = faturaSend.Empresa,
                Servico = faturaSend.Servico,
                SubtipoId = faturaSend.SubTipo,
                PavimentacaoId = faturaSend.Pavimentacao,
                ProtocoloId = faturaSend.ProtocoloId,
                Identificador = faturaSend.Identificador,
                Matricula = faturaSend.Matricula
            };
            AtualizaProtocolo(dAtualiza);

            if (!faturaSend.Matricula.IsNullOrEmpty() && !faturaSend.Identificador.IsNullOrEmpty())
            {
                dUltimasFaturasReceive.descricaoRetorno = sCN6ISFPReceive.descricaoRetorno;
            }
            else
            {
                dUltimasFaturasReceive.descricaoRetorno = "A operação não pode ser concluída. Por favor, tente novamente.";
            }

            response.Model = dUltimasFaturasReceive;

            return response;
        }

        /// <summary>
        /// retorna dados da certidão negativa
        /// </summary>
        /// <param name="dCertidaoNegativa"></param>
        /// <returns></returns>
        public BaseResponse CertidaoNegativa(DCertidaoNegativaSend dCertidaoNegativa)
        {
            var localidadeId = dCertidaoNegativa.LocalidadeId;

            if (dCertidaoNegativa.Origem == "APP")
            {
                BaseResponse dLocalidade = new BaseResponse();
                if (!localidadeId.IsNullOrEmpty())
                {
                    DLocalidadeNomeSend dLocalidadeNomeSend = new DLocalidadeNomeSend()
                    {
                        Empresa = dCertidaoNegativa.Empresa,
                        NomeLocalidade = localidadeId
                    };

                    dLocalidade = BuscaIdLocalidade(dLocalidadeNomeSend);

                    DLocalidadeReceive dados = (DLocalidadeReceive)dLocalidade.Model;
                    localidadeId = dados.Localidades.Select(s => s.LocalidadeId).FirstOrDefault();
                };
            }

            //cria e atualiza o procotolo
            GeraProtocoloSend geraProtocoloSend = new GeraProtocoloSend()
            {
                CpfCnpjLogin = dCertidaoNegativa.CpfCnpjLogin,
                IdCpfCnpj = dCertidaoNegativa.IdCpfCnpj,
                TipoProtocolo = "5",
                Empresa = dCertidaoNegativa.Empresa,
                Origem = dCertidaoNegativa.Origem,
                Pavimentacao = dCertidaoNegativa.Pavimentacao,
                Servico = dCertidaoNegativa.Servico,
                Subtipo = dCertidaoNegativa.Subtipo,
                // Campos adicionados conforme descrição nos BUGS - 857 contemplando também BUG 858
                LocalidadeId = localidadeId,
                copasa_referenciaentreruas = dCertidaoNegativa.Referencia,
                DescricaoSolicitacao = dCertidaoNegativa.DescricaoDaSolicitacao,
                Titulo = "Certidao Negativa de Débito " + dCertidaoNegativa.Origem + " " + DateTime.Today.ToString()

            };
            GeraProtocolo(geraProtocoloSend);

            //--
            SCN6ISCNSend sCN6ISCNSend = new SCN6ISCNSend() { cpfCnpj = dCertidaoNegativa.CpfCnpjLogin, empresa = dCertidaoNegativa.Empresa };
            return _certidaoNegativaDebitoRule.SCN6ISCN(sCN6ISCNSend);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="geraProtocoloSend"></param>
        /// <returns></returns>
        public BaseResponse GeraProtocolo(GeraProtocoloSend geraProtocoloSend)
        {
            //CRIAR PROTOCOLO
            DCriaProtocoloSend criaProtocoloSend = new DCriaProtocoloSend()
            {
                CpfCnpj = geraProtocoloSend.CpfCnpjLogin,
                IdCpfCnpj = geraProtocoloSend.IdCpfCnpj,
                TipoProtocolo = geraProtocoloSend.TipoProtocolo,
                Empresa = geraProtocoloSend.Empresa,
                Origem = geraProtocoloSend.Origem,
                Referencia = (!string.IsNullOrEmpty(geraProtocoloSend.copasa_referenciaentreruas) ? geraProtocoloSend.copasa_referenciaentreruas : "Sem Referencia"),

                // Campos adicionados conforme descrição nos BUGS - 857 contemplando também BUG 858
                LocalidadeId = geraProtocoloSend.LocalidadeId,
                DescricaoDaSolicitacao = geraProtocoloSend.DescricaoSolicitacao

            };
            DCriaProtocoloReceive dCriaProtocoloReceive = ((DCriaProtocoloReceive)CriaProtocolo(criaProtocoloSend).Model);
            //--
            //faturas pagas
            //--ATUALIZAÇÃO DO PROTOCOLO
            DAtualizaProtocoloSend dAtualiza = new DAtualizaProtocoloSend()
            {
                Titulo = geraProtocoloSend.Subtipo + " APP - Copasa Digital",
                Empresa = geraProtocoloSend.Empresa,
                Servico = geraProtocoloSend.Servico,
                SubtipoId = geraProtocoloSend.Subtipo,
                PavimentacaoId = geraProtocoloSend.Pavimentacao,
                ProtocoloId = dCriaProtocoloReceive.ProtocoloId,
            };
            AtualizaProtocolo(dAtualiza);


            var response = new BaseResponse();
            response.Model = dCriaProtocoloReceive;
            //--
            return response;
        }

        /// <summary>
        /// solicita um servico de abertura ou cancelamento de falta de água de acordo com a situração da matricula
        /// </summary>
        /// <param name="verificaFaltaDaguaSend"></param>
        /// <returns></returns>
        public BaseResponse VerificaFaltaDagua(VerificaFaltaDaguaSend verificaFaltaDaguaSend)
        {
            TrabPesquisaFaltaAguaSend situacaoMatriculaSend = new TrabPesquisaFaltaAguaSend()
            {
                empresa = "",
                protocolo = "",
                matricula = verificaFaltaDaguaSend.Matricula,
                identificador = verificaFaltaDaguaSend.Identificador
            };

            TrabPesquisaFaltaAguaReceive situacaoMatriculaReceive = (TrabPesquisaFaltaAguaReceive)_clienteRule.getSituacaoMatriculas(situacaoMatriculaSend).Model;
            string codigoservico = "";
            if (!string.IsNullOrEmpty(situacaoMatriculaReceive.descricaoSituacao))
            {
                //caso tenha informações no descricaoSituacao, cancelar o serviço de falta de água
                codigoservico = "1500200";

            }
            else
            {
                codigoservico = "1500100";
            }
            //chamada da função que solicita o serviço
            SCN4CRSSSend sCN4CRSSSend = new SCN4CRSSSend()
            {
                matricula = verificaFaltaDaguaSend.Matricula,
                nomeUsuario = "APP",
                codigoLocalidade = "",
                codigoLogradouro = "",
                numeroLogradouro = "",
                complementoLogradouro = "",
                codigoBairro = "",
                nomeSolicitante = verificaFaltaDaguaSend.NomeSolicitante,
                telefoneSolicitante = verificaFaltaDaguaSend.TelefoneSolicitante,
                referenciaEndereco = verificaFaltaDaguaSend.NomeLogradouro,
                observacao = "",
                empresa = "COPASA",
                numeroProtocolo = verificaFaltaDaguaSend.NumeroProtocolo,
                codigoServicoSolicitado = codigoservico,
                protocolo = "",
                tipoComplementoLogradouro = "",
                codigoUsuario = "",
                emergenciaRisco = "",
                justificativaEmergenciaRisco = "",
                agenciaUsuario = "",
                tipoLogradouro = ""
            };

            Dyn365SomenteMensagem dyn365SomenteMensagem = (Dyn365SomenteMensagem)_servicoOperacionalRule.SCN4CRSS(sCN4CRSSSend).Model;

            return new BaseResponse
            {
                Model = dyn365SomenteMensagem
            };

        }

        /// <summary>
        /// solicita serviço ne rua(endereço)/imóvel(identificador)
        /// </summary>
        /// <param name="dSolicitaServicoSend"></param>
        /// <returns></returns>
        public BaseResponse SolicitaServico(DSolicitaServicoSend dSolicitaServicoSend)
        {
            // (vazamento agua(14800) / esgoto(31500))/ falta de agua(15001)
            string TipoLogradouro = "";
            DAtualizaProtocoloSend dAtualiza = new DAtualizaProtocoloSend();

            #region cria e atualiza o protocolo
            if (string.IsNullOrEmpty(dSolicitaServicoSend.Matricula))
            {
                if (dSolicitaServicoSend.Origem != Model.Enumerador.TipoOrigem.APP)
                {
                    if (!string.IsNullOrEmpty(dSolicitaServicoSend.CpfCnpj))
                    {
                        //verifica se CPF ou CNPJ existe no D365, se não existir cadastra
                        dSolicitaServicoSend.IdCpfCnpj = VerificaCadastroCpfCnpj(dSolicitaServicoSend.CpfCnpj, dSolicitaServicoSend.NomeSolicitante, dSolicitaServicoSend.TelefoneSolicitante, "");
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(dSolicitaServicoSend.CpfCnpj))
                    {
                        //Caso CNPJ estiver vazio é utilizado CNPJ da Copasa para gerar protocolo
                        //para o serviço Vazamento na Rua
                        dSolicitaServicoSend.CpfCnpj = "17281106000103";

                        //verifica se CPF ou CNPJ existe no D365, se não existir cadastra
                        dSolicitaServicoSend.IdCpfCnpj = VerificaCadastroCpfCnpj(dSolicitaServicoSend.CpfCnpj, dSolicitaServicoSend.NomeSolicitante, dSolicitaServicoSend.TelefoneSolicitante, "");

                    }
                }
                //CRIAR PROTOCOLO
                if (string.IsNullOrEmpty(dSolicitaServicoSend.IdProtocolo))
                {
                    DCriaProtocoloSend criaProtocoloSend = new DCriaProtocoloSend()
                    {
                        CpfCnpj = dSolicitaServicoSend.CpfCnpj,
                        IdCpfCnpj = dSolicitaServicoSend.IdCpfCnpj,
                        TipoProtocolo = "1",
                        Empresa = dSolicitaServicoSend.Empresa,
                        Origem = dSolicitaServicoSend.Origem,
                        Nome = dSolicitaServicoSend.NomeSolicitante,
                        Telefone1 = dSolicitaServicoSend.TelefoneSolicitante,
                        Telefone2 = ""
                    };

                    DCriaProtocoloReceive dCriaProtocoloReceive = (DCriaProtocoloReceive)CriaProtocolo(criaProtocoloSend).Model;
                    if (dCriaProtocoloReceive != null)
                    {
                        dSolicitaServicoSend.NumeroProtocolo = dCriaProtocoloReceive.Protocolo;
                        dSolicitaServicoSend.IdProtocolo = dCriaProtocoloReceive.ProtocoloId;
                        dSolicitaServicoSend.Identificador = "";
                    }
                }
                //atualização do endereço no protocolo
                dAtualiza.IdLocalidade = $"/copasa_localidades({dSolicitaServicoSend.IdLocalidade})";
                dAtualiza.IdBairro = $"/copasa_bairros({dSolicitaServicoSend.IdBairro})";
                dAtualiza.IdLogradouro = $"/copasa_logradouros({dSolicitaServicoSend.IdLogradouro})";
            }
            else
            {
                dSolicitaServicoSend.CodigoLocalidade = dSolicitaServicoSend.CodigoLogradouro = dSolicitaServicoSend.NumeroLogradouro =
                dSolicitaServicoSend.Complemento = dSolicitaServicoSend.CodigoBairro = "";

                dAtualiza.Matricula = dSolicitaServicoSend.Matricula;
                TipoLogradouro = "R";
            }

            //obj da atualização de protocolo
            //dAtualiza.Titulo = dSolicitaServicoSend.Origem + " " + dSolicitaServicoSend.Subtipo;
            dAtualiza.Titulo = dSolicitaServicoSend.Subtipo + " - APP Copasa Digital";
            dAtualiza.Empresa = dSolicitaServicoSend.Empresa;
            dAtualiza.Servico = dSolicitaServicoSend.Servico;
            dAtualiza.SubtipoId = dSolicitaServicoSend.Subtipo;
            dAtualiza.PavimentacaoId = dSolicitaServicoSend.Pavimentacao;
            dAtualiza.ProtocoloId = dSolicitaServicoSend.IdProtocolo;
            dAtualiza.Identificador = dSolicitaServicoSend.Identificador;
            dAtualiza.Matricula = dSolicitaServicoSend.Matricula;
            dAtualiza.Referencia = string.IsNullOrEmpty(dSolicitaServicoSend.Referencia) ? "Sem referência" : dSolicitaServicoSend.Referencia;
            dAtualiza.Descricao = string.IsNullOrEmpty(dSolicitaServicoSend.Observacao) ? dSolicitaServicoSend.Servico : dSolicitaServicoSend.Observacao; //Observacao enviando para descricao
            dAtualiza.Complemento = dSolicitaServicoSend.Complemento;
            dAtualiza.TipoComplemento = dSolicitaServicoSend.TipoComplemento;

            AtualizaProtocolo(dAtualiza);

            #endregion

            #region Registra imagem do vazamento associando ao IdProtocolo 

            if (!dSolicitaServicoSend.IdProtocolo.IsNullOrEmpty() && !dSolicitaServicoSend.ImagemBase64.IsNullOrEmpty())
            {
                DImagemVazamentoSend dImagemVazamento = new DImagemVazamentoSend()
                {
                    TipoVazamento = dSolicitaServicoSend.TipoVazamento,
                    IdIncident = "/incidents(" + dSolicitaServicoSend.IdProtocolo + ")",
                    NomeArquivo = dSolicitaServicoSend.NomeArquivo,
                    ImgBase64 = dSolicitaServicoSend.ImagemBase64
                };

                RegistrarImagemVazamento(dImagemVazamento);
            }

            #endregion

            #region Efetua tratamento para número do logradouro

            //Tratamento para retirar letras e caracteres especiais do número
            //Caso número estiver entre faixas (123 a 125) será considerado o primeiro número
            string numeroLogradouroFormatado = dSolicitaServicoSend.NumeroLogradouro;
            int num = 0;
            if (int.TryParse(numeroLogradouroFormatado, out num) == false &&
                !dSolicitaServicoSend.NumeroLogradouro.IsNullOrEmpty())
                numeroLogradouroFormatado = ObterNumeroSemLetrasCaracteresEspeciais(dSolicitaServicoSend.NumeroLogradouro);

            //Caso número estiver vazio será atribuído o valor 1
            if (numeroLogradouroFormatado.IsNullOrEmpty())
                numeroLogradouroFormatado = "1";

            #endregion

            //chamada da função que solicita o serviço
            SCN4CRSSSend sCN4CRSSSend = new SCN4CRSSSend()
            {
                matricula = dSolicitaServicoSend.Matricula,
                nomeUsuario = dSolicitaServicoSend.Origem,
                codigoLocalidade = dSolicitaServicoSend.CodigoLocalidade,
                codigoLogradouro = dSolicitaServicoSend.CodigoLogradouro,
                numeroLogradouro = numeroLogradouroFormatado,
                complementoLogradouro = dSolicitaServicoSend.Complemento,
                codigoBairro = dSolicitaServicoSend.CodigoBairro,
                nomeSolicitante = dSolicitaServicoSend.NomeSolicitante,
                telefoneSolicitante = dSolicitaServicoSend.TelefoneSolicitante,
                referenciaEndereco = dSolicitaServicoSend.Referencia,
                observacao = dSolicitaServicoSend.Observacao,
                empresa = dSolicitaServicoSend.Empresa,
                numeroProtocolo = dSolicitaServicoSend.NumeroProtocolo,
                codigoServicoSolicitado = dSolicitaServicoSend.Subtipo + dSolicitaServicoSend.Pavimentacao,
                protocolo = "",
                tipoComplementoLogradouro = "",
                codigoUsuario = "APP",
                emergenciaRisco = "",
                justificativaEmergenciaRisco = "",
                agenciaUsuario = "",
                tipoLogradouro = TipoLogradouro,
                observacaoSicom = new string[0]
            };

            Dyn365SomenteMensagem dyn365SomenteMensagem = (Dyn365SomenteMensagem)_servicoOperacionalRule.SCN4CRSS(sCN4CRSSSend).Model;

            return new BaseResponse
            {
                Model = dyn365SomenteMensagem
            };
        }

        /// <summary>
        /// Método para retirar letras/caracteres do número do logradouro
        /// </summary>
        /// <param name="numeroLogradouro"></param>
        /// <returns></returns>
        public static string ObterNumeroSemLetrasCaracteresEspeciais(string numeroLogradouro)
        {
            string numeroFormatado = numeroLogradouro.Trim().RemoveAccents().ToUpper();
            string[] letras = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            //Troca letras da string por " "
            for (int i = 0; i < letras.Length; i++)
            {
                numeroFormatado = numeroFormatado.Replace(letras[i], " ");
            }

            //Troca os caracteres especiais da string por " "
            string[] caracteresEspeciais = { "¹", "²", "³", "£", "¢", "¬", "º", "¨", "\"", "'", ".", ",", "-", ":", "(", ")", "ª", "|", "\\\\", "°", "_", "@", "#", "!", "$", "%", "&", "*", ";", "/", "<", ">", "?", "[", "]", "{", "}", "=", "+", "§", "´", "`", "^", "~" };

            for (int i = 0; i < caracteresEspeciais.Length; i++)
            {
                numeroFormatado = numeroFormatado.Replace(caracteresEspeciais[i], " ");
            }

            //Troca os caracteres especiais por " "
            numeroFormatado = Regex.Replace(numeroFormatado, @"[^\w\.@-]", " ",
                                RegexOptions.None, TimeSpan.FromSeconds(1.5));

            var numeros = numeroFormatado.Split(' ');
            if (numeros.Count() > 1)
            {
                int num = 0;
                foreach (String item in numeros)
                {
                    if (int.TryParse(item, out num) == true)
                    {
                        numeroFormatado = item;
                        break;
                    }
                }
            }

            return numeroFormatado;
        }

        /// <summary>
        /// Cadastro e validação de emails para o envio de contas por email
        /// </summary>
        /// <param name="dContasEmailSend"></param>
        /// <returns></returns>
        /// <remarks>função chamada pela função home/confirmaçãoEmail</remarks>
        public BaseResponse ContasPorEmail(DContasEmailSend dContasEmailSend)
        {
            //lista dos que sao enviados pelo email;
            StringBuilder protocoloMatriculas = new StringBuilder();

            #region processo de cancelamento de status de email

            List<SCN6ISCESendMatricula> listamatriculas = new List<SCN6ISCESendMatricula>();
            SCN6ISCESendMatricula sCN6ISCESendMatricula;
            string mensagemretorno = "";

            //geração da lista de cancelamentos
            foreach (EmailMatriculaItem emailitem in dContasEmailSend.EmailsMatriculas)
            {
                foreach (MatriculaItem matricula in emailitem.Matriculas)
                {
                    if (matricula.Status == "N")
                    {
                        sCN6ISCESendMatricula = new SCN6ISCESendMatricula() { matricula = matricula.Matricula, status = "N", email = emailitem.Email };
                        listamatriculas.Add(sCN6ISCESendMatricula);
                    }
                }
            }

            //cancelar status dos emails
            if (listamatriculas.Count > 0)
            {
                SCN6ISCESend sCN6ISCESend = new SCN6ISCESend();
                sCN6ISCESend.matriculas = listamatriculas.ToArray();
                SCN6ISCEReceive sCN6ISCEReceive = (SCN6ISCEReceive)_clienteRule.SCN6ISCE(sCN6ISCESend).Model;

                mensagemretorno = "Cancelamentos efetuados";
            }
            #endregion

            //monta a lista de matriculas para registro para a atualização de protocolos
            foreach (EmailMatriculaItem emailitem in dContasEmailSend.EmailsMatriculas)
            {
                foreach (MatriculaItem matriculaitem in emailitem.Matriculas)
                {
                    if (matriculaitem.Status == "R")
                    {
                        protocoloMatriculas.Append(matriculaitem.Matricula + "|");
                    }
                }
            }

            if (!string.IsNullOrEmpty(protocoloMatriculas.ToString()))
            {
                //--ATUALIZAÇÃO DO PROTOCOLO
                DAtualizaProtocoloSend dAtualiza = new DAtualizaProtocoloSend()
                {
                    Titulo = dContasEmailSend.Origem + " " + dContasEmailSend.SubTipo,
                    Empresa = dContasEmailSend.Empresa,
                    Servico = dContasEmailSend.Servico,
                    SubtipoId = dContasEmailSend.SubTipo,
                    PavimentacaoId = dContasEmailSend.Pavimentacao,
                    ProtocoloId = dContasEmailSend.IdProtocolo,
                    Descricao = protocoloMatriculas.ToString()
                };

                //atualiza o protocolo, em caso de sucesso, envia os emails para os emails no json
                if (AtualizaProtocolo(dAtualiza))
                {
                    //--disparo dos emails
                    StringBuilder corpoEmail = new StringBuilder();
                    Copasa.Atende.Util.EnviaEmail enviaEmail = new Util.EnviaEmail("servico.comunicacao@copasa.com.br", "em@il2279", "smtp.copasa.com.br", 587);//zm-mta.copasa.com.br"
                                                                                                                                                                //caso tenha algum tipo R para validar email
                    bool enviaemail = false;

                    foreach (EmailMatriculaItem matriculaItem in dContasEmailSend.EmailsMatriculas)
                    {
                        //verifica se neste email tem alguma matricula com R.
                        enviaemail = false;
                        foreach (var item in matriculaItem.Matriculas)
                        {
                            if (item.Status == "R")
                            {
                                enviaemail = true;
                                break;
                            }
                        }
                        //envio de email de validação
                        if (enviaemail)
                        {
                            corpoEmail = new StringBuilder();

                            //origens dos ambientes
                            //teste local
                            //corpoEmail.Append($"<a href = 'http://localhost:50756/Home/ConfirmacaoEmail?Protocolo={dContasEmailSend.IdProtocolo}&Email={matriculaItem.Email}'>Confirmação de Email</a><br/>");
                            //ambiente copasa
                            corpoEmail.Append($"<p><img src='https://www.copasa.com.br/servicos/WebServiceAPI/Hml/CopasaAtende/Imagens/Cabecalho_Email_Copasa_Atende.png'></p>" +
                                $"<p><a href = 'https://www.copasa.com.br/servicos/WebServiceAPI/Hml/CopasaAtende/Home/ConfirmacaoEmail?Protocolo={dContasEmailSend.IdProtocolo}&Email={matriculaItem.Email}'>Clique para confirmar seu email</a></p><br/>");
                            var mensagem = enviaEmail.CreateMessage("Email de Confirmação", corpoEmail.ToString(), "servico.comunicacao@copasa.com.br", matriculaItem.Email, null);
                            enviaEmail.Send(mensagem);
                        }
                    }
                    //
                    if (enviaemail)
                    {
                        if (!string.IsNullOrEmpty(mensagemretorno))
                        {
                            mensagemretorno += " e emails de confirmação enviados.";
                        }
                        else
                            mensagemretorno = "Emails de confirmação enviados.";
                    }
                }
                else
                {
                    mensagemretorno = "Erro ao tentar atualizar o protocolo";
                }
            }
            return new BaseResponse() { IsValid = true, Model = new DMensagemRetornoReceive() { Mensagem = mensagemretorno } };
        }

        /// <summary>
        /// Envio de Fatura por email
        /// </summary>
        /// <param name="dEnviaFaturaEmailSend"></param>
        /// <returns></returns>
        public BaseResponse EnviaFaturaEmail(DEnviaFaturaEmailSend dEnviaFaturaEmailSend)
        {
            string mensagemretorno = "";
            //
            try
            {
                //--disparo dos emails
                StringBuilder corpoEmail = new StringBuilder();
                Copasa.Atende.Util.EnviaEmail enviaEmail = new Util.EnviaEmail("servico.comunicacao@copasa.com.br", "em@il2279", "smtp.copasa.com.br", 587);//zm-mta.copasa.com.br"

                WebResponse responseFacade = _faturaRule.retornaFaturaPDF(dEnviaFaturaEmailSend.NumeroFatura);
                Stream retorno = responseFacade.GetResponseStream();

                corpoEmail = new StringBuilder();

                var mediatype = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
                var attach = new Attachment(retorno, mediatype);
                attach.ContentDisposition.FileName = "Fatura.pdf";

                //origens dos ambientes
                //teste local
                //corpoEmail.Append($"<a href = 'http://localhost:50756/Home/ConfirmacaoEmail?Protocolo={dContasEmailSend.IdProtocolo}&Email={matriculaItem.Email}'>Confirmação de Email</a><br/>");
                //ambiente copasa
                corpoEmail.Append($"<p><img src='https://www.copasa.com.br/servicos/WebServiceAPI/Hml/CopasaAtende/Imagens/Cabecalho_Email_Copasa_Atende.png'></p>" +
                    $"<p>Segue em anexo a segunda via de sua fatura</p><br/>");
                var mensagem = enviaEmail.CreateMessage("Segunda Via de Fatura", corpoEmail.ToString(), "servico.comunicacao@copasa.com.br", dEnviaFaturaEmailSend.Email, attach);

                enviaEmail.Send(mensagem);
                mensagemretorno = "Email enviado";
            }
            catch { mensagemretorno = "Erro ao tentar enviar o email."; }

            return new BaseResponse() { IsValid = true, Model = new DMensagemRetornoReceive() { Mensagem = mensagemretorno } };
        }

        ///// <summary>
        ///// Consulta dados de um usuário
        ///// </summary>
        ///// <param name="dValidaCpfCnpjSend"></param>
        ///// <returns></returns>
        //public BaseResponse ConsultaUsuario(DValidaCpfCnpjSend dValidaCpfCnpjSend)
        //{
        //    string condicao = $"$filter=copasa_cpf_cnpj eq '{InsereMascaraCpfCnpj(dValidaCpfCnpjSend.CpfCnpj)}'";
        //    dRepository = new DRepository("contacts", "Dyn365Host");
        //    //
        //    return new BaseResponse() { Model = ((DConsultaUsuarioReceive)dRepository.DPesquisarObjeto(condicao, typeof(DConsultaUsuarioReceive))) };
        //}

        /// <summary>
        /// Consulta dados de um usuário
        /// </summary>
        /// <param name="dValidaCpfCnpjSend"></param>
        /// <returns></returns>
        public BaseResponse ConsultaUsuario(DValidaCpfCnpjSend dValidaCpfCnpjSend)
        {
            string condicao = $"$filter=copasa_cpf_cnpj eq '{InsereMascaraCpfCnpj(dValidaCpfCnpjSend.CpfCnpj)}'";
            dRepository = new DRepository("contacts", "Dyn365Host");
            //
            BaseResponse response = new BaseResponse() { Model = ((DConsultaUsuarioReceive)dRepository.DPesquisarObjeto(condicao, typeof(DConsultaUsuarioReceive))) };

            if (response.Model == null)
            {
                var cliente = (DConsultaUsuarioReceive)ConsultaUsuarioDigital(dValidaCpfCnpjSend.CpfCnpj);
                response.Model = cliente;
            }

            DConsultaUsuarioReceive dados = (DConsultaUsuarioReceive)response.Model;

            dados.locality = string.IsNullOrEmpty(dados.locality) ? null : dados.locality;
            dados.neighborhood = string.IsNullOrEmpty(dados.locality) ? null : dados.neighborhood;
            dados.copasa_termoaceite = string.IsNullOrEmpty(dados.copasa_termoaceite) ? "0" : dados.copasa_termoaceite;
            dados.copasa_politicaprivacidade = string.IsNullOrEmpty(dados.copasa_politicaprivacidade) ? "0" : dados.copasa_politicaprivacidade;
            dados.donotemail = string.IsNullOrEmpty(dados.donotemail) ? "false" : dados.donotemail;
            dados.copasa_tipocliente = string.IsNullOrEmpty(dados.copasa_tipocliente) ? "176410000" : dados.copasa_tipocliente;
            dados.copasa_validacaoemail = string.IsNullOrEmpty(dados.copasa_validacaoemail) ? "176410000" : dados.copasa_validacaoemail;
            dados.phonetype = string.IsNullOrEmpty(dados.phonetype) ? "176410000" : dados.phonetype;

            response.Model = dados;
            return response;
        }

        /// <summary>
        /// Consulta Usuário no CopasaDigital e devolve um objeto preparado para ser gravado como usuário do Dynamics
        /// </summary>
        /// <param name="loginUsuario"></param>
        /// <returns></returns>
        public BaseModel ConsultaUsuarioDigital(string loginUsuario)
        {

            try
            {
                var cpfCnpj = Convert.ToInt64(loginUsuario);
                var cliente = RepositoryFactory.UnitOfWork.ClienteRepository.Get(x => x.CpfCnpj.Equals(cpfCnpj));
                var retorno = new DConsultaUsuarioReceive();
                if (cliente != null)
                {
                    #region Preenche dados retorno DConsultaUsuarioReceive     
                    retorno.copasa_politicaprivacidade = cliente.FlagPoliticaPrivacidade.ToString() == "True" ? "1" : "0";
                    retorno.copasa_termoaceite = cliente.FlagTermoAceite.ToString() == "True" ? "1" : "2";
                    switch (cliente.TipoCliente)
                    {
                        case "F":
                            retorno.copasa_tipocliente = "176410000"; // Pessoa Física
                            break;
                        case "J":
                            retorno.copasa_tipocliente = "176410001"; // Pessoa Jurídica
                            break;
                        default:
                            break;
                    }
                    switch (cliente.StatusEmail)
                    {
                        case "S":
                            retorno.copasa_validacaoemail = "176410000";
                            break;
                        case "N":
                            retorno.copasa_validacaoemail = "176410001";
                            break;
                        case "B":
                            retorno.copasa_validacaoemail = "176410002";
                            break;
                        case "R":
                            retorno.copasa_validacaoemail = "176410003";
                            break;
                        case "C":
                            retorno.copasa_validacaoemail = "176410004";
                            break;
                        default:
                            break;
                    }

                    retorno.cpfcnpj = InsereMascaraCpfCnpj(loginUsuario);
                    retorno.username = loginUsuario;
                    retorno.emailaddress1 = cliente.Email;
                    retorno.entityimage = (cliente.ImagemPerfil != null) ? cliente.ImagemPerfil.ByteArrayToBase64() : null;
                    retorno.firstname = cliente.Nome.Split(' ').First();
                    retorno.lastname = cliente.Nome.Substring(retorno.firstname.Length).TrimStart(' ');
                    List<TelefoneModel> listaTelefones = new List<TelefoneModel>();

                    foreach (var telefone in cliente.Telefones)
                    {
                        switch (telefone.IdTipoTelefone)
                        {
                            case TipoTelefoneEnum.CELULAR:
                                retorno.mobilephone = telefone.NumeroTelefone.ToString();
                                switch (telefone.IdComunicacao)
                                {

                                    case 0://nao utiliza
                                        retorno.phonetype = "176410003";
                                        break;
                                    case 1: //whatsapp
                                        retorno.phonetype = "176410000";
                                        break;
                                    case 2://telegram
                                        retorno.phonetype = "176410001";
                                        break;
                                    case 3://viber
                                        retorno.phonetype = "176410002";
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case TipoTelefoneEnum.COMERCIAL:
                                retorno.telephone1 = telefone.NumeroTelefone.ToString();
                                break;
                            case TipoTelefoneEnum.RESIDENCIAL:
                                retorno.telephone2 = telefone.NumeroTelefone.ToString();
                                break;
                            default:
                                break;
                        }
                    }
                    retorno.locality = null;
                    retorno.neighborhood = null;
                    #endregion
                    return retorno;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
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

        /// <summary>
        /// Altera o status para envio de contas por email.
        /// </summary>
        /// <returns></returns>
        public BaseResponse ConfirmaContasEmail(DConfirmaEmailSend dConfirmaEmailSend)
        {
            //consulta a lista de matriculas pelo id do protocolo
            string condicao = $"$select=description&$filter=incidentid eq '{dConfirmaEmailSend.ProtocoloId}'";
            dRepository = new DRepository("incidents", "Dyn365Host");
            string descricao = ((DConsultaProtocoloReceive)dRepository.DPesquisarObjeto(condicao, typeof(DConsultaProtocoloReceive))).Descricao;

            SCN6ISCESend sCN6ISCESend = new SCN6ISCESend();
            List<SCN6ISCESendMatricula> listamatriculas = new List<SCN6ISCESendMatricula>();
            SCN6ISCESendMatricula sCN6ISCESendMatricula;
            //divide a string de descrição, separando as matriculas que sofrerão o update e cria o obtejo para o ws sCN6ISCE
            foreach (string matricula in descricao.Split('|'))
            {
                if (!string.IsNullOrEmpty(matricula))
                {
                    sCN6ISCESendMatricula = new SCN6ISCESendMatricula() { matricula = matricula, status = "R", email = dConfirmaEmailSend.Email };
                    listamatriculas.Add(sCN6ISCESendMatricula);
                }
            }
            sCN6ISCESend.matriculas = listamatriculas.ToArray();

            SCN6ISCEReceive sCN6ISCEReceive = (SCN6ISCEReceive)_clienteRule.SCN6ISCE(sCN6ISCESend).Model;

            return new BaseResponse() { IsValid = true, Model = new DMensagemRetornoReceive() { Mensagem = sCN6ISCEReceive.descricaoRetornoSicom } };
        }

        /// <summary>
        /// Atualiza e-mail e telefones do usuário Copasa.
        /// </summary>
        /// <param name="usuarioCopasa">Objeto de Entrada.</param>
        /// <param name="ambiente">Ambiemte.</param>
        /// <returns></returns>
        public BaseResponse AtualizacaoCadastral(UsuarioCopasaModel usuarioCopasa, EnvironmentEnum ambiente)
        {
            try
            {
                var baseResponse = new BaseResponse();
                string environment = string.Empty;

                if (ambiente == EnvironmentEnum.H)
                {
                    environment = "hml/";
                }
                else
                {
                    environment = string.Empty;
                }

                //var logServico = new LogServicoComercialModel()
                //{
                //    DataAcesso = DateTime.Now,
                //    UrlAcesso = string.Format("api/" + environment + "usuario/atualizacaocadastral/{0}/{1}", loginUsuario, origem),
                //    Identificador = usuarioCopasa.Identificador,
                //    StatusAcesso = StatusEnum.S,
                //    Operacao = TipoOperacaoEnum.UPDATE,
                //    TipoServico = TipoServicoEnum.ATUALIZACAO_CADASTRAL,
                //    ClienteCpfCnpj = loginUsuario.ToLong(),
                //    Origem = origem.ToUpper()
                //};

                //RuleFactory.LogServicoComercialRule.GravarLog(logServico);

                var usuario = UsuarioCopasaRepository.AtualizacaoCadastral(usuarioCopasa, ambiente);

                baseResponse.Model = usuario;

                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse(ex);
            }
        }


        #region servicos de localidade
        /// <summary>
        /// busca dados de uma localidade
        /// </summary>
        /// <returns></returns>
        public BaseResponse BuscaIdLocalidade(DLocalidadeNomeSend dLocalidadeNomeSend)
        {
            if (dLocalidadeNomeSend.Empresa == "COPASA")
            {
                dLocalidadeNomeSend.Empresa = "CSMG";
            }
            else
            {
                dLocalidadeNomeSend.Empresa = "CNOR";
            }

            string condicao = $"$select=copasa_name,copasa_codigo,copasa_localidadeid&$filter=statuscode eq 1 and copasa_operaagua eq '{dLocalidadeNomeSend.Empresa}' and copasa_operaesgoto eq '{dLocalidadeNomeSend.Empresa}' and copasa_name eq '{dLocalidadeNomeSend.NomeLocalidade}'  ";
            dRepository = new DRepository("copasa_localidades", "Dyn365Host");
            DLocalidadeReceive dLocalidadeReceive = new DLocalidadeReceive() { Localidades = dRepository.DPesquisarLista(condicao, typeof(DLocalidade)).Cast<DLocalidade>().OrderBy(x => x.LocalidadeNome).ToList() };
            return new BaseResponse { Model = dLocalidadeReceive };
        }

        /// <summary>
        /// busca as localidades ativas do sistema de uma determinada empresa
        /// </summary>
        /// <returns></returns>
        public BaseResponse BuscaLocalidade(DLocalidadeSend dLocalidadeSend)
        {
            if (dLocalidadeSend.Empresa == "COPASA")
            {
                dLocalidadeSend.Empresa = "CSMG";
            }
            else
            {
                dLocalidadeSend.Empresa = "CNOR";
            }

            string condicao = $"$select=copasa_name,copasa_codigo,copasa_localidadeid&$filter=statuscode eq 1 and copasa_operaagua eq '{dLocalidadeSend.Empresa}' and copasa_operaesgoto eq '{dLocalidadeSend.Empresa}'";
            dRepository = new DRepository("copasa_localidades", "Dyn365Host");
            DLocalidadeReceive dLocalidadeReceive = new DLocalidadeReceive() { Localidades = dRepository.DPesquisarLista(condicao, typeof(DLocalidade)).Cast<DLocalidade>().OrderBy(x => x.LocalidadeNome).ToList() };
            return new BaseResponse { Model = dLocalidadeReceive };
        }

        /// <summary>
        /// busca os bairros de uma localidade
        /// </summary>
        /// <returns></returns>
        public BaseResponse BuscaBairro(DBairroSend dBairroSend)
        {
            string condicao = $"$select=copasa_name,copasa_codigosicom,copasa_bairroid&$filter=_copasa_localidadeid_value  eq '{dBairroSend.IdLocalidade}' and statuscode eq 1";
            dRepository = new DRepository("copasa_bairros", "Dyn365Host");
            DBairroReceive dBairroReceive = new DBairroReceive() { Bairros = dRepository.DPesquisarLista(condicao, typeof(DBairro)).Cast<DBairro>().OrderBy(x => x.BairroNome).ToList() };
            return new BaseResponse { Model = dBairroReceive };
        }

        /// <summary>
        /// busca os logradouros ativos de um bairro
        /// </summary>
        /// <returns></returns>
        public BaseResponse BuscaLogradouro(DLogradouroSend dLogradouroSend)
        {
            string condicao = $"$select=copasa_name,copasa_codigosicom,copasa_logradouroid&$filter=_copasa_bairroid_value eq '{dLogradouroSend.BairroId}' and statuscode eq 1";
            dRepository = new DRepository("copasa_logradouros", "Dyn365Host");
            DLogradouroReceive dLogradouroReceive = new DLogradouroReceive() { Logradouros = dRepository.DPesquisarLista(condicao, typeof(DLogradouro)).Cast<DLogradouro>().OrderBy(x => x.LogradouroNome).ToList() };
            return new BaseResponse { Model = dLogradouroReceive };
        }

        /// <summary>
        /// Valida se endereço é coberto pela copasa
        /// </summary>
        /// <returns></returns>
        public BaseResponse ValidaEndereco(DEnderecoSend dEnderecoSend)
        {

            BaseResponse response = new BaseResponse();
            DEnderecoReceive dEnderecoReceive = new DEnderecoReceive();

            if (string.IsNullOrEmpty(dEnderecoSend.Empresa) || string.IsNullOrEmpty(dEnderecoSend.Localidade) ||
                string.IsNullOrEmpty(dEnderecoSend.Bairro) || string.IsNullOrEmpty(dEnderecoSend.Logradouro))
            {
                dEnderecoReceive.descricaoRetorno = "Todos os campos são obrigatórios";
                response.Model = dEnderecoReceive;
                return response;
            }
            else
            {
                dEnderecoSend.Empresa = dEnderecoSend.Empresa.ToUpper().RemoveAccents().Trim();
                dEnderecoSend.Localidade = TratarNomeLocalidadeBairro(dEnderecoSend.Localidade);
                dEnderecoSend.Bairro = TratarNomeLocalidadeBairro(dEnderecoSend.Bairro);
                dEnderecoSend.Logradouro = dEnderecoSend.Logradouro.RemoveAccents().ToUpper().Trim();
            }

            DLocalidadeSend dLocalidadeSend = new DLocalidadeSend
            {
                Empresa = dEnderecoSend.Empresa
            };
            DLocalidadeReceive dLocalidadeReceive = (DLocalidadeReceive)BuscaLocalidade(dLocalidadeSend).Model;

            if (dLocalidadeReceive != null && dLocalidadeReceive.Localidades.Any())
            {
                var localidade = dLocalidadeReceive.Localidades
                    .Where(x => x.LocalidadeNome.ToUpper().RemoveAccents().Equals(dEnderecoSend.Localidade)).FirstOrDefault();

                if (localidade == null)
                {
                    dEnderecoReceive.descricaoRetorno = "Não foi possível encontrar a localidade.";
                    response.Model = dEnderecoReceive;
                    return response;
                }

                DBairroSend dBairroSend = new DBairroSend
                {
                    IdLocalidade = localidade.LocalidadeId
                };

                DBairroReceive dBairroReceive = (DBairroReceive)BuscaBairro(dBairroSend).Model;
                DBairro bairro = null;

                if (dBairroReceive != null && dBairroReceive.Bairros.Any())
                {
                    bairro = dBairroReceive.Bairros
                        .Where(x => x.BairroNome.ToUpper().RemoveAccents().Equals(dEnderecoSend.Bairro)).FirstOrDefault();

                    if (bairro == null)
                    {
                        dEnderecoReceive.descricaoRetorno = "Não foi possível encontrar o bairro.";
                        response.Model = dEnderecoReceive;
                        return response;
                    }
                }
                else
                {
                    dEnderecoReceive.descricaoRetorno = "Não foi localizado bairros para esta localidade.";
                    response.Model = dEnderecoReceive;
                    return response;
                }

                DLogradouroSend dLogradouroSend = new DLogradouroSend
                {
                    BairroId = bairro.BairroId
                };

                DLogradouroReceive dLogradouroReceive = (DLogradouroReceive)BuscaLogradouro(dLogradouroSend).Model;
                DLogradouro logradouro = null;

                if (dLogradouroReceive != null && dLogradouroReceive.Logradouros.Any())
                {
                    string logradouroTratado = string.Empty;

                    //1 - tratando o logradouro removendo prefixo e numeração
                    logradouroTratado = TratarLogradouro(dEnderecoSend.Logradouro);
                    logradouro = dLogradouroReceive.Logradouros
                        .Where(x => x.LogradouroNome.ToUpper().RemoveAccents().Equals(logradouroTratado)).FirstOrDefault();

                    if (logradouro == null)
                    {
                        //2 - tratando o logradouro removendo somente numeração
                        logradouroTratado = TratarLogradouro(dEnderecoSend.Logradouro, false);
                        logradouro = dLogradouroReceive.Logradouros
                            .Where(x => x.LogradouroNome.ToUpper().RemoveAccents().Equals(logradouroTratado)).FirstOrDefault();
                    }

                    if (logradouro == null)
                    {
                        //3 - Sem tratamento
                        logradouro = dLogradouroReceive.Logradouros
                            .Where(x => x.LogradouroNome.ToUpper().RemoveAccents().Equals(dEnderecoSend.Logradouro)).FirstOrDefault();
                    }

                    if (logradouro == null)
                    {
                        dEnderecoReceive.descricaoRetorno = "Não foi possível encontrar o logradouro.";
                        response.Model = dEnderecoReceive;
                        return response;
                    }
                }
                else
                {
                    dEnderecoReceive.descricaoRetorno = "Não foi localizado logradouros para este bairro.";
                    response.Model = dEnderecoReceive;
                    return response;
                }

                dEnderecoReceive.LocalidadeCodigo = localidade.LocalidadeCodigo;
                dEnderecoReceive.LocalidadeId = localidade.LocalidadeId;
                dEnderecoReceive.BairroCodigo = bairro.BairroCodigo;
                dEnderecoReceive.BairroId = bairro.BairroId;
                dEnderecoReceive.LogradouroCodigo = logradouro.LogradouroCodigo;
                dEnderecoReceive.LogradouroId = logradouro.LogradouroId;

                response.Model = dEnderecoReceive;
                return response;
            }

            dEnderecoReceive.descricaoRetorno = "Localidade não é coberta pela Copasa.";
            response.Model = dEnderecoReceive;
            return response;
        }

        /// <summary>
        /// busca os logradouros ativos de um bairro
        /// </summary>
        /// <returns></returns>
        public BaseResponse BuscaPavimentacao(DPavimentacaoSend dPavimentacaoSend)
        {
            //recupera o id do subtipo
            string condicao = $"$select=copasa_subtipodeservicoid&$filter=copasa_codigosicom eq '{dPavimentacaoSend.Subtipo}'";
            dRepository = new DRepository("copasa_subtipodeservicos", "Dyn365Host");
            DServicoReceive servicoReceive = (DServicoReceive)dRepository.DPesquisarObjeto(condicao, typeof(DServicoReceive));
            //--

            if (dPavimentacaoSend.Empresa.ToUpper() == "COPASA") dPavimentacaoSend.Empresa = "1";
            else dPavimentacaoSend.Empresa = "2";

            condicao = $"$select=copasa_name,copasa_tipodepavimentacaoid,copasa_codigosicom,copasa_codigo&$filter=_copasa_subtipodeservioid_value eq '{servicoReceive.SubtipoId}' and copasa_empresa eq {dPavimentacaoSend.Empresa}";
            dRepository = new DRepository("copasa_tipodepavimentacaos", "Dyn365Host");
            DPavimentacaoReceive dPavimentacaoReceive = new DPavimentacaoReceive() { Pavimentacoes = dRepository.DPesquisarLista(condicao, typeof(DPavimentacao)).Cast<DPavimentacao>().ToList() };

            if (dPavimentacaoReceive.Pavimentacoes.Count == 0)
            {
                DPavimentacao dPavimentacao = new DPavimentacao()
                {
                    descricaoRetorno = null,
                    Nome = null,
                    TipoId = null,
                    CodigoSiCom = "0000000",
                    Codigo = "0000000",
                };
                dPavimentacaoReceive.Pavimentacoes.Add(dPavimentacao);
            }

            return new BaseResponse { Model = dPavimentacaoReceive };
        }

        #endregion

        #region funções internas

        private bool AtualizaProtocolo(DAtualizaProtocoloSend dAtualiza)
        {
            string pavimentacao = dAtualiza.SubtipoId + dAtualiza.PavimentacaoId;
            //recupera o id do serviço
            string condicao = $"$select=copasa_name,copasa_codigo,copasa_codigosicom,copasa_portfoliodeservicoid,copasa_codigo,copasa_empresa&$filter=copasa_codigo eq '{dAtualiza.Empresa}-{dAtualiza.Servico}'";
            DServicoReceive servicoReceive = new DServicoReceive();
            DServicoReceive servico = new DServicoReceive();
            //
            dRepository = new DRepository("copasa_portfoliodeservicos", "Dyn365Host");
            servicoReceive = (DServicoReceive)dRepository.DPesquisarObjeto(condicao, servicoReceive.GetType());
            servico.ServicoId = servicoReceive.ServicoId;
            dAtualiza.ServicoId = $"/copasa_portfoliodeservicos({servico.ServicoId})";
            //

            //recupera o id do subtipo
            dAtualiza.Servico = dAtualiza.SubtipoId;
            condicao = $"$select=copasa_name,copasa_codigosicom,copasa_subtipodeservicoid,_copasa_portfoliodeservicoid_value,copasa_empresa&$filter=copasa_codigosicom eq '{dAtualiza.SubtipoId}'";
            dRepository = new DRepository("copasa_subtipodeservicos", "Dyn365Host");
            servicoReceive = (DServicoReceive)dRepository.DPesquisarObjeto(condicao, servicoReceive.GetType());
            servico.SubtipoId = servicoReceive.SubtipoId;
            dAtualiza.SubtipoId = $"/copasa_subtipodeservicos({servico.SubtipoId})";
            //
            //recupera o id da pavimentação
            condicao = $"$select=copasa_tipodepavimentacaoid&$filter=_copasa_subtipodeservioid_value eq '{servicoReceive.SubtipoId}'";

            dAtualiza.Servico += dAtualiza.PavimentacaoId;

            if (!string.IsNullOrEmpty(dAtualiza.PavimentacaoId))
                condicao += $" and copasa_codigosicom eq '{pavimentacao}'";

            dRepository = new DRepository("copasa_tipodepavimentacaos", "Dyn365Host");
            servicoReceive = (DServicoReceive)dRepository.DPesquisarObjeto(condicao, servicoReceive.GetType());

            //em caso de pavimentação 
            if (servicoReceive == null) return false;

            servico.PavimentacaoId = servicoReceive.PavimentacaoId;
            dAtualiza.PavimentacaoId = $"/copasa_tipodepavimentacaos({servico.PavimentacaoId})";

            dRepository = new DRepository("incidents", "Dyn365Host");
            bool retorno = dRepository.Atualizar(dAtualiza, dAtualiza.ProtocoloId, null);
            //
            return retorno;
        }

        /// <summary>
        /// Verifica o cadastro do cpfcnpj
        /// </summary>
        /// <returns>o idcpfcnpj</returns>
        private string VerificaCadastroCpfCnpj(string CpfCnpjLogin, string NomeSolicitante, string TelefoneSolicitante, string EmailSolicitante)
        {

            DValidaCpfCnpjSend dyn365ValidaCpfCnpjSend = new DValidaCpfCnpjSend()
            {
                CpfCnpj = CpfCnpjLogin
            };

            DValidaCpfCnpjReceive dyn365ValidaCpfCnpjReceive = (DValidaCpfCnpjReceive)ValidaCpfCnpjDyn365(dyn365ValidaCpfCnpjSend).Model;

            //caso nao tenha, cadastrar
            if (string.IsNullOrEmpty(dyn365ValidaCpfCnpjReceive.CpfCnpjId))
            {
                DCadastraUsuarioSend dCadastraUsuario = new DCadastraUsuarioSend()
                {
                    Username = "servico.app@copasa.com.br",
                    Password = "CYiEjHlatF9W33G56MjcmrfEh1nRXazWw+sYuVM/QGc=",
                    CpfCnpj = InsereMascaraCpfCnpj(CpfCnpjLogin),
                    PortalUsername = CpfCnpjLogin,
                    Firstname = NomeSolicitante,
                    Lastname = "",
                    Phonetype = "",
                    Telephone1 = TelefoneSolicitante,
                    Telephone2 = "",
                    Mobilephone = "",
                    DoNotEmail = "false",
                    EmailAddress1 = EmailSolicitante,
                    CopasaTermoAceite = "",
                    CopasaPoliticaPrivacidade = "",
                    CopasaTipoCliente = "",
                    CopasaValidacaoEmail = "",
                    EntityImage = "",
                    PortalUserpassword = "RZG0a8LwY+NBJFyuIFkcAs40c1J2iMosCIIrzK+fJy8=",
                    PortaluserPasswordBtoa = "false"
                };

                dRepository = new DRepository("CopasaUser", "Dyn365HostAuthenticate");

                BaseModelAzureCopaUserReceive CopaUserReceive = (BaseModelAzureCopaUserReceive)dRepository.DExecutarServico("CreateDyn365PortalUser", dCadastraUsuario, typeof(BaseModelAzureCopaUserReceive));

                return CopaUserReceive.Id;
            }
            else
            {
                return dyn365ValidaCpfCnpjReceive.CpfCnpjId;
            }
        }

        private string InsereMascaraCpfCnpj(string cpfCnpj)
        {
            if (!string.IsNullOrEmpty(cpfCnpj))
            {
                //insere máscara no cpf ou cnpj
                if (cpfCnpj.Length <= 11)
                {
                    cpfCnpj = cpfCnpj.PadLeft(11, '0');
                    cpfCnpj = $"{cpfCnpj.Substring(0, 3)}.{cpfCnpj.Substring(3, 3)}." +
                        $"{cpfCnpj.Substring(6, 3)}-{cpfCnpj.Substring(9, 2)}";
                }
                else
                if (cpfCnpj.Length > 11 && cpfCnpj.Length <= 14)
                {
                    cpfCnpj = cpfCnpj.PadLeft(14, '0');
                    cpfCnpj = $"{cpfCnpj.Substring(0, 2)}.{cpfCnpj.Substring(2, 3)}.{cpfCnpj.Substring(5, 3)}" +
                        $"/{cpfCnpj.Substring(8, 4)}-{cpfCnpj.Substring(12, 2)}";

                }

                // TODO após aprovação da TASK apagar este código comentado

                //if (cpfCnpj.Length == 11)
                //{

                //    cpfCnpj = $"{cpfCnpj.Substring(0, 3)}.{cpfCnpj.Substring(3, 3)}." +
                //        $"{cpfCnpj.Substring(6, 3)}-{cpfCnpj.Substring(9, 2)}";
                //}
                //else if (cpfCnpj.IndexOf('.') == -1)
                //{
                //    cpfCnpj = $"{cpfCnpj.Substring(0, 2)}.{cpfCnpj.Substring(2, 3)}.{cpfCnpj.Substring(5, 3)}" +
                //        $"/{cpfCnpj.Substring(8, 4)}-{cpfCnpj.Substring(12, 2)}";
                //}
            }
            return cpfCnpj;
        }

        /// <summary>
        /// Recupera o id da agencia
        /// </summary>
        /// <param name="origem"></param>
        /// <returns>id fixo da agencia</returns>
        private string TrataOrigem(string origem)
        {

            dRepository = new DRepository("copasa_agenciafisicas", "Dyn365Host");
            string condicao = $"$select=copasa_agenciafisicaid&$filter=copasa_codigo eq '310620058AG{origem}'";
            DAgenciaReceive dAgenciaReceive = (DAgenciaReceive)dRepository.DPesquisarObjeto(condicao, typeof(DAgenciaReceive));
            if (dAgenciaReceive != null)
                return dAgenciaReceive.AgenciaId;
            else
                return null;
        }

        /// <summary>
        /// Trata a string que representa um logradouro removendo prefixo rua, avenida, praça, etc e numeração.
        /// 
        /// Ex. de sucesso:
        /// Rua Boaventura, 99
        /// R. Boaventura - 99
        /// Av. Prof. Antonio da Silva, 99
        /// Avenida Prof. Antonio da Silva- 99
        /// Rua Central 99
        /// Av. do Contorno 1000
        /// 
        /// Ex. de falha:
        /// Rua Trinta s/n
        /// Rua Principal apto 101, numero 10
        /// Rua 15 de novembro 99
        /// R. 15 de novembro, 99
        /// 
        /// </summary>
        /// <param name="logradouro"></param>
        /// <param name="contemPrefixo">True, tenta remover prefixo</param>
        /// <returns></returns>
        private string TratarLogradouro(string logradouro, bool contemPrefixo = true)
        {
            var rgTipoLogradouro = @"[a-zA-Z.]+\s";
            var rgLogradouro = @"[a-zA-Z\s\.]+(,|-|[0-9])";

            //remove tipo logradouro
            if (contemPrefixo)
            {
                Regex rgx = new Regex(rgTipoLogradouro);
                logradouro = rgx.Replace(logradouro, "", 1);
                logradouro = logradouro?.Trim();
            }

            //extrai o logradouro, separando de possivel numeracao
            var matchLogradouro = Regex.Match(logradouro, rgLogradouro);
            if (matchLogradouro.Success)
            {
                logradouro = matchLogradouro.Value;
                return logradouro?.Remove(logradouro.Length - 1).Trim();
            }

            return logradouro?.Trim();
        }

        /// <summary>
        /// Trata uma string removendo caracteres numéricos antes do nome
        /// </summary>
        /// <param name="nome">Localidade ou Bairro</param>
        /// <returns></returns>
        private string TratarNomeLocalidadeBairro(string nome)
        {
            var rgNumeros = @"[0-9]";

            Regex rgx = new Regex(rgNumeros);
            nome = rgx.Replace(nome, "");
            nome = nome?.Replace("-", "").Replace(",", "").RemoveAccents().ToUpper().Trim();

            return nome;
        }

        /// <summary>
        /// Registra Imagem na Base64 para Vazamento na Rua/Imóvel
        /// </summary>
        /// <param name="dImagemVazamentoSend">Dados da Imagem</param>
        /// <returns></returns>
        private bool RegistrarImagemVazamento(DImagemVazamentoSend dImagemVazamentoSend)
        {
            dRepository = new DRepository("annotations", "Dyn365Host");

            bool registrou = dRepository.Incluir(dImagemVazamentoSend, null);

            return registrou;
        }

        #endregion
        /// <summary>
        /// Retorna Historico de Servicos
        /// </summary>
        /// <param name="dHistoricoServicoSendApp">Dados da Imagem</param>
        /// <returns></returns>
        public BaseResponse GetHistoricoServico(DHistoricoServicoSendApp dHistoricoServicoSendApp)
        {
            DHistoricoServicoReceiveApp historicoServicoReceiveApp = new DHistoricoServicoReceiveApp();
            dRepository = new DRepository("incidents", "Dyn365Host");
            Dyn365ProtocoloApp dyn365ProtocoloApp = new Dyn365ProtocoloApp();
            string cpfCnpj = InsereMascaraCpfCnpj(dHistoricoServicoSendApp.cpfcnpj);
            string iDcpfCnpj = dHistoricoServicoSendApp.idCpfCnpj;
            List<DListaIdentificadorReceive> ListaRetorno = new List<DListaIdentificadorReceive>();

            //string condicao = $"$select=copasa_codigoservico,createdon,copasa_dataconclusao," +
            //   $"copasa_previsaoatendimentoss,copasa_descrconclusao,copasa_protocolo," +
            //   $"copasa_servicosid,statecode,statuscode,copasa_subtipoid,copasa_respostaaosolicitante," +
            //   $"copasa_tiposervicodeorigem,copasa_execucaobaixa&" +
            //   $"$filter=copasa_cpf_cnpj eq '{cpfCnpj}' &$orderby=createdon desc &$expand=copasa_subtipoid($select=copasa_name),copasa_servicosid($select=copasa_name)";

            //string condicao = $"$filter=copasa_cpf_cnpj eq '{cpfCnpj}' &$orderby=createdon desc &$expand=copasa_subtipoid($select=copasa_name),copasa_servicosid($select=copasa_name)";
            string condicao = $"$filter=_customerid_value eq '{iDcpfCnpj}' &$orderby=createdon desc &$expand=copasa_subtipoid($select=copasa_name),copasa_servicosid($select=copasa_name)";
            historicoServicoReceiveApp.Protocolos = dRepository.DPesquisarLista(condicao, dyn365ProtocoloApp.GetType(), true).Cast<Dyn365ProtocoloApp>().ToList();

            //caso de lista vazia.
            if (historicoServicoReceiveApp.Protocolos.Count == 0)
            {
                historicoServicoReceiveApp.IsValid = false;
                historicoServicoReceiveApp.descricaoRetorno = "Protocolos não encontrados.";
            }

            //CRIAR PROTOCOLO
            if (dHistoricoServicoSendApp.Origem != null && !"URA".Equals(dHistoricoServicoSendApp.Origem.ToUpper()))
            {
                DCriaProtocoloSend criaProtocoloSend = new DCriaProtocoloSend()
                {
                    CpfCnpj = InsereMascaraCpfCnpj(dHistoricoServicoSendApp.cpfcnpj),
                    IdCpfCnpj = dHistoricoServicoSendApp.idCpfCnpj,
                    TipoProtocolo = "5",
                    Empresa = dHistoricoServicoSendApp.empresa,
                    Origem = dHistoricoServicoSendApp.Origem
                };

                BaseResponse baseResponse = CriaProtocolo(criaProtocoloSend);
                if (baseResponse != null)
                {
                    DCriaProtocoloReceive dCriaProtocoloReceive = (DCriaProtocoloReceive)baseResponse.Model;
                    historicoServicoReceiveApp.ProtocoloId = dCriaProtocoloReceive.ProtocoloId;
                    historicoServicoReceiveApp.Protocolo = dCriaProtocoloReceive.Protocolo;
                }
            }
            var response = new BaseResponse();
            response.Model = historicoServicoReceiveApp;
            return response;

        }

        /// <summary>
        /// Retorna Historico de Servicos
        /// </summary>
        /// <param name="dHistoricoServicoDetalheSend">Dados da Imagem</param>
        /// <returns></returns>
        public BaseResponse GetHistoricoServicoDetalhe(DHistoricoServicoDetalheSend dHistoricoServicoDetalheSend)
        {
            
            DHistoricoServicoDetalheReceive historicoServicoDetalheReceive = new DHistoricoServicoDetalheReceive();
            dRepository = new DRepository("incidents", "Dyn365Host");
            Dyn365ProtocoloDetalhe dyn365ProtocoloDetalhe = new Dyn365ProtocoloDetalhe();
            string cpfCnpj = InsereMascaraCpfCnpj(dHistoricoServicoDetalheSend.cpfcnpj);
            List<DListaIdentificadorReceive> ListaRetorno = new List<DListaIdentificadorReceive>();

            string condicao = $"$filter=copasa_cpf_cnpj eq '{cpfCnpj}' &$orderby=createdon desc &$expand=copasa_subtipoid($select=copasa_name),copasa_servicosid($select=copasa_name)";
            historicoServicoDetalheReceive.Protocolos = dRepository.DPesquisarLista(condicao, dyn365ProtocoloDetalhe.GetType()).Cast<Dyn365ProtocoloDetalhe>().ToList();

            var response = new BaseResponse();

            response.Model = historicoServicoDetalheReceive.Protocolos
                .Where(w => w.numeroProtocoloSS == dHistoricoServicoDetalheSend.protocolo)
                .Select(s => new Dyn365ProtocoloHistoricoDetalhe
                {
                    numeroProtocoloSS = s.numeroProtocoloSS,
                    descricaoServicoSS = s.descricaoServicoSS,
                    situacaoSS = s.situacaoSS,
                    dataPrevisaoSSDyn365 = s.dataPrevisaoSSDyn365,
                    dataTerminoAtendimentoDyn365 = s.dataTerminoAtendimentoDyn365,
                    IdServicoExpand = s.IdServicoExpand,
                    IdSubTipoServicoExpand = s.IdSubTipoServicoExpand,
                    CreatedOn = s.CreatedOn,
                    RespostaAoSolicitante = s.RespostaAoSolicitante,

                }).First();

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dComentarioSend"></param>
        /// <returns></returns>
        public BaseResponse GetComentarios(DComentariosSend dComentarioSend)
        {
            throw new NotImplementedException();
        }
    }
}
