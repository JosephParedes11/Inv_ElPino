using System.IO;

namespace GestionContratos.Utils
{
    public class Conexion
    {
        private string cadenaSql = string.Empty;

        public Conexion()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            cadenaSql = builder.GetSection("ConnectionStrings:DbConnection").Value;
        }

        public string getCadenaSql()
        {
            return cadenaSql;
        }
    }
}
