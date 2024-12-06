internal static class ListHelpers
{
    internal static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
    {
        T tmp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = tmp;
        return list;
    }
}
