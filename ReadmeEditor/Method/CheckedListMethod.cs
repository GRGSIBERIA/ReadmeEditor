using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReadmeEditor
{
	public class CheckedListMethod
	{
		public static void SelectCheckBox(CheckedListBox listBox, RichTextBox licenseTextBox, List<TextPackage> licenseCaptions)
		{
			if (listBox.SelectedIndex != -1)
			{
				int selected_index = listBox.SelectedIndex;
				licenseTextBox.Text = licenseCaptions[selected_index]["Full"];
			}
		}

		public static TextPackage[] GetSelectedItems(CheckedListBox listBox, List<TextPackage> licenseCaptions)
		{
			List<TextPackage> packs = new List<TextPackage>();
			foreach (int index in listBox.CheckedIndices)
				packs.Add(licenseCaptions[index]);
			return packs.ToArray();
		}
	}
}
