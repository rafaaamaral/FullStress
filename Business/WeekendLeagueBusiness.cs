using AutoMapper;
using DataBase.Entidade;
using DTO;
using Global.Enum;
using Global.Retornos;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class WeekendLeagueBusiness : BaseBusiness<WeekendLeagueDTO>
    {
        private readonly WeekendLeagueRepositorio _weekendLeagueRepositorio;

        public WeekendLeagueBusiness()
        {
            base.EntityType = typeof(WeekendLeague);
            base.RepositoryType = typeof(WeekendLeagueRepositorio);
            base.Includes = new[] { "Clube", "Versao", "Classificacao" };
            _weekendLeagueRepositorio = new WeekendLeagueRepositorio();
        }

        public Retorno<List<WeekendLeagueDTO>> ObterWeekendLeaguePorUsuarioEVersaoAtiva(int idUsuario)
        {
            var retorno = new Retorno<List<WeekendLeagueDTO>>();

            try
            {
                var versao = new VersaoRepositorio().ObterVersaoAtiva();

                var jogador = _weekendLeagueRepositorio.ObterWeekendLeaguePorUsuarioEVersaoAtiva(idUsuario, versao.Id);
                var weekDTO = Mapper.DynamicMap<List<WeekendLeagueDTO>>(jogador);

                retorno.Data = weekDTO;
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao trazer lista de Weekend League!";
            }

            return retorno;
        }

        public Retorno<int> IncluirWeekendLeague(int idUsuario)
        {
            var retorno = new Retorno<int>();

            try
            {
                var WeekendLeagueDTO = ObterDTO(idUsuario);

                var entidade = _weekendLeagueRepositorio.Save(Mapper.DynamicMap<WeekendLeague>(WeekendLeagueDTO));

                retorno.Data = entidade.Id;
                retorno.Mensagem = "WeekendLeague incluido com sucesso!";
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao incluir WeekendLeague";
            }

            return retorno;
        }

        public Retorno<int> AlterarWeekendLeague(WeekendLeagueDTO weekendLeague, int idUsuario)
        {
            var retorno = new Retorno<int>();

            try
            {
                _weekendLeagueRepositorio.AlterarWeekendLeague(weekendLeague.Id, weekendLeague.Descricao, weekendLeague.Status, weekendLeague.Top100);

                retorno.Data = 1;
                retorno.Mensagem = "WeekendLeague alterado com sucesso!";
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao alterar WeekendLeague";
            }

            return retorno;
        }

        public Retorno<int> AtualizarWeekendLeague(int idWeekendLeague, int progresso, int idClassificacao, int status,
            int idUsuario, int aproveitamento, int posseBola, int precisaoPasse, int? jogador, int? artilheiro, int? assistencia)
        {
            var retorno = new Retorno<int>();

            try
            {
                _weekendLeagueRepositorio.AtualizarWeekendLeague(idWeekendLeague, progresso, idClassificacao,
                    status, aproveitamento, posseBola, precisaoPasse, jogador, artilheiro, assistencia);

                retorno.Data = 1;
                retorno.Mensagem = "WeekendLeague alterado com sucesso!";
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao alterar WeekendLeague";
            }

            return retorno;
        }

        private WeekendLeagueDTO ObterDTO(int idUsuario)
        {
            return new WeekendLeagueDTO()
            {
                IdVersao = new VersaoBusiness().ObterVersaoAtiva(idUsuario).Data.Id,
                IdClube = new ClubeBusiness().ObterClubePorUsuarioVersao(idUsuario).Data.FirstOrDefault().Id,
                Descricao = "",
                Progresso = 0,
                IdClassificacao = new ClassificacaoWeekendLeagueBusiness().ObterClassificacaoPorNumeroVitoria(1, idUsuario).Data.Id,
                Top100 = false,
                Status = (int)StatusWeekendLeague.Iniciada,
                Ativo = true,
                DataCadastro = DateTime.Now,
            };
        }
    }
}
