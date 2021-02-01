using Copasa.Atende.Model.Core;
using Copasa.Util.Attributes;
using Copasa.Util.Enumerador;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Broker
{
    /// <summary>
    /// Modelo para consumo da função de alteração de contatos de usuario broker
    /// </summary>
    public class UsuarioCopasaModel : BaseModel
    {

        ///// <summary>
        ///// 
        ///// </summary>
        //[JsonIgnore]
        //public string LoginUsuario { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //[JsonIgnore]
        //public string Origem { get; set; }


        /// <summary>
        /// Identificador
        /// </summary>
        [ConvertString(Start = 0, Length = 11, DataType = (int)TipoDadosEnum.N)]
        public int Identificador { get; set; }

        /// <summary>
        /// InformacoesEmail
        /// </summary>
        [ConvertString(Start = 11, Length = 50, DataType = (int)TipoDadosEnum.A)]
        public string InformacoesEmail { get; set; }

        /// <summary>
        /// DDDTelefoneCelular
        /// </summary>
        [ConvertString(Start = 61, Length = 2, DataType = (int)TipoDadosEnum.N)]
        public int DDDTelefoneCelular { get; set; }

        /// <summary>
        /// NumeroTelefoneCelular
        /// </summary>
        [ConvertString(Start = 63, Length = 9, DataType = (int)TipoDadosEnum.N)]
        public int NumeroTelefoneCelular { get; set; }

        /// <summary>
        /// DDDTelefoneResidencial
        /// </summary>
        [ConvertString(Start = 72, Length = 2, DataType = (int)TipoDadosEnum.N)]
        public int DDDTelefoneResidencial { get; set; }

        /// <summary>
        /// NumeroTelefoneResidencial
        /// </summary>
        [ConvertString(Start = 74, Length = 9, DataType = (int)TipoDadosEnum.N)]
        public int NumeroTelefoneResidencial { get; set; }

        /// <summary>
        /// DDDTelefoneComercial
        /// </summary>
        [ConvertString(Start = 83, Length = 2, DataType = (int)TipoDadosEnum.N)]
        public int DDDTelefoneComercial { get; set; }

        /// <summary>
        /// NumeroTelefoneComercial
        /// </summary>
        [ConvertString(Start = 85, Length = 9, DataType = (int)TipoDadosEnum.N)]
        public int NumeroTelefoneComercial { get; set; }

        /// <summary>
        /// FlagGrava
        /// </summary>
        [ConvertString(Start = 94, Length = 1, DataType = (int)TipoDadosEnum.A)]
        public string FlagGrava { get; set; }

        /// <summary>
        /// Origem
        /// </summary>
        [ConvertString(Start = 95, Length = 3, DataType = (int)TipoDadosEnum.A)]
        public string Origem { get; set; }
    }
}
