using System;
using System.Collections.Generic;
using System.Linq;
using ControleDeEstacionamento.Dominio.Entidades.Base;
using ControleDeEstacionamento.Transporte.ViewModels.Base;

namespace ControleDeEstacionamento.Dominio.Interfaces.Base
{
    public interface IServico<T, V>
        where T : Entidade
        where V : ViewModel
    {
        T ObterPorId(long id);
        IQueryable<T> ObterTodos();
        long Salvar(V viewModel, Func<IEnumerable<string>> metodoParaValidarViewModel, Func<V, T, T> metodoParaTransformarViewModelEmModel);
    }
}
