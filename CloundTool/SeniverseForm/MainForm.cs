using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherLib;
using WebApiClient;

namespace SeniverseForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Load += MainForm_Load;
        }

        async void MainForm_Load(object sender, EventArgs e)
        {
            GetIp();
        }


        private async void GetIp()
        {
            var api = HttpApiClient.Create<SeniverseApi>();
            var ip = await api.GetIp().
                Retry(3, TimeSpan.FromSeconds(2))
                .WhenResult(n => string.IsNullOrEmpty(n))
                .WhenCatch<Exception>()
                .HandleAsDefaultWhenException(ex => MessageBox.Show("自动获取IP 失败," + ex.Message));

            if (string.IsNullOrEmpty(ip))
            {
                this.textBox1.Text = "珠海";
            }
            else
            {
                this.textBox1.Text = ip;
            }
            button1_Click(this.button1, null);
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.Enabled = false;
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                MessageBox.Show("请输入地区名.");
                return;
            }
            var data = await WeatherApi.GetDailyRestAsync(this.textBox1.Text);
            if (!data.State)
            {
                btn.Enabled = true;
                return;
            }
            var i = 0;
            foreach (var item in data.Data.results.FirstOrDefault().daily)
            {

                var tabPage = new TabPage(item.date);
                tabPage.BackColor = Color.White;
                if (this.tabControl1.TabPages.Count <= i)
                {
                    this.tabControl1.TabPages.Add(tabPage);
                    tabPage.Controls.Add(new SeniverseControl(item));
                }
                else
                {
                    tabPage = this.tabControl1.TabPages[i];
                    tabPage.Name = item.date;
                    var uCtrl = tabPage.Controls.Cast<Control>().First() as SeniverseControl;
                    uCtrl.Refresh(item);
                }
                i++;
            }
            var resultFirst = data.Data.results.FirstOrDefault();
            this.lbUpdateTime.Text = resultFirst.last_update.ToString("yyyy-MM-dd HH:mm:ss");
            var path = resultFirst.location.path.Split(',');
            this.lbCity.Text = string.Join(" ", path.Where((item, n) => n > 0).Reverse());
            btn.Enabled = true;
        }
    }
}
