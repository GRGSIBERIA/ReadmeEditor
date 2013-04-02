using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace ReadmeEditor
{
	public class OpenXml
	{
		public static AttributePackage Open(Stream stream)
		{
			AttributePackage pack = new AttributePackage();

			XmlDocument doc = new XmlDocument();
			try
			{
				doc.Load(stream);
				var root = doc.DocumentElement;
				ReadAllElements(root, pack);
			}
			catch (XmlException e)
			{
				MessageBox.Show(e.Message, "読み込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return pack;
		}

		private static void ReadAllElements(XmlElement root, AttributePackage pack)
		{
			ReadPack rpack = new ReadPack(root, pack);
			SafeReadElement(rpack, ReadInformation, "Information");
			SafeReadElement(rpack, ReadCaption, "Caption");
			SafeReadElement(rpack, ReadHowto, "Howto");
			SafeReadElement(rpack, ReadDerivate, "Derivate");
			SafeReadElement(rpack, ReadRemark, "Remark");
			SafeReadElement(rpack, ReadLicense, "License");
			SafeReadElement(rpack, ReadUpdate, "Update");
		}

		delegate void DelegateToReadElement(XmlElement root, AttributePackage pack);

		private class ReadPack
		{
			public XmlElement root;
			public AttributePackage pack;
			public ReadPack(XmlElement root, AttributePackage pack)
			{
				this.root = root;
				this.pack = pack;
			}
		}

		private static void SafeReadElement(ReadPack rpack, DelegateToReadElement method, string sectionName)
		{
			// どこで読み込み不全を起こしているのかチェックする用
			try
			{
				method(rpack.root, rpack.pack);
			}
			catch
			{
				MessageBox.Show(sectionName + "の読み込みに失敗しました．\n読み込んだXMLファイルのタグの閉じ忘れ等確認してください．", "読み込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private static void ReadInformation(XmlElement root, AttributePackage pack)
		{
			var nodes = root.GetElementsByTagName("Information");
			if (nodes.Count > 0)
			{
				foreach (XmlElement node in nodes[0])
				{
					switch (node.Name)
					{
						case "Title":
							pack.Title = node.InnerText;
							break;

						case "Author":
							pack.Author = node.InnerText;
							break;

						case "CreatedAt":
							pack.CreatedAt = node.InnerText;
							break;

						case "Version":
							pack.Version = node.InnerText;
							break;

						case "Email":
							pack.Email = node.InnerText;
							break;
					}
				}
			}
		}

		private static void ReadCaption(XmlElement root, AttributePackage pack)
		{
			var nodes = root.GetElementsByTagName("Caption");
			if (nodes.Count > 0)
			{
				pack.Caption = nodes[0].InnerText;
			}
			else
			{
				pack.Caption = "";
			}
		}

		private static void ReadHowto(XmlElement root, AttributePackage pack)
		{
			pack.Howto = ReadItemsAsForeach(root, "Howto", new string[] { "Title", "Section", "Caption" });
		}

		private static void ReadDerivate(XmlElement root, AttributePackage pack)
		{
			pack.Derivate = ReadItemsAsForeach(root, "Derivate", new string[] { "Title", "Link", "Comment" });
		}

		private static void ReadLicense(XmlElement root, AttributePackage pack)
		{
			pack.License = ReadItemsAsForeach(root, "License", new string[] { "Type" });
		}

		private static void ReadUpdate(XmlElement root, AttributePackage pack)
		{
			pack.Update = ReadItemsAsForeach(root, "Update", new string[] { "Title", "Section", "Caption" });
		}

		private static void ReadRemark(XmlElement root, AttributePackage pack)
		{
			pack.Remark = ReadItemsAsForeach(root, "Remark", new string[] { "Title", "Caption" });
		}

		private static TextPackage[] ReadItemsAsForeach(XmlElement root, string sectionName, string[] targetItemNode)
		{
			List<TextPackage> text_pack = new List<TextPackage>();
			XmlNodeList elements_by_tag_name = root.GetElementsByTagName(sectionName);
			if (elements_by_tag_name.Count > 0)
			{	// sectionNameが存在しなかったらジャンプする
				foreach (XmlElement item in elements_by_tag_name[0])
				{
					// ここはitemノード
					ReadAttributes(item, text_pack, targetItemNode);
				}
			}
			return text_pack.ToArray();
		}

		private static void ReadAttributes(XmlElement item, List<TextPackage> textPackage, string[] targets)
		{
			TextPackage pack = new TextPackage();
			foreach (XmlNode node in item)
			{
				foreach (string target in targets)
				{
					if (node.Name == target)
					{
						pack[target] = node.InnerText;
						break;
					}
				}
			}
			textPackage.Add(pack);
		}
	}
}
