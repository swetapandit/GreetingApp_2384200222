using System;
using BusinessLayer.Interface;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

namespace BusinessLayer.Service
{
	public class GreetingBL : IGreetingBL
	{
        private readonly IGreetingRL greetingRL;
        private PostGreetingResponse _postGreetingResponse;
        private readonly HelloGreetingContext helloGreetingContext;

        public GreetingBL(IGreetingRL _greetingRL, HelloGreetingContext _helloGreetingContext)
        {
            greetingRL = _greetingRL;
            helloGreetingContext = _helloGreetingContext;
        }

        public PostGreetingResponse registerBL(RequestGreetingModel requestGreetingModel)
        {
            _postGreetingResponse = new PostGreetingResponse();


            var result = greetingRL.PostGreeting(requestGreetingModel);
            _postGreetingResponse.Id = result.Id;
            _postGreetingResponse.Message = result.Message;
            return _postGreetingResponse;
        }

        public GreetingEntity GetGreetingById(int id)
        {
            return greetingRL.GetGreetingById(id);
        }

        public List<GreetingEntity> GetAllGreetings()
        {
            return greetingRL.GettAllGreetings();
        }

        public string GetMsg()
		{
			return "Hello World!";
		}

        public string GetGreetingMessage(UserNameRequestModel request)
        {
            if (!string.IsNullOrWhiteSpace(request.FirstName) && !string.IsNullOrWhiteSpace(request.LastName))
            {
                return $"Hello, {request.FirstName} {request.LastName}!";
            }
            else if (!string.IsNullOrWhiteSpace(request.FirstName))
            {
                return $"Hello, {request.FirstName}!";
            }
            else if (!string.IsNullOrWhiteSpace(request.LastName))
            {
                return $"Hello, {request.LastName}!";
            }
            return "Hello World!";
        }

        //patch Update data using id
        public GreetingEntity UpdateGreeting(int id, string newMessage)
        {
            return greetingRL.UpdateGreeting(id, newMessage);
        }
    }
}

