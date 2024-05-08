using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class InscritosStreamerDTO : BaseDTO
    {
        public string NomeUsuario { get; set; }
        public DateTime DataInscricao { get; set; }
        public string CurrentTier { get; set; }
        public int TempoInscricao { get; set; }
        public int Sequencial { get; set; }
        public string TipoInscricao { get; set; }
        public int IdStreamer { get; set; }
        public virtual UsuarioDTO Streamer { get; set; }
    }
}
