using Copasa.Atende.Model.Digital;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Copasa.Atende.Repository.Map.Digital
{
    /// <summary>
    /// Classe de mapeamento para a entidade APP_PARAMETRO_ATENDE.
    /// </summary>
    public class ParametroMap : EntityTypeConfiguration<ParametroModel>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public ParametroMap()
        {
            ToTable("APP_PARAMETRO_ATENDE");

            HasKey(x => x.IdParametro)
                .Property(x => x.IdParametro)
                .HasColumnName("ID_PARAMETRO")
                .IsRequired();

            Property(x => x.DataInicio)
                .HasColumnName("DT_INICIO")
                .IsRequired();

            Property(x => x.DataFim)
               .HasColumnName("DT_FIM")
               .IsRequired();

            Property(x => x.DescricaoMensagem)
               .HasColumnName("DS_MENSAGEM")
               .IsRequired();

            Property(x => x.FlagPermissao)
              .HasColumnName("FL_PERMISSAO")
              .IsOptional();

            Property(x => x.Versao)
              .HasColumnName("NU_VERSAO")
              .IsOptional();

            Property(x => x.SistemaOperacional)
              .HasColumnName("SISTEMA_OPERACIONAL")
              .IsOptional();
        }
    }
}
