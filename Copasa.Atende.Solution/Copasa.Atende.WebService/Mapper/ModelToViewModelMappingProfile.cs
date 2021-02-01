namespace Copasa.Atende.WebService.Mapper
{
    using AutoMapper;
    using Copasa.Atende.Model;
    using Copasa.Atende.Model.Digital;
    using Copasa.Atende.WebService.Models;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Classe ModelToViewModelMappingProfile.
    /// </summary>
    public class ModelToViewModelMappingProfile : Profile
    {
        /// <summary>
        /// Propriedade ProfileName.
        /// </summary>
        public override string ProfileName
        {
            get { return "ModelToViewModelMappings"; }
        }

        /// <summary>
        /// Configurar mapeamento Model para ViewModel.
        /// </summary>
        [Obsolete]
        protected override void Configure()
        {
            CreateMap<SCN6ISCNView, CertidaoNegativaViewModel>();
            CreateMap<SCN6ISCNViewIdentificador, CertidaoNegativaIdentificadorViewModel>();
            CreateMap<SCN6ISCNViewEnderecoIdentificador, CertidaoNegativaEnderecoViewModel>();
            CreateMap<SCN6ISCNViewDebito, CertidaoNegativaDebitoViewModel>();
            CreateMap<SCN6ISCNReceiveLancamento, CertidaoNegativaLancamentoViewModel>();
            CreateMap<SCN6ISCNReceiveParcelamento, CertidaoNegativaParcelamentoViewModel>();
            CreateMap<SCN6ISCNReceiveFinanciamento, CertidaoNegativaFinanciamentoViewModel>();
        }
    }
}