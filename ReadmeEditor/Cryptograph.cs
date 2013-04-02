using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace ReadmeEditor
{
	/// <summary>
	/// 制作者名と秘密鍵から暗号化と復号化を行うためのクラス
	/// </summary>
	public class Cryptograph
	{
		/// <summary>
		/// 制作者と秘密鍵で暗号化
		/// </summary>
		/// <param name="author">制作者名</param>
		/// <param name="secret">秘密鍵</param>
		/// <returns>公開鍵</returns>
		public static string Encrypt(string author, string secret)
		{
			// SHA???で暗号化
			var sha = new SHA256CryptoServiceProvider();
			byte[] sha_bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(author + secret));

			// バイト列を文字列に戻す
			StringBuilder result_text = new StringBuilder();
			foreach (var b in sha_bytes)
				result_text.Append(b.ToString("X2"));
			return result_text.ToString();
		}
	}
}
