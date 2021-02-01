namespace Copasa.Atende.WebService.Mapper
{
    using AutoMapper;
    using System;

    /// <summary>
    /// Classe ViewModelToModelMappingProfile.
    /// </summary>
    public class ViewModelToModelMappingProfile : Profile
    {
        /// <summary>
        /// Propriedade ProfileName.
        /// </summary>
        public override string ProfileName
        {
            get { return "ViewModelToModelMappings"; }
        }

        /// <summary>
        /// Configurar mapeamento ViewModel para Model.
        /// </summary>
        [Obsolete]
        protected override void Configure()
        {
        }
    }
}