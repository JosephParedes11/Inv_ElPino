using GestionContratos.Models;
using GestionContratos.Utils;
using System.Data;
using System.Data.SqlClient;

namespace GestionContratos.Datos
{
    public class ClientesDatos
    {
        public async Task<List<ClienteModel>> GetAllClientes()
        {

            var oLista = new List<ClienteModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_GetAllClientes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        oLista.Add(new ClienteModel()
                        {
                            ClienteID = (int)dr["ClienteID"],
                            Nombres = (String)dr["Nombres"],
                            Apellidos = (String)dr["Apellidos"],
                            DNI = (String)dr["DNI"],
                            Correo = (String)dr["Correo"],
                            Direccion = (String)dr["Direccion"],
                            Telefono = (String)dr["Telefono"]
                        });
                    }
                }
            }
            return oLista;
        }
    }
}
