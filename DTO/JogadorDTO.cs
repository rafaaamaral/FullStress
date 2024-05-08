using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class JogadorDTO : BaseDTO
    {
        public string Nome { get; set; }
        public int Overral { get; set; }
        public string Link { get; set; }
        public int IdTipoJogador { get; set; }
        public TipoJogadorDTO TipoJogador { get; set; }
        public int IdVersao { get; set; }
        public VersaoDTO Versao { get; set; }
    }
}
