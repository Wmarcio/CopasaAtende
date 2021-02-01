using Copasa.Atende.Model.Core;
using Newtonsoft.Json;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// TrabParametroSend - Busca Mensagem Informativa
    /// </summary>
    public class TrabParametroSend : BaseModel
    {
        /// <summary>
        /// Origem
        /// </summary>
        public string Origem { get; set; }
    }
}
