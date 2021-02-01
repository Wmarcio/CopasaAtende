using System.Collections.Generic;

namespace Etc
{
    public class BrokerCliente
    {
        /*
        public void buscaCliente(Broker b,Modelos.Cliente c,bool atualiza)
        {
            b.addCampo("DESCRICAO_ERRO", 80,'A');
            b.addCampo("IDENTIFICADOR_USUARIO", 11, 'N');
            b.addCampo("NOME_USUARIO", 35,'A');
            b.addCampo("EMAIL", 50, 'A');
            b.addArrayCampo("MATRICULA_IMOVEL", 11, 'N',20);
            b.addArrayCampo("TIPO_LOGRADOURO", 2, 'A', 20);
            b.addArrayCampo("NOME_LOGRADOURO", 40, 'A', 20);
            b.addArrayCampo("NUM_IMOVEL", 5, 'N', 20);
            b.addArrayCampo("TIPO_COMPLEMENTO", 2, 'A',20);
            b.addArrayCampo("INF_COMPLEMENTO", 12, 'A', 20);
            b.addArrayCampo("NOME_BAIRRO", 30, 'A', 20);
            b.addArrayCampo("NOME_LOCALIDADE", 30, 'A',20);
            b.addArrayCampo("FLAG_CONTA_EMAIL", 1, 'A',20);
            b.addCampo("DDD_TELEFONE_CEL", 2, 'N');
            b.addCampo("TELEFONE_CEL", 9, 'N');
            b.addCampo("DDD_TELEFONE_RES", 2, 'N');
            b.addCampo("TELEFONE_RES", 9, 'N');
            b.addCampo("DDD_TELEFONE_COM", 2, 'N');
            b.addCampo("TELEFONE_COM", 9, 'N');

            b.RequisitaDadosBroker();

            if (atualiza)
            {
                c.identificadorusuario = b.getValor("IDENTIFICADOR_USUARIO");
                c.nomeusuario = b.getValor("NOME_USUARIO");
                c.emailusuario = b.getValor("EMAIL");
                c.dddtelefonecel = b.getValorAsInt("DDD_TELEFONE_CEL");
                c.dddtelefonecel = b.getValorAsInt("TELEFONE_CEL");
                c.dddtelefonecel = b.getValorAsInt("DDD_TELEFONE_RES");
                c.dddtelefonecel = b.getValorAsInt("TELEFONE_RES");
                c.dddtelefonecel = b.getValorAsInt("DDD_TELEFONE_COM");
                c.dddtelefonecel = b.getValorAsInt("TELEFONE_COM");

                for (int i = 1; i <= 20; i++)
                {
                    if (!"".Equals(b.getValor("MATRICULA_IMOVEL_" + i).Trim()))
                    {
                        Imovel im = new Imovel();
                        im.matriculaimovel = b.getValor("MATRICULA_IMOVEL_" + i);
                        im.tipologradouro = b.getValor("TIPO_LOGRADOURO_" + i);
                        im.nomelogradouro = b.getValor("NOME_LOGRADOURO_" + i);
                        im.numimovel = b.getValor("NUM_IMOVEL_" + i);
                        im.tipocomplemento = b.getValor("TIPO_COMPLEMENTO_" + i);
                        im.infcomplemento = b.getValor("INF_COMPLEMENTO_" + i);
                        im.nomebairro = b.getValor("NOME_BAIRRO_" + i);
                        im.nomelocalidade = b.getValor("NOME_LOCALIDADE_" + i);
                        im.enviarPorEmail = b.getValor("FLAG_CONTA_EMAIL_" + i);
                        c.imoveis.Add(im);
                    }
                }
            }
        }
        */
        private List<string> carregaList(string nomeCampo, Broker b, Modelos.Cliente c)
        {
            List<string> l = new List<string>();
            for (int i = 1; i <= 20; i++)
            {
                if (!"".Equals(b.getValor(nomeCampo+"_"+i).Trim()))
                {
                    l.Add(b.getValor(nomeCampo + "_" + i));
                }
            }
            return l;
        }
    }
}
