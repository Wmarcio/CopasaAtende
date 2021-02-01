using Newtonsoft.Json;

namespace Copasa.Atende.Model.Core
{
    /// <summary>
    /// Classe Base das Entidades de Retorno
    /// </summary>
    public class BaseModelRetorno : BaseModelReceive
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public BaseModelRetorno()
        {
            descricaoRetornoSicom = "";
            retornoBroker = "";
        }

        /// <summary>
        /// DescricaoRetornoSicom.
        /// </summary>
        [Broker("descricaoRetornoSicom", 1, "A80")]
        [JsonIgnore]
        public string descricaoRetornoSicom { get; set; }

        /// <summary>
        /// RetornoBroker.
        /// </summary>
        [JsonIgnore]
        public string retornoBroker;
    }
}
