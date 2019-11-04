using System;
using System.Collections.Generic;
using ControleDeEstacionamento.Dominio.Entidades;
using ControleDeEstacionamento.Dominio.Interfaces.Servicos;
using ControleDeEstacionamento.Dominio.Regras;
using ControleDeEstacionamento.Persistencia;
using ControleDeEstacionamento.Servico.Base;
using ControleDeEstacionamento.Servico.ViewModelExtensions;
using ControleDeEstacionamento.Transporte.ViewModels;

namespace ControleDeEstacionamento.Servico.Servicos
{
    public class PrecoServico : Servico<Preco, PrecoViewModel>, IPrecoServico
    {
        public PrecoServico(Context contexto) : base(contexto)
        {
        }

        public long Salvar(PrecoViewModel viewModel)
        {
            Func<IEnumerable<string>> metodoParaValidarViewModel = (() => PrecoRegras.ValidarParaSalvar(viewModel, ObterTodos()));
            return base.Salvar(viewModel, metodoParaValidarViewModel, PrecoExtension.TransformarViewEmModel);
        }

        public virtual long Excluir(long id)
        {
            Preco entidade = ObterEntidadeParaExcluir(id);
            return ExecutarExclusao(entidade);
        }

        private Preco ObterEntidadeParaExcluir(long id)
        {
            return Contexto.ObterEntidadePorId<Preco>(id);
        }

        private long ExecutarExclusao(Preco entidadeParaExcluir)
        {
            Contexto.Excluir(entidadeParaExcluir);
            Contexto.SaveChanges();
            return entidadeParaExcluir.Id;
        }
    }
}
