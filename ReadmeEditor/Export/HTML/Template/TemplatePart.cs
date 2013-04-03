using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReadmeEditor.Export.HTML.Template
{
	/// <summary>
	/// パートごとのテンプレート
	/// この中にItemを入れていく
	/// </summary>
	public abstract class TemplatePart : TemplateBase
	{

	}

	/// <summary>
	/// 一応，全てのパートで適応できる
	/// </summary>
	public class StandardPart : TemplatePart
	{
		public static string RenderTest<T>(PartPackage partPack, ItemPackage[] itemPacks)
			where T : TemplateItem
		{
			string result = "";

			switch (typeof(T).Name)
			{
				case "DerivateItem":
					result = RenderDerivate(partPack["ItemTemplate"], itemPacks);
					break;

				case "StandardItem":
					break;

				case "Caption":
					break;
			}

			return result;
		}

		private static string RenderDerivate(string resource, ItemPackage[] itemPacks)
		{
			string result = "";
			if (itemPacks.Length > 0)
			{
				result += DerivateItem.Render(itemPacks[0], resource);
				for (int i = 1; i < itemPacks.Length; i++)
				{
					result += "\n" + Properties.Resources.Separate + "\n";
					result += DerivateItem.Render(itemPacks[i], resource);
				}
			}
			return result;
		}

		private static string RenderStandard()
		{
			return "";
		}

		private static string RenderCaption()
		{
			return "";
		}
	}

	/// <summary>
	/// ナビゲーション用パート
	/// </summary>
	public class NavigationPart : TemplatePart
	{

	}
}
