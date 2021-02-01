using Copasa.Atende.Model.Core;
using Copasa.Util.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Broker
{

    /// <summary>
    /// Modelo de retorno para alteração de contatos de usuario broker
    /// </summary>
    public class UsuarioCopasaReceive : BaseModel
    {
        /// <summary>
        /// MensagemRetorno
        /// </summary>
        [ConvertString(Start = 0, Length = 80)]
        public string MensagemRetorno { get; set; }

        /// <summary>
        /// Identificador
        /// </summary>
        [ConvertString(Start = 80, Length = 11)]
        public int Identificador { get; set; }

        /// <summary>
        /// NomeUsuario
        /// </summary>
        [ConvertString(Start = 91, Length = 35)]
        public string NomeUsuario { get; set; }

        /// <summary>
        /// CPFCNPJUsuario
        /// </summary>
        [ConvertString(Start = 126, Length = 14)]
        public string CPFCNPJUsuario { get; set; }

        /// <summary>
        /// TipoCPFCNPJUsuario
        /// </summary>
        [ConvertString(Start = 140, Length = 1)]
        public string TipoCPFCNPJUsuario { get; set; }

        /// <summary>
        /// DDDTelefoneCelular
        /// </summary>
        [ConvertString(Start = 141, Length = 2)]
        public int DDDTelefoneCelular { get; set; }

        /// <summary>
        /// NumeroTelefoneCelular
        /// </summary>
        [ConvertString(Start = 143, Length = 9)]
        public int NumeroTelefoneCelular { get; set; }

        /// <summary>
        /// DDDTelefoneResidencial
        /// </summary>
        [ConvertString(Start = 152, Length = 2)]
        public int DDDTelefoneResidencial { get; set; }

        /// <summary>
        /// NumeroTelefoneResidencial
        /// </summary>
        [ConvertString(Start = 154, Length = 9)]
        public int NumeroTelefoneResidencial { get; set; }

        /// <summary>
        /// DDDTelefoneComercial
        /// </summary>
        [ConvertString(Start = 163, Length = 2)]
        public int DDDTelefoneComercial { get; set; }

        /// <summary>
        /// NumeroTelefoneComercial
        /// </summary>
        [ConvertString(Start = 165, Length = 9)]
        public int NumeroTelefoneComercial { get; set; }

        /// <summary>
        /// TipoLogradouro
        /// </summary>        
        [ConvertString(Start = 174, Length = 2)]
        public string TipoLogradouro { get; set; }

        /// <summary>
        /// NomeLogradouro
        /// </summary>        
        [ConvertString(Start = 176, Length = 40)]
        public string NomeLogradouro { get; set; }

        /// <summary>
        /// NumeroImovel
        /// </summary>        
        [ConvertString(Start = 216, Length = 5)]
        public int NumeroImovel { get; set; }

        /// <summary>
        /// TipoComplemento
        /// </summary>        
        [ConvertString(Start = 221, Length = 2)]
        public string TipoComplemento { get; set; }

        /// <summary>
        /// InformacaoComplemento
        /// </summary>        
        [ConvertString(Start = 223, Length = 12)]
        public string InformacaoComplemento { get; set; }

        /// <summary>
        /// NomeBairro
        /// </summary>        
        [ConvertString(Start = 235, Length = 30)]
        public string NomeBairro { get; set; }

        /// <summary>
        /// NumeroCEP
        /// </summary>        
        [ConvertString(Start = 265, Length = 8)]
        public string NumeroCEP { get; set; }

        /// <summary>
        /// NomeLocalidade
        /// </summary>        
        [ConvertString(Start = 273, Length = 30)]
        public string NomeLocalidade { get; set; }

        /// <summary>
        /// InformacaoEmail
        /// </summary>        
        [ConvertString(Start = 303, Length = 50)]
        public string InformacaoEmail { get; set; }

        /// <summary>
        /// CodigoRetorno
        /// </summary>        
        [ConvertString(Start = 353, Length = 3)]
        public string CodigoRetorno { get; set; }
    }
}
