using Copasa.Atende.Model.Core;
using Newtonsoft.Json;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN3PCLIReceive - Dados básicos cliente
    /// </summary>
    public class SCN3PCLIReceive : BaseModelRetorno
    {
        /// <summary>
        /// DescricaoRetornoSicom.
        /// </summary>
        [Broker("descricaoRetornoSicom", 1, "A80")]
        [JsonIgnore]
        public new string descricaoRetornoSicom { get; set; }

        /// <summary>
        /// Usuarios.
        /// </summary>
        [Broker("identificadores", 2, "SCN3PCLIReceiveIdentificador", 50)]
        public SCN3PCLIReceiveIdentificador[] identificadores { get; set; }

    }
}
