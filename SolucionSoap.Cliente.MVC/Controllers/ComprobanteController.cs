using SolucionSoap.Cliente.MVC.ServicioComprobante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SolucionSoap.Cliente.MVC.Controllers
{
    public class ComprobanteController : Controller
    {
        public ActionResult Inicio(ComprobanteModelo comprobanteModelo)
        {
            return View("Inicio");
        }

        public JsonResult Actualizar(ComprobanteModelo comprobanteModelo)
        {
            ComprobanteRepositorioClient comprobanteRepositorioClient = new ComprobanteRepositorioClient();
            ActualizarRespuestaModelo actualizarRespuestaModelo = comprobanteRepositorioClient.Actualizar(comprobanteModelo);
            return Json(actualizarRespuestaModelo);
        }

        public JsonResult Registrar(ComprobanteModelo comprobanteModelo)
        {
            ComprobanteRepositorioClient comprobanteRepositorioClient = new ComprobanteRepositorioClient();
            RegistrarRespuestaModelo registrarRespuestaModelo = comprobanteRepositorioClient.Registrar(comprobanteModelo);
            return Json(registrarRespuestaModelo);
        }

        public JsonResult Eliminar(int IdentificadorComprobante)
        {
            ComprobanteRepositorioClient comprobanteRepositorioClient = new ComprobanteRepositorioClient();
            EliminarRespuestaModelo eliminarRespuestaModelo = comprobanteRepositorioClient.Eliminar(IdentificadorComprobante);
            return Json(eliminarRespuestaModelo);
        }

        public JsonResult Obtener(int IdentificadorComprobante)
        {
            ComprobanteRepositorioClient comprobanteRepositorioClient = new ComprobanteRepositorioClient();
            ObtenerRespuestaModeloOfComprobanteModeloKTVAo3T5 obtenerRespuestaModeloOfComprobanteModeloKTVAo3T5 = comprobanteRepositorioClient.Obtener(IdentificadorComprobante);
            return Json(obtenerRespuestaModeloOfComprobanteModeloKTVAo3T5, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Listar()
        {
            ComprobanteRepositorioClient comprobanteRepositorioClient = new ComprobanteRepositorioClient();
            ListarRespuestaModeloOfComprobanteModeloKTVAo3T5 listarRespuestaModeloOfComprobanteModeloKTVAo3T5 = comprobanteRepositorioClient.Listar();
            return Json(listarRespuestaModeloOfComprobanteModeloKTVAo3T5, JsonRequestBehavior.AllowGet);
        }
    }
}