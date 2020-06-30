using System;
using System.Runtime.Serialization;

namespace SolucionSoap.Servidor.WCF.Modelo
{
    [DataContract]
    public class ComprobanteModelo
    {
        [DataMember]
        public int IdentificadorComprobante { get; set; }

        [DataMember]
        public string TipoComprobante { get; set; }

        [DataMember]
        public string VendedorComprobante { get; set; }

        [DataMember]
        public string ClienteComprobante { get; set; }

        [DataMember]
        public DateTime FechaComprobante { get; set; }

        [DataMember]
        public decimal DescuentoComprobante { get; set; }

        [DataMember]
        public decimal ImpuestoComprobante { get; set; }

        [DataMember]
        public decimal SubTotalComprobante { get; set; }

        [DataMember]
        public decimal TotalComprobante { get; set; }
    }
}