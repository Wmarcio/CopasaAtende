using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISRE Receive
    /// </summary>
    public class SCN4ISRESend : BaseModelSend
    {
        /// <summary>
        /// Origem
        /// </summary>
        [XmlElement("_RCV-FLAG-ORIGEM")]
        public string origem { get; set; }

        /// <summary>
        /// Identificador origem
        /// </summary>
        [XmlElement("_RCV-IDENT-ORIGEM")]
        public string identificadorOrigem { get; set; }

        /// <summary>
        /// Número do Protocolo no CRM
        /// </summary>
        [XmlElement("_RCV-NUM-PROTOCOLO-CRM")]
        public string numeroProtocoloCrm { get; set; }

        /// <summary>
        /// Número de matrícula do imóvel
        /// </summary>
        [XmlElement("_RCV-NUM-MATRICULA-IMOVEL")]
        public string numeroMatriculaImovel { get; set; }

        /// <summary>
        /// Código do serviço
        /// </summary>
        [XmlElement("_RCV-COD-SERVICO")]
        public string codigoServico { get; set; }

        /// <summary>
        /// Valor da religação
        /// </summary>
        [XmlElement("_RCV-VALOR-RELIGACAO-N")]
        public string valorReligacao { get; set; }

        /// <summary>
        /// Nome do solicitante
        /// </summary>
        [XmlElement("_RCV-NOME-SOLICITANTE")]
        public string nomeSolicitante { get; set; }

        /// <summary>
        /// Unidade do solicitante
        /// </summary>
        [XmlElement("_RCV-UNID-SOLICITANTE")]
        public string unidadeSolicitante { get; set; }

        /// <summary>
        /// Referência do endereço
        /// </summary>
        [XmlElement("_RCV-REFERENCIA-ENDERECO")]
        public string referenciaEndereco { get; set; }

        /// <summary>
        /// Serviço da fatura avulsa
        /// </summary>
        [XmlElement("_RCV-FATURA-AVULSA-SERV")]
        public string faturaAvulsaServico { get; set; }

        /// <summary>
        /// Flag Parcelamento
        /// </summary>
        [XmlElement("_RCV-FLAG-PARCELAMENTO")]
        public string flagParcelamento { get; set; }

        /// <summary>
        /// Quantidade de parcelas
        /// </summary>
        [XmlElement("_RCV-QTDE-PARCELAS")]
        public string quantidadeParcelas { get; set; }

        /// <summary>
        /// Lista de parcelamento
        /// </summary>
        [XmlElement("_RCV-PARCELAMENTO")]
        public SCN4ISRESendParcelamento[] parcelamento { get; set; }

        /// <summary>
        /// Débitos em aberto
        /// </summary>
        [XmlElement("_RCV-DEBITOS-EM-ABERTO")]
        public string debitoEmAberto { get; set; }

        /// <summary>
        /// Lista de Contas
        /// </summary>
        [XmlElement("_RCV-CONTAS")]
        public SCN4ISRESendContas[] contas { get; set; }

        /// <summary>
        /// Descrição do erro
        /// </summary>
        [XmlElement("_SND-DESC-ERRO")]
        public string descricaoErro { get; set; }
    }
}
