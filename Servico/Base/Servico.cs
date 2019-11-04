using System;
using System.Collections.Generic;
using System.Linq;
using ControleDeEstacionamento.Dominio.Entidades.Base;
using ControleDeEstacionamento.Infraestrutura.Extensions;
using ControleDeEstacionamento.Persistencia;
using ControleDeEstacionamento.Transporte.ViewModels.Base;
using Microsoft.EntityFrameworkCore;

namespace ControleDeEstacionamento.Servico.Base
{
    public abstract class Servico<T, V>
        where T : Entidade
        where V : ViewModel
    {
        protected Context Contexto { get; }

        public Servico(Context contexto)
        {
            Contexto = contexto;
        }

        public virtual T ObterPorId(long id)
        {
            return Contexto.ObterEntidadePorId<T>(id);
        }

        public virtual IQueryable<T> ObterTodos()
        {
            return Contexto.Set<T>();
        }

        public virtual long Salvar(
            V viewModel,
            Func<IEnumerable<string>> metodoParaValidarViewModel,
            Func<V, T, T> metodoParaTransformarViewModelEmModel)
        {
            if (metodoParaValidarViewModel == null)
            {
                throw new ArgumentNullException(nameof(metodoParaValidarViewModel));
            }
            metodoParaValidarViewModel().ThrowRegrasException();

            return ExecutarSalvar(viewModel, metodoParaTransformarViewModelEmModel);
        }

        protected long ExecutarIncluir(T entidade)
        {
            if (entidade == null)
            {
                throw new ArgumentNullException(nameof(entidade));
            }
            Contexto.Incluir(entidade);
            Contexto.SaveChanges();
            return entidade.Id;
        }

        protected virtual T ObterEntidadeParaAlterar(long id)
        {
            return Contexto.ObterEntidadePorId<T>(id);
        }

        private long ExecutarSalvar(
            V viewModel,
            Func<V, T, T> metodoParaTransformarViewModelEmModel)
        {
            return viewModel.Id > 0 ?
                Alterar(viewModel, metodoParaTransformarViewModelEmModel)
                : Incluir(viewModel, metodoParaTransformarViewModelEmModel);
        }

        private long Incluir(V viewModel,
            Func<V, T, T> metodoParaTransformarViewModelEmModel)
        {
            T entidade = Activator.CreateInstance<T>();
            entidade = metodoParaTransformarViewModelEmModel(viewModel, entidade);
            return ExecutarIncluir(entidade);
        }

        private long Alterar(
            V viewModel,
            Func<V, T, T> metodoParaTransformarViewModelEmModel)
        {
            T entidadeParaAlterar = ObterEntidadeParaAlterar(viewModel.Id);

            return VerificaSeEntidadeFoiAlteradaEExecutaAlteracao(metodoParaTransformarViewModelEmModel(viewModel, entidadeParaAlterar));
        }

        private long VerificaSeEntidadeFoiAlteradaEExecutaAlteracao(T entidade)
        {
            bool foiAlterada = Contexto.Entry(entidade).State == EntityState.Modified;
            return foiAlterada ? ExecutarAlteracao(entidade) : entidade.Id;
        }

        private long ExecutarAlteracao(T entidade)
        {
            Contexto.Alterar(entidade);
            Contexto.SaveChanges();
            return entidade.Id;
        }
    }
}
