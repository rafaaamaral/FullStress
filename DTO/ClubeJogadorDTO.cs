using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ClubeJogadorDTO : BaseDTO
    {
        public int IdClube { get; set; }
        public ClubeDTO Clube { get; set; }
        public int IdJogador { get; set; }
        public JogadorDTO Jogador { get; set; }
    }
}
