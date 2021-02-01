using Copasa.Atende.Model;
using Copasa.Atende.Util;
using Copasa.Atende.WebService.Models;
using Newtonsoft.Json;
using Rotativa;
using System;
using System.IO;
using System.Net;
using System.Web.Mvc;

namespace Copasa.Atende.WebService.Controllers
{
    /// <summary>
    /// QuitacaoAnualDebitoController
    /// </summary>
    public class QuitacaoAnualDebitoController : Controller
    {
        /// <summary>
        /// Método que gera o PDF da quitação anual de débito
        /// </summary>
        /// <returns></returns>
        public ActionResult GerarPdfPost()
        {
            ILog log = new Log();
            try
            {
                Stream req = Request.InputStream;
                req.Seek(0, System.IO.SeekOrigin.Begin);
                string json = new StreamReader(req).ReadToEnd();
                SCN6ISQAReceive sCN6ISQAReceive = new SCN6ISQAReceive();
                try
                {
                    sCN6ISQAReceive = JsonConvert.DeserializeObject<SCN6ISQAReceive>(json);
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var quitacaoAnualDebitoViewModel = new QuitacaoAnualDebitoViewModel();
                //AutoMapper.Mapper.Map(sCN6ISQAReceive, quitacaoAnualDebitoViewModel);
                var gerarPdf = new ViewAsPdf()
                {
                    Model = sCN6ISQAReceive,
                    ViewName = "Index",
                    PageSize = Rotativa.Options.Size.A4
                };

                return gerarPdf;
            }
            catch (Exception e)
            {
                log.GravaLog("Erro na certidao negativa:" + e.Message);
                return null;
            }
        }
    }
}