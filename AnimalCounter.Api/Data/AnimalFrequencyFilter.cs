using System.Collections.Generic;
using System.Linq;

namespace AnimalCounter.Api.Data
{
    public static class AnimalFrequencyFilter
    {
        public static IQueryable<AnimalFrequency> FilterByName(this IQueryable<AnimalFrequency> items, string? nameFilter)
        {
            if (string.IsNullOrEmpty(nameFilter))
            {
                return items;
            }

            return items.Where(a => a.AnimalName.Contains(nameFilter) || a.ScientificName.Contains(nameFilter));
        }
    }
}
