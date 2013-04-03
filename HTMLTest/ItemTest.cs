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
		public void InputDerivate1()
		{
			ItemPackage pack = new ItemPackage("derivate");
			pack["Title"] = "TestDerivate";
			pack["Link"] = "TestDerivateLink";
			pack["Comment"] = "TestDerivateComment";
			Console.WriteLine(DerivateItem.TestRender(pack));
		}

		[TestMethod]
		public void InputDerivate2()
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
		public void InputRemark1()
		{
			ItemPackage pack = SetTest("remark");
			Console.WriteLine(StandardItem.TestRender(pack));
		}

		[TestMethod]
		public void InputRemark2()
		{
			ItemPackage pack = new ItemPackage("remark");
			pack["Title"] = "日本語の";
			pack["Caption"] = "もやもや";
			Console.WriteLine(StandardItem.TestRender(pack));
		}

		[TestMethod]
		public void InputLicense1()
		{
			ItemPackage pack = SetTest("license");
			Console.WriteLine(StandardItem.TestRender(pack));
		}

		[TestMethod]
		public void InputSection1()
		{
			ItemPackage pack = SetTest("section");
			Console.WriteLine(StandardItem.TestRender(pack));
		}

		[TestMethod]
		public void InputCaption1()
		{
			ItemPackage pack = new ItemPackage("caption");
			pack["ChapterTitle"] = "説明だよ！";
			pack["Content"] = "ほげほげだよぉ";
			Console.WriteLine(Caption.TestRender(pack));
		}
	}

	[TestClass]
	public class IdTest
	{
		[TestMethod]
		public void InputHeader1()
		{
			ItemPackage pack = new ItemPackage("header");
			pack["Title"] = "HogehogeTitle";
			Console.WriteLine(Header.TestRender(pack));
		}

		[TestMethod]
		public void InputMeta1()
		{
			ItemPackage pack = new ItemPackage("");
			pack["GenVersion"] = "1.0";
			pack["Author"] = "Eiichi Takebuchi(GRGSIBERIA)";
			pack["Title"] = "HogehogeTitle";
			pack["Tags"] = "タグ,A,B";
			Console.WriteLine(MetaHead.TestRender(pack));
		}
	}
}
