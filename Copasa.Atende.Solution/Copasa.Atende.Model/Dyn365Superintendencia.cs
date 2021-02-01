using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// Dyn365Superintendencia - Tabela de superintendências no Dynamics
    /// </summary>
    [Dyn365Name("copasa_unidades")]
    public class Dyn365Superintendencia : BaseModel
    {
        /// <summary>
        /// Id.
        /// </summary>
        [Dyn365Id("copasa_superintendenciaid")]
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

        /// <summary>
        /// EmailGerente.
        /// </summary>
        [Dyn365Name("copasa_emailgerente")]
        public string emailGerente { get; set; }

        /// <summary>
        /// Empresa.
        /// </summary>
        [Dyn365Name("copasa_empresa")]
        public string empresa { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        [Dyn365Name("emailaddress")]
        public string email { get; set; }
    }
}
