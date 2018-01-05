using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IISMonitor
{
    public partial class MainForm : Form
    {
        private CancellationTokenSource cancellSource = new CancellationTokenSource();
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var monitor in Config.Instance.Webs)
            {
                monitor.StartAsync(cancellSource.Token);
            }
            this.button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var iis = ServerManager.OpenRemote("localhost"))
            {
                var webSizes = iis.Sites.Select(item => new { item.Name, item.State }).ToList();
                var appPools = iis.ApplicationPools.Select(item => new { item.Name, item.State }).ToList();

                this.dgvPool.DataSource = appPools;
                this.dgvSizeList.DataSource = webSizes;
            }
        }
    }
}
