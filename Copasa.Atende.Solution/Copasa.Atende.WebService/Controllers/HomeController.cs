using Copasa.Atende.Business.Interfaces;
using Copasa.Atende.Business.Rules;
using Copasa.Atende.Facade.Facades;
using Copasa.Atende.Facade.Interfaces;
using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Copasa.Atende.WebService.Controllers
{
    /// <summary>
    /// Controller Home.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// GET: Index.
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        /// <summary>
        /// View de resposta à confirmação de email
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ConfirmacaoEmail(string Email, string Protocolo)
        {
            //
            //teste
            //string url = $"http://localhost:50756/api/crm/cliente/confirma/statusFaturaPorEmail/{Email}/{Protocolo}";
            //copasa
            string url = $"https://www.copasa.com.br/servicos/WebServiceAPI/Hml/CopasaAtende/api/crm/cliente/confirma/statusFaturaPorEmail/{Protocolo}/{Email}";
            //

            //chamada do ws na camada dcliente
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var table = Newtonsoft.Json.JsonConvert.DeserializeObject<BaseResponse>(data);
                    ViewBag.Mensagem = "Confirmação efetuada!";
                }
                else
                {
                    ViewBag.Mensagem = response.ToString() + url;
                }
                return View();
            }
        }
    }
}
