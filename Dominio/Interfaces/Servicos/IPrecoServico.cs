using ControleDeEstacionamento.Dominio.Entidades;
using ControleDeEstacionamento.Dominio.Interfaces.Base;
using ControleDeEstacionamento.Transporte.ViewModels;

namespace ControleDeEstacionamento.Dominio.Interfaces.Servicos
{
    public interface IPrecoServico : IServico<Preco, PrecoViewModel>
    {
        long Salvar(PrecoViewModel viewModel);
        long Excluir(long id);
    }
}
