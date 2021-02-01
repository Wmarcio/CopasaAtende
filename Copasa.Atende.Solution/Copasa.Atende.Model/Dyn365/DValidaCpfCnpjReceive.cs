using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model.Dyn365
{
    /// <summary>
    /// Dyn365ValidaCpfCnpjReceive - Valida cpfCnpjDyn365
    /// </summary>
    public class DValidaCpfCnpjReceive : BaseModelReceive
    {
        /// <summary>
        /// Identificador.
        /// </summary>
        [Dyn365Id("contactid")]
        public string CpfCnpjId { get; set; }
    }
}
