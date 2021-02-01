using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365
{
    /// <summary>
    /// DGetComentariosReceive
    /// </summary>
    public class DComentariosReceive : BaseModelReceive
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Lista de comentarios
        /// </summary>
        public List<DComentario> comentarios;
    }

    /// <summary>
    /// DGetComentariosSend
    /// </summary>
    public class DComentariosSend : BaseModelReceive
    {
        /// <summary>
        /// protocolo
        /// </summary>
        public string protocolo { get; set; }
    }
}
