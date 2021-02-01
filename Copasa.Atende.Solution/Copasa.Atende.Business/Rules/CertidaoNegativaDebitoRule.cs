using Copasa.Atende.Business.Core;
using Copasa.Atende.Business.Interfaces;
using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;
using System;
using System.Linq;

namespace Copasa.Atende.Business.Rules
{
    /// <summary>
    /// Rule - CertidaoNegativaDebito.
    /// </summary>
    public class CertidaoNegativaDebitoRule : BaseRule, ICertidaoNegativaDebitoRule
    {

        private IISSCN6ISCNRepository _iISSCN6ISCNRepository;
        private IMensagemRepository _mensagemRepository;
        private ILog _log;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="iISSCN6ISCNRepository"></param>
        /// <param name="mensagemRepository"></param>
        /// <param name="log">ILog.</param>
        public CertidaoNegativaDebitoRule(IISSCN6ISCNRepository iISSCN6ISCNRepository, IMensagemRepository mensagemRepository, ILog log)
        {
            _iISSCN6ISCNRepository = iISSCN6ISCNRepository;
            _mensagemRepository = mensagemRepository;
            _log = log;
        }
        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// Certidão negativa de débito de um CPF ou CNPJ.
        /// </summary>
        /// <param name="sCN6ISCNSend"></param>
        /// <returns></returns>
        public BaseResponse SCN6ISCN(SCN6ISCNSend sCN6ISCNSend)
        {
            sCN6ISCNSend.cpfCnpj = Util.Util.retiraCaracteresMascara(sCN6ISCNSend.cpfCnpj);
            SCN6ISCNReceive sCN6ISCNReceive = (SCN6ISCNReceive)_iISSCN6ISCNRepository.Connect(sCN6ISCNSend);

            SCN6ISCNView certidaoFormatada = FormatarCertidaoNegativaDebito(sCN6ISCNReceive, sCN6ISCNSend.cpfCnpj);
            BaseResponse retorno = new BaseResponse();
            retorno.Model = certidaoFormatada;
            return retorno;
        }

        private SCN6ISCNView FormatarCertidaoNegativaDebito(SCN6ISCNReceive certidaoSicom, string CPFCNPJ)
        {
            SCN6ISCNView certidaoFormatada = new SCN6ISCNView();
            SCN6ISCNViewIdentificador identificador = new SCN6ISCNViewIdentificador();
            identificador.IdentificadorCliente = certidaoSicom.identificador;
            if (certidaoSicom.codigoRetornoSicom != "0")
            {
                if ("36".Equals(certidaoSicom.codigoRetornoSicom))
                {
                    string mensagem = "Solicitação de serviço não pode ser gerada.Favor comparecer em uma agência de atendimento munido de documentos pessoais e que comprovem o vínculo com o imóvel.";
                    certidaoFormatada.mensagemRetorno = _mensagemRepository.trataMensagem(mensagem, certidaoSicom);
                }
                else
                {
                    certidaoFormatada.mensagemRetorno = _mensagemRepository.geraMensagem("M" + certidaoSicom.codigoRetornoSicom.PadLeft(4, '0'));
                }
            }
            else
            {
                var temDebito = false;
                var temDebitoAVencer = false;
                if (certidaoSicom.matriculas.Count > 0)
                {
                    var matriculasOrdenadas = certidaoSicom.matriculas.OrderBy(x => x.matricula).ToList();
                    //var matriculasOrdenadas = certidaoSicom.matriculas.ToList();
                    string matriculaAnt = "";
                    SCN6ISCNViewEnderecoIdentificador enderecoIdentificador = null;
                    foreach (var matricula in matriculasOrdenadas)
                    {
                        if (!matriculaAnt.Equals(matricula.matricula))
                        {
                            matriculaAnt = matricula.matricula;
                            string localidade = (certidaoSicom.matriculas.ToList())[0].localidade;
                            if (enderecoIdentificador != null)
                                identificador.Enderecos.Add(enderecoIdentificador);
                            enderecoIdentificador = new SCN6ISCNViewEnderecoIdentificador();
                            enderecoIdentificador.MatriculaCliente = matricula.matricula;
                            enderecoIdentificador.descricaoTipoLogradouro =
                                $"{matricula.tipoLogradouro}. {matricula.nomeLogradouro}, {matricula.numeroLogradouro}, {matricula.bairro}, {matricula.CEP}, {localidade}, {matricula.siglaUF}";
                        }

                        SCN6ISCNViewDebito debito = new SCN6ISCNViewDebito();
                        debito.dataVencimentoFatura = matricula.dataVencimentoFatura;
                        debito.numeroFatura = long.Parse(matricula.numeroFatura);
                        debito.valorTotalfatura = matricula.valorTotalFatura;
                        if (matricula.dataVencimentoFatura != null && !"".Equals(matricula.dataVencimentoFatura))
                        {
                            if (Convert.ToDateTime(matricula.dataVencimentoFatura) >= DateTime.Now.AddDays(-1))
                            {
                                enderecoIdentificador.DebitosVencer.Add(debito);
                                temDebitoAVencer = true;
                            }
                            else
                            {
                                enderecoIdentificador.Debitos.Add(debito);
                                temDebito = true;
                            }
                        }
                        foreach (var lancamento in certidaoSicom.lancamentos.ToList())
                        {
                            if (lancamento.matricula.Equals(matricula.matricula))
                            {
                                enderecoIdentificador.Lancamentos = certidaoSicom.lancamentos;
                            }
                        }

                        foreach (var parcelamento in certidaoSicom.parcelamento.ToList())
                        {
                            if (parcelamento.matricula.Equals(matricula.matricula))
                            {
                                enderecoIdentificador.Parcelamentos = certidaoSicom.parcelamento;
                            }
                        }
                    }
                    if (enderecoIdentificador != null)
                        identificador.Enderecos.Add(enderecoIdentificador);
                }

                if (!temDebito)
                {
                    certidaoFormatada.textoCorpo = string.Format(@"DECLARAMOS, QUE A(S) MATRÍCULA(S) RELACIONADA(S) ABAIXO, CADASTRADA(S) EM NOME DO USUÁRIO {0}, CPF/CNPJ {1}, NÃO APRESENTA, NA PRESENTE DATA, DÉBITO(S) VENCIDO(S) COM A COPASA-MG.", certidaoSicom.nome, CPFCNPJ);

                    certidaoFormatada.textoRodape = "ESTA CERTIDÃO NÃO CONTEMPLA EVENTUAIS DÉBITOS POSTERIORMENTE APURADOS EM FUNÇÃO DE" +
                    " IDENTIFICAÇÃO DE IRREGULARIDADE(S) OU DE REVISÃO DE FATURAMENTO.";

                    certidaoFormatada.deferimento = "PEDIDO DEFERIDO";

                    certidaoFormatada.local = string.Format("Belo Horizonte, {0:dd} de {0:MMMM} de {0:yyyy}", DateTime.Now);
                }
                else
                {
                    certidaoFormatada.textoCorpo = string.Format(@"DECLARAMOS, QUE A(S) MATRÍCULA(S) RELACIONADA(S) ABAIXO, CADASTRADA(S) EM NOME DO USUÁRIO {0}, CPF/CNPJ {1}, APRESENTA, NA PRESENTE DATA, DÉBITO(S) VENCIDO(S) COM A COPASA-MG.", certidaoSicom.nome, CPFCNPJ);

                    certidaoFormatada.textoComplementar1 = $@"OS VALORES AQUI APRESENTADOS, JÁ VENCIDOS, SERÃO ACRESCIDOS DE MULTA, JUROS DE MORA E CORREÇÃO MONETÁRIA.";

                    certidaoFormatada.textoComplementar2 = $@"CASO O(S) DÉBITO(S) ACIMA MENCIONADO(S) JÁ TENHA(M) SIDO QUITADO(S), FINEZA PROCURAR UMA AGÊNCIA DE ATENDIMENTO E APRESENTAR A(S) FATURA(S) PAGA(S), PARA EMISSÃO DA CERTIDÃO NEGATIVA DE DÉBITO.";

                    certidaoFormatada.deferimento = "PEDIDO INDEFERIDO";

                    certidaoFormatada.textoRodape = "ESTA CERTIDÃO NÃO CONTEMPLA EVENTUAIS DÉBITOS POSTERIORMENTE APURADOS EM FUNÇÃO DE" +
                    " IDENTIFICAÇÃO DE IRREGULARIDADE(S) OU DE REVISÃO DE FATURAMENTO.";
                    certidaoFormatada.local = string.Format("{0}, {1:dd} de {1:MMMM} de {1:yyyy}", certidaoSicom.matriculas.Select(x => x.localidade).FirstOrDefault(), DateTime.Now);
                }

                if (temDebito)
                {
                    certidaoFormatada.textoDebitos = "FATURA(S) VENCIDAS";
                }

                if (temDebitoAVencer)
                {
                    certidaoFormatada.textoDebitosVencer = "FATURA(S) A VENCER";
                }

                if (certidaoSicom.parcelamento.Count > 0)
                {
                    certidaoFormatada.textoParcelamentos = "PARCELAMENTO: \n";
                }

                if (certidaoSicom.lancamentos.Count > 0)
                {
                    certidaoFormatada.textoLancamentos = "LANÇAMENTOS A FATURAR: \n";
                }
                certidaoFormatada.identificadores.Add(identificador);
            }
            return certidaoFormatada;
        }
    }
}
