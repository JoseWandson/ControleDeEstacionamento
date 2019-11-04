using System;
using System.Globalization;
using ControleDeEstacionamento.Dominio.Entidades;
using ControleDeEstacionamento.Infraestrutura.Extensions;
using ControleDeEstacionamento.Transporte.Requests;
using ControleDeEstacionamento.Transporte.ViewModels;

namespace ControleDeEstacionamento.Servico.ViewModelExtensions
{
    public static class EstacionamentoExtension
    {
        public static EstacionamentoViewModel TransformarEntradaRequestEmView(this EstacionamentoEntradaRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            return new EstacionamentoViewModel
            {
                Placa = request.Placa,
                HorarioChegada = request.HorarioChegada
            };
        }

        public static Estacionamento TransformarViewEmModel(this EstacionamentoViewModel viewModel, Estacionamento entidade)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }
            if (entidade == null)
            {
                throw new ArgumentNullException(nameof(entidade));
            }

            bool possuiChegada = DateTime.TryParse(viewModel.HorarioChegada, out DateTime chegada);
            bool possuiSaida = DateTime.TryParse(viewModel.HorarioSaida, out DateTime saida);
            bool possuiDuracao = DateTime.TryParse(viewModel.Duracao, out DateTime duracao);

            entidade.Id = viewModel.Id;
            entidade.Placa = viewModel.Placa;
            entidade.HorarioChegada = possuiChegada ? chegada : entidade.HorarioChegada;
            entidade.HorarioSaida = possuiSaida ? saida : (DateTime?)null;
            entidade.Duracao = possuiDuracao ? duracao : entidade.Duracao;
            entidade.TempoCobrado = viewModel.TempoCobrado;
            entidade.Preco = viewModel.Preco;
            entidade.ValorAPagar = viewModel.ValorAPagar;

            return entidade;
        }

        public static EstacionamentoViewModel TransformarSaidaRequestEmView(this EstacionamentoSaidaRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            return new EstacionamentoViewModel
            {
                Id = request.Id,
                HorarioSaida = request.HorarioSaida
            };
        }

        public static EstacionamentoViewModel TransformarModelEmView(this Estacionamento entidade)
        {
            if (entidade == null)
            {
                throw new ArgumentNullException(nameof(entidade));
            }

            return new EstacionamentoViewModel
            {
                Id = entidade.Id,
                Placa = entidade.Placa,
                HorarioChegada = entidade.HorarioChegada.ConverterDataCompletaParaTexto(),
                HorarioSaida = entidade.HorarioSaida?.ConverterDataCompletaParaTexto(),
                Duracao = entidade.Duracao?.ConverterHoraParaTexto(),
                TempoCobrado = entidade.TempoCobrado.GetValueOrDefault(),
                Preco = entidade.Preco.GetValueOrDefault(),
                ValorAPagar = entidade.ValorAPagar.GetValueOrDefault()
            };
        }
    }
}
