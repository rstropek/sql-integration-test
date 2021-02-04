using AnimalCounter.Api.Data;
using System.Linq;

namespace AnimalCounter.Tests
{
    public static class TestAnimals
    {
        public static readonly IQueryable<AnimalFrequency> Animals = 
            Enumerable.Range(0, 11).Select(n => new AnimalFrequency
            {
                AnimalName = n.ToString(),
                ScientificName = ((char)('A' + n)).ToString()
            }).AsQueryable();
    }
}
