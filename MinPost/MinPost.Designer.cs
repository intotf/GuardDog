namespace MinPost
{
    partial class MinPost
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MinPost));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cbIsBinary = new System.Windows.Forms.CheckBox();
            this.btLoding = new System.Windows.Forms.Button();
            this.cbQueryList = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btAddFile = new System.Windows.Forms.Button();
            this.dgvFiles = new System.Windows.Forms.DataGridView();
            this.FilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btDelFile = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.btSave = new System.Windows.Forms.Button();
            this.btSubmit = new System.Windows.Forms.Button();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.cbLangType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDataBody = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbUrl = new System.Windows.Forms.TextBox();
            this.cbPostType = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.btLogin = new System.Windows.Forms.Button();
            this.Password = new System.Windows.Forms.TextBox();
            this.Username = new System.Windows.Forms.TextBox();
            this.lbPassword = new System.Windows.Forms.Label();
            this.lbUsername = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvParameter = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btClear = new System.Windows.Forms.Button();
            this.btSaveLog = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParameter)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(769, 535);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btSaveLog);
            this.tabPage1.Controls.Add(this.btClear);
            this.tabPage1.Controls.Add(this.cbIsBinary);
            this.tabPage1.Controls.Add(this.btLoding);
            this.tabPage1.Controls.Add(this.cbQueryList);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.btAddFile);
            this.tabPage1.Controls.Add(this.dgvFiles);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.btSave);
            this.tabPage1.Controls.Add(this.btSubmit);
            this.tabPage1.Controls.Add(this.tbResult);
            this.tabPage1.Controls.Add(this.cbLangType);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.tbDataBody);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.tbUrl);
            this.tabPage1.Controls.Add(this.cbPostType);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(761, 509);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "基本信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cbIsBinary
            // 
            this.cbIsBinary.AutoSize = true;
            this.cbIsBinary.Location = new System.Drawing.Point(667, 192);
            this.cbIsBinary.Name = "cbIsBinary";
            this.cbIsBinary.Size = new System.Drawing.Size(84, 16);
            this.cbIsBinary.TabIndex = 31;
            this.cbIsBinary.Text = "二进制提交";
            this.cbIsBinary.UseVisualStyleBackColor = true;
            // 
            // btLoding
            // 
            this.btLoding.Location = new System.Drawing.Point(519, 7);
            this.btLoding.Name = "btLoding";
            this.btLoding.Size = new System.Drawing.Size(75, 23);
            this.btLoding.TabIndex = 30;
            this.btLoding.Text = "加载参数";
            this.btLoding.UseVisualStyleBackColor = true;
            this.btLoding.Click += new System.EventHandler(this.btLoding_Click);
            // 
            // cbQueryList
            // 
            this.cbQueryList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQueryList.FormattingEnabled = true;
            this.cbQueryList.Location = new System.Drawing.Point(79, 9);
            this.cbQueryList.Name = "cbQueryList";
            this.cbQueryList.Size = new System.Drawing.Size(434, 20);
            this.cbQueryList.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 28;
            this.label4.Text = "历史调试：";
            // 
            // btAddFile
            // 
            this.btAddFile.Location = new System.Drawing.Point(79, 188);
            this.btAddFile.Name = "btAddFile";
            this.btAddFile.Size = new System.Drawing.Size(75, 23);
            this.btAddFile.TabIndex = 27;
            this.btAddFile.Text = "添加文件";
            this.btAddFile.UseVisualStyleBackColor = true;
            this.btAddFile.Click += new System.EventHandler(this.btAddFile_Click);
            // 
            // dgvFiles
            // 
            this.dgvFiles.AllowUserToAddRows = false;
            this.dgvFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FilePath,
            this.btDelFile});
            this.dgvFiles.Location = new System.Drawing.Point(79, 219);
            this.dgvFiles.Name = "dgvFiles";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFiles.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvFiles.RowTemplate.Height = 23;
            this.dgvFiles.Size = new System.Drawing.Size(672, 114);
            this.dgvFiles.TabIndex = 26;
            this.dgvFiles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFiles_CellContentClick);
            // 
            // FilePath
            // 
            this.FilePath.HeaderText = "文件路径";
            this.FilePath.Name = "FilePath";
            this.FilePath.ReadOnly = true;
            this.FilePath.Width = 550;
            // 
            // btDelFile
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = "删除";
            this.btDelFile.DefaultCellStyle = dataGridViewCellStyle1;
            this.btDelFile.HeaderText = "操作";
            this.btDelFile.Name = "btDelFile";
            this.btDelFile.ReadOnly = true;
            this.btDelFile.Text = "删除";
            this.btDelFile.Width = 70;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "上传的文件：";
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(600, 7);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(86, 23);
            this.btSave.TabIndex = 24;
            this.btSave.Text = "保存(Ctrl+S)";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btSubmit
            // 
            this.btSubmit.Location = new System.Drawing.Point(694, 37);
            this.btSubmit.Name = "btSubmit";
            this.btSubmit.Size = new System.Drawing.Size(57, 23);
            this.btSubmit.TabIndex = 23;
            this.btSubmit.Text = "提 交";
            this.btSubmit.UseVisualStyleBackColor = true;
            this.btSubmit.Click += new System.EventHandler(this.btSubmit_Click);
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(10, 370);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbResult.Size = new System.Drawing.Size(741, 133);
            this.tbResult.TabIndex = 22;
            // 
            // cbLangType
            // 
            this.cbLangType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLangType.FormattingEnabled = true;
            this.cbLangType.Location = new System.Drawing.Point(79, 341);
            this.cbLangType.Name = "cbLangType";
            this.cbLangType.Size = new System.Drawing.Size(387, 20);
            this.cbLangType.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 345);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "接交编码：";
            // 
            // tbDataBody
            // 
            this.tbDataBody.Location = new System.Drawing.Point(79, 71);
            this.tbDataBody.Multiline = true;
            this.tbDataBody.Name = "tbDataBody";
            this.tbDataBody.Size = new System.Drawing.Size(672, 108);
            this.tbDataBody.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "提交参数：";
            // 
            // tbUrl
            // 
            this.tbUrl.Location = new System.Drawing.Point(79, 39);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(609, 21);
            this.tbUrl.TabIndex = 17;
            // 
            // cbPostType
            // 
            this.cbPostType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPostType.FormattingEnabled = true;
            this.cbPostType.Location = new System.Drawing.Point(10, 40);
            this.cbPostType.Name = "cbPostType";
            this.cbPostType.Size = new System.Drawing.Size(62, 20);
            this.cbPostType.TabIndex = 16;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(761, 509);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "高级配置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.btLogin);
            this.panel2.Controls.Add(this.Password);
            this.panel2.Controls.Add(this.Username);
            this.panel2.Controls.Add(this.lbPassword);
            this.panel2.Controls.Add(this.lbUsername);
            this.panel2.Location = new System.Drawing.Point(7, 184);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(275, 107);
            this.panel2.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(185, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "Authorization 登录(Basic Auth)";
            // 
            // btLogin
            // 
            this.btLogin.Location = new System.Drawing.Point(190, 34);
            this.btLogin.Name = "btLogin";
            this.btLogin.Size = new System.Drawing.Size(63, 55);
            this.btLogin.TabIndex = 4;
            this.btLogin.Text = "登录";
            this.btLogin.UseVisualStyleBackColor = true;
            this.btLogin.Click += new System.EventHandler(this.btLogin_Click);
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(74, 68);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(100, 21);
            this.Password.TabIndex = 3;
            // 
            // Username
            // 
            this.Username.Location = new System.Drawing.Point(74, 37);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(100, 21);
            this.Username.TabIndex = 2;
            // 
            // lbPassword
            // 
            this.lbPassword.AutoSize = true;
            this.lbPassword.Location = new System.Drawing.Point(15, 68);
            this.lbPassword.Name = "lbPassword";
            this.lbPassword.Size = new System.Drawing.Size(53, 12);
            this.lbPassword.TabIndex = 1;
            this.lbPassword.Text = "登录密码";
            // 
            // lbUsername
            // 
            this.lbUsername.AutoSize = true;
            this.lbUsername.Location = new System.Drawing.Point(15, 40);
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.Size = new System.Drawing.Size(53, 12);
            this.lbUsername.TabIndex = 0;
            this.lbUsername.Text = "登 录 名";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvParameter);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(7, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(748, 170);
            this.panel1.TabIndex = 1;
            // 
            // dgvParameter
            // 
            this.dgvParameter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParameter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.Column1,
            this.dataGridViewButtonColumn1});
            this.dgvParameter.Location = new System.Drawing.Point(7, 22);
            this.dgvParameter.Name = "dgvParameter";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvParameter.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvParameter.RowTemplate.Height = 23;
            this.dgvParameter.Size = new System.Drawing.Size(741, 141);
            this.dgvParameter.TabIndex = 27;
            this.dgvParameter.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvParameter_CellContentClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "参数名";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 130;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "参数值";
            this.Column1.Name = "Column1";
            this.Column1.Width = 470;
            // 
            // dataGridViewButtonColumn1
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = "删除";
            this.dataGridViewButtonColumn1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewButtonColumn1.HeaderText = "操作";
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.ReadOnly = true;
            this.dataGridViewButtonColumn1.Text = "删除";
            this.dataGridViewButtonColumn1.Width = 70;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "Headers";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 550);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(793, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(613, 342);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(57, 23);
            this.btClear.TabIndex = 32;
            this.btClear.Text = "清空";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // btSaveLog
            // 
            this.btSaveLog.Location = new System.Drawing.Point(676, 341);
            this.btSaveLog.Name = "btSaveLog";
            this.btSaveLog.Size = new System.Drawing.Size(75, 23);
            this.btSaveLog.TabIndex = 33;
            this.btSaveLog.Text = "保存日志";
            this.btSaveLog.UseVisualStyleBackColor = true;
            this.btSaveLog.Click += new System.EventHandler(this.btSaveLog_Click);
            // 
            // MinPost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 572);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MinPost";
            this.Text = "minPost 调试工具V1.2 - QQ:42309073";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParameter)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btLoding;
        private System.Windows.Forms.ComboBox cbQueryList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btAddFile;
        private System.Windows.Forms.DataGridView dgvFiles;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilePath;
        private System.Windows.Forms.DataGridViewButtonColumn btDelFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btSubmit;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.ComboBox cbLangType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDataBody;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbUrl;
        private System.Windows.Forms.ComboBox cbPostType;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvParameter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.CheckBox cbIsBinary;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbPassword;
        private System.Windows.Forms.Label lbUsername;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.Button btLogin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.Button btSaveLog;
    }
}