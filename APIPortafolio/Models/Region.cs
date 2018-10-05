using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIPortafolio.Models
{
    public class Region
    {
        private Conexion conn;
        private int id;
        private string nombre;


        public Region()
        {
            this.id = 0;
            this.nombre = "";
        }

        public Region(int id, string nombre)
        {
            this.id = id;
            this.nombre = nombre;
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public int Id { get => id; set => id = value; }

        public List<Region> GetRegions()
        {
            this.conn = new Conexion();

            List<Region> lista_regiones = new List<Region>();
            string sql = "SELECT * FROM PRUEBA";

            OracleCommand command = new OracleCommand(sql, this.conn.Connection);
            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string nombre = reader.GetString(1);

                Region region = new Region(id, nombre);
                lista_regiones.Add(region);

            }

            this.conn.Connection.Close();
            command.Dispose();

            return lista_regiones;
        }

        public bool ReadById(int id)
        {
            bool found = true;
            this.conn = new Conexion();
            this.Id = id;

            string sql = "SELECT * FROM PRUEBA WHERE id=:id";

            OracleCommand command = new OracleCommand();
            command.CommandText = sql;
            command.Connection = this.conn.Connection;
            command.Parameters.Add(new OracleParameter("id", OracleDbType.Int32)).Value = this.Id;
            OracleDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    this.Id = reader.GetInt32(0);
                    this.Nombre = reader.GetString(1);
                }
                found = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            this.conn.Connection.Close();
            command.Dispose();

            return found;
        }

        public bool Create()
        {
            bool created = false;
            this.conn = new Conexion();

            List<Region> lista_regiones = new List<Region>();
            string sql = "INSERT INTO PRUEBA (id,nombre) VALUES (:id,:nombre)";

            OracleCommand command = new OracleCommand();
            command.CommandText = sql;
            command.Connection = this.conn.Connection;
            command.Parameters.Add(new OracleParameter("id", OracleDbType.Int32)).Value = this.Id;
            command.Parameters.Add(new OracleParameter("nombre", OracleDbType.Varchar2)).Value = this.Nombre;
            try
            {
                int rowsUpdated = command.ExecuteNonQuery();
                if (rowsUpdated > 0)
                    created = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            this.conn.Connection.Close();
            command.Dispose();

            return created;
        }

        
        public bool Update()
        {
            bool updated = true;
            this.conn = new Conexion();

            string sql = "UPDATE PRUEBA SET nombre=:nombre WHERE id=:id";

            OracleCommand command = new OracleCommand();
            command.CommandText = sql;
            command.Connection = this.conn.Connection;
            command.Parameters.Add(new OracleParameter(":nombre", OracleDbType.Varchar2)).Value = this.Nombre;
            command.Parameters.Add(new OracleParameter(":id", OracleDbType.Int32)).Value = this.Id;
            
            try
            {
                int rowsUpdated = command.ExecuteNonQuery();
                if (rowsUpdated > 0)
                    updated = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            this.conn.Connection.Close();
            command.Dispose();

            return updated;
        }
    }
}