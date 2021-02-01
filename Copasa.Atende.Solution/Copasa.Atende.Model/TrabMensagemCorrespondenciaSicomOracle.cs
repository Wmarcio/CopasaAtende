using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// TrabMensagemCorrespondencia - Correspondência entre código retorno do Sicom e código do Oracle
    /// </summary>
    public class TrabMensagemCorrespondenciaSicomOracle : BaseModel
    {
        /// <summary>
        /// codigoSicom.
        /// </summary>
        public int codigoSicom { get; set; }

        /// <summary>
        /// ProgramaSicom.
        /// </summary>
        public string programaSicom { get; set; }

        /// <summary>
        /// CodigoOracle.
        /// </summary>
        public string codigoOracle { get; set; }

        /// <summary>
        /// TipoUsuario.
        /// </summary>
        public int tipoUsuario { get; set; }

        /// <summary>
        /// CondicaoGenerica.
        /// </summary>
        public int condicaoGenerica { get; set; }

        /// <summary>
        /// TextoComplementar.
        /// </summary>
        public string textoComplementar { get; set; }

        /// <summary>
        /// TextoComplementar.
        /// </summary>
        public string textoComplementar2 { get; set; }
    }
}
