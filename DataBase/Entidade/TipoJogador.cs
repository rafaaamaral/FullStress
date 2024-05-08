using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entidade
{
    public class TipoJogador : BaseEntidade
    {
        [Required]
        public string Descricao { get; set; }

        [Required]
        public string Foto { get; set; }

        public int? IdVersao { get; set; }

        [ForeignKey("IdVersao")]
        public virtual Versao Versao { get; set; }
    }
}
