using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entidade
{
    public class Erro : BaseEntidade
    {
        [Column(TypeName = "varchar(MAX)")]
        public string Rastro { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string Mensagem { get; set; }

        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }
    }
}
