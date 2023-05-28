using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prueba.Logic;
using Prueba.Models;
using Prueba.Models.Request;
using Prueba.Models.Response;
using System.Xml;
using System.Xml.Linq;

namespace Prueba.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private string endpoint { get; set; }

    public PedidosController(IConfiguration config)
    {
        this.endpoint = config.GetSection("endpoint").Value;
    }

    [HttpPost]
    public IActionResult JsonToXml(EnvioPedidos request)
    {
        try
        {
            string requestXml = Serialize.Generate<EnvioPedidos>(request);
            string result = SoapDefault.Generate(requestXml);

            //return Content(result, "application/xml");
            return Ok(new { Response = result });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> XmlToJson()
    {
        try
        {
            PedidoRequest response = await Http.GET<PedidoRequest>(
                this.endpoint,
                "EnvioPedidoResponse"
            ).ConfigureAwait(false);

            return Ok(new PedidoResponse()
            {
                enviarPedidoRespuesta = response
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
