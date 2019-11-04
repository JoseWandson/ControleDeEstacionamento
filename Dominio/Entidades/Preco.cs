using System;
using ControleDeEstacionamento.Dominio.Entidades.Base;

namespace ControleDeEstacionamento.Dominio.Entidades
{
    public class Preco : Entidade
    {
        public decimal Valor { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}
