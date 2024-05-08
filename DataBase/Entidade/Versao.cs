using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entidade
{
    public class Versao : BaseEntidade
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public bool AtualProducao { get; set; }
    }
}
