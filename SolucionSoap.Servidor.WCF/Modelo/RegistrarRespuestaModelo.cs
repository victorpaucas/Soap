using System.Runtime.Serialization;

namespace SolucionSoap.Servidor.WCF.Modelo
{
    [DataContract]
    public class RegistrarRespuestaModelo : BaseRespuestaModelo
    {
        [DataMember]
        public int IdentificadorRespuesta { get; set; }
    }
}