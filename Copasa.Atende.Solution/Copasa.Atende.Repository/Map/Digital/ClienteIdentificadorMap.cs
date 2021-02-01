using Copasa.Atende.Model.Digital;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Repository.Map.Digital
{
    /// <summary>
    /// 
    /// </summary>
    public class ClienteIdentificadorMap : EntityTypeConfiguration<ClienteIdentificadorModel>
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        public ClienteIdentificadorMap()
        {
            ToTable("APP_CLIENTE_IDENTIFICADOR");

            HasKey(x => new { x.Identificador, x.CpfCnpj });

            Property(x => x.Identificador)
                .HasColumnName("NU_IDENTIFICADOR")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            Property(x => x.CpfCnpj)
                .HasColumnName("NU_CLIENTE_CPF_CNPJ")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            Property(x => x.Ativo)
                .HasColumnName("FL_ATIVO")
                .IsRequired();

            Property(x => x.CodigoLocalidade)
               .HasColumnName("NU_LOCALIDADE")
               .IsRequired();

            HasRequired(x => x.Cliente)
                .WithMany(x => x.Identificadores)
                .HasForeignKey(x => x.CpfCnpj);

            Ignore(x => x.Id);
            Ignore(x => x.IsAtivo);
            Ignore(x => x.Descricao);
            Ignore(x => x.Endereco);
            Ignore(x => x.CpfCnpjClienteCopasa);
            Ignore(x => x.NomeClienteCopasa);
        }


    }
}
