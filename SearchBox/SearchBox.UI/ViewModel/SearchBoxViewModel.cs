using Microsoft.Practices.Prism.Events;
using Model;
using SearchBox.Command;
using SearchBox.Data;
using SearchBox.View.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SearchBox.ViewModel
{
    public class SearchBoxViewModel:Observable
    {
        private bool visibilidadLista;
        private string palabraAFiltrar;
        private Articulo elementoSeleccionado;
        private ObservableCollection<Articulo> listaDeElementos;
        private int paginaActual;

        private ISQLDataService SqlDataService { get; }
        private IMessageBoxService MessageBoxService { get; }

        #region Variables Observables
        public bool VisibilidadLista
        {
            get { return visibilidadLista; }
            set
            {
                if (visibilidadLista!=value)
                {
                    visibilidadLista = value;
                    NotifyPropertyChanged(nameof(VisibilidadLista));
                }
            }
        }
        
        public string PalabraAFiltrar
        {
            get { return palabraAFiltrar; }
            set
            {
                if (palabraAFiltrar!=value)
                {
                    PaginaActual = 1;
                    palabraAFiltrar = value;
                    NotifyPropertyChanged(nameof(PalabraAFiltrar));
                    RellenarListaAsync(palabraAFiltrar);
                }
            }
        }

        public Articulo ElementoSeleccionado
        {
            get { return elementoSeleccionado; }
            set
            {
                if (elementoSeleccionado!=value)
                {
                    elementoSeleccionado = value;
                    NotifyPropertyChanged(nameof(ElementoSeleccionado));
                }
            }
        }
        
        public ObservableCollection<Articulo> ListaDeElementos
        {
            get { return listaDeElementos; }
            set
            {
                listaDeElementos = value;
                NotifyPropertyChanged(nameof(ListaDeElementos));
            }
        }

        public int PaginaActual
        {
            get { return paginaActual; }
            set
            {
                if (paginaActual != value)
                {
                    paginaActual = value;
                    NotifyPropertyChanged(nameof(PaginaActual));
                }
            }
        }
        #endregion

        public ICommand SeleccionarElementoCommand { get; set; }
        public ICommand MostrarListaCommand { get; set; }
        public ICommand OcultarListaCommand { get; set; }

        public SearchBoxViewModel()
        {
            ListaDeElementos = new ObservableCollection<Articulo>();
            VisibilidadLista = false;
            SeleccionarElementoCommand = new RelayCommand<Articulo>(SeleccionarElemento);
            MostrarListaCommand = new RelayCommand(MostrarLista);
            OcultarListaCommand = new RelayCommand(OcultarLista);
            SqlDataService = new SQLDataService();
            MessageBoxService = new MessageBoxService();
            PaginaActual = 1;
        }

        public void RellenarListaAsync(string filtro)
        {
            try
            {
                Task<IEnumerable<Articulo>> task = Task<IEnumerable<Articulo>>.Factory.StartNew(() =>
                {
                    return SqlDataService.ObtenerArticulosPorPagina(PaginaActual,filtro);
                });
                task.ContinueWith((t) => RellenarLista(t.Result));
            }
            catch (Exception ex)
            {
                MessageBoxService.MostrarMensajeDeError("Error de base de datos", ex.Message);
            }
        }

        public void CargarPaginaAnterior()
        {
            PaginaActual--;
            RellenarListaAsync(PalabraAFiltrar);
        }

        public void CargarPaginaSiguiente()
        {
            PaginaActual++;
            RellenarListaAsync(PalabraAFiltrar);
        }

        private void RellenarLista(IEnumerable<Articulo> lista)
        {
            var task = Task.Factory.StartNew(() =>
            {
                ListaDeElementos = lista.ToObservableCollection();
            });
        }

        #region Acciones de Commands

        private void SeleccionarElemento(Articulo elemento)
        {
            OcultarLista();
            PalabraAFiltrar = elemento.Descripcion;
        }

        private void MostrarLista()
        {
            VisibilidadLista=true;
        }

        private void OcultarLista()
        {
            VisibilidadLista = false;
        }
        #endregion
    }
}
