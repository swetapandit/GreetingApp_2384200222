using System;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Service
{
    public class GreetingRL : IGreetingRL
	{
		private readonly HelloGreetingContext dbContext;


        public GreetingRL(HelloGreetingContext _dbContext)
		{
            dbContext = _dbContext;
		}

		//Registration
		public GreetingEntity PostGreeting(RequestGreetingModel requestGreetingModel)
		{
            var existingGreeting = dbContext.Greetings.FirstOrDefault(g => g.Message == requestGreetingModel.Message);
            var newGreeting = new GreetingEntity
			{
				Message = requestGreetingModel.Message
			
            };
            dbContext.Greetings.Add(newGreeting);
            dbContext.SaveChanges();
            return newGreeting;
        }
		
	}
}

