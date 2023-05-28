using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Prueba.Models;

public class InfoPedido
{
    [XmlElement(ElementName = "pedido")]
    [Required]
    public string NumPedido { get; set; }

    [XmlElement(ElementName = "Cantidad")]
    [Required]
    public string CantidadPedido { get; set; }

    [XmlElement(ElementName = "EAN")]
    [Required]
    public string CodigoEAN { get; set; }

    [XmlElement(ElementName = "Producto")]
    [Required]
    public string NombreProducto { get; set; }

    [XmlElement(ElementName = "Cedula")]
    [Required]
    public string NumDocumento { get; set; }

    [XmlElement(ElementName = "Direccion")]
    [Required]
    public string Direccion { get; set; }
}
