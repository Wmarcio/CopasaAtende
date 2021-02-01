using Newtonsoft.Json;

namespace Copasa.Atende.Model.Core
{
    /// <summary>
    /// BaseModelAzureCopaUserReceive - Modelo Base de Retorno dos WS Azure CopaUser
    /// </summary>
    public class BaseModelAzureCopaUserReceive : BaseModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Registros
        /// </summary>
        [JsonProperty("records")]
        public string Records { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Mensagem
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Protocolo
        /// </summary>
        [JsonProperty("protocol")]
        public string Protocol { get; set; }
    }
}
