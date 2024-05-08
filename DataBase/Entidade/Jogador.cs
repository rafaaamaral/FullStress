using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entidade
{
    public class Jogador : BaseEntidade
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public int Overral { get; set; }

        public string Link { get; set; }

        public int IdTipoJogador { get; set; }

        [ForeignKey("IdTipoJogador")]
        public virtual TipoJogador TipoJogador { get; set; }

        public int IdVersao { get; set; }

        [ForeignKey("IdVersao")]
        public virtual Versao Versao { get; set; }
    }
}
