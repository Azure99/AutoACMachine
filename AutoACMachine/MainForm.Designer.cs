namespace AutoACMachine
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_Msg = new System.Windows.Forms.TextBox();
            this.button_Start = new System.Windows.Forms.Button();
            this.comboBox_OJ = new System.Windows.Forms.ComboBox();
            this.label_OJ = new System.Windows.Forms.Label();
            this.label_Crawler = new System.Windows.Forms.Label();
            this.label_Username = new System.Windows.Forms.Label();
            this.label_Password = new System.Windows.Forms.Label();
            this.label_StartID = new System.Windows.Forms.Label();
            this.label_EndID = new System.Windows.Forms.Label();
            this.textBox_Username = new System.Windows.Forms.TextBox();
            this.textBox_Password = new System.Windows.Forms.TextBox();
            this.textBox_StartID = new System.Windows.Forms.TextBox();
            this.textBox_EndID = new System.Windows.Forms.TextBox();
            this.comboBox_Crawler = new System.Windows.Forms.ComboBox();
            this.linkLabel_Blog = new System.Windows.Forms.LinkLabel();
            this.linkLabel_SourceCode = new System.Windows.Forms.LinkLabel();
            this.checkBox_SaveCode = new System.Windows.Forms.CheckBox();
            this.textBox_MaxRetry = new System.Windows.Forms.TextBox();
            this.label_MaxRetry = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_Msg
            // 
            this.textBox_Msg.Location = new System.Drawing.Point(12, 190);
            this.textBox_Msg.MaxLength = 32767000;
            this.textBox_Msg.Multiline = true;
            this.textBox_Msg.Name = "textBox_Msg";
            this.textBox_Msg.ReadOnly = true;
            this.textBox_Msg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_Msg.Size = new System.Drawing.Size(450, 437);
            this.textBox_Msg.TabIndex = 13;
            // 
            // button_Start
            // 
            this.button_Start.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_Start.Location = new System.Drawing.Point(387, 147);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(75, 23);
            this.button_Start.TabIndex = 12;
            this.button_Start.Text = "开始AC!";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox_OJ
            // 
            this.comboBox_OJ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_OJ.FormattingEnabled = true;
            this.comboBox_OJ.Items.AddRange(new object[] {
            "hdu"});
            this.comboBox_OJ.Location = new System.Drawing.Point(49, 12);
            this.comboBox_OJ.Name = "comboBox_OJ";
            this.comboBox_OJ.Size = new System.Drawing.Size(160, 23);
            this.comboBox_OJ.TabIndex = 1;
            // 
            // label_OJ
            // 
            this.label_OJ.AutoSize = true;
            this.label_OJ.Location = new System.Drawing.Point(12, 15);
            this.label_OJ.Name = "label_OJ";
            this.label_OJ.Size = new System.Drawing.Size(31, 15);
            this.label_OJ.TabIndex = 0;
            this.label_OJ.Text = "OJ:";
            // 
            // label_Crawler
            // 
            this.label_Crawler.AutoSize = true;
            this.label_Crawler.Location = new System.Drawing.Point(233, 15);
            this.label_Crawler.Name = "label_Crawler";
            this.label_Crawler.Size = new System.Drawing.Size(45, 15);
            this.label_Crawler.TabIndex = 2;
            this.label_Crawler.Text = "爬虫:";
            // 
            // label_Username
            // 
            this.label_Username.AutoSize = true;
            this.label_Username.Location = new System.Drawing.Point(9, 58);
            this.label_Username.Name = "label_Username";
            this.label_Username.Size = new System.Drawing.Size(60, 15);
            this.label_Username.TabIndex = 4;
            this.label_Username.Text = "用户名:";
            // 
            // label_Password
            // 
            this.label_Password.AutoSize = true;
            this.label_Password.Location = new System.Drawing.Point(233, 58);
            this.label_Password.Name = "label_Password";
            this.label_Password.Size = new System.Drawing.Size(45, 15);
            this.label_Password.TabIndex = 6;
            this.label_Password.Text = "密码:";
            // 
            // label_StartID
            // 
            this.label_StartID.AutoSize = true;
            this.label_StartID.Location = new System.Drawing.Point(9, 104);
            this.label_StartID.Name = "label_StartID";
            this.label_StartID.Size = new System.Drawing.Size(91, 15);
            this.label_StartID.TabIndex = 8;
            this.label_StartID.Text = "起始题目ID:";
            // 
            // label_EndID
            // 
            this.label_EndID.AutoSize = true;
            this.label_EndID.Location = new System.Drawing.Point(233, 104);
            this.label_EndID.Name = "label_EndID";
            this.label_EndID.Size = new System.Drawing.Size(91, 15);
            this.label_EndID.TabIndex = 10;
            this.label_EndID.Text = "结束题目ID:";
            // 
            // textBox_Username
            // 
            this.textBox_Username.Location = new System.Drawing.Point(75, 55);
            this.textBox_Username.Name = "textBox_Username";
            this.textBox_Username.Size = new System.Drawing.Size(134, 25);
            this.textBox_Username.TabIndex = 5;
            // 
            // textBox_Password
            // 
            this.textBox_Password.Location = new System.Drawing.Point(285, 55);
            this.textBox_Password.Name = "textBox_Password";
            this.textBox_Password.PasswordChar = '*';
            this.textBox_Password.Size = new System.Drawing.Size(177, 25);
            this.textBox_Password.TabIndex = 7;
            // 
            // textBox_StartID
            // 
            this.textBox_StartID.Location = new System.Drawing.Point(109, 101);
            this.textBox_StartID.Name = "textBox_StartID";
            this.textBox_StartID.Size = new System.Drawing.Size(100, 25);
            this.textBox_StartID.TabIndex = 9;
            this.textBox_StartID.Text = "1000";
            // 
            // textBox_EndID
            // 
            this.textBox_EndID.Location = new System.Drawing.Point(341, 101);
            this.textBox_EndID.Name = "textBox_EndID";
            this.textBox_EndID.Size = new System.Drawing.Size(121, 25);
            this.textBox_EndID.TabIndex = 11;
            this.textBox_EndID.Text = "6000";
            // 
            // comboBox_Crawler
            // 
            this.comboBox_Crawler.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Crawler.FormattingEnabled = true;
            this.comboBox_Crawler.Items.AddRange(new object[] {
            "ACMSearch",
            "CSDN"});
            this.comboBox_Crawler.Location = new System.Drawing.Point(285, 12);
            this.comboBox_Crawler.Name = "comboBox_Crawler";
            this.comboBox_Crawler.Size = new System.Drawing.Size(177, 23);
            this.comboBox_Crawler.TabIndex = 3;
            // 
            // linkLabel_Blog
            // 
            this.linkLabel_Blog.AutoSize = true;
            this.linkLabel_Blog.Location = new System.Drawing.Point(12, 151);
            this.linkLabel_Blog.Name = "linkLabel_Blog";
            this.linkLabel_Blog.Size = new System.Drawing.Size(39, 15);
            this.linkLabel_Blog.TabIndex = 14;
            this.linkLabel_Blog.TabStop = true;
            this.linkLabel_Blog.Text = "BLOG";
            this.linkLabel_Blog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Blog_LinkClicked);
            // 
            // linkLabel_SourceCode
            // 
            this.linkLabel_SourceCode.AutoSize = true;
            this.linkLabel_SourceCode.Location = new System.Drawing.Point(72, 151);
            this.linkLabel_SourceCode.Name = "linkLabel_SourceCode";
            this.linkLabel_SourceCode.Size = new System.Drawing.Size(87, 15);
            this.linkLabel_SourceCode.TabIndex = 15;
            this.linkLabel_SourceCode.TabStop = true;
            this.linkLabel_SourceCode.Text = "SourceCode";
            this.linkLabel_SourceCode.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_SourceCode_LinkClicked);
            // 
            // checkBox_SaveCode
            // 
            this.checkBox_SaveCode.AutoSize = true;
            this.checkBox_SaveCode.Location = new System.Drawing.Point(173, 151);
            this.checkBox_SaveCode.Name = "checkBox_SaveCode";
            this.checkBox_SaveCode.Size = new System.Drawing.Size(89, 19);
            this.checkBox_SaveCode.TabIndex = 16;
            this.checkBox_SaveCode.Text = "保存代码";
            this.checkBox_SaveCode.UseVisualStyleBackColor = true;
            // 
            // textBox_MaxRetry
            // 
            this.textBox_MaxRetry.Location = new System.Drawing.Point(350, 145);
            this.textBox_MaxRetry.Name = "textBox_MaxRetry";
            this.textBox_MaxRetry.Size = new System.Drawing.Size(25, 25);
            this.textBox_MaxRetry.TabIndex = 17;
            this.textBox_MaxRetry.Text = "5";
            // 
            // label_MaxRetry
            // 
            this.label_MaxRetry.AutoSize = true;
            this.label_MaxRetry.Location = new System.Drawing.Point(269, 150);
            this.label_MaxRetry.Name = "label_MaxRetry";
            this.label_MaxRetry.Size = new System.Drawing.Size(75, 15);
            this.label_MaxRetry.TabIndex = 18;
            this.label_MaxRetry.Text = "最多尝试:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 653);
            this.Controls.Add(this.label_MaxRetry);
            this.Controls.Add(this.textBox_MaxRetry);
            this.Controls.Add(this.checkBox_SaveCode);
            this.Controls.Add(this.linkLabel_SourceCode);
            this.Controls.Add(this.linkLabel_Blog);
            this.Controls.Add(this.comboBox_Crawler);
            this.Controls.Add(this.textBox_EndID);
            this.Controls.Add(this.textBox_StartID);
            this.Controls.Add(this.textBox_Password);
            this.Controls.Add(this.textBox_Username);
            this.Controls.Add(this.label_EndID);
            this.Controls.Add(this.label_StartID);
            this.Controls.Add(this.label_Password);
            this.Controls.Add(this.label_Username);
            this.Controls.Add(this.label_Crawler);
            this.Controls.Add(this.label_OJ);
            this.Controls.Add(this.comboBox_OJ);
            this.Controls.Add(this.button_Start);
            this.Controls.Add(this.textBox_Msg);
            this.Name = "MainForm";
            this.Text = "自动AC机";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Msg;
        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.ComboBox comboBox_OJ;
        private System.Windows.Forms.Label label_OJ;
        private System.Windows.Forms.Label label_Crawler;
        private System.Windows.Forms.Label label_Username;
        private System.Windows.Forms.Label label_Password;
        private System.Windows.Forms.Label label_StartID;
        private System.Windows.Forms.Label label_EndID;
        private System.Windows.Forms.TextBox textBox_Username;
        private System.Windows.Forms.TextBox textBox_Password;
        private System.Windows.Forms.TextBox textBox_StartID;
        private System.Windows.Forms.TextBox textBox_EndID;
        private System.Windows.Forms.ComboBox comboBox_Crawler;
        private System.Windows.Forms.LinkLabel linkLabel_Blog;
        private System.Windows.Forms.LinkLabel linkLabel_SourceCode;
        private System.Windows.Forms.CheckBox checkBox_SaveCode;
        private System.Windows.Forms.TextBox textBox_MaxRetry;
        private System.Windows.Forms.Label label_MaxRetry;
    }
}

