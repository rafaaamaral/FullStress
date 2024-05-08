using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entidade
{
    public class Modulo : BaseEntidade
    {
        [Required]
        [MaxLength(200)]
        public string Descricao { get; set; }

        public string Icone { get; set; }
    }
}
