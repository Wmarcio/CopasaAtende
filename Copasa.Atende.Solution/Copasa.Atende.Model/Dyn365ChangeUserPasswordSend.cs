using Copasa.Atende.Model.Core;
using Newtonsoft.Json;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// ChangeDyn365UserPasswordSend - Altera a Senha de um Contato no Microsoft Dynamics 365.
    /// </summary>
    public class Dyn365ChangeUserPasswordSend : BaseModelSend
    {
        /// <summary>
        /// Usuário do Dyn365
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// Senha do Usr do Dyn365 criptografada (SHA1)
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// Id do Contato do Dyn365
        /// </summary>
        [JsonProperty("contactid")]
        public string ContactId { get; set; }

        /// <summary>
        /// Nova Senha para o Contato do Dyn365 criptografada (SHA1)
        /// </summary>
        [JsonProperty("newpassword")]
        public string NewPassword { get; set; }

    }
}
