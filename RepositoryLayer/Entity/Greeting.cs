using System;
using System.ComponentModel.DataAnnotations;

namespace RepositoryLayer.Entity
{

	public class Greeting
	{
		[Key]
		public int Id { get; set; }
		[Required]
        public string Message { get; set; }
        
    }
}

