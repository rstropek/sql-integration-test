using Microsoft.EntityFrameworkCore;

namespace AnimalCounter.Api.Data
{
    public class AnimalCounterDbContext : DbContext
    {
        public AnimalCounterDbContext(DbContextOptions<AnimalCounterDbContext> options)
            : base(options)
        { }

        public virtual DbSet<AnimalFrequency> AnimalFrequencies { get; set; } = null!;
    }
}
