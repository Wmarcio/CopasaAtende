using Copasa.Atende.Business.Interfaces;
using Copasa.Atende.Business.Rules;
using Copasa.Atende.Facade.Facades;
using Copasa.Atende.Facade.Interfaces;
using Copasa.Atende.Repository;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Repository.Interfaces.Digital;
using Copasa.Atende.Repository.Repositories;
using Copasa.Atende.Util;
using Copasa.Digital.Repository.Interfaces;
using Copasa.Digital.Repository.Repositories;
using SimpleInjector;

namespace Copasa.Atende.Bootstrapper
{
    /// <summary>
    /// Container de configurações do simples injector. 
    /// Todos os contratos devem estar devidamente associados às suas implementações.
    /// </summary>
    public static class SimpleInjectorContainer
    {
        public static void Register(Container container)
        {
            container.Register<CopasaAtendeDataContext>(Lifestyle.Scoped);
            container.Register<IORAMensagemRepository, ORAMensagemRepository>(Lifestyle.Scoped);
            container.Register<IMensagemRepository, MensagemRepository>(Lifestyle.Scoped);
            container.Register<ILog, Log>(Lifestyle.Scoped);

            // Banco de Dados Oracle - Copasa Digital
            container.Register<ITelaRule, TelaRule>(Lifestyle.Scoped);
            container.Register<ITelaFacade, TelaFacade>(Lifestyle.Scoped);



            container.Register<IISSCN6ISFPRepository, ISSCN6ISFPRepository>(Lifestyle.Scoped);
            container.Register<IISSCN6ISFDRepository, ISSCN6ISFDRepository>(Lifestyle.Scoped);
            container.Register<IISSCN6ISDFRepository, ISSCN6ISDFRepository>(Lifestyle.Scoped);
            container.Register<IISSCN6ISSVRepository, ISSCN6ISSVRepository>(Lifestyle.Scoped);
            container.Register<IISSCN6ISGPRepository, ISSCN6ISGPRepository>(Lifestyle.Scoped);
            container.Register<IISSCN6ISPDRepository, ISSCN6ISPDRepository>(Lifestyle.Scoped);
            container.Register<IISSCN6ISCBRepository, ISSCN6ISCBRepository>(Lifestyle.Scoped);
            container.Register<IISSCN6ISNFRepository, ISSCN6ISNFRepository>(Lifestyle.Scoped);
            container.Register<IISSCN6ISTPRepository, ISSCN6ISTPRepository>(Lifestyle.Scoped);
            container.Register<IISSCN6ISCFRepository, ISSCN6ISCFRepository>(Lifestyle.Scoped);
            container.Register<IBrokerSCN6EFEMRepository, BrokerSCN6EFEMRepository>(Lifestyle.Scoped);
            container.Register<IFaturaRule, FaturaRule>(Lifestyle.Scoped);
            container.Register<IFaturaFacade, FaturaFacade>(Lifestyle.Scoped);
            container.Register<ICodigoBarrasRule, CodigoBarrasRule>(Lifestyle.Scoped);

            container.Register<IISSCN5IS01Repository, ISSCN5IS01Repository>(Lifestyle.Scoped);
            container.Register<IISSCN5IS02Repository, ISSCN5IS02Repository>(Lifestyle.Scoped);
            container.Register<IISSCN5IS03Repository, ISSCN5IS03Repository>(Lifestyle.Scoped);
            container.Register<IInformarLeituraRule, InformarLeituraRule>(Lifestyle.Scoped);
            container.Register<IInformarLeituraFacade, InformarLeituraFacade>(Lifestyle.Scoped);

            container.Register<IISSCN4ISFARepository, ISSCN4ISFARepository>(Lifestyle.Scoped);
            //container.Register<IEstouSemAguaRule, EstouSemAguaRule>(Lifestyle.Scoped);
            //container.Register<IEstouSemAguaFacade, EstouSemAguaFacade>(Lifestyle.Scoped);

            container.Register<IISSCN4ISVRRepository, ISSCN4ISVRRepository>(Lifestyle.Scoped);
            container.Register<IISSCN4ISVIRepository, ISSCN4ISVIRepository>(Lifestyle.Scoped);
            container.Register<IVazamentoRule, VazamentoRule>(Lifestyle.Scoped);
            container.Register<IVazamentoFacade, VazamentoFacade>(Lifestyle.Scoped);

            container.Register<IBrokerSCN6UEFIRepository, BrokerSCN6UEFIRepository>(Lifestyle.Scoped);
            container.Register<IBrokerSCN3PCLIRepository, BrokerSCN3PCLIRepository>(Lifestyle.Scoped);
            container.Register<IISSCN4ISU1Repository, ISSCN4ISU1Repository>(Lifestyle.Scoped);
            container.Register<IISSCN4ISU1_IdentificadoresRepository, ISSCN4ISU1_IdentificadoresRepository>(Lifestyle.Scoped);
            container.Register<IISSCN5ISHCRepository, ISSCN5ISHCRepository>(Lifestyle.Scoped);
            container.Register<IISSCN6ISCNRepository, ISSCN6ISCNRepository>(Lifestyle.Scoped);
            container.Register<IISSCN6ISCERepository, ISSCN6ISCERepository>(Lifestyle.Scoped);
            container.Register<IISSCN3ISPSRepository, ISSCN3ISPSRepository>(Lifestyle.Scoped);
            container.Register<IISSCN6ISAVRepository, ISSCN6ISAVRepository>(Lifestyle.Scoped);
            container.Register<IISSCN3ISMTRepository, ISSCN3ISMTRepository>(Lifestyle.Scoped);
            container.Register<IISSCN6ISCCRepository, ISSCN6ISCCRepository>(Lifestyle.Scoped);
            container.Register<IISSCN6ISQARepository, ISSCN6ISQARepository>(Lifestyle.Scoped);
            container.Register<IISSCN4ISACRepository, ISSCN4ISACRepository>(Lifestyle.Scoped);
            container.Register<IClienteRule, ClienteRule>(Lifestyle.Scoped);
            container.Register<IClienteFacade, ClienteFacade>(Lifestyle.Scoped);
            //Dynamics 365
            container.Register<IDClienteRule, DClienteRule>(Lifestyle.Scoped);
            container.Register<IDClienteFacade, DClienteFacade>(Lifestyle.Scoped);
            //
            container.Register<IORAEmpregadoRepository, ORAEmpregadoRepository>(Lifestyle.Scoped);
            container.Register<IORACargoRepository, ORACargoRepository>(Lifestyle.Scoped);
            container.Register<IORAUnidadeOrganizacionalRepository, ORAUnidadeOrganizacionalRepository>(Lifestyle.Scoped);
            container.Register<IISSCN4CRALRepository, ISSCN4CRALRepository>(Lifestyle.Scoped);
            container.Register<IISSCN4CRATRepository, ISSCN4CRATRepository>(Lifestyle.Scoped);
            container.Register<IISSCN4CRBXRepository, ISSCN4CRBXRepository>(Lifestyle.Scoped);
            container.Register<IISSCN4CRSSRepository, ISSCN4CRSSRepository>(Lifestyle.Scoped);
            container.Register<IISSCN4ISORRepository, ISSCN4ISORRepository>(Lifestyle.Scoped);
            container.Register<IISSCN4ISSSRepository, ISSCN4ISSSRepository>(Lifestyle.Scoped);
            container.Register<IISSCN4CASSRepository, ISSCN4CASSRepository>(Lifestyle.Scoped);
            container.Register<IISSCN4CREXRepository, ISSCN4CREXRepository>(Lifestyle.Scoped);
            container.Register<IServicoOperacionalRule, ServicoOperacionalRule>(Lifestyle.Scoped);
            container.Register<IServicoOperacionalFacade, ServicoOperacionalFacade>(Lifestyle.Scoped);

            container.Register<IISSCN4ISRLRepository, ISSCN4ISRLRepository>(Lifestyle.Scoped);
            container.Register<IISSCN4ISCPRepository, ISSCN4ISCPRepository>(Lifestyle.Scoped);
            container.Register<IISSCN4ISRERepository, ISSCN4ISRERepository>(Lifestyle.Scoped);
            container.Register<IReligacaoRule, ReligacaoRule>(Lifestyle.Scoped);
            container.Register<IReligacaoFacade, ReligacaoFacade>(Lifestyle.Scoped);
            container.Register<ICertidaoNegativaDebitoRule, CertidaoNegativaDebitoRule>(Lifestyle.Scoped);
            container.Register<ICertidaoNegativaDebitoFacade, CertidaoNegativaDebitoFacade>(Lifestyle.Scoped);

            container.Register<IISSCN6ISAARepository, ISSCN6ISAARepository>(Lifestyle.Scoped);
            container.Register<IISSCN4ISCSRepository, ISSCN4ISCSRepository>(Lifestyle.Scoped);
            container.Register<IISSCN4ISAERepository, ISSCN4ISAERepository>(Lifestyle.Scoped);
            container.Register<IISSCN6ISDLRepository, ISSCN6ISDLRepository>(Lifestyle.Scoped);
            container.Register<IISSCN7ISOPRepository, ISSCN7ISOPRepository>(Lifestyle.Scoped);
            container.Register<IISSCNISPS1Repository, ISSCNISPS1Repository>(Lifestyle.Scoped);
            container.Register<IISSCN4CRUNRepository, ISSCN4CRUNRepository>(Lifestyle.Scoped);
            container.Register<IAtendimentoRule, AtendimentoRule>(Lifestyle.Scoped);
            container.Register<IAtendimentoFacade, AtendimentoFacade>(Lifestyle.Scoped);
            container.Register<IAzureRepository, AzureRepository>(Lifestyle.Scoped);

            container.Register<IURARule, URARule>(Lifestyle.Scoped);
            container.Register<IURAFacade, URAFacade>(Lifestyle.Scoped);

            container.Register<CopasaDigitalDataContext>(Lifestyle.Scoped);
        }
    }
}