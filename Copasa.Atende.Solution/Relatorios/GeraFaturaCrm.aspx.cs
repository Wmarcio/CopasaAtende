using Etc;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;

namespace Relatorios
{
    public partial class GeraFaturaCrm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NameValueCollection coll = Request.QueryString;
            string fatura = coll.Get("fatura");
            try
            {
                if (fatura != null)
                {
                    fatura = fatura.PadLeft(14, '0');
                    Funcoes f = new Funcoes(Server.MapPath("~") + "\\");
                    if (fatura != null && !"".Equals(fatura.Trim()))
                    {
                        Etc.Broker b = new Broker();

                        b.setUrlBroker(ConfigurationSettings.AppSettings["urlBrokerCopasa"]);
                        b.setNomeProgrma(ConfigurationSettings.AppSettings["programaNatural"]);
                        b.addParametro(fatura, 'N', 14);
                        for (int i = 0; i < 4; i++)
                        {
                            b.RequisitaDadosBroker();
                            String dadosFatura = b.retorno.Trim().Replace('"', ' ');
                            if (dadosFatura != null && dadosFatura.Length > 4 && !"ERRO".Equals(dadosFatura.Substring(0, 4).ToUpper()))
                            {
                                ReportFactory rf = new ReportFactory();
                                rf.setFuncoes(f);
                                rf.setMapPath(Server.MapPath("~") + "\\");
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

                                    byte[] bytesComp = f.compress(bytesPaginas);
                                    String tamanhoBytesComp = Convert.ToString(bytesComp.Length);
                                    tamanhoBytesComp = f.repeat("0", 10 - tamanhoBytesComp.Length) + tamanhoBytesComp;
                                    Response.ContentType = "application/pdf";
                                    Response.BinaryWrite(bytesPaginas);
                                }
                                else
                                {
                                    Response.ContentType = "application/text";
                                    Response.Write("Erro na geração da fatura");
                                    return;
                                }
                                break;
                            }
                            else
                            {
                                if (i > 2)
                                {
                                    Response.ContentType = "application/text";
                                    Response.Write("Fatura não encontrada");
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        Response.ContentType = "application/text";
                        Response.Write("Fatura não encontrada");
                        return;
                    }
                }
                else
                {
                    Response.ContentType = "application/text";
                    Response.Write("Fatura não encontrada");
                    return;
                }
            }
            catch (Exception ex)
            {
                Response.ContentType = "application/text";
                Response.Write("Erro na geração da Fatura:"+ex.Message);
                return;
            }
        }
    }
}