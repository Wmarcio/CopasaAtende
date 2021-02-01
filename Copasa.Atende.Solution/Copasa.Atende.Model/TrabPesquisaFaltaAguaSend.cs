using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// TrabPesquisaFaltaAguaSend  Verifica se há falta de água
    /// </summary>
    public class TrabPesquisaFaltaAguaSend : BaseModelSend
    {
        /// <summary>
        /// Identificador.
        /// </summary>
        public string identificador { get; set; }

        /// <summary>
        /// Matricula.
        /// </summary>
        public string matricula { get; set; }
    }
}
