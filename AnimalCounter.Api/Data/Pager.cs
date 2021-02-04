using System;
using System.Linq;

namespace AnimalCounter.Api.Data
{
    public static class Pager
    { 
        public static IQueryable<T> Page<T>(this IQueryable<T> source, int pageIndex, int pageSize)
        {
            if (pageSize < 0 || pageSize > 50)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            }

            if (pageIndex < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageIndex));
            }

            return source.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        }
    }
}
