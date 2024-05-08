using DataBase;
using DataBase.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class JogadorRepositorio : BaseRepositorio<Jogador>
    {
        public List<Jogador> ObterJogadorPorVersaoAtiva(int idVersao)
        {
            using (var contexto = new Context())
            {
                var jogador = contexto.Jogador.Include("TipoJogador")
                    .Where(w => w.IdVersao == idVersao).AsQueryable();

                return jogador.ToList();
            }
        }

        public void AlterarJogador(int idJogador, string nome, int overral, string link, int idTipoJogador)
        {
            using (var contexto = new Context())
            {
                var jogador = contexto.Jogador.FirstOrDefault(f => f.Id == idJogador);

                jogador.Nome = nome.ToUpper();
                jogador.Overral = overral;
                jogador.Link = link;
                jogador.IdTipoJogador = idTipoJogador;

                contexto.SaveChanges();
            }
        }
    }
}
