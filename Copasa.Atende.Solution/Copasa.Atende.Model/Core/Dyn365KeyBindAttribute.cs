using System;

namespace Copasa.Atende.Model.Core
{

    /// <summary>
    /// Dyn365KeyBindAttribute
    /// </summary>
    [AttributeUsage(System.AttributeTargets.All)]
    public class Dyn365KeyBindAttribute : System.Attribute
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public Dyn365KeyBindAttribute(string KeyName, string ValueName, string FieldBindName)
        {
            this.KeyName = KeyName;
            this.ValueName = ValueName;
            this.FieldBindName = FieldBindName;
            this.TableName = null;
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public Dyn365KeyBindAttribute(string KeyName, string ValueName, string FieldBindName, string TableName)
        {
            this.KeyName = KeyName;
            this.ValueName = ValueName;
            this.FieldBindName = FieldBindName;
            this.TableName = TableName;
        }

        /// <summary>
        /// KeyName
        /// </summary>
        public string KeyName { get; set; }

        /// <summary>
        /// ValueName
        /// </summary>
        public string ValueName { get; set; }

        /// <summary>
        /// FieldBindName
        /// </summary>
        public string FieldBindName { get; set; }

        /// <summary>
        /// KeyName
        /// </summary>
        public string TableName { get; set; }

    }
}
