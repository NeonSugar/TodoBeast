using System.ComponentModel.DataAnnotations;

namespace NeonSugar.TodoBeast.Backend.IdentityServer.Models
{
	public class LoginRequest
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password  { get; set; }
	}
}
