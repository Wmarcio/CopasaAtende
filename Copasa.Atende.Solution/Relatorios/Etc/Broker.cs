using System;
using System.Configuration;
using System.Net;
using System.Text;
using System.Collections.Generic;

namespace Etc
{
    public class Broker
    {
        private StringBuilder requestString  = new StringBuilder();
        private string nomePrograma;
        private string urlBroker;
        public string retorno = null;
        private List<string> nomeCampo = new List<string>();
        private List<int> tamanhoCampo = new List<int>();
        private List<char> tipoCampo = new List<char>();

        private string[] conteudoCampo;

        public void setUrlBroker(string urlBroker)
        {
            this.urlBroker = urlBroker;
        }

        public void setNomeProgrma(string nome)
        {
            nomePrograma = nome;
        }

        public void addCampo(string nome,int tamanho,char tipo)
        {
            nomeCampo.Add(nome);
            tamanhoCampo.Add(tamanho);
            tipoCampo.Add(tipo);
        }

        public void addArrayCampo(string nome, int tamanho,char tipo, int ocor)
        {
            for (int i = 1; i <= ocor; i++)
            {
                addCampo(nome + "_" + i, tamanho,tipo);
            }
        }

        public string getValor(string nome)
        {
            return conteudoCampo[nomeCampo.IndexOf(nome)];
        }

        public int getValorAsInt(string nome)
        {
            return Convert.ToInt32(conteudoCampo[nomeCampo.IndexOf(nome)]);
        }

        public void RequisitaDadosBroker()
        {
            try
            {
                int tam = requestString.Length;
                //string host = "www2.copasa.com.br";
                //string host = "10.1.13.35:8090";
                //string host = "127.0.0.1";
                //string url = "http://" + host + "/padroes/buscaDadosBroker.asp?Parametro=" + nomePrograma + requestString;                
                string url = "http://" + urlBroker + "?Parametro=" + nomePrograma + requestString; 
                try
                {
                    WebClient client = new WebClient();
                    retorno = client.DownloadString(url);
                    string[] arrayNomeCampo = nomeCampo.ToArray();
                    conteudoCampo = new string[arrayNomeCampo.Length];
                    int[] arrayTamanhoCampo = tamanhoCampo.ToArray();
                    int pos = 0;
                    for (int i = 0; i < arrayNomeCampo.Length; i++)
                    {
                        if (tipoCampo[i] == 'N')
                        {
                            conteudoCampo[i] = formatint(retorno.Substring(pos, arrayTamanhoCampo[i]));
                        }
                        else
                        {
                            conteudoCampo[i] = retorno.Substring(pos, arrayTamanhoCampo[i]);
                        }
                        
                        pos = pos + arrayTamanhoCampo[i];
                    }
                }
                catch (Exception e)
                {
                    string erro = e.Message;                    
                }
            }
            catch (Exception e1)
            {
                string erro = e1.Message;
            }
        }

        private byte[] stringToByteArray(string StringToConvert)
        {
            char[] CharArray = StringToConvert.ToCharArray();
            byte[] ByteArray = new byte[CharArray.Length];
            for (int i = 0; i < CharArray.Length; i++)
            {
                ByteArray[i] = Convert.ToByte(CharArray[i]);
            }
            return ByteArray;
        }

        public void addParametro(string conteudo, char tipo, int tamanho)
        {
            requestString.Append(formata(conteudo,tipo,tamanho));
        }

        public string formata(string conteudo, char tipo, int tamanho)
        {
            string resultado = "";
            conteudo = conteudo.Trim();
            if (tipo == 'N')
            {
                if (tamanho > conteudo.Length)
                {
                    resultado = new string('0', tamanho - conteudo.Length) + conteudo;
                }
                else
                {
                    resultado = conteudo.Substring(0, tamanho);
                }
            }
            else
            {
                if (tamanho > conteudo.Length)
                {
                    resultado = conteudo + new string(' ', tamanho - conteudo.Length);
                }
                else
                {
                    resultado = conteudo.Substring(conteudo.Length-tamanho, tamanho);
                }
            }
            return resultado;
        }

        private string formatint(string parm)
        {
            int aux;
            try
            {
                if (parm != null)
                {
                    return Convert.ToString(Int32.Parse(parm));
                }
                else
                {
                    return parm;
                }
            }
            catch (Exception)
            {
                return parm;
            }
        }

    }
}
