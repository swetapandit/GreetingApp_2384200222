using System;
using BusinessLayer.Interface;
using ModelLayer.Model;

namespace BusinessLayer.Service
{
	public class GreetingBL : IGreetingBL
	{
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

