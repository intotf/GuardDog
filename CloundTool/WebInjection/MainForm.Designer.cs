namespace WebInjection
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.tbUrl = new System.Windows.Forms.TextBox();
            this.btnGoto = new System.Windows.Forms.Button();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.BtnInjection = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer.Panel1.Controls.Add(this.BtnInjection);
            this.splitContainer.Panel1.Controls.Add(this.BtnRefresh);
            this.splitContainer.Panel1.Controls.Add(this.btnGoto);
            this.splitContainer.Panel1.Controls.Add(this.tbUrl);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.webBrowser);
            this.splitContainer.Size = new System.Drawing.Size(849, 437);
            this.splitContainer.SplitterDistance = 35;
            this.splitContainer.SplitterWidth = 1;
            this.splitContainer.TabIndex = 0;
            // 
            // tbUrl
            // 
            this.tbUrl.Location = new System.Drawing.Point(12, 7);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(557, 21);
            this.tbUrl.TabIndex = 0;
            this.tbUrl.Text = "http://www.baidu.com";
            // 
            // btnGoto
            // 
            this.btnGoto.Location = new System.Drawing.Point(575, 6);
            this.btnGoto.Name = "btnGoto";
            this.btnGoto.Size = new System.Drawing.Size(81, 23);
            this.btnGoto.TabIndex = 1;
            this.btnGoto.Text = "转到";
            this.btnGoto.UseVisualStyleBackColor = true;
            this.btnGoto.Click += new System.EventHandler(this.btnGoto_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(662, 6);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(75, 23);
            this.BtnRefresh.TabIndex = 2;
            this.BtnRefresh.Text = "刷新";
            this.BtnRefresh.UseVisualStyleBackColor = true;
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(847, 399);
            this.webBrowser.TabIndex = 0;
            // 
            // BtnInjection
            // 
            this.BtnInjection.Location = new System.Drawing.Point(744, 6);
            this.BtnInjection.Name = "BtnInjection";
            this.BtnInjection.Size = new System.Drawing.Size(75, 23);
            this.BtnInjection.TabIndex = 3;
            this.BtnInjection.Text = "测试注入";
            this.BtnInjection.UseVisualStyleBackColor = true;
            this.BtnInjection.Click += new System.EventHandler(this.BtnInjection_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 437);
            this.Controls.Add(this.splitContainer);
            this.Name = "MainForm";
            this.Text = "Web 注入";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TextBox tbUrl;
        private System.Windows.Forms.Button btnGoto;
        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Button BtnInjection;
    }
}

