using DataBase;
using DataBase.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class PerfilModuloTransacaoRepositorio: BaseRepositorio<PerfilModuloTransacao>
    {
        public List<PerfilModuloTransacao> BuscarTransacaoPorPerfil(int idPerfil)
        {
            using (var contexto = new Context())
            {
                var listaPerfilTransacao = contexto.PerfilModuloTransacao.Include("ModuloTransacao").Include("ModuloTransacao.Transacao").Include("ModuloTransacao.Modulo")
                    .Where(w => w.IdPerfil == idPerfil &&
                                w.ModuloTransacao.Ativo && w.ModuloTransacao.Transacao.Ativo && w.ModuloTransacao.Modulo.Ativo).ToList();

                return listaPerfilTransacao.ToList();
            }
        }
    }
}
