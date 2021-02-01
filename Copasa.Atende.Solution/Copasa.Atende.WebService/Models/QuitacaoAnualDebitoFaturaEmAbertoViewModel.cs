namespace Copasa.Atende.WebService.Models
{
    /// <summary>
    /// Classe referente às faturas em abeto da quitação anual de débito
    /// </summary>
    public class QuitacaoAnualDebitoFaturaEmAbertoViewModel
    {
        /// <summary>
        /// NumeroFatura.
        /// </summary>
        public string numeroFatura { get; set; }

        /// <summary>
        /// Referencia.
        /// </summary>
        public string referencia { get; set; }

        /// <summary>
        /// Valor.
        /// </summary>
        public string valor { get; set; }

        /// <summary>
        /// DataVencimento.
        /// </summary>
        public string dataVencimento { get; set; }

        /// <summary>
        /// Endereco.
        /// </summary>
        public string endereco { get; set; }
    }
}