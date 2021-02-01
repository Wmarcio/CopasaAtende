namespace Copasa.Atende.Model
{
    /// <summary>
    /// BrokerAttribute
    /// </summary>
    public class ISAttribute : System.Attribute
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public ISAttribute(string Tipo,int Tamanho)
        {
            this.Tipo = Tipo;
            this.Tamanho = Tamanho;
        }
        /// <summary>
        /// Tipo
        /// </summary>
        public string Tipo { get; set; }

        /// <summary>
        /// Tamanho
        /// </summary>
        public int Tamanho { get; set; }
    }
}
