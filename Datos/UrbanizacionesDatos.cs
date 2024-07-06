using GestionContratos.Models;
using GestionContratos.Utils;
using System.Data.SqlClient;
using System.Data;

namespace GestionContratos.Datos
{
    public class UrbanizacionesDatos
    {
        public async Task<List<UrbanizacionModel>> GetAllUrbanizaciones()
        {

            var oLista = new List<UrbanizacionModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_GetAllUrbanizaciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        oLista.Add(new UrbanizacionModel()
                        {
                            UrbanizacionID = (int)dr["UrbanizacionID"],
		                    Nombre = (string)dr["Nombre"],
                            Departamento = (string)dr["Departamento"],
                            Provincia = (string)dr["Provincia"],
                            Distrito = (string)dr["Distrito"]
                        });
                    }
                }
            }
            return oLista;
        }
    }
}
