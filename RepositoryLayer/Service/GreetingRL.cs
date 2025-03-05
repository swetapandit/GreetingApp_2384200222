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

		//Post data
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

        // Get all Details
        public List<GreetingEntity> GettAllGreetings()
        {
            return dbContext.Greetings.ToList();
        }

        public GreetingEntity GetGreetingById(int id)
        {
            return dbContext.Greetings.FirstOrDefault(g => g.Id == id);
        }


        //Patch 
        public GreetingEntity UpdateGreeting(int id, string newMessage)
        {
            var existingGreeting = dbContext.Greetings.FirstOrDefault(g => g.Id == id);
            if (existingGreeting != null)
            {
                existingGreeting.Message = newMessage;
                dbContext.Greetings.Update(existingGreeting);  // Mark entity as modified
                dbContext.SaveChanges();
            }
            return existingGreeting;
        }
       
    }
}

