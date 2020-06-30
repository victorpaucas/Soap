using System.Configuration;

namespace SolucionSoap.Servidor.WCF.Consulta
{
    public class ConexionConsulta
    {
        public static string cadenaConexion = ConfigurationManager.ConnectionStrings["Soap"].ToString();

        public static string ComprobanteActualizar = ConfigurationManager.AppSettings["ComprobanteActualizar"];
        public static string ComprobanteRegistrar = ConfigurationManager.AppSettings["ComprobanteRegistrar"];
        public static string ComprobanteEliminar = ConfigurationManager.AppSettings["ComprobanteEliminar"];
        public static string ComprobanteObtener = ConfigurationManager.AppSettings["ComprobanteObtener"];
        public static string ComprobanteListar = ConfigurationManager.AppSettings["ComprobanteListar"];

        public static string ComprobanteProductoActualizar = ConfigurationManager.AppSettings["ComprobanteProductoActualizar"];
        public static string ComprobanteProductoRegistrar = ConfigurationManager.AppSettings["ComprobanteProductoRegistrar"];
        public static string ComprobanteProductoEliminar = ConfigurationManager.AppSettings["ComprobanteProductoEliminar"];
        public static string ComprobanteProductoObtener = ConfigurationManager.AppSettings["ComprobanteProductoObtener"];
        public static string ComprobanteProductoListar = ConfigurationManager.AppSettings["ComprobanteProductoListar"];
        public static string ComprobanteProductoListarPorIdentificadorComprobante = ConfigurationManager.AppSettings["ComprobanteProductoListarPorIdentificadorComprobante"];
    }
}