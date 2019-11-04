using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ControleDeEstacionamento.Infraestrutura.Extensions
{
    public static class ExceptionExtensions
    {
        public static void ThrowRegrasException(this IEnumerable<string> erros)
        {
            if (erros.Any())
                throw new ValidationException(string.Join(";", erros));
        }
    }
}
