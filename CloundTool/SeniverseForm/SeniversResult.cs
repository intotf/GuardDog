using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeniverseForm
{
    /// <summary>
    /// 心知天气-逐日天气预报实体
    /// </summary>
    public class SeniversResult
    {
        /// <summary>
        /// 详细内容
        /// </summary>
        public Result[] results { get; set; }
    }

    /// <summary>
    /// 详细内容
    /// </summary>
    public class Result
    {
        /// <summary>
        /// 城市信息
        /// </summary>
        public Location location { get; set; }

        /// <summary>
        /// 逐日天气数据
        /// </summary>
        public Daily[] daily { get; set; }

        /// <summary>
        /// 数据更新时间（该城市的本地时间）
        /// </summary>
        public DateTime last_update { get; set; }
    }

    /// <summary>
    /// 城市排名数组，从好到差排序
    /// </summary>
    public class Location
    {
        /// <summary>
        /// 城市ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 国家代码
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// 城市隶属层级，从小到大
        /// </summary>
        public string path { get; set; }

        /// <summary>
        /// IANA标准时区名称（该名称不受夏令时影响）
        /// </summary>
        public string timezone { get; set; }

        /// <summary>
        /// 相对于UTC时区的偏移量（采用夏令时的城市会因夏令时而变化）
        /// </summary>
        public string timezone_offset { get; set; }
    }

    /// <summary>
    /// 天气数据
    /// </summary>
    public class Daily
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string date { get; set; }

        /// <summary>
        /// 白天天气现象文字/多云
        /// </summary>
        public string text_day { get; set; }

        /// <summary>
        /// 天气现象代码
        /// </summary>
        public string code_day { get; set; }

        /// <summary>
        /// 晚间天气现象文字
        /// </summary>
        public string text_night { get; set; }

        /// <summary>
        /// 晚间天气现象代码
        /// </summary>
        public string code_night { get; set; }

        /// <summary>
        /// 当天最高温度
        /// </summary>
        public string high { get; set; }

        /// <summary>
        /// 当天最低温度
        /// </summary>
        public string low { get; set; }

        /// <summary>
        /// 降水概率，范围0~100，单位百分比
        /// </summary>
        public string precip { get; set; }

        /// <summary>
        /// 风向文字
        /// </summary>
        public string wind_direction { get; set; }

        /// <summary>
        /// 风向角度，范围0~360
        /// </summary>
        public string wind_direction_degree { get; set; }

        /// <summary>
        /// 风速，单位km/h（当unit=c时）、mph（当unit=f时）
        /// </summary>
        public string wind_speed { get; set; }

        /// <summary>
        /// 风力等级
        /// </summary>
        public string wind_scale { get; set; }
    }

}
