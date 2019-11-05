using System;
using ControleDeEstacionamento.Dominio.Entidades;
using ControleDeEstacionamento.Dominio.Entidades.Base;
using ControleDeEstacionamento.Dominio.Mensagens;
using Microsoft.EntityFrameworkCore;

namespace ControleDeEstacionamento.Persistencia
{
    public class Context : DbContext
    {
        public DbSet<Preco> Precos { get; set; }
        public DbSet<Estacionamento> Estacionamento { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }
            modelBuilder.Entity<Preco>().ToTable("Preco");
            modelBuilder.Entity<Estacionamento>().ToTable("Estacionamento");
        }

        public T Incluir<T>(T entidade) where T : Entidade
        {
            Set<T>().Add(entidade);
            return entidade;
        }

        public T Alterar<T>(T entidade) where T : Entidade
        {
            Entry(entidade).State = EntityState.Modified;
            return entidade;
        }

        public T ObterEntidadePorId<T>(long id) where T : Entidade
        {
            T entidade = Set<T>().Find(id);

            if (entidade == null)
            {
                throw new Exception(Mensagem.EntidadeNaoEncontrada);
            }

            return entidade;
        }

        public T Excluir<T>(T entidade) where T : Entidade
        {
            Set<T>().Remove(entidade);
            return entidade;
        }
    }
}
