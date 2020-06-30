using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SolucionSoap.Servidor.WCF.Modelo
{
    [DataContract]
    public class ListarRespuestaModelo<T> : BaseRespuestaModelo
    {
        [DataMember]
        public List<T> ListaRespuesta { get; set; }
    }
}