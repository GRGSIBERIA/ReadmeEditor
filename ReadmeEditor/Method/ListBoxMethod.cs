using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReadmeEditor
{
	public class ListBoxMethod
	{
		public static void SelectItemAsCheckIndexOutOfRange(ListBox listBox, RichTextBox textBox, List<TextPackage> packs, string attrName)
		{
			// 未選択が選択された場合とありえない場所が選択された場合は無視する
			int selected_index = listBox.SelectedIndex;
			if (CheckIndexOutOfRange(listBox))
			{
				RichTextMethod.EnabledTextAsCheckOverZero(listBox, textBox);
				textBox.Text = SubstitutePackageForTextBox(packs, selected_index, attrName);
			}
		}

		public static void SelectItemAsCheckIndexOutOfRange(ListBox listBox, TextBox textBox, List<TextPackage> packs, string attrName)
		{
			// 未選択が選択された場合とありえない場所が選択された場合は無視する
			int selected_index = listBox.SelectedIndex;
			if (CheckIndexOutOfRange(listBox))
			{
				RichTextMethod.EnabledTextAsCheckOverZero(listBox, textBox);
				textBox.Text = SubstitutePackageForTextBox(packs, selected_index, attrName);
			}
		}

		private static string SubstitutePackageForTextBox(List<TextPackage> packs, int selectedIndex, string attrName)
		{
			try
			{
				return packs[selectedIndex][attrName];
			}
			catch (KeyNotFoundException)
			{
				packs[selectedIndex][attrName] = "";
				return "";
			}
		}

		public static bool CheckIndexOutOfRange(ListBox listBox)
		{
			int selected_index = listBox.SelectedIndex;
			return selected_index != -1 && selected_index < listBox.Items.Count;
		}

		public static bool CheckSelectedIndexOutOfRange(ListBox listBox)
		{
			// 選択項目が閾値かチェックする
			int selected_index = listBox.SelectedIndex;
			if (selected_index >= 0 && selected_index < listBox.Items.Count)
			{
				return true;
			}
			return false;
		}

		public static void SwapItem(ListBox listBox, List<TextPackage> packs, int a, int b)
		{
			{
				var tmp = listBox.Items[a];
				listBox.Items[a] = listBox.Items[b];
				listBox.Items[b] = tmp;
			}
			{
				var tmp = packs[a];
				packs[a] = packs[b];
				packs[b] = tmp;
			}
		}
	}
}
