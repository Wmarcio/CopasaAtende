namespace Copasa.Atende.Repository.UnitOfWork
{
    using Copasa.Atende.Repository.Interfaces.Digital;
    using Copasa.Digital.Repository.Interfaces;
    using System;

    /// <summary>
    /// Interface IUnitOfWork.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commit.
        /// </summary>
        void Commit();

        /// <summary>
        /// 
        /// </summary>
        void DCommit();


        /// <summary>
        /// Propriedade ClienteRepository.
        /// </summary>
        IClienteRepository ClienteRepository { get; }

        /// <summary>
        /// Propriedade ParametroRepository.
        /// </summary>
        IParametroRepository ParametroRepository { get; }

        /// <summary>
        /// 
        /// </summary>
        IClienteIdentificadorRepository ClienteIdentificadorRepository { get; }

        /// <summary>
        /// Propriedade ClienteRepository.
        /// </summary>
        ITelaRepository TelaRepository { get; }
        /// <summary>
        /// Propriedade ClienteRepository.
        /// </summary>
        ITituloRepository TituloRepository { get; }
        /// <summary>
        /// Propriedade ClienteRepository.
        /// </summary>
        ITelaTituloRepository TelaTituloRepository { get; }

    }
}
