using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotePadThenuka
{
    public partial class MainNotepadfrm : Form
    {
        String hash = "46yhy6yh@1";
        List<String> paths;
        RichTextBox rtb;
        List<TextBoxData> textBoxList = new List<TextBoxData>();
        public MainNotepadfrm()
        {
            InitializeComponent();
            CreateNew();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("All Rights Reserved By Author", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNew(); //Create new Note
        }



        private void wordCount()
        {

        }


        private void MainNotepadfrm_Load(object sender, EventArgs e)
        {

        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Undo();
            undoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem.Enabled = true;

        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Redo();
            undoToolStripMenuItem.Enabled = true;
            redoToolStripMenuItem.Enabled = false;

        }

        private void MainRichTextBox_TextChanged(object sender, EventArgs e)
        {
            if (MainRichTextBox.Text.Length > 0)
            {
                undoToolStripMenuItem.Enabled = true;
                cutToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
                selectAllToolStripMenuItem.Enabled = true;
            }
            else
            {
                cutToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
                selectAllToolStripMenuItem.Enabled = false;
                undoToolStripMenuItem.Enabled = false;
                redoToolStripMenuItem.Enabled = false;

            }
        }
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFile();
        }
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Cut();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Copy();
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Paste();
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectedText = "";
        }

        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectAll();
        }
        private void OpenToolStripButton_Click(object sender, EventArgs e)
        {
            openFile(); //Open File Method Call
        }

        private void openFile()
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.Filter = "text Files (.text)|*.txt*";
                openDialog.Title = "Open File";
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    System.IO.StreamReader reader = new System.IO.StreamReader(openDialog.FileName);
                    GetCurrentText().Text = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }
        }

        private RichTextBox GetCurrentText() //Getting Active TextBox
        {
            try
            {
                RichTextBox currentTab = null;
                TabPage mainTab = tabControl1.SelectedTab;
                if (mainTab != null)
                {
                    currentTab = mainTab.Controls[0] as RichTextBox;

                }
                return currentTab;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Occured");
            }
            return null;
        }

        private void CreateNew() //Create New Tab
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => CreateNew()));
            }
            else
            {
                TabPage page = new TabPage("New Note");
                RichTextBox richTextBox = new RichTextBox();
                richTextBox.Dock = DockStyle.Fill;
                page.Controls.Add(richTextBox);
                tabControl1.TabPages.Add(page);
             
               
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs(GetCurrentText());
        }

        private void SaveAs(RichTextBox box)
        {
            try
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "text Files (.text) | .txt";
                save.Title = "Save File";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    System.IO.StreamWriter writer = new System.IO.StreamWriter(save.FileName);
                    writer.Write(box.Text);
                    writer.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }
        private RichTextBox getRichTextbox()
        {
            RichTextBox currentTextBox = null;
            TabPage mainTab = tabControl1.SelectedTab;

            if (mainTab != null)
            {
                currentTextBox = mainTab.Controls[0] as RichTextBox;
            }
            return currentTextBox;
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveData(); //Calling Save File Method
        }

        private void saveData()
        {
            try
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "text Files (.text)| *.txt*";
                saveFile.Title = "Save File";

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    System.IO.StreamWriter writer = new System.IO.StreamWriter(saveFile.FileName);
                    writer.Write(getRichTextbox().Text);
                    writer.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }

        public class TextBoxData
        {
            public int Code { get; set; }
            public string Filepath { get; set; }
            public RichTextBox txtBox { get; set; }
            public TextBoxData(int code, string filePath, RichTextBox textbox)
            {
                this.Code = code;
                this.Filepath = filePath;
                this.txtBox = textbox;
            }

            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            TabPage CurrentTabPage = tabControl1.SelectedTab;
            if (tabControl1.TabPages.Count > 1)
            {
                tabControl1.TabPages.Remove(CurrentTabPage);
            }
            else
            {
                MessageBox.Show("Cant Close Last Tab", "Error");
            }
        }

        private void NewToolStripButton_Click(object sender, EventArgs e)
        {
            CreateNew(); //Create New  from the toolstrip
        }

        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            saveData(); //Save note from the toolstrip
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => GetWordCount());
            GetLineCount();
        }

        private void GetWordCount()
        {
           if(this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => GetWordCount()));

            }
            else
            {
                try
                {
                    char[] delimiters = (" " + Environment.NewLine).ToCharArray();
                    int words = GetCurrentText().Text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
                    textBox1.Text = "Word Count: " + words.ToString();
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message, "Error");
                }
            }
        }

        private void WordCounter_TextChanged(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => GetWordCount());
            
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void GetLineCount()
        {
            try
            {
                int position = GetCurrentText().SelectionStart;
                int line = GetCurrentText().GetLineFromCharIndex(position) + 1;
                int col = position - GetCurrentText().GetFirstCharIndexOfCurrentLine() + 1;
                textBox2.Text = "Line: " + line + "  Column: " + col;

            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            GetCurrentText().SelectionFont = new Font(tabControl1.Font, System.Drawing.FontStyle.Bold);
        }

        private void ToolStripButton3_Click(object sender, EventArgs e)
        {
            GetCurrentText().SelectionFont = new Font(tabControl1.Font, System.Drawing.FontStyle.Regular);
        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            GetCurrentText().SelectionFont = new Font(tabControl1.Font, System.Drawing.FontStyle.Italic);
        }

        private void EncryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EncryptData(); //Calling encrypt method
        }

        private void EncryptData()
        {

            try
            {
                byte[] data = UTF8Encoding.UTF8.GetBytes(GetCurrentText().Text);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateEncryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        GetCurrentText().Text = Convert.ToBase64String(results, 0, results.Length);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }

        private void DecryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DecryptData(); //Decypting Data
        }

        private void DecryptData()
        {
            try
            {
                byte[] data = Convert.FromBase64String(GetCurrentText().Text);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateDecryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        TabPage newtab = new TabPage(GetCurrentText().Text = UTF8Encoding.UTF8.GetString(results));

                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }

        private void ToolStripButton4_Click(object sender, EventArgs e)
        {
            GetCurrentText().SelectionFont = new Font(tabControl1.Font, System.Drawing.FontStyle.Underline);

        }
    }
}
