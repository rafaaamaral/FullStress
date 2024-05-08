using DataBase;
using DataBase.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class WeekendLeagueJogoDetalheRepositorio : BaseRepositorio<WeekendLeagueJogoDetalhe>
    {
        public List<WeekendLeagueJogoDetalhe> ObterJogoDetalhePorJogo(int idJogo)
        {
            using (var contexto = new Context())
            {
                var detalhe = contexto.WeekendLeagueJogoDetalhe.Include("JogadorGol").Include("JogadorAssistencia")
                    .Where(w => w.IdWeekenLeagueJogo == idJogo).AsQueryable();

                return detalhe.ToList();
            }
        }

        public List<WeekendLeagueJogoDetalhe> ObterJogoDetalhePorWL(int idWL)
        {
            using (var contexto = new Context())
            {
                var detalhe = contexto.WeekendLeagueJogoDetalhe.Include("WeekendLeagueJogo").Include("JogadorGol").Include("JogadorAssistencia")
                    .Where(w => w.WeekendLeagueJogo.IdWeekendLeague == idWL).AsQueryable();

                return detalhe.ToList();
            }
        }
    }
}
