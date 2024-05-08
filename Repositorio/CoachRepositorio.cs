using DataBase;
using DataBase.Entidade;
using Global.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class CoachRepositorio : BaseRepositorio<Coach>
    {
        public List<Coach> ObterListaCoachPorUsuario(int idUsuario)
        {
            using (var contexto = new Context())
            {
                var coach = contexto.Coach
                    .Where(w => w.IdPlayer == idUsuario).AsQueryable();

                return coach.OrderByDescending(o => o.Id).ToList();
            }
        }

        public List<Coach> ObterCoachPorStatus(int? idStatus)
        {
            using (var contexto = new Context())
            {
                var coach = contexto.Coach.Include("Player")
                    .Where(w => w.Status == (idStatus.HasValue ? idStatus.Value : w.Status)).AsQueryable();

                return coach.OrderByDescending(o => o.Id).ToList();
            }
        }

        public void ResponderCoach(int idCoach, string feedback)
        {
            using (var contexto = new Context())
            {
                var coach = contexto.Coach.Where(w => w.Id == idCoach).FirstOrDefault();

                coach.FeedbackCoach = feedback;
                coach.Status = (int)StatusCoach.Respondido;

                contexto.SaveChanges();
            }
        }
    }
}
