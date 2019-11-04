using ControleDeEstacionamento.Transporte.ViewModels.Base;

namespace ControleDeEstacionamento.Transporte.ViewModels
{
    public class PrecoViewModel : ViewModel
    {
        public decimal Valor { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
    }
}
