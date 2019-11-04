using System;
using System.Collections.Generic;
using System.Linq;
using ControleDeEstacionamento.Dominio.Entidades;
using ControleDeEstacionamento.Dominio.Mensagens;
using ControleDeEstacionamento.Infraestrutura.Extensions;
using ControleDeEstacionamento.Transporte.ViewModels;

namespace ControleDeEstacionamento.Dominio.Regras
{
    public static class PrecoRegras
    {
        public static IEnumerable<string> ValidarParaSalvar(PrecoViewModel viewModel, IQueryable<Preco> precos)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            if (viewModel.Valor == 0)
            {
                yield return Mensagem.ParametroObrigatorio.Formatar(Termo.Valor);
            }
            else if (viewModel.Valor < 0)
            {
                yield return Mensagem.ValorMenorQueZero.Formatar(Termo.Valor);
            }

            DateTime? _dataInicio = viewModel.DataInicio.ConverterParaData();
            DateTime? _dataFim = viewModel.DataFim.ConverterParaData();
            if (string.IsNullOrWhiteSpace(viewModel.DataInicio))
            {
                yield return Mensagem.ParametroObrigatorio.Formatar(Termo.DataInicio);
            }
            else if (!_dataInicio.HasValue)
            {
                yield return Mensagem.ParametroInvalido.Formatar(Termo.DataInicio);
            }
            else if (string.IsNullOrWhiteSpace(viewModel.DataFim))
            {
                yield return Mensagem.ParametroObrigatorio.Formatar(Termo.DataFim);
            }
            else if (!_dataFim.HasValue)
            {
                yield return Mensagem.ParametroInvalido.Formatar(Termo.DataInicio);
            }
            else if (DateTime.Compare(_dataInicio.Value, _dataFim.Value) > 0)
            {
                yield return Mensagem.DataInicioMaiorQueDataFim.Formatar(Termo.DataInicio, Termo.DataFim);
            }
            else if (PeriodoInvalido(precos, _dataInicio, _dataFim))
            {
                yield return Mensagem.PeriodoInvalido;
            }


        }

        private static bool PeriodoInvalido(IQueryable<Preco> precos, DateTime? _dataInicio, DateTime? _dataFim)
        {
            return precos.Any(preco =>
                (DateTime.Compare(_dataInicio.Value, preco.DataInicio) >= 0 && DateTime.Compare(_dataInicio.Value, preco.DataFim) <= 0)
                    || DateTime.Compare(_dataFim.Value, preco.DataInicio) >= 0 && DateTime.Compare(_dataFim.Value, preco.DataFim) <= 0);
        }
    }
}
