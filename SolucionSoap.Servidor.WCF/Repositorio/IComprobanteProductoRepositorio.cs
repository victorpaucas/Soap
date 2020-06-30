using SolucionSoap.Servidor.WCF.Modelo;
using System.ServiceModel;

namespace SolucionSoap.Servidor.WCF.Repositorio
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IComprobanteProductoRepositorio" in both code and config file together.
    [ServiceContract]
    public interface IComprobanteProductoRepositorio
    {
        [OperationContract]
        RegistrarRespuestaModelo Registrar(ComprobanteProductoModelo comprobanteProductoModelo);

        [OperationContract]
        ActualizarRespuestaModelo Actualizar(ComprobanteProductoModelo comprobanteProductoModelo);

        [OperationContract]
        EliminarRespuestaModelo Eliminar(int IdentificadorComprobanteProducto);

        [OperationContract]
        ObtenerRespuestaModelo<ComprobanteProductoModelo> Obtener(int IdentificadorComprobanteProducto);

        [OperationContract]
        ListarRespuestaModelo<ComprobanteProductoModelo> ListarPorIdentificadorComprobante(int IdentificadorComprobante);

        [OperationContract]
        ListarRespuestaModelo<ComprobanteProductoModelo> Listar();
    }
}
