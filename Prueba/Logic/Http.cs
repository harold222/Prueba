using System.Net.Mime;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Prueba.Logic;

public static class Http
{
    public static async Task<Response> Get<Response>(string url)
    {
        Response returnObject = default;

        using (HttpClient httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
            using HttpResponseMessage response = await httpClient.GetAsync(url);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response>(apiResponse);
        }
    }

    public static async Task<TResponse> GET<TResponse>(string url, string searchTag = "")
    {
        HttpWebRequest web = WebRequest.Create(url) as HttpWebRequest;

        web.Method = "GET";

        HttpWebResponse response = (HttpWebResponse)await web.GetResponseAsync().ConfigureAwait(false);

        Stream stream = response.GetResponseStream();

        Encoding encoding = Encoding.Default;
        ContentType contentType = new ContentType(response.Headers[HttpResponseHeader.ContentType]);

        if (!string.IsNullOrEmpty(contentType.CharSet))
            encoding = Encoding.GetEncoding(contentType.CharSet);

        string text;
        using (StreamReader reader = new StreamReader(stream, encoding))
        {
            text = await reader.ReadToEndAsync().ConfigureAwait(false);
        }

        if (!string.IsNullOrEmpty(searchTag))
        {
            try
            {
                text = Regex.Replace(text, @"(<\s*\/?)\s*(\w+):(\w+)", "$1$3");

                XDocument xdoc = XDocument.Parse(text);

                List<XElement> allTagsWithoutNamespaces = new();

                foreach (var item in xdoc.Descendants())
                {
                    allTagsWithoutNamespaces.Add(RemoveAllNamespaces(item));
                }

                IEnumerable<XElement>? specificTag = allTagsWithoutNamespaces.Descendants(searchTag);

                if (specificTag.Count() > 0)
                    text = specificTag.First().ToString(SaveOptions.DisableFormatting);
            }
            catch (Exception e) { }
        }

        return Serialize.Xml<TResponse>(text);
    }

    private static XElement RemoveAllNamespaces(XElement xmlDocument)
    {
        if (!xmlDocument.HasElements)
        {
            XElement xElement = new XElement(xmlDocument.Name.LocalName);
            xElement.Value = xmlDocument.Value;

            foreach (XAttribute attribute in xmlDocument.Attributes())
                xElement.Add(attribute);

            return xElement;
        }

        return new XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(el => RemoveAllNamespaces(el)));
    }
}
