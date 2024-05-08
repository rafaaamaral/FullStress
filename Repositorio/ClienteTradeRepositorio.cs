using DataBase;
using DataBase.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class ClienteTradeRepositorio : BaseRepositorio<ClienteTrade>
    {
        public List<ClienteTrade> ObterClienteTradePorUsuario(int idUsuario)
        {
            using (var contexto = new Context())
            {
                var clube = contexto.ClienteTrade.Include("Usuario")
                    .Where(w => w.IdUsuario == idUsuario).AsQueryable();

                return clube.ToList();
            }
        }

        public void AlterarClienteTrade(int idClienteTrade, string nome, string telefone, int plano)
        {
            using (var contexto = new Context())
            {
                var clube = contexto.ClienteTrade.FirstOrDefault(f => f.Id == idClienteTrade);

                clube.Nome = nome;
                clube.Telefone = telefone;
                clube.Plano = plano;

                contexto.SaveChanges();
            }
        }
    }
}
