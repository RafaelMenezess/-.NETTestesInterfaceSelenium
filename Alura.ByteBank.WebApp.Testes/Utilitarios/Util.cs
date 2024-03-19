using System.IO;
using System.Reflection;

namespace Alura.ByteBank.WebApp.Testes.Utilitarios
{
    public static class Util
    {
        public static string CaminhoDriverNavegador() => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
}
