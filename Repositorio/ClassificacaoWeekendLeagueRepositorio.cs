using DataBase;
using DataBase.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class ClassificacaoWeekendLeagueRepositorio: BaseRepositorio<ClassificacaoWeekendLeague>
    {
        public ClassificacaoWeekendLeague ObterClassificacaoPorNumeroVitoria(int numeroVitoria, int idVersao)
        {
            using (var context = new Context())
            {
                var retorno = context.ClassificacaoWeekendLeague
                    .Where(w => w.IdVersao == idVersao && w.NumeroVitoria >= 0 && w.NumeroVitoria <= numeroVitoria).AsQueryable();

                return retorno.OrderByDescending(o => o.Id).FirstOrDefault();
            }
        }

    }
}
