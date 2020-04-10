using System.ComponentModel;

namespace MyDemo.Core
{
    public enum ResultCode
    {

        /// <summary>
        /// 操作成功
        ///</summary>
        [Description("操作成功")]
        Ok = 1,

        /// <summary>
        /// 操作失败
        ///</summary>
        [Description("操作失败")]
        Fail = 0,

        /// <summary>
        /// 登陆失败
        ///</summary>
        [Description("登陆失败")]
        LoginFail = 12,

        /// <summary>
        /// 没有该数据
        ///</summary>
        [Description("没有数据")]
        NoRecord = 13,

        /// <summary>
        /// 用户不存在
        ///</summary>
        [Description("用户不存在")]
        NoSuchUser = 14,

        /// <summary>
        /// 密码错误
        /// </summary>
        [Description("密码错误")]
        WrongPassword = 15,

        /// <summary>
        /// 未登录
        ///</summary>
        [Description("未登录")]
        Unauthorized = 20,

        /// <summary>
        /// 未授权
        /// </summary>
        [Description("未授权")]
        Forbidden = 21,

        /// <summary>
        /// 无效Token
        /// </summary>
        [Description("无效Token")]
        InvalidToken = 22,

        /// <summary>
        /// 参数验证失败
        /// </summary>
        [Description("参数验证失败")]
        InvalidData = 23,

        /// <summary>
        /// 无效用户
        /// </summary>
        [Description("无效用户")]
        InvalidUser = 24
    }
}
