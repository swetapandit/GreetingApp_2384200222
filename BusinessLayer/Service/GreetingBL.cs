using System;
using BusinessLayer.Interface;
using ModelLayer.Model;
using RepositoryLayer.Interface;

namespace BusinessLayer.Service
{
	public class GreetingBL : IGreetingBL
	{
        private readonly IGreetingRL greetingRL;
        private PostGreetingResponse _postGreetingResponse;

        public GreetingBL(IGreetingRL _greetingRL)
        {
            greetingRL = _greetingRL;
        }

        public PostGreetingResponse registerBL(RequestGreetingModel requestGreetingModel)
        {
            _postGreetingResponse = new PostGreetingResponse();


            var result = greetingRL.PostGreeting(requestGreetingModel);
            _postGreetingResponse.Id = result.Id;
            _postGreetingResponse.Message = result.Message;
            return _postGreetingResponse;
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

    }
}

