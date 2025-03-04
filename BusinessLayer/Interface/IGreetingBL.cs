using System;
using ModelLayer.Model;

namespace BusinessLayer.Interface
{
	public interface IGreetingBL
	{
		string GetMsg();
		string GetGreetingMessage(UserNameRequestModel request);

    }
}

