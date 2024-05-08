using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TransacaoDTO : BaseDTO
    {
        public string Descricao { get; set; }
        public string URL { get; set; }
        public int? ParametroURL { get; set; }
    }
}
