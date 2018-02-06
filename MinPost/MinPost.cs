using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinPost
{
    public partial class MinPost : Form
    {
        /// <summary>
        /// 默认文件路径
        /// </summary>
        private string bastDataPath = Directory.GetCurrentDirectory() + "\\Data";

        /// <summary>
        /// Post 提交类型
        /// </summary>
        private IEnumerable<SelectModel> postType = default(PostType).GetValueDisplays().Select(item => new SelectModel { Value = item.Key.GetHashCode(), Title = item.Value });

        /// <summary>
        /// Post 提交语言
        /// </summary>
        //private IEnumerable<SelectModel> langType = Encoding.GetEncodings().Select(item => new SelectModel { Value = item.CodePage, Title = item.DisplayName });
        private IEnumerable<SelectModel> langType = default(LanguageType).GetValueDisplays().Select(item => new SelectModel { Value = item.Key.GetHashCode(), Title = item.Value });


        public MinPost()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializePage();
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitializePage()
        {

            this.setStatusTxt("正在加载...");
            this.cbLangType.Items.Clear();
            this.cbPostType.Items.Clear();
            this.cbQueryList.Items.Clear();

            //检查Data 目录是否存在
            FileHelper.CreateDirectory(bastDataPath);

            //初始化提交方式
            this.cbPostType.DisplayMember = "Title";
            this.cbPostType.Items.AddRange(postType.ToArray());
            var postTypeList = postType.ToList();
            this.cbPostType.SelectedIndex = postTypeList.IndexOf(postTypeList.Where(item => item.Title == ConfigModel.DefaultPostType).FirstOrDefault());
            //this.cbPostType.SelectedIndexChanged += new EventHandler(cbPostType_SelectedIndexChanged);

            //初始化提交编码
            this.cbLangType.DisplayMember = "Title";
            this.cbLangType.Items.AddRange(langType.ToArray());
            var langTypeList = langType.ToList();
            this.cbLangType.SelectedIndex = langTypeList.IndexOf(langTypeList.Where(item => item.Title == ConfigModel.DefaultLanguage).FirstOrDefault());

            var queryList = GetQueryList();
            this.cbQueryList.DisplayMember = "Title";
            this.cbQueryList.Items.AddRange(queryList.ToArray());
            this.cbQueryList.SelectedIndex = (queryList.Count > 0 ? 0 : -1);

            this.setStatusTxt("初始化完成.");
        }

        /// <summary>
        /// 获取查询历史
        /// </summary>
        /// <returns></returns>
        private List<FilesTree> GetQueryList()
        {
            var filesTree = new List<FilesTree>();
            //除根目录下所有文件夹
            var folders = new string[] { this.bastDataPath }.Concat(FileHelper.GetDirectories(bastDataPath, "*", true));
            foreach (var item in folders)
            {
                var folderModel = new FilesTree();
                folderModel.IsFolder = true;
                folderModel.Name = item.Substring(item.LastIndexOf("\\")).Replace("\\", "");
                folderModel.AbsolutePath = item;
                folderModel.RelativePath = item.Replace(bastDataPath, "");
                folderModel.Title = "+ " + folderModel.Name;
                filesTree.Add(folderModel);

                var files = FileHelper.GetFileNames(item);
                foreach (var it in files)
                {
                    var model = new FilesTree();
                    model.AbsolutePath = it;
                    model.RelativePath = it.Replace(bastDataPath, "");
                    model.Name = FileHelper.GetFileName(it);
                    model.Title = "  - " + FileHelper.GetFileName(it);
                    model.IsFolder = false;
                    filesTree.Add(model);
                }
            }
            return filesTree;
        }

        /// <summary>
        /// 添加文件到列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAddFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var dgvIndex = this.dgvFiles.Rows.Add();
                this.dgvFiles.Rows[dgvIndex].Cells[0].Value = dialog.FileName;
            }
        }

        /// <summary>
        /// 删除文件列表中的记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvFiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int CIndex = e.ColumnIndex;
            if (CIndex == 1)
            {
                if (e.RowIndex >= 0)
                {
                    this.dgvFiles.Rows.RemoveAt(e.RowIndex);  //删除当前行
                }
            }
        }

        /// <summary>
        /// 提交参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSubmit_Click(object sender, EventArgs e)
        {
            var model = GetParameterModel();
            if (model == null)
                return;

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            if (model.postType == PostType.Post)
            {
                Task.Run(() =>
                        this.SubmitPostHttp(model)
                    ).ContinueWith((item) =>
                    {
                        stopWatch.Stop();
                        setStatusTxt(string.Format("总共用时：{0}ms", stopWatch.ElapsedMilliseconds.ToString()));
                    });
            }
            else
            {
                Task.Run(() =>
                        this.SubmitGetHttp(model)
                    ).ContinueWith((item) =>
                    {
                        stopWatch.Stop();
                        setStatusTxt(string.Format("总共用时：{0}ms", stopWatch.ElapsedMilliseconds.ToString()));
                    });
            }
        }

        /// <summary>
        /// 设置状态栏文本
        /// </summary>
        /// <param name="msg"></param>
        private void setStatusTxt(string msg)
        {
            this.Invoke(new Action(() =>
            {
                this.toolStripStatusLabel1.Text = msg;
            }));
        }

        /// <summary>
        /// 获取介面提交参数实体
        /// </summary>
        private ParameterModel GetParameterModel()
        {
            if (!this.tbUrl.Text.IsUrl())
            {
                MessageBox.Show("提交地址必需为一个完整的Url");
                return null;
            }
            var model = new ParameterModel();
            var fileList = new List<string>();
            foreach (DataGridViewRow item in this.dgvFiles.Rows)
            {
                var tempFile = item.Cells[0].Value.ToString();
                if (!fileList.Contains(tempFile))
                {
                    fileList.Add(tempFile);
                }
            }

            //提交 Headers 参数
            Dictionary<string, string> HeadersDic = new Dictionary<string, string>();
            foreach (DataGridViewRow item in this.dgvParameter.Rows)
            {
                var key = item.Cells[0].Value;
                if (key != null)
                {
                    HeadersDic.Add(key.ToString(), item.Cells[1].Value.ToString());
                }
            }

            model.IsBinary = this.cbIsBinary.Checked;
            var langType = this.cbLangType.SelectedItem.ToOfType<SelectModel>();
            var postType = this.cbPostType.SelectedItem.ToOfType<SelectModel>();
            model.HeadersDic = HeadersDic;
            model.fileList = fileList;
            model.postBody = this.tbDataBody.Text;
            model.langType = (LanguageType)langType.Value;
            model.postType = (PostType)postType.Value;
            model.postUrl = this.tbUrl.Text.ToUri();
            return model;
        }


        /// <summary>
        /// Post提交数据
        /// </summary>
        /// <param name="model">提交模型</param>
        private void SubmitPostHttp(ParameterModel model)
        {
            var clientResult = "<!--" + DateTime.Now.ToDateTimeString() + "-->" + Environment.NewLine;
            using (var client = new HttpClient())
            {
                try
                {   //添加请求头
                    client.Headers = client.AddWebHeaders(model.HeadersDic);

                    var langType = Encoding.GetEncoding(model.langType.GetFieldDisplay());
                    if (model.IsBinary || model.fileList.Count() > 0)
                    {
                        var form = new HttpClient.MultipartForm();
                        foreach (var file in model.fileList)
                        {
                            string fileName = file.Substring(file.LastIndexOf('\\') + 1);
                            form.AddFile(fileName, file);
                        }

                        var parArr = model.postBody.Split('&');
                        foreach (var item in parArr)
                        {
                            if (!item.IsNullOrEmpty())
                            {
                                var items = item.Split('=');
                                form.Add(items[0], items[1]);
                            }
                        }
                        clientResult += client.HttpPost(model.postUrl.AbsoluteUri, form, langType);
                    }
                    else
                    {
                        clientResult += client.HttpPost(model.postUrl.AbsoluteUri, model.postBody, langType);
                    }
                }
                catch (Exception ex)
                {
                    clientResult += ex.Message;
                    setStatusTxt("异常：" + ex.Message);
                }
                tbAppendText(this.tbResult, clientResult + "\r\n");
            }
        }

        /// <summary>
        /// 设置委托设置文本框内容
        /// </summary>
        /// <param name="tb"></param>
        /// <param name="value"></param>
        private void tbAppendText(TextBox tb, string value)
        {
            this.Invoke(new Action(() =>
            {
                tb.AppendText(Environment.NewLine);
                tb.AppendText(value);
                tb.ScrollToCaret();
            }));
        }

        /// <summary>
        /// Get提交数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <param name="langType"></param>
        private void SubmitGetHttp(ParameterModel model)
        {
            var clientResult = "<!-- " + DateTime.Now.ToDateTimeString() + " -->" + Environment.NewLine;
            using (var client = new HttpClient())
            {
                try
                {
                    var langType = Encoding.GetEncoding(model.langType.GetFieldDisplay());
                    clientResult += client.HttpGet(model.postUrl.AbsoluteUri, model.postBody, langType);
                }
                catch (Exception ex)
                {
                    clientResult += ex.Message;
                    setStatusTxt("异常：" + ex.Message);
                }
                tbAppendText(this.tbResult, clientResult + "\r\n");
            }
        }

        /// <summary>
        /// 保存查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSave_Click(object sender, EventArgs e)
        {
            var model = GetParameterModel();
            if (model == null)
                return;

            var modelJson = JsonSerializer.Serialize(model);
            //设置保存文件的格式  
            saveFileDialog1.Filter = "数据文件(*.dat)|*.dat";
            saveFileDialog1.InitialDirectory = bastDataPath;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, modelJson, Encoding.UTF8);
                MessageBox.Show("保存成功.");
                InitializePage();
            }
        }

        /// <summary>
        /// 加载历史查询数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btLoding_Click(object sender, EventArgs e)
        {
            //var filePath = bastDataPath + "\\" + this.cbQueryList.Text;
            var fileModel = this.cbQueryList.SelectedItem.ToOfType<FilesTree>();
            if (fileModel.IsFolder)
            {
                MessageBox.Show("请不要选择目录.");
                return;
            }

            var fileJson = FileHelper.FileToString(fileModel.AbsolutePath);
            var model = JsonSerializer.Deserialize<ParameterModel>(fileJson);

            var a = new SelectModel { Value = model.postType.GetHashCode(), Title = model.postType.GetFieldDisplay() };
            var b = new SelectModel { Value = model.postType.GetHashCode(), Title = model.postType.GetFieldDisplay() };

            if (model != null)
            {
                this.tbDataBody.Text = model.postBody;
                this.tbUrl.Text = model.postUrl.AbsoluteUri;
                var postTypeList = postType.ToList();
                this.cbPostType.SelectedIndex = postTypeList.IndexOf(postTypeList.Where(item => item.Value == model.postType.GetHashCode()).FirstOrDefault());

                var langTypeList = langType.ToList();
                this.cbLangType.SelectedIndex = langTypeList.IndexOf(langTypeList.Where(item => item.Value == model.langType.GetHashCode()).FirstOrDefault());
                this.dgvFiles.Rows.Clear();
                foreach (var item in model.fileList)
                {
                    var dgvIndex = this.dgvFiles.Rows.Add();
                    this.dgvFiles.Rows[dgvIndex].Cells[0].Value = item;
                }
                this.cbIsBinary.Checked = model.IsBinary;

                this.dgvParameter.Rows.Clear();
                if (model.HeadersDic != null)
                {
                    foreach (var item in model.HeadersDic)
                    {
                        var dgvIndex = this.dgvParameter.Rows.Add();
                        this.dgvParameter.Rows[dgvIndex].Cells[0].Value = item.Key;
                        this.dgvParameter.Rows[dgvIndex].Cells[1].Value = item.Value;
                    }
                }
            }
        }

        /// <summary>
        /// 提交方式选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbPostType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbPostType.SelectedItem != null)
            {
                var postTypeSelect = this.cbPostType.SelectedItem.ToOfType<SelectModel>();
                var postType = (PostType)postTypeSelect.Value;
                if (postType == PostType.Get)
                {
                    btAddFile.Enabled = false;
                }
                else
                {
                    btAddFile.Enabled = true;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                e.Handled = true; //将Handled设置为true，指示已经处理过KeyPress事件
                this.btSave.PerformClick(); //执行单击保存按钮
            }
        }

        /// <summary>
        /// 删除请求头当前行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvParameter_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int CIndex = e.ColumnIndex;
            if (CIndex == 2)
            {
                if (e.RowIndex >= 0 && e.RowIndex < this.dgvParameter.RowCount - 1)
                {
                    this.dgvParameter.Rows.RemoveAt(e.RowIndex);  //删除当前行
                }
            }
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            var strAuthor = "Authorization";
            var strValue = "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(this.Username.Text + this.Password.Text));
            bool isChechk = true;
            foreach (DataGridViewRow v in this.dgvParameter.Rows)
            {
                if (v.Cells[0].Value != null && v.Cells[0].Value.ToString().Equals(strAuthor))
                {
                    v.Cells[1].Value = strValue;
                    isChechk = false;
                }
            }
            if (isChechk)
            {
                var dgvIndex = this.dgvParameter.Rows.Add();
                this.dgvParameter.Rows[dgvIndex].Cells[0].Value = strAuthor;
                this.dgvParameter.Rows[dgvIndex].Cells[1].Value = strValue;
            }
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            this.tbResult.Clear();

        }

        private void btSaveLog_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "文本文本(*.txt)|*.txt";
            saveFileDialog1.InitialDirectory = bastDataPath;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, this.tbResult.Text);
            }
        }
    }
}
