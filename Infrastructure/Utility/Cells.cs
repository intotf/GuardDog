using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.IO;
using Aspose.Cells;
using System.Drawing;

namespace Infrastructure.Utility
{
    public class Cells<T>
    {
        /// <summary>
        /// 字段
        /// </summary>
        private List<IField> fields = new List<IField>();

        /// <summary>
        /// 获取数据模型
        /// </summary>
        public IEnumerable<T> Models { get; private set; }

        /// <summary>
        /// 数据转换为Excel
        /// </summary>
        /// <param name="models">数据</param>
        public Cells(IEnumerable<T> models)
        {
            this.Models = models;
        }

        /// <summary>
        /// 添加字段
        /// </summary>
        /// <typeparam name="TField">字段类型</typeparam>   
        /// <param name="name">字段名</param>
        /// <param name="value">字段值</param>
        public Cells<T> AddField<TField>(string name, Func<T, TField> value)
        {
            var field = new Field<TField>(name, value);
            this.fields.Add(field);
            return this;
        }

        /// <summary>
        /// 标题头简单样式
        /// </summary>
        /// <returns></returns>
        private Style GetHeaderStyle()
        {
            var style = new Style();
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.VerticalAlignment = TextAlignmentType.Center;
            style.Font.Name = "Arial";
            style.Font.Size = 11;
            style.IsTextWrapped = false;
            style.Font.IsBold = true;
            return style;
        }

        /// <summary>
        /// 保存为csv格式
        /// </summary>
        /// <param name="stream">流</param>
        public void SaveAsCsv(Stream stream)
        {
            var sb = new StringBuilder();
            var fieldItem = this.fields.Select(item => item.Name);
            var head = string.Join(",", fieldItem);
            sb.AppendLine(head);

            foreach (var item in this.Models)
            {
                var lineItems = this.fields.Select(f =>
                {
                    var v = f.GetValue(item);
                    return v == null ? null : v.ToString();
                });
                var line = string.Join(",", lineItems);
                sb.AppendLine(line);
            }

            var datas = Encoding.UTF8.GetBytes(sb.ToString());
            stream.Write(datas, 0, datas.Length);
        }


        /// <summary>
        /// 保存到文件
        /// </summary>
        /// <param name="stream">流</param>
        public void Save(Stream stream)
        {
            var xls = new Aspose.Cells.Workbook();
            var sheet = xls.Worksheets[0];
            var style = this.GetHeaderStyle();

            for (var i = 0; i < this.fields.Count; i++)
            {
                var header = sheet.Cells[0, i];
                header.SetStyle(style, true);
                header.PutValue(this.fields[i].Name);
            }

            var row = 0;
            foreach (var model in this.Models)
            {
                row = row + 1;
                for (var i = 0; i < this.fields.Count; i++)
                {
                    var fieldValue = this.fields[i].GetValue(model);
                    sheet.Cells[row, i].PutValue(fieldValue);
                }
            }

            sheet.AutoFitColumns();
            xls.Save(stream, Aspose.Cells.SaveFormat.Excel97To2003);
        }

        /// <summary>
        /// 字段接口
        /// </summary>
        private interface IField
        {
            string Name { get; }
            object GetValue(T model);
        }

        /// <summary>
        /// 表示字段信息
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        private class Field<TKey> : IField
        {
            private Func<T, TKey> valueFunc;

            public string Name { get; private set; }

            public Field(string name, Func<T, TKey> value)
            {
                this.Name = name;
                this.valueFunc = value;
            }

            public object GetValue(T model)
            {
                return this.valueFunc(model);
            }
        }
    }
}
