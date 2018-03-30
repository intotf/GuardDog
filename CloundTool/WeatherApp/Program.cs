using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text;
using System.Threading.Tasks;
using WeatherLib;
using System.ComponentModel.DataAnnotations;

namespace WeatherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "心知天气查询";
            Console.WriteLine("请输要查询的地区(帮助请输 -h)：");
            Search();
        }

        static void HelpInfo()
        {
            Console.WriteLine(@"
------------------------------------------
    城市ID 例如：WX4FBXXFKE4F
    城市中文名 例如：北京
    省市名称组合 例如：辽宁朝阳、北京朝阳
    城市拼音/英文名 例如：beijing（如拼音相同城市，可在之前加省份和空格，例：shanxi yulin）
    经纬度 例如：39.93:116.40（格式是 纬度:经度，英文冒号分隔）
    IP地址 例如：220.181.111.86（某些IP地址可能无法定位到城市）
    “ip”两个字母 自动识别请求IP地址，例如：ip
------------------------------------------");
        }

        static void Search()
        {
            var area = Console.ReadLine();
            if (area.Trim().Equals("-h", StringComparison.OrdinalIgnoreCase))
            {
                HelpInfo();
                Search();
                return;
            }
            GetWeather(area);
            Search();
        }

        static async void GetWeather(string area)
        {
            var data = await WeatherApi.GetTodayRestAsync(area);
            if (!data.State)
            {
                Console.WriteLine(data.Msg);
                return;
            }
            var properties = data.Data.GetType().GetProperties();
            var sb = new StringBuilder();
            foreach (var item in properties)
            {
                var attr = item.GetCustomAttributes(typeof(DisplayAttribute), true).FirstOrDefault();
                if (attr != null)
                {
                    var nameText = ((DisplayAttribute)attr).Name;
                    sb.AppendLine(string.Format("{0} = {1}", nameText, item.GetValue(data.Data, null)));
                }
            }
            Console.WriteLine("{0} 天气情况如下：", area);
            Console.WriteLine(sb.ToString());
            Console.WriteLine("请输要查询的地区(帮助请输 -h)：");
        }
    }
}
