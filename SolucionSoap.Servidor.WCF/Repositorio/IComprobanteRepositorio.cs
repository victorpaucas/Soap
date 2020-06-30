using SolucionSoap.Servidor.WCF.Modelo;
using System.ServiceModel;

namespace SolucionSoap.Servidor.WCF.Repositorio
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IComprobanteRepositorio" in both code and config file together.
    [ServiceContract]
    public interface IComprobanteRepositorio
    {
        [OperationContract]
        RegistrarRespuestaModelo Registrar(ComprobanteModelo comprobanteModelo);

        [OperationContract]
        ActualizarRespuestaModelo Actualizar(ComprobanteModelo comprobanteModelo);

        [OperationContract]
        EliminarRespuestaModelo Eliminar(int IdentificadorComprobante);

        [OperationContract]
        ObtenerRespuestaModelo<ComprobanteModelo> Obtener(int IdentificadorComprobante);

        [OperationContract]
        ListarRespuestaModelo<ComprobanteModelo> Listar();
    }
}
