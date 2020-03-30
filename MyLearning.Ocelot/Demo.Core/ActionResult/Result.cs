using System;
using System.Collections.Generic;
using System.Text;

namespace Demo
{
    public class Result : IResult
    {
        public ResultCode code { get; set; }

        public string message { get; set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="message">提示信息</param>
        public Result(ResultCode code, string message = null)
        {
            this.code = code;
            this.message = message ?? code.GetDescription();
        }

        /// <summary>
        /// 返回指定 Code
        /// </summary>
        public static Result FromCode(ResultCode code, string message = null)
        {
            return new Result(code, message);
        }

        /// <summary>
        /// 返回指定 Code
        /// </summary>
        public static Result<TType> FromCode<TType>(ResultCode code, TType data, string message = null) where TType : class, new()
        {
            return new Result<TType>(code, data, message);
        }

        /// <summary>
        /// 返回错误信息
        /// </summary>
        public static Result FromError(string message, ResultCode code = ResultCode.Fail)
        {
            return new Result(code, message);
        }

        /// <summary>
        /// 返回错误信息
        /// </summary>
        public static Result<TType> FromError<TType>(string message, ResultCode code = ResultCode.Fail) where TType : class, new()
        {
            return new Result<TType>(code, new TType(), message);
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        public static Result Ok(string message = null)
        {
            return FromCode(ResultCode.Ok, message);
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        public static Result<TType> Ok<TType>(TType data = default(TType), string message = null) where TType : class, new()
        {
            return FromCode(ResultCode.Ok, data, message);
        }

    }

    public class Result<TType> : Result, IResult<TType> where TType : class, new()
    {
        public TType data { get; set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        public Result(TType data)
            : base(ResultCode.Ok)
        {
            this.data = data;
        }

        /// <summary>
        /// 返回结果
        /// </summary>
        public Result(ResultCode code, TType data, string message = null)
            : base(code, message)
        {
            this.data = data;
        }
    }
}
