using Copasa.Atende.Facade.Interfaces;
using Copasa.Atende.Model;
using Copasa.Atende.Model.Broker;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Model.Dyn365;
using Copasa.Atende.Model.Dyn365.Localidade;
using Copasa.Atende.Model.Dyn365.Protocolo;
using Copasa.Atende.Util;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Copasa.Atende.WebService.Controllers
{
    /// <summary>
    /// API's para o serviço de cliente do dynamics 365
    /// </summary>
    [RoutePrefix("api/crm/cliente")]
    public class DClienteController : BaseApiController
    {
        private IDClienteFacade _dclienteFacade;
        private ICertidaoNegativaDebitoFacade _certidaoNegativaDebitoFacade;
        private IClienteFacade _clienteFacade;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="dclienteFacade"></param>
        /// <param name="certidaoNegativaDebitoFacade"></param>
        /// <param name="clienteFacade"></param>
        /// <param name="log"></param>
        public DClienteController(IDClienteFacade dclienteFacade, ICertidaoNegativaDebitoFacade certidaoNegativaDebitoFacade, IClienteFacade clienteFacade, ILog log)
        {
            _dclienteFacade = dclienteFacade;
            _log = log;
            _certidaoNegativaDebitoFacade = certidaoNegativaDebitoFacade;
            _clienteFacade = clienteFacade;
        }

        /// <summary>
        /// Consulta faturas pagas de uma matricula/identificador
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("fatura/lista/pagas")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN6ISFPReceive))]
        public Task<HttpResponseMessage> FaturasPagas([FromBody] DFaturasSend entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "DFaturas", entrada);
        }

        /// <summary>
        /// Consulta o historico de consumo de uma matricula/identificador
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("fatura/lista/historicoConsumo")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN5ISHCReceive))]
        public Task<HttpResponseMessage> HistoricoConsumo([FromBody] DFaturasSend entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "DFaturas", entrada);
        }

        /// <summary>
        /// Consulta as faturas em débito de uma matricula/identificador
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("fatura/lista/emDebito")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN6ISFDReceive))]
        public Task<HttpResponseMessage> EmDebito([FromBody] DFaturasSend entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "DFaturas", entrada);
        }

        /// <summary>
        /// Exibe as 12 ultimas faturas de um cliente
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("fatura/lista/ultimas")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(DUltimasFaturasReceive))]
        public Task<HttpResponseMessage> UltimasFaturas([FromBody] DFaturasSend entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "DUltimasFaturas", entrada);
        }

        /// <summary>
        /// Lista os identificadores e matriculas associadas ao cpf/cnpj
        /// </summary>
        /// <remarks>
        /// Origens: 
        /// APP Copasa Atende – APP |
        /// PRODEMGE MGAPP - MG |
        /// PRODEMGE WebMGAPP – WMG |
        /// PRODEMGE Toten – TMG |
        /// IMPLY MGAPP – IMP |
        /// IMPLY MGAPP Empresarial – IEP |
        /// IMPLY WebMGAPP – WIM |
        /// IMPLY Toten – TIM |
        /// </remarks>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("usuario/lista")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(DListaUsuarioReceive))]
        public Task<HttpResponseMessage> ListaUsuario([FromBody] DListaUsuarioSend entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "ListaUsuario", entrada);
        }

        /// <summary>
        /// Verifica se o cpf ou cnpj existe no Dynamics 365.
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("usuario/valida")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(DValidaCpfCnpjReceive))]
        public Task<HttpResponseMessage> ValidaUsuario([FromBody] DValidaCpfCnpjSend entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "ValidaCpfCnpjDyn365", entrada);
        }

        /// <summary>
        /// Consulta dados de um usuário
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("usuario/consulta")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(DConsultaUsuarioReceive))]
        public Task<HttpResponseMessage> ConsultaUsuario([FromBody] DValidaCpfCnpjSend entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "ConsultaUsuario", entrada);
        }

        ///// <summary>
        ///// Consulta dados de um usuário
        ///// </summary>
        ///// <param name="entrada"></param>
        ///// <returns></returns>
        //[Route("usuario/autentica")]
        //[HttpPost]
        //[SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(BaseModelAzureCopaUserReceive))]
        //public Task<HttpResponseMessage> DAutenticausuario([FromBody] Dyn365AuthenticateUserSend entrada)
        //{
        //    return TrataRetornoFacade(_dclienteFacade, "DAutenticaUsuario", entrada);
        //}

        /// <summary>
        /// Cria um protocolo base
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("criaProtocolo")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(DCriaProtocoloReceive))]
        public Task<HttpResponseMessage> CriaProtocolo([FromBody] DCriaProtocoloSend entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "CriaProtocolo", entrada);
        }

        /// <summary>
        /// Consulta um identificador no Dyn365, 
        /// </summary>
        /// <param name="entrada">código do identificador</param>
        /// <returns>retornando id do identificador</returns>
        [Route("identificador/consulta")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(DConsultaIdentificadorReceive))]
        public Task<HttpResponseMessage> ConsultaIdentificador([FromBody] DConsultaIdentificadorSend entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "ConsultaIdentificador", entrada);
        }

        /// <summary>
        /// Cria um Novo Identificador no Microsoft Dynamics 365.
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("identificador/cadastra")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(BaseModelAzureCopaUserReceive))]
        public Task<HttpResponseMessage> CadastraIdentificador([FromBody] DCadastraIdentificadorSend entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "CadastraIdentificador", entrada);
        }

        /// <summary>
        /// Lista os identificadores associados ao id do cpf/cnpj no D365
        /// </summary>
        /// <param name="entrada">Id do cpf/cnpj</param>
        /// <returns></returns>
        [Route("identificador/lista")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(DListaIdentificadorReceive))]
        public Task<HttpResponseMessage> ListaIdentificadorDyn365([FromBody] DListaIdentificadorSend entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "ListaIdentificador", entrada);
        }

        /// <summary>
        /// Associa um Identificador ao cpf/cnpj no D365
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("identificador/associa")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(DAssociaIdentificadorReceive))]
        public Task<HttpResponseMessage> AssociaIdentificadorDyn365([FromBody] DAssociaIdentificadorSend entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "DAssociaIdentificador", entrada);
        }

        /// <summary>
        ///  Desassocia um identificador a um cpf/cnpj e um identificador
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("identificador/desassocia")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(DMensagemRetornoReceive))]
        public Task<HttpResponseMessage> DesassociaIdentificador([FromBody] DDesassociaIdentificadorSend entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "DesassociaIdentificador", entrada);
        }

        /// <summary>
        /// Retorna a certidão negativa de um usuario
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("certidaoNegativa")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN6ISCNView))]
        public Task<HttpResponseMessage> CertidaoNegativa([FromBody] DCertidaoNegativaSend entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "CertidaoNegativa", entrada);
        }

        /// <summary>
        /// Retorna a certidão negativa de um usuario em formato Pdf
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("certidaoNegativa/pdf")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno formato PDF")]
        public Task<HttpResponseMessage> CertidaoNegativaPDF([FromBody] DCertidaoNegativaSend entrada)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                //chama um facade. que chama a função certidaonegativa com o paramentro de só criar o protocolo referente ao serviço
                //BaseResponse baseResponse = TrataRetornoFacade(_dclienteFacade, "CertidaoNegativa", entrada);
                /*
                SCN6ISCNSend sCN6ISCNSend = new SCN6ISCNSend();
                sCN6ISCNSend.empresa = entrada.Empresa;
                sCN6ISCNSend.cpfCnpj = entrada.CpfCnpjLogin;
                var baseResponse = _certidaoNegativaDebitoFacade.SCN6ISCN(sCN6ISCNSend);
                */
                var baseResponse = _dclienteFacade.CertidaoNegativa(entrada);
                SCN6ISCNView sCN6ISCNReceive = (SCN6ISCNView)baseResponse.Model;
                if (sCN6ISCNReceive.identificadores.Count == 0)
                {
                    sCN6ISCNReceive.mensagemRetorno = "Não há certidões negativas";
                }

                System.IO.Stream retorno = conectarServidorviaViaPost("CertidaoNegativaCpf/GerarPdfPost", converteQuebraPaginaHtml(sCN6ISCNReceive)).GetResponseStream();

                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    //Content = new ByteArrayContent(retorno.ToArray)
                    Content = new StreamContent(retorno)
                };
                response.Content.Headers.ContentDisposition =
                        new System.Net.Http.Headers.ContentDispositionHeaderValue("inline")
                        {
                            FileName = "contaCopasa.pdf"
                        };
                response.Content.Headers.ContentType =
                        new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            }
            catch (Exception e)
            {
                ILog log = getLog();
                log.AddLog("Erro exibe/CertidaoNegativaDebito/PDF: " + e.Message);
                log.PringLog();
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        /// <summary>
        /// Retorna a lista de localidades
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("lista/localidade")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(DLocalidadeReceive))]
        public Task<HttpResponseMessage> ListaLocalidade([FromBody] DLocalidadeSend entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "BuscaLocalidade", entrada);
        }

        /// <summary>
        /// Retorna a lista de bairros de uma localidade
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("lista/bairro")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(DBairroReceive))]
        public Task<HttpResponseMessage> ListaBairro([FromBody] DBairroSend entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "BuscaBairro", entrada);
        }

        /// <summary>
        /// Retorna a lista de logradouros de um bairro
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("lista/logradouro")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(DLogradouroReceive))]
        public Task<HttpResponseMessage> ListaLogradouro([FromBody] DLogradouroSend entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "BuscaLogradouro", entrada);
        }

        /// <summary>
        /// Valida Endereço
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("valida/endereco")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(DEnderecoReceive))]
        public Task<HttpResponseMessage> ValidaEndereco([FromBody] DEnderecoSend entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "ValidaEndereco", entrada);
        }

        /// <summary>
        /// Retorna a lista de Pavimentações de um Subtipo
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("lista/pavimentacao")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(DPavimentacaoReceive))]
        public Task<HttpResponseMessage> ListaPavimentacao([FromBody] DPavimentacaoSend entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "BuscaPavimentacao", entrada);
        }

        /// <summary>
        /// Solicitaçação de serviço
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("solicitacaoServico/solicita")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN4CRSSReceive))]
        public Task<HttpResponseMessage> SolicitacaoServico([FromBody] DSolicitaServicoSend entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "SolicitaServico", entrada);
        }


        /// <summary>
        /// Solicitaçação de serviço
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("modifica/statusFaturaPorEmail")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(DMensagemRetornoReceive))]
        public Task<HttpResponseMessage> ContasPorEmail([FromBody] DContasEmailSend entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "ContasPorEmail", entrada);
        }

        /// <summary>
        /// Confirmação de recebimento por email das faturas.
        /// </summary>
        /// <param name="Protocolo"></param>
        /// <param name="Email"></param>
        /// <returns></returns>
        [Route("confirma/statusFaturaPorEmail/{Protocolo}/{Email}")]
        [HttpGet]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(DMensagemRetornoReceive))]
        public Task<HttpResponseMessage> ConfirmaContasEmail([FromUri] string Protocolo, [FromUri] string Email)
        {
            DConfirmaEmailSend dConfirmaEmailSend = new DConfirmaEmailSend() { Email = Email, ProtocoloId = Protocolo };
            return TrataRetornoFacade(_dclienteFacade, "ConfirmaContasEmail", dConfirmaEmailSend);
        }

        /// <summary>
        /// Atualiza e-mail e telefones do usuário Copasa NO SICOM pelo Broker(SCN4FTE1).
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("usuario/contatos/altera")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(UsuarioCopasaReceive))]
        public Task<HttpResponseMessage> AlteraContatoUsuario([FromBody] UsuarioCopasaModel entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "AtualizacaoCadastral", entrada);
        }

        /// <summary>
        /// Historico de consumo com barras, caso tenha vinculo com faturas em débito
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("usuario/historicoConsumo/barras")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(DHistoricoConsumoBarra))]
        public Task<HttpResponseMessage> DHistoricoConsumoBarra([FromBody] DFaturasSend entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "DHistoricoConsumoBarra", entrada);
        }

        /// <summary>
        /// Envio de fatura por email
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("usuario/fatura/enviaEmail")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(DMensagemRetornoReceive))]
        public Task<HttpResponseMessage> DEnviaFaturaEmail([FromBody] DEnviaFaturaEmailSend entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "EnviaFaturaEmail", entrada);
        }

        /// <summary>
        /// Lista Status Fatura por Email Alteráveis
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("statusFaturaPorEmail/lista")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(DListaStatusFaturaEmailReceive))]
        public Task<HttpResponseMessage> DListaStatusFaturaEmail([FromBody] DListaStatusFaturaSend entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "ListaStatusFaturaEmail", entrada);
        }

        /// <summary>
        /// Solicita serviço de falta d'agua ou de cancelamento de acordo com a situação atual
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("verifica/faltaDagua")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(Dyn365SomenteMensagem))]
        public Task<HttpResponseMessage> VerificaFaltaDagua([FromBody] VerificaFaltaDaguaSend entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "VerificaFaltaDagua", entrada);
        }

        /// <summary>
        /// gera um protocolo
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("protocolo/gera")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(DCriaProtocoloReceive))]
        public Task<HttpResponseMessage> GeraProtocolo([FromBody] GeraProtocoloSend entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "GeraProtocolo", entrada);
        }

        /// <summary>
        /// Pesquisa por Protocolo
        /// </summary>
        /// <param name="entrada"></param>
        // <returns></returns>
        [Route("protocolo/historico/detalhe")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(DHistoricoServicoDetalheReceive))]
        public Task<HttpResponseMessage> GetHistoricoServicoDetalhe([FromBody] DHistoricoServicoDetalheSend entrada)
        {
            // TODO criar detalhe do GetHistoricoServicoDetalhe
            return TrataRetornoFacade(_dclienteFacade, "GetHistoricoServicoDetalhe", entrada);
        }

        /// <summary>
        /// Historico de serviço
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("protocolo/historico")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(DHistoricoServicoReceiveApp))]
        public Task<HttpResponseMessage> GetHistoricoServico([FromBody] DHistoricoServicoSendApp entrada)
        {
            return TrataRetornoFacade(_dclienteFacade, "GetHistoricoServico", entrada);
        }




        /// <summary>
        /// Recebe os comentarios
        /// </summary>
        /// <param name="id"></param>
        [Route("protocolo/comentarios/{id}")]
        [HttpGet]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(DComentariosReceive))]
        public Task<HttpResponseMessage> GetComentarios([FromBody] DComentariosSend id )
        {
            return TrataRetornoFacade(_dclienteFacade, "GetComentarios", id);
        }

        /// <summary>
        /// Cria um comentario
        /// </summary>
        /// <param name="dComentariosSend"></param>
        [Route("protocolo/comentarios/cria")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(DComentariosReceive))]
        public Task<HttpResponseMessage> CriaComentario([FromBody] DComentariosSend dComentariosSend)
        {
            return TrataRetornoFacade(_dclienteFacade, "GetComentarios", dComentariosSend);
        }
    }
}
