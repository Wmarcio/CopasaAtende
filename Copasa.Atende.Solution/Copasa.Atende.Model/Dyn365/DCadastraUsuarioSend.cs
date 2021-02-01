using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365
{
    /// <summary>
    /// CreateDyn365PortalUserSend - Cria um Contato Novo no Microsoft Dynamics 365.
    /// </summary>
   public class DCadastraUsuarioSend : BaseModel
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
        /// Guid locality - Cidade
        /// </summary>
        [Dyn365Name("locality")]
        public string Locality { get; set; }

        /// <summary>
        /// Guid neighborhood - Bairro
        /// </summary>
        [Dyn365Name("neighborhood")]
        public string Neighborhood { get; set; }
    }
}
