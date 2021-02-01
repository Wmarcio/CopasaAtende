namespace Copasa.Atende.WebService.Models
{
    /// <summary>
    /// Classe referente aos lançamentos da certidão negativa
    /// </summary>
    public class CertidaoNegativaLancamentoViewModel
    {
        /// <summary>
        /// Descrição do histórico
        /// </summary>
        public string descricaoHistorico { get; set; }

        /// <summary>
        /// Primeira descrição do histórico
        /// </summary>
        public string descricaoHistorico1 { get; set; }

        /// <summary>
        /// Segunda descrição do histórico
        /// </summary>
        public string descricaoHistorico2 { get; set; }

        /// <summary>
        /// Data de referência do lançamento
        /// </summary>
        public string dataReferenciaLancamento { get; set; }

        /// <summary>
        /// Número da fatura
        /// </summary>
        public string numeroFatura { get; set; }

        /// <summary>
        /// Valor de lançamento
        /// </summary>
        public string valorLancamento { get; set; }

        /// <summary>
        /// Data de Vencimento da fatura
        /// </summary>
        public string dataVencimentoFatura { get; set; }
    }
}