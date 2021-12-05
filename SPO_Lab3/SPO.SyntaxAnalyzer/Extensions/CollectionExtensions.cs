using System.Collections.Generic;

namespace SPO.SyntaxAnalyzer.Extensions
{
    /// <summary>
    /// Методы расширений для коллекций.
    /// </summary>
    public static class CollectionExtensions
    {
        public static bool ExistsPlaceByLeft<T>(this IList<T> list, T element) => list.IndexOf(element) > 0;

        public static bool ExistsPlaceByRight<T>(this IList<T> list, T element) => list.IndexOf(element) < list.Count - 1;

        public static void RemoveSince<T>(this IList<T> list, int index)
        {
            for (int i = list.Count - 1; i >= index; i--)
            {
                list.RemoveAt(i);
            }
        }
    }
}
