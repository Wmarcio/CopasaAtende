using Copasa.Sigos.Model;
using Copasa.Sigos.Model.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Copasa.Sigos.Console
{
    class StartWS
    {
        public void EnviaLoteOsParaMaxProcess()
        {
            try
            {
                ServidorWebService servidores = new ServidorWebService();
                servidores.ServidorMaxProcessPrincipal = ConfigurationManager.AppSettings["ServidorWebMaxProcess"].ToString();
                servidores.ServidorMaxProcessAdicional = ConfigurationManager.AppSettings["ServidorWebMaxProcessAdicional"].ToString();
                if ("S".Equals(ConfigurationManager.AppSettings["ProcessarWSOrdemServico"].ToString()))
                {
                    string retorno = conectarServidorviaViaPost("lote/enviar", servidores);
                    gravaLog(retorno);
                    System.Console.WriteLine(retorno);
                }


                if ("S".Equals(ConfigurationManager.AppSettings["ProcessarWSAlertaPrioridade"].ToString()))
                {
                    string retorno = conectarServidorviaViaPost("alerta/prioridade/enviar", servidores);
                    gravaLog(retorno);
                    System.Console.WriteLine(retorno);
                }
                if ("S".Equals(ConfigurationManager.AppSettings["ProcessarWSAlertaStatus"].ToString()))
                {
                    string retorno = conectarServidorviaViaPost("alerta/status/enviar", servidores);
                    gravaLog(retorno);
                    System.Console.WriteLine(retorno);
                }
            }
            catch (Exception e)
            {
                gravaLog("Ocorreu o seguinte erro: "+e.Message);
            }
        }


        private string conectarServidorviaViaGet(string servico)
        {
            try
            {
                string host = ConfigurationManager.AppSettings["ServidorWebCopasa"].ToString();
                string strUri = host + servico;
                Uri uri = new Uri(strUri);
                WebRequest request = WebRequest.Create(uri);
                WebResponse response = request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                using (var reader = new StreamReader(responseStream))
                {
                    return reader.ReadToEnd().Replace('"', ' ').Trim();
                }
            }catch (Exception e)
            {
                return "Erro na chamada ao web service "+servico +": "+ e.Message;
            }
        }

        private string conectarServidorviaViaPost(string servico, BaseModel objeto)
        {
            string host = ConfigurationManager.AppSettings["ServidorWebCopasa"].ToString();
            JavaScriptSerializer jsonSerializer;

            string strUri = host + servico;
            Uri uri = new Uri(strUri);
            WebRequest request = WebRequest.Create(uri);

            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            jsonSerializer = new JavaScriptSerializer();
            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(jsonSerializer.Serialize(objeto));
            }

            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using (var reader = new StreamReader(responseStream))
            {
                return reader.ReadToEnd().Replace('"', ' ').Trim();
            }
        }

        private void gravaLog(string texto)
        {
            using (StreamWriter w = File.AppendText(ConfigurationManager.AppSettings["ArquivoLogWS"].ToString()))
            {
                w.WriteLine(" ");
                w.WriteLine(DateTime.Now.ToString() + " " + texto);
            }
        }
    }
}
