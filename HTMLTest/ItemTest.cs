using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ReadmeEditor.Export.HTML;
using ReadmeEditor.Export.HTML.Template;

namespace HTMLTest
{
	[TestClass]
	public class ItemTest
	{
		[TestMethod]
		public void ItemDerivate1()
		{
			ItemPackage pack = new ItemPackage("derivate");
			pack["Title"] = "TestDerivate";
			pack["Link"] = "TestDerivateLink";
			pack["Comment"] = "TestDerivateComment";
			Console.WriteLine(DerivateItem.TestRender(pack));
		}

		[TestMethod]
		public void ItemDerivate2()
		{
			ItemPackage pack = new ItemPackage("derivate");
			pack["Title"] = "日本語の";
			pack["Link"] = "htmldesu";
			pack["Comment"] = "もやもや";
			Console.WriteLine(DerivateItem.TestRender(pack));
		}

		private ItemPackage SetTest(string name)
		{
			ItemPackage pack = new ItemPackage(name);
			pack["Title"] = "Test" + name.ToUpper() + "Title";
			pack["Caption"] = "Test" + name.ToUpper() + "Caption";
			return pack;
		}

		[TestMethod]
		public void ItemRemark1()
		{
			ItemPackage pack = SetTest("remark");
			Console.WriteLine(StandardItem.TestRender(pack));
		}

		[TestMethod]
		public void ItemRemark2()
		{
			ItemPackage pack = new ItemPackage("remark");
			pack["Title"] = "日本語の";
			pack["Caption"] = "もやもや";
			Console.WriteLine(StandardItem.TestRender(pack));
		}

		[TestMethod]
		public void ItemLicense1()
		{
			ItemPackage pack = SetTest("license");
			Console.WriteLine(StandardItem.TestRender(pack));
		}

		[TestMethod]
		public void ItemSection1()
		{
			ItemPackage pack = SetTest("section");
			Console.WriteLine(StandardItem.TestRender(pack));
		}

		[TestMethod]
		public void ItemCaption1()
		{
			ItemPackage pack = new ItemPackage("caption");
			pack["ChapterTitle"] = "説明だよ！";
			pack["Contents"] = "ほげほげだよぉ";
			Console.WriteLine(Caption.TestRender(pack));
		}

		[TestMethod]
		public void ItemMeta1()
		{
			ItemPackage pack = new ItemPackage("");
			pack["GenVersion"] = "1.0";
			pack["Author"] = "Eiichi Takebuchi(GRGSIBERIA)";
			pack["Title"] = "HogehogeTitle";
			pack["Tags"] = "タグ,A,B";
			Console.WriteLine(MetaHead.TestRender(pack));
		}
	}

	[TestClass]
	public class ComponentTest
	{
		[TestMethod]
		public void ComponentHeader1()
		{
			ItemPackage pack = new ItemPackage("header");
			pack["Title"] = "HogehogeTitle";
			Console.WriteLine(Header.TestRender(pack));
		}
	}

	public class TestBase
	{
		protected ItemPackage MakeItem(string part, string title, string caption)
		{
			ItemPackage item = new ItemPackage(part);
			item["Title"] = title;
			item["Caption"] = caption;
			return item;
		}

		protected PartPackage MakePart(string part, string chapTitle)
		{
			PartPackage partPack = new PartPackage(part);
			partPack["ChapterTitle"] = chapTitle;
			return partPack;
		}
	}

	[TestClass]
	public class PartTest : TestBase
	{
		

		[TestMethod]
		public void PartCaption()
		{
			ItemPackage item = new ItemPackage("caption");
			item["Contents"] = "せつめいしょ";

			PartPackage part = new PartPackage("caption");
			part["ChapterTitle"] = "せつめいしょのたいとる";
			
			string result = StandardPart.RenderTest(part, new ItemPackage[] {item}, "Caption");
			Console.WriteLine(result);
		}

		[TestMethod]
		public void PartRemark()
		{
			ItemPackage[] items = new ItemPackage[] {
				MakeItem("remark", "備考1", "説明1"),
				MakeItem("remark", "備考2", "説明2")
			};

			PartPackage part = MakePart("remark", "びこう");

			string result = StandardPart.RenderTest(part, items, "StandardItem");
			Console.WriteLine(result);
		}

		[TestMethod]
		public void PartUpdate()
		{
			ItemPackage[] items = new ItemPackage[] {
				MakeItem("update", "V1.0", "あっぷ"),
				MakeItem("update", "V1.2", "hoge"),
				MakeItem("update", "V1.4", "puyo")
			};

			PartPackage part = MakePart("update", "うpだて");
			string result = StandardPart.RenderTest(part, items, "StandardItem");
			Console.WriteLine(result);
		}

		[TestMethod]
		public void PartLicense()
		{
			ItemPackage[] items = new ItemPackage[] {
				MakeItem("license", "GPL", "あっぷ"),
				MakeItem("license", "LGPL", "hoge"),
				MakeItem("license", "BSD", "puyo")
			};

			PartPackage part = MakePart("license", "raisensu");
			string result = StandardPart.RenderTest(part, items, "StandardItem");
			Console.WriteLine(result);
		}
	}

	[TestClass]
	public class SectionTest : TestBase
	{
		private SectionPackage MakeSection(string sectionName, string title, string caption, SectionPackage[] childs)
		{
			SectionPackage pack = new SectionPackage(sectionName, childs);
			pack["Title"] = title;
			pack["Caption"] = caption;
			return pack;
		}

		[TestMethod]
		public void PartSectionHowto1()
		{
			SectionPackage[] sections = new SectionPackage[] {
				MakeSection("section", "1", "あ", new SectionPackage[] {}),
				MakeSection("section", "2", "い", new SectionPackage[] {}),
				MakeSection("section", "3", "う", new SectionPackage[] {})
			};

			PartPackage part = MakePart("howto", "はうつー");
			string result = StandardPart.RenderSection(part, sections);
			Console.WriteLine(result);
		}

		[TestMethod]
		public void PartSectionHowto2()
		{
			SectionPackage[] childs = new SectionPackage[] {
				MakeSection("section", "a", "hoge", new SectionPackage[] {}),
				MakeSection("section", "b", "puyo", new SectionPackage[] {}),
				MakeSection("section", "c", "moe", new SectionPackage[] {})
			};

			SectionPackage[] sections = new SectionPackage[] {
				MakeSection("section", "1", "あ", childs),
				MakeSection("section", "2", "い", new SectionPackage[] {}),
				MakeSection("section", "3", "う", new SectionPackage[] {})
			};

			PartPackage part = MakePart("howto", "はうつー");
			string result = StandardPart.RenderSection(part, sections);
			Console.WriteLine(result);
		}
	}
}
