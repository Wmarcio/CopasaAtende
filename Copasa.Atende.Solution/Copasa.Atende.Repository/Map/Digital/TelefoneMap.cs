using Copasa.Atende.Model.Digital;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Repository.Map.Digital
{
    /// <summary>
    /// 
    /// </summary>
    public class TelefoneMap : EntityTypeConfiguration<TelefoneModel>
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        public TelefoneMap()
        {
            ToTable("APP_TELEFONE");

            HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasColumnName("ID_TELEFONE")
                .IsRequired();

            Property(x => x.IdTipoTelefone)
                .HasColumnName("ID_TP_TELEFONE")
                .IsRequired();

            Property(x => x.CpfCnpj)
              .HasColumnName("NU_CLIENTE_CPF_CNPJ")
              .IsRequired();

            Property(x => x.NumeroTelefone)
              .HasColumnName("NU_TELEFONE")
              .IsRequired();

            Property(x => x.IdComunicacao)
                .HasColumnName("ID_COMUNICACAO");

            HasRequired(x => x.Cliente)
                .WithMany(x => x.Telefones)
                .HasForeignKey(x => x.CpfCnpj);

            Ignore(x => x.Descricao);
            Ignore(x => x.IsAtivo);
        }
    }
}
