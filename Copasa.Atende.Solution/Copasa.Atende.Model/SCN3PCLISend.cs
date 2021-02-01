using Copasa.Atende.Model.Core;
using Newtonsoft.Json;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN3PCLISend - Dados básicos cliente
    /// </summary>
    public class SCN3PCLISend : BaseModelSend
    {
        /// <summary>
        /// CpfCnpjSicom.
        /// </summary>
        [Broker("CpfCnpjSicom", 1, "N14")]
        [JsonIgnore]
        public long CpfCnpjSicom { get; set; }

        /// <summary>
        /// identificadorSicom.
        /// </summary>
        [Broker("identificadorSicom", 2, "N11")]
        [JsonIgnore]
        public long identificadorSicom { get; set; }

        /// <summary>
        /// CpfCnpj.
        /// </summary>
        public string CpfCnpj { get; set; }
    }
}
