using Copasa.Atende.Facade.Interfaces;
using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Util;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Copasa.Atende.WebService.Controllers
{
    /// <summary>
    /// API's para o serviço de fatura
    /// </summary>
    [RoutePrefix("api/fatura")]
    public class FaturaController : BaseApiController
    {
        private IFaturaFacade _faturaFacade;

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="faturaFacade">IFaturaFacade.</param>
        /// <param name="log">ILog</param>
        public FaturaController(IFaturaFacade faturaFacade, ILog log)
        {
            _faturaFacade = faturaFacade;
            _log = log;
        }

        /// <summary>
        /// Lista faturas em débito de um determinado cliente(SCN6ISFD)
        /// </summary>
        /// <param name="entrada">Matrícula do imóvel, identificador do cliente, período inicial(opcional) e empresa</param>
        [Route("lista/emDebito")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN6ISFDReceive))]
        public Task<HttpResponseMessage> listaFaturasEmDebito([FromBody] SCN6ISFDSend entrada)
        {
            return TrataRetornoFacade(_faturaFacade, "SCN6ISFD", entrada);
        }

        /// <summary>
        /// Lista faturas pagas de um determinado cliente(SCN6ISFP)
        /// </summary>
        /// <param name="entrada">Matrícula do imóvel, identificador do cliente, período de referência(opcional) e empresa.</param>
        [Route("lista/pagas")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN6ISFPReceive))]
        public Task<HttpResponseMessage> listaFaturasPagas([FromBody] SCN6ISFPSend entrada)
        {
            return TrataRetornoFacade(_faturaFacade, "SCN6ISFP", entrada);
        }

        /// <summary>
        /// Informa detalhes da fatura(SCN6ISDF)
        /// </summary>
        /// <param name="entrada">Matrícula do imóvel, período de referência e empresa</param>
        [Route("detalhar")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN6ISDFReceive))]
        public Task<HttpResponseMessage> detalhaFatura([FromBody] SCN6ISDFSend entrada)
        {
            return TrataRetornoFacade(_faturaFacade, "SCN6ISDF", entrada);
        }

        /// <summary>
        /// Exibe imagem do código de barras da fatura 
        /// </summary>
        /// <param name="numeroCodigoBarras">Número do código de barras.</param>
        [Route("exibe/codigoBarras/{numeroCodigoBarras}")]
        [HttpGet]
        //[Authorize]        
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno formato jpeg")]
        public Task<HttpResponseMessage> ExibirCodigoBarras([FromUri] string numeroCodigoBarras)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                DateTime tempoInicio = DateTime.Now;
                _log.SetEntrada();
                _log.SetNomeServico(GetNameCallMethod());
                Stream retorno = _faturaFacade.retornaCodigoBarras(numeroCodigoBarras);
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(retorno)
                };
                response.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("inline")
                        {
                            FileName = "codigoBarras.jpeg"
                        };
                response.Content.Headers.ContentType =
                        new MediaTypeHeaderValue("image/jpeg");
                TimeSpan DuracaoConexao = DateTime.Now.Subtract(tempoInicio);
                double duracaoSegundos = Math.Round(DuracaoConexao.TotalSeconds, 2);
            }
            catch (Exception e)
            {
                _log.AddLog("Serviço geração codigo barras numero: " + numeroCodigoBarras);
                _log.AddLog("  Erro exibe/codigoBarras: " + e.Message);
                _log.PringLog();
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        /// <summary>
        /// Exibe imagem do código de barras da fatura 
        /// </summary>
        /// <param name="numeroFatura">Número da fatura.</param>
        /// <param name="valorFatura">Valor da fatura.</param>
        [Route("exibe/QRCode/{numeroFatura}/{valorFatura}")]
        [HttpGet]
        //[Authorize]        
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno formato jpeg")]
        public Task<HttpResponseMessage> ExibirQRCode([FromUri] string numeroFatura,string valorFatura)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                Stream retorno = _faturaFacade.retornaQRCode(numeroFatura, valorFatura);
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(retorno)
                };
                response.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("inline")
                        {
                            FileName = "QRCode.jpeg"
                        };
                response.Content.Headers.ContentType =
                        new MediaTypeHeaderValue("image/jpeg");
            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        /// <summary>
        /// Exibe fatura em formato PDF a partir do número
        /// </summary>
        /// <param name="empresa">Empresa.</param>
        /// <param name="numeroFatura">Número da fatura.</param>
        [Route("exibe/PDF/{empresa}/{numeroFatura}")]
        [HttpGet]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno formato PDF")]
        public Task<HttpResponseMessage> ExibirFaturaPDF([FromUri] string empresa, string numeroFatura)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                SCN6EFEMSend sCN6EFEMSend = new SCN6EFEMSend();
                sCN6EFEMSend.numeroFatura = long.Parse(numeroFatura);
                sCN6EFEMSend.empresa = empresa;
                WebResponse retorno = conectarServidorviaViaPost("SegundaViaFatura/GerarPdfPost", converteQuebraPaginaHtml(sCN6EFEMSend));
                _log.SetEntrada();
                _log.SetNomeServico(GetNameCallMethod());
                _log.AddLog(GetClassNameCallMethod() + " " + GetNameCallMethod() + "- ExibirFaturaPDF: numeroFatura" + numeroFatura);
                /*
                        DateTime tempoInicio = DateTime.Now;
                        WebResponse responseFacade = _faturaFacade.retornaFaturaPDF(numeroFatura);
                        var type = responseFacade.ContentType;
                        _log.AddLog(type.ToString());
                        Stream retorno = responseFacade.GetResponseStream();
                        */
                if ("application/pdf".Equals(retorno.ContentType.ToString()))
                {
                    response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        //Content = new ByteArrayContent(retorno.ToArray)
                        Content = new StreamContent(retorno.GetResponseStream())
                    };
                    response.Content.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("inline")
                            {
                                FileName = "contaCopasa.pdf"
                            };
                    response.Content.Headers.ContentType =
                            new MediaTypeHeaderValue("application/pdf");
                }
                else
                {
                    using (StreamReader reader = new StreamReader(retorno.GetResponseStream()))
                    {
                        string texto = reader.ReadToEnd();
                        Stream retornoErro = conectarServidorviaViaPost("SegundaViaFatura/ExibirMensagem", texto);

                        response = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new StreamContent(retornoErro)
                        };
                        response.Content.Headers.ContentDisposition =
                                new ContentDispositionHeaderValue("inline")
                                {
                                    FileName = numeroFatura+".pdf"
                                };
                        response.Content.Headers.ContentType =
                                new MediaTypeHeaderValue("application/pdf");
                    }
                }
            }
            catch (Exception e)
            {
                _log.AddLog("Erro exibe/PDF: " + e.Message);
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            _log.PringLog();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        /// <summary>
        /// Download da fatura em formato PDF a partir do número
        /// </summary>
        /// <param name="empresa">Empresa.</param>
        /// <param name="numeroFatura">Número da fatura.</param>
        [Route("download/PDF/{empresa}/{numeroFatura}")]
        [HttpGet]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno formato PDF")]
        public Task<HttpResponseMessage> DownloadFaturaPDF([FromUri]string empresa, string numeroFatura)
        {
            ILog log = getLog();
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                SCN6EFEMSend sCN6EFEMSend = new SCN6EFEMSend();
                sCN6EFEMSend.numeroFatura = long.Parse(numeroFatura);
                sCN6EFEMSend.empresa = empresa;
                WebResponse retorno = conectarServidorviaViaPost("SegundaViaFatura/GerarPdfPost", converteQuebraPaginaHtml(sCN6EFEMSend));
                _log.SetEntrada();
                _log.SetNomeServico(GetNameCallMethod());
                _log.AddLog(GetClassNameCallMethod() + " " + GetNameCallMethod() + "- DownloadFaturaPDF: numeroFatura" + numeroFatura);
                /*
                DateTime tempoInicio = DateTime.Now;
                WebResponse responseFacade = _faturaFacade.retornaFaturaPDF(numeroFatura);
                var type = responseFacade.ContentType;
                _log.AddLog(type.ToString());
                Stream retorno = responseFacade.GetResponseStream();
                */
                if ("application/pdf".Equals(retorno.ContentType.ToString()))
                {

                    response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        //Content = new ByteArrayContent(retorno.ToArray)
                        Content = new StreamContent(retorno.GetResponseStream())
                    };
                    response.Content.Headers.ContentDisposition =
                            new ContentDispositionHeaderValue("attachment")
                            {
                                FileName = numeroFatura+".pdf"
                            };
                    response.Content.Headers.ContentType =
                            new MediaTypeHeaderValue("application/octet-stream");
                }
                else
                {
                    using (StreamReader reader = new StreamReader(retorno.GetResponseStream()))
                    {
                        string texto = reader.ReadToEnd();
                        Stream retornoErro = conectarServidorviaViaPost("SegundaViaFatura/ExibirMensagem", texto);

                        response = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new StreamContent(retornoErro)
                        };
                        response.Content.Headers.ContentDisposition =
                                new ContentDispositionHeaderValue("attachment")
                                {
                                    FileName = "contaCopasa.pdf"
                                };
                        response.Content.Headers.ContentType =
                                new MediaTypeHeaderValue("application/pdf");
                    }
                }
            }
            catch (Exception e)
            {
                log.AddLog("Erro download/PDF: " + e.Message);
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            _log.PringLog();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        /// <summary>
        /// Consulta parcelamento de débito(SCN6ISPD)
        /// </summary>
        [Route("consulta/parcelamentoDebito")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN6ISPDReceive))]
        public Task<HttpResponseMessage> consultaParcelamentoDebito([FromBody] SCN6ISPDSend entrada)
        {
            return TrataRetornoFacade(_faturaFacade, "SCN6ISPD", entrada);
        }

        /// <summary>
        /// Gera parcelamento de débito(SCN6ISGP)
        /// </summary>
        [Route("gera/parcelamentoDebito")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN6ISGPReceive))]
        public Task<HttpResponseMessage> geraParcelamentoDebito([FromBody] SCN6ISGPSend entrada)
        {
            return TrataRetornoFacade(_faturaFacade, "SCN6ISGP", entrada);
        }

        /// <summary>
        /// Teste stress chamadas .
        /// </summary>
        //[Route("teste/stress/{quantidade}")]
        //[HttpGet]
        public Task<HttpResponseMessage> TesteStress1([FromUri] int quantidade)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                _log.InciaRajada(quantidade);
                TesteStress testeStress = new TesteStress();
                testeStress.Testar(quantidade,1, _log);
                SCN4ISU1Receive sCN4ISU1Receive = new SCN4ISU1Receive();
                sCN4ISU1Receive.descricaoRetorno = "Rajada de conexões enviadas";
                baseResponse.Model = sCN4ISU1Receive;
            }
            catch (Exception e)
            {
                baseResponse.Message = "Erro:" + e.Message;
                baseResponse.IsValid = false;
            }
            //_log.PrintLogTempoMedio();

            if (baseResponse.IsValid)
                response = Request.CreateResponse(HttpStatusCode.OK, baseResponse.Message);
            else
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, baseResponse.Message);
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        /// <summary>
        /// Teste stress chamadas .
        /// </summary>
        //[Route("teste/Pagas/stress/{quantidade}")]
        //[HttpGet]
        public Task<HttpResponseMessage> TesteStress2([FromUri] int quantidade)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                _log.InciaRajada(quantidade);
                TesteStress testeStress = new TesteStress();
                testeStress.Testar(quantidade, 2, _log);
                SCN4ISU1Receive sCN4ISU1Receive = new SCN4ISU1Receive();
                sCN4ISU1Receive.descricaoRetorno = "Rajada de conexões enviadas";
                baseResponse.Model = sCN4ISU1Receive;
            }
            catch (Exception e)
            {
                baseResponse.Message = "Erro:" + e.Message;
                baseResponse.IsValid = false;
            }
            _log.PrintLogTempoMedio();

            if (baseResponse.IsValid)
                response = Request.CreateResponse(HttpStatusCode.OK, baseResponse.Message);
            else
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, baseResponse.Message);
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        /// <summary>
        /// Teste stress chamadas .
        /// </summary>
        //[Route("teste/EmDebito/stress/{quantidade}")]
        //[HttpGet]
        public Task<HttpResponseMessage> TesteStress3([FromUri] int quantidade)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                _log.InciaRajada(quantidade);
                TesteStress testeStress = new TesteStress();
                testeStress.Testar(quantidade, 3, _log);
                SCN4ISU1Receive sCN4ISU1Receive = new SCN4ISU1Receive();
                sCN4ISU1Receive.descricaoRetorno = "Rajada de conexões enviadas";
                baseResponse.Model = sCN4ISU1Receive;
            }
            catch (Exception e)
            {
                baseResponse.Message = "Erro:" + e.Message;
                baseResponse.IsValid = false;
            }
            _log.PrintLogTempoMedio();

            if (baseResponse.IsValid)
                response = Request.CreateResponse(HttpStatusCode.OK, baseResponse.Message);
            else
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, baseResponse.Message);
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        /// <summary>
        /// Teste stress chamadas .
        /// </summary>
        //[Route("teste/PagasEDebito/stress/{quantidade}")]
        //[HttpGet]
        public Task<HttpResponseMessage> TesteStress4([FromUri] int quantidade)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                _log.InciaRajada(quantidade);
                TesteStress testeStress = new TesteStress();
                testeStress.Testar(quantidade, 4, _log);
                SCN4ISU1Receive sCN4ISU1Receive = new SCN4ISU1Receive();
                sCN4ISU1Receive.descricaoRetorno = "Rajada de conexões enviadas";
                baseResponse.Model = sCN4ISU1Receive;
            }
            catch (Exception e)
            {
                baseResponse.Message = "Erro:" + e.Message;
                baseResponse.IsValid = false;
            }
            _log.PrintLogTempoMedio();

            if (baseResponse.IsValid)
                response = Request.CreateResponse(HttpStatusCode.OK, baseResponse.Message);
            else
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, baseResponse.Message);
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        /// <summary>
        /// Lista notas fiscais de um fatura(SCN6ISNF)
        /// </summary>
        /// <param name="entrada">Número da fatura.</param>
        [Route("lista/notasFiscais")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN6ISNFReceive))]
        public Task<HttpResponseMessage> listaNotaFiscal([FromBody] SCN6ISNFSend entrada)
        {
            return TrataRetornoFacade(_faturaFacade, "SCN6ISNF", entrada);
        }

        /// <summary>
        /// Tarifa proporcional(SCN6ISTP)
        /// </summary>
        [Route("tarifaProporcional/exibe")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN6ISTPReceive))]
        public Task<HttpResponseMessage> exibeTarifaProporcional([FromBody] SCN6ISTPSend entrada)
        {
            return TrataRetornoFacade(_faturaFacade, "SCN6ISTP", entrada);
        }

        /// <summary>
        /// Simula fatura(SCN6ISCF)
        /// </summary>
        [Route("simula")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN6ISCFReceive))]
        public Task<HttpResponseMessage> simula([FromBody] SCN6ISCFSend entrada)
        {
            return TrataRetornoFacade(_faturaFacade, "SCN6ISCF", entrada);
        }
    }
}
