using Copasa.Atende.Business.Core;
using Copasa.Atende.Business.Interfaces;
using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Repository.Repositories;
using System;

namespace Copasa.Atende.Business.Rules
{
    /// <summary>
    /// Rule - Vazamento.
    /// </summary>
    public class VazamentoRule : BaseRule, IVazamentoRule
    {
        private IISSCN4ISVRRepository _sCN4ISVRRepository;
        private IISSCN4ISVIRepository _sCN4ISVIRepository;

        /// <summary>
        /// Construtor VazamentoRule.
        /// </summary>
        /// <param name="sCN4ISVRRepository">IISSCN4ISVRRepository.</param>
        /// <param name="sCN4ISVIRepository">IISSCN4ISVIRepository.</param>
        public VazamentoRule(IISSCN4ISVRRepository sCN4ISVRRepository, IISSCN4ISVIRepository sCN4ISVIRepository)
        {
            _sCN4ISVRRepository = sCN4ISVRRepository;
            _sCN4ISVIRepository = sCN4ISVIRepository;
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Vazamento na rua - SCN4ISVR.
        /// </summary>
        public BaseResponse SCN4ISVR(SCN4ISVRSend sCN4ISVRSend)
        {            
            BaseResponse response = new BaseResponse();
            response.Model = _sCN4ISVRRepository.Connect(sCN4ISVRSend);
            return response;
        }

        /// <summary>
        /// Vazamento no imovel - SCN4ISVI.
        /// </summary>
        public BaseResponse SCN4ISVI(SCN4ISVISend sCN4ISVISend)
        {            
            BaseResponse response = new BaseResponse();
            response.Model = _sCN4ISVIRepository.Connect(sCN4ISVISend);
            return response;
        }
    }
}
