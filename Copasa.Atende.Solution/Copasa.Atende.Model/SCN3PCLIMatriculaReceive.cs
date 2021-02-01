using Copasa.Atende.Model.Core;
using System.Collections.Generic;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN3PCLIMatriculaReceive - Dados básicos matricula
    /// </summary>
    public class SCN3PCLIMatriculaReceive : BaseModelRetorno
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public SCN3PCLIMatriculaReceive()
        {
            matriculas = new List<SCN3PCLIReceiveIdentificador>();
        }

        /// <summary>
        /// Matriculas.
        /// </summary>
        public List<SCN3PCLIReceiveIdentificador> matriculas { get; set; }
        //public List<string> matriculas { get; set; }

    }
}
