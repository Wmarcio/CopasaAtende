using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISCNReceiveMatriculas - Certidão negativa de débito
    /// </summary>
    public class SCN6ISCNReceiveMatriculas : BaseModelReceive
    {
        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_MATRICULA")]
        public string matricula { get; set; }

        /// <summary>
        /// TipoLogradouro.
        /// </summary>
        [XmlElement("_DESC-TIPO-LOGRADOURO")]
        public string tipoLogradouro { get; set; }

        /// <summary>
        /// NomeLogradouro.
        /// </summary>
        [XmlElement("_NOME-LOGRADOURO")]
        public string nomeLogradouro { get; set; }

        /// <summary>
        /// NumeroLogradouro.
        /// </summary>
        [XmlElement("_NUM-IMOVEL")]
        public string numeroLogradouro { get; set; }

        /// <summary>
        /// TipoComplementoLogradouro.
        /// </summary>
        [XmlElement("_TIPO-COMPLEMENTO-ENDERECO")]
        public string tipoComplementoLogradouro { get; set; }

        /// <summary>
        /// DescricaoComplementoLogradouro.
        /// </summary>
        [XmlElement("_DESC-COMPLEMENTO-ENDERECO")]
        public string descricaoComplementoLogradouro { get; set; }

        /// <summary>
        /// InformacaoComplementarLogradouro.
        /// </summary>
        [XmlElement("_INF-COMPLEMENTAR-ENDERECO")]
        public string informacaoComplementarLogradouro { get; set; }

        /// <summary>
        /// CEP.
        /// </summary>
        [XmlElement("_CEP")]
        public string CEP { get; set; }

        /// <summary>
        /// Bairro.
        /// </summary>
        [XmlElement("_NOME-BAIRRO")]
        public string bairro { get; set; }

        /// <summary>
        /// Localidade.
        /// </summary>
        [XmlElement("_NOME-LOCALIDADE")]
        public string localidade { get; set; }

        /// <summary>
        /// SiglaUF.
        /// </summary>
        [XmlElement("_SIGLA-UF")]
        public string siglaUF { get; set; }

        /// <summary>
        /// NumeroFatura.
        /// </summary>
        [XmlElement("_NUM-FATURA")]
        public string numeroFatura { get; set; }

        /// <summary>
        /// ValorTotalFatura.
        /// </summary>
        [XmlElement("_VALOR-TOTAL-FATURA")]
        public string valorTotalFatura { get; set; }

        /// <summary>
        /// DataVencimentoFatura.
        /// </summary>
        [XmlElement("_DATA-VENC-FATURA")]
        public string dataVencimentoFatura { get; set; }
    }
}
