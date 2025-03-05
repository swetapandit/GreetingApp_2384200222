using System;
using ModelLayer.Model;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
	public interface IGreetingRL
	{
        GreetingEntity PostGreeting(RequestGreetingModel requestGreetingModel);
        GreetingEntity GetGreetingById(int id);
        List<GreetingEntity> GettAllGreetings();
    }
}

