using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// Dyn365Localidade - Tabela de localidades no Dynamics
    /// </summary>
    [Dyn365Name("copasa_localidades")]
    public class Dyn365Localidade : BaseModel
    {
        /// <summary>
        /// Id.
        /// </summary>
        [Dyn365Id("copasa_localidadeid")]
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
        public string descricao { get; set; }
    }
}
