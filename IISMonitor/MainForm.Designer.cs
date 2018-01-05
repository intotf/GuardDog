namespace IISMonitor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dgvSizeList = new System.Windows.Forms.DataGridView();
            this.dgvPool = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSizeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPool)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(518, 310);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "IIS 站点监控";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(25, 310);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(157, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "加载所有站点及应用池";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dgvSizeList
            // 
            this.dgvSizeList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSizeList.Location = new System.Drawing.Point(25, 34);
            this.dgvSizeList.Name = "dgvSizeList";
            this.dgvSizeList.RowTemplate.Height = 23;
            this.dgvSizeList.Size = new System.Drawing.Size(287, 270);
            this.dgvSizeList.TabIndex = 2;
            this.dgvSizeList.Visible = false;
            // 
            // dgvPool
            // 
            this.dgvPool.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPool.Location = new System.Drawing.Point(350, 34);
            this.dgvPool.Name = "dgvPool";
            this.dgvPool.RowTemplate.Height = 23;
            this.dgvPool.Size = new System.Drawing.Size(266, 270);
            this.dgvPool.TabIndex = 3;
            this.dgvPool.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 355);
            this.Controls.Add(this.dgvPool);
            this.Controls.Add(this.dgvSizeList);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "IIS 站点监控 QQ:42309073";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSizeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPool)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dgvSizeList;
        private System.Windows.Forms.DataGridView dgvPool;
    }
}

