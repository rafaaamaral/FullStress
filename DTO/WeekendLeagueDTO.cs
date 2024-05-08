using Global.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class WeekendLeagueDTO : BaseDTO
    {
        public int IdVersao { get; set; }
        public virtual VersaoDTO Versao { get; set; }
        public int IdClube { get; set; }
        public virtual ClubeDTO Clube { get; set; }
        public string Descricao { get; set; }
        public int Progresso { get; set; }
        public int IdClassificacao { get; set; }
        public virtual ClassificacaoWeekendLeagueDTO Classificacao { get; set; }
        public bool Top100 { get; set; }
        public int Status { get; set; }
        public int Aproveitamento { get; set; }
        public int? IdArtilheiro { get; set; }
        public JogadorDTO Artilheiro { get; set; }
        public int? IdLiderAssistencia { get; set; }
        public JogadorDTO LiderAssistencia { get; set; }
        public int PosseDeBola { get; set; }
        public int PrecisaoPasse { get; set; }
        public int NumeroVitorias { get; set; }
        public int? IdMelhorJogador { get; set; }
        public JogadorDTO MelhorJogador { get; set; }

        public string DescricaoStatus
        {
            get
            {
                switch (Status)
                {
                    case (int)StatusWeekendLeague.Iniciada:
                        return "Iniciada";
                    case (int)StatusWeekendLeague.EmAndamento:
                        return "Em Andamento";
                    case (int)StatusWeekendLeague.Finalizada:
                        return "Finalizada";
                    default:
                        return "";
                }
            }
            set { }

        }
    }
}
