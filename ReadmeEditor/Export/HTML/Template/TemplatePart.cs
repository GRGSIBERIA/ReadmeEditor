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
		public static string RenderPart(PartPackage partPack, PackageBase[] itemPacks)
		{
			partPack["Contents"] = RenderingItems(partPack, itemPacks);
			return DepthForIndent(partPack) + TemplateBase.Render(partPack, partPack["PartTemplate"]);
		}

		// AddIndentとは互換性のない処理なので$Indent$を挿入してもうまくいかないので注意
		private static string DepthForIndent(PartPackage partPack)
		{
			// subsection以降のインデントを整える
			if (partPack.ContainsKey("Depth"))
			{
				int depth = int.Parse(partPack["Depth"]);
				switch (depth)
				{
					case 1:
						return "                    ";

					case 2:
						return "                        ";
				}
			}
			return "";
		}

		private static string RenderingItems(PartPackage partPack, PackageBase[] itemPacks)
		{
			string result = "";

			if (itemPacks.Length > 0)
			{
				result += RenderItemAsIfRenderSection(itemPacks[0], partPack);
				for (int i = 1; i < itemPacks.Length; i++)
				{
					string indent = "";
					if (partPack.ContainsKey("Depth"))
					{
						int depth = int.Parse(partPack["Depth"]);
						indent = AddIndent(depth);
						indent += "    ";
					}
					result += "\n" + indent + Properties.Resources.Separate;
					result += RenderItemAsIfRenderSection(itemPacks[i], partPack);
				}
			}

			return result;
		}

		private static string RenderItemAsIfRenderSection(PackageBase itemPack, PartPackage partPack)
		{
			string result = RenderItemAsIfSectionPack(itemPack, partPack);

			SectionPackage section_pack = itemPack as SectionPackage;
			if (section_pack != null)
			{
				var childs = section_pack.Childs.ToArray();
				if (childs.Length > 0)
				{
					int depth = int.Parse(partPack["Depth"]) + 1;
					result += SectionPart.RenderSection(childs, depth);
				}
			}
			return result;
		}

		protected static string AddIndent(int depth)
		{
			switch (depth)
			{
				case 1:
					return "    ";

				case 2:
					return "        ";
			}
			return "";
		}

		private static string RenderItemAsIfSectionPack(PackageBase itemPack, PartPackage partPack)
		{
			SectionPackage section = itemPack as SectionPackage;
			if (section != null)
			{
				// sectionのHタグのために設定
				int depth = int.Parse(partPack["Depth"]);
				itemPack["Depth"] = (depth + 3).ToString();
				itemPack["Indent"] = AddIndent(depth);
			}

			return TemplateBase.Render(itemPack, partPack["ItemTemplate"]) + "\n";
		}
	}

	/// <summary>
	/// 一応，全てのパートで適応できる
	/// </summary>
	public class StandardPart : TemplatePart
	{
		public static string RenderTest(PartPackage partPack, PackageBase[] itemPacks, string renderType)
		{
			partPack["PartTemplate"] = Properties.Resources.StandardPart;
			switch (renderType)
			{
				case "StandardItem":
					partPack["ItemTemplate"] = Properties.Resources.StandardItem;
					break;

				case "DerivateItem":
					partPack["ItemTemplate"] = Properties.Resources.DerivateItem;
					break;

				case "Caption":
					partPack["ItemTemplate"] = Properties.Resources.Caption;
					break;
			}
			return RenderPart(partPack, itemPacks);
		}

		public static string RenderSection(PartPackage partPack, SectionPackage[] sectionPacks)
		{
			ItemPackage inner_pack = MakeInner(partPack, sectionPacks);

			partPack["ItemTemplate"] = Properties.Resources.SingleContents;
			partPack["PartTemplate"] = Properties.Resources.StandardPart;
			return RenderPart(partPack, new ItemPackage[] { inner_pack });
		}

		private static ItemPackage MakeInner(PartPackage partPack, SectionPackage[] sectionPacks)
		{
			int depth = 0;
			if (partPack.ContainsKey("Depth"))
			{
				depth = int.Parse(partPack["Depth"]) + 1;
			}

			ItemPackage inner_pack = new ItemPackage(partPack["ClassIdentifier"]);
			inner_pack["Contents"] = SectionPart.RenderSection(sectionPacks, depth);
			return inner_pack;
		}
	}

	public class SectionPart : TemplatePart
	{
		public static string RenderSection(SectionPackage[] items, int depth)
		{
			string sectionName = DepthToSectionName(depth);

			PartPackage pack = new PartPackage(sectionName);
			pack["PartTemplate"] = Properties.Resources.SectionPart;
			pack["ItemTemplate"] = Properties.Resources.SectionItem;
			pack["Depth"] = depth.ToString();
			pack["Indent"] = AddIndent(depth);
			return StandardPart.RenderPart(pack, items);
		}

		protected static string DepthToSectionName(int depth)
		{
			string sectionName = "";
			switch (depth)
			{
				case 0:
					sectionName = "section";
					break;

				case 1:
					sectionName = "subsection";
					break;

				case 2:
					sectionName = "subsubsection";
					break;
			}
			return sectionName;
		}
	}

	/// <summary>
	/// ナビゲーション用パート
	/// </summary>
	public class NavigationPart : TemplatePart
	{
		public static string RenderTest(PartPackage partPackage)
		{
			return Render(partPackage, Properties.Resources.NavigationPart);
		}
	}
}
