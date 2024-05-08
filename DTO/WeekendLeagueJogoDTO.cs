using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class WeekendLeagueJogoDTO : BaseDTO
    {
        public int IdWeekendLeague { get; set; }
        public virtual WeekendLeagueDTO WeekendLeague { get; set; }
        public int GolsFavor { get; set; }
        public string PsnAdversario { get; set; }
        public int GolsContra { get; set; }
        public int PrecisaoFinalizacao { get; set; }
        public int PrecisaoPasses { get; set; }
        public int PosseDeBola { get; set; }
        public bool QuitRage { get; set; }
        public bool Vitoria { get; set; }
        public string Observacao { get; set; }
        public int? IdJogadorMelhorCampo { get; set; }
        public JogadorDTO JogadorMelhorCampo { get; set; }

        public List<WeekendLeagueJogoDetalheDTO> ListaJogoDetalhe { get; set; }
        public List<WeekendLeagueJogoDetalheDTO> ListaRemoverJogoDetalhe { get; set; }
    }
}
