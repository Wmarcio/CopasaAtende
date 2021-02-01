using Copasa.Atende.Model.Core;
using Newtonsoft.Json;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// UpdateDyn365PortalUserSend - Atualiza um Contato no Microsoft Dynamics 365.
    /// </summary>
    public class Dyn365UpdatePortalUserSend : BaseModel
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
        /// CPF/CNPJ
        /// </summary>
        [JsonProperty("cpfcnpj")]
        public string CpfCnpj { get; set; }

        /// <summary>
        /// Nome do Usuário Portal
        /// </summary>
        [JsonProperty("portalusername")]
        public string PortalUsername { get; set; }

        /// <summary>
        /// Senha do Usuário Portal
        /// </summary>
        [JsonProperty("portaluserpassword")]
        public string PortalUserpassword { get; set; }

        /// <summary>
        /// Senha do Usuário Portal
        /// </summary>
        [JsonProperty("portaluserpassword_btoa")]
        public string PortaluserPasswordBtoa { get; set; }

        /// <summary>
        /// Primeiro Nome
        /// </summary>
        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        /// <summary>
        /// Útilmo Nome
        /// </summary>
        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        /// <summary>
        /// Tipo de Telefone
        /// </summary>
        [JsonProperty("phonetype")]
        public string Phonetype { get; set; }

        /// <summary>
        /// Telefone 1
        /// </summary>
        [JsonProperty("telephone1")]
        public string Telephone1 { get; set; }

        /// <summary>
        /// Telefone 1
        /// </summary>
        [JsonProperty("telephone2")]
        public string Telephone2 { get; set; }

        /// <summary>
        /// Celular
        /// </summary>
        [JsonProperty("mobilephone")]
        public string Mobilephone { get; set; }

        /// <summary>
        /// Não possui e-mail
        /// </summary>
        [JsonProperty("donotemail")]
        public string DoNotEmail { get; set; }

        /// <summary>
        /// Endereço de e-mail
        /// </summary>
        [JsonProperty("emailaddress1")]
        public string EmailAddress1 { get; set; }

        /// <summary>
        /// Termo de Aceita COPASA
        /// </summary>
        [JsonProperty("copasa_termoaceite")]
        public string CopasaTermoAceite { get; set; }

        /// <summary>
        /// Política de Privacidade COPASA
        /// </summary>
        [JsonProperty("copasa_politicaprivacidade")]
        public string CopasaPoliticaPrivacidade { get; set; }

        /// <summary>
        /// Tipo do Cliente - Física ou Jurídica
        /// </summary>
        [JsonProperty("copasa_tipocliente")]
        public string CopasaTipoCliente { get; set; }

        /// <summary>
        /// Validação e-mail (S, N, B, R ou C)
        /// </summary>
        [JsonProperty("copasa_validacaoemail")]
        public string CopasaValidacaoEmail { get; set; }

        /// <summary>
        /// String no formato base64 que representa a imagem 
        /// </summary>
        [JsonProperty("entityimage")]
        public string EntityImage { get; set; }

        /// <summary>
        /// GUID que representa a cidade 
        /// </summary>
        [JsonProperty("locality")]
        public string locality { get; set; }

        /// <summary>
        /// GUID que representa o bairro
        /// </summary>
        [JsonProperty("neighborhood")]
        public string neighborhood { get; set; }
    }
}
