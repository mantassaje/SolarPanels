using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SolarPanels.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static void RemoveAll<T>(this ObservableCollection<T> collection)
        {
            for (int i = collection.Count - 1; i >= 0; i--)
            {
                collection.RemoveAt(i);
            }
        }

        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> addItems)
        {
            foreach(var item in addItems)
            {
                collection.Add(item);
            }
        }
    }
}
