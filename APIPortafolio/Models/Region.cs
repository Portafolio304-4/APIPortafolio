﻿using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace APIPortafolio.Models
{
    public class Region
    {
        private Conexion conn;
        private int id;
        private string nombre;
        private int habilitado;


        public Region()
        {
            this.id = -1;
            this.nombre = "";
            this.habilitado = 1;
        }

        public Region(int id, string nombre)
        {
            this.id = id;
            this.nombre = nombre;
            this.habilitado = 1;
        }

        [JsonRequired]
        public string Nombre { get => nombre; set => nombre = value; }
        [JsonIgnore]
        public int Id { get => id; set => id = value; }
        [JsonIgnore]
        public int Habilitado { get => habilitado; set => habilitado = value; }

        public List<Region> GetRegions()
        {
            this.conn = new Conexion();

            List<Region> lista_regiones = new List<Region>();
            string sql = "SELECT * FROM REGION WHERE habilitado=1";

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
            bool found = false;
            this.conn = new Conexion();
            this.Id = id;

            string sql = "SELECT * FROM REGION WHERE id_region=:id";

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

            string sql = "INSERT INTO REGION (nombre_region) VALUES (:nombre)";

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

            string sql = "UPDATE REGION SET nombre_region=:nombre WHERE id_region=:id";

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

            string sql = "UPDATE REGION SET habilitado=:habilitado WHERE id_region=:id";

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