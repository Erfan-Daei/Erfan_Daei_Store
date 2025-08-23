namespace Practice_Store.Common
{
    public static class Pagination
    {
        public static IEnumerable<TSource> ToPaged<TSource>(this IEnumerable<TSource> Source, int Page, int PageSize)
        {
            return Source.Skip((Page - 1) * PageSize).Take(PageSize);
        }
    }
}
