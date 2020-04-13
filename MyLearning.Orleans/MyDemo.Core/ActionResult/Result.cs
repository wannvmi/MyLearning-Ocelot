using System;

namespace MyDemo.Core
{
    public class Result<T> where T : class
    {
        public ResultCode code { get; set; }

        public string msg { get; set; }

        public T data { get; set; }

        public Result(T data)
        {
            code = ResultCode.Ok;
            msg = ResultCode.Ok.GetDescription();
            this.data = data;
        }

        public Result(ResultCode code, string msg, T data)
        {
            this.code = code;
            this.msg = msg ?? code.GetDescription();
            this.data = data;
        }

        public static Result<T> FromCode(ResultCode code, string msg = null, T data = default)
        {
            if (data == null)
                data = (T) new Object();
            return new Result<T>(code, msg, data);
        }

        public static Result<T> Ok(T data)
        {
            return new Result<T>(data);
        }

        public static Result<T> FromError(ResultCode code = ResultCode.Fail, string msg = null, T data = default)
        {
            return new Result<T>(code, msg, data);
        }
    }
}
