using SolucionSoap.Servidor.WCF.Consulta;
using SolucionSoap.Servidor.WCF.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SolucionSoap.Servidor.WCF.Repositorio
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ComprobanteProductoRepositorio" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ComprobanteProductoRepositorio.svc or ComprobanteProductoRepositorio.svc.cs at the Solution Explorer and start debugging.
    public class ComprobanteProductoRepositorio : IComprobanteProductoRepositorio
    {
        public ActualizarRespuestaModelo Actualizar(ComprobanteProductoModelo comprobanteProductoModelo)
        {
            ActualizarRespuestaModelo actualizarRespuestaModelo = new ActualizarRespuestaModelo();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConexionConsulta.cadenaConexion))
                {
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand
                    {
                        CommandText = ConexionConsulta.ComprobanteProductoActualizar,
                        CommandType = CommandType.StoredProcedure,
                        Connection = sqlConnection
                    };

                    sqlCommand.Parameters.AddWithValue("@IdentificadorComprobanteProducto", comprobanteProductoModelo.IdentificadorComprobanteProducto);
                    sqlCommand.Parameters.AddWithValue("@IdentificadorComprobante", comprobanteProductoModelo.IdentificadorComprobante);
                    sqlCommand.Parameters.AddWithValue("@NombreComprobanteProducto", comprobanteProductoModelo.NombreComprobanteProducto);
                    sqlCommand.Parameters.AddWithValue("@CantidadComprobanteProducto", comprobanteProductoModelo.CantidadComprobanteProducto);
                    sqlCommand.Parameters.AddWithValue("@PrecioComprobanteProducto", comprobanteProductoModelo.PrecioComprobanteProducto);
                    sqlCommand.Parameters.AddWithValue("@TotalComprobanteProducto", comprobanteProductoModelo.TotalComprobanteProducto);

                    SqlParameter mensajeRespuesta = sqlCommand.Parameters.Add("@MensajeRespuesta", SqlDbType.VarChar, 100);
                    mensajeRespuesta.Direction = ParameterDirection.Output;
                    SqlParameter errorRespuesta = sqlCommand.Parameters.Add("@ErrorRespuesta", SqlDbType.Bit);
                    errorRespuesta.Direction = ParameterDirection.Output;

                    sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();

                    actualizarRespuestaModelo.MensajeRespuesta = (string)mensajeRespuesta.Value;
                    actualizarRespuestaModelo.ErrorRespuesta = (bool)errorRespuesta.Value;
                }
            }
            catch (Exception exception)
            {
                actualizarRespuestaModelo.MensajeRespuesta = exception.ToString();
                actualizarRespuestaModelo.ErrorRespuesta = true;
            }

            return actualizarRespuestaModelo;
        }

        public EliminarRespuestaModelo Eliminar(int IdentificadorComprobanteProducto)
        {
            EliminarRespuestaModelo eliminarRespuestaModelo = new EliminarRespuestaModelo();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConexionConsulta.cadenaConexion))
                {
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand()
                    {
                        CommandText = ConexionConsulta.ComprobanteProductoEliminar,
                        CommandType = CommandType.StoredProcedure,
                        Connection = sqlConnection
                    };

                    sqlCommand.Parameters.AddWithValue("@IdentificadorComprobanteProducto", IdentificadorComprobanteProducto);

                    SqlParameter mensajeRespuesta = sqlCommand.Parameters.Add("@MensajeRespuesta", SqlDbType.VarChar, 100);
                    mensajeRespuesta.Direction = ParameterDirection.Output;
                    SqlParameter errorRespuesta = sqlCommand.Parameters.Add("@ErrorRespuesta", SqlDbType.Bit);
                    errorRespuesta.Direction = ParameterDirection.Output;

                    sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();

                    eliminarRespuestaModelo.MensajeRespuesta = (string)mensajeRespuesta.Value;
                    eliminarRespuestaModelo.ErrorRespuesta = (bool)errorRespuesta.Value;
                }
            }
            catch (Exception exception)
            {
                eliminarRespuestaModelo.MensajeRespuesta = exception.ToString();
                eliminarRespuestaModelo.ErrorRespuesta = true;
            }

            return eliminarRespuestaModelo;
        }

        public ListarRespuestaModelo<ComprobanteProductoModelo> Listar()
        {
            ListarRespuestaModelo<ComprobanteProductoModelo> listarRespuestaModelo = new ListarRespuestaModelo<ComprobanteProductoModelo>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConexionConsulta.cadenaConexion))
                {
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand()
                    {
                        CommandText = ConexionConsulta.ComprobanteProductoListar,
                        CommandType = CommandType.StoredProcedure,
                        Connection = sqlConnection
                    };

                    SqlParameter mensajeRespuesta = sqlCommand.Parameters.Add("@MensajeRespuesta", SqlDbType.VarChar, 100);
                    mensajeRespuesta.Direction = ParameterDirection.Output;
                    SqlParameter errorRespuesta = sqlCommand.Parameters.Add("@ErrorRespuesta", SqlDbType.Bit);
                    errorRespuesta.Direction = ParameterDirection.Output;

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    List<ComprobanteProductoModelo> listaComprobanteProductoModelo = new List<ComprobanteProductoModelo>();

                    while (sqlDataReader.Read())
                    {
                        ComprobanteProductoModelo comprobanteProductoModelo = new ComprobanteProductoModelo
                        {
                            IdentificadorComprobanteProducto = (int)sqlDataReader["IdentificadorComprobanteProducto"],
                            IdentificadorComprobante = (int)sqlDataReader["IdentificadorComprobante"],
                            NombreComprobanteProducto = (string)sqlDataReader["NombreComprobanteProducto"],
                            CantidadComprobanteProducto = (int)sqlDataReader["CantidadComprobanteProducto"],
                            PrecioComprobanteProducto = (decimal)sqlDataReader["PrecioComprobanteProducto"],
                            TotalComprobanteProducto = (decimal)sqlDataReader["TotalComprobanteProducto"]
                        };

                        listaComprobanteProductoModelo.Add(comprobanteProductoModelo);
                    }

                    sqlConnection.Close();

                    listarRespuestaModelo.ListaRespuesta = listaComprobanteProductoModelo;
                    listarRespuestaModelo.MensajeRespuesta = (string)mensajeRespuesta.Value;
                    listarRespuestaModelo.ErrorRespuesta = (bool)errorRespuesta.Value;
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

        public ObtenerRespuestaModelo<ComprobanteProductoModelo> Obtener(int IdentificadorComprobanteProducto)
        {
            ObtenerRespuestaModelo<ComprobanteProductoModelo> obtenerRespuestaModelo = new ObtenerRespuestaModelo<ComprobanteProductoModelo>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConexionConsulta.cadenaConexion))
                {
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand()
                    {
                        CommandText = ConexionConsulta.ComprobanteProductoObtener,
                        CommandType = CommandType.StoredProcedure,
                        Connection = sqlConnection
                    };

                    sqlCommand.Parameters.AddWithValue("@IdentificadorComprobanteProducto", IdentificadorComprobanteProducto);

                    SqlParameter mensajeRespuesta = sqlCommand.Parameters.Add("@MensajeRespuesta", SqlDbType.VarChar, 100);
                    mensajeRespuesta.Direction = ParameterDirection.Output;
                    SqlParameter errorRespuesta = sqlCommand.Parameters.Add("@ErrorRespuesta", SqlDbType.Bit);
                    errorRespuesta.Direction = ParameterDirection.Output;

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    ComprobanteProductoModelo comprobanteProductoModelo = new ComprobanteProductoModelo();

                    while (sqlDataReader.Read())
                    {
                        comprobanteProductoModelo.IdentificadorComprobanteProducto = (int)sqlDataReader["IdentificadorComprobanteProducto"];
                        comprobanteProductoModelo.IdentificadorComprobante = (int)sqlDataReader["IdentificadorComprobante"];
                        comprobanteProductoModelo.NombreComprobanteProducto = (string)sqlDataReader["NombreComprobanteProducto"];
                        comprobanteProductoModelo.CantidadComprobanteProducto = (int)sqlDataReader["CantidadComprobanteProducto"];
                        comprobanteProductoModelo.PrecioComprobanteProducto = (decimal)sqlDataReader["PrecioComprobanteProducto"];
                        comprobanteProductoModelo.TotalComprobanteProducto = (decimal)sqlDataReader["TotalComprobanteProducto"];
                    }

                    sqlConnection.Close();

                    obtenerRespuestaModelo.ModeloRespuesta = comprobanteProductoModelo;
                    obtenerRespuestaModelo.MensajeRespuesta = (string)mensajeRespuesta.Value;
                    obtenerRespuestaModelo.ErrorRespuesta = (bool)errorRespuesta.Value;
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

        public ListarRespuestaModelo<ComprobanteProductoModelo> ListarPorIdentificadorComprobante(int IdentificadorComprobante)
        {
            ListarRespuestaModelo<ComprobanteProductoModelo> listarRespuestaModelo = new ListarRespuestaModelo<ComprobanteProductoModelo>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConexionConsulta.cadenaConexion))
                {
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand()
                    {
                        CommandText = ConexionConsulta.ComprobanteProductoListarPorIdentificadorComprobante,
                        CommandType = CommandType.StoredProcedure,
                        Connection = sqlConnection
                    };

                    sqlCommand.Parameters.AddWithValue("@IdentificadorComprobante", IdentificadorComprobante);

                    SqlParameter mensajeRespuesta = sqlCommand.Parameters.Add("@MensajeRespuesta", SqlDbType.VarChar, 100);
                    mensajeRespuesta.Direction = ParameterDirection.Output;
                    SqlParameter errorRespuesta = sqlCommand.Parameters.Add("@ErrorRespuesta", SqlDbType.Bit);
                    errorRespuesta.Direction = ParameterDirection.Output;

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    List<ComprobanteProductoModelo> listaComprobanteProductoModelo = new List<ComprobanteProductoModelo>();

                    while (sqlDataReader.Read())
                    {
                        ComprobanteProductoModelo comprobanteProductoModelo = new ComprobanteProductoModelo
                        {
                            IdentificadorComprobanteProducto = (int)sqlDataReader["IdentificadorComprobanteProducto"],
                            IdentificadorComprobante = (int)sqlDataReader["IdentificadorComprobante"],
                            NombreComprobanteProducto = (string)sqlDataReader["NombreComprobanteProducto"],
                            CantidadComprobanteProducto = (int)sqlDataReader["CantidadComprobanteProducto"],
                            PrecioComprobanteProducto = (decimal)sqlDataReader["PrecioComprobanteProducto"],
                            TotalComprobanteProducto = (decimal)sqlDataReader["TotalComprobanteProducto"]
                        };

                        listaComprobanteProductoModelo.Add(comprobanteProductoModelo);
                    }

                    sqlConnection.Close();

                    listarRespuestaModelo.ListaRespuesta = listaComprobanteProductoModelo;
                    listarRespuestaModelo.MensajeRespuesta = (string)mensajeRespuesta.Value;
                    listarRespuestaModelo.ErrorRespuesta = (bool)errorRespuesta.Value;
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

        public RegistrarRespuestaModelo Registrar(ComprobanteProductoModelo comprobanteProductoModelo)
        {
            RegistrarRespuestaModelo registrarRespuestaModelo = new RegistrarRespuestaModelo();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConexionConsulta.cadenaConexion))
                {
                    sqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand()
                    {
                        CommandText = ConexionConsulta.ComprobanteProductoRegistrar,
                        CommandType = CommandType.StoredProcedure,
                        Connection = sqlConnection
                    };

                    sqlCommand.Parameters.AddWithValue("@IdentificadorComprobante", comprobanteProductoModelo.IdentificadorComprobante);
                    sqlCommand.Parameters.AddWithValue("@NombreComprobanteProducto", comprobanteProductoModelo.NombreComprobanteProducto);
                    sqlCommand.Parameters.AddWithValue("@CantidadComprobanteProducto", comprobanteProductoModelo.CantidadComprobanteProducto);
                    sqlCommand.Parameters.AddWithValue("@PrecioComprobanteProducto", comprobanteProductoModelo.PrecioComprobanteProducto);
                    sqlCommand.Parameters.AddWithValue("@TotalComprobanteProducto", comprobanteProductoModelo.TotalComprobanteProducto);

                    SqlParameter identificadorRespuesta = sqlCommand.Parameters.Add("@IdentificadorRespuesta", SqlDbType.Int);
                    identificadorRespuesta.Direction = ParameterDirection.Output;
                    SqlParameter mensajeRespuesta = sqlCommand.Parameters.Add("@MensajeRespuesta", SqlDbType.VarChar, 100);
                    mensajeRespuesta.Direction = ParameterDirection.Output;
                    SqlParameter errorRespuesta = sqlCommand.Parameters.Add("@ErrorRespuesta", SqlDbType.Bit);
                    errorRespuesta.Direction = ParameterDirection.Output;

                    sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();

                    registrarRespuestaModelo.IdentificadorRespuesta = (int)identificadorRespuesta.Value;
                    registrarRespuestaModelo.MensajeRespuesta = (string)mensajeRespuesta.Value;
                    registrarRespuestaModelo.ErrorRespuesta = (bool)errorRespuesta.Value;
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
