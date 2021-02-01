using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365
{

    /// <summary>
    /// Retorno da consulta de usuário
    /// </summary>
    public class DConsultaUsuarioReceive : BaseModelReceive
    {
        /// <summary>
        /// CPF/CNPJ
        /// </summary>
        [Dyn365Name("copasa_cpf_cnpj")]
        public string cpfcnpj { get; set; }

        /// <summary>
        /// Nome do Usuário Portal
        /// </summary>
        [Dyn365Name("adx_identity_username")]
        public string username { get; set; }

        ///// <summary>
        ///// Senha do Usuário Portal Btoa
        ///// </summary>
        //[Dyn365Name("portaluserpassword_btoa")]
        //public string PortaluserPasswordBtoa { get; set; }

        /// <summary>
        /// Primeiro Nome
        /// </summary>
        [Dyn365Name("firstname")]
        public string firstname { get; set; }

        /// <summary>
        /// Útilmo Nome
        /// </summary>
        [Dyn365Name("lastname")]
        public string lastname { get; set; }

        /// <summary>
        /// Tipo de Telefone Whatsapp, Viber
        /// </summary>
        [Dyn365Name("copasa_tipotelefone")]
        public string phonetype { get; set; }

        /// <summary>
        /// Telefone 1
        /// </summary>
        [Dyn365Name("telephone1")]
        public string telephone1 { get; set; }

        /// <summary>
        /// Telefone 1
        /// </summary>
        [Dyn365Name("telephone2")]
        public string telephone2 { get; set; }

        /// <summary>
        /// Celular
        /// </summary>
        [Dyn365Name("mobilephone")]
        public string mobilephone { get; set; }

        /// <summary>
        /// Não possui e-mail
        /// </summary>
        [Dyn365Name("donotemail")]
        public string donotemail { get; set; }

        /// <summary>
        /// Endereço de e-mail
        /// </summary>
        [Dyn365Name("emailaddress1")]
        public string emailaddress1 { get; set; }

        /// <summary>
        /// Termo de Aceita COPASA
        /// </summary>
        [Dyn365Name("copasa_termoaceite")]
        public string copasa_termoaceite { get; set; }

        /// <summary>
        /// Política de Privacidade COPASA
        /// </summary>
        [Dyn365Name("copasa_politicaprivacidade")]
        public string copasa_politicaprivacidade { get; set; }

        /// <summary>
        /// Tipo do Cliente - Física ou Jurídica
        /// </summary>
        [Dyn365Name("copasa_tipocliente")]
        public string copasa_tipocliente { get; set; }

        /// <summary>
        /// Validação e-mail (S, N, B, R ou C)
        /// </summary>
        [Dyn365Name("copasa_validacaoemail")]
        public string copasa_validacaoemail { get; set; }

        /// <summary>
        /// String no formato base64 que representa a imagem 
        /// </summary>
        [Dyn365Name("entityimage")]
        public string entityimage { get; set; }

        /// <summary>
        /// GUID que representa a cidade
        /// </summary>
        [Dyn365Name("_copasa_localidadeid_value")]
        public string locality { get; set; }

        /// <summary>
        /// GUID que representa o bairro 
        /// </summary>
        [Dyn365Name("_copasa_bairroid_value")]
        public string neighborhood { get; set; }

    }
}
