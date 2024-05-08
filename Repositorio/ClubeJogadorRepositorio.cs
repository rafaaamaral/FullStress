using DataBase;
using DataBase.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class ClubeJogadorRepositorio: BaseRepositorio<ClubeJogador>
    {
        public List<ClubeJogador> ObterJogadorPorClube(int idClube)
        {
            using (var contexto = new Context())
            {
                var jogador = contexto.ClubeJogador.Include("Jogador").Include("Jogador.TipoJogador")
                    .Where(w => w.IdClube == idClube).AsQueryable();

                return jogador.ToList();
            }
        }

        public void AtivarInativarClubeJogador(int idClubeJogador, bool ativo)
        {
            using (var contexto = new Context())
            {
                var clube = contexto.ClubeJogador.FirstOrDefault(f => f.Id == idClubeJogador);

                clube.Ativo = ativo;

                contexto.SaveChanges();
            }
        }
    }
}
