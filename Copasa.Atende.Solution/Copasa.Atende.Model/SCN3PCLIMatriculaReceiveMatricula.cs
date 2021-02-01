using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN3PCLIMatriculaReceiveMatricula - Dados básicos cliente
    /// </summary>
    public class SCN3PCLIMatriculaReceiveMatricula : SCN3PCLIReceiveIdentificador
    {
        /// <summary>
        /// Matricula.
        /// </summary>
        public string matricula { get; set; }

        /// <summary>
        /// inicio.
        /// </summary>
        public string inicio { get; set; }

        /// <summary>
        /// Termino.
        /// </summary>
        public string termino { get; set; }

        /// <summary>
        /// numeroImovel.
        /// </summary>
        [JsonIgnore]
        public override int numeroImovel { get; set; }

        /// <summary>
        /// complementoImovel.
        /// </summary>
        [JsonIgnore]
        public override string complementoImovel { get; set; }

        /// <summary>
        /// UF.
        /// </summary>
        [JsonIgnore]
        public override string UF { get; set; }

        /// <summary>
        /// CEP.
        /// </summary>
        [JsonIgnore]
        public override string CEP { get; set; }

    }
}
