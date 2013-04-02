using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace ReadmeEditor
{
	/// <summary>
	/// フォームの値を格納するためのコンテナみたいなもの
	/// </summary>
	public class AttributePackage
	{
		public string Title { get; set; }
		public string Author { get; set; }
		public string OpenPass { get; set; }
		public string Caption { get; set; }
		public string CreatedAt { get; set; }
		public string Version { get; set; }
		public string Email { get; set; }
		
		public TextPackage[] Update { get; set; }
		public TextPackage[] Howto { get; set; }
		public TextPackage[] Derivate { get; set; }
		public TextPackage[] Remark { get; set; }
		public TextPackage[] License { get; set; }
	}

	public class TextPackage
	{
		private Dictionary<string, string> data = new Dictionary<string,string>();

		public string this[string i]
		{
			get { return data[i]; }
			set { data[i] = value; }
		}

		public int Count { get { return data.Count; } }
	}

	/// <summary>
	/// AttributePackageからフォームへ流し込む
	/// </summary>
	public class Unpacker
	{
		public static List<TextPackage> UnpackListBoxOnSection(TextPackage[] sourcePacks, ListBox listBox)
		{
			if (sourcePacks != null)
			{
				List<TextPackage> target_packs = new List<TextPackage>(sourcePacks);
				foreach (var item in target_packs)
				{
					string add_name = item["Section"].Length > 0 ?
						item["Section"] + "#" + item["Title"] : item["Title"];
					listBox.Items.Add(add_name);
				}
				return target_packs;
			}
			else
			{
				return new List<TextPackage>();
			}
		}

		public static List<TextPackage> UnpackListBox(TextPackage[] sourcePacks, ListBox listBox)
		{
			if (sourcePacks != null)
			{
				List<TextPackage> result = new List<TextPackage>(sourcePacks);
				foreach (var item in result)
					listBox.Items.Add(item["Title"]);
				return result;
			}
			else
			{
				return new List<TextPackage>();
			}
		}

		public static void UnpackLicense(TextPackage[] sourcePacks, CheckedListBox licenseCheckedListBox)
		{
			if (sourcePacks != null)
			{
				foreach (var item in sourcePacks)
				{
					var checkedItems = licenseCheckedListBox.Items;	// チェックボックスの中身とライセンスが一致していたらチェックする
					for (int i = 0; i < checkedItems.Count; i++)
					{
						if (item["Type"] == (string)checkedItems[i])
							licenseCheckedListBox.SetItemChecked(i, true);
					}
				}
			}
		}
	}
}
