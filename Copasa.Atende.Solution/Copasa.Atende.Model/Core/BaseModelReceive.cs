using Newtonsoft.Json;

namespace Copasa.Atende.Model.Core
{
    /// <summary>
    /// Classe Base das Entidades para retorno de dados do IBM
    /// </summary>
    public abstract class BaseModelReceive : BaseModel
    {
        /// <summary>
        /// CodigoRetorno.
        /// </summary>
        [JsonIgnore]
        public string codigoRetorno;

        /// <summary>
        /// MensagemRetorno.
        /// </summary>
        public string descricaoRetorno;

        /*
        /// <summary>
        /// CodigoRetornoSicom.
        /// </summary>
        [JsonIgnore]
        public string codigoRetornoSicom;

        /// <summary>
        /// MensagemRetornoSicom.
        /// </summary>
        [JsonIgnore]
        public string descricaoRetornoSicom;
        */
        /// <summary>
        /// DescricaoErroIS.
        /// </summary>
        [JsonIgnore]
        public string descricaoErroIS;

        /// <summary>
        /// IsValid.
        /// </summary>
        [JsonIgnore]
        public bool IsValid;
    }
}
