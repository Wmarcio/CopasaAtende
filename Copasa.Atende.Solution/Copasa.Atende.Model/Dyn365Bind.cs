using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// Bind para relacionamento entre tabelas no Dynamics 365
    /// </summary>
    public class Dyn365Bind : BaseModel
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public Dyn365Bind(string TableName,string BindName)
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
