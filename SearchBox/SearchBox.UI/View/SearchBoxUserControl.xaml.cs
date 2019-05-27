using SearchBox.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SearchBox.View
{
    public partial class SearchBoxUserControl : UserControl
    {
        public SearchBoxViewModel SearchBoxViewModel { get; set; }

        public SearchBoxUserControl()
        {
            InitializeComponent();
            SearchBoxViewModel = new SearchBoxViewModel();
            DataContext = SearchBoxViewModel;
            this.Loaded += SearchBoxUserControl_Loaded;
        }

        private void SearchBoxUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SearchBoxViewModel.RellenarListaAsync(null);
        }

        #region Cambio de Foco
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                e.Handled = true;
                if (Elementos.Items.Count == 0)
                {
                    return;
                }
                if (Elementos.SelectedItem==null)
                {
                    Elementos.SelectedItem = Elementos.Items[0];
                }
                Elementos.UpdateLayout();
                if (!EnfocarItem())
                    Elementos.Focus();
            }
        }

        private bool EnfocarItem()
        {
            var listBoxItem = (ListBoxItem)Elementos
                              .ItemContainerGenerator
                              .ContainerFromItem(Elementos.SelectedItem);
            if (listBoxItem == null)
            {
                return false;
            }
            listBoxItem.Focus();
            return true;
        }
        #endregion
        #region Cambio de Foco y Cargado de Páginas en listbox con el teclado
        private void ComboMedicamentos_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up && ((ListBox)sender).SelectedIndex == 0 && SearchBoxViewModel.PaginaActual==1)
            {
                Keyboard.Focus(FiltroDeBusqueda);
            }
            else if (e.Key == Key.Up && ((ListBox)sender).SelectedIndex == 0)
            {
                this.SearchBoxViewModel.CargarPaginaAnterior();
            }
            else if (e.Key == Key.Down && ((ListBox)sender).SelectedIndex == 9)
            {
                this.SearchBoxViewModel.CargarPaginaSiguiente();
            }
        }
        #endregion
    }
}
