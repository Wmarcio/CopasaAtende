using Copasa.Atende.Model.Core;
using Newtonsoft.Json;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// RecoveryDyn365UserPasswordSend - Gera uma Nova Senha para um Contato no Microsoft Dynamics 365.
    /// </summary>
    public class Dyn365RecoveryUserPasswordSend : BaseModel
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
        /// CPF_CNPJ do Contato do Dyn365
        /// </summary>
        [JsonProperty("cpfcnpj")]
        public string CpfCnpj { get; set; }

        /// <summary>
        /// Origem
        /// </summary>
        public string Origem { get; set; }
    }
}
