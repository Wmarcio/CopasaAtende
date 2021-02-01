namespace Copasa.Atende.Model.Core
{
    /// <summary>
    /// BrokerAttribute
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.All)]
    public class BrokerAttribute : System.Attribute
    {
        /// <summary>
        /// Construtor BrokerAttribute
        /// </summary>
        public BrokerAttribute(string Nome, int Ordem, string Tipo)
        {
            this.Nome = Nome;
            this.Tipo = Tipo;
            this.Ordem = Ordem;
            this.Ocorrencia = 1;
        }

        /// <summary>
        /// Construtor BrokerAttribute
        /// </summary>
        public BrokerAttribute(string Nome, int Ordem, string Tipo,int Ocorrencia)
        {
            this.Nome = Nome;
            this.Tipo = Tipo;
            this.Ordem = Ordem;
            this.Ocorrencia = Ocorrencia;
        }

        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Tamanho
        /// </summary>
        public string Tipo { get; set; }

        /// <summary>
        /// Ordem
        /// </summary>
        public int Ordem { get; set; }

        /// <summary>
        /// Quantidade ocorrências
        /// </summary>
        public int Ocorrencia { get; set; }

    }
}
