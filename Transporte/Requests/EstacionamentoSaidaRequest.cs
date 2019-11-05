using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstacionamento.Transporte.Requests
{
    public class EstacionamentoSaidaRequest
    {
        public long Id { get; set; }
        public string HorarioSaida { get; set; }
    }
}
