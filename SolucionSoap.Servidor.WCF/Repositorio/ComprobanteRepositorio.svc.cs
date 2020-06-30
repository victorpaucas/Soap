using SolucionSoap.Servidor.WCF.Consulta;
using SolucionSoap.Servidor.WCF.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SolucionSoap.Servidor.WCF.Repositorio
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ComprobanteRepositorio" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ComprobanteRepositorio.svc or ComprobanteRepositorio.svc.cs at the Solution Explorer and start debugging.
    public class ComprobanteRepositorio : IComprobanteRepositorio
    {
        public ActualizarRespuestaModelo Actualizar(ComprobanteModelo comprobanteModelo)
        {
            ActualizarRespuestaModelo actualizarRespuestaModelo = new ActualizarRespuestaModelo();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConexionConsulta.cadenaConexion))
                {
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand
                    {
                        CommandText = ConexionConsulta.ComprobanteActualizar,
                        CommandType = CommandType.StoredProcedure,
                        Connection = sqlConnection
                    };

                    sqlCommand.Parameters.AddWithValue("@IdentificadorComprobante", comprobanteModelo.IdentificadorComprobante);
                    sqlCommand.Parameters.AddWithValue("@TipoComprobante", comprobanteModelo.TipoComprobante);
                    sqlCommand.Parameters.AddWithValue("@VendedorComprobante", comprobanteModelo.VendedorComprobante);
                    sqlCommand.Parameters.AddWithValue("@ClienteComprobante", comprobanteModelo.ClienteComprobante);
                    sqlCommand.Parameters.AddWithValue("@FechaComprobante", comprobanteModelo.FechaComprobante);
                    sqlCommand.Parameters.AddWithValue("@DescuentoComprobante", comprobanteModelo.DescuentoComprobante);
                    sqlCommand.Parameters.AddWithValue("@ImpuestoComprobante", comprobanteModelo.ImpuestoComprobante);
                    sqlCommand.Parameters.AddWithValue("@SubTotalComprobante", comprobanteModelo.SubTotalComprobante);
                    sqlCommand.Parameters.AddWithValue("@TotalComprobante", comprobanteModelo.TotalComprobante);

                    SqlParameter mensajeRespuesta = sqlCommand.Parameters.Add("@MensajeRespuesta", SqlDbType.VarChar, 100);
                    mensajeRespuesta.Direction = ParameterDirection.Output;
                    SqlParameter errorRepsuesta = sqlCommand.Parameters.Add("@ErrorRespuesta", SqlDbType.Bit);
                    errorRepsuesta.Direction = ParameterDirection.Output;

                    sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();

                    actualizarRespuestaModelo.MensajeRespuesta = (string)mensajeRespuesta.Value;
                    actualizarRespuestaModelo.ErrorRespuesta = (bool)errorRepsuesta.Value;
                }
            }
            catch (Exception exception)
            {
                actualizarRespuestaModelo.MensajeRespuesta = exception.ToString();
                actualizarRespuestaModelo.ErrorRespuesta = true;
            }

            return actualizarRespuestaModelo;
        }

        public EliminarRespuestaModelo Eliminar(int IdentificadorComprobante)
        {
            EliminarRespuestaModelo eliminarRespuestaModelo = new EliminarRespuestaModelo();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConexionConsulta.cadenaConexion))
                {
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand
                    {
                        CommandText = ConexionConsulta.ComprobanteEliminar,
                        CommandType = CommandType.StoredProcedure,
                        Connection = sqlConnection
                    };

                    sqlCommand.Parameters.AddWithValue("@IdentificadorComprobante", IdentificadorComprobante);

                    SqlParameter mensajeRespuesta = sqlCommand.Parameters.Add("@MensajeRespuesta", SqlDbType.VarChar, 100);
                    mensajeRespuesta.Direction = ParameterDirection.Output;
                    SqlParameter errorRepsuesta = sqlCommand.Parameters.Add("@ErrorRespuesta", SqlDbType.Bit);
                    errorRepsuesta.Direction = ParameterDirection.Output;

                    sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();

                    eliminarRespuestaModelo.MensajeRespuesta = (string)mensajeRespuesta.Value;
                    eliminarRespuestaModelo.ErrorRespuesta = (bool)errorRepsuesta.Value;
                }
            }
            catch (Exception exception)
            {
                eliminarRespuestaModelo.MensajeRespuesta = exception.ToString();
                eliminarRespuestaModelo.ErrorRespuesta = true;
            }

            return eliminarRespuestaModelo;
        }

        public ListarRespuestaModelo<ComprobanteModelo> Listar()
        {
            ListarRespuestaModelo<ComprobanteModelo> listarRespuestaModelo = new ListarRespuestaModelo<ComprobanteModelo>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConexionConsulta.cadenaConexion))
                {
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand
                    {
                        CommandText = ConexionConsulta.ComprobanteListar,
                        CommandType = CommandType.StoredProcedure,
                        Connection = sqlConnection
                    };

                    SqlParameter mensajeRespuesta = sqlCommand.Parameters.Add("@MensajeRespuesta", SqlDbType.VarChar, 100);
                    mensajeRespuesta.Direction = ParameterDirection.Output;
                    SqlParameter errorRepsuesta = sqlCommand.Parameters.Add("@ErrorRespuesta", SqlDbType.Bit);
                    errorRepsuesta.Direction = ParameterDirection.Output;

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    List<ComprobanteModelo> listaComprobanteModelo = new List<ComprobanteModelo>();

                    while (sqlDataReader.Read())
                    {
                        ComprobanteModelo comprobanteModelo = new ComprobanteModelo
                        {
                            IdentificadorComprobante = (int)sqlDataReader["IdentificadorComprobante"],
                            TipoComprobante = (string)sqlDataReader["TipoComprobante"],
                            VendedorComprobante = (string)sqlDataReader["VendedorComprobante"],
                            ClienteComprobante = (string)sqlDataReader["ClienteComprobante"],
                            FechaComprobante = (DateTime)sqlDataReader["FechaComprobante"],
                            DescuentoComprobante = (decimal)sqlDataReader["DescuentoComprobante"],
                            ImpuestoComprobante = (decimal)sqlDataReader["ImpuestoComprobante"],
                            SubTotalComprobante = (decimal)sqlDataReader["SubTotalComprobante"],
                            TotalComprobante = (decimal)sqlDataReader["TotalComprobante"]
                        };

                        listaComprobanteModelo.Add(comprobanteModelo);
                    }

                    sqlConnection.Close();

                    listarRespuestaModelo.ListaRespuesta = listaComprobanteModelo;
                    listarRespuestaModelo.MensajeRespuesta = (string)mensajeRespuesta.Value;
                    listarRespuestaModelo.ErrorRespuesta = (bool)errorRepsuesta.Value;
                }
            }
            catch (Exception exception)
            {
                listarRespuestaModelo.ListaRespuesta = null;
                listarRespuestaModelo.MensajeRespuesta = exception.ToString();
                listarRespuestaModelo.ErrorRespuesta = true;
            }

            return listarRespuestaModelo;
        }

        public ObtenerRespuestaModelo<ComprobanteModelo> Obtener(int IdentificadorComprobante)
        {
            ObtenerRespuestaModelo<ComprobanteModelo> obtenerRespuestaModelo = new ObtenerRespuestaModelo<ComprobanteModelo>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConexionConsulta.cadenaConexion))
                {
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand
                    {
                        CommandText = ConexionConsulta.ComprobanteObtener,
                        CommandType = CommandType.StoredProcedure,
                        Connection = sqlConnection
                    };

                    sqlCommand.Parameters.AddWithValue("@IdentificadorComprobante", IdentificadorComprobante);

                    SqlParameter mensajeRespuesta = sqlCommand.Parameters.Add("@MensajeRespuesta", SqlDbType.VarChar, 100);
                    mensajeRespuesta.Direction = ParameterDirection.Output;
                    SqlParameter errorRepsuesta = sqlCommand.Parameters.Add("@ErrorRespuesta", SqlDbType.Bit);
                    errorRepsuesta.Direction = ParameterDirection.Output;

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    ComprobanteModelo obtenerComprobanteModelo = new ComprobanteModelo();

                    while (sqlDataReader.Read())
                    {
                        obtenerComprobanteModelo.IdentificadorComprobante = (int)sqlDataReader["IdentificadorComprobante"];
                        obtenerComprobanteModelo.TipoComprobante = (string)sqlDataReader["TipoComprobante"];
                        obtenerComprobanteModelo.VendedorComprobante = (string)sqlDataReader["VendedorComprobante"];
                        obtenerComprobanteModelo.ClienteComprobante = (string)sqlDataReader["ClienteComprobante"];
                        obtenerComprobanteModelo.FechaComprobante = (DateTime)sqlDataReader["FechaComprobante"];
                        obtenerComprobanteModelo.DescuentoComprobante = (decimal)sqlDataReader["DescuentoComprobante"];
                        obtenerComprobanteModelo.ImpuestoComprobante = (decimal)sqlDataReader["ImpuestoComprobante"];
                        obtenerComprobanteModelo.SubTotalComprobante = (decimal)sqlDataReader["SubTotalComprobante"];
                        obtenerComprobanteModelo.TotalComprobante = (decimal)sqlDataReader["TotalComprobante"];
                    }

                    sqlConnection.Close();

                    obtenerRespuestaModelo.ModeloRespuesta = obtenerComprobanteModelo;
                    obtenerRespuestaModelo.MensajeRespuesta = (string)mensajeRespuesta.Value;
                    obtenerRespuestaModelo.ErrorRespuesta = (bool)errorRepsuesta.Value;
                }
            }
            catch (Exception exception)
            {
                obtenerRespuestaModelo.ModeloRespuesta = null;
                obtenerRespuestaModelo.MensajeRespuesta = exception.ToString();
                obtenerRespuestaModelo.ErrorRespuesta = true;
            }

            return obtenerRespuestaModelo;
        }

        public RegistrarRespuestaModelo Registrar(ComprobanteModelo comprobanteModelo)
        {
            RegistrarRespuestaModelo registrarRespuestaModelo = new RegistrarRespuestaModelo();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConexionConsulta.cadenaConexion))
                {
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand
                    {
                        CommandText = ConexionConsulta.ComprobanteRegistrar,
                        CommandType = CommandType.StoredProcedure,
                        Connection = sqlConnection
                    };

                    sqlCommand.Parameters.AddWithValue("@TipoComprobante", comprobanteModelo.TipoComprobante);
                    sqlCommand.Parameters.AddWithValue("@VendedorComprobante", comprobanteModelo.VendedorComprobante);
                    sqlCommand.Parameters.AddWithValue("@ClienteComprobante", comprobanteModelo.ClienteComprobante);
                    sqlCommand.Parameters.AddWithValue("@FechaComprobante", comprobanteModelo.FechaComprobante);
                    sqlCommand.Parameters.AddWithValue("@DescuentoComprobante", comprobanteModelo.DescuentoComprobante);
                    sqlCommand.Parameters.AddWithValue("@ImpuestoComprobante", comprobanteModelo.ImpuestoComprobante);
                    sqlCommand.Parameters.AddWithValue("@SubTotalComprobante", comprobanteModelo.SubTotalComprobante);
                    sqlCommand.Parameters.AddWithValue("@TotalComprobante", comprobanteModelo.TotalComprobante);

                    SqlParameter identificadorRespuesta = sqlCommand.Parameters.Add("@IdentificadorRespuesta", SqlDbType.Int);
                    identificadorRespuesta.Direction = ParameterDirection.Output;
                    SqlParameter mensajeRespuesta = sqlCommand.Parameters.Add("@MensajeRespuesta", SqlDbType.VarChar, 100);
                    mensajeRespuesta.Direction = ParameterDirection.Output;
                    SqlParameter errorRepsuesta = sqlCommand.Parameters.Add("@ErrorRespuesta", SqlDbType.Bit);
                    errorRepsuesta.Direction = ParameterDirection.Output;

                    sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();

                    registrarRespuestaModelo.IdentificadorRespuesta = (int)identificadorRespuesta.Value;
                    registrarRespuestaModelo.MensajeRespuesta = (string)mensajeRespuesta.Value;
                    registrarRespuestaModelo.ErrorRespuesta = (bool)errorRepsuesta.Value;
                }
            }
            catch (Exception exception)
            {
                registrarRespuestaModelo.IdentificadorRespuesta = 0;
                registrarRespuestaModelo.MensajeRespuesta = exception.ToString();
                registrarRespuestaModelo.ErrorRespuesta = true;
            }

            return registrarRespuestaModelo;
        }
    }
}
