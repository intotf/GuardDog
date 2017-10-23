using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Utility;
using Infrastructure;
using WebMonitor.Push;

namespace WebMonitor
{
    public partial class MainForm : Form
    {
        private CancellationTokenSource cancellSource = new CancellationTokenSource();

        private static Config SysConfig = Config.Instance;

        public MainForm()
        {
            InitializeComponent();
            this.Load += MainForm_Load;
        }

        /// <summary>
        /// 程序加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainForm_Load(object sender, EventArgs e)
        {
            this.btnStop.Enabled = false;
            Debugger.SetOut(new MyWriter(SynchronizationContext.Current, SetTblog));
            Console.SetOut(Debugger.Out);

            //初始化控件内容
            foreach (var item in SysConfig.Webs)
            {
                var dgvIndex = this.dgvWebs.Rows.Add();
                this.dgvWebs.Rows[dgvIndex].Cells[0].Value = item.Name;
                this.dgvWebs.Rows[dgvIndex].Cells[1].Value = item.Url.ToString();
            }

            foreach (var item in SysConfig.Emails)
            {
                var dgvIndex = this.dgvEmails.Rows.Add();
                this.dgvEmails.Rows[dgvIndex].Cells[0].Value = item;
            }

            this.tbIntervalTime.Text = SysConfig.IntervalTime.ToString();
            this.tbTimeOut.Text = SysConfig.TimeOut.ToString();
            this.cbIsSendMail.Checked = SysConfig.IsSendMail;
            this.tbAttempts.Text = SysConfig.Attempts.ToString();
        }

        private void SetTblog(string msg)
        {
            var MaxLength = 100000 * 10000;
            if (this.rtbLog.Text.Length > MaxLength)
            {
                StreamWriter sw = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "Log" + DateTime.Now.ToString("yyyyMMddHHmm") + ".txt", true, Encoding.UTF8);
                sw.WriteLine("----------------网站监控日志----------------");
                sw.WriteLine(this.rtbLog.Text.Replace("\n", Environment.NewLine));
                sw.Flush();
                sw.Close();
                this.rtbLog.Text = "日志过长,已经保存到日志文件." + Environment.NewLine;
            }
            this.rtbLog.AppendText(DateTime.Now + " :" + msg + Environment.NewLine);
        }

        /// <summary>
        /// 开始监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            SysConfig = new Config().NewConfig();
            this.btnStart.Enabled = false;
            this.btnStop.Enabled = true;
            this.btnSave.Enabled = false;
            if (this.cancellSource.IsCancellationRequested)
            {
                this.cancellSource = new CancellationTokenSource();
            }

            Task.Run(() =>
            {
                foreach (var item in SysConfig.Webs)
                {
                    var control = new Monitoring(item.Name, item.Url);
                    control.StartAsync(this.cancellSource.Token);
                }
            });
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.btnStart.Enabled = true;
            this.btnStop.Enabled = false;
            this.btnSave.Enabled = true;
            this.cancellSource.Cancel();
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            var model = new Config();
            model.IsSendMail = this.cbIsSendMail.Checked;
            model.IntervalTime = this.tbIntervalTime.Text.ToInt32(SysConfig.IntervalTime);
            model.TimeOut = this.tbTimeOut.Text.ToInt32(SysConfig.TimeOut);
            model.Attempts = this.tbAttempts.Text.ToInt32(SysConfig.Attempts);

            //获取邮件列表
            List<string> listEmail = new List<string>();
            foreach (DataGridViewRow item in this.dgvEmails.Rows)
            {
                var value = item.Cells[0].Value;
                if (value != null && Mail.r.IsMatch(value.ToString()))
                {
                    listEmail.Add(value.ToString());
                }
            }
            model.Emails = listEmail;

            //获取监控站点列表
            List<Webs> listWeb = new List<Webs>();
            foreach (DataGridViewRow item in this.dgvWebs.Rows)
            {
                var name = item.Cells[0].Value;
                var url = item.Cells[1].Value;
                if (name != null && url != null)
                {
                    var web = new Webs(name.ToString(), url.ToString());
                    listWeb.Add(web);
                }
            }
            model.Webs = listWeb;
            Config.SavaConfig(model);
            MessageBox.Show("保存成功,请点击开始监控!");
        }

        private void dgvWebs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int CIndex = e.ColumnIndex;
            if (CIndex == 2)
            {
                if (e.RowIndex >= 0 && e.RowIndex < this.dgvWebs.RowCount - 1)
                {
                    this.dgvWebs.Rows.RemoveAt(e.RowIndex);  //删除当前行
                }
            }
        }

        private void dgvEmails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int CIndex = e.ColumnIndex;
            if (CIndex == 1)
            {
                if (e.RowIndex >= 0 && e.RowIndex < this.dgvEmails.RowCount - 1)
                {
                    this.dgvEmails.Rows.RemoveAt(e.RowIndex);  //删除当前行
                }
            }
        }
    }
}
