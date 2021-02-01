using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365
{
    /// <summary>
    /// Entrada do servico de historico de consumo
    /// </summary>
    public class DFaturasSend : BaseModel
    {

        /// <summary>
        /// numero do identificador
        /// </summary>
        public string Identificador { get; set; }

        /// <summary>
        /// matricula
        /// </summary>
        public string Matricula { get; set; }

        /// <summary>
        /// numero do protocolo
        /// </summary>
        public string ProtocoloNumero { get; set; }

        /// <summary>
        /// id do protocolo
        /// </summary>
        public string ProtocoloId { get; set; }


        /// <summary>
        /// Servico
        /// </summary>
        public string Servico { get; set; }

        /// <summary>
        /// Subtipo
        /// </summary>
        public string SubTipo { get; set; }

        /// <summary>
        /// Pavimentacao
        /// </summary>
        public string Pavimentacao { get; set; }

        /// <summary>
        /// Empresa(COPASA/COPANOR)
        /// </summary>
        public string Empresa { get; set; }

        /// <summary>
        /// Código do servico
        /// </summary>
        public string Origem { get; set; }
    }
}
