using Copasa.Atende.Model.Core;

namespace Copasa.Atende.WebService.Models
{
    /// <summary>
    /// Classe referente à quitação anual de débito
    /// </summary>
    public class QuitacaoAnualDebitoViewModel : BaseModel
    {
        /// <summary>
        /// CodigoRetornoSicom.
        /// </summary>
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// DescricaoRetornoSicom.
        /// </summary>
        public string descricaoRetornoSicom { get; set; }

        /// <summary>
        /// PossuiFaturaEmDebito.
        /// </summary>
        public string possuiFaturaEmDebito { get; set; }

        /// <summary>
        /// CodigoLogradouro.
        /// </summary>
        public string codigoLogradouro { get; set; }

        /// <summary>
        /// TipoLogradouro.
        /// </summary>
        public string tipoLogradouro { get; set; }

        /// <summary>
        /// NomeLogradouro.
        /// </summary>
        public string nomeLogradouro { get; set; }

        /// <summary>
        /// NumeroImovel.
        /// </summary>
        public string numeroImovel { get; set; }

        /// <summary>
        /// TipoComplemento.
        /// </summary>
        public string tipoComplementoImovel { get; set; }

        /// <summary>
        /// ComplementoImovel.
        /// </summary>
        public string complementoImovel { get; set; }

        /// <summary>
        /// CodigoBairro.
        /// </summary>
        public string codigoBairro { get; set; }

        /// <summary>
        /// NomeBairro.
        /// </summary>
        public string nomeBairro { get; set; }

        /// <summary>
        /// CodigoLocalidade.
        /// </summary>
        public string codigoLocalidade { get; set; }

        /// <summary>
        /// NomeLocalidade.
        /// </summary>
        public string nomeLocalidade { get; set; }

        /// <summary>
        /// CepImovel.
        /// </summary>
        public string cepImovel { get; set; }
        /// <summary>
        /// DescricaoRetornoSicom.
        /// </summary>
        public QuitacaoAnualDebitoFaturaEmAbertoViewModel[] faturasEmDebito { get; set; }

        /// <summary>
        /// Texto Corpo.
        /// </summary>
        public string textoCorpo { get; set; }

        /// <summary>
        /// Texto Consideracoes.
        /// </summary>
        public string textoConsideracoes { get; set; }

        /// <summary>
        /// Texto Debito.
        /// </summary>
        public string textoDebito { get; set; }

        /// <summary>
        /// Local.
        /// </summary>
        public string local { get; set; }

        /// <summary>
        /// Deferido ou indeferido
        /// </summary>
        public string deferimento { get; set; }
    }
}