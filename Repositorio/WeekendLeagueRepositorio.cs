using DataBase;
using DataBase.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class WeekendLeagueRepositorio : BaseRepositorio<WeekendLeague>
    {
        public List<WeekendLeague> ObterWeekendLeaguePorUsuarioEVersaoAtiva(int idUsuario, int idVersao)
        {
            using(var context = new Context())
            {
                var retorno = context.WeekendLeague.Include("Clube").Include("Classificacao")
                    .Include("Artilheiro").Include("LiderAssistencia").Include("MelhorJogador")
                    .Where(w => w.Clube.IdUsuario == idUsuario && w.IdVersao == idVersao).AsQueryable();

                return retorno.ToList();
            }
        }

        public void AlterarWeekendLeague(int idWeekendLeague, string descricao, int status, bool top100)
        {
            using (var contexto = new Context())
            {
                var weekendLeague = contexto.WeekendLeague.FirstOrDefault(f => f.Id == idWeekendLeague);

                weekendLeague.Descricao = descricao;
                weekendLeague.Status = status;
                weekendLeague.Top100 = top100;

                contexto.SaveChanges();
            }
        }

        public void AtualizarWeekendLeague(int idWeekendLeague, int progresso, int idClassificacao, int status,
            int aproveitamento, int posseBola, int precisaoPasse, int? jogador, int? artilheiro, int? assistencia)
        {
            using (var contexto = new Context())
            {
                var weekendLeague = contexto.WeekendLeague.FirstOrDefault(f => f.Id == idWeekendLeague);

                weekendLeague.Progresso = progresso;
                weekendLeague.IdClassificacao = idClassificacao;
                weekendLeague.Status = status;
                weekendLeague.Aproveitamento = aproveitamento;
                weekendLeague.PosseDeBola = posseBola;
                weekendLeague.PrecisaoPasse = precisaoPasse;
                weekendLeague.IdMelhorJogador = jogador == 0 ? null : jogador;
                weekendLeague.IdArtilheiro = artilheiro == 0 ? null : artilheiro;
                weekendLeague.IdLiderAssistencia = assistencia == 0 ? null : assistencia;

                contexto.SaveChanges();
            }
        }
    }
}
