namespace Copasa.Atende.Model.Core
{
    /// <summary>
    /// Dyn365Attribute
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.All)]
    public class Dyn365IdAttribute : System.Attribute
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public Dyn365IdAttribute(string Name)
        {
            this.Name = Name;
        }

        /// <summary>
        /// Nome
        /// </summary>
        public string Name { get; set; }
    }
}
