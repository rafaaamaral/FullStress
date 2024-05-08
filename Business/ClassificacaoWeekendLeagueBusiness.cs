using AutoMapper;
using DataBase.Entidade;
using DTO;
using Global.Retornos;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ClassificacaoWeekendLeagueBusiness : BaseBusiness<ClassificacaoWeekendLeagueDTO>
    {
        private readonly ClassificacaoWeekendLeagueRepositorio _classificacaoWeekendLeagueRepositorio;

        public ClassificacaoWeekendLeagueBusiness()
        {
            base.EntityType = typeof(ClassificacaoWeekendLeague);
            base.RepositoryType = typeof(ClassificacaoWeekendLeagueRepositorio);
            base.Includes = new string[] { };
            _classificacaoWeekendLeagueRepositorio = new ClassificacaoWeekendLeagueRepositorio();
        }

        public Retorno<ClassificacaoWeekendLeagueDTO> ObterClassificacaoPorNumeroVitoria(int numeroVitoria, int idUsuario)
        {
            var retorno = new Retorno<ClassificacaoWeekendLeagueDTO>();

            try
            {
                var versao = new VersaoRepositorio().ObterVersaoAtiva();

                var jogador = _classificacaoWeekendLeagueRepositorio.ObterClassificacaoPorNumeroVitoria(numeroVitoria, versao.Id);
                retorno.Data = Mapper.DynamicMap<ClassificacaoWeekendLeagueDTO>(jogador);
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao trazer classificação!";
            }

            return retorno;
        }
    }
}
