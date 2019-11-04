using System;
using ControleDeEstacionamento.Dominio.Entidades.Base;

namespace ControleDeEstacionamento.Dominio.Entidades
{
    public class Estacionamento : Entidade
    {
        public string Placa { get; set; }
        public DateTime HorarioChegada { get; set; }
        public DateTime? HorarioSaida { get; set; }
        public DateTime? Duracao { get; set; }
        public double? TempoCobrado { get; set; }
        public decimal? Preco { get; set; }
        public decimal? ValorAPagar { get; set; }
    }
}
