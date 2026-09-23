using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ydt_workflows_backend.CommonResults
{
    public class CommonResultFilter : IAsyncResultFilter
    {
        /// <summary>
        /// 1.通用结果包装
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            // 1、获取Action结果
            IActionResult actionResult = context.Result;

            // 2、判断Action结果类型 ObjectResult
            if (actionResult is ObjectResult objectResult)
            {
                // 2.1、获取结果值
                object result = objectResult.Value;

                // 2.1.1、结果值判断
                if (result is CommonResult common)
                {
                    // 2.3、结果返回
                    context.Result = new JsonResult(common);
                }
                else
                {
                    // 2.2、结果包装
                    CommonResult commonResult = new CommonResult();
                    commonResult.ErrorNo = "0";
                    commonResult.Result = result;

                    // 2.3、结果返回
                    context.Result = new JsonResult(commonResult);
                }
            }
            // 3、判断Action结果类型 EmptyResult
            else if (actionResult is EmptyResult emptyResult)
            {
                //3.1、空结果包装
                CommonResult commonResult = new CommonResult();
                commonResult.ErrorNo = "0";
                commonResult.Result = "无结果";

                // 3.2 、结果返回
                context.Result = new JsonResult(commonResult);
            }
            // 4、判断Action结果类型 JsonResult
            else if (actionResult is JsonResult jsonResult)
            {
                // 4.1、JsonResult包装
                CommonResult commonResult = new CommonResult();
                commonResult.ErrorNo = "0";
                commonResult.Result = jsonResult.Value; // Json值

                // 4.2、结果返回
                context.Result = new JsonResult(commonResult);
            }
            // 5、判断Action结果类型 StatusCodeResult
            else if (actionResult is StatusCodeResult statusCodeResult)
            {
                // 4.1、StatusCodeResult包装
                CommonResult commonResult = new CommonResult();
                commonResult.ErrorNo = "0";
                commonResult.Result = statusCodeResult.StatusCode; // 状态码

                // 4.2、结果返回
                context.Result = new JsonResult(commonResult);
            }

            // 3、下一个过滤器
            await next();
        }
    }
}
