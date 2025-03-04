using System;
namespace ModelLayer.Model
{
	public class ResponseModel<T>
	{
		public int StatusCode { get; set; }
		public bool Sucess { get; set; } = false;
		public string Message { get; set; } = "";
		public T Data { get; set; } = default(T);
		public ResponseModel()
		{

		}
	}
}

