using Newtonsoft.Json;

namespace Copasa.Atende.Model.Core
{
    /// <summary>
    /// Classe Base das Entidades para retorno de dados do IBM
    /// </summary>
    public abstract class BaseModelReceiveTeste : BaseModel
    {
        /// <summary>
        /// CodigoRetorno.
        /// </summary>
        [JsonIgnore]
        public string codigoRetorno { get; set; }

        /// <summary>
        /// MensagemRetorno.
        /// </summary>
        public string descricaoRetorno { get; set; }

        /// <summary>
        /// CodigoRetornoSicom.
        /// </summary>
        [JsonIgnore]
        public virtual string codigoRetornoSicom { get; set; }

        /// <summary>
        /// MensagemRetornoSicom.
        /// </summary>
        [JsonIgnore]
        public virtual string descricaoRetornoSicom { get; set; }

        /// <summary>
        /// DescricaoErroIS.
        /// </summary>
        [JsonIgnore]
        public string descricaoErroIS { get; set; }

        /// <summary>
        /// IsValid.
        /// </summary>
        [JsonIgnore]
        public bool IsValid { get; set; }
    }
}
