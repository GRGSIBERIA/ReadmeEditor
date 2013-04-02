using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReadmeEditor
{
	public class RichTextMethod
	{
		public static void DisabledTextAsCheckZeroItems(ListBox listBox, RichTextBox textBox)
		{
			textBox.Enabled = DisabledText(listBox);
		}

		public static void DisabledTextAsCheckZeroItems(ListBox listBox, TextBox textBox)
		{
			textBox.Enabled = DisabledText(listBox);
		}

		private static bool DisabledText(ListBox listBox)
		{
			if (listBox.Items.Count <= 0 || listBox.SelectedIndex == -1)
			{
				return false;
			}
			return true;
		}

		public static void EnabledTextAsCheckOverZero(ListBox listBox, RichTextBox textBox)
		{
			textBox.Enabled = EnabledText(listBox);
		}

		public static void EnabledTextAsCheckOverZero(ListBox listBox, TextBox textBox)
		{
			textBox.Enabled = EnabledText(listBox);
		}

		private static bool EnabledText(ListBox listBox)
		{
			if (listBox.Items.Count > 0)
			{
				return true;
			}
			return false;
		}

		public static bool ChangeTextAsCheckIndexOutOfRange(ListBox listBox, RichTextBox textBox, List<TextPackage> packs, string attrName)
		{
			return ChangeText(listBox, textBox.Text, textBox.Enabled, packs, attrName);
		}

		public static bool ChangeTextAsCheckIndexOutOfRange(ListBox listBox, TextBox textBox, List<TextPackage> packs, string attrName)
		{
			return ChangeText(listBox, textBox.Text, textBox.Enabled, packs, attrName);
		}

		private static bool ChangeText(ListBox listBox, string text, bool enabledText, List<TextPackage> packs, string attrName)
		{
			int selected_index = listBox.SelectedIndex;
			if (selected_index != -1 && enabledText)	// enabledは削除した時のテキストボックスが変更されたときの対策
			{
				packs[selected_index][attrName] = text;
				return true;
			}
			return false;
		}
	}
}
