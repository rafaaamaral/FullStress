using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ModuloTransacaoDTO : BaseDTO
    {
        public int IdModulo { get; set; }
        public virtual ModuloDTO Modulo { get; set; }
        public int IdTransacao { get; set; }
        public virtual TransacaoDTO Transacao { get; set; }
    }
}
