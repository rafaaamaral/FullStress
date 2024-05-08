using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entidade
{
    public class InscritosStreamer : BaseEntidade
    {
        public string NomeUsuario { get; set; }
        public DateTime DataInscricao { get; set; }
        public string CurrentTier { get; set; }
        public int TempoInscricao { get; set; }
        public int Sequencial { get; set; }
        public string TipoInscricao { get; set; }
        public int IdStreamer { get; set; }

        [ForeignKey("IdStreamer")]
        public virtual Usuario Streamer { get; set; }
    }
}
