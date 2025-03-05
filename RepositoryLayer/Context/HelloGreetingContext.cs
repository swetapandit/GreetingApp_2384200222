using Microsoft.EntityFrameworkCore;
using ModelLayer.Model;
using RepositoryLayer.Entity;

namespace ModelLayer.Model
{
    public class HelloGreetingContext : DbContext
    {
        public HelloGreetingContext(DbContextOptions<HelloGreetingContext> options)
            : base(options) { }

        public DbSet<GreetingEntity> Greetings { get; set; }
    }

}

