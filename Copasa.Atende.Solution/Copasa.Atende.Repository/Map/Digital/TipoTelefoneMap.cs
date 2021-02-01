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
    public class TipoTelefoneMap : EntityTypeConfiguration<TipoTelefoneModel>
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        public TipoTelefoneMap()
        {
            ToTable("APP_TP_TELEFONE");

            HasKey(x => x.IdTipoTelefone)
                .Property(x => x.IdTipoTelefone)
                .HasColumnName("ID_TP_TELEFONE")
                .IsRequired();

            Property(x => x.DescricaoTipoTelefone)
                .HasColumnName("DS_TP_TELEFONE")
                .HasMaxLength(25)
                .IsRequired();

            //Ignore(x => x.IsAtivo);
            //Ignore(x => x.Descricao);
            //Ignore(x => x.Id);
        }
    }
}
