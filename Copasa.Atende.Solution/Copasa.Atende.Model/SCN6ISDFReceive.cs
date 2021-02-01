using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISDFReceive Detalhe fatura
    /// </summary>
    public class SCN6ISDFReceive : BaseModelReceive
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public SCN6ISDFReceive()
        {
            hidrometros = new List<SCN6ISDFReceiveHidrometro>();
            categorias = new List<string>();
        }

        /// <summary>
        /// CodigoRetornoSicom.
        /// </summary>
        [XmlElement("_NUM-ERRO")]
        [JsonIgnore]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// Nome.
        /// </summary>
        [XmlElement("_BRK-NOME-CLIENTE")]
        public string nome { get; set; }

        /// <summary>
        /// Referencia.
        /// </summary>
        [XmlElement("_BRK-ANO-MES-REF-SAIDA")]
        public string referencia { get; set; }

        /// <summary>
        /// NumeroFatura.
        /// </summary>
        [XmlElement("_BRK-NUM-FATURA")]
        public string numeroFatura { get; set; }

        /// <summary>
        /// DataVencimento.
        /// </summary>
        [XmlElement("_BRK-DT-VENCIMENTO")]
        public string dataVencimento { get; set; }

        /// <summary>
        /// Valor.
        /// </summary>
        [XmlElement("_BRK-VL-FATURADO")]
        public string valor { get; set; }

        /// <summary>
        /// ConsumoLitros.
        /// </summary>
        [XmlElement("_BRK-LITRO-FAT")]
        public string consumoLitros { get; set; }

        /// <summary>
        /// CategoriasSicom.
        /// </summary>
        [XmlElement("_BRK-CATEGORIA")]
        [JsonIgnore]
        public string[] categoriasSicom { get; set; }

        /// <summary>
        /// Categorias.
        /// </summary>
        public List<string> categorias { get; set; }

        /// <summary>
        /// UnidadeConsumo.
        /// </summary>
        [XmlElement("_BRK-UNID-CONS-LITROS")]
        public string unidadeConsumo { get; set; }

        /// <summary>
        /// Data do pagamento
        /// </summary>
        [XmlElement("_BRK-DATA-PAGAMENTO")]
        public string dataPagamento { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// Código do banco de pagamento
        /// </summary>
        [XmlElement("_BRK-COD-BCO-PAGAMENTO")]
        public string codigoBancoPagamento { get; set; }

        /// <summary>
        /// Nome do banco do pagamento
        /// </summary>
        [XmlElement("_BRK-NOME-BCO-PAGAMENTO")]
        public string nomeBancoPagamento { get; set; }

        /// <summary>
        /// Agência de pagamento
        /// </summary>
        [XmlElement("_BRK-AGENCIA-PAGAMENTO")]
        public string agenciaPagamento { get; set; }

        /// <summary>
        /// Valor pago da fatura
        /// </summary>
        public string valorPago { get; set; }

        /// <summary>
        /// HidrometrosSicom.
        /// </summary>
        [XmlElement("_TAB-HIDROMETRO")]
        [JsonIgnore]
        public SCN6ISDFReceiveHidrometro[] hidrometrosSicom { get; set; }

        /// <summary>
        /// Hidrometros.
        /// </summary>
        public List<SCN6ISDFReceiveHidrometro> hidrometros { get; set; }

        /// <summary>
        /// ValorAgua.
        /// </summary>
        [XmlElement("_BRK-VL-FAT-AGUA")]
        public string valorAgua { get; set; }

        /// <summary>
        /// ValorEsgoto.
        /// </summary>
        [XmlElement("_BRK-VL-FAT-ESGOTO")]
        public string valorEsgoto { get; set; }

        /// <summary>
        /// ValorFaturado.
        /// </summary>
        [XmlElement("_BRK-VL-FAT-DIVERSOS")]
        public string valorFaturado { get; set; }

        /// <summary>
        /// ValorDesconto.
        /// </summary>
        [XmlElement("_BRK-VL-FAT-DESCONTO")]
        public string valorDesconto { get; set; }

        /// <summary>
        /// DebitoAutomatico.
        /// </summary>
        [XmlElement("_BRK-FL-DEB-AUT")]
        public string debitoAutomatico { get; set; }

        /// <summary>
        /// DebitoAutomaticoBanco.
        /// </summary>
        [XmlElement("_BRK-COD-BANCO-DEB-AUT")]
        public string debitoAutomaticoBanco { get; set; }

        /// <summary>
        /// Nome do banco de débito
        /// </summary>
        [XmlElement("_BRK-NOME-BANCO-DEB-AUT")]
        public string nomeBancoDebito { get; set; }

        /// <summary>
        /// DebitoAutomaticoAgencia.
        /// </summary>
        [XmlElement("_BRK-VL-AGENCIA-DEB-AUT")]
        public string debitoAutomaticoAgencia { get; set; }

        /// <summary>
        /// DebitoAutomaticoConta.
        /// </summary>
        [XmlElement("_BRK-VL-CONTA-DEB-AUT")]
        public string debitoAutomaticoConta { get; set; }

        /// <summary>
        /// DescricaoFatura.
        /// </summary>
        [XmlElement("_BRK-DESCRICAO-FATURA")]
        public string descricaoFatura { get; set; }

    }
}
