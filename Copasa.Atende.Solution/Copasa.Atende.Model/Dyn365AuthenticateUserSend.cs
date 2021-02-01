using Copasa.Atende.Model.Core;
using Newtonsoft.Json;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// AuthenticateDyn365UserSend - Autentica um Contato no Microsoft Dynamics 365.
    /// </summary>
    public class Dyn365AuthenticateUserSend : BaseModel
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
        /// Login do Contato do Dyn365
        /// </summary>
        [JsonProperty("adxusername")]
        public string ADXUsername { get; set; }

        /// <summary>
        /// Senha do Contato do Dyn365 criptografada (SHA1)
        /// </summary>
        [JsonProperty("adxpassword")]
        public string ADXPassword { get; set; }
    }
}
