using Copasa.Atende.Facade.Interfaces;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Model.Digital;
using Copasa.Atende.Util;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Copasa.Atende.WebService.Controllers
{
    /// <summary>
    /// API's para o serviço de atendimento
    /// </summary>
    [RoutePrefix("api")]
    public class TelaController : BaseApiController
    {
        private ITelaFacade _telaFacade;
        private ILog _log;

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="telaFacade">IClienteFacade.</param>
        /// <param name="log">ILog</param>
        public TelaController(ITelaFacade telaFacade, ILog log)
        {
            _telaFacade = telaFacade;
            _log = log;
        }
        
        /// <summary>
        /// Obter Tela.
        /// </summary>
        [Route("Tela")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Retorno em Json")]
        public Task<HttpResponseMessage> BuscaHelpPorIdTela([FromBody] HelpTelaSend entrada)
        {            
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                var baseResponse = _telaFacade.BuscarTelaPorId(entrada.IdTela, entrada.LoginUsuario, entrada.Origem);

                if (baseResponse.IsValid)
                {
                    var telaModel = (TelaModel)baseResponse.Model;

                    var result = new
                    {
                        telaModel.Descricao,
                        telaModel.Detalhe,
                        telaModel.IdTela,
                        TelaTitulos = telaModel.TelaTitulos.Select(s => new
                        {
                            s.IdTela,
                            s.IdTitulo,
                            s.Ordem,
                            Titulo = new
                            {
                                s.Titulo.IdTitulo,
                                s.Titulo.Descricao,
                                s.Titulo.Informacao
                            }
                        }).ToList()
                    };

                    response = Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    if (baseResponse.Messages.Any())
                    {
                        response = Request.CreateResponse(HttpStatusCode.OK, baseResponse.Messages);
                    }
                    else
                    {
                        response = Request.CreateResponse(HttpStatusCode.OK, baseResponse.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }

            tsc.SetResult(response);
            return tsc.Task;
        }

        
        /// <summary>
        /// 
        /// </summary>
        public class HelpTelaSend : BaseModel
        {
            /// <summary>
            /// 
            /// </summary>
            public int IdTela { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string LoginUsuario { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string Origem { get; set; }

        }
    }
}
