using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entidade
{
    public class WeekendLeagueJogo : BaseEntidade
    {
        public int IdWeekendLeague { get; set; }

        [ForeignKey("IdWeekendLeague")]
        public virtual WeekendLeague WeekendLeague { get; set; }

        [Required]
        public int GolsFavor { get; set; }

        [Required]
        public string PsnAdversario { get; set; }

        [Required]
        public int GolsContra { get; set; }

        public int PrecisaoFinalizacao { get; set; }

        public int PrecisaoPasses { get; set; }

        public int PosseDeBola { get; set; }

        public bool QuitRage { get; set; }

        public bool Vitoria { get; set; }

        public string Observacao { get; set; }

        public int? IdJogadorMelhorCampo { get; set; }

        [ForeignKey("IdJogadorMelhorCampo")]
        public Jogador JogadorMelhorCampo { get; set; }
    }
}
