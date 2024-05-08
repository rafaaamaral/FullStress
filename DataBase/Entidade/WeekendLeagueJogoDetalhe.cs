using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entidade
{
    public class WeekendLeagueJogoDetalhe : BaseEntidade
    {
        public int IdWeekenLeagueJogo { get; set; }

        [ForeignKey("IdWeekenLeagueJogo")]
        public virtual WeekendLeagueJogo WeekendLeagueJogo { get; set; }

        public int? IdJogadorGol { get; set; }

        [ForeignKey("IdJogadorGol")]
        public virtual Jogador JogadorGol { get; set; }

        public int? QuantidadeGol { get; set; }

        public int? IdJogadorAssistencia { get; set; }

        [ForeignKey("IdJogadorAssistencia")]
        public virtual Jogador JogadorAssistencia { get; set; }

        public int? QuantidadeAssistencia { get; set; }

    }
}
