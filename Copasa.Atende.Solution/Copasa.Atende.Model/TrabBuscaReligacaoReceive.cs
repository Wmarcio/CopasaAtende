using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// BuscaReligacaoReceive - Religação
    /// </summary>
    public class TrabBuscaReligacaoReceive : BaseModelReceive
    {
        /// <summary>
        /// Dados Religação
        /// </summary>
        public SCN4ISRLReceive dados { get; set; }

        /// <summary>
        /// Dados Parcelamento
        /// </summary>
        public SCN4ISCPReceive dadosParcelamento { get; set; }

        /// <summary>
        /// Dados Faturas em Débito
        /// </summary>
        public SCN6ISFDReceive faturasEmDebito { get; set; }

    }
}
