using Copasa.Atende.Model.Core;
using Newtonsoft.Json;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// Dyn365User - Usuario Dynamics
    /// </summary>
    public class Dyn365User : BaseModel
    {
        /// <summary>
        /// Usuário do Dyn365
        /// </summary>
        [Dyn365Name("username")]
        public string Username { get; set; }

        /// <summary>
        /// Senha do Usr do Dyn365 criptografada
        /// </summary>
        [Dyn365Name("password")]
        public string Password { get; set; }

        /// <summary>
        /// Login do Contato do Dyn365
        /// </summary>
        [Dyn365Name("adxusername")]
        public string ADXUsername { get; set; }

        /// <summary>
        /// Senha do Contato do Dyn365 criptografada (SHA1)
        /// </summary>
        [Dyn365Name("adxpassword")]
        public string ADXPassword { get; set; }
        /// <summary>
        /// CPF/CNPJ
        /// </summary>
        [Dyn365Name("cpfcnpj")]
        public string CpfCnpj { get; set; }

        /// <summary>
        /// Nome do Usuário Portal
        /// </summary>
        [Dyn365Name("portalusername")]
        public string PortalUsername { get; set; }

        /// <summary>
        /// Senha do Usuário Portal
        /// </summary>
        [Dyn365Name("portaluserpassword")]
        public string PortalUserpassword { get; set; }

        /// <summary>
        /// Senha do Usuário Portal
        /// </summary>
        [Dyn365Name("portaluserpassword_btoa")]
        public string PortaluserPasswordBtoa { get; set; }

        /// <summary>
        /// Primeiro Nome
        /// </summary>
        [Dyn365Name("firstname")]
        public string Firstname { get; set; }

        /// <summary>
        /// Útilmo Nome
        /// </summary>
        [Dyn365Name("lastname")]
        public string Lastname { get; set; }

        /// <summary>
        /// Tipo de Telefone
        /// </summary>
        [Dyn365Name("phonetype")]
        public string Phonetype { get; set; }

        /// <summary>
        /// Telefone 1
        /// </summary>
        [Dyn365Name("telephone1")]
        public string Telephone1 { get; set; }

        /// <summary>
        /// Telefone 1
        /// </summary>
        [Dyn365Name("telephone2")]
        public string Telephone2 { get; set; }

        /// <summary>
        /// Celular
        /// </summary>
        [Dyn365Name("mobilephone")]
        public string Mobilephone { get; set; }

        /// <summary>
        /// Não possui e-mail
        /// </summary>
        [Dyn365Name("donotemail")]
        public string DoNotEmail { get; set; }

        /// <summary>
        /// Endereço de e-mail
        /// </summary>
        [Dyn365Name("emailaddress1")]
        public string EmailAddress1 { get; set; }

        /// <summary>
        /// Termo de Aceita COPASA
        /// </summary>
        [Dyn365Name("copasa_termoaceite")]
        public string CopasaTermoAceite { get; set; }

        /// <summary>
        /// Política de Privacidade COPASA
        /// </summary>
        [Dyn365Name("copasa_politicaprivacidade")]
        public string CopasaPoliticaPrivacidade { get; set; }

        /// <summary>
        /// Tipo do Cliente - Física ou Jurídica
        /// </summary>
        [Dyn365Name("copasa_tipocliente")]
        public string CopasaTipoCliente { get; set; }

        /// <summary>
        /// Validação e-mail (S, N, B, R ou C)
        /// </summary>
        [Dyn365Name("copasa_validacaoemail")]
        public string CopasaValidacaoEmail { get; set; }

        /// <summary>
        /// String no formato base64 que representa a imagem 
        /// </summary>
        [Dyn365Name("entityimage")]
        public string EntityImage { get; set; }

        /// <summary>
        /// GUID do Identificador
        /// </summary>
        [Dyn365Name("copasa_Identificadorid")]
        public string CopasaIdentificadorId { get; set; }

        /// <summary>
        /// GUID do Contato
        /// </summary>
        [Dyn365Name("copasa_ContatoId")]
        public string CopasaContatoId { get; set; }

        /// <summary>
        /// Código da Razão do Status
        /// </summary>
        [Dyn365Name("statuscode")]
        public string StatusCode { get; set; }

        /// <summary>
        /// Código do Status
        /// </summary>
        [Dyn365Name("statecode")]
        public string StateCode { get; set; }

        /// <summary>
        /// Senha Atual do Contato do Dyn365 criptografada (SHA1)
        /// </summary>
        [Dyn365Name("currentpassword")]
        public string CurrentPassword { get; set; }

        /// <summary>
        /// Nova Senha para o Contato do Dyn365 criptografada (SHA1)
        /// </summary>
        [Dyn365Name("newpassword")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Guid locality - Cidade
        /// </summary>
        [Dyn365Name("locality")]
        public string locality { get; set; }

        /// <summary>
        /// Guid neighborhood - Bairro
        /// </summary>
        [Dyn365Name("neighborhood")]
        public string neighborhood { get; set; }

        /// <summary>
        /// Guid contactid
        /// </summary>
        [Dyn365Name("contactid")]
        public string ContactId { get; set; }

    }
}
