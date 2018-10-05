using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaPortafolio
{
    public class Region
    {
        private int id;
        private string nombre;

        public Region()
        {
            this.Id = -1;
            this.Nombre = "";
        }

        public Region(int id, string nombre)
        {
            this.Id = id;
            this.Nombre = nombre;
        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
    }
}
