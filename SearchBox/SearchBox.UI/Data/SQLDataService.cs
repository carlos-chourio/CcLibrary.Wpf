using DataAccess;
using Model;
using System.Collections.Generic;

namespace SearchBox.Data
{
    public class SQLDataService : ISQLDataService
    {
        private ConexionSQL conexionSQL;

        public SQLDataService()
        {
            conexionSQL = new ConexionSQL();
        }

        public IEnumerable<Articulo> ObtenerData(string filtro=null)
        {
            return conexionSQL.ObtenerArticulos(filtro);
        }

        public IEnumerable<Articulo> ObtenerArticulosPorPagina(int pagina, string filtro=null)
        {
            return conexionSQL.ObtenerPaginaDeArticulos(filtro, pagina);
        }
    }
}