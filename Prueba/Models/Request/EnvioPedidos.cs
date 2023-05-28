using System.Xml.Serialization;

namespace Prueba.Models.Request;

[XmlRoot(ElementName = "EnvioPedidoAcme")]
public class EnvioPedidos
{
    [XmlElement(ElementName = "EnvioPedidoRequest")]
    public InfoPedido EnviarPedido { get; set; }
}
