namespace ControleDeEstacionamento.Transporte.Response
{
    public class ValorResponse<T>
    {
        public T Valor { get; }

        public ValorResponse(T valor)
        {
            Valor = valor;
        }
    }
}
