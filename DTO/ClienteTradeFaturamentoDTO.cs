using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ClienteTradeFaturamentoDTO : BaseDTO
    {
        public int IdClienteTrade { get; set; }
        public ClienteTradeDTO ClienteTrade { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Termino { get; set; }
        public decimal Valor { get; set; }

        public string DtInicio { get; set; }
        public string DtTermino { get; set; }
        public string VlValor { get; set; }
    }
}
