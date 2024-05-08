using Global.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CoachDTO : BaseDTO
    {
        public int IdPlayer { get; set; }
        public UsuarioDTO Player { get; set; }
        public string Titulo { get; set; }
        public string LinkVideo { get; set; }
        public string ObservacaoPlayer { get; set; }
        public string FeedbackCoach { get; set; }
        public int? Status { get; set; }

        public string DescricaoStatus
        {
            get
            {
                switch (Status)
                {
                    case (int)StatusCoach.Solicitado:
                        return "Solicitado";
                    case (int)StatusCoach.Respondido:
                        return "Respondido";
                    default:
                        return "";
                }
            }
            set { }

        }
    }
}
