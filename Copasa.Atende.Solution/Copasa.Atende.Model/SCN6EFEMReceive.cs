using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6EFEMReceive - Segunda via fatura
    /// </summary>
    public class SCN6EFEMReceive : BaseModelRetorno
    {
        /// <summary>
        /// dataEmissao.
        /// </summary>
        [Broker("dataEmissao", 1, "A10")]
        public string dataEmissao { get; set; }

        /// <summary>
        /// DataApresentacao.
        /// </summary>
        [Broker("dataApresentacao", 2, "A10")]
        public string dataApresentacao { get; set; }

        /// <summary>
        /// Localizador.
        /// </summary>
        [Broker("localizador", 3, "A36")]
        public string localizador { get; set; }

        /// <summary>
        /// Pagina.
        /// </summary>
        [Broker("pagina", 4, "A5")]
        public string pagina { get; set; }

        /// <summary>
        /// NomeCliente.
        /// </summary>
        [Broker("nomeCliente", 5, "A35")]
        public string nomeCliente { get; set; }

        /// <summary>
        /// TipoLogradouro.
        /// </summary>
        [Broker("tipoLogradouro", 6, "A2")]
        public string tipoLogradouro { get; set; }

        /// <summary>
        /// NomeLogradouro.
        /// </summary>
        [Broker("nomeLogradouro", 7, "A40")]
        public string nomeLogradouro { get; set; }

        /// <summary>
        /// NumeroLogradouro.
        /// </summary>
        [Broker("numeroLogradouro", 8, "A5")]
        public string numeroLogradouro { get; set; }

        /// <summary>
        /// TipoComplementoLogradouro.
        /// </summary>
        [Broker("tipoComplementoLogradouro", 9, "A2")]
        public string tipoComplementoLogradouro { get; set; }

        /// <summary>
        /// ComplementoLogradouro.
        /// </summary>
        [Broker("complementoLogradouro", 10, "A12")]
        public string complementoLogradouro { get; set; }

        /// <summary>
        /// cep.
        /// </summary>
        [Broker("cep", 11, "A15")]
        public string cep { get; set; }

        /// <summary>
        /// NomeBairro.
        /// </summary>
        [Broker("nomeBairro", 12, "A30")]
        public string nomeBairro { get; set; }

        /// <summary>
        /// NomeLocalidade.
        /// </summary>
        [Broker("nomeLocalidade", 13, "A30")]
        public string nomeLocalidade { get; set; }

        /// <summary>
        /// SiglaUf.
        /// </summary>
        [Broker("siglaUf", 14, "A2")]
        public string siglaUf { get; set; }

        /// <summary>
        /// Campos ainda não utilizados.
        /// </summary>
        [Broker("campos", 15, "A4244")]
        public string campos { get; set; }

        /// <summary>
        /// NumeroCodigoBarrasFormatado.
        /// </summary>
        [Broker("numeroCodigoBarrasFormatado", 16, "A56")]
        public string numeroCodigoBarrasFormatado { get; set; }

        /// <summary>
        /// numeroCodigoBarras.
        /// </summary>
        [Broker("numeroCodigoBarras", 17, "A116")]
        public string numeroCodigoBarras { get; set; }

        /// <summary>
        /// Resto.
        /// </summary>
        [Broker("resto", 18, "A199")]
        public string resto { get; set; }
    }
}
