using ControleDeEstacionamento.Transporte.ViewModels.Base;

namespace ControleDeEstacionamento.Transporte.ViewModels
{
    public class EstacionamentoViewModel : ViewModel
    {
        public string Placa { get; set; }
        public string HorarioChegada { get; set; }
        public string HorarioSaida { get; set; }
        public string Duracao { get; set; }
        public double TempoCobrado { get; set; }
        public decimal Preco { get; set; }
        public decimal ValorAPagar { get; set; }
    }
}
