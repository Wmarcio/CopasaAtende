namespace Copasa.Atende.Repository.UnitOfWork
{
    using Copasa.Atende.Repository.Interfaces.Digital;
    using Copasa.Atende.Repository.Repositories;
    using Copasa.Atende.Repository.Repositories.Digital;
    using Copasa.Digital.Repository.Interfaces;
    using Copasa.Digital.Repository.Repositories;
    using Infrastructure;
    using System;

    /// <summary>
    /// Classe Unit Of Work.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;
        // TODO Trocar para o DbContext do seu projeto.
        private readonly CopasaAtendeDataContext _dataContext;
        private readonly CopasaDigitalDataContext _DigitaldataContext;

        private IClienteRepository _ClienteRepository;
        private IParametroRepository _ParametroRepository;
        private IClienteIdentificadorRepository _ClienteIdentificadorRepository;
        private ITelaRepository _TelaRepository;
        private ITituloRepository _TituloRepository;
        private ITelaTituloRepository _TelaTituloRepository;

        /// <summary>
        /// Construtor.
        /// </summary>
        public UnitOfWork()
        {
            // TODO Trocar para o DbContext do seu projeto.
            _dataContext = new CopasaAtendeDataContext();
            _DigitaldataContext = new CopasaDigitalDataContext();
        }
               
        /// <summary>
        /// Commit.
        /// </summary>
        public void Commit()
        {
            _dataContext.SaveChanges();
        }

        /// <summary>
        /// Commit.
        /// </summary>
        public void DCommit()
        {
            _DigitaldataContext.SaveChanges();
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dataContext.Dispose();
                }
            }
            _disposed = true;
        }

        

        /// <summary>
        /// acesso ao cliente copasadigital
        /// </summary>
        public IClienteRepository ClienteRepository
        {
            get
            {
                if (_ClienteRepository == null)
                {

                    _ClienteRepository = new ClienteRepository(_DigitaldataContext);
                }

                return _ClienteRepository;

            }
        }

        /// <summary>
        /// Propriedade ClienteIdentificadorRepository.
        /// </summary>
        public IClienteIdentificadorRepository ClienteIdentificadorRepository
        {
            get
            {
                if (_ClienteIdentificadorRepository == null)
                {
                    _ClienteIdentificadorRepository = new ClienteIdentificadorRepository(_DigitaldataContext);
                }

                return _ClienteIdentificadorRepository;
            }
        }

        /// <summary>
        /// Acesso Parametro CopasaDigital
        /// </summary>
        public IParametroRepository ParametroRepository
        {
            get
            {
                if (_ParametroRepository == null)
                {

                    _ParametroRepository = new ParametroRepository(_DigitaldataContext);
                }

                return _ParametroRepository;

            }
        }

        /// <summary>
        /// Acesso Parametro CopasaDigital
        /// </summary>
        public ITelaRepository TelaRepository
        {
            get
            {
                if (_TelaRepository == null)
                {

                    _TelaRepository = new TelaRepository(_DigitaldataContext);
                }

                return _TelaRepository;

            }
        }


        /// <summary>
        /// Acesso Parametro CopasaDigital
        /// </summary>
        public ITituloRepository TituloRepository 
        {
            get
            {
                if (_TituloRepository == null)
                {

                    _TituloRepository = new TituloRepository(_DigitaldataContext);
    }

                return _TituloRepository;

            }
        }

        /// <summary>
        /// Acesso Parametro CopasaDigital
        /// </summary>
        public ITelaTituloRepository TelaTituloRepository
        {
            get
            {
                if (_TelaTituloRepository == null)
                {

                    _TelaTituloRepository = new TelaTituloRepository(_DigitaldataContext);
                }

                return _TelaTituloRepository;

            }
        }
    }
}
