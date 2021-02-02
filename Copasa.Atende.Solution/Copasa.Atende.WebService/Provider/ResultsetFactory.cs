using System;

using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using Copasa.Atende.Util;
using System.Web;
using System.Web.Caching;

namespace Copasa.Atende.WebService.Provider
{
    public class ResultsetFactory
    {
        private Funcoes f;

        private string receiveText;
        private string templateName;
        private string mapPath;
        private int ponteiroArq;
        private int tamanhoArq = 0;
        private int ponteiroMetadados;

        private string numeroFatura;
        private string valorFatura;
        List<string> listMetadados=null;
        public StringBuilder sbXmlDados;
        public StringBuilder sbXmlDatasetRelat;

        private List<string> camposL = new List<string>();
        private List<List<string>> resultSetL = new List<List<string>>();
        private List<string> recordL;

        private string[] campos;
        private List<string> camposFuncoes = new List<string>();
        private List<string[]> resultSet = new List<string[]>();
        private string[] record;
        private bool primeiroRegitro = true;

        public void setFuncoes(Funcoes f)
        {
            this.f = f;
        }
        public void setMapPath(string mapPath)
        {
            this.mapPath = mapPath;
        }
        public void setReceiveText(string receiveText)
        {
            this.receiveText = receiveText;
        }

        public void setTemplateName(string templateName)
        {
            this.templateName = templateName;
        }

        public void geraResultSet() 
        {
            try
            {
                listMetadados = null;
                if (HttpContext.Current != null && HttpContext.Current.Cache != null)
                {
                    if (HttpContext.Current.Cache["metadadosRelatSegVia"] == null)
                    {
                        ponteiroArq = 0;
                        listMetadados = new List<string>();
                        string pathFile = mapPath + "\\metadados\\" + templateName + ".properties";
                        System.IO.StreamReader myFile = new System.IO.StreamReader(pathFile);
                        string line = "";
                        do
                        {
                            line = myFile.ReadLine();
                            if (line != null && line != "")
                            {
                                listMetadados.Add(line);
                            }
                        } while (line != null && line != "");

                        DateTime expiration = DateTime.Now.AddHours(5);
                        HttpContext.Current.Cache.Add("metadadosRelatSegVia", listMetadados, null, expiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                    }
                    else
                    {
                        listMetadados = (List<string>)HttpContext.Current.Cache["metadadosRelatSegVia"];
                    }
                }

                if (listMetadados == null)
                {
                    ponteiroArq = 0;
                    listMetadados = new List<string>();
                    string pathFile = mapPath + "\\metadados\\" + templateName + ".properties";
                    System.IO.StreamReader myFile = new System.IO.StreamReader(pathFile);
                    string line = "";
                    do
                    {
                        line = myFile.ReadLine();
                        if (line != null && line != "")
                        {
                            listMetadados.Add(line);
                        }
                    } while (line != null && line != "");
                }

                tamanhoArq = receiveText.Length;
                do
                {
                    recordL = new List<string>();

                    for (ponteiroMetadados = 0; ponteiroMetadados < listMetadados.Count; ponteiroMetadados++)
                    {
                        string s = listMetadados[ponteiroMetadados];
                        if (s.Substring(0, 1) == "@")
                        {
                            if (s.Contains("array_inic"))
                            {
                                int dimens = 1;
                                if (s.Contains("dimens="))
                                {
                                    dimens = Convert.ToInt32(getProperties(s, "dimens"));
                                }
                                ponteiroMetadados = addArray(s, "", sbXmlDados, 1, dimens);
                            }
                        }
                        else
                        {
                            addField(listMetadados[ponteiroMetadados], "");
                        }
                    }
                    resultSet.Add(recordL.ToArray());
                    if (primeiroRegitro)
                    {
                        campos = camposL.ToArray();
                        primeiroRegitro = false;
                    }
                } while (ponteiroArq < receiveText.Length);
            }
            catch (Exception e)
            {
            }
        }


        public string getXml()
        {
            try
            {
                sbXmlDados = new StringBuilder();
                sbXmlDados.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                sbXmlDados.Append("<Dados Relatorio=\"" + templateName + ".xml\">");
                sbXmlDatasetRelat = new StringBuilder();
                sbXmlDatasetRelat.Append("<DataSets>");
                sbXmlDatasetRelat.Append("<DataSet Name=\"Dados_Reg\">");
                sbXmlDatasetRelat.Append("<Fields>");
                List<string> fields = new List<string>();
                List<List<string>> values = new List<List<string>>();
                for (int i = 0; i < campos.Length; i++)
                {
                    sbXmlDatasetRelat.Append("<Field Name=\"" + campos[i] + "\">");
                    sbXmlDatasetRelat.Append("<DataField>" + campos[i] + "</DataField>");
                    sbXmlDatasetRelat.Append("<rd:TypeName>System.String</rd:TypeName>");
                    sbXmlDatasetRelat.Append("</Field>");
                    sbXmlDatasetRelat.Append("");
                }
                foreach (string[] r in resultSet)
                {
                    sbXmlDados.Append("<Reg");
                    for (int i = 0; i < r.Length; i++)
                    {
                        sbXmlDados.Append(" ");
                        sbXmlDados.Append(campos[i]);
                        sbXmlDados.Append("=\"");
                        sbXmlDados.Append(r[i]);
                        sbXmlDados.Append("\"");
                    }
                    foreach (string s in camposFuncoes)
                    {
                        sbXmlDados.Append(" ");
                        sbXmlDados.Append(s);
                        sbXmlDados.Append("=\"");
                        sbXmlDados.Append("");
                        sbXmlDados.Append("\"");

                        sbXmlDatasetRelat.Append("<Field Name=\"" + s + "\">");
                        sbXmlDatasetRelat.Append("<DataField>" + s + "</DataField>");
                        sbXmlDatasetRelat.Append("<rd:TypeName>System.String</rd:TypeName>");
                        sbXmlDatasetRelat.Append("</Field>");
                        sbXmlDatasetRelat.Append("");
                    }
                    sbXmlDados.Append("/>");
                }
                sbXmlDados.Append("</Dados>");
                sbXmlDatasetRelat.Append("</Fields>");
                sbXmlDatasetRelat.Append("<Query>");
                sbXmlDatasetRelat.Append("<DataSourceName>DummyDataSource</DataSourceName>");
                sbXmlDatasetRelat.Append("<CommandText />");
                sbXmlDatasetRelat.Append("<rd:UseGenericDesigner>true</rd:UseGenericDesigner>");
                sbXmlDatasetRelat.Append("</Query>");
                sbXmlDatasetRelat.Append("<rd:DataSetInfo>");
                sbXmlDatasetRelat.Append("<rd:DataSetName>Dados</rd:DataSetName>");
                sbXmlDatasetRelat.Append("<rd:TableName>Reg</rd:TableName>");
                sbXmlDatasetRelat.Append("</rd:DataSetInfo>");
                sbXmlDatasetRelat.Append("</DataSet>");
                sbXmlDatasetRelat.Append("</DataSets>");
                return sbXmlDados.ToString();
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
         

        public void addField(string s, string ocorTxt)
        {
            string[] fiedDef = s.Split(new string[] { "," }, StringSplitOptions.None);
            string nome = fiedDef[0];
            string tipo = fiedDef[1];
            int tamanho = Int32.Parse(fiedDef[2]);
            string valor = "";
            if (tamanhoArq >= (ponteiroArq + tamanho))
            {
                valor = receiveText.Substring(ponteiroArq, tamanho);
            }
            else
            {
                if (tamanhoArq > ponteiroArq)
                {
                    valor = receiveText.Substring(ponteiroArq);
                }
            }
            //if ("n".Equals(tipo))
            //{
            //    valor = Int32.Parse(valor.Replace(',','.'));
            //}
            
            ponteiroArq += tamanho;
            if (primeiroRegitro)
            {
                camposL.Add(nome + ocorTxt);
            }
            if("codBarrasSd".Equals(nome))
            {
                valor = valor.Replace(
                    "HTTP://WWW2.COPASA.COM.BR/SERVICOS/2AVIA2/LAYOUT/GCB.EXE?NROCONTA=", ConfigurationSettings.AppSettings["servicoCodigoBarras"]);
                //"/SERVICOS/2AVIA2/LAYOUT/GCB.EXE?NROCONTA=", ConfigurationSettings.AppSettings["servicoCodigoBarras"]);
                //    "WWW2.COPASA.COM.BR","127.0.0.1");                          
                //    "WWW2.COPASA.COM.BR/SERVICOS/2AVIA2/LAYOUT/GCB.EXE?NROCONTA=",
                //    "192.168.0.41:7010/codigoBarras/Interleaved2of5?codigo=");
                //    "127.0.0.1:7010/codigoBarras/Interleaved2of5?codigo=");                
            }
            if ("urlQRCode".Equals(nome))
            {
                valor = ConfigurationSettings.AppSettings["servicoCodigoBarras"]+numeroFatura+"/"+valorFatura;
            }
            if ("valor".Equals(nome))
                valorFatura = valor.Trim();
            if ("numFatura".Equals(nome))
                numeroFatura = valor.Trim();
            recordL.Add(valor);
        }

        public void addFunctionField(string nome)
        {
            camposFuncoes.Add(nome);
        }

        public void addField2(string s, string ocorTxt, StringBuilder result)
        {
            string[] fiedDef = s.Split(new string[] { "," }, StringSplitOptions.None);
            string nome = fiedDef[0];
            string tipo = fiedDef[1];
            int tamanho = Int32.Parse(fiedDef[2]);
            result.Append(" ");
            result.Append(nome + ocorTxt);
            result.Append("=\"");
            string valor = receiveText.Substring(ponteiroArq, tamanho);
            result.Append(valor);
            result.Append("\"");
            ponteiroArq += tamanho;
            sbXmlDatasetRelat.Append("<Field Name=\"" + nome + ocorTxt + "\">");
            sbXmlDatasetRelat.Append("<DataField>" + nome + ocorTxt + "</DataField>");
            sbXmlDatasetRelat.Append("<rd:TypeName>System.String</rd:TypeName>");
            sbXmlDatasetRelat.Append("</Field>");
            sbXmlDatasetRelat.Append("");
        }

        private string getProperties(string s, string propertieName)
        {
            int pos = s.IndexOf(propertieName + "=") + propertieName.Length + 1;
            int tamanho = s.Substring(pos).IndexOf(',');
            if (tamanho < 0)
            {
                tamanho = s.Substring(pos).IndexOf(')');
            }
            return s.Substring(pos, tamanho);

        }

        private int addArray(string s, string labelArray, StringBuilder result, int dimens, int totalDimens)
        {
            int retorno = -1;
            if (dimens > totalDimens)
            {
                int indArrayFields = ponteiroMetadados + 1;
                while (!listMetadados[indArrayFields].Contains("array_fim"))
                {
                    addField(listMetadados[indArrayFields], labelArray);
                    indArrayFields++;
                }
                retorno = indArrayFields;
            }
            else
            {
                string labelProperiesOcor = "ocor";
                if (totalDimens > 1)
                {
                    labelProperiesOcor = "ocor" + Convert.ToString(dimens);
                }
                int ocor = Convert.ToInt32(getProperties(s,labelProperiesOcor));
                for (int i = 1; i <= ocor; i++)
                {
                    string labelAux = labelArray + "_" + Convert.ToString(i);
                    dimens++;
                    retorno = addArray(s, labelAux, result, dimens, totalDimens);
                }
            }
            return retorno;
        }
        private int addArray2(string s, string labelArray, StringBuilder result, int dimens, int totalDimens)
        {
            int retorno = -1;
            if (dimens > totalDimens)
            {
                int indArrayFields = ponteiroMetadados + 1;
                while (!listMetadados[indArrayFields].Contains("array_fim"))
                {
                    addField(listMetadados[indArrayFields], labelArray);
                    indArrayFields++;
                }
                retorno = indArrayFields;
            }
            else
            {
                string labelProperiesOcor = "ocor";
                if (totalDimens > 1)
                {
                    labelProperiesOcor = "ocor" + Convert.ToString(dimens);
                }
                int ocor = Convert.ToInt32(getProperties(s, labelProperiesOcor));
                for (int i = 1; i <= ocor; i++)
                {
                    string labelAux = labelArray + "_" + Convert.ToString(i);
                    dimens++;
                    retorno = addArray(s, labelAux, result, dimens, totalDimens);
                }
            }
            return retorno;
        }
    }
}
