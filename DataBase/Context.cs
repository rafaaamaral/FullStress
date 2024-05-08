using DataBase.Entidade;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class Context : DbContext, IDisposable
    {
        public Context()
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<ClassificacaoWeekendLeague> ClassificacaoWeekendLeague { get; set; }
        public DbSet<ClienteTrade> ClienteTrade { get; set; }
        public DbSet<ClienteTradeFaturamento> ClienteTradeFaturamento { get; set; }
        public DbSet<Clube> Clube { get; set; }
        public DbSet<ClubeJogador> ClubeJogador { get; set; }
        public DbSet<Coach> Coach { get; set; }
        public DbSet<Erro> Erro { get; set; }
        public DbSet<InscritosStreamer> InscritosStreamer { get; set; }
        public DbSet<Jogador> Jogador { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<PerfilModuloTransacao> PerfilModuloTransacao { get; set; }
        public DbSet<Modulo> Modulo { get; set; }
        public DbSet<ModuloTransacao> ModuloTransacao { get; set; }
        public DbSet<Sessao> Sessao { get; set; }
        public DbSet<TipoJogador> TipoJogador { get; set; }
        public DbSet<Transacao> Transacao { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<UsuarioModuloTransacao> UsuarioModuloTransacao { get; set; }
        public DbSet<Versao> Versao { get; set; }
        public DbSet<WeekendLeague> WeekendLeague { get; set; }
        public DbSet<WeekendLeagueJogo> WeekendLeagueJogo { get; set; }
        public DbSet<WeekendLeagueJogoDetalhe> WeekendLeagueJogoDetalhe { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Perfil>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Modulo>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<TipoJogador>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Transacao>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Properties<string>().Configure(x => x.HasMaxLength(500).HasColumnType("varchar"));
            modelBuilder.Properties<decimal>().Configure(x => x.HasPrecision(18, 2));

        }
    }
}
