using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ModelLayer.Model;

namespace RepositoryLayer.Context
{

    public class HelloGreetingContextFactory : IDesignTimeDbContextFactory<HelloGreetingContext>
    {
        public HelloGreetingContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HelloGreetingContext>();
            optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=HelloGreeting;User=sa;Password={password};");

            return new HelloGreetingContext(optionsBuilder.Options);
        }
    }

}

