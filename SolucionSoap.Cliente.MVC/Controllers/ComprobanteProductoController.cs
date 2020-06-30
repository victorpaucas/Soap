using SolucionSoap.Cliente.MVC.ServicioComprobanteProducto;
using System.Web.Mvc;

namespace SolucionSoap.Cliente.MVC.Controllers
{
    public class ComprobanteProductoController : Controller
    {
        public JsonResult Actualizar(ComprobanteProductoModelo ComprobanteProductoModelo)
        {
            ComprobanteProductoRepositorioClient ComprobanteProductoRepositorioClient = new ComprobanteProductoRepositorioClient();
            ActualizarRespuestaModelo actualizarRespuestaModelo = ComprobanteProductoRepositorioClient.Actualizar(ComprobanteProductoModelo);
            return Json(actualizarRespuestaModelo);
        }

        public JsonResult Registrar(ComprobanteProductoModelo ComprobanteProductoModelo)
        {
            ComprobanteProductoRepositorioClient ComprobanteProductoRepositorioClient = new ComprobanteProductoRepositorioClient();
            RegistrarRespuestaModelo registrarRespuestaModelo = ComprobanteProductoRepositorioClient.Registrar(ComprobanteProductoModelo);
            return Json(registrarRespuestaModelo);
        }

        public JsonResult Eliminar(int IdentificadorComprobanteProducto)
        {
            ComprobanteProductoRepositorioClient ComprobanteProductoRepositorioClient = new ComprobanteProductoRepositorioClient();
            EliminarRespuestaModelo eliminarRespuestaModelo = ComprobanteProductoRepositorioClient.Eliminar(IdentificadorComprobanteProducto);
            return Json(eliminarRespuestaModelo);
        }

        public JsonResult Obtener(int IdentificadorComprobanteProducto)
        {
            ComprobanteProductoRepositorioClient ComprobanteProductoRepositorioClient = new ComprobanteProductoRepositorioClient();
            ObtenerRespuestaModeloOfComprobanteProductoModeloKTVAo3T5 obtenerRespuestaModeloOfComprobanteProductoModeloKTVAo3T5 = ComprobanteProductoRepositorioClient.Obtener(IdentificadorComprobanteProducto);
            return Json(obtenerRespuestaModeloOfComprobanteProductoModeloKTVAo3T5, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarPorIdentificadorComprobante(int IdentificadorComprobante)
        {
            ComprobanteProductoRepositorioClient ComprobanteProductoRepositorioClient = new ComprobanteProductoRepositorioClient();
            ListarRespuestaModeloOfComprobanteProductoModeloKTVAo3T5 listarRespuestaModeloOfComprobanteProductoModeloKTVAo3T5 = ComprobanteProductoRepositorioClient.ListarPorIdentificadorComprobante(IdentificadorComprobante);
            return Json(listarRespuestaModeloOfComprobanteProductoModeloKTVAo3T5, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Listar()
        {
            ComprobanteProductoRepositorioClient ComprobanteProductoRepositorioClient = new ComprobanteProductoRepositorioClient();
            ListarRespuestaModeloOfComprobanteProductoModeloKTVAo3T5 listarRespuestaModeloOfComprobanteProductoModeloKTVAo3T5 = ComprobanteProductoRepositorioClient.Listar();
            return Json(listarRespuestaModeloOfComprobanteProductoModeloKTVAo3T5, JsonRequestBehavior.AllowGet);
        }
    }
}