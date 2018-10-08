using APIPortafolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace APIPortafolio.Controllers
{
    [RoutePrefix("api/region")]
    public class RegionController : ApiController
    {
        // GET api/region/1
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            Region region = new Region();

            // se genera formato de salida
            var jsonFormatter = new JsonMediaTypeFormatter();

            jsonFormatter.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
            jsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            jsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.Culture = new System.Globalization.CultureInfo("en-US");


            if (region.ReadById(id))
            {
                // se crea mensaje de respuesta
                ResponseMenssage response = new ResponseMenssage("success", region);
                return Request.CreateResponse(HttpStatusCode.OK, response, jsonFormatter);
            }
            else
            {
                ResponseMenssage response = new ResponseMenssage("error", "recurso no encontrado");
                return Request.CreateResponse(HttpStatusCode.OK, response, jsonFormatter);
            }
        }
        // GET api/region
        [HttpGet]
        public HttpResponseMessage Get()
        {
            Region region = new Region();
            var jsonFormatter = new JsonMediaTypeFormatter();

            jsonFormatter.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
            jsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            jsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.Culture = new System.Globalization.CultureInfo("en-US");

            ResponseMenssage response = new ResponseMenssage("success", region.GetRegions());
            return Request.CreateResponse(HttpStatusCode.OK, response, jsonFormatter);
        }

        // POST api/region
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Region region)
        {

            var jsonFormatter = new JsonMediaTypeFormatter();

            jsonFormatter.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
            jsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            jsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.Culture = new System.Globalization.CultureInfo("en-US");

            if (region.Create())
            {
                ResponseMenssage response = new ResponseMenssage("success", region);
                return Request.CreateResponse(HttpStatusCode.OK, response, jsonFormatter);
            }
            else
            {
                ResponseMenssage response = new ResponseMenssage("error", "error al crear");
                return Request.CreateResponse(HttpStatusCode.OK, response, jsonFormatter);
            }
        }

        // PUT api/region/1
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody]Region region)
        {
            var jsonFormatter = new JsonMediaTypeFormatter();

            jsonFormatter.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
            jsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            jsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.Culture = new System.Globalization.CultureInfo("en-US");
            region.Id = id;

            if (region.Update())
            {
                ResponseMenssage response = new ResponseMenssage("success", region);
                return Request.CreateResponse(HttpStatusCode.OK, response, jsonFormatter);
            }
            else
            {
                ResponseMenssage response = new ResponseMenssage("error", "error al modificar");
                return Request.CreateResponse(HttpStatusCode.OK, "error al modificar", jsonFormatter);
            }
        }

        // DELETE api/region/1
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            var jsonFormatter = new JsonMediaTypeFormatter();
 
            jsonFormatter.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
            jsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            jsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.Culture = new System.Globalization.CultureInfo("en-US");

            Region region = new Region();
            region.Id = id;

            if (region.Delete())
            {
                ResponseMenssage response = new ResponseMenssage("success", region);
                return Request.CreateResponse(HttpStatusCode.OK, response, jsonFormatter);
            }
            else
            {
                ResponseMenssage response = new ResponseMenssage("error", "error al eliminar");
                return Request.CreateResponse(HttpStatusCode.OK, response, jsonFormatter);
            }
        }

    }
}
