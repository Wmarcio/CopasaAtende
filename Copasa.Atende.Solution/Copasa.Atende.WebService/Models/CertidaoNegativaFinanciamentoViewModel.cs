using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Copasa.Atende.WebService.Models
{
    /// <summary>
    /// Financiamentos da certidão negativa de débito
    /// </summary>
    public class CertidaoNegativaFinanciamentoViewModel
    {
        /// <summary>
        /// Quantidade de parcelas de financiamento
        /// </summary>
        public string parcelasFinanciamento { get; set; }

        /// <summary>
        /// Valor restante do financiamento
        /// </summary>
        public string valorRestanteFinanciamento { get; set; }

        /// <summary>
        /// Valor de parcelas do financiamento
        /// </summary>
        public string valorParcelaFinanciamento { get; set; }
    }
}