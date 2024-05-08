using DataBase;
using DataBase.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class ClubeRepositorio : BaseRepositorio<Clube>
    {
        public List<Clube> ObterClubePorUsuario(int idUsuario, int idVersao)
        {
            using (var contexto = new Context())
            {
                var clube = contexto.Clube.Include("Usuario")
                    .Where(w => w.IdUsuario == idUsuario && w.IdVersao == idVersao).AsQueryable();

                return clube.ToList();
            }
        }

        public void AlterarClube(int idClube, string nome)
        {
            using (var contexto = new Context())
            {
                var clube = contexto.Clube.FirstOrDefault(f => f.Id == idClube);

                clube.Nome = nome;

                contexto.SaveChanges();
            }
        }
    }
}
