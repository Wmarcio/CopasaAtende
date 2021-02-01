using Copasa.Atende.Business.Interfaces;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Model.Digital;
using Copasa.Atende.Model.Dyn365;
using Copasa.Atende.Repository.Repositories.Digital;
using Copasa.Atende.Repository.Repositories.Dyn365;
using Copasa.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Business
{
    /// <summary>
    /// 
    /// </summary>
   public class MigraBanco
    {
        private readonly IDClienteRule _clienteRule;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clienteRule"></param>
        public MigraBanco(IDClienteRule clienteRule)
        {
            _clienteRule = clienteRule;
        }

        /// <summary>
        /// Função de migração
        /// </summary>
        /// <returns></returns>
        public void LoopDigital()
        {
            string validado = "N";
            using (var unitOfWork = RepositoryFactory.UnitOfWork)
            {
                IEnumerable<ClienteModel> clientes = unitOfWork.ClienteRepository.GetMany(x => x.Validado == validado & x.TipoCliente == "F").Take(2);

                int i = 0;

                foreach (ClienteModel cliente in clientes)
                {

                    if (Migracliente(cliente))
                    {
                        cliente.Validado = "S";
                        unitOfWork.ClienteRepository.Update(cliente);
                        unitOfWork.DCommit();
                        Console.WriteLine($"cpf: {cliente.CpfCnpj}");
                    }
                    i = i + 1;
                }
            }

            //var baseResponse = new BaseResponse();

            //var teste = CriptografarSHA(senhaUsuario);
            //try
            //{
            //string teste = "N";

            //long testeb = 0;
            //Validado.Equals(false)

            //todos os clientes com validado igual a n
            //var clientes = RepositoryFactory.UnitOfWork.ClienteRepository.GetMany(x => x.Validado == teste & x.TipoCliente == "F" && x.ImagemPerfil != null);


            //var imagem = System.Text.Encoding.UTF8.GetString(clientes.Select(x => x.ImagemPerfil).First());



            //cpf vem como um inteiro sem tratamento



            //montar um objeto cliente d365 para receber o obj digital

            //63039834649
            //var cliente = RepositoryFactory.UnitOfWork.ClienteRepository.GetMany(x => x.CpfCnpj > testeb);

            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }



        //variaveis usadas na estrutura de updates
        private StringBuilder log;

        private IEnumerable<TelefoneModel> listacelular, listatelefones;
        private string cpf, cpfformatado, numerocelular, telefonea, telefoneb, primeiro_nome, sobrenome, politicaprivacidade, termoaceite;
        private string[] nome;
        private DRepository dRepository;
        //
        /// <summary>
        /// 
        /// </summary>
        public bool Migracliente(ClienteModel cliente)
        {
            bool retorno = false;

            try
            {

                log = new StringBuilder();

                //ANTES DE TUDO, VERIFICAR SE JÁ EXISTE NO BANCO ATUAL, PELO CPF
                //transforma o cpf em string
                cpf = cliente.CpfCnpj.ToString().PadLeft(11, '0');
                log.Append("cpf: " + cpf);
                DValidaCpfCnpjSend dyn365ValidaCpfCnpjSend = new DValidaCpfCnpjSend()
                {
                    CpfCnpj = cpf
                };


                DValidaCpfCnpjReceive dyn365ValidaCpfCnpjReceive = (DValidaCpfCnpjReceive)_clienteRule.ValidaCpfCnpjDyn365(dyn365ValidaCpfCnpjSend).Model;

                //caso nao tenha, cadastrar
                if (string.IsNullOrEmpty(dyn365ValidaCpfCnpjReceive.CpfCnpjId))
                {

                    //SE NAO EXISTIR CONTINUAR O PROCESSO DE MIGRAÇÃO

                    //var cpfCnpj = Convert.ToInt64(63039834649);
                    //cliente = unitOfWork.ClienteRepository.Get(x => x.CpfCnpj.Equals(cpfCnpj));

                    //ter certeza de que as variaveis estão limpas e separadas, por isso, evitei declará-las durante a função.

                    listacelular = listatelefones = null;
                    nome = new string[3];
                    primeiro_nome = sobrenome = numerocelular = telefonea = telefoneb = cpfformatado = politicaprivacidade = termoaceite = "";

                    //formtata cpf
                    cpfformatado = $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}." +
                            $"{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";

                    //usuarios sem nome e email nao tem como cadastrar.
                    if (!string.IsNullOrEmpty(cliente.Nome))
                    {
                        //separar o nome do sobrenome se possivel
                        nome = cliente.Nome.Split(new char[] { ' ' }, 2);
                        primeiro_nome = cliente.Nome.Split(new char[] { ' ' }, 2)[0]; //.ToString().Split(' ');
                        sobrenome = "";
                        if (nome.Length > 1)
                        {
                            sobrenome = cliente.Nome.Split(new char[] { ' ' }, 2)[1];
                        }

                        //separar o telefone celular
                        //telefone celular
                        listacelular = cliente.Telefones.Where(x => x.IdTipoTelefone == (TipoTelefoneEnum.CELULAR));

                        if (listacelular.Count() > 0)
                            numerocelular = listacelular.Select(x => x.NumeroTelefone).First().ToString();

                        //caso tenha outros, cadastrar nos telefones a e b
                        listatelefones = cliente.Telefones.Where(x => x.IdTipoTelefone != (TipoTelefoneEnum.CELULAR));

                        if (listatelefones.Count() > 0) telefonea = listatelefones.Select(x => x.NumeroTelefone).First().ToString();
                        if (listatelefones.Count() > 1) telefoneb = listatelefones.Select(x => x.NumeroTelefone).Last().ToString();
                        string imagem = "";
                        //capturar a imagem e convertê-la para uma imagem base64 válida se possivel

                        if (cliente.ImagemPerfil != null)
                            imagem = cliente.ImagemPerfil.ByteArrayToBase64();

                        politicaprivacidade = (cliente.FlagPoliticaPrivacidade ? "1" : "0");
                        termoaceite = (cliente.FlagTermoAceite ? "1" : "0");

                        //objeto de inclusão
                        DCadastraUsuarioSend dCadastraUsuario = new DCadastraUsuarioSend()
                        {
                            Username = "servico.app@copasa.com.br",
                            Password = "CYiEjHlatF9W33G56MjcmrfEh1nRXazWw+sYuVM/QGc=",
                            CpfCnpj = cpfformatado,
                            PortalUsername = cpf,
                            PortaluserPasswordBtoa = "false",
                            PortalUserpassword = "Hh00u0sLyUNK8nnday23KXjnR2zMYknlSHKceorPJ6c=",
                            Firstname = primeiro_nome,
                            Lastname = sobrenome,
                            Phonetype = "176410000",
                            Mobilephone = numerocelular,
                            Telephone1 = telefonea,
                            Telephone2 = telefoneb,
                            DoNotEmail = "false",
                            EmailAddress1 = cliente.Email.ToString(),
                            CopasaTipoCliente = "176410000",
                            CopasaValidacaoEmail = "176410000",
                            EntityImage = imagem,
                            CopasaPoliticaPrivacidade = politicaprivacidade,
                            CopasaTermoAceite = termoaceite
                        };
                        dRepository = new DRepository("CopasaUser", "Dyn365HostAuthenticate");

                        BaseModelAzureCopaUserReceive CopaUserReceive = (BaseModelAzureCopaUserReceive)dRepository.DExecutarServico("CreateDyn365PortalUser", dCadastraUsuario, typeof(BaseModelAzureCopaUserReceive));
                        if (CopaUserReceive != null)
                        {
                            //caso de erro no cadastro
                            if (string.IsNullOrEmpty(CopaUserReceive.Id))
                            {
                                log.Append(" - não foi possivel cadastrar no d365");

                            }
                            else
                            {
                                log.Append(" - cadastrado no d365");
                                retorno = true;
                            }
                        }
                        else
                        {
                            log.Append(" - erro ao tentar cadastrar no d365");
                        }
                    }
                }
                else
                {
                    log.Append(" - já inserido");
                    retorno = true;
                }
            }
            catch (Exception ex)
            {
                log.Append("excessão no sistema: " + ex.Message);
            }

            geralog(log.ToString());
            return retorno;
        }


        //public bool MigraidentifAssc()
        //{

        //}


        /// <summary>
        /// gerar um log dos cpfs impostados
        /// </summary>
        private void geralog(string mensagem)
        {
            try
            {
                using (StreamWriter logger = new StreamWriter(@"C:\Temp\teste.log", true))
                {
                    logger.WriteLine(mensagem);
                    logger.WriteLine();
                }
            }
            catch (Exception ex)
            { throw ex; }

        }
    }
}
