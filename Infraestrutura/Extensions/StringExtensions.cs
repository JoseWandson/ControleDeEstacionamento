using System;
using System.Globalization;

namespace ControleDeEstacionamento.Infraestrutura.Extensions
{
    public static class StringExtensions
    {
        public static string Formatar(this string texto, params string[] termo)
        {
            return string.Format(CultureInfo.CurrentCulture, texto, termo);
        }

        public static DateTime? ConverterParaData(this string texto)
        {
            if (!string.IsNullOrWhiteSpace(texto) &&
                DateTime.TryParseExact(texto, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime resultado))
            {
                return resultado;
            }
            return null;
        }

        public static string ConverterDataParaTexto(this DateTime data)
        {
            return data != null ? data.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : string.Empty;
        }

        public static string ConverterDataCompletaParaTexto(this DateTime data)
        {
            return data != null ? data.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture) : string.Empty;
        }

        public static string ConverterHoraParaTexto(this DateTime data)
        {
            return data != null ? data.ToString("HH:mm:ss", CultureInfo.InvariantCulture) : string.Empty;
        }
    }
}
