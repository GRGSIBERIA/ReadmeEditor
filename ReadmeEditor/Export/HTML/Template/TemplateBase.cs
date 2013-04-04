using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ReadmeEditor.Export.HTML.Template
{
	/// <summary>
	/// テンプレート用のベースクラス
	/// </summary>
	public abstract class TemplateBase
	{
		public static string Render(PackageBase package, string renderStr)
		{
			string render_str = renderStr;
			Regex regex = new Regex(@"\$\w+\$");
			MatchCollection match = regex.Matches(render_str);

			foreach (Match pattern in match)
			{
				string match_pattern = pattern.Groups[0].Value.Replace("$", @"\$");
				string template_variable = match_pattern.Replace(@"\$", "");
				render_str = Regex.Replace(render_str, match_pattern, package[template_variable]);
			}
			return render_str;
		}
	}

	public class MetaHead : TemplateBase
	{
		public static string TestRender(ItemPackage package)
		{
			return Render(package, Properties.Resources.MetaHead);
		}
	}
}