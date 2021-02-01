using System;

namespace Copasa.Atende.Util
{
    /// <summary>
    /// Interface de classe que contem as ferramentas para registrar logs.
    /// </summary>
    public interface ILog
    {
        string GravaLog(string s);

        /// <summary>
        /// Grava entrada em arquivo de log
        /// </summary>
        string GravaLog(string[] s);

        void IsAsync();


        void IsBatch();

        void IsRajada();

        void AddLog(string s);

        void AddLog(string s, bool isErro);

        void ClearLog();

        string PringLog();

        void setErroIS();

        void setErroBroker();

        void setErroOracle();

        void SetEntrada();

        void SetNomeServico(string nomeServico);

        void GravaLogAdc(string nomeServico, DateTime entradaParm);

        void GravaLogAdc(string nomeServico, DateTime entradaParm,string erro);

        void GravaLogAdc(DateTime entradaParm);

        void GravaLogAdc(DateTime entradaParm, string erro);

        /// <summary>
        /// Retorno o caminho até a pasta de trabalho
        /// </summary>
        string GetPastaTrabalho();

        void PrintTexto(string texto);

        void PrintLogTempoMedio();

        void InciaRajada(int quantidade);
    }
}
