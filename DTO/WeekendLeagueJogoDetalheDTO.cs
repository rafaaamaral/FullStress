using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class WeekendLeagueJogoDetalheDTO : BaseDTO
    {
        public int IdWeekenLeagueJogo { get; set; }
        public virtual WeekendLeagueJogoDTO WeekendLeagueJogo { get; set; }
        public int? IdJogadorGol { get; set; }
        public virtual JogadorDTO JogadorGol { get; set; }
        public int? QuantidadeGol { get; set; }
        public int? IdJogadorAssistencia { get; set; }
        public virtual JogadorDTO JogadorAssistencia { get; set; }
        public int? QuantidadeAssistencia { get; set; }
    }
}
