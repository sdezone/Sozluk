using System;
using System.Security.Cryptography;
using System.Text;

namespace Sozluk.Common.Infrastructure
{
	public class PasswordEncryptor
	{
		public static string Encryp(string password)
		{
			using var md5 = MD5.Create();
			byte[] bytes = Encoding.ASCII.GetBytes(password);
			byte[] hashBytes = md5.ComputeHash(bytes);
            return Convert.ToHexString(hashBytes);
		}
	}
}

