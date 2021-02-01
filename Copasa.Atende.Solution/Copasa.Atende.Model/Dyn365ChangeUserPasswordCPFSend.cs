using Copasa.Atende.Model.Core;
using Newtonsoft.Json;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// ChangeDyn365UserPasswordSend - Altera a Senha de um Contato no Microsoft Dynamics 365.
    /// </summary>
    public class Dyn365ChangeUserPasswordCpfSend : BaseModelSend
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
        /// Nova Senha para o Contato do Dyn365 criptografada (SHA1)
        /// </summary>
        [JsonProperty("newpassword")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Login do Contato do Dyn365
        /// </summary>
        [JsonProperty("cpfcnpj")]
        public string CpfCnpj { get; set; }

        /// <summary>
        /// Senha do Contato do Dyn365 criptografada (SHA1)
        /// </summary>
        [JsonProperty("currentpassword")]
        public string CurrentPassword { get; set; }

    }
}
