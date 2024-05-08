using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entidade
{
    public class WeekendLeague : BaseEntidade
    {
        public int IdVersao { get; set; }

        [ForeignKey("IdVersao")]
        public virtual Versao Versao { get; set; }

        public int IdClube { get; set; }

        [ForeignKey("IdClube")]
        public virtual Clube Clube { get; set; }

        public string Descricao { get; set; }

        public int Progresso { get; set; }

        public int IdClassificacao { get; set; }

        [ForeignKey("IdClassificacao")]
        public virtual ClassificacaoWeekendLeague Classificacao { get; set; }

        public bool Top100 { get; set; }

        public int Status { get; set; }

        public int Aproveitamento { get; set; }

        public int? IdArtilheiro { get; set; }

        [ForeignKey("IdArtilheiro")]
        public virtual Jogador Artilheiro { get; set; }

        public int? IdLiderAssistencia { get; set; }

        [ForeignKey("IdLiderAssistencia")]
        public virtual Jogador LiderAssistencia { get; set; }

        public int PosseDeBola { get; set; }

        public int PrecisaoPasse { get; set; }

        public int? IdMelhorJogador { get; set; }

        [ForeignKey("IdMelhorJogador")]
        public virtual Jogador MelhorJogador { get; set; }
    }
}
