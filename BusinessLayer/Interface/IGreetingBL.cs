using System;
using ModelLayer.Model;
using RepositoryLayer.Entity;

namespace BusinessLayer.Interface
{
	public interface IGreetingBL
	{
		string GetMsg();
		string GetGreetingMessage(UserNameRequestModel request);
        GreetingEntity GetGreetingById(int id);
        PostGreetingResponse registerBL(RequestGreetingModel requestGreetingModel);
    }
}