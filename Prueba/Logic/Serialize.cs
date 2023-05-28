using System.Xml.Serialization;
using System.Xml;
using System.Text;

namespace Prueba.Logic;

public static class Serialize
{
    public static string Generate<T>(this T value)
    {
        if (value == null)
            return string.Empty;

        XmlSerializer xmlserializer = new XmlSerializer(typeof(T));
        Utf8StringWriter stringWriter = new Utf8StringWriter();

        XmlWriterSettings settings = new XmlWriterSettings();
        settings.OmitXmlDeclaration = true;
        settings.Indent = true;

        using XmlWriter writer = XmlWriter.Create(stringWriter, settings);

        XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
        namespaces.Add(string.Empty, string.Empty);
        xmlserializer.Serialize(writer, value, namespaces);
        return stringWriter.ToString();
    }

    public static T Xml<T>(string xml)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        TextReader reader = new StringReader(xml);
        return (T) serializer.Deserialize(reader);
    }

    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }
}
