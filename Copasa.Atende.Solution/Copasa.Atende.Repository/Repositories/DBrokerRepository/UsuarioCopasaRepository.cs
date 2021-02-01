using Copasa.Atende.Model.Broker;
using Copasa.Atende.Model.Enumerador;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces.DBrokerRepository;
using Copasa.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Repository.Repositories.DBrokerRepository
{
    /// <summary>
    /// Repository para alteração de contatos de usuario broker
    /// </summary>
    public class UsuarioCopasaRepository : DBrokerRepository<UsuarioCopasaModel>, IUsuarioCopasaRepository
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        public UsuarioCopasaRepository()
            : base("SCN4FTE1")
        {

        }

        /// <summary>
        /// Atualiza e-mail e telefones do usuário Copasa.
        /// </summary>
        /// <param name="usuarioCopasa">Usuário Copasa.</param>     
        /// <param name="ambiente">Ambiente.</param>
        /// <returns></returns>
        public UsuarioCopasaReceive AtualizacaoCadastral(UsuarioCopasaModel usuarioCopasa, EnvironmentEnum ambiente)
        {
            try
            {
                var erroRetornoSicom = string.Empty;

                var usuarioCopasaRetornoModel = new UsuarioCopasaReceive();

                var parametros = ConvertStringUtil.Set(usuarioCopasa);

                var dadosBroker = Download(parametros, ambiente);

                var codigoRetornoSicom = ValidarRetornoServicoSicom(dadosBroker);

                if (codigoRetornoSicom > 0)
                {
                    codigoRetornoSicom = 0;

                    dadosBroker = Download(parametros, ambiente);

                    codigoRetornoSicom = ValidarRetornoServicoSicom(dadosBroker);
                }

                //if (codigoRetornoSicom > 0)
                //{
                //    switch (codigoRetornoSicom)
                //    {
                //        case 1:
                //            erroRetornoSicom = Messages.SERVICE_ERROR;
                //            break;
                //        case 2:
                //            erroRetornoSicom = Messages.SERVICE_ERROR_BROKER;
                //            break;
                //        case 3:
                //            erroRetornoSicom = Messages.SERVICE_ERROR_ADABAS;
                //            break;
                //    }

                //    string solicitacao = string.Empty;

                //    if (ambiente.Equals("P"))
                //    {
                //        solicitacao = "<table border = '1' cellspacing = '0' cellpadding = '2' style = 'font-family:arial;font-size:12'>" +
                //            "<tr><td><b>Identificador: </b></td><td>" + usuarioCopasa.Identificador + "</td></tr>" +
                //            "<tr><td><b>E-mail:  </b></td><td>" + usuarioCopasa.InformacoesEmail + "</td></tr>" +
                //            "<tr><td><b>DDD Celular:  </b></td><td>" + usuarioCopasa.DDDTelefoneCelular + "</td></tr>" +
                //            "<tr><td><b>Número Celular:  </b></td><td>" + usuarioCopasa.NumeroTelefoneCelular + "</td></tr>" +
                //            "<tr><td><b>DDD Residencial:  </b></td><td>" + usuarioCopasa.DDDTelefoneResidencial + "</td></tr>" +
                //            "<tr><td><b>Número Residencial:  </b></td><td>" + usuarioCopasa.NumeroTelefoneResidencial + "</td></tr>" +
                //            "<tr><td><b>DDD Comercial:  </b></td><td>" + usuarioCopasa.NumeroTelefoneComercial + "</td></tr>" +
                //            "<tr><td><b>Número Comercial:  </b></td><td>" + usuarioCopasa.DDDTelefoneComercial + "</td></tr>" +
                //            "<tr><td><b>Origem: </b></td><td>" + origem + "</td></tr>" +
                //            "</table>";

                //        ComercialUtil.EnviaEmail(solicitacao, "Serviço - Atualização Cadastral");
                //    }

                //    throw new Exception(erroRetornoSicom);
                //}
                //else
                //{
                usuarioCopasaRetornoModel.MensagemRetorno = dadosBroker.Substring(0, 80).ToString().Trim();

                if (usuarioCopasaRetornoModel.MensagemRetorno.IsNullOrEmpty())
                {
                    usuarioCopasaRetornoModel = ConvertStringUtil.Get<UsuarioCopasaReceive>(dadosBroker);
                }
                else
                {
                    usuarioCopasaRetornoModel = ConvertStringUtil.Get<UsuarioCopasaReceive>(dadosBroker);
                    verificarCodigoErroMainframe(usuarioCopasaRetornoModel);
                }


                return usuarioCopasaRetornoModel;
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        ///  Verifica se os telefones foram inseridos corretamente pelo cod do Mainframe.
        /// </summary>
        /// <param name="usuarioCopasaRetornoModel"></param>
        /// <returns></returns>
        private string verificarCodigoErroMainframe(UsuarioCopasaReceive usuarioCopasaRetornoModel)
        {
            string erroRetornoSicom;
            switch (usuarioCopasaRetornoModel.CodigoRetorno)
            {
                case "001":
                    erroRetornoSicom = usuarioCopasaRetornoModel.MensagemRetorno;
                    break;
                case "002":
                    erroRetornoSicom = usuarioCopasaRetornoModel.MensagemRetorno;
                    break;
                case "003":
                    erroRetornoSicom = usuarioCopasaRetornoModel.MensagemRetorno;
                    break;
                case "004":
                    erroRetornoSicom = usuarioCopasaRetornoModel.MensagemRetorno;
                    break;
                case "005":
                    erroRetornoSicom = usuarioCopasaRetornoModel.MensagemRetorno;
                    break;
                case "006":
                    erroRetornoSicom = usuarioCopasaRetornoModel.MensagemRetorno;
                    break;
                default:
                    return null;
            }

            throw new Exception(erroRetornoSicom);
        }

        /// <summary>
        /// Valida Retorno Broker.
        /// </summary>
        /// <param name="dadosBroker">Dados Broker.</param>
        public static int ValidarRetornoServicoSicom(string dadosBroker)
        {
            var retornoSicom = 0;

            if (dadosBroker.Trim().IsNullOrEmpty())
            {
                retornoSicom = 1; // retorna vazio, emitir mensagem padrão.
            }
            else if (!dadosBroker.IsNullOrEmpty())
            {
                // verificar se retornou um valor númerico ou um mensagem de erro.
                int i = 0;
                bool resultado = int.TryParse(dadosBroker.ToString(), out i);

                if (resultado)
                {
                    var codigoRetorno = dadosBroker.ToString().ToLong();

                    if (codigoRetorno.Equals(1718))
                    {
                        retornoSicom = 4; // não possue faturas.
                    }
                    else if (codigoRetorno.Equals(1535))
                    {
                        retornoSicom = 5; // 
                    }
                }
                else
                {
                    var mensagemRetorno = dadosBroker.Contains("Código do Erro ...: ");
                    if (mensagemRetorno)
                    {
                        retornoSicom = 2; // retorna algum erro no broker.
                    }
                    else
                    {
                        mensagemRetorno = dadosBroker.Contains(" ERRO ");
                        if (mensagemRetorno)
                        {
                            mensagemRetorno = dadosBroker.Contains("ERRO 3009 ");

                            if (mensagemRetorno)
                            {
                                retornoSicom = 3; // retorna erro de timeout.
                            }
                        }
                    }
                }
            }

            return retornoSicom;
        }


    }
}
