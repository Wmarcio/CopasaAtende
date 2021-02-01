using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6UEFIReceive - Informa identificador e retorna matriculas
    /// </summary>
    public class SCN6UEFIReceive : BaseModelRetorno
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public SCN6UEFIReceive()
        {
            matriculas = new List<SCN6UEFIReceiveMatriculas>();
        }

        /// <summary>
        /// Total.
        /// </summary>
        [Broker("total", 1, "N3")]
        [JsonIgnore]
        public int total { get; set; }

        /// <summary>
        /// MatriculaSicom.
        /// </summary>
        [Broker("matriculaSicom", 2, "A11", 75)]
        [JsonIgnore]
        public string[] matriculaSicom { get; set; }

        /// <summary>
        /// Matriculas.
        /// </summary>
        public List<SCN6UEFIReceiveMatriculas> matriculas { get; set; }
        //public List<string> matriculas { get; set; }

        /// <summary>
        /// Endereco.
        /// </summary>
        [Broker("endereco", 3, "A35", 75)]
        [JsonIgnore]
        public string[] endereco { get; set; }

        /// <summary>
        /// inicio.
        /// </summary>
        [Broker("inicio", 4, "A8", 75)]
        [JsonIgnore]
        public string[] inicio { get; set; }

        /// <summary>
        /// Termino.
        /// </summary>
        [Broker("termino", 5, "A8", 75)]
        [JsonIgnore]
        public string[] termino { get; set; }

        /// <summary>
        /// Bairro.
        /// </summary>
        [Broker("bairro", 6, "A35", 75)]
        [JsonIgnore]
        public string[] bairro { get; set; }

        /// <summary>
        /// Localidade.
        /// </summary>
        [Broker("localidade", 7, "A30", 75)]
        [JsonIgnore]
        public string[] localidade { get; set; }
    }
}
