using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// Tabela de empregados no Oracle
    /// </summary>
    public class ORAEmpregado : BaseModel
    {
        /// <summary>
        /// Matricula
        /// </summary>
        public int matricula { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        public string nome { get; set; }

        /// <summary>
        /// CodigoCargo
        /// </summary>
        public long codigoCargo { get; set; }

        /// <summary>
        /// Cargo
        /// </summary>
        public ORACargo cargo { get; set; }

        /// <summary>
        /// CodigoUnidadeOrganizacional
        /// </summary>
        public long codigoUnidadeOrganizacional { get; set; }

        /// <summary>
        /// UnidadeOrganizacional
        /// </summary>
        public ORACargo unidadeOrganizacional { get; set; }
    }
}
