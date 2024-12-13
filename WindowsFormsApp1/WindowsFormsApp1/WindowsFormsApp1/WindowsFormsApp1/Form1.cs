using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        int deletedStringIndex = -1; // Lưu vị trí của ký tự vừa bị xóa
        string deletedString;
        object content;
        object position;
        public void New_Text(object a)
        {
            string selectedText = a.ToString();
            if (!string.IsNullOrEmpty(selectedText))
            {
                // Thêm vào stack
                if (Clipboard.top == null)
                {
                    Clipboard.Push(selectedText);
                    // Hiển thị trong richtextbox clipboard              
                    string currentContent = richTextBox2.Text;
                    richTextBox2.Clear();
                    richTextBox2.AppendText(selectedText + '\n' + '\n' + currentContent);
                }
                else if (selectedText.CompareTo(Clipboard.Peek().data) != 0)
                {
                    Clipboard.Push(selectedText);
                    // Hiển thị trong richtextbox clipboard              
                    string currentContent = richTextBox2.Text;
                    richTextBox2.Clear();
                    richTextBox2.AppendText(selectedText + '\n' + '\n' + currentContent);
                }
            }
        }
        public void Paste(object a)
        {
            string word_need_to_paste = a.ToString();
            if (richTextBox1.SelectedText != null)
            {
                richTextBox1.SelectedText = word_need_to_paste;
            }
            else
            {
                int selectionStart = richTextBox1.SelectionStart;
                // Chèn văn bản từ Clipboard tại vị trí con trỏ
                richTextBox1.Text = richTextBox1.Text.Insert(selectionStart, word_need_to_paste);
                // Đặt lại con trỏ ngay sau đoạn văn bản vừa được chèn
                richTextBox1.SelectionStart = selectionStart + word_need_to_paste.Length;
            }
        }
        public void Paste2(object a, int b)
        {
            string word_need_to_undo = a.ToString();
            int place_to_paste = b;
                // Chèn văn bản từ Clipboard tại vị trí con trỏ
                richTextBox1.Text = richTextBox1.Text.Insert(place_to_paste, word_need_to_undo);
                // Đặt lại con trỏ ngay sau đoạn văn bản vừa được chèn
                richTextBox1.SelectionStart = place_to_paste + word_need_to_undo.Length;            
        }
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true; // Để form nhận các sự kiện phím trước
            this.KeyDown += MainForm_KeyDown;
        }
        public class Node
        {
            public Node next;
            public object data;
        }
        // this class stack plays a role of a clipboard to store the copied data temporarily
        public class MyStack
        {
            public Node top;
            public bool IsEmpty()
            {
                return top == null;
            }
            public void Push(object ele)
            {
                Node n = new Node();
                n.data = ele;
                n.next = top;
                top = n;
            }
            public Node Pop()
            {
                if (top == null)
                    return null;
                Node d = top;
                top = top.next;
                return d;
            }
            public void Clear()
            {
                while (!this.IsEmpty())
                {
                    this.Pop();
                }
            }
            public Node Peek()
            {
                return this.top;
            }
        }
        public MyStack Clipboard = new MyStack();
        public MyStack Undo_content = new MyStack();
        public MyStack Undo_position = new MyStack();
        public MyStack Redo_content = new MyStack();
        public MyStack Redo_position = new MyStack();
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.BtnPaste = new System.Windows.Forms.Button();
            this.BtnCut = new System.Windows.Forms.Button();
            this.BtnClear = new System.Windows.Forms.Button();
            this.BtnCopy = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Location = new System.Drawing.Point(-4, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(683, 58);
            this.panel1.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(197, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(297, 50);
            this.label2.TabIndex = 12;
            this.label2.Text = "Clipboard Stack";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::WindowsFormsApp1.Properties.Resources.image_psd__1_;
            this.pictureBox2.Location = new System.Drawing.Point(3, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(680, 58);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(128, 320);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 50);
            this.label1.TabIndex = 9;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label4.Location = new System.Drawing.Point(430, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 31);
            this.label4.TabIndex = 3;
            this.label4.Text = "Clipboard";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label3.Location = new System.Drawing.Point(12, 342);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 28);
            this.label3.TabIndex = 15;
            this.label3.Click += new System.EventHandler(this.label3_Click_1);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(437, 404);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(123, 28);
            this.textBox1.TabIndex = 16;
            this.textBox1.Text = "Tìm kiếm lịch sử";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Font = new System.Drawing.Font("MS Reference Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.button1.Location = new System.Drawing.Point(566, 404);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 28);
            this.button1.TabIndex = 17;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 114);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(417, 349);
            this.richTextBox1.TabIndex = 19;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label5.Location = new System.Drawing.Point(11, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(251, 31);
            this.label5.TabIndex = 20;
            this.label5.Text = "Entering New Content";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(437, 438);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(218, 72);
            this.textBox2.TabIndex = 21;
            this.textBox2.Text = "Kết quả tìm kiếm";
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(437, 117);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(242, 281);
            this.richTextBox2.TabIndex = 22;
            this.richTextBox2.Text = "";
            this.richTextBox2.TextChanged += new System.EventHandler(this.richTextBox2_TextChanged);
            // 
            // button2
            // 
            this.button2.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = global::WindowsFormsApp1.Properties.Resources.anh113;
            this.button2.Location = new System.Drawing.Point(257, 68);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(33, 33);
            this.button2.TabIndex = 0;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button3
            // 
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Image = global::WindowsFormsApp1.Properties.Resources.anh121;
            this.button3.Location = new System.Drawing.Point(296, 68);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(33, 33);
            this.button3.TabIndex = 24;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // BtnPaste
            // 
            this.BtnPaste.Image = global::WindowsFormsApp1.Properties.Resources.Dự_án_mới__1_6;
            this.BtnPaste.Location = new System.Drawing.Point(233, 471);
            this.BtnPaste.Name = "BtnPaste";
            this.BtnPaste.Size = new System.Drawing.Size(91, 40);
            this.BtnPaste.TabIndex = 4;
            this.BtnPaste.Text = "Paste";
            this.BtnPaste.Click += new System.EventHandler(this.BtnPaste_Click);
            // 
            // BtnCut
            // 
            this.BtnCut.Image = global::WindowsFormsApp1.Properties.Resources.Dự_án_mới__1_7;
            this.BtnCut.Location = new System.Drawing.Point(127, 471);
            this.BtnCut.Name = "BtnCut";
            this.BtnCut.Size = new System.Drawing.Size(89, 40);
            this.BtnCut.TabIndex = 2;
            this.BtnCut.Text = "Cut";
            this.BtnCut.Click += new System.EventHandler(this.BtnCut_Click);
            // 
            // BtnClear
            // 
            this.BtnClear.BackColor = System.Drawing.SystemColors.Control;
            this.BtnClear.FlatAppearance.BorderSize = 0;
            this.BtnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.BtnClear.ForeColor = System.Drawing.SystemColors.Desktop;
            this.BtnClear.Image = global::WindowsFormsApp1.Properties.Resources.Dự_án_mới__1_4;
            this.BtnClear.Location = new System.Drawing.Point(340, 471);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(89, 42);
            this.BtnClear.TabIndex = 5;
            this.BtnClear.Text = "Clear";
            this.BtnClear.UseVisualStyleBackColor = false;
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnCopy
            // 
            this.BtnCopy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.BtnCopy.FlatAppearance.BorderSize = 0;
            this.BtnCopy.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BtnCopy.Image = global::WindowsFormsApp1.Properties.Resources.Dự_án_mới__1_5;
            this.BtnCopy.Location = new System.Drawing.Point(18, 469);
            this.BtnCopy.Name = "BtnCopy";
            this.BtnCopy.Size = new System.Drawing.Size(91, 42);
            this.BtnCopy.TabIndex = 3;
            this.BtnCopy.Text = "Copy";
            this.BtnCopy.Click += new System.EventHandler(this.BtnCopy_Click);
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(683, 522);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BtnPaste);
            this.Controls.Add(this.BtnCut);
            this.Controls.Add(this.BtnClear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnCopy);
            this.Name = "Form1";
            this.Text = "Clipboard Stack";
            this.MouseEnter += new System.EventHandler(this.Form1_MouseEnter);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private void DeleteTextAt(int startIndex, int length)
        {
            // Kiểm tra nếu startIndex và length hợp lệ
            if (startIndex >= 0 && startIndex + length <= richTextBox1.TextLength)
            {
                // Di chuyển con trỏ đến vị trí bắt đầu cần xóa
                richTextBox1.Select(startIndex, length);
                richTextBox1.SelectedText = string.Empty;
            }
        }
            private void BtnCut_Click(object sender, EventArgs e)
        {
            string selectedText = richTextBox1.SelectedText;
            New_Text(selectedText);
            int cursorPosition = richTextBox1.SelectionStart; // Vị trí con trỏ hiện tại

            if (cursorPosition >= 0) // Chỉ xử lý nếu không ở đầu văn bản
            {
                deletedStringIndex = cursorPosition; // Vị trí của chuỗi bị xóa
                                                     //deletedString = richTextBox1.Text.Substring(deletedStringIndex, selectedText.Length); // Lấy chuỗi bị xóa

                Undo_content.Push(selectedText);
                Undo_position.Push(deletedStringIndex);
            }
            richTextBox1.SelectedText = string.Empty;
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            /*char[] letter = (richTextBox1.SelectedText.ToString().ToCharArray());
            MyStack Clipboard = new MyStack();           
            for (int i = letter.Length - 1; i >= 0; i--)
            {
                Clipboard.Push(letter[i]);
            }
        string history_word = null;
        while (!Clipboard.IsEmpty())
        {
            history_word = history_word + Clipboard.Pop().data.ToString();

        }
        History.Add(history_word);
        if(History.Count == 1)
        {
            ListBoxStack.Items.Insert(0, History[History.Count - 1]);
        }
        if(History.Count > 1)
        {
            if (History[History.Count - 1] != History[History.Count - 2])
            {
                ListBoxStack.Items.Insert(0, History[History.Count - 1]);
            }    
        }
        ListBoxStack.Items.Clear();
        for (int i = History.Count -1; i>= 0; i--) 
        {

            ListBoxStack.Items.Add(history_word);
        }*/
            string selectedText = richTextBox1.SelectedText;
            New_Text(selectedText);
        }


        private void BtnPaste_Click(object sender, EventArgs e)
        {
            //if (Clipboard.ContainsText())
            //{
            //   richTextBox1.Text = Clipboard.GetText();
            //}
            /*if (History.Count == 0)
            {
                richTextBox1.Text = "";
                return;
            }*/


            /*int selectionStart = richTextBox1.SelectionStart;
            string word_need_to_paste = History[History.Count - 1];
            if (richTextBox1.SelectedText != null)
            {
                richTextBox1.SelectedText = word_need_to_paste;
            }
            else
            {
                richTextBox1.Text = richTextBox1.Text.Insert(selectionStart, word_need_to_paste);

                // Đặt lại con trỏ ngay sau đoạn văn bản vừa được chèn
                richTextBox1.SelectionStart = selectionStart + word_need_to_paste.Length;
            }*/
            if (Clipboard.IsEmpty()) { MessageBox.Show("Clipboard trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                string word_need_to_paste = Clipboard.Peek().data.ToString();
                Paste(word_need_to_paste);
            }
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void Form1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void ListBoxResults_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            Clipboard.Clear();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //string keyword = textBox1.Text.ToLower();

            //// Chuyển đổi các mục trong ListBox thành danh sách chuỗi và lọc
            //var filteredItems = richTextBox2
            //    .Cast<string>() // Chuyển các mục sang kiểu chuỗi
            //    .Where(item => item.ToLower().Contains(keyword)) // Lọc các mục phù hợp
            //    .ToList();

            //// Hiển thị kết quả (nếu cần)
            //if (filteredItems.Any())
            //{
            //    textBox2.Text = "Kết quả: " + string.Join(", ", filteredItems);
            //}
            //else
            //{
            //    textBox2.Text = "Không tìm thấy kết quả.";

            //    string keyword = textBox1.Text.ToLower(); // Lấy từ khóa và chuyển về chữ thường để tìm không phân biệt chữ hoa chữ thường.

            //    if (string.IsNullOrEmpty(keyword))
            //    {
            //        textBox2.Text = "Vui lòng nhập từ khóa để tìm.";
            //        return;
            //    }

            // Lấy nội dung từ richTextBox2
            string content = richTextBox2.Text;
            string keyword = textBox1.Text.ToLower();

            // Dùng LINQ để tìm các dòng chứa từ khóa
            var filteredLines = content
                    .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries) // Tách văn bản thành từng dòng
                    .Where(line => line.ToLower().Contains(keyword)) // Lọc các dòng chứa từ khóa
                    .ToList();

            // Hiển thị kết quả
            if (filteredLines.Any())
            {
                textBox2.Text = string.Join(Environment.NewLine, filteredLines);
            }
            else
            {
                textBox2.Text = "Không tìm thấy kết quả.";
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            string selectedText = richTextBox1.SelectedText;
            // Kiểm tra nếu Ctrl + C được nhấn
            if (e.Control && e.KeyCode == Keys.D)
            {
                New_Text(selectedText);
            }
            if (e.Control && e.KeyCode == Keys.S)
            {
                New_Text(selectedText);
                int cursorPosition = richTextBox1.SelectionStart; // Vị trí con trỏ hiện tại

                if (cursorPosition >= 0) // Chỉ xử lý nếu không ở đầu văn bản
                {
                    deletedStringIndex = cursorPosition; // Vị trí của chuỗi bị xóa
                    //deletedString = richTextBox1.Text.Substring(deletedStringIndex, selectedText.Length); // Lấy chuỗi bị xóa

                    Undo_content.Push(selectedText);
                    Undo_position.Push(deletedStringIndex);
                }
                richTextBox1.SelectedText = string.Empty;      
            }
            if (e.Control && e.KeyCode == Keys.F)
            {
                if (Clipboard.IsEmpty()) { MessageBox.Show("Clipboard trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                else
                {
                    string word_need_to_paste = Clipboard.Peek().data.ToString();
                    Paste(word_need_to_paste);
                }
            }
            if (e.KeyCode == Keys.Back)
            {
                int cursorPosition = richTextBox1.SelectionStart; // Vị trí con trỏ hiện tại

                if (cursorPosition >= 0) // Chỉ xử lý nếu không ở đầu văn bản
                {
                    deletedStringIndex = cursorPosition; // Vị trí của chuỗi bị xóa
                    //deletedString = richTextBox1.Text.Substring(deletedStringIndex, selectedText.Length); // Lấy chuỗi bị xóa

                    Undo_content.Push(selectedText);
                    Undo_position.Push(deletedStringIndex);
                }
            }
            if (e.Control && e.KeyCode == Keys.Q)
            {
                if((string)Undo_content.top.data != "")
                {
                    content = Undo_content.Pop().data;
                   position = Undo_position.Pop().data;
                    Paste2(content.ToString(), (int)position);
                   Redo_content.Push(content);
                    Redo_position.Push(position);
                }    
            }
            if (e.Control && e.KeyCode == Keys.T)
            {
                if (Redo_content.top != null)
                {
                    content = Redo_content.Pop().data;
                   position = Redo_position.Pop().data;
                    DeleteTextAt((int)position, content.ToString().Length);
                    Undo_content.Clear();
                    Undo_position.Clear();
                    Undo_content.Push(content);
                    Undo_position.Push(position);
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            
            {
                if ((string)Undo_content.top.data != "")
                {
                    content = Undo_content.Pop().data;
                    position = Undo_position.Pop().data;
                    Paste2(content.ToString(), (int)position);
                    Redo_content.Push(content);
                    Redo_position.Push(position);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Redo_content.top != null)
            {
                content = Redo_content.Pop().data;
                position = Redo_position.Pop().data;
                DeleteTextAt((int)position, content.ToString().Length);
                //Undo_content.Clear();
                //Undo_position.Clear();
                Undo_content.Push(content);
                Undo_position.Push(position);
            }
        }
    } 
}
