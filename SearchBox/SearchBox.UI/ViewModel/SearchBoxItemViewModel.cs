using Microsoft.Practices.Prism.Events;
using Model;
using SearchBox.Command;
using SearchBox.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SearchBox.ViewModel
{
    public class SearchBoxItemViewModel : Observable
    {
        public Articulo Articulo { get; set; }

        private IEventAggregator eventAggregator { get; }
        
        public ICommand SeleccionarElementoCommand { get; set; }

        public SearchBoxItemViewModel(Articulo Articulo, IEventAggregator eventAggregator)
        {
            this.Articulo = Articulo;
            this.eventAggregator = eventAggregator;
            SeleccionarElementoCommand = new RelayCommand(SeleccionarElemento);
        }

        private void SeleccionarElemento()
        {
            eventAggregator.GetEvent<SeleccionarItemEvent>().Publish(Articulo);
        }
    }
}
