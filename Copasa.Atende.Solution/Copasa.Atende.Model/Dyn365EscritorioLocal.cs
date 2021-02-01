using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// Dyn365EscritorioLocal - Tabela de escritórios locais no Dynamics
    /// </summary>
    public class Dyn365EscritorioLocal : BaseModel
    {
        /// <summary>
        /// Id.
        /// </summary>
        [Dyn365Id("copasa_escritoriolocalid")]
        public string id { get; set; }

        /// <summary>
        /// Codigo.
        /// </summary>
        [Dyn365Name("copasa_codigo")]
        public string codigo { get; set; }

        /// <summary>
        /// Sigla.
        /// </summary>
        [Dyn365Name("copasa_name")]
        public string sigla { get; set; }

        /// <summary>
        /// Descricao.
        /// </summary>
        [Dyn365Name("copasa_descricao")]
        public string descricao { get; set; }
    }
}

