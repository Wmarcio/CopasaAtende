using Copasa.Atende.Model.Core;
using Newtonsoft.Json;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// Associa um Identificador com um Contato no Microsoft Dynamics 365.
    /// </summary>
    public class Dyn365AssociateIdentifierXUserSend : BaseModel
    {
        /// <summary>
        /// Usuário do Dyn365
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// Senha do Usr do Dyn365 criptografada
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// GUID do Identificador
        /// </summary>
        [JsonProperty("copasa_Identificadorid")]
        public string CopasaIdentificadorId { get; set; }

        /// <summary>
        /// GUID do Contato
        /// </summary>
        [JsonProperty("copasa_ContatoId")]
        public string CopasaContatoId { get; set; }
    }
}
