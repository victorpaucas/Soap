using System.Runtime.Serialization;

namespace SolucionSoap.Servidor.WCF.Modelo
{
    [DataContract]
    public class BaseRespuestaModelo
    {
        [DataMember]
        public string MensajeRespuesta { get; set; }

        [DataMember]
        public bool ErrorRespuesta { get; set; }
    }
}