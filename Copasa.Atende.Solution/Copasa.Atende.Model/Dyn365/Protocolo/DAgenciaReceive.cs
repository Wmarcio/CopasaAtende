using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365.Protocolo
{
    /// <summary>
    /// retorno da consulta de agência - copasa_agenciafisicas
    /// </summary>
    public class DAgenciaReceive : BaseModelReceive
    {
        /// <summary>
        /// Id da agência
        /// </summary>
        [Dyn365Name("copasa_agenciafisicaid")]
        public string AgenciaId { get; set; }
    }
}
