namespace Copasa.Atende.WebService.Mapper
{
    /// <summary>
    /// Classe AutoMapperConfiguration.
    /// </summary>
    public class AutoMapperConfiguration
    {
        /// <summary>
        /// Configurar Automapper.
        /// </summary>
        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(x =>
            {
                x.AddProfile<ModelToViewModelMappingProfile>();
                x.AddProfile<ViewModelToModelMappingProfile>();
            });
        }
    }
}