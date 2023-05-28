using System.Xml.Serialization;

namespace Prueba.Models.Request;

[Serializable, XmlRoot(ElementName = "EnvioPedidoResponse")]
public class PedidoRequest
{
    [XmlElement(ElementName = "Codigo")]
    public string codigoEnvio { get; set; }

    [XmlElement(ElementName = "Mensaje")]
    public string estado { get; set; }
}
