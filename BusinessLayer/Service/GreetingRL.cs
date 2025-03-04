using System;
using BusinessLayer.Interface;

namespace BusinessLayer.Service
{
	public class GreetingBL : IGreetingBL
	{
		public string GetMsg()
		{
			return "Hello World!";
		}
	}
}

