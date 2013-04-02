using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO.Compression;
using System.IO;
using System.Diagnostics;

using Microsoft.VisualBasic;

namespace ReadmeEditor
{
	public partial class MainForm : Form
	{
		private List<TextPackage> updateCaption;
		private List<TextPackage> howtoCaption;
		private List<TextPackage> derivateCaption;
		private List<TextPackage> licenseCaption;
		private List<TextPackage> remarkCaption;

		private bool saveFlag;
		private string filePath;

		public MainForm()
		{
			InitializeComponent();

			InitializeVariables();
		}

		private void InitializeVariables()
		{
			ClearControlls();

			updateCaption = new List<TextPackage>();
			howtoCaption = new List<TextPackage>();
			derivateCaption = new List<TextPackage>();
			remarkCaption = new List<TextPackage>();

			RichTextMethod.DisabledTextAsCheckZeroItems(updateListBox, updateTextBox);
			RichTextMethod.DisabledTextAsCheckZeroItems(howtoListBox, howtoTextBox);
			RichTextMethod.DisabledTextAsCheckZeroItems(derivateListBox, derivateCommentTextBox);
			RichTextMethod.DisabledTextAsCheckZeroItems(derivateListBox, derivateLinkTextBox);
			RichTextMethod.DisabledTextAsCheckZeroItems(remarkListBox, remarkTextBox);

			// ライセンスの種類と全文をロードして，チェックボックスへ入れる
			licenseCaption = LicenseCaptionMethod.Create();
			foreach (var item in licenseCaption)
			{
				licenseCheckedListBox.Items.Add(item["Type"]);
			}

			EnableSaveFlag();
			filePath = "";
		}

		private void ClearControlls()
		{
			author.Clear();
			developedName.Clear();
			version.Clear();
			createdAt.Text = DateTime.Now.ToString();
			captionTextBox.Clear();
			howtoListBox.Items.Clear();
			howtoTextBox.Clear();
			howtoTextBox.Enabled = false;
			updateListBox.Items.Clear();
			updateTextBox.Clear();
			updateTextBox.Enabled = false;
			derivateListBox.Items.Clear();
			derivateLinkTextBox.Clear();
			derivateLinkTextBox.Enabled = false;
			derivateCommentTextBox.Clear();
			derivateCommentTextBox.Enabled = false;
			licenseCheckedListBox.Items.Clear();
			licenseTextBox.Clear();
			remarkListBox.Items.Clear();
			remarkTextBox.Clear();
			remarkTextBox.Enabled = false;
		}

		private AttributePackage PackingAttributes()
		{
			// パッケージに書き込みを行う
			AttributePackage pack = new AttributePackage();
			pack.Author = author.Text;
			pack.OpenPass = openPass.Text;
			pack.Title = developedName.Text;
			pack.CreatedAt = createdAt.Text;
			pack.Email = mailAddress.Text;
			pack.Caption = captionTextBox.Text;
			pack.Version = version.Text;
			pack.Update = updateCaption.ToArray();
			pack.Howto = howtoCaption.ToArray();
			pack.Derivate = derivateCaption.ToArray();
			pack.Remark = remarkCaption.ToArray();
			pack.License = CheckedListMethod.GetSelectedItems(licenseCheckedListBox, licenseCaption);
			return pack;
		}

		private void EnableSaveFlag()
		{
			if (!saveFlag)
			{
				saveFlag = true;
				this.Text = "ReadmeEditor";
			}
		}

		private void DisableSaveFlag()
		{
			if (saveFlag)
			{
				saveFlag = false;
				Text += "*";
			}
		}

		private void 新規作成ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool new_file_flag = true;
			if (saveFlag == false)
			{
				if (MessageBox.Show("保存していません，新規作成しますか？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
					new_file_flag = false;
			}
			if (new_file_flag)
			{
				InitializeVariables();
				EnableSaveFlag();
			}
		}

		//////////////////////////////////
		// 暗号化周り
		//////////////////////////////////

		private void copyOpenPass_Click(object sender, EventArgs e)
		{
			// クリップボードへ貼り付け
			Clipboard.SetText(this.openPass.Text);
		}

		private void secretPass_Leave(object sender, EventArgs e)
		{
			// 作者名と秘密の鍵で暗号化する
			this.openPass.Text = Cryptograph.Encrypt(this.author.Text, this.secretPass.Text);
		}

		private void secretPass_KeyDown(object sender, KeyEventArgs e)
		{
			// エンターキーが押されると更新
			if (e.KeyCode == Keys.Enter)
			{
				this.openPass.Text = Cryptograph.Encrypt(this.author.Text, this.secretPass.Text);
			}
		}

		//////////////////////////////////
		// テキスト書き出し
		//////////////////////////////////

		// テキスト形式の書き出しを行った場合のイベント
		private void テキストtxtToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AttributePackage pack = PackingAttributes();
		}

		//////////////////////////////////
		// 追加
		//////////////////////////////////

		private void 追加ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ContextMenuMethod.AddListBox(updateListBox, updateCaption))
				DisableSaveFlag();
		}

		private void 追加ToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (ContextMenuMethod.AddListBox(howtoListBox, howtoCaption))
				DisableSaveFlag();
		}

		private void 追加ToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			if (ContextMenuMethod.AddListBox(derivateListBox, derivateCaption, false))
				DisableSaveFlag();
		}

		private void 追加ToolStripMenuItem3_Click(object sender, EventArgs e)
		{
			if (ContextMenuMethod.AddListBox(remarkListBox, remarkCaption, false))
				DisableSaveFlag();
		}

		//////////////////////////////////
		// 削除
		//////////////////////////////////
		
		private void 削除ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ContextMenuMethod.DeleteItemAsCheckingDelete(updateListBox, updateTextBox, updateCaption))
				DisableSaveFlag();
		}

		private void 削除ToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (ContextMenuMethod.DeleteItemAsCheckingDelete(howtoListBox, howtoTextBox, howtoCaption))
				DisableSaveFlag();
		}

		private void 削除ToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			if (ContextMenuMethod.DeleteListBox(derivateListBox, derivateCaption))
			{
				DisableSaveFlag();
				derivateLinkTextBox.Enabled = false;
				derivateCommentTextBox.Enabled = false;
			}
		}

		private void 削除ToolStripMenuItem3_Click(object sender, EventArgs e)
		{
			if (ContextMenuMethod.DeleteItemAsCheckingDelete(remarkListBox, remarkTextBox, remarkCaption))
				DisableSaveFlag();
		}

		//////////////////////////////////
		// 名前の変更
		//////////////////////////////////

		private void 名前の変更ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ContextMenuMethod.RenameListBox(updateListBox, updateCaption))
				DisableSaveFlag();
		}

		private void 名前の変更ToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (ContextMenuMethod.RenameListBox(howtoListBox, howtoCaption))
				DisableSaveFlag();
		}

		private void 名前の変更ToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			if (ContextMenuMethod.RenameListBox(derivateListBox, derivateCaption))
				DisableSaveFlag();
		}

		private void 名前の変更ToolStripMenuItem3_Click(object sender, EventArgs e)
		{
			if (ContextMenuMethod.RenameListBox(remarkListBox, remarkCaption))
				DisableSaveFlag();
		}

		//////////////////////////////////
		// 項目の選択
		//////////////////////////////////

		private void howtoListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ListBoxMethod.SelectItemAsCheckIndexOutOfRange(howtoListBox, howtoTextBox, howtoCaption, "Caption");
		}

		private void updateListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ListBoxMethod.SelectItemAsCheckIndexOutOfRange(updateListBox, updateTextBox, updateCaption, "Caption");
		}

		private void derivateListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ListBoxMethod.SelectItemAsCheckIndexOutOfRange(derivateListBox, derivateCommentTextBox, derivateCaption, "Comment");
			ListBoxMethod.SelectItemAsCheckIndexOutOfRange(derivateListBox, derivateLinkTextBox, derivateCaption, "Link");
		}

		private void remarkListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ListBoxMethod.SelectItemAsCheckIndexOutOfRange(remarkListBox, remarkTextBox, remarkCaption, "Caption");
		}

		//////////////////////////////////
		// テキストボックスの変更
		//////////////////////////////////

		private void howtoTextBox_TextChanged(object sender, EventArgs e)
		{
			if (RichTextMethod.ChangeTextAsCheckIndexOutOfRange(howtoListBox, howtoTextBox, howtoCaption, "Caption"))
				DisableSaveFlag();
		}

		private void updateTextBox_TextChanged(object sender, EventArgs e)
		{
			if (RichTextMethod.ChangeTextAsCheckIndexOutOfRange(updateListBox, updateTextBox, updateCaption, "Caption"))
				DisableSaveFlag();
		}

		private void derivedCommentTextBox1_TextChanged(object sender, EventArgs e)
		{
			if (RichTextMethod.ChangeTextAsCheckIndexOutOfRange(derivateListBox, derivateCommentTextBox, derivateCaption, "Comment"))
				DisableSaveFlag();
		}

		private void remarkTextBox_TextChanged(object sender, EventArgs e)
		{
			if (RichTextMethod.ChangeTextAsCheckIndexOutOfRange(remarkListBox, remarkTextBox, remarkCaption, "Caption"))
				DisableSaveFlag();
		}

		//////////////////////////////////
		// 選択項目を上へ移動させる
		//////////////////////////////////

		private void 上へ移動ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ContextMenuMethod.LiftUpFromSelectedItem(updateListBox, updateCaption))
				DisableSaveFlag();
		}

		private void 上へ移動ToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (ContextMenuMethod.LiftUpFromSelectedItem(howtoListBox, howtoCaption))
				DisableSaveFlag();
		}

		private void 上へ移動ToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			if (ContextMenuMethod.LiftUpFromSelectedItem(derivateListBox, derivateCaption))
				DisableSaveFlag();
		}

		private void 上へ移動ToolStripMenuItem3_Click(object sender, EventArgs e)
		{
			if (ContextMenuMethod.LiftUpFromSelectedItem(remarkListBox, remarkCaption))
				DisableSaveFlag();
		}

		//////////////////////////////////
		// 選択項目を下へ移動させる
		//////////////////////////////////

		private void 下へ移動ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ContextMenuMethod.LiftDownFromSelectedItem(updateListBox, updateCaption)) 
				DisableSaveFlag();
		}

		private void 下へ移動ToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (ContextMenuMethod.LiftDownFromSelectedItem(howtoListBox, howtoCaption))
				DisableSaveFlag();
		}

		private void 下へ移動ToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			if (ContextMenuMethod.LiftDownFromSelectedItem(derivateListBox, derivateCaption))
				DisableSaveFlag();
		}

		private void 下へ移動ToolStripMenuItem3_Click(object sender, EventArgs e)
		{
			if (ContextMenuMethod.LiftDownFromSelectedItem(remarkListBox, remarkCaption))
				DisableSaveFlag();
		}

		//////////////////////////////////
		// 保存する関連
		//////////////////////////////////

		private void 名前を付けて保存ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveAsDialog();
		}

		private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (filePath != "")
			{
				Stream stream = new FileStream(filePath, FileMode.Truncate, FileAccess.Write);
				if (stream != null)
				{
					SaveOnCrashCheck(stream, filePath);
					stream.Close();
				}
				else
				{
					FailedSaveMessage();
				}
			}
			else
			{
				// 保存するときにパスを持ってなければ，ダイアログを出して保存先を指定する
				SaveAsDialog();
			}
		}

		private void FailedSaveMessage()
		{
			MessageBox.Show("ファイルの保存に失敗しました．\n変更を加えた後，もう一度保存をやり直してみてください．", "保存に失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void SaveAsDialog()
		{
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				Stream stream = saveFileDialog.OpenFile();
				if (stream != null)
				{
					SaveOnCrashCheck(stream, saveFileDialog.FileName);
					stream.Close();
				}
				else
				{
					FailedSaveMessage();
				}
			}
		}

		private void SaveOnCrashCheck(Stream stream, string path)
		{
			try
			{
				WriteStream(stream, path);
				EnableSaveFlag();
			}
			catch
			{
				FailedSaveMessage();
			}
		}

		private void WriteStream(Stream stream, string path)
		{
			if (stream != null)
			{
				filePath = path;
				AttributePackage pack = PackingAttributes();
				SaveXml.Save(pack, stream);
			}
		}

		//////////////////////////////////
		// 開く関連
		//////////////////////////////////

		private void 開くToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool open_flag = false;
			if (saveFlag == false)
			{
				if (MessageBox.Show("保存されていません，ファイルを開きますか？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
				{
					open_flag = true;
				}
				// elseはキャンセルして開かないって意味
			}
			else
			{
				open_flag = true;
			}
			if (open_flag)
			{
				OpenXmlFileAsShowDialog();
			}
		}

		private void OpenXmlFileAsShowDialog()
		{
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				Stream stream = openFileDialog.OpenFile();
				var pack = OpenXml.Open(stream);
				stream.Close();

				InitializeVariables();
				filePath = openFileDialog.FileName;
				UnpackingAttributes(pack);
				EnableSaveFlag();
			}
		}

		private void UnpackingAttributes(AttributePackage pack)
		{
			// AttributePackageの中身をフォームへ流し込む
			author.Text = pack.Author;
			developedName.Text = pack.Title;
			createdAt.Text = pack.CreatedAt;
			captionTextBox.Text = pack.Caption;
			version.Text = pack.Version;
			howtoCaption = Unpacker.UnpackListBoxOnSection(pack.Howto, howtoListBox);
			updateCaption = Unpacker.UnpackListBoxOnSection(pack.Update, updateListBox);
			remarkCaption = Unpacker.UnpackListBox(pack.Remark, remarkListBox);
			derivateCaption = Unpacker.UnpackListBox(pack.Derivate, derivateListBox);
			Unpacker.UnpackLicense(pack.License, licenseCheckedListBox);
		}

		//////////////////////////////////
		// ライセンス関連
		//////////////////////////////////

		private void licenseCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// 項目が選択された時，ライセンス全文を表示させる
			CheckedListMethod.SelectCheckBox(licenseCheckedListBox, licenseTextBox, licenseCaption);
		}

		//////////////////////////////////
		// 借り物関連
		//////////////////////////////////

		private void derivateLinkTextBox_TextChanged(object sender, EventArgs e)
		{
			// リンクが変更された
			if (RichTextMethod.ChangeTextAsCheckIndexOutOfRange(derivateListBox, derivateLinkTextBox, derivateCaption, "Link"))
				DisableSaveFlag();
		}

		//////////////////////////////////
		// リンク関連
		//////////////////////////////////

		private void textBox_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			Process.Start(e.LinkText);
		}

		//////////////////////////////////
		// 情報関連
		//////////////////////////////////

		private void textBox_TextChangedAsDisableSave(object sender, EventArgs e)
		{
			DisableSaveFlag();
		}

		//////////////////////////////////
		// 終了関連
		//////////////////////////////////

		private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExitFormAsAskNotSave();
		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			ExitFormAsAskNotSave();
		}

		private void ExitFormAsAskNotSave()
		{
			if (!saveFlag)
			{
				if (MessageBox.Show("保存されていません，終了しますか？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
				{
					Application.Exit();
				}
			}
		}
	}
}
