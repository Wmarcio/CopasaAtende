using Copasa.Atende.Model;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Repository.Repositories;
using Copasa.Atende.Util;
using Copasa.Atende.WebService.Models;
using Copasa.Atende.WebService.Provider;
using Newtonsoft.Json;
using Rotativa;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace Copasa.Atende.WebService.Controllers
{
    /// <summary>
    /// SegundaViaFaturaController
    /// </summary>
    public class SegundaViaFaturaController : Controller
    {
        /// <summary>
        /// Método que gera o PDF da nova certidão negativa de débito
        /// </summary>
        /// <returns></returns>
        public void GerarPdfPost()
        {            
            ILog log = new Log();
            try
            {
                DateTime tempoInicio = DateTime.Now;
                log.SetEntrada();
                log.SetNomeServico("CriarFaturaPDF");

                IBrokerSCN6EFEMRepository _brokerSCN6EFEMRepository = new BrokerSCN6EFEMRepository();
                Stream retorno = null;
                Stream req = Request.InputStream;
                req.Seek(0, System.IO.SeekOrigin.Begin);
                string json = new StreamReader(req).ReadToEnd();
                SCN6EFEMSend sCN6EFEMSend = new SCN6EFEMSend();
                sCN6EFEMSend = JsonConvert.DeserializeObject<SCN6EFEMSend>(json);

                try
                {
                    string origem = sCN6EFEMSend.empresa;
                    if ("RAJADA".Equals(origem))
                    {
                        log.IsRajada();
                    }
                }
                catch (Exception) { }

                SCN6EFEMReceive sCN6EFEMReceive = (SCN6EFEMReceive)_brokerSCN6EFEMRepository.Connect(sCN6EFEMSend).Model;
                string dadosFatura = sCN6EFEMReceive.retornoBroker;
                TimeSpan DuracaoConexao = DateTime.Now.Subtract(tempoInicio);
                double duracaoSegundos = Math.Round(DuracaoConexao.TotalSeconds, 2);
                tempoInicio = DateTime.Now;

                Funcoes f = new Funcoes("");
                //string dadosFatura = sCN6EFEMReceive.descricaoRetorno;
                //string dadosFatura = System.IO.File.ReadAllText(ControllerContext.HttpContext.Server.MapPath("~")+ "\fatura.txt");
                if (dadosFatura != null && dadosFatura.Length > 4 && !"ERRO".Equals(dadosFatura.Substring(0, 4).ToUpper()))
                {
                    ReportFactory rf = new ReportFactory();
                    rf.setFuncoes(f);
                    rf.setMapPath(ControllerContext.HttpContext.Server.MapPath("~"));
                    List<Stream> m_pages = rf.getReport(dadosFatura, "seg2avia");
                    if (m_pages != null && m_pages.Count > 0)
                    {
                        string totPaginas = Convert.ToString(m_pages.Count);
                        if (m_pages.Count < 10)
                        {
                            totPaginas = "0" + totPaginas;
                        }

                        long totalBytesPaginas = 0;
                        foreach (Stream s in m_pages)
                        {
                            totalBytesPaginas = totalBytesPaginas + (s.Length + 6);
                        }

                        byte[] bytesPaginas = new byte[totalBytesPaginas];

                        int pos = 0;
                        byte[] aux;
                        byte[] bytes;
                        String tamanho = "";
                        foreach (Stream s in m_pages)
                        {
                            tamanho = Convert.ToString(s.Length);
                            tamanho = f.repeat("0", 6 - tamanho.Length) + tamanho;
                            aux = f.stringToByteArray(tamanho);
                            System.Buffer.BlockCopy(aux, 0, bytesPaginas, pos, aux.Length);
                            pos = pos + 6;
                            bytes = f.ReadToEnd(s);
                            System.Buffer.BlockCopy(bytes, 0, bytesPaginas, pos, bytes.Length);
                            pos = pos + bytes.Length;
                        }

                        retorno = new MemoryStream();
                        retorno.Write(bytesPaginas, 0, bytesPaginas.Length);
                        Response.AddHeader("Content-Disposition", "inline; filename=contaCopasa.pdf");
                        Response.ContentType = "application/pdf";
                        Response.BinaryWrite(bytesPaginas);
                    } 
                }
                DuracaoConexao = DateTime.Now.Subtract(tempoInicio);
                duracaoSegundos = Math.Round(DuracaoConexao.TotalSeconds, 2);
                log.PringLog();
            }
            catch (Exception e)
            {
                log.GravaLog("Erro na segunda via de fatura:" + e.Message);
            }            
        }

        public ActionResult ExibirMensagem()
        {
            ILog log = new Log();
            try
            {
                Stream req = Request.InputStream;
                req.Seek(0, System.IO.SeekOrigin.Begin);
                string mensagem = new StreamReader(req).ReadToEnd();

                var segundaViaFaturaViewModel = new SegundaViaFaturaViewModel();
                segundaViaFaturaViewModel.mensagemRetorno = mensagem;
                var gerarPdf = new ViewAsPdf()
                {
                    Model = segundaViaFaturaViewModel,
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