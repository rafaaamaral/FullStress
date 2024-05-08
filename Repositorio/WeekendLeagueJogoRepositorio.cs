using DataBase;
using DataBase.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class WeekendLeagueJogoRepositorio : BaseRepositorio<WeekendLeagueJogo>
    {
        public List<WeekendLeagueJogo> ObterJogoPorWL(int idWeekendLeague)
        {
            using (var contexto = new Context())
            {
                var jogo = contexto.WeekendLeagueJogo.Include("JogadorMelhorCampo")
                    .Where(w => w.IdWeekendLeague == idWeekendLeague).ToList();

                return jogo;
            }
        }

        public WeekendLeagueJogo ObterJogoPorId(int idJogo)
        {
            using (var contexto = new Context())
            {
                var jogo = contexto.WeekendLeagueJogo.Include("WeekendLeague")
                    .Where(w => w.Id == idJogo).FirstOrDefault();

                return jogo;
            }
        }

        public void ExcluirWeekendLeagueJogo(int idJogo)
        {
            using (var contexto = new Context())
            {

                contexto.WeekendLeagueJogoDetalhe.RemoveRange(contexto.WeekendLeagueJogoDetalhe.Where(w => w.IdWeekenLeagueJogo == idJogo));
                contexto.WeekendLeagueJogo.Remove(contexto.WeekendLeagueJogo.FirstOrDefault(f => f.Id == idJogo));

                contexto.SaveChanges();
            }
        }

    }
}
