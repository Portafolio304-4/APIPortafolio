using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIPortafolio.Models
{
    public class ResponseMenssage
    {
        private string status;
        private object respuesta;

        public ResponseMenssage(string status, object respuesta)
        {
            this.Status = status;
            this.Respuesta = respuesta;
        }

        public string Status { get => status; set => status = value; }
        public object Respuesta { get => respuesta; set => respuesta = value; }
    }
}