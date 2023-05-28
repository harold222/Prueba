# Prueba

Fue realizada mediante .NET 6, se expusieron dos endpoints uno get y otro post con la ruta api/Pedidos

Para el punto B, se uso el endpoint POST se toma la peticion en JSON y se transforma a XML, el resultado se genera en string dentro de un JSON.

Para el punto C, se configuro en el appsettings el endpoint de run.mocky.io, primero se hace una peticion get a este enpoint para que regrese el XML de respuesta, luego por medio de la libreria newtonsoft se convierta a JSON y eso es lo que se responde