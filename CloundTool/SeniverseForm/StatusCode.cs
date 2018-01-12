using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeniverseForm
{
    /// <summary>
    /// 接口错误码
    /// </summary>
    public enum StatusCode
    {
        /// <summary>
        /// API请求参数错误
        /// </summary>
        [Display(Name = "AP010001", Description = "API请求参数错误")]
        AP010001,

        /// <summary>
        /// 没有权限访问这个API接口
        /// </summary>
        [Display(Name = "AP010002", Description = "没有权限访问这个API接口")]
        AP010002,

        /// <summary>
        /// API密钥key错误
        /// </summary>
        [Display(Name = "AP010003", Description = "API密钥key错误")]
        AP010003,

        /// <summary>
        /// 签名错误
        /// </summary>
        [Display(Name = "AP010004", Description = "签名错误,请重试")]
        AP010004,

        /// <summary>
        /// 你请求的API不存在
        /// </summary>
        [Display(Name = "AP010005", Description = "你请求的API不存在")]
        AP010005,

        /// <summary>
        /// 没有权限访问这个地点
        /// </summary>
        [Display(Name = "AP010006", Description = "没有权限访问这个地点")]
        AP010006,

        /// <summary>
        /// JSONP请求需要使用签名验证方式
        /// </summary>
        [Display(Name = "AP010007", Description = "JSONP请求需要使用签名验证方式")]
        AP010007,

        /// <summary>
        /// 没有绑定域名
        /// </summary>
        [Display(Name = "AP010008", Description = "没有绑定域名")]
        AP010008,

        /// <summary>
        /// API请求的user-agent与你设置的不一致
        /// </summary>
        [Display(Name = "AP010009", Description = "API请求的user-agent与你设置的不一致")]
        AP010009,

        /// <summary>
        /// 没有这个地点
        /// </summary>
        [Display(Name = "AP010010", Description = "没有这个地点")]
        AP010010,

        /// <summary>
        /// 无法查找到指定IP地址对应的城市
        /// </summary>
        [Display(Name = "AP010011", Description = "无法查找到指定IP地址对应的城市")]
        AP010011,

        /// <summary>
        /// 你的服务已经过期
        /// </summary>
        [Display(Name = "AP010012", Description = "你的服务已经过期")]
        AP010012,

        /// <summary>
        /// 访问量余额不足
        /// </summary>
        [Display(Name = "AP010013", Description = "访问量余额不足")]
        AP010013,

        /// <summary>
        /// 免费用户超过了每小时访问量额度,一小时后自动恢复
        /// </summary>
        [Display(Name = "AP010014", Description = "免费用户超过了每小时访问量额度,一小时后自动恢复")]
        AP010014,

        /// <summary>
        /// 暂不支持该城市的车辆限行信息
        /// </summary>
        [Display(Name = "AP010015", Description = "暂不支持该城市的车辆限行信息")]
        AP010015,

        /// <summary>
        /// 系统内部错误：数据缺失
        /// </summary>
        [Display(Name = "AP100001", Description = "系统内部错误：数据缺失")]
        AP100001,

        /// <summary>
        /// 系统内部错误：数据错误
        /// </summary>
        [Display(Name = "AP100002", Description = "系统内部错误：数据错误")]
        AP100002,

        /// <summary>
        /// 系统内部错误：服务内部错误
        /// </summary>
        [Display(Name = "AP100003", Description = "系统内部错误：服务内部错误")]
        AP100003
    }

    /// <summary>
    /// 错误返回实体
    /// </summary>
    public class ResultError
    {
        /// <summary>
        /// 状态
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 错码
        /// </summary>
        public StatusCode status_code { get; set; }
    }



}
