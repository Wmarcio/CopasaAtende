using Copasa.Atende.Facade.Interfaces;
using Copasa.Atende.Model;
using Copasa.Atende.Util;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Copasa.Atende.WebService.Controllers
{
    /// <summary>
    /// API's para o serviço de atendimento
    /// </summary>
    [RoutePrefix("api/atendimento")]
    public class AtendimentoController : BaseApiController
    {
        private IClienteFacade _clienteFacade;
        private IInformarLeituraFacade _informarLeituraFacade;
        private IReligacaoFacade _religacaoFacade;
        private IAtendimentoFacade _atendimentoFacade;

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="clienteFacade">IClienteFacade.</param>
        /// <param name="informarLeituraFacade">IInformarLeituraFacade</param>
        /// <param name="religacaoFacade">IReligacaoFacade</param>
        /// <param name="atendimentoFacade">IAtendimentoFacade</param>
        /// <param name="log">ILog</param>
        public AtendimentoController(IClienteFacade clienteFacade, IInformarLeituraFacade informarLeituraFacade, IReligacaoFacade religacaoFacade, IAtendimentoFacade atendimentoFacade, ILog log)
        {
            _clienteFacade = clienteFacade;
            _informarLeituraFacade = informarLeituraFacade;
            _religacaoFacade = religacaoFacade;
            _atendimentoFacade = atendimentoFacade;
            _log = log;
        }

        /// <summary>
        /// Verifica situação de serviços solicitados pelo cliente
        /// </summary>
        /// <param name="entrada">CPF ou CNPJ e empresa.</param>
        [Route("verifica/faltaDagua")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(TrabPesquisaFaltaAguaReceive))]
        public Task<HttpResponseMessage> getSituacaoSS([FromBody] TrabPesquisaFaltaAguaSend entrada)
        {
            return TrataRetornoFacade(_clienteFacade, "getSituacaoMatriculas", entrada);
        }

        /// <summary>
        /// Verifica falta d'água de um endereço
        /// </summary>
        /// <param name="entrada">CPF ou CNPJ e empresa.</param>
        [Route("verifica/faltaDaguaEndereco")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(TrabPesquisaFaltaAguaReceive))]
        public Task<HttpResponseMessage> getSituacaoSSEndereco([FromBody] SCN3ISMTSend entrada)
        {
            return TrataRetornoFacade(_clienteFacade, "getSituacaoMatriculasEndereco", entrada);
        }

        /// <summary>
        /// Busca Dados para informar Leitura
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("informarLeitura/buscarDados")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN5IS01Receive))]
        public Task<HttpResponseMessage> BuscarDadosInformarLeitura([FromBody] SCN5IS01Send entrada)
        {
            return TrataRetornoFacade(_informarLeituraFacade, "SCN5IS01", entrada);
        }

        /// <summary>
        /// Gravar dados informar leitura
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("informarLeitura/gravar")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN5IS03Receive))]
        public Task<HttpResponseMessage> GravarDadosLeitura([FromBody] SCN5IS03Send entrada)
        {
            return TrataRetornoFacade(_informarLeituraFacade, "SCN5IS03", entrada);
        }

        /// <summary>
        /// Buscar dados para informar religação de água
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("religacao/buscarDados")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN4ISRLReceive))]
        public Task<HttpResponseMessage> BuscaDados([FromBody] SCN4ISRLSend entrada)
        {
            return TrataRetornoFacade(_religacaoFacade, "SCN4ISRL", entrada);
        }

        /// <summary>
        /// Buscar dados do parcelamento para religar
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("religacao/buscarDadosParcelamento")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN4ISCPReceive))]
        public Task<HttpResponseMessage> BuscaDadosParcelamento([FromBody] SCN4ISCPSend entrada)
        {
            return TrataRetornoFacade(_religacaoFacade, "SCN4ISCP", entrada);
        }

        /// <summary>
        /// Grava solicitacao religação
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("religacao/gravarSolicitacao")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN4ISREReceive))]
        public Task<HttpResponseMessage> BuscaDadosReligacao([FromBody] SCN4ISRESend entrada)
        {
            return TrataRetornoFacade(_religacaoFacade, "SCN4ISRE", entrada);
        }

        /// <summary>
        /// Busca todos os dados sobre religação de água
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("religacao/religacao")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(TrabBuscaReligacaoReceive))]
        public Task<HttpResponseMessage> BuscaReligacao([FromBody] TrabBuscaReligacaoSend entrada)
        {
            return TrataRetornoFacade(_religacaoFacade, "BuscaReligacao", entrada);
        }

        /// <summary>
        /// Lista agências atendimento
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("lista/agenciaAtendimento")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN6ISAAReceive))]
        public Task<HttpResponseMessage> ListaAgenciasAtendimento([FromBody] SCN6ISAASend entrada)
        {
            return TrataRetornoFacade(_atendimentoFacade, "SCN6ISAA", entrada);
        }

        /// <summary>
        /// Calendário faturamento(SCN6ISDL)
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("exibe/calendarioFaturamento")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN6ISDLReceive))]
        public Task<HttpResponseMessage> getCalendarioFaturamento([FromBody] SCN6ISDLSend entrada)
        {
            return TrataRetornoFacade(_atendimentoFacade, "SCN6ISDL", entrada);
        }

        /// <summary>
        /// Onde pagar a conta(SCN7ISOP)
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("lista/ondePagarConta")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN7ISOPReceive))]
        public Task<HttpResponseMessage> getOndePagarConta([FromBody] SCN7ISOPSend entrada)
        {
            return TrataRetornoFacade(_atendimentoFacade, "SCN7ISOP", entrada);
        }

        /// <summary>
        /// Busca informações de  uma matrícula
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("lista/informacaoMatricula")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN4ISCSReceive))]
        public Task<HttpResponseMessage> BuscaInformacoesMatricula([FromBody] SCN4ISCSSend entrada)
        {
            return TrataRetornoFacade(_atendimentoFacade, "SCN4ISCS", entrada);
        }

        /// <summary>
        /// Lista hidrômetros de uma matrícula(SCNISPS1)
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("lista/hidrometro")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCNISPS1Receive))]
        public Task<HttpResponseMessage> ListaHidrometros([FromBody] SCNISPS1Send entrada)
        {
            return TrataRetornoFacade(_atendimentoFacade, "SCNISPS1", entrada);
        }

        /// <summary>
        /// Exibe unidade de destino de uma matricula, localidade e bairro (SCN4CRUN)
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("exibe/unidadeDestino")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN4CRUNReceive))]
        public Task<HttpResponseMessage> ExibeUnidadeDestino([FromBody] SCN4CRUNSend entrada)
        {
            return TrataRetornoFacade(_atendimentoFacade, "SCN4CRUN", entrada);
        }
    }
}
