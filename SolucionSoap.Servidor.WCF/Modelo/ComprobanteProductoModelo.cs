using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SolucionSoap.Servidor.WCF.Modelo
{
    [DataContract]
    public class ComprobanteProductoModelo
    {
        [DataMember]
        public int IdentificadorComprobanteProducto { get; set; }

        [DataMember]
        public int IdentificadorComprobante { get; set; }

        [DataMember]
        public string NombreComprobanteProducto { get; set; }

        [DataMember]
        public int CantidadComprobanteProducto { get; set; }

        [DataMember]
        public decimal PrecioComprobanteProducto { get; set; }

        [DataMember]
        public decimal TotalComprobanteProducto { get; set; }
    }
}