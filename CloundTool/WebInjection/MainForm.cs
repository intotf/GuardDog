using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsQuery;

namespace WebInjection
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Load += MainForm_Load;
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            this.webBrowser.ObjectForScripting = this;
        }

        private void btnGoto_Click(object sender, EventArgs e)
        {
            var url = new Uri(tbUrl.Text);
            this.webBrowser.Url = url;
        }

        /// <summary>
        /// 加载js内容
        /// </summary>
        /// <returns></returns>
        private string LoadJsText()
        {
            return File.ReadAllText("injection.js");
        }

        private void BtnInjection_Click(object sender, EventArgs e)
        {
            if (webBrowser.Document.Url != null)
            {
                var url = webBrowser.Document.Url.ToString();
                //var cq = CQ.Create(webBrowser.Document.Body.InnerHtml);
                //var body = cq.Where(item => item.Name == "<body>");
                if (!string.IsNullOrEmpty(url) && webBrowser.Document.GetElementById("monitor") == null)
                {
                    var script = webBrowser.Document.CreateElement("script");
                    script.SetAttribute("type", "text/javascript");
                    script.SetAttribute("id", "monitor");
                    script.SetAttribute("text", this.LoadJsText());
                    webBrowser.Document.Body.AppendChild(script);
                }
                webBrowser.Document.InvokeScript("_InjectionAlert");
            }
        }

        public void SendNotify(string msg)
        {
            var message = string.Format("收到 {0} 发送来的消息,{1}.", this.webBrowser.Url.ToString(), msg);
            MessageBox.Show(message);
        }
    }
}
