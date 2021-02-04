using AnimalCounter.Api.Data;
using System.Linq;
using Xunit;

namespace AnimalCounter.Tests
{
    public class AnimalFrequencyFilterTest
    {
        [Fact]
        public void Unfiltered() => Assert.Equal(TestAnimals.Animals.Count(), TestAnimals.Animals.FilterByName(string.Empty).Count());

        [Fact]
        public void FilterAnimalName() => Assert.Equal(2, TestAnimals.Animals.FilterByName("1").Count());

        [Fact]
        public void FilterScientificName() => Assert.Single(TestAnimals.Animals.FilterByName("B"));
    }
}
