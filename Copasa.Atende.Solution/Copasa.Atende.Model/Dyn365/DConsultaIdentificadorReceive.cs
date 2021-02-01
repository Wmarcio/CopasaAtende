using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365
{

    /// <summary>
    /// retorno da consulta de identificador no dyn365
    /// </summary>
    public class DConsultaIdentificadorReceive : BaseModelReceive
    {
        /// <summary>
        /// Id do identificador do Dyn365
        /// </summary>
        [Dyn365Name("copasa_identificadorid")]
        public string IdentificadorId { get; set; }

        /// <summary>
        /// Nome/codigo do identificador do D365
        /// </summary>
        [Dyn365Name("copasa_name")]
        public string Identificador { get; set; }
    }
}
