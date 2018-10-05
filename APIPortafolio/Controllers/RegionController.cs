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

        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            Region region = new Region();

            var jsonFormatter = new JsonMediaTypeFormatter();

            jsonFormatter.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
            jsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            jsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.Culture = new System.Globalization.CultureInfo("en-US");

            if(region.ReadById(id))
                return Request.CreateResponse(HttpStatusCode.OK, region, jsonFormatter);
            else
                return Request.CreateResponse(HttpStatusCode.OK, "recurso no encontrado", jsonFormatter);
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            Region region = new Region();
            var jsonFormatter = new JsonMediaTypeFormatter();

            jsonFormatter.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
            jsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            jsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.Culture = new System.Globalization.CultureInfo("en-US");

            return Request.CreateResponse(HttpStatusCode.OK, region.GetRegions(), jsonFormatter);
        }


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
                return Request.CreateResponse(HttpStatusCode.OK, region, jsonFormatter);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "error al crear", jsonFormatter);
            }
        }

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
                return Request.CreateResponse(HttpStatusCode.OK, region, jsonFormatter);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "error al modificar", jsonFormatter);
            }
        }

        [HttpDelete]
        public string Delete()
        {
            return "metodo delete";
        }

    }
}
