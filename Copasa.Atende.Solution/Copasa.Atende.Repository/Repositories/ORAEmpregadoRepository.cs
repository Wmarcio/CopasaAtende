using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ORAEmpregadoRepository - Tabela de empregados
    /// </summary>
    public class ORAEmpregadoRepository : BaseRepository<ORAEmpregado>, IORAEmpregadoRepository
    {
        private IORACargoRepository _oraCargoRepository;
        private IORAUnidadeOrganizacionalRepository _oraUnidadeOrganizacionalRepository;

        /// <summary>
        /// Contrutor.
        /// </summary>
        /// <param name="oraCargoRepository">IORACargoRepository</param>
        /// <param name="oraUnidadeOrganizacionalRepository">IORAUnidadeOrganizacionalRepository</param>
        /// <param name="log">ILog</param>
        public ORAEmpregadoRepository(
            IORACargoRepository oraCargoRepository,
            IORAUnidadeOrganizacionalRepository oraUnidadeOrganizacionalRepository,
            ILog log)
            : base(log)
        { 
            _oraCargoRepository = oraCargoRepository;
            _oraUnidadeOrganizacionalRepository = oraUnidadeOrganizacionalRepository;
        }

        /// <summary>
        /// Preenche dados do usuário de acordo com seu perfil
        /// </summary>
        public bool preencheDadosUsuario(TrabUsuario trabUsuario)
        {
            bool retorno = false;
            try
            {
                string prefixo = "";
                if ((trabUsuario.codigoUsuario == null || "".Equals(trabUsuario.codigoUsuario)) && trabUsuario.nomeUsuario != null && !"".Equals(trabUsuario.nomeUsuario))
                {
                    trabUsuario.codigoUsuario = trabUsuario.nomeUsuario;
                }
                long parmPesquisa;
                if (trabUsuario.codigoUsuario != null && !"".Equals(trabUsuario.codigoUsuario) && long.TryParse(trabUsuario.codigoUsuario, out parmPesquisa))
                {
                    IList<ORAEmpregado> listaEmpregado = GetMany(x => x.matricula == parmPesquisa);
                    if (listaEmpregado.Count > 0)
                    {
                        ORAEmpregado empregado = listaEmpregado.ToArray()[0];
                        if ("".Equals(trabUsuario.nomeUsuario))
                            trabUsuario.nomeUsuario = empregado.nome;
                        IList<ORACargo> listaCargo = _oraCargoRepository.GetMany(x => x.codigo == empregado.codigoCargo);
                        IList<ORAUnidadeOrganizacional> listaUnidade = _oraUnidadeOrganizacionalRepository.GetMany(x => x.codigo == empregado.codigoUnidadeOrganizacional);
                        if (listaCargo.Count > 0 && listaUnidade.Count > 0)
                        {
                            ORACargo cargo = listaCargo.ToArray()[0];
                            ORAUnidadeOrganizacional unidade = listaUnidade.ToArray()[0];
                            if (unidade.siglao.Contains("SPIN"))
                            {
                                if (cargo.descricao.ToUpper().Contains("ANAL"))
                                    prefixo = "AN";
                                else
                                    prefixo = "PG";
                            }
                            else
                            {
                                prefixo = "US";
                            }
                            if (listaUnidade.Count > 0 && "".Equals(trabUsuario.agenciaUsuario))
                            {
                                trabUsuario.agenciaUsuario = unidade.sigla;
                            }
                            retorno = true;
                        }
                    }
                    trabUsuario.codigoUsuario = prefixo + trabUsuario.codigoUsuario;
                }
                else
                {
                    trabUsuario.codigoUsuario = trabUsuario.codigoUsuario.ToUpper();
                    if (trabUsuario.codigoUsuario != null && trabUsuario.codigoUsuario.Length > 2)
                    {
                        if ("CL".Equals(trabUsuario.codigoUsuario.Substring(0, 2)))
                        {
                            if (trabUsuario.nomeUsuario == null && "".Equals(trabUsuario.nomeUsuario))
                                trabUsuario.nomeUsuario = trabUsuario.codigoUsuario;
                            if ("".Equals(trabUsuario.agenciaUsuario))
                                trabUsuario.agenciaUsuario = "AEC";
                            retorno = true;
                        }
                        else if ("WEB".Equals(trabUsuario.codigoUsuario) 
                            || "MGAPP".Equals(trabUsuario.codigoUsuario)
                            || "APP".Equals(trabUsuario.codigoUsuario))
                        {
                            trabUsuario.nomeUsuario = trabUsuario.codigoUsuario;
                            trabUsuario.agenciaUsuario = trabUsuario.codigoUsuario;
                        }
                        else
                        {
                            trabUsuario.nomeUsuario = trabUsuario.codigoUsuario;
                            trabUsuario.codigoUsuario = "CRM";
                            trabUsuario.agenciaUsuario = "CRM";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string erro = e.Message;
            }
            return retorno;
        }

    }
}
