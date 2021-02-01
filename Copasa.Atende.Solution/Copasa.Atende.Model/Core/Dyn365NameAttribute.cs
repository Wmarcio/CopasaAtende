namespace Copasa.Atende.Model.Core
{
    /// <summary>
    /// Dyn365TableAttribute
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.All)]
    public class Dyn365NameAttribute : System.Attribute
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public Dyn365NameAttribute(string Name)
        {
            this.Name = Name;
        }

        /// <summary>
        /// Nome
        /// </summary>
        public string Name { get; set; }
    }
}
