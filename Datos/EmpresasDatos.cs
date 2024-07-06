using GestionContratos.Models;
using GestionContratos.Utils;
using System.Data.SqlClient;
using System.Data;

namespace GestionContratos.Datos
{
    public class EmpresasDatos
    {
        public async Task<List<EmpresaModel>> GetAllEmpresas()
        {

            var oLista = new List<EmpresaModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_GetAllEmpresas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        oLista.Add(new EmpresaModel()
                        {
                            EmpresaID = (int)dr["EmpresaID"],
                            RUC = (string)dr["RUC"],
                            RazonSocial = (string)dr["RazonSocial"],
                        });
                    }
                }
            }
            return oLista;
        }
    }
}
