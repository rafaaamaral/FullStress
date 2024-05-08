using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entidade
{
    public class ClienteTradeFaturamento : BaseEntidade
    {
        [Required]
        public int IdClienteTrade { get; set; }

        [ForeignKey("IdClienteTrade")]
        public virtual ClienteTrade ClienteTrade { get; set; }

        [Required]
        public DateTime Inicio { get; set; }
        
        [Required]
        public DateTime Termino { get; set; }

        public decimal Valor { get; set; }
    }
}
