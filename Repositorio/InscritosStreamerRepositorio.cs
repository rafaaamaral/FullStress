using DataBase;
using DataBase.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class InscritosStreamerRepositorio : BaseRepositorio<InscritosStreamer>
    {
        public List<InscritosStreamer> ObterIncritosPorUsuario(int idUsuario)
        {
            using (var contexto = new Context())
            {
                var inscrito = contexto.InscritosStreamer.Include("Streamer")
                    .Where(w => w.IdStreamer == idUsuario).AsQueryable();

                return inscrito.OrderBy(o => o.NomeUsuario).ToList();
            }
        }

        public void InativarTodosIncritos()
        {
            using (var contexto = new Context())
            {
                var inscritos = contexto.InscritosStreamer.ToList();

                inscritos.ForEach(a => a.Ativo = false);
                contexto.SaveChanges();
            }
        }

        public InscritosStreamer ObterInscritoPorNome(string nome)
        {
            using (var contexto = new Context())
            {
                var inscrito = contexto.InscritosStreamer.Include("Streamer")
                    .Where(w => w.NomeUsuario == nome).AsQueryable();

                return inscrito.FirstOrDefault();
            }
        }

        public void AlterarInscrito(string nome, string dataInscricao)
        {
            using (var contexto = new Context())
            {
                var inscrito = contexto.InscritosStreamer
                    .FirstOrDefault(w => w.NomeUsuario == nome);

                inscrito.NomeUsuario = nome;
                inscrito.DataInscricao = Convert.ToDateTime(dataInscricao);
                inscrito.Ativo = true;

                contexto.SaveChanges();
            }
        }
    }
}
