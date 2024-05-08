using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entidade
{
    public class ClubeJogador : BaseEntidade
    {
        [Required]
        public int IdClube { get; set; }

        [ForeignKey("IdClube")]
        public Clube Clube { get; set; }

        [Required]
        public int IdJogador { get; set; }

        [ForeignKey("IdJogador")]
        public Jogador Jogador { get; set; }
    }
}
