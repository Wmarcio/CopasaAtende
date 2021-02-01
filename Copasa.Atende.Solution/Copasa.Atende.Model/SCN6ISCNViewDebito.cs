using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISCNViewDebito - Certidão negativa de débito
    /// </summary>
    public class SCN6ISCNViewDebito : BaseModel
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
