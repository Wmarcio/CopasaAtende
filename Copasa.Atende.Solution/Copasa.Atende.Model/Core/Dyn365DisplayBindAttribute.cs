namespace Copasa.Atende.Model.Core
{
    /// <summary>
    /// Dyn365DisplayBindAttribute
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.All)]
    public class Dyn365DisplayBindAttribute : System.Attribute
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public Dyn365DisplayBindAttribute(string IdName)
        {
            this.IdName = IdName;
        }

        /// <summary>
        /// Nome
        /// </summary>
        public string IdName { get; set; }
    }
}
