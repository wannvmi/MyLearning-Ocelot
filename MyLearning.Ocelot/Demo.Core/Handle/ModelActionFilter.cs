using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Demo
{
    public class ModelActionFilter : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errorResults = new List<ErrorResultDto>();
                foreach (var item in context.ModelState)
                {
                    var result = new ErrorResultDto
                    {
                        Field = item.Key,
                        Msg = "",
                    };
                    foreach (var error in item.Value.Errors)
                    {
                        if (!string.IsNullOrEmpty(result.Msg))
                        {
                            result.Msg += '|';
                        }
                        result.Msg += error.ErrorMessage;
                    }
                    errorResults.Add(result);
                }
                context.Result = new JsonResult(Result<List<ErrorResultDto>>.FromCode(ResultCode.InvalidData, errorResults));
            }
        }
    }

    public class ErrorResultDto
    {
        /// <summary>
        /// 参数领域
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Msg { get; set; }
    }
}
