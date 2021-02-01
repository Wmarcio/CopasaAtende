using Copasa.Atende.Facade.Interfaces;
using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Util;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Copasa.Atende.WebService.Controllers
{
    /// <summary>
    /// API's para o serviços de ordens de serviços
    /// </summary>
    [RoutePrefix("api")]
    public class SolicitacaoServicoController : BaseApiController
    {
        private IServicoOperacionalFacade _servicoOperacionalFacade;

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="servicoOperacionalFacade">IServicoOperacionalFacade.</param>
        /// <param name="log">ILog</param>
        public SolicitacaoServicoController(IServicoOperacionalFacade servicoOperacionalFacade, ILog log)
        {
            _servicoOperacionalFacade = servicoOperacionalFacade;
            _log = log;
        }

        /// <summary>
        /// Recebe alertas de prioridade de serviços para atualização(SCN4CRAL)
        /// </summary>
        /// <param name="entrada">Número da ordem servico, evento, origem, observação do evento, número do protocolo e empresa</param>
        [Route("eventoPrioridade/gera")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN4CRALReceive))]
        public Task<HttpResponseMessage> atualizaAlertaPrioridade([FromBody] SCN4CRALSend entrada)
        {
            return TrataRetornoFacade(_servicoOperacionalFacade,"SCN4CRAL",entrada);
        }

        /// <summary>
        /// Solicita abertura de Ordem de serviço(SCN4CRSS)
        /// </summary>
        /// <param name="entrada"></param>
        [Route("ordemServico/solicita")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(Dyn365SomenteMensagem))]
        public Task<HttpResponseMessage> solicitaOrdemServico([FromBody] SCN4CRSSSend entrada)
        {
            return TrataRetornoFacade(_servicoOperacionalFacade,"SCN4CRSS",entrada);
        }

        /// <summary>
        /// Solicita abertura de Ordem de serviço adicional(SCN4CREX)
        /// </summary>
        /// <param name="entrada"></param>
        [Route("ordemServicoAdicional/solicita")]
        [HttpPost]
        //[Authorize]
        public Task<HttpResponseMessage> solicitaOrdemServicoAdc([FromBody] SCN4CREXSend entrada)
        {
            return TrataRetornoFacade(_servicoOperacionalFacade, "SCN4CREX", entrada);
        }

        /// <summary>
        /// Lista ordens de serviço de uma solicitação de serviço(SCN4ISOR)
        /// </summary>
        /// <param name="entrada"></param>
        [Route("ordemServico/lista")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN4ISORReceive))]
        public Task<HttpResponseMessage> listaOrdemServico([FromBody] SCN4ISORSend entrada)
        {
            return TrataRetornoFacade(_servicoOperacionalFacade,"SCN4ISOR",entrada );
        }

        /// <summary>
        /// Lista solicitações de serviços de uma matrícula(SCN4ISSS)
        /// </summary>
        /// <param name="entrada"></param>
        [Route("solicitacaoServico/lista")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN4ISSSReceive))]
        public Task<HttpResponseMessage> listaSolicitacaoServico([FromBody] SCN4ISSSSend entrada)
        {
            return TrataRetornoFacade(_servicoOperacionalFacade,"SCN4ISSS",entrada);
        }

        /// <summary>
        /// Cancela Solicitação de serviço(SCN4CASS)
        /// </summary>
        /// <param name="entrada"></param>
        [Route("solicitacaoServico/cancelar")]
        [HttpPost]
        //[Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN4CASSReceive))]
        public Task<HttpResponseMessage> cancelarOrdemServico([FromBody] SCN4CASSSend entrada)
        {
            return TrataRetornoFacade(_servicoOperacionalFacade,"SCN4CASS",entrada);
        }

        /// <summary>
        /// Busca baixas de OS no Sicom para atualizar o Dynamics 365.
        /// </summary>
        [Route("solicitacaoServico/atualiza/baixaOS")]
        [HttpGet]
        public Task<HttpResponseMessage> atualizaBaixaNoDynamics()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                baseResponse = _servicoOperacionalFacade.AtualizaDynamicsBaxiasOS();
            }
            catch (Exception e )
            {
                baseResponse.Message = "Erro:"+e.Message;
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

        /// <summary>
        /// Busca baixas de OS no Sicom para atualizar o Dynamics 365.
        /// </summary>
        [Route("solicitacaoServico/atualiza/geradas")]
        [HttpGet]
        public Task<HttpResponseMessage> AtualizaDynamicsOSGeradas()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                baseResponse = _servicoOperacionalFacade.AtualizaDynamicsOSGeradas();
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

        /// <summary>
        /// Gera alteração de economias(SCN4ISAE)
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [Route("solicitacaoServico/economiaCategoria")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN4ISAEReceive))]
        public Task<HttpResponseMessage> GeraAlteracaoEconomias([FromBody] SCN4ISAESend entrada)
        {
            return TrataRetornoFacade(_servicoOperacionalFacade, "SCN4ISAE", entrada);
        }

        /// <summary>
        /// Lista solicitações de serviços do Dynamics
        /// </summary>
        /// <param name="entrada"></param>
        //[Route("solicitacaoServicoDyn/lista")]
        //[HttpPost]
        ////[Authorize]
        //[SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(ListaSolicitacaoServicoReceive))]
        //public Task<HttpResponseMessage> ListaSolicitacaoServicoDyn([FromBody] SCN4ISSSSend entrada)
        //{
        //    return TrataRetornoFacade(_servicoOperacionalFacade, "ListaSolicitacaoServicoDyn", entrada);
        //}
    }
}
