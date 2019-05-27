using System.Windows;

namespace SearchBox.View.Services
{
    public class MessageBoxService : IMessageBoxService
    {
        public void MostrarMensajeDeError(string Error,string Titulo)
        {
            MessageBox.Show(Error, Titulo, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
