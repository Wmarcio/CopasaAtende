using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365.Protocolo
{

    /// <summary>
    /// Retorno da consulta de servico
    /// </summary>
   public class DServicoReceive : BaseModel
    {
        /// <summary>
        /// consulta do servico - Id do serviço do Dyn365
        /// </summary>
        [Dyn365Name("copasa_portfoliodeservicoid")]
        public string ServicoId { get; set; }

        /// <summary>
        /// consulta subtipo do servico - Id do subtipo
        /// </summary>
        [Dyn365Name("copasa_subtipodeservicoid")]
        public string SubtipoId { get; set; }

        /// <summary>
        /// Consulta pacimentação - Id da pavimentacao
        /// </summary>
        [Dyn365Name("copasa_tipodepavimentacaoid")]
        public string PavimentacaoId { get; set; }

    }
}
