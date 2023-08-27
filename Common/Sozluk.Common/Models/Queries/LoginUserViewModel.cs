using System;
namespace Sozluk.Common.Models.Queries
{
	public class LoginUserViewModel
	{
		public Guid Id { get; set; }
		public string FirsName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string Token { get; set; }
	}
}

