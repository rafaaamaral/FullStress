using DataBase;
using DataBase.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class SessaoRepositorio : BaseRepositorio<Sessao>
    {
        public void InativarSessaoAntiga(int id)
        {
            using (var contexto = new Context())
            {
                var entidade = contexto.Sessao.FirstOrDefault(f => f.SessaoAtiva && f.IdUsuario == id && f.SessaoAtiva);

                if (entidade != null)
                {
                    entidade.SessaoAtiva = false;
                    entidade.Ativo = false;

                    contexto.SaveChanges();
                }
            }
        }
    }
}
