using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entidade
{
    public class ModuloTransacao : BaseEntidade
    {
        [Required]
        public int IdModulo { get; set; }

        [ForeignKey("IdModulo")]
        public virtual Modulo Modulo { get; set; }

        [Required]
        public int IdTransacao { get; set; }

        [ForeignKey("IdTransacao")]
        public virtual Transacao Transacao { get; set; }
    }
}
