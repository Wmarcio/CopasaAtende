using Copasa.Atende.Facade.Interfaces;
using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Model.Dyn365;
using Copasa.Atende.Util;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Http;

namespace Copasa.Atende.WebService.Controllers
{
    /// <summary>
    /// API's para o serviço de cliente
    /// </summary>
    [RoutePrefix("api/cliente")]
    public class ClienteController : BaseApiController
    {
        private IClienteFacade _clienteFacade;
        private IDClienteFacade _dclienteFacade;
        private ICertidaoNegativaDebitoFacade _certidaoNegativaDebitoFacade;

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="clienteFacade">IClienteFacade.</param>
        /// <param name="dclienteFacade">IDClienteFacade.</param>
        /// <param name="certidaoNegativaDebitoFacade"></param>
        /// <param name="log">ILog</param>
        public ClienteController(IClienteFacade clienteFacade, IDClienteFacade dclienteFacade, ICertidaoNegativaDebitoFacade certidaoNegativaDebitoFacade, ILog log)
        {
            _clienteFacade = clienteFacade;
            _dclienteFacade = dclienteFacade;
            _certidaoNegativaDebitoFacade = certidaoNegativaDebitoFacade;
            _log = log;
        }

        /// <summary>
        /// Lista identificadores a partir de um CPF ou CNPJ(SCN3PCLI)
        /// </summary>
        /// <param name="entrada">CPF ou CNPJ e empresa</param>
        [Route("lista/identificadores")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN3PCLIReceive))]
        public Task<HttpResponseMessage> listaIdentificadores([FromBody] SCN3PCLISend entrada)
        {
            return TrataRetornoFacade(_clienteFacade, "SCN3PCLI", entrada);
        }

        /// <summary>
        /// Lista matrículas a partir de um CPF ou CNPJ e opcionalmente, identificadores(SCN4ISU1)
        /// </summary>
        /// <param name="entrada">CPF ou CNPJ e/ou lista de identificadores e empresa</param>
        [Route("lista/matriculas")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN4ISU1Receive))]
        public Task<HttpResponseMessage> listaMatriculas([FromBody] SCN4ISU1Send entrada)
        {
            return TrataRetornoFacade(_clienteFacade, "SCN4ISU1", entrada);
        }

        /// <summary>
        /// Lista dados de identificadores(SCN4ISU1)
        /// </summary>
        /// <param name="entrada">CPF ou CNPJ e/ou lista de identificadores e empresa</param>
        [Route("lista/dadosIdentificadores")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN4ISU1_IdentificadoresReceive))]
        public Task<HttpResponseMessage> listaItentificadores([FromBody] SCN4ISU1_IdentificadoresSend entrada)
        {
            return TrataRetornoFacade(_clienteFacade, "SCN4ISU1Identificadores", entrada);
        }

        /// <summary>
        /// Lista nomes de identificadores(SCN4ISU1)
        /// </summary>
        /// <param name="entrada">CPF ou CNPJ e/ou lista de identificadores e empresa</param>
        [Route("lista/nomeCPFCNPJ")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN4ISU1_NomesReceive))]
        public Task<HttpResponseMessage> listaNomvesItentificadores([FromBody] SCN4ISU1_NomesSend entrada)
        {
            return TrataRetornoFacade(_clienteFacade, "SCN4ISU1Nomes", entrada);
        }

        /// <summary>
        /// Verifica se o cliente de um identificador é pessoa física ou jurídica
        /// </summary>
        /// <param name="entrada">Identificador</param>
        [Route("validaCNPJ")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(TrabValidaCNPJReceive))]
        public Task<HttpResponseMessage> validaCNPJ([FromBody] TrabValidaCNPJSend entrada)
        {
            return TrataRetornoFacade(_clienteFacade, "validaCNPJ", entrada);
        }

        /// <summary>
        /// Lista histórico de consumo de uma matrícula e identificador(SCN5ISHC)
        /// </summary>
        /// <param name="entrada">Mátricula do imóvel, identificador do cliente e empresa</param>
        [Route("lista/historicoConsumo")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN5ISHCReceive))]
        public Task<HttpResponseMessage> listaHistoricoConsumo([FromBody] SCN5ISHCSend entrada)
        {
            return TrataRetornoFacade(_clienteFacade,"SCN5ISHC",entrada);            
        }

        /// <summary>
        /// Lista matrículas a partir de um endereço(SCN3ISMT)
        /// </summary>
        /// <param name="entrada">CPF ou CNPJ e/ou lista de identificadores e empresa</param>
        [Route("lista/matriculasPorEndereco")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN3ISMTReceive))]
        public Task<HttpResponseMessage> listaMatriculasPorEndereco([FromBody] SCN3ISMTSend entrada)
        {
            return TrataRetornoFacade(_clienteFacade, "SCN3ISMT",entrada);            
        }

        /// <summary>
        /// Lista pontos de serviço de esgoto, água, fonte, dedução e medição de esgoto de uma matrícula(SCN3ISPS)
        /// </summary>
        /// <param name="entrada">Matrícula do imóvel e empresa</param>
        [Route("lista/pontosServico")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN3ISPSReceive))]
        public Task<HttpResponseMessage> listaPontosServicos([FromBody] SCN3ISPSSend entrada)
        {
            return TrataRetornoFacade(_clienteFacade, "SCN3ISPS",entrada);
        }

        /// <summary>
        /// Valida Usuário
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("lista/validaUsuario")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(TrabValidaUsuarioReceive))]
        public Task<HttpResponseMessage> validaUsuario([FromBody] TrabValidaUsuarioSend entrada)
        {
            return TrataRetornoFacade(_clienteFacade, "validaUsuario",entrada);
        }

        /// <summary>
        /// Obtem a certidão negativa de débito a partir de um CPF ou CNPJ(SCN6ISCN)
        /// </summary>
        /// <param name="entrada">CPF ou CNPJ e empresa.</param>
        [Route("obtem/CertidaoNegativaDebito")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN6ISCNView))]
        public Task<HttpResponseMessage> getCertidaoNegativa([FromBody] SCN6ISCNSend entrada)
        {
            return TrataRetornoFacade(_certidaoNegativaDebitoFacade, "SCN6ISCN",entrada);
        }

        /// <summary>
        /// Exibe fatura em formato PDF a partir do número
        /// </summary>
        /// <param name="cpfCnpj">CPF ou CNPJ.</param>
        /// <param name="empresa">Empresa.</param>
        [Route("exibe/CertidaoNegativaDebito/PDF/{empresa}/{cpfCnpj}")]
        [HttpGet]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno formato PDF")]
        public Task<HttpResponseMessage> ExibirPDFCertidaoNegativa([FromUri] string empresa, string cpfCnpj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                _log.SetEntrada();
                _log.SetNomeServico(GetNameCallMethod());
                _log.AddLog(GetClassNameCallMethod() + " ExibirPDFCertidaoNegativa cpfCnpj:" + cpfCnpj);
                SCN6ISCNSend entrada = new SCN6ISCNSend();
                entrada.empresa = empresa;
                entrada.cpfCnpj = cpfCnpj;
                try
                {
                    string origem = entrada.empresa;
                    if ("RAJADA".Equals(origem))
                    {
                        _log.IsRajada();
                    }
                }
                catch (Exception) { }

                var baseResponse = _certidaoNegativaDebitoFacade.SCN6ISCN(entrada);
                SCN6ISCNView sCN6ISCNView = (SCN6ISCNView)baseResponse.Model;
                DateTime tempoInicio = DateTime.Now;
                int totalPasses = 2;
                bool temPasse = false;
                string idPasse = null;
                if (HttpContext.Current != null && HttpContext.Current.Cache != null)
                {
                    do
                    {
                        for (int i = 0; i < HttpContext.Current.Cache.Count; i++)
                        {
                            if (i >= totalPasses)
                                break;

                            idPasse = "PasseCertidao_" + i;
                            if (HttpContext.Current.Cache[idPasse] == null)
                            {
                                DateTime expiration = DateTime.Now.AddMinutes(5);
                                HttpContext.Current.Cache.Add(idPasse, idPasse, null, expiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                                temPasse = true;
                                Thread.Sleep(500);
                                break;
                            }
                        }
                        if (!temPasse)
                        {
                            Thread.Sleep(1500);
                        }
                    }
                    while (!temPasse);
                }
                DateTime tempoRotativa = DateTime.Now;
                Stream retorno = conectarServidorviaViaPost("CertidaoNegativaCpf/GerarPdfPost", converteQuebraPaginaHtml(sCN6ISCNView)).GetResponseStream();
                TimeSpan DuracaoConexaoRotativa = DateTime.Now.Subtract(tempoRotativa);
                double duracaoSegundosRotativa = Math.Round(DuracaoConexaoRotativa.TotalSeconds, 2);

                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    //Content = new ByteArrayContent(retorno.ToArray)
                    Content = new StreamContent(retorno)
                };
                response.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("inline")
                        {
                            FileName = "certidao.pdf"
                        };
                response.Content.Headers.ContentType =
                        new MediaTypeHeaderValue("application/pdf");
                TimeSpan DuracaoConexao = DateTime.Now.Subtract(tempoInicio);
                double duracaoSegundos = Math.Round(DuracaoConexao.TotalSeconds, 2);
                if (idPasse != null)
                {
                    Thread.Sleep(500);
                    HttpContext.Current.Cache.Remove(idPasse);
                }
            }
            catch (Exception e)
            {
                _log.AddLog("Erro exibe/CertidaoNegativaDebito/PDF: " + e.Message);
                _log.PringLog();
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            _log.PringLog();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        /// <summary>
        /// Modifica o status para envio de fatura por email(SCN6ISCE)
        /// </summary>
        /// <param name="entrada">Matrícula do imóvel, status a ser atualizado, email e empresa.</param>
        [Route("modifica/statusFaturaPorEmail")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN6ISCEReceive))]
        public Task<HttpResponseMessage> Incluir([FromBody] SCN6ISCESend entrada)
        {
            return TrataRetornoFacade(_clienteFacade, "SCN6ISCE",entrada);
        }

        /// <summary>
        /// Modifica email e telefone(SCN4ISAC)
        /// </summary>
        [Route("modifica/emailTelefone")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN4ISACReceive))]
        public Task<HttpResponseMessage> Incluir([FromBody] SCN4ISACSend entrada)
        {
            return TrataRetornoFacade(_clienteFacade, "SCN4ISAC", entrada);
        }

        /// <summary>
        /// Altera vencimento fatura(SCN6ISAV)
        /// </summary>
        [Route("altera/vencimentoFatura")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN6ISAVReceive))]
        public Task<HttpResponseMessage> alteraVencimento([FromBody] SCN6ISAVSend entrada)
        {
            return TrataRetornoFacade(_clienteFacade, "SCN6ISAV", entrada);
        }

        /// <summary>
        /// Consiste matrícula centralizadora(SCN6ISCC)
        /// </summary>
        [Route("consiste/matriculaCentralizadora")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN6ISCCReceive))]
        public Task<HttpResponseMessage> consisteMatrCentralizadora([FromBody] SCN6ISCCSend entrada)
        {
            return TrataRetornoFacade(_clienteFacade, "SCN6ISCC", entrada);
        }

        /// <summary>
        /// Altera a Senha de um Contato no Microsoft Dynamics 365 enviando ContatoId.
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("modifica/senha")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(BaseModelAzureCopaUserReceive))]
        public Task<HttpResponseMessage> AtualizaSenha([FromBody] Dyn365ChangeUserPasswordCpfSend entrada)
        {
            return TrataRetornoFacade(_clienteFacade,"AlteraSenha",entrada);
        }

        /// <summary>
        /// Atualiza um Contato no Microsoft Dynamics 365.
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("modifica/usuario")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(BaseModelAzureCopaUserReceive))]
        public Task<HttpResponseMessage> AtualizaUsuario([FromBody] Dyn365UpdatePortalUserSend entrada)
        {
            return TrataRetornoFacade(_clienteFacade, "AtualizaUsuario", entrada);            
        }

        /// <summary>
        /// Altera o Status do [Identificador do Contato] no Microsoft Dynamics 365.
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("modifica/statusIdentificador")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(BaseModelAzureCopaUserReceive))]
        public Task<HttpResponseMessage> AtualizaUsuario([FromBody] Dyn365ChangeControllerIdentifierStatusSend entrada)
        {
            return TrataRetornoFacade(_clienteFacade, "AtualizaStatusIdentificador", entrada);            
        }    

        /// <summary>
        /// Cria um Contato Novo no Microsoft Dynamics 365.
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("cadastra/usuario")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(BaseModelAzureCopaUserReceive))]
        public Task<HttpResponseMessage> CadastraUsuario([FromBody] Dyn365CreatePortalUserSend entrada)
        {
            return TrataRetornoFacade(_clienteFacade, "CadastraUsuario", entrada);            
        }


        /// <summary>
        /// Autentica um Contato no Microsoft Dynamics 365.
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("autentica/usuario")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(BaseModelAzureCopaUserReceive))]
        public Task<HttpResponseMessage> AutenticaUsuario([FromBody] Dyn365AuthenticateUserSend entrada)
        {
            return TrataRetornoFacade(_clienteFacade, "DAutenticaUsuario", entrada);
        }
        
        /// <summary>
        /// Gera uma Nova Senha para um Contato no Microsoft Dynamics 365.
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("recupera/senha")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(BaseModelAzureCopaUserReceive))]
        public Task<HttpResponseMessage> RecuperaSenha([FromBody] Dyn365RecoveryUserPasswordSend entrada)
        {
            return TrataRetornoFacade(_clienteFacade, "RecuperaSenha", entrada);
        }

        /// <summary>
        /// Lista dados de hidrômetros de uma matrícula
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("lista/dadosHidrometro")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(TrabListaDadosHidrometro))]
        public Task<HttpResponseMessage> listaDadosHidrometro([FromBody] SCN5IS01Send entrada)
        {
            return TrataRetornoFacade(_clienteFacade, "listaDadosHidrometro", entrada);
        }

        /// <summary>
        /// Informa quitação anual de débito(SCN6ISQA)
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("obtem/quitacaoAnualDebito")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN6ISQAReceive))]
        public Task<HttpResponseMessage> obtemQuitacaoAnualDebito([FromBody] SCN6ISQASend entrada)
        {
            return TrataRetornoFacade(_clienteFacade, "SCN6ISQA", entrada);
        }

        /// <summary>
        /// Exibe quitação anual de débito em formato PDF a partir de uma matrícula e ano de referência
        /// </summary>
        /// <param name="matricula">Matrícula cliente.</param>
        /// <param name="anoPesquisa">Ano da pesquisa.</param>
        /// <param name="empresa">Empresa.</param>
        [Route("exibe/quitacaoAnualDebito/PDF/{empresa}/{matricula}/{anoPesquisa}")]
        [HttpGet]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno formato PDF")]
        public Task<HttpResponseMessage> ExibirPDFQuitacaoAnualDebito([FromUri] string empresa, string matricula,string anoPesquisa)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                _log.SetEntrada();
                _log.SetNomeServico(GetNameCallMethod());
                _log.AddLog(GetClassNameCallMethod() + " ExibirPDFQuitacaoAnualDebito matricula:" + matricula + " anoPesquisa:" + anoPesquisa);
                SCN6ISQASend entrada = new SCN6ISQASend();
                entrada.empresa = empresa;
                entrada.matricula = matricula;
                entrada.anoPesquisa = anoPesquisa;
                var baseResponse = _clienteFacade.SCN6ISQA(entrada);
                SCN6ISQAReceive sCN6ISQAReceive = (SCN6ISQAReceive)baseResponse.Model;
                DateTime tempoInicio = DateTime.Now;
                DateTime tempoRotativa = DateTime.Now;
                Stream retorno = conectarServidorviaViaPost("QuitacaoAnualDebito/GerarPdfPost", converteQuebraPaginaHtml(sCN6ISQAReceive)).GetResponseStream();
                TimeSpan DuracaoConexaoRotativa = DateTime.Now.Subtract(tempoRotativa);
                double duracaoSegundosRotativa = Math.Round(DuracaoConexaoRotativa.TotalSeconds, 2);

                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    //Content = new ByteArrayContent(retorno.ToArray)
                    Content = new StreamContent(retorno)
                };
                response.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("inline")
                        {
                            FileName = "certidao.pdf"
                        };
                response.Content.Headers.ContentType =
                        new MediaTypeHeaderValue("application/pdf");
                TimeSpan DuracaoConexao = DateTime.Now.Subtract(tempoInicio);
                double duracaoSegundos = Math.Round(DuracaoConexao.TotalSeconds, 2);
                _log.AddLog(" Montar PDF certidao negativa debito: " + duracaoSegundos);
                _log.GravaLogAdc("MontarPDFCertidaoNegativa", tempoInicio);
            }
            catch (Exception e)
            {
                _log.AddLog("Erro exibe/CertidaoNegativaDebito/PDF: " + e.Message);
                _log.PringLog();
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            _log.PringLog();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }


        /// <summary>
        /// Buscar Mensagem Informativa
        /// </summary>
        /// <returns></returns>
        [Route("obtem/informativo")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(TrabParametroReceive))]
        public Task<HttpResponseMessage> ObterInformativo([FromBody] TrabParametroSend entrada)
        {
            return TrataRetornoFacade(_clienteFacade, "ObterInformativo", entrada);
        }

        /// <summary>
        /// Busca baixas de OS no Sicom para atualizar o Dynamics 365.
        /// </summary>
        [Route("corrige/protocolo")]
        [HttpGet]
        public Task<HttpResponseMessage> atualizaBaixaNoDynamics()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                baseResponse = _clienteFacade.corrigirProtocolos();
            }
            catch (Exception e)
            {
                baseResponse.Message = "Erro:" + e.Message;
                baseResponse.IsValid = false;
            }
            if (baseResponse.IsValid)
                response = Request.CreateResponse(HttpStatusCode.OK, baseResponse.Message);
            else
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, baseResponse.Message);
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            _log.PringLog();

            tsc.SetResult(response);
            return tsc.Task;
        }
    }
}
