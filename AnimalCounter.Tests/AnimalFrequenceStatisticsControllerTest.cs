using AnimalCounter.Api.Data;
using System.Threading.Tasks;
using Xunit;
using AnimalCounter.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace AnimalCounter.Tests
{
    public class AnimalFrequenceStatisticsControllerTest
    {
        private static AnimalCounterDbContext CreateMockContext()
            => new(new DbContextOptionsBuilder<AnimalCounterDbContext>()
                .UseInMemoryDatabase(databaseName: "AnimalCounter")
                .Options);

        private static AnimalCounterDbContext CreateContext()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var optionsBuilder = new DbContextOptionsBuilder<AnimalCounterDbContext>();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

            return new(optionsBuilder.Options);
        }

        private static async Task<AnimalCounterDbContext> FillWithTestData(AnimalCounterDbContext context)
        {
            context.AnimalFrequencies.AddRange(TestAnimals.Animals);
            await context.SaveChangesAsync();
            return context;
        }

        [Fact]
        public async Task GetDefaultParams()
        {
            using var context = await FillWithTestData(CreateMockContext());
            await ExecuteDefaultParamsTest(context);
        }

        private static async Task ExecuteDefaultParamsTest(AnimalCounterDbContext context)
        {
            var controller = new AnimalFrequenceStatisticsController(context);
            var response = await controller.Get(null, null);

            Assert.IsType<OkObjectResult>(response);
            var okResponse = (OkObjectResult)response;

            Assert.IsAssignableFrom<IEnumerable<AnimalFrequency>>(okResponse.Value);
            var result = (IEnumerable<AnimalFrequency>)okResponse.Value;

            Assert.Equal(10, result.Count());
        }

        [Fact]
        public async Task GetInvalidPage()
        {
            using var context = CreateMockContext();
            var controller = new AnimalFrequenceStatisticsController(context);
            var response = await controller.Get(null, -1);

            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task IntegrationTest()
        {
            using var context = CreateContext();
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
            await FillWithTestData(context);

            await ExecuteDefaultParamsTest(context);
        }
    }
}
