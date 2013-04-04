using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReadmeEditor.Export.HTML.Template
{
	/// <summary>
	/// Itemクラスを利用している場合のテンプレート
	/// </summary>
	public class TemplateItem : TemplateBase
	{

	}

	public class DerivateItem : TemplateItem
	{
		public static string TestRender(ItemPackage package)
		{
			return Render(package, Properties.Resources.DerivateItem);
		}
	}

	public class StandardItem : TemplateItem
	{
		public static string TestRender(ItemPackage package)
		{
			return Render(package, Properties.Resources.StandardItem);
		}
	}

	public class Caption : TemplateBase
	{
		public static string TestRender(ItemPackage package)
		{
			return Render(package, Properties.Resources.Caption);
		}
	}

	public class Separate : TemplateBase
	{
		
	}

	
}