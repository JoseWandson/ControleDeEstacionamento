using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstacionamento.Transporte.Requests
{
    public class EstacionamentoSaidaRequest
    {
        public int Id { get; set; }
        public string HorarioSaida { get; set; }
    }
}
