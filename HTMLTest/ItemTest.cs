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
	public class ItemTest : TestBase
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

		protected PartPackage MakeNavigation()
		{
			PartPackage part = new PartPackage("head-navigation");
			part["IndexTitle"] = "もくじタイトル";
			part["CaptionTitle"] = "きゃぷしょん";
			part["HowtoTitle"] = "はうつー";
			part["RemarkTitle"] = "びこう";
			part["UpdateTitle"] = "うpだて";
			part["LicenseTitle"] = "らいせんす";
			part["DerivateTitle"] = "かりもの";
			return part;
		}

		protected ComponentPackage MakeHead()
		{
			var pack = new ComponentPackage();
			pack["GenVersion"] = "1.0";
			pack["Author"] = "Eiichi Takebuchi(GRGSIBERIA)";
			pack["Title"] = "HogehogeTitle";
			pack["Tags"] = "タグ,A,B";
			return pack;
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

		private SectionPackage[] MakeMultiSection(string sectionName)
		{
			SectionPackage[] babies1_1 = new SectionPackage[] {
				MakeSection("section", "A", "男", new SectionPackage[] {}),
				MakeSection("section", "B", "山", new SectionPackage[] {}),
				MakeSection("section", "C", "皮", new SectionPackage[] {})
			};

			SectionPackage[] babies2_1 = new SectionPackage[] {
				MakeSection("section", "D", "ひげ", new SectionPackage[] {})
			};

			SectionPackage[] childs1 = new SectionPackage[] {
				MakeSection("section", "a", "hoge", babies1_1),
				MakeSection("section", "b", "puyo", new SectionPackage[] {}),
				MakeSection("section", "c", "moe", new SectionPackage[] {})
			};

			SectionPackage[] childs2 = new SectionPackage[] {
				MakeSection("section", "d", "age", babies2_1),
				MakeSection("section", "e", "dofu", new SectionPackage[] {}),
				MakeSection("section", "f", "mobu", new SectionPackage[] {})
			};

			SectionPackage[] sections = new SectionPackage[] {
				MakeSection("section", "1", "あ", childs1),
				MakeSection("section", "2", "い", childs2),
				MakeSection("section", "3", "う", new SectionPackage[] {})
			};

			return sections;
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
			SectionPackage[] sections = MakeMultiSection("section");

			PartPackage part = MakePart("howto", "はうつー");
			string result = StandardPart.RenderSection(part, sections);
			Console.WriteLine(result);
		}

		[TestMethod]
		public void NavigationPartTest()
		{
			var part = MakeNavigation();
			Console.WriteLine(NavigationPart.RenderTest(part));
		}
	}

	[TestClass]
	public class TestComponent : TestBase
	{
		[TestMethod]
		public void TestHeader()
		{
			ComponentPackage pack = new ComponentPackage();
			pack["Title"] = "たいとる";
			Console.WriteLine(Header.TestRender(pack));
		}

		[TestMethod]
		public void TestFooter()
		{
			ComponentPackage pack = new ComponentPackage();
			pack["Year"] = "2013";
			pack["Author"] = "GRGSIBERIA";
			pack["GenVersion"] = "1.0";
			Console.WriteLine(Footer.TestRender(pack));
		}

		[TestMethod]
		public void TestMain()
		{
			ComponentPackage pack = new ComponentPackage();
			pack["MainContent"] = "本番";
			Console.WriteLine(Main.TestRender(pack));
		}

		[TestMethod]
		public void TestBody()
		{
			ComponentPackage pack = new ComponentPackage();
			var nav = MakeNavigation();
			pack["Contents"] = NavigationPart.RenderTest(nav);
			Console.WriteLine(Body.TestRender(pack));
		}

		[TestMethod]
		public void TestHead()
		{
			Console.WriteLine(Head.TestRender(MakeHead()));
		}

		[TestMethod]
		public void TestHTML()
		{
			ComponentPackage body = new ComponentPackage();
			var nav = MakeNavigation();
			body["Contents"] = NavigationPart.RenderTest(nav);
			ComponentPackage html = new ComponentPackage();
			html["Body"] = Body.TestRender(body);
			html["Head"] = Head.TestRender(MakeHead());
			Console.WriteLine(HTML.TestRender(html));
		}

	}
}
