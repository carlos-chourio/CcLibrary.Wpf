using System.Collections.Generic;
using Model;

namespace SearchBox.Data
{
    public interface ISQLDataService
    {
        IEnumerable<Articulo> ObtenerArticulosPorPagina(int pagina = 0, string filtro = null);
        IEnumerable<Articulo> ObtenerData(string filtro = null);
    }
}