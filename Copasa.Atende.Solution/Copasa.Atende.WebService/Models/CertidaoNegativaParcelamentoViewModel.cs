namespace Copasa.Atende.WebService.Models
{
    /// <summary>
    /// Classe referente aos parcelamentos da certidão negativa
    /// </summary>
    public class CertidaoNegativaParcelamentoViewModel
    {
        /// <summary>
        /// Quantidade de parcelas
        /// </summary>
        public string parcelasParcial { get; set; }

        /// <summary>
        /// Valor restante do parcelamento
        /// </summary>
        public string valorRestanteParcial { get; set; }

        /// <summary>
        /// Valor total do parcelamento
        /// </summary>
        public string valorParcelaParcial { get; set; }
    }
}