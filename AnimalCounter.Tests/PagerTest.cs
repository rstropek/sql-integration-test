using AnimalCounter.Api.Data;
using System;
using System.Linq;
using Xunit;

namespace AnimalCounter.Tests
{
    public class PagerTest
    {
        private readonly IQueryable<int> EmptyQueryable = Array.Empty<int>().AsQueryable();

        [Fact]
        public void InvalidPageIndex() => Assert.Throws<ArgumentOutOfRangeException>(
            () => EmptyQueryable.Page(0, 42));

        [Fact]
        public void TooLowPageSize() => Assert.Throws<ArgumentOutOfRangeException>(
                () => EmptyQueryable.Page(1, -1));

        [Fact]
        public void TooHighPageSize() => Assert.Throws<ArgumentOutOfRangeException>(
                () => EmptyQueryable.Page(1, 51));

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Paging(int pageIndex)
        {
            var result = Enumerable.Range(1, 100).AsQueryable().Page(pageIndex, 20).ToArray();
            Assert.Equal(20, result.Length);
            Assert.Equal(1 + 20 * (pageIndex - 1), result.Min());
            Assert.Equal(20 + 20 * (pageIndex - 1), result.Max());
        }
    }
}
