using Copasa.Atende.Facade.Interfaces;
using Copasa.Atende.Model;
using Copasa.Atende.Model.Dyn365.Protocolo;
using Copasa.Atende.Util;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Copasa.Atende.WebService.Controllers
{
    /// <summary>
    /// API's para o serviços disponibilizados para a URA
    /// </summary>
    [RoutePrefix("api/ura")]
    public class URAController : BaseApiController
    {
        private IURAFacade _uRAFacade;

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="uRAFacade">IURAFacade.</param>
        /// <param name="log">ILog</param>
        public URAController(IURAFacade uRAFacade, ILog log)
        {
            _uRAFacade = uRAFacade;
            _log = log;
        }

        /// <summary>
        /// Lista os identificadores associados ao id do cpf/cnpj no D365
        /// </summary>
        /// <param name="entrada">cpf/cnpj</param>
        /// <returns></returns>
        [Route("cliente/identificador/lista")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(SCN3PCLIReceive))]
        public Task<HttpResponseMessage> ListaIdentificador([FromBody] URAIdentificadorListaSend entrada)
        {
            return TrataRetornoFacade(_uRAFacade, "ListaIdentificador", entrada);
        }

        /// <summary>
        /// Histórico de protocolo no Dynamics
        /// </summary>
        /// <param name="entrada">cpf/cnpj</param>
        /// <returns></returns>
        [Route("cliente/protocolo/historico")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(DHistoricoServicoReceiveApp))]
        public Task<HttpResponseMessage> GetHistoricoProtocolo([FromBody] URAHistoricoProtocoloSend entrada)
        {
            return TrataRetornoFacade(_uRAFacade, "GetHistoricoProtocolo", entrada);
        }

        /// <summary>
        /// Atualiza protocolo no Dynamics
        /// </summary>
        /// <param name="entrada">cpf/cnpj</param>
        /// <returns></returns>
        [Route("cliente/protocolo/atualiza")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(Dyn365ProtocoloURAReceive))]
        public Task<HttpResponseMessage> AtualizaProtocolo([FromBody] Dyn365ProtocoloURASend entrada)
        {
            return TrataRetornoFacade(_uRAFacade, "AtualizaProtocolo", entrada);
        }

        /// <summary>
        /// Pesquisa por Protocolo
        /// </summary>
        /// <param name="entrada"></param>
        // <returns></returns>
        [Route("cliente/protocolo/historico/detalhe")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno", Type = typeof(DHistoricoServicoDetalheReceive))]
        public Task<HttpResponseMessage> GetHistoricoServicoDetalhe([FromBody] URAHistoricoProtocoloDetalheSend entrada)
        {
            // TODO criar detalhe do GetHistoricoServicoDetalhe
            return TrataRetornoFacade(_uRAFacade, "GetHistoricoServicoDetalhe", entrada);
        }
    }
}
