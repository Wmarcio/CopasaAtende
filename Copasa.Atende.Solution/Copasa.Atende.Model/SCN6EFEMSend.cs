using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6EFEMSend - Segunda via fatura
    /// </summary>
    public class SCN6EFEMSend : BaseModel
    {
        /// <summary>
        /// NumeroFatura.
        /// </summary>
        [Broker("numeroFatura", 1, "N14")]
        public long numeroFatura { get; set; }

        /// <summary>
        /// Empresa.
        /// </summary>
        public string empresa { get; set; }
    }
}
