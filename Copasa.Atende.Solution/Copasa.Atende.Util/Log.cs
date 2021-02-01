using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace Copasa.Atende.Util
{
    /// <summary>
    /// Classe que contem as ferramentas para registrar logs.
    /// </summary>
    public class Log : ILog
    {
        private List<string> texto = new List<string>();
        //private List<string> textoAsync = new List<string>();
        private bool _isErro = false;
        private bool _isErroIS = false;
        private bool _isErroBroker = false;
        private bool _isErroOracle = false;
        private bool _isAsync = false;
        private bool _isBatch = false;
        private bool _isRajada = false;
        private DateTime entrada;
        private DateTime entradaAdc;
        private DateTime saida;
        private DateTime saidaAdc;
        private string nomeServico;

        private string applicationPath;

        /// <summary>
        /// Construtor
        /// </summary>
        public Log()
        {
            applicationPath = HttpContext.Current.Request.PhysicalApplicationPath;
            SetEntrada();
        }

        /// <summary>
        /// Grava entrada em arquivo de log
        /// </summary>
        public string GravaLog(string s)
        {
            return GravaLog(new string[1] { s });
        }

        /// <summary>
        /// Grava entrada em arquivo de log
        /// </summary>
        public void PrintTexto(string texto)
        {
            try
            {
                string LogFilePath = GetPastaTrabalho() + "log.txt";
                using (FileStream fs = File.Open(LogFilePath, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    StringBuilder buffer = new StringBuilder();

                    buffer.Append("\r\n****************************************\r\n");

                    buffer.Append(texto + "\r\n");
                    Byte[] info = new UTF8Encoding(true).GetBytes(buffer.ToString());
                    fs.Write(info, 0, info.Length);
                    fs.Close();
                }

            }
            catch (Exception) { }
        }

        /// <summary>
        /// Grava entrada em arquivo de log adicional
        /// </summary>
        public void GravaLogAdc(DateTime entradaParm)
        {
            GravaLogAdc(null, entradaParm,null);
        }

        /// <summary>
        /// Grava entrada em arquivo de log adicional
        /// </summary>
        public void GravaLogAdc(DateTime entradaParm,string erro)
        {
            GravaLogAdc(null, entradaParm, erro);
        }

        /// <summary>
        /// Grava entrada em arquivo de log adicional
        /// </summary>
        public void GravaLogAdc(string nomeServicoParm, DateTime entradaParm)
        {
            GravaLogAdc(nomeServicoParm, entradaParm,null);
        }

        /// <summary>
        /// Grava entrada em arquivo de log adicional
        /// </summary>
        public void GravaLogAdc(string nomeServicoParm, DateTime entradaParm, string erro)
        {
            entradaAdc = entradaParm;
            saidaAdc = DateTime.Now;
            TimeSpan duracao = saidaAdc.Subtract(entradaAdc);
            if (duracao.TotalSeconds > 0.009)
            {
                string duracaoSegundos = Math.Round(duracao.TotalSeconds, 2).ToString().Replace('.', ',');
                if (nomeServicoParm != null)
                    nomeServico = nomeServicoParm;
                if (nomeServico != null && !"".Equals(nomeServico))
                {
                    //if (!_isRajada)
                    //{
                    try
                    {
                        string LogFilePathAdc = GetPastaTrabalho() + "logAdicional.txt";
                        if (File.Exists(LogFilePathAdc))
                        {
                            DateTime lastWrite = File.GetLastWriteTime(LogFilePathAdc);
                            if (lastWrite.Date < DateTime.Now.Date)
                            {
                                string OldLogFileAdcPath = GetPastaTrabalho() + "log\\Adc" + lastWrite.ToString("yyyyMMdd") + ".txt";
                                if (File.Exists(LogFilePathAdc) && !File.Exists(OldLogFileAdcPath))
                                {
                                    File.Move(LogFilePathAdc, OldLogFileAdcPath);
                                }
                            }
                        }
                        using (FileStream fs = File.Open(LogFilePathAdc, FileMode.Append, FileAccess.Write, FileShare.None))
                        {
                            StringBuilder buffer = new StringBuilder();
                            if (erro != null)
                                buffer.Append(nomeServico + ";" + entradaAdc.ToString("dd/MM/yyyy") + ";" + entradaAdc.ToString("H:mm:ss.ff") + ";" + saidaAdc.ToString("H:mm:ss.ff") + ";" + duracaoSegundos + ";" + erro + "\r\n");
                            else
                                buffer.Append(nomeServico + ";" + entradaAdc.ToString("dd/MM/yyyy") + ";" + entradaAdc.ToString("H:mm:ss.ff") + ";" + saidaAdc.ToString("H:mm:ss.ff") + ";" + duracaoSegundos + "\r\n");

                            Byte[] info = new UTF8Encoding(true).GetBytes(buffer.ToString());
                            fs.Write(info, 0, info.Length);
                            fs.Close();
                        }
                        if (duracao.TotalSeconds > 10)
                        {
                            string LogFilePath10 = GetPastaTrabalho() + "log10.txt";
                            if (File.Exists(LogFilePath10))
                            {
                                DateTime lastWrite = File.GetLastWriteTime(LogFilePathAdc);
                                if (lastWrite.Date < DateTime.Now.Date)
                                {
                                    string OldLogFileAdc10 = GetPastaTrabalho() + "log\\10_" + lastWrite.ToString("yyyyMMdd") + ".txt";
                                    if (File.Exists(LogFilePath10) && !File.Exists(OldLogFileAdc10))
                                    {
                                        File.Move(LogFilePath10, OldLogFileAdc10);
                                    }
                                }
                            }
                            using (FileStream fs = File.Open(LogFilePath10, FileMode.Append, FileAccess.Write, FileShare.None))
                            {
                                StringBuilder buffer = new StringBuilder();
                                if (erro != null)
                                    buffer.Append(nomeServico + ";" + entradaAdc.ToString("dd/MM/yyyy") + ";" + entradaAdc.ToString("H:mm:ss.ff") + ";" + saidaAdc.ToString("H:mm:ss.ff") + ";" + duracaoSegundos + ";" + erro + "\r\n");
                                else
                                    buffer.Append(nomeServico + ";" + entradaAdc.ToString("dd/MM/yyyy") + ";" + entradaAdc.ToString("H:mm:ss.ff") + ";" + saidaAdc.ToString("H:mm:ss.ff") + ";" + duracaoSegundos + "\r\n");

                                Byte[] info = new UTF8Encoding(true).GetBytes(buffer.ToString());
                                fs.Write(info, 0, info.Length);
                                fs.Close();
                            }
                        }
                    }
                    catch (Exception) { }
                }
            }
        }
        /// <summary>
        /// Grava entrada em arquivo de log
        /// </summary>
        public string GravaLog(string[] s)
        {
            string id = DateTime.Now.ToString("yyyyMMddHmmssfffff");
            if (s.Length > 0)
            {
                string data = id;
                saida = DateTime.Now;
                TimeSpan duracao = saida.Subtract(entrada);
                double duracaoSegundos = Math.Round(duracao.TotalSeconds, 2);
                try
                {
                    if (HttpContext.Current != null)
                    {
                        if (HttpContext.Current.Application["LogContadorTempoConexao"] != null
                            && HttpContext.Current.Application["LogTotalTempoConexao"] != null
                            && HttpContext.Current.Application["LogUltimaAtualizacao"] != null)
                        {
                            DateTime ultimaAtualizacao = (DateTime)HttpContext.Current.Application["LogUltimaAtualizacao"];
                            ultimaAtualizacao = ultimaAtualizacao.Add(new TimeSpan(0, 1, 0));
                            
                            if (ultimaAtualizacao < DateTime.Now)
                            {
                                PrintLogTempoMedio();
                            }
                            else
                            {                            
                                double logTotalTempo = (double)HttpContext.Current.Application["LogTotalTempoConexao"];
                                logTotalTempo = logTotalTempo + duracaoSegundos;
                                HttpContext.Current.Application["LogTotalTempoConexao"] = logTotalTempo;

                                long logContadorTempo = (long)HttpContext.Current.Application["LogContadorTempoConexao"];
                                logContadorTempo++;
                                HttpContext.Current.Application["LogContadorTempoConexao"] = logContadorTempo;
                            }
                        }
                        else
                        {
                            HttpContext.Current.Application["LogUltimaAtualizacao"] = DateTime.Now;
                            HttpContext.Current.Application["LogTotalTempoConexao"] = duracaoSegundos;
                            long logContadorTempo = 1;
                            HttpContext.Current.Application["LogContadorTempoConexao"] = logContadorTempo;
                        }
                    }
                }
                catch (Exception) { }

                try
                {
                    string LogFilePath = GetPastaTrabalho() + "log.txt";
                    if (_isBatch)
                    {
                        LogFilePath = GetPastaTrabalho() + "logBatch.txt";
                    }
                    else if (_isRajada)
                        LogFilePath = GetPastaTrabalho() + "logRajada.txt";

                    if (File.Exists(LogFilePath) && !_isRajada)
                    {
                        try
                        {
                            if (File.Exists(LogFilePath))
                            {
                                DateTime lastWrite = File.GetLastWriteTime(LogFilePath);
                                if (lastWrite.Date < DateTime.Now.Date)
                                {
                                    string OldLogFilePath = GetPastaTrabalho() + "log\\" + lastWrite.ToString("yyyyMMdd") + ".txt";
                                    if (_isBatch)
                                    {
                                        OldLogFilePath = GetPastaTrabalho() + "log\\Batch" + lastWrite.ToString("yyyyMMdd") + ".txt";
                                    }
                                    else
                                    {
                                        if (!File.Exists(OldLogFilePath))
                                            File.Move(LogFilePath, OldLogFilePath);
                                    }
                                }
                            }
                        }
                        catch (Exception) { }
                    }

                    using (FileStream fs = File.Open(LogFilePath, FileMode.Append, FileAccess.Write, FileShare.None))
                    {
                        StringBuilder buffer = new StringBuilder();

                        buffer.Append("\r\n********************  " + data + "  ********************\r\n");
                        buffer.Append("Entrada: " + entrada.ToString() + " Retorno: " + saida.ToString() + " Duração: " + duracaoSegundos + "\r\n");

                        foreach (string texto in s)
                        {
                            buffer.Append(texto + "\r\n");
                        }
                        Byte[] info = new UTF8Encoding(true).GetBytes(buffer.ToString());
                        fs.Write(info, 0, info.Length);
                        fs.Close();
                    }

                    if (!_isRajada)
                    {
                        if (_isErro)
                        {
                            string LogFilePathErro = GetPastaTrabalho() + "logErro.txt";
                            using (FileStream fs = File.Open(LogFilePathErro, FileMode.Append, FileAccess.Write, FileShare.None))
                            {
                                StringBuilder buffer = new StringBuilder();

                                buffer.Append("\r\n********************  " + data + "  ********************\r\n");
                                buffer.Append("Entrada: " + entrada.ToString() + " Retorno: " + saida.ToString() + " Duração: " + duracaoSegundos + "\r\n");
                                foreach (string texto in s)
                                {
                                    buffer.Append(texto + "\r\n");
                                }
                                Byte[] info = new UTF8Encoding(true).GetBytes(buffer.ToString());
                                fs.Write(info, 0, info.Length);
                                fs.Close();
                            }
                        }

                        if (_isErroIS)
                        {
                            string LogFilePathErro = GetPastaTrabalho() + "logErroIS.txt";
                            using (FileStream fs = File.Open(LogFilePathErro, FileMode.Append, FileAccess.Write, FileShare.None))
                            {
                                StringBuilder buffer = new StringBuilder();

                                buffer.Append("\r\n********************  " + data + "  ********************\r\n");
                                buffer.Append("Entrada: " + entrada.ToString() + " Retorno: " + saida.ToString() + " Duração: " + duracaoSegundos + "\r\n");
                                foreach (string texto in s)
                                {
                                    buffer.Append(texto + "\r\n");
                                }
                                Byte[] info = new UTF8Encoding(true).GetBytes(buffer.ToString());
                                fs.Write(info, 0, info.Length);
                                fs.Close();
                            }
                        }

                        if (_isErroBroker)
                        {
                            string LogFilePathErro = GetPastaTrabalho() + "logErroBroker.txt";
                            using (FileStream fs = File.Open(LogFilePathErro, FileMode.Append, FileAccess.Write, FileShare.None))
                            {
                                StringBuilder buffer = new StringBuilder();

                                buffer.Append("\r\n********************  " + data + "  ********************\r\n");
                                buffer.Append("Entrada: " + entrada.ToString() + " Retorno: " + saida.ToString() + " Duração: " + duracaoSegundos + "\r\n");
                                foreach (string texto in s)
                                {
                                    buffer.Append(texto + "\r\n");
                                }
                                Byte[] info = new UTF8Encoding(true).GetBytes(buffer.ToString());
                                fs.Write(info, 0, info.Length);
                                fs.Close();
                            }
                        }

                        if (_isErroOracle)
                        {
                            string LogFilePathErro = GetPastaTrabalho() + "logErroOracle.txt";
                            using (FileStream fs = File.Open(LogFilePathErro, FileMode.Append, FileAccess.Write, FileShare.None))
                            {
                                StringBuilder buffer = new StringBuilder();

                                buffer.Append("\r\n********************  " + data + "  ********************\r\n");
                                buffer.Append("Entrada: " + entrada.ToString() + " Retorno: " + saida.ToString() + " Duração: " + duracaoSegundos + "\r\n");
                                foreach (string texto in s)
                                {
                                    buffer.Append(texto + "\r\n");
                                }
                                Byte[] info = new UTF8Encoding(true).GetBytes(buffer.ToString());
                                fs.Write(info, 0, info.Length);
                                fs.Close();
                            }
                        }
                    }
                }
                catch (Exception) { }
            }
            return id;
        }

        public void PrintLogTempoMedio()
        {
            double logTotalTempo = 0;
            long logContadorTempo = 0;
            if (HttpContext.Current.Application["LogContadorTempoConexao"] != null
                && HttpContext.Current.Application["LogTotalTempoConexao"] != null
                && HttpContext.Current.Application["LogUltimaAtualizacao"] != null)
            {
                HttpContext.Current.Application["LogUltimaAtualizacao"] = DateTime.Now;
                logTotalTempo = (double)HttpContext.Current.Application["LogTotalTempoConexao"];
                logContadorTempo = (long)HttpContext.Current.Application["LogContadorTempoConexao"];
                if (logContadorTempo > 0)
                {
                    double tempoMedio = Math.Round(logTotalTempo / logContadorTempo, 2);
                    string LogFilePathTempoMedio = GetPastaTrabalho() + "logTempoMedio.txt";
                    using (FileStream fs = File.Open(LogFilePathTempoMedio, FileMode.Append, FileAccess.Write, FileShare.None))
                    {
                        StringBuilder buffer = new StringBuilder();
                        buffer.Append(DateTime.Now.ToString("HH:mm:ss") + " Total conexões: " + logContadorTempo + " Tempo médio:" + tempoMedio + "s\r\n");
                        Byte[] info = new UTF8Encoding(true).GetBytes(buffer.ToString());
                        fs.Write(info, 0, info.Length);
                        fs.Close();
                    }
                }
                logTotalTempo = 0;
                logContadorTempo = 0;
            }
            HttpContext.Current.Application["LogTotalTempoConexao"] = logTotalTempo;
            HttpContext.Current.Application["LogContadorTempoConexao"] = logContadorTempo;
        }

        public void SetEntrada()
        {
            entrada = DateTime.Now;
        }

        public void SetNomeServico(string nomeServico)
        {
            this.nomeServico = nomeServico;
        }

        public void AddLog(string s,bool isErro)
        {
            texto.Add(s);
            _isErro = isErro;
        }

        public void AddLog(string s)
        {
            texto.Add(s);
        }

        public void ClearLog()
        {
            texto.Clear();
            _isErro = false;
            _isErroIS = false;
            _isErroBroker = false;
            _isErroOracle = false;
        }

    public void setErroIS()
        {
            _isErroIS = true;
        }

        public void setErroBroker()
        {
            _isErroBroker = true;
        }

        public void setErroOracle()
        {
            _isErroOracle = true;
        }

        public string PringLog()
        {
            string retorno = "";
            if (_isAsync)
            {
                retorno = GravaLog(texto.ToArray());
                texto.Clear();
            }
            else
            {
                retorno = GravaLog(texto.ToArray());
                texto.Clear();
            }
            _isAsync = false;
            _isErro = false;
            _isErroIS = false;
            _isErroBroker = false;
            _isErroOracle = false;
            return retorno;
        }

        public void InciaRajada(int quantidade)
        {
            string LogFilePath = GetPastaTrabalho() + "logRajada.txt";
            if (File.Exists(LogFilePath))
            {
                File.Delete(LogFilePath);
            }
            foreach (string nomeArquivo in Directory.GetFiles(GetPastaTrabalho() + "log"))
            {
                string nomeArquivoDelete = nomeArquivo.Replace(GetPastaTrabalho(), "");
                if (!"2020".Equals(nomeArquivo.Substring(GetPastaTrabalho().Length+4, 4)) && !"Batch".Equals(nomeArquivo.Substring(GetPastaTrabalho().Length+4, 5)))
                {
                    File.Delete(nomeArquivo);
                }
            }
            try
            {
                using (FileStream fs = File.Open(LogFilePath, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    StringBuilder buffer = new StringBuilder();

                    buffer.Append("\r\n****************************************\r\n");

                    buffer.Append("Rajada de " + quantidade + " conexões enviadas\r\n");
                    Byte[] info = new UTF8Encoding(true).GetBytes(buffer.ToString());
                    fs.Write(info, 0, info.Length);
                    fs.Close();
                }

            }
            catch (Exception) { }
        }

        /// <summary>
        /// Retorno o caminho até a pasta de trabalho
        /// </summary>
        public string GetPastaTrabalho()
        {
            
            return applicationPath;
        }

        public void IsAsync()
        {
            _isAsync = true;
        }

        public void IsBatch()
        {
            _isBatch = true;
        }

        public void IsRajada()
        {
            _isRajada = true;
        }
    }
}
