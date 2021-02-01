using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// Tabela de cargos no Oracle
    /// </summary>
    public class ORACargo : BaseModel
    {
        /// <summary>
        /// Codigo
        /// </summary>
        public long codigo { get; set; }

        /// <summary>
        /// Descricao
        /// </summary>
        public string descricao { get; set; }
    }
}
