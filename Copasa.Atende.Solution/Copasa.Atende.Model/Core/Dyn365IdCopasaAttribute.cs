namespace Copasa.Atende.Model.Core
{
    /// <summary>
    /// Dyn365IdCopasaAttribute
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.All)]
    public class Dyn365IdCopasaAttribute : System.Attribute
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public Dyn365IdCopasaAttribute(string Name)
        {
            this.Name = Name;
        }

        /// <summary>
        /// Nome
        /// </summary>
        public string Name { get; set; }
    }
}
