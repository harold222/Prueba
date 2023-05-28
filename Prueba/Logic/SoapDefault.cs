using System.Xml;

namespace Prueba.Logic;

public static class SoapDefault
{
    public static string Generate(string xml, string env = "http://WSDLs/EnvioPedidos/EnvioPedidosAcme")
    {
        XmlDocument soap = new();

        XmlDeclaration xmlDeclaration = soap.CreateXmlDeclaration("1.0", "UTF-8", null);
        soap.AppendChild(xmlDeclaration);

        XmlElement envelope = soap.CreateElement("soapenv", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
        soap.AppendChild(envelope);

        if (!string.IsNullOrEmpty(env))
        {
            XmlAttribute envAttribute = soap.CreateAttribute("xmlns:env");
            envAttribute.Value = env;
            envelope.Attributes.Append(envAttribute);
        }

        XmlElement header = soap.CreateElement("soapenv", "Header", envelope.NamespaceURI);
        envelope.AppendChild(header);

        XmlElement body = soap.CreateElement("soapenv", "Body", envelope.NamespaceURI);
        envelope.AppendChild(body);

        XmlDocument contentDoc = new XmlDocument();
        contentDoc.LoadXml(xml);

        XmlNode importedNode = soap.ImportNode(contentDoc.DocumentElement, true);
        body.AppendChild(importedNode);

        return soap.OuterXml;
    }
}
