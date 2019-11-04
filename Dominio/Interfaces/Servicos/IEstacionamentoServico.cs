using ControleDeEstacionamento.Dominio.Entidades;
using ControleDeEstacionamento.Dominio.Interfaces.Base;
using ControleDeEstacionamento.Transporte.Requests;
using ControleDeEstacionamento.Transporte.ViewModels;

namespace ControleDeEstacionamento.Dominio.Interfaces.Servicos
{
    public interface IEstacionamentoServico : IServico<Estacionamento, EstacionamentoViewModel>
    {
        long Entrada(EstacionamentoEntradaRequest viewModel);
        long Saida(EstacionamentoSaidaRequest viewModel);
    }
}
