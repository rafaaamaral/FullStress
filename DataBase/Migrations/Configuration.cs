using DataBase.Entidade;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Migrations
{
    public class Configuration : DbMigrationsConfiguration<Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(Context context)
        {
            //SeedPerfil(context);            
        }

        private void SeedPerfil(Context context)
        {
            context.Perfil.AddOrUpdate(x => x.Id,
                new Perfil() { Id = 1, Nome = "Administrador Geral", Descricao = "Administrador do Site", Ativo = true, DataCadastro = DateTime.Now, DataDesativado = null },
                new Perfil() { Id = 2, Nome = "Administrador Cliente", Descricao = "Administrador do Cliente", Ativo = true, DataCadastro = DateTime.Now, DataDesativado = null },
                new Perfil() { Id = 3, Nome = "Usuario", Descricao = "Usuario", Ativo = true, DataCadastro = DateTime.Now, DataDesativado = null }
                );
        }
    }
}
