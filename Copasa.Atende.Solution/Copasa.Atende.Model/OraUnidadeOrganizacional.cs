using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// Tabela unidade organizacional no Oracle
    /// </summary>
    public class ORAUnidadeOrganizacional : BaseModel
    {
        /// <summary>
        /// Codigo
        /// </summary>
        public long codigo { get; set; }

        /// <summary>
        /// Sigla
        /// </summary>
        public string sigla { get; set; }

        /// <summary>
        /// Siglao
        /// </summary>
        public string siglao { get; set; }
    }
}
