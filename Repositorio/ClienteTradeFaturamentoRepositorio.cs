using DataBase;
using DataBase.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class ClienteTradeFaturamentoRepositorio : BaseRepositorio<ClienteTradeFaturamento>
    {
        public List<ClienteTradeFaturamento> ObterFaturamentoPorCliente(int idCliente)
        {
            using (var contexto = new Context())
            {
                var jogador = contexto.ClienteTradeFaturamento
                    .Where(w => w.IdClienteTrade == idCliente).AsQueryable();

                return jogador.ToList();
            }
        }
    }
}
