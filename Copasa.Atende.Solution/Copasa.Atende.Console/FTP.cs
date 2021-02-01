using Copasa.Sigos.Model;
using Copasa.Sigos.Model.Core;
using System;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Web.Script.Serialization;

namespace Copasa.Sigos.Console
{
    class FTP
    {
        private void compactaArquivosOS(string origem, string destino)
        {
            try
            {
                string startPath = @origem;
                string zipPath = @destino;
                if (File.Exists(destino))
                    File.Delete(destino);
                ZipFile.CreateFromDirectory(startPath, zipPath);

                if (File.Exists(destino))
                {
                    string[] fileEntries = Directory.GetFiles(@origem);
                    foreach (string fileName in fileEntries)
                    {
                        File.Delete(fileName);
                    }
                }
            }
            catch (Exception e)
            {
                string m = e.Message;
            }
        }

        private byte[] getBytesArquivo(string nomeArquivo)
        {
            byte[] b;
            using (FileStream fileStream = new FileStream(nomeArquivo, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (BinaryReader br = new BinaryReader(fileStream))
                {
                    b = br.ReadBytes((int)fileStream.Length);
                }
            }
            return b;
        }

        private string conectarServidorviaViaGet(string servico)
        {
            string host = ConfigurationManager.AppSettings["ServidorWebCopasa"].ToString();
            string strUri = host+servico;
            Uri uri = new Uri(strUri);
            WebRequest request = WebRequest.Create(uri);
            WebResponse response = request.GetResponse();
            
            Stream responseStream = response.GetResponseStream();
            using (var reader = new StreamReader(responseStream))
            {
                return reader.ReadToEnd().Replace('"', ' ').Trim();
            }
        }

        private string conectarServidorviaViaPost(string servico, BaseModel objeto)
        {
            string host = ConfigurationManager.AppSettings["ServidorWebCopasa"].ToString();
            JavaScriptSerializer jsonSerializer;

            string strUri = host+servico;
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
                return reader.ReadToEnd().Replace('"',' ').Trim();
            }
        }

        private void gravaLog(string texto)
        {
            using (StreamWriter w = File.AppendText(ConfigurationManager.AppSettings["ArquivoLogFTP"].ToString()))
            {                
                w.WriteLine(" ");
                w.WriteLine(DateTime.Now.ToString()+" "+texto);
            }
        }

        private string ProcessaArquivo(string nomeArquivoEnvio, string pastaTrabalho, string tipoArquivo,int qtdeArquivosPasta)
        {
            string retorno = "NT";
            string origem = ConfigurationManager.AppSettings["EnderecoArquivosEntrada"].ToString()
                + pastaTrabalho;
            string emExecucao = ConfigurationManager.AppSettings["EnderecoArquivosEmExecucao"].ToString()
                + pastaTrabalho;
            string nomeArquivoBackup = ConfigurationManager.AppSettings["EnderecoArquivosBackup"].ToString()
                + pastaTrabalho + nomeArquivoEnvio;
            string nomeArquivoCompactado = ConfigurationManager.AppSettings["EnderecoArquivosCompactado"].ToString()
                + pastaTrabalho + nomeArquivoEnvio;
            if (Directory.GetFiles(@origem).Length >= qtdeArquivosPasta) 
            {
                retorno = "OK";
                string[] fileEntries = Directory.GetFiles(origem);
                int minutosEspera = int.Parse(ConfigurationManager.AppSettings["MinutosEsperaTB"].ToString());
                if ("OS".Equals(tipoArquivo))
                {
                    minutosEspera = int.Parse(ConfigurationManager.AppSettings["MinutosEsperaOS"].ToString());
                }
                DateTime criacao = DateTime.Now;
                if (fileEntries.Length > 0)
                {
                    criacao = File.GetCreationTime(@fileEntries[0]).Add(new TimeSpan(0, minutosEspera, 0));
                    DateTime tempoMinimo = DateTime.Now;
                }
                if (criacao < DateTime.Now && fileEntries.Length > 0 && !File.Exists(nomeArquivoBackup))
                {
                    System.Console.WriteLine("Enviando arquivos da pasta " + pastaTrabalho + "...");
                    ArquivoCompactado arquivoCompactado = new ArquivoCompactado();
                    arquivoCompactado.NomeArquivo = nomeArquivoEnvio;
                    arquivoCompactado.ServidorFTP.Endereco = ConfigurationManager.AppSettings["ServidorFTPMaxProcess"].ToString();
                    arquivoCompactado.ServidorFTP.Usuario = ConfigurationManager.AppSettings["UsuarioFTPMaxProcess"].ToString();
                    arquivoCompactado.ServidorFTP.Senha = ConfigurationManager.AppSettings["SenhaFTPMaxProcess"].ToString();
                    string enderecoFtpAticional = ConfigurationManager.AppSettings["ServidorFTPMaxProcessAdicional"].ToString();
                    if (enderecoFtpAticional != null && !"".Equals(enderecoFtpAticional))
                    {
                        try
                        {
                            arquivoCompactado.ServidorFTPAdicional.Endereco = ConfigurationManager.AppSettings["ServidorFTPMaxProcessAdicional"].ToString();
                            arquivoCompactado.ServidorFTPAdicional.Usuario = ConfigurationManager.AppSettings["UsuarioFTPMaxProcessAdicional"].ToString();
                            arquivoCompactado.ServidorFTPAdicional.Senha = ConfigurationManager.AppSettings["SenhaFTPMaxProcessAdicional"].ToString();
                        }
                        catch (Exception)
                        {
                            arquivoCompactado.ServidorFTPAdicional.Endereco = "";
                        }
                    }

                    foreach (string file in fileEntries)
                    {
                        string fileName = Path.GetFileName(file);
                        File.Move(origem + fileName, emExecucao + fileName);
                        ArquivoFTP arquivoFTP = new ArquivoFTP();
                        arquivoFTP.NomeArquivo = fileName;
                        arquivoFTP.TipoArquivo = tipoArquivo;
                        arquivoFTP.UltimoNomeArquivoZip = nomeArquivoEnvio;
                        arquivoCompactado.ArquivoFTP.Add(arquivoFTP);
                    }
                    compactaArquivosOS(emExecucao, nomeArquivoCompactado);
                    if ("S".Equals(ConfigurationManager.AppSettings["EnviarFTP"].ToString())
                        && File.Exists(nomeArquivoCompactado))
                    {
                        retorno = EnviarArquivo(nomeArquivoEnvio, nomeArquivoCompactado, pastaTrabalho, true, arquivoCompactado);
                    }
                    else
                    {
                        System.Console.WriteLine("   Aguardar envio");
                        gravaLog("Aguardar envio:" + nomeArquivoEnvio);
                        string nomeArquivoReEnvio = ConfigurationManager.AppSettings["EnderecoArquivosAReenviar"].ToString()
                                + pastaTrabalho
                                + nomeArquivoEnvio;
                        if (File.Exists(nomeArquivoReEnvio))
                            EnviarParaErro(nomeArquivoReEnvio, pastaTrabalho, nomeArquivoEnvio);
                        File.Move(nomeArquivoCompactado, nomeArquivoReEnvio);
                    }
                }
                gravaLog(nomeArquivoEnvio + " " + retorno);
            }
            return retorno;
        }

        private string ReenviaArquivo(string pastaTrabalho,string tipoArquivo)
        {
            string retorno = "OK";
            if ("S".Equals(ConfigurationManager.AppSettings["ReenviarFTP"].ToString()))
            {
                string[] fileEntries = Directory.GetFiles(ConfigurationManager.AppSettings["EnderecoArquivosAReenviar"].ToString() + pastaTrabalho);
                foreach (string nomeArquivoCompactado in fileEntries)                {
                    string nomeArquivoEnvio = Path.GetFileName(nomeArquivoCompactado);
                    ArquivoCompactado arquivoCompactado = new ArquivoCompactado();
                    arquivoCompactado.NomeArquivo = nomeArquivoEnvio;
                    arquivoCompactado.ServidorFTP.Endereco = ConfigurationManager.AppSettings["ServidorFTPMaxProcess"].ToString();
                    arquivoCompactado.ServidorFTP.Usuario = ConfigurationManager.AppSettings["UsuarioFTPMaxProcess"].ToString();
                    arquivoCompactado.ServidorFTP.Senha = ConfigurationManager.AppSettings["SenhaFTPMaxProcess"].ToString();
                    string enderecoFtpAticional = ConfigurationManager.AppSettings["ServidorFTPMaxProcessAdicional"].ToString();
                    if (enderecoFtpAticional != null && !"".Equals(enderecoFtpAticional))
                    {
                        arquivoCompactado.ServidorFTPAdicional.Endereco = ConfigurationManager.AppSettings["ServidorFTPMaxProcessAdicional"].ToString();
                        arquivoCompactado.ServidorFTPAdicional.Usuario = ConfigurationManager.AppSettings["UsuarioFTPMaxProcessAdicional"].ToString();
                        arquivoCompactado.ServidorFTPAdicional.Senha = ConfigurationManager.AppSettings["SenhaFTPMaxProcessAdicional"].ToString();
                    }

                    string zipPath = @nomeArquivoCompactado;
                    using (ZipArchive archive = ZipFile.OpenRead(zipPath))
                    {
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            ArquivoFTP arquivoFTP = new ArquivoFTP();
                            arquivoFTP.NomeArquivo = entry.Name;
                            arquivoFTP.UltimoNomeArquivoZip = nomeArquivoEnvio;
                            arquivoFTP.TipoArquivo = tipoArquivo;
                            arquivoCompactado.ArquivoFTP.Add(arquivoFTP);
                        }
                    }
                    retorno = EnviarArquivo(nomeArquivoEnvio,nomeArquivoCompactado,pastaTrabalho,false, arquivoCompactado);
                }
            }
            return retorno;
        }

        private string EnviarArquivo(string nomeArquivoEnvio,string nomeArquivoCompactado,string pastaTrabalho,bool moverArquivo, ArquivoCompactado arquivoCompactado)
        {
            string retorno = conectarServidorviaViaGet("arquivoFtp/deletaArquivoFisico/" + nomeArquivoEnvio);
            //FilesSoapClient sc = new FilesSoapClient();
            //sc.DeletaArquivo(nomeArquivoEnvio);
            string etapa = "Deletar arquivo no servidor";
            if ("OK".Equals(retorno.ToUpper()))
            {
                etapa = "Enviar bytes para o servidor";
                retorno = "";
                using (FileStream fileStream = new FileStream(nomeArquivoCompactado, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (BinaryReader br = new BinaryReader(fileStream))
                    {
                        int totalBytes = 0;
                        byte[] data = new byte[10240];
                        int read = 0;
                        do
                        {
                            read = br.Read(data, 0, 10240);
                            totalBytes += read;
                            if (read > 0)
                            {
                                ArquivoUpload au = new ArquivoUpload();
                                au.NomeArquivo = nomeArquivoEnvio;
                                au.Dados = data;
                                retorno = conectarServidorviaViaPost("arquivoFtp/recebeBytesArquivo/", au);
                            }
                            if (!"OK".Equals(retorno.ToUpper()))
                            {
                                break;
                            }
                        }
                        while (read > 0);
                        etapa = "Enviar arquivo via FTP";
                        retorno = conectarServidorviaViaPost("arquivoFtp/incluir/", arquivoCompactado);                        
                    }
                }
            }

            if ("OK".Equals(retorno.ToUpper()))
            {
                File.Move(nomeArquivoCompactado,
                    ConfigurationManager.AppSettings["EnderecoArquivosBackup"].ToString()
                    + pastaTrabalho
                    + nomeArquivoEnvio);
                System.Console.WriteLine("   Arquivo enviado com sucesso");
            }
            else
            {
                gravaLog("Arquivo: " + nomeArquivoEnvio + ". Ocorreu o seguinte erro:" + retorno);
                gravaLog("    " + etapa);
                System.Console.WriteLine("   Ocorreu o seguinte erro:" + retorno);
                EnviarParaErro(nomeArquivoCompactado, pastaTrabalho, nomeArquivoEnvio);
            }
            return retorno;
        }

        private void ReenviarLote()
        {
            string pastaReenviarIDLote = ConfigurationManager.AppSettings["EnderecoArquivosAReenviar"].ToString() +
                ConfigurationManager.AppSettings["PastaIDOrdemServico"].ToString();
            string pastaReenviarLote = ConfigurationManager.AppSettings["EnderecoArquivosAReenviar"].ToString() +
                ConfigurationManager.AppSettings["PastaOrdemServico"].ToString();

            if (Directory.GetFiles(pastaReenviarIDLote).Length > 0 && Directory.GetFiles(pastaReenviarLote).Length == 0)
            {
                EnviarLote(null, ConfigurationManager.AppSettings["EnderecoArquivosAReenviar"].ToString(),"OK");
            }
        }

        private string EnviarLote(string nomeArquivoZip, string pastaOrigem,string retorno)
        {
            if ("OK".Equals(retorno))
            {
                string nomeArquivoDadosLote = pastaOrigem + ConfigurationManager.AppSettings["PastaIDOrdemServico"].ToString();
                bool reenvio = false;
                if (pastaOrigem.Equals(ConfigurationManager.AppSettings["EnderecoArquivosAReenviar"].ToString()))
                {
                    reenvio = true;
                }

                if (Directory.GetFiles(nomeArquivoDadosLote).Length > 0)
                {
                    string[] fileEntries = Directory.GetFiles(nomeArquivoDadosLote);
                    System.Console.WriteLine("Enviando dados do lote de ordem serviço...");
                    string conteudo = System.IO.File.ReadAllText(@fileEntries[0]);
                    string[] arrayConteudo = conteudo.Split(';');
                    Lote loteOrdemServico = new Lote();
                    loteOrdemServico.Identificador = arrayConteudo[3];
                    loteOrdemServico.QtdeOrdensServico = Int32.Parse(arrayConteudo[4]);
                    loteOrdemServico.DataInclusao = DateTime.Today;
                    loteOrdemServico.ModoEnvio = "F";
                    loteOrdemServico.Tipo = "OS";
                    loteOrdemServico.Sequencial = 1;
                    if (reenvio)
                    {
                        loteOrdemServico.NomeArquivoZip = arrayConteudo[5];
                    }
                    else
                    {
                        loteOrdemServico.NomeArquivoZip = nomeArquivoZip;
                    }
                    retorno = conectarServidorviaViaPost("lote/incluir/", loteOrdemServico);
                    if (!reenvio)
                    {
                        using (StreamWriter w = File.AppendText(fileEntries[0]))
                        {
                            w.Write(";" + nomeArquivoZip);
                        }
                    }
                    if ("OK".Equals(retorno))
                    {
                        string dateTime = DateTime.Now.ToString();
                        string dataString = Convert.ToDateTime(dateTime).ToString("yyyyMMdd");

                        string destino = ConfigurationManager.AppSettings["EnderecoArquivosBackup"].ToString()
                            + ConfigurationManager.AppSettings["PastaIDOrdemServico"].ToString()
                            + dataString+Path.GetFileName(fileEntries[0]);
                        if (File.Exists(destino))
                        {
                            File.Delete(destino);
                        }
                        File.Move(fileEntries[0], destino);
                    }
                    else
                    {
                        string destino = ConfigurationManager.AppSettings["EnderecoArquivosAReenviar"].ToString()
                            + ConfigurationManager.AppSettings["PastaIDOrdemServico"].ToString()
                            + Path.GetFileName(fileEntries[0]);
                        File.Move(fileEntries[0], destino);
                    }
                    if ("OK".Equals(retorno))
                    {
                        System.Console.WriteLine("   Dados lote enviado com sucesso");
                    }
                    else
                    {
                        System.Console.WriteLine("   Ocorreu o seguinte erro:" + retorno);
                    }
                }
            }
            return retorno;
        }
        public void EnviaArquivoOS()
        {
            try
            {
                int horaProcessamentoTabelas = int.Parse(ConfigurationManager.AppSettings["HoraProcessamentoOS"].ToString());
                if (DateTime.Now.Hour == horaProcessamentoTabelas)
                {
                    string dateTime = DateTime.Now.ToString();
                    string dataString = Convert.ToDateTime(dateTime).ToString("yyyyMMdd");
                    string nomeArquivoEnvio = "COPASA-SICOM-OS-" + dataString + ".ZIP";

                    ReenviarLote();
                    string retorno = ReenviaArquivo(ConfigurationManager.AppSettings["PastaOrdemServico"].ToString(), "OS");
                    /*
                    if ("OK".Equals(retorno))
                    {
                        EnviarLote(nomeArquivoEnvio, ConfigurationManager.AppSettings["EnderecoArquivosEntrada"].ToString(),retorno);
                    }
                    string nomePastaArquivoIDLote = ConfigurationManager.AppSettings["EnderecoArquivosEmExecucao"].ToString()
                        + ConfigurationManager.AppSettings["PastaIDOrdemServico"].ToString();
                    */

                    retorno = ProcessaArquivo(nomeArquivoEnvio, ConfigurationManager.AppSettings["PastaOrdemServico"].ToString(), "OS", 1);
                    if ("OK".Equals(retorno))
                    {
                        EnviarLote(nomeArquivoEnvio, ConfigurationManager.AppSettings["EnderecoArquivosEntrada"].ToString(), retorno);
                    }
                    gravaLog(nomeArquivoEnvio + " " + retorno);
                }
            }
            catch (Exception e)
            {
                gravaLog("Ocorreu o seguinte erro:" + e.Message);
            }
        }

        public void EnviaArquivoTabelas()
        {
            try
            {
                string dateTime = DateTime.Now.ToString();
                string dataString = Convert.ToDateTime(dateTime).ToString("yyyyMMdd");
                var keys = ConfigurationManager.AppSettings.AllKeys;
                int horaProcessamentoTabelas = int.Parse(ConfigurationManager.AppSettings["HoraProcessamentoTabelas"].ToString());
                if (DateTime.Now.Hour >= horaProcessamentoTabelas)
                {
                    foreach (string nomeKey in keys)
                    {
                        if (nomeKey != null && nomeKey.Length > 12 && "PastaTabelas".Equals(nomeKey.Substring(0, 12)))
                        {
                            string nomeTabela = nomeKey.Split('_')[1];
                            string nomeArquivoEnvio = "COPASA-" + nomeTabela + "-TB-" + dataString + ".ZIP";
                            ReenviaArquivo(ConfigurationManager.AppSettings[nomeKey].ToString(), "TB");
                            int qtdeArquivosPasta = 1;
                            try
                            {
                                qtdeArquivosPasta = int.Parse(ConfigurationManager.AppSettings["QtdeArquivos" + nomeKey.Substring(12)].ToString());
                            }
                            catch (Exception) { }
                            ProcessaArquivo(nomeArquivoEnvio, ConfigurationManager.AppSettings[nomeKey].ToString(), "TB", qtdeArquivosPasta);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                gravaLog("Ocorreu o seguinte erro:" + e.Message);
            }
        }

        private void EnviarParaErro(string origem, string pastaTrabalho, string nomeArquivoEnvio)
        {
            string dateTime = DateTime.Now.ToString();
            string dataString = Convert.ToDateTime(dateTime).ToString("yyyyMMdd");
            int sequencial = 0;
            string destino = "";
            for (sequencial = 0; sequencial < 10; sequencial++)
            {
                destino = ConfigurationManager.AppSettings["EnderecoArquivosErro"].ToString()
                    + pastaTrabalho
                    + sequencial + "_" + dataString + "_" + nomeArquivoEnvio;
                if (!File.Exists(destino))
                    break;
            }
            if (File.Exists(destino))
                File.Delete(destino);
            File.Move(origem, destino);
        }
    }
}
