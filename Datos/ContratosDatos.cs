using GestionContratos.Models;
using GestionContratos.Utils;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Diagnostics.Contracts;

namespace GestionContratos.Datos
{
    public class ContratosDatos
    {
        public async Task<List<ContratoModel>> GetAllContratos()
        {

            var oLista = new List<ContratoModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_GetAllContratos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        oLista.Add(new ContratoModel()
                        {
                            ContratoID = (int)dr["ContratoID"],
                            Fecha = (DateTime)dr["Fecha"],
                            LoteID = (int)dr["LoteID"],
                            ClienteID = (int)dr["ClienteID"],
                            PagoTipo = (string)dr["PagoTipo"],
                            EmpresaID = (int)dr["EmpresaID"],
                            MontoTotal = (decimal)dr["MontoTotal"],
                            CuotaInicial = (decimal)dr["CuotaInicial"],
                            Igv = (decimal)dr["Igv"],

                            Lote = new LoteModel()
                            {
                                Manzana = (string)dr["ManzanaLote"],
                                Letra = (string)dr["LetraLote"],
                            },
                            Cliente = new ClienteModel()
                            {
                                DNI = (string)dr["DNICliente"],
                                Nombres = (string)dr["NombresCliente"],
                                Apellidos = (string)dr["ApellidosCliente"]
                            },
                            Empresa = new EmpresaModel()
                            {
                                RUC = (string)dr["RucEmpresa"],
                                RazonSocial = (string)dr["RazonSocialEmpresa"]
                            }
                        });
                    }
                }
            }
            return oLista;
        }

        public async Task<ContratoModel> GetContratoById(int ContratoID)
        {
            var contrato = new ContratoModel();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_GetContratoByID", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ContratoID", ContratoID);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {

                        contrato.ContratoID = (int)dr["ContratoID"];
                        contrato.Fecha = (DateTime)dr["Fecha"];
                        contrato.LoteID = (int)dr["LoteID"];
                        contrato.ClienteID = (int)dr["ClienteID"];
                        contrato.PagoTipo = (string)dr["PagoTipo"];
                        contrato.EmpresaID = (int)dr["EmpresaID"];
                        contrato.Igv = (decimal)dr["Igv"];
                        contrato.MontoTotal = (decimal)dr["MontoTotal"];
                        contrato.CuotaInicial = (decimal)dr["CuotaInicial"];
                    }
                }
            }

            return contrato;
        }

        public async Task<int> CreateContrato(ContratoModel model)
        {
            int insertId = 0;
           
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                await conexion.OpenAsync();

                using (var cmd = new SqlCommand("sp_insertContrato", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Fecha", model.Fecha);
                    cmd.Parameters.AddWithValue("@LoteID", model.LoteID);
                    cmd.Parameters.AddWithValue("@ClienteID", model.ClienteID);
                    cmd.Parameters.AddWithValue("@PagoTipo", model.PagoTipo);
                    cmd.Parameters.AddWithValue("@EmpresaID", model.EmpresaID);
                    cmd.Parameters.AddWithValue("@MontoTotal", model.MontoTotal);
                    cmd.Parameters.AddWithValue("@CuotaInicial", model.CuotaInicial);
                    cmd.Parameters.AddWithValue("@Igv", model.Igv);

                    var OutResult = new SqlParameter("@result", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(OutResult);

                    await cmd.ExecuteNonQueryAsync();

                    if (OutResult.Value != DBNull.Value)
                    {
                        insertId = Convert.ToInt32(OutResult.Value);
                    }
                }
            }

            return insertId;
        }

        public async Task<bool> UpdateContrato(ContratoModel model)
        {
            bool estado = false;
         
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                await conexion.OpenAsync();

                using (var cmd = new SqlCommand("SP_UpdateContrato", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ContratoID", model.ContratoID);
                    cmd.Parameters.AddWithValue("@LoteID", model.LoteID);
                    cmd.Parameters.AddWithValue("@ClienteID", model.ClienteID);
                    cmd.Parameters.AddWithValue("@PagoTipo", model.PagoTipo);
                    cmd.Parameters.AddWithValue("@EmpresaID", model.EmpresaID);
                    cmd.Parameters.AddWithValue("@MontoTotal", model.MontoTotal);
                    cmd.Parameters.AddWithValue("@CuotaInicial", model.CuotaInicial);
                    cmd.Parameters.AddWithValue("@Igv", model.Igv);

                    await cmd.ExecuteNonQueryAsync();
                    estado = true;
                }
            }
            return estado;
        }

        public async Task<bool> DeleteContrato(int ContratoID)
        {
            bool estado = false;

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                await conexion.OpenAsync();

                using (var cmd = new SqlCommand("SP_DeleteContrato", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ContratoID", ContratoID);

                    await cmd.ExecuteNonQueryAsync();
                }
                estado = true;
            }
           
            return estado;
        }

        public async Task<List<ContratoModel>> GetContratosPaginados(int paginaActual, int tamanoPagina)
        {

            var oLista = new List<ContratoModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_GetContratosPaginados", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PaginaActual", paginaActual);
                cmd.Parameters.AddWithValue("@TamanoPagina", tamanoPagina);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        oLista.Add(new ContratoModel()
                        {
                            ContratoID = (int)dr["ContratoID"],
                            Fecha = (DateTime)dr["Fecha"],
                            LoteID = (int)dr["LoteID"],
                            ClienteID = (int)dr["ClienteID"],
                            PagoTipo = (string)dr["PagoTipo"],
                            EmpresaID = (int)dr["EmpresaID"],
                            MontoTotal = (decimal)dr["MontoTotal"],
                            CuotaInicial = (decimal)dr["CuotaInicial"],
                            Igv = (decimal)dr["Igv"],

                            Lote = new LoteModel()
                            {
                                Manzana = (string)dr["ManzanaLote"],
                                Letra = (string)dr["LetraLote"],
                            },
                            Cliente = new ClienteModel()
                            {
                                DNI = (string)dr["DNICliente"],
                                Nombres = (string)dr["NombresCliente"],
                                Apellidos = (string)dr["ApellidosCliente"]
                            },
                            Empresa = new EmpresaModel()
                            {
                                RUC = (string)dr["RucEmpresa"],
                                RazonSocial = (string)dr["RazonSocialEmpresa"]
                            }
                        });
                    }
                }
            }
            return oLista;
        }

        public async Task<int> GetTotalContratos()
        {
            int totalRegistros = 0;

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                await conexion.OpenAsync();

                using (var cmd = new SqlCommand("SP_GetTotalContratos", conexion))
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
