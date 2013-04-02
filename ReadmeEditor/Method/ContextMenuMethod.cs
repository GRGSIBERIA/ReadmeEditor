using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace ReadmeEditor
{
	public class ContextMenuMethod
	{
		/// <summary>
		/// 入力された項目名から自動的にTextPackageを作成する
		/// </summary>
		/// <param name="item_name">section title形式の文字列</param>
		/// <param name="caption"></param>
		/// <returns></returns>
		private static TextPackage MakeTextPackage(string item_name, string caption)
		{
			var section = "";
			var title = "";
			var splited_str = item_name.Split('#');
			if (splited_str.Length > 1)
			{
				section = splited_str[0];
				title = item_name.Remove(0, section.Length + 1);
			}
			else
			{
				title = item_name;
			}
			var pack = new TextPackage();
			pack["Section"] = section;
			pack["Caption"] = caption;
			pack["Title"] = title;
			return pack;
		}

		/// <summary>
		/// リストボックスに項目を追加
		/// </summary>
		/// <param name="listBox">対象のリストボックス</param>
		public static bool AddListBox(ListBox listBox, List<TextPackage> packs, bool attension=true)
		{
			var msg = "";

			if (attension)
			{
				msg +=
					"追加する項目名を入れてください．\n" +
					"段落と項目名を#で区切ると，自動的に段落を設定します．\n" +
					"例：1.2.3#MMDの使い方\n";
			}
			else
			{
				msg += "追加する項目名を入れてください．";
			}
			var item_name = Interaction.InputBox(msg, "項目の追加");
			if (item_name != "")
			{
				listBox.Items.Add(item_name);
				packs.Add(ContextMenuMethod.MakeTextPackage(item_name, ""));
				return true;
			}
			return false;
		}

		public static bool DeleteItemAsCheckingDelete(ListBox listBox, RichTextBox textBox, List<TextPackage> packs)
		{
			// 先に無効化しないと削除したときにテキストボックスが変更される
			textBox.Enabled = false;
			if (!ContextMenuMethod.DeleteListBox(listBox, packs))
			{
				textBox.Enabled = true;	// キャンセルが押された
				return true;
			}
			else
			{
				textBox.Text = "";
				return false;
			}
		}

		/// <summary>
		/// リストボックスの項目を削除
		/// </summary>
		/// <param name="listBox">対象のリストボックス</param>
		public static bool DeleteListBox(ListBox listBox, List<TextPackage> packs)
		{
			string msg = "";
			if (listBox.SelectedItems.Count > 1)
			{
				msg += listBox.SelectedItems.Count.ToString() + "項目を削除します．";
			}
			else
			{
				msg += (string)listBox.SelectedItem + "を削除します．";
			}

			// 選択中の項目を削除する
			if (MessageBox.Show(
				msg + "元に戻せません．\n削除しますか？",
				listBox.SelectedItem + "の削除の確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				// 現在選択中の項目を削除する
				var items = listBox.SelectedItems;
				for (int i = 0; i < listBox.SelectedItems.Count; i++)
				{
					var index = listBox.SelectedIndices[i];
					listBox.Items.RemoveAt(index);
					packs.RemoveAt(index);
				}
				return true;
			}
			return false;
		}

		public static bool RenameListBox(ListBox listBox, List<TextPackage> packs)
		{
			var rename = Interaction.InputBox("変更する名前を入力してください．", (string)listBox.SelectedItem + "の名前を変更");
			if (rename != "")
			{
				int index = listBox.SelectedIndex;
				listBox.Items[index] = rename;
				packs[index] = ContextMenuMethod.MakeTextPackage(rename, packs[index]["Caption"]);
				return true;
			}
			return false;
		}

		public static bool LiftUpFromSelectedItem(ListBox listBox, List<TextPackage> packs)
		{
			// 選択項目を1段上げる
			if (ListBoxMethod.CheckSelectedIndexOutOfRange(listBox))
			{
				int selected_index = listBox.SelectedIndex;
				if (selected_index > 0)
				{
					ListBoxMethod.SwapItem(listBox, packs, selected_index, selected_index - 1);
					listBox.SelectedIndex = selected_index - 1;
					return true;
				}
			}
			return false;
		}

		public static bool LiftDownFromSelectedItem(ListBox listBox, List<TextPackage> packs)
		{
			// 選択項目を1段下げる
			if (ListBoxMethod.CheckSelectedIndexOutOfRange(listBox))
			{
				int selected_index = listBox.SelectedIndex;
				if (selected_index < listBox.Items.Count - 1)
				{
					ListBoxMethod.SwapItem(listBox, packs, selected_index, selected_index + 1);
					listBox.SelectedIndex = selected_index + 1;
					return true;
				}
			}
			return false;
		}
	}
}
