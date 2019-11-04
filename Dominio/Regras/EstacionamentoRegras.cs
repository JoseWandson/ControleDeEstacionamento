using System;
using System.Collections.Generic;
using System.Linq;
using ControleDeEstacionamento.Dominio.Entidades;
using ControleDeEstacionamento.Dominio.Mensagens;
using ControleDeEstacionamento.Infraestrutura.Extensions;
using ControleDeEstacionamento.Transporte.Requests;

namespace ControleDeEstacionamento.Dominio.Regras
{
    public static class EstacionamentoRegras
    {
        public static IEnumerable<string> ValidarParaEntrar(EstacionamentoEntradaRequest request, IQueryable<Estacionamento> estacionamento)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (string.IsNullOrWhiteSpace(request.Placa))
            {
                yield return Mensagem.ParametroObrigatorio.Formatar(Termo.Placa);
            }
            else if (PossuiEntradaSemSaidadeDeVeiculo(estacionamento, request.Placa))
            {
                yield return Mensagem.EntidadeDuplicada.Formatar(Termo.Placa);
            }

            if (string.IsNullOrWhiteSpace(request.HorarioChegada))
            {
                yield return Mensagem.ParametroObrigatorio.Formatar(Termo.HorarioChegada);
            }
            else if (!DateTime.TryParse(request.HorarioChegada, out _))
            {
                yield return Mensagem.ParametroInvalido.Formatar(Termo.HorarioChegada);
            }
        }

        public static IEnumerable<string> ValidarParaSair(EstacionamentoSaidaRequest request, IQueryable<Estacionamento> estacionamento)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (request.Id == 0)
            {
                yield return Mensagem.ParametroObrigatorio.Formatar(Termo.Id);
            }

            if (string.IsNullOrWhiteSpace(request.HorarioSaida))
            {
                yield return Mensagem.ParametroObrigatorio.Formatar(Termo.HorarioSaida);
            }
            else if (!DateTime.TryParse(request.HorarioSaida, out _))
            {
                yield return Mensagem.ParametroInvalido.Formatar(Termo.HorarioSaida);
            }

            Estacionamento model = estacionamento.FirstOrDefault(e => e.Id == request.Id);
            if (model == null)
            {
                yield return Mensagem.EntidadeNaoEncontrada.Formatar(Termo.Placa);
            }
            else if (DateTime.Compare(model.HorarioChegada, DateTime.Now) > 0)
            {
                yield return Mensagem.HorarioChegadaMaiorQueHorarioSaida.Formatar(Termo.HorarioChegada, Termo.HorarioSaida);
            }
        }

        private static bool PossuiEntradaSemSaidadeDeVeiculo(IQueryable<Estacionamento> estacionamento, string placa)
        {
            return estacionamento.Any(e => e.HorarioSaida == null && e.Placa == placa);
        }
    }
}
