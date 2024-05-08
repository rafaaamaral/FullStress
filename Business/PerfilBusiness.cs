using DataBase.Entidade;
using DTO;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class PerfilBusiness : BaseBusiness<PerfilDTO>
    {
        private readonly PerfilRepositorio _perfilRepositorio;

        public PerfilBusiness()
        {
            base.EntityType = typeof(Perfil);
            base.RepositoryType = typeof(PerfilRepositorio);
            //base.Includes = new[] { "" };
            _perfilRepositorio = new PerfilRepositorio();
        }
    }
}
