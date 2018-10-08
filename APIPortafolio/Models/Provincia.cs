using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIPortafolio.Models
{
    public class Provincia
    {
        private Conexion conn;
        private int id;
        private string nombre;
        private int habilitado;
        private int id_region;

        public Provincia(int id, string nombre, int habilitado, int id_region)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Habilitado = habilitado;
            this.Id_region = id_region;
        }

        [JsonIgnore]
        public int Id { get => id; set => id = value; }
        [JsonRequired]
        public string Nombre { get => nombre; set => nombre = value; }
        [JsonIgnore]
        public int Habilitado { get => habilitado; set => habilitado = value; }
        [JsonIgnore]
        public int Id_region { get => id_region; set => id_region = value; }


        public List<Provincia> GetRegions()
        {
            this.conn = new Conexion();

            List<Provincia> lista_provincia = new List<Provincia>();
            string sql = "SELECT * FROM PROVINCIA WHERE habilitado=1";

            OracleCommand command = new OracleCommand(sql, this.conn.Connection);
            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string nombre = reader.GetString(1);
                int habilitado = reader.GetInt32(2);
                int id_region = reader.GetInt32(3);

                Provincia provincia = new Provincia(id, nombre, habilitado, id_region);
                lista_provincia.Add(provincia);

            }

            this.conn.Connection.Close();
            command.Dispose();

            return lista_provincia;
        }

        public bool ReadById(int id)
        {
            bool found = false;
            this.conn = new Conexion();
            this.Id = id;

            string sql = "SELECT * FROM PROVINCIA WHERE id=:id";

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
                    this.Habilitado = reader.GetInt32(2);
                }
                if (reader.HasRows)
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

            string sql = "INSERT INTO PROVINCIA (nombre_region) VALUES (:nombre)";

            OracleCommand command = new OracleCommand();
            command.CommandText = sql;
            command.Connection = this.conn.Connection;
            command.Parameters.Add(new OracleParameter("nombre", OracleDbType.Varchar2)).Value = this.Nombre;

            try
            {
                int rowsUpdated = command.ExecuteNonQuery();
                if (rowsUpdated > 0)
                {
                    created = true;
                }

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
            bool updated = false;
            this.conn = new Conexion();

            string sql = "UPDATE PROVINCIA SET nombre=:nombre WHERE id=:id";

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

        public bool Delete()
        {
            bool deleted = false;
            this.conn = new Conexion();

            string sql = "UPDATE PROVINCIA SET habilitado=:habilitado WHERE id=:id";

            OracleCommand command = new OracleCommand();
            command.CommandText = sql;
            command.Connection = this.conn.Connection;
            command.Parameters.Add(new OracleParameter(":nombre", OracleDbType.Int32)).Value = 0;
            command.Parameters.Add(new OracleParameter(":id", OracleDbType.Int32)).Value = this.Id;

            try
            {
                int rowsUpdated = command.ExecuteNonQuery();
                if (rowsUpdated > 0)
                    deleted = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            this.conn.Connection.Close();
            command.Dispose();

            return deleted;



        }
    }
}