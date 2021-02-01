using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using System;
using System.IO;
using System.Net;
using System.Web.Helpers;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace Copasa.Atende.Util
{
    public class TesteStressThread
    {
        private string _host;
        private WebRequest _request;
        private string _cpf;
        private string _identificador;
        private string _matricula;
        private int _tipoTeste;

        public TesteStressThread(string host,string cpf, int tipoTeste)
        {
            try
            {
                _host = host;
                _cpf = cpf;
                _tipoTeste = tipoTeste;
            }
            catch (Exception)
            {
            }

        }

        public TesteStressThread(string host, string identificador, string matricula, int tipoTeste)
        {
            try
            {
                _host = host;
                _identificador = identificador;
                _matricula = matricula;
                _tipoTeste = tipoTeste;
            }
            catch (Exception)
            {
            }

        }

        public void Run()
        {
            if (_tipoTeste == 1)
                TesteGeral();
            else if (_tipoTeste == 2)
                TesteFaturasPagas();
            else if (_tipoTeste == 3)
                TesteFaturasEmDebido();
            else if (_tipoTeste == 4)
            {
                TesteFaturasPagas();
                TesteFaturasEmDebido();
            }
        }

        private void TesteFaturasPagas()
        {
            SCN6ISFPSend sCN6ISFPSend = new SCN6ISFPSend();
            sCN6ISFPSend.matricula = _matricula;
            sCN6ISFPSend.identificador = _identificador;
            SCN6ISFPReceive sCN6ISFPReceive = new SCN6ISFPReceive();
            sCN6ISFPReceive = (SCN6ISFPReceive)ConnectPost("api/fatura/lista/pagas", sCN6ISFPSend, sCN6ISFPReceive.GetType());
        }

        private void TesteFaturasEmDebido()
        {
            SCN6ISFDSend sCN6ISFDSend = new SCN6ISFDSend();
            sCN6ISFDSend.matricula = _matricula;
            sCN6ISFDSend.identificador = _identificador;
            SCN6ISFDReceive sCN6ISFDReceive = new SCN6ISFDReceive();
            sCN6ISFDReceive = (SCN6ISFDReceive)ConnectPost("api/fatura/lista/emDebito", sCN6ISFDSend, sCN6ISFDReceive.GetType());
        }

        private void TesteGeral()
        { 
            
            SCN4ISU1Send sCN4ISU1Send = new SCN4ISU1Send();
            sCN4ISU1Send.cpfCnpj = _cpf;
            sCN4ISU1Send.Origem = "RAJADA";
            SCN4ISU1Receive sCN4ISU1Receive = new SCN4ISU1Receive();
            sCN4ISU1Receive = (SCN4ISU1Receive)ConnectPost("api/cliente/lista/matriculas", sCN4ISU1Send, sCN4ISU1Receive.GetType());

            foreach(SCN4ISU1ReceiveMatriculas matricula in sCN4ISU1Receive.matriculas)
            {
                SCN6ISFDSend sCN6ISFDSend = new SCN6ISFDSend();
                sCN6ISFDSend.matricula = matricula.matricula;
                sCN6ISFDSend.identificador = matricula.identificador;
                sCN6ISFDSend.Origem = "RAJADA";
                SCN6ISFDReceive sCN6ISFDReceive = new SCN6ISFDReceive();
                sCN6ISFDReceive = (SCN6ISFDReceive)ConnectPost("api/fatura/lista/emDebito", sCN6ISFDSend, sCN6ISFDReceive.GetType());
                foreach(SCN6ISFDReceiveFaturas fatura in sCN6ISFDReceive.faturas)
                {
                    SCN6ISDFSend sCN6ISDFSend = new SCN6ISDFSend();
                    sCN6ISDFSend.matricula = matricula.matricula;
                    sCN6ISDFSend.anoMesReferencia = fatura.referencia;
                    sCN6ISDFSend.Origem = "RAJADA";
                    SCN6ISDFReceive sCN6ISDFReceive = new SCN6ISDFReceive();
                    sCN6ISDFReceive = (SCN6ISDFReceive)ConnectPost("api/fatura/detalhar", sCN6ISDFSend, sCN6ISDFReceive.GetType());

                    ConnectGet("api/fatura/exibe/codigoBarras/"+ fatura.numeroCodigoBarras);
                    ConnectGet("api/fatura/download/PDF/RAJADA/" + fatura.numeroFatura);

                }

                SCN6ISFPSend sCN6ISFPSend = new SCN6ISFPSend();
                sCN6ISFPSend.matricula = matricula.matricula;
                sCN6ISFPSend.identificador = matricula.identificador;
                sCN6ISFPSend.Origem = "RAJADA";
                SCN6ISFPReceive sCN6ISFPReceive = new SCN6ISFPReceive();
                sCN6ISFPReceive = (SCN6ISFPReceive)ConnectPost("api/fatura/lista/pagas", sCN6ISFPSend, sCN6ISFPReceive.GetType());
            }
            
            ConnectGet("api/cliente/exibe/CertidaoNegativaDebito/PDF/RAJADA/" + _cpf);
        }

        private BaseModelReceive ConnectPost(string servico, BaseModel baseModel,Type type)
        {
            try
            {
                string _strUri = _host + servico;
                Uri uri = new Uri(_strUri);

                string metodo = "POST";
                _request = WebRequest.Create(uri);
                if (baseModel != null)
                {
                    string dados = baseModel.getJson();
                    if (dados != null && !"".Equals(dados))
                    {
                        _request.Method = metodo;
                        _request.ContentType = "application/json";
                        using (StreamWriter writer = new StreamWriter(_request.GetRequestStream()))
                        {
                            writer.Write(dados);
                        }
                    }
                }
                HttpWebResponse response = (HttpWebResponse)_request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                XmlSerializer serializer = new XmlSerializer(type);
                var reader = new StreamReader(responseStream);
                JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                string retornoString = reader.ReadToEnd();                
                var baseModelOut = Activator.CreateInstance(type);
                baseModelOut = jsonSerializer.Deserialize(retornoString, type);
                return (BaseModelReceive)baseModelOut;
            }
            catch (Exception e)
            {
                string erro = e.Message;
                return null;
            }
        }

        private Stream ConnectGet(string servico)
        {
            try
            {
                string _strUri = _host + servico;
                Uri uri = new Uri(_strUri);

                string metodo = "GET";
                _request = WebRequest.Create(uri);
                _request.Method = metodo;
                HttpWebResponse response = (HttpWebResponse)_request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                return responseStream;
            }
            catch (Exception e)
            {
                string erro = e.Message;
                return null;
            }

        }
    }
}
