using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using ControleDeEstacionamento.Dominio.Entidades;
using ControleDeEstacionamento.Dominio.Interfaces.Servicos;
using ControleDeEstacionamento.Dominio.Regras;
using ControleDeEstacionamento.Infraestrutura.Extensions;
using ControleDeEstacionamento.Persistencia;
using ControleDeEstacionamento.Servico.Base;
using ControleDeEstacionamento.Servico.ViewModelExtensions;
using ControleDeEstacionamento.Transporte.Requests;
using ControleDeEstacionamento.Transporte.ViewModels;
using Microsoft.Extensions.Configuration;

namespace ControleDeEstacionamento.Servico.Servicos
{
    public class EstacionamentoServico : Servico<Estacionamento, EstacionamentoViewModel>, IEstacionamentoServico
    {
        private readonly IConfiguration Config;

        public EstacionamentoServico(Context contexto, IConfiguration config) : base(contexto)
        {
            Config = config;
        }

        public long Entrada(EstacionamentoEntradaRequest request)
        {
            Func<IEnumerable<string>> metodoParaValidarViewModel = (() => EstacionamentoRegras.ValidarParaEntrar(request, ObterTodos()));
            EstacionamentoViewModel viewModel = request.TransformarEntradaRequestEmView();
            return base.Salvar(viewModel, metodoParaValidarViewModel, EstacionamentoExtension.TransformarViewEmModel);
        }

        public long Saida(EstacionamentoSaidaRequest request)
        {
            Func<IEnumerable<string>> metodoParaValidarViewModel = (() => EstacionamentoRegras.ValidarParaSair(request, ObterTodos()));
            EstacionamentoViewModel viewModel = request.TransformarSaidaRequestEmView();
            viewModel = ObterValorAPagar(viewModel);
            return base.Salvar(viewModel, metodoParaValidarViewModel, EstacionamentoExtension.TransformarViewEmModel);
        }

        private EstacionamentoViewModel ObterValorAPagar(EstacionamentoViewModel viewModel)
        {
            if (DateTime.TryParse(viewModel.HorarioSaida, out DateTime saida))
            {
                Estacionamento estacionamento = Contexto.Estacionamento.FirstOrDefault(e => e.Id == viewModel.Id);
                Preco preco = Contexto.Precos.FirstOrDefault(p => DateTime.Now >= p.DataInicio && DateTime.Now <= p.DataFim);
                viewModel.Preco = preco.Valor > 0 ? preco.Valor : 2;

                double tolerancia = Convert.ToDouble(Config.GetSection("AppConfiguration")["TempoDeTolerancia"], CultureInfo.CurrentCulture);
                TimeSpan timeSpan = saida.Subtract(estacionamento.HorarioChegada);
                viewModel.Duracao = $"{timeSpan.TotalHours}:{timeSpan.Minutes}:{timeSpan.Seconds}";
                double duracao = timeSpan.TotalHours;
                double duracaoComTolerancia = duracao > 1 ? duracao - tolerancia : duracao;

                int y = (int)duracaoComTolerancia;
                if (0 < duracaoComTolerancia - y && duracaoComTolerancia - y <= 0.5)
                {
                    viewModel.TempoCobrado = y + 0.5;
                }
                else if (0.5 < duracaoComTolerancia - y)
                {
                    viewModel.TempoCobrado = y + 1;
                }

                viewModel.ValorAPagar = Convert.ToDecimal(viewModel.TempoCobrado) * viewModel.Preco;
            }

            return viewModel;
        }
    }
}
