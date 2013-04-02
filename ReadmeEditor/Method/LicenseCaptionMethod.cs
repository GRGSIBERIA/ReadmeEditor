using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace ReadmeEditor
{
	public class LicenseCaptionMethod
	{
		public static List<TextPackage> Create()
		{
			List<TextPackage> packs = new List<TextPackage>();
			XmlDocument doc = new XmlDocument();
			doc.Load(@"license.xml");
			try
			{
				var childs = doc.DocumentElement.ChildNodes;
				foreach (XmlElement item in childs)
				{
					// type -> full, abstract
					var data = new TextPackage();
					data["Type"] = item.GetAttribute("type");
					foreach (XmlElement node in item.ChildNodes)
					{
						data[node.Name] = node.InnerText;
					}
					packs.Add(data);
				}
			}
			catch (XmlException e)
			{
				MessageBox.Show(e.Message, "ライセンスファイルの読み込みエラー");
			}
			return packs;
		}
	}
}
