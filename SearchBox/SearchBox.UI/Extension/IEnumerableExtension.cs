using System.Collections.Generic;

namespace System.Collections.ObjectModel
{
    public static class IEnumerableExtension
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            ObservableCollection<T> observableCollection = new ObservableCollection<T>();
            foreach (var item in collection)
            {
                observableCollection.Add(item);
            }
            return observableCollection;
        }
    }
}
