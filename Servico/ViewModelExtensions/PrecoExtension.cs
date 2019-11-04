using System;
using ControleDeEstacionamento.Dominio.Entidades;
using ControleDeEstacionamento.Infraestrutura.Extensions;
using ControleDeEstacionamento.Transporte.ViewModels;

namespace ControleDeEstacionamento.Servico.ViewModelExtensions
{
    public static class PrecoExtension
    {
        public static Preco TransformarViewEmModel(this PrecoViewModel viewModel, Preco entidade)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }
            if (entidade == null)
            {
                throw new ArgumentNullException(nameof(entidade));
            }

            bool possuiInicio = DateTime.TryParse(viewModel.DataInicio, out DateTime inicio);
            bool possuiFim = DateTime.TryParse(viewModel.DataFim, out DateTime fim);

            entidade.Id = viewModel.Id;
            entidade.Valor = viewModel.Valor;
            entidade.DataInicio = possuiInicio ? inicio : entidade.DataInicio;
            entidade.DataFim = possuiFim ? fim : entidade.DataFim;

            return entidade;
        }

        public static PrecoViewModel TransformarModelEmView(this Preco entidade)
        {
            if (entidade == null)
            {
                throw new ArgumentNullException(nameof(entidade));
            }
            return new PrecoViewModel
            {
                Id = entidade.Id,
                DataInicio = entidade.DataInicio.ConverterDataParaTexto(),
                DataFim = entidade.DataFim.ConverterDataParaTexto()
            };
        }
    }
}
