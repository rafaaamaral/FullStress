using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entidade
{
    public class Coach : BaseEntidade
    {
        [Required]
        public int IdPlayer { get; set; }

        [ForeignKey("IdPlayer")]
        public virtual Usuario Player { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string LinkVideo { get; set; }

        public string ObservacaoPlayer { get; set; }

        public string FeedbackCoach { get; set; }

        public int Status { get; set; }
    }
}
