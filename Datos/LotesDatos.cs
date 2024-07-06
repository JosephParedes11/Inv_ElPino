using GestionContratos.Models;
using GestionContratos.Utils;
using System.Data.SqlClient;
using System.Data;

namespace GestionContratos.Datos
{
    public class LotesDatos
    {
        public async Task<List<LoteModel>> GetAllLotes()
        {

            var oLista = new List<LoteModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_GetAllLotes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        oLista.Add(new LoteModel()
                        {
                            LoteID = (int)dr["LoteID"],
                            UrbanizacionID = (int)dr["UrbanizacionID"],
                            Manzana = (string)dr["Manzana"],
                            Letra = (string)dr["Letra"],
                            MetrosCuadrados = (decimal)dr["MetrosCuadrados"],
                            PrecioPorMetro = (decimal)dr["PrecioPorMetro"],
                            Urbanizacion = new UrbanizacionModel
                            {
                                Nombre = (string)dr["UrbanizacionNombre"]
                            }
                        });
                    }
                }
            }
            return oLista;
        }

        public async Task<List<LoteModel>> GetLotesPaginados(int paginaActual, int tamanoPagina)
        {

            var oLista = new List<LoteModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_GetLotesPaginados", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PaginaActual", paginaActual);
                cmd.Parameters.AddWithValue("@TamanoPagina", tamanoPagina);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        oLista.Add(new LoteModel()
                        {
                            LoteID = (int)dr["LoteID"],
                            UrbanizacionID = (int)dr["UrbanizacionID"],
                            Manzana = (string)dr["Manzana"],
                            Letra = (string)dr["Letra"],
                            MetrosCuadrados = (decimal)dr["MetrosCuadrados"],
                            PrecioPorMetro = (decimal)dr["PrecioPorMetro"],
                            Urbanizacion = new UrbanizacionModel
                            {
                                Nombre = (string)dr["UrbanizacionNombre"]
                            }
                        });
                    }
                }
            }
            return oLista;
        }

        public async Task<int> GetTotalLotes()
        {
            int totalRegistros = 0;

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                await conexion.OpenAsync();

                using (var cmd = new SqlCommand("SP_GetTotalLotes", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        if (await dr.ReadAsync())
                        {
                            totalRegistros = (int)dr["TotalRegistros"];
                        }
                    }

                }
            }

            return totalRegistros;
        }

    }
}
