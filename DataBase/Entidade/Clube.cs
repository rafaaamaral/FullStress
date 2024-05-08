using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entidade
{
    public class Clube : BaseEntidade
    {
        [Required]
        public string Nome { get; set; }

        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }

        public int IdVersao { get; set; }

        [ForeignKey("IdVersao")]
        public virtual Versao Versao { get;set;}
    }
}
