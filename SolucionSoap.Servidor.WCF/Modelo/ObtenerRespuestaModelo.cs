using System.Runtime.Serialization;

namespace SolucionSoap.Servidor.WCF.Modelo
{
    [DataContract]
    public class ObtenerRespuestaModelo<T> : BaseRespuestaModelo
    {
        [DataMember]
        public T ModeloRespuesta { get; set; }
    }
}