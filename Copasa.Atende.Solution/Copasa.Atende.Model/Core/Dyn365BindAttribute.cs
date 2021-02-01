namespace Copasa.Atende.Model.Core
{
    /// <summary>
    /// Dyn365BindAttribute
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.All)]
    public class Dyn365BindAttribute : System.Attribute
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public Dyn365BindAttribute(string TableName,string BindName)
        {
            this.TableName = TableName;
            this.BindName = BindName;
        }

        /// <summary>
        /// TableName
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// BindName
        /// </summary>
        public string BindName { get; set; }
    }
}
