using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using Model;

namespace DataAccess
{
    public class ConexionSQL
    {
        public List<Articulo> ObtenerArticulos(string filtro=null)
        {
            List<Articulo> Datos = new List<Articulo>();
            using (var conexionSQL = new SqlConnection(ObtenerCadenaDeConexion()))
            {
                try
                {
                    conexionSQL.Open();
                    SqlDataReader lectorSQL;
                    string sentenciaSQL = (string.IsNullOrWhiteSpace(filtro))
                                          ? "SELECT Codigo,Descripcion FROM dbo.articuloInventario;"
                                          : $"SELECT Codigo,Descripcion FROM dbo.articuloInventario a WHERE a.Descripcion LIKE '{filtro}%'";
                    SqlCommand myCommand = new SqlCommand(sentenciaSQL, conexionSQL);
                    lectorSQL = myCommand.ExecuteReader();
                    while (lectorSQL.Read())
                    {
                        string codigo = lectorSQL.GetString(0);
                        string descripcion = lectorSQL.GetString(1);
                        Datos.Add(new Articulo { Codigo = codigo, Descripcion = descripcion });
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"No se pudo establecer la conexión con la Base de datos.{Environment.NewLine}{ex.Message}");
                }
            }
            return Datos;
        }

        public List<Articulo> ObtenerPaginaDeArticulos(string filtro, int pagina)
        {
            List<Articulo> Datos = new List<Articulo>();
            using (var conexionSQL = new SqlConnection(ObtenerCadenaDeConexion()))
            {
                try
                {
                    conexionSQL.Open();
                    SqlDataReader lectorSQL;
                    string sentenciaSQL = $"SELECT * FROM dbo.ObtenerPaginaDeArticulos('{filtro}',{pagina.ToString()},10)";
                    SqlCommand myCommand = new SqlCommand(sentenciaSQL, conexionSQL);
                    lectorSQL = myCommand.ExecuteReader();
                    while (lectorSQL.Read())
                    {
                        string codigo = lectorSQL.GetString(0);
                        string descripcion = lectorSQL.GetString(1);
                        Datos.Add(new Articulo { Codigo = codigo, Descripcion = descripcion });
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"No se pudo establecer la conexión con la Base de datos.{Environment.NewLine}{ex.Message}");
                }
            }
            return Datos;
        }

        private string ObtenerCadenaDeConexion()
        {
            return ConfigurationManager.ConnectionStrings["CarnavalitoDB"].ConnectionString;
        }
    }
}
