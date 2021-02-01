namespace Copasa.Atende.WebService.Models
{
    /// <summary>
    /// Classe referente aos débitos da certidão negativa
    /// </summary>
    public class CertidaoNegativaDebitoViewModel
    {
        /// <summary>
        /// Número da Fatura.
        /// </summary>        
        public long numeroFatura { get; set; }

        /// <summary>
        /// Valor Total da Fatura.
        /// </summary>        
        public string valorTotalfatura { get; set; }

        /// <summary>
        /// Data do Vencimento da Fatura.
        /// </summary>        
        public string dataVencimentoFatura { get; set; }
    }
}