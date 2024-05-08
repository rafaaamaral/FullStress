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
    public class WeekendLeagueJogoBusiness : BaseBusiness<WeekendLeagueJogoDTO>
    {
        private readonly WeekendLeagueJogoRepositorio _weekendLeagueJogoRepositorio;

        public WeekendLeagueJogoBusiness()
        {
            base.EntityType = typeof(WeekendLeagueJogo);
            base.RepositoryType = typeof(WeekendLeagueJogoRepositorio);
            base.Includes = new[] { "WeekendLeague", "JogadorMelhorCampo" };
            _weekendLeagueJogoRepositorio = new WeekendLeagueJogoRepositorio();
        }

        public Retorno<List<WeekendLeagueJogoDTO>> ObterJogoPorWL(int idWeekendLeague, int idUsuario)
        {
            var retorno = new Retorno<List<WeekendLeagueJogoDTO>>();

            try
            {
                var jogador = _weekendLeagueJogoRepositorio.ObterJogoPorWL(idWeekendLeague);
                retorno.Data = Mapper.DynamicMap<List<WeekendLeagueJogoDTO>>(jogador);
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao trazer lista de jogos!";
            }

            return retorno;
        }

        public Retorno<WeekendLeagueJogoDTO> ObterJogoPorId(int idJogo, int idUsuario)
        {
            var retorno = new Retorno<WeekendLeagueJogoDTO>();

            try
            {
                var jogador = _weekendLeagueJogoRepositorio.ObterJogoPorId(idJogo);
                retorno.Data = Mapper.DynamicMap<WeekendLeagueJogoDTO>(jogador);
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao trazer lista de jogos!";
            }

            return retorno;
        }

        public Retorno<int> ProcessarWeekendLeagueJogo(WeekendLeagueJogoDTO objDados, int idUsuario)
        {
            var retorno = new Retorno<int>();

            try
            {
                var validar = ValidarWeekendLeagueJogo(objDados);


                if (!string.IsNullOrEmpty(validar))
                {
                    retorno.Sucesso = false;
                    retorno.Mensagem = validar;
                    return retorno;
                }

                var dtoWLJogo = ObterDTO(objDados);

                var entidade = _weekendLeagueJogoRepositorio.Save(Mapper.DynamicMap<WeekendLeagueJogo>(dtoWLJogo));

                if(entidade.Id > 0)
                {
                    ProcessarWeekendLeagueJogoDetalhe(objDados, idUsuario, retorno, entidade);
                    if (!retorno.Sucesso)
                    {
                        return retorno;
                    }

                    AtualizarWeekendLeague(objDados, idUsuario, retorno);
                    if (!retorno.Sucesso)
                    {
                        return retorno;
                    }
                }
            }
            catch(Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao atualizar WL, porém o jogo foi salvo, atualize a página!";
            }

            return retorno;
        }

        public Retorno ExcluirWeekendLeagueJogo(WeekendLeagueJogoDTO objDados, int idUsuario)
        {
            var retorno = new Retorno();

            try
            {
                _weekendLeagueJogoRepositorio.ExcluirWeekendLeagueJogo(objDados.Id);
            }
            catch (Exception ex)
            {
                new ErroBusiness().IncluirErro(idUsuario, ex.StackTrace, ex.Message);
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao Excluir!";
            }

            return retorno;
        }

        private void AtualizarWeekendLeague(WeekendLeagueJogoDTO objDados, int idUsuario, Retorno<int> retorno)
        {
            var retornoWeekendLeague = new WeekendLeagueBusiness().GetById(objDados.IdWeekendLeague);

            if (retornoWeekendLeague.Sucesso)
            {
                var artilheiro = 0;
                var assistencia = 0;
                var jogador = 0;

                var weekendLeague = retornoWeekendLeague.Data;

                var jogos = _weekendLeagueJogoRepositorio.ObterJogoPorWL(objDados.IdWeekendLeague);
                var jogosDetalhe = new WeekendLeagueJogoDetalheRepositorio().ObterJogoDetalhePorWL(objDados.IdWeekendLeague);

                var progresso = (jogos.Count() * 100 ) / 30;

                var aproveitamento = (jogos.Count(c => c.Vitoria) * 100) / jogos.Count();

                var posseBola = Convert.ToInt32(jogos.Average(a => a.PosseDeBola));

                var precisaoPasse = Convert.ToInt32(jogos.Average(a => a.PrecisaoPasses));

                jogador = jogos.Count(c => c.IdJogadorMelhorCampo != null) <= 0 ? 0 :
                    jogos.GroupBy(g => g.IdJogadorMelhorCampo).Select(s => new { id = s.Key, count = s.Count() }).FirstOrDefault().id.Value;

                if (jogosDetalhe.Count() > 0)
                {
                    artilheiro = jogosDetalhe.GroupBy(g => g.IdJogadorGol).Select(s => new { id = s.Key, count = s.Sum(su => su.QuantidadeGol) }).OrderByDescending(o => o.count).FirstOrDefault().id.Value;
                    assistencia = jogosDetalhe.GroupBy(g => g.IdJogadorAssistencia).Select(s => new { id = s.Key, count = s.Sum(su => su.QuantidadeAssistencia) }).OrderByDescending(o => o.count).FirstOrDefault().id.Value;
                }

                var classificacao = new ClassificacaoWeekendLeagueRepositorio()
                    .ObterClassificacaoPorNumeroVitoria(jogos.Where(w => w.Vitoria).Count(), weekendLeague.IdVersao).Id;

                var status = (int)StatusWeekendLeague.EmAndamento;

                var retornoAtualizacaoWeekendLeague = new WeekendLeagueBusiness()
                    .AtualizarWeekendLeague(weekendLeague.Id, progresso, classificacao, status, 
                    idUsuario, aproveitamento, posseBola, precisaoPasse, jogador, artilheiro, assistencia);

                if (!retornoAtualizacaoWeekendLeague.Sucesso)
                {
                    retorno.Sucesso = false;
                    retorno.Mensagem = retornoAtualizacaoWeekendLeague.Mensagem;
                };
            }
        }

        private static void ProcessarWeekendLeagueJogoDetalhe(WeekendLeagueJogoDTO objDados, int idUsuario, Retorno<int> retorno, WeekendLeagueJogo entidade)
        {
            foreach (var jogodetalhe in objDados.ListaJogoDetalhe)
            {
                var retornoJogoDetalhe = new WeekendLeagueJogoDetalheBusiness().IncluirWeekendLeagueJogoDetalhe(jogodetalhe, idUsuario, entidade.Id);

                if (retornoJogoDetalhe.Sucesso)
                    continue;
                else
                {
                    retorno.Sucesso = false;
                    retorno.Mensagem = retornoJogoDetalhe.Mensagem;
                }
            }

            foreach (var jogodetalhe in objDados.ListaRemoverJogoDetalhe)
            {
                var jogoExcluir = new WeekendLeagueJogoDetalheRepositorio()
                    .Get(w => w.IdJogadorGol == jogodetalhe.IdJogadorGol
                    && w.IdJogadorAssistencia == jogodetalhe.IdJogadorAssistencia && w.IdWeekenLeagueJogo == entidade.Id).FirstOrDefault();

                if (jogoExcluir != null)
                {
                    var retornoJogoDetalhe = new WeekendLeagueJogoDetalheBusiness().Remove(jogoExcluir.Id);

                    if (retornoJogoDetalhe.Sucesso)
                        continue;
                    else
                    {
                        retorno.Sucesso = false;
                        retorno.Mensagem = retornoJogoDetalhe.Mensagem;
                    }
                }
            }
        }

        private string ValidarWeekendLeagueJogo(WeekendLeagueJogoDTO objDados)
        {
            var retorno = string.Empty;

            var golsJogadores = objDados.ListaJogoDetalhe.Sum(s => s.QuantidadeGol);
            var assistencia = objDados.ListaJogoDetalhe.Sum(s => s.QuantidadeAssistencia);

            if(golsJogadores > objDados.GolsFavor)
            {
                retorno += "Quantidade de gols dos jogadores maior que Gols a favor <br />";
            }

            if (assistencia > objDados.GolsFavor)
            {
                retorno += "Quantidade de assistência dos jogadores maior que Gols a favor <br />";
            }


            return retorno;
        }

        private WeekendLeagueJogoDTO ObterDTO(WeekendLeagueJogoDTO objDados)
        {
            return new WeekendLeagueJogoDTO
            {
                IdWeekendLeague = objDados.IdWeekendLeague,
                GolsContra = objDados.GolsContra,
                GolsFavor = objDados.GolsFavor,
                IdJogadorMelhorCampo = objDados.IdJogadorMelhorCampo,
                Observacao = objDados.Observacao,
                PosseDeBola = objDados.PosseDeBola,
                PrecisaoFinalizacao = objDados.PrecisaoFinalizacao,
                PrecisaoPasses = objDados.PrecisaoPasses,
                PsnAdversario = objDados.PsnAdversario,
                QuitRage = objDados.QuitRage,
                Vitoria = (objDados.GolsFavor > objDados.GolsContra)
            };
        }
    }
}
