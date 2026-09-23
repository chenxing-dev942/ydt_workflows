using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ydt_workflows_backend.CommonExceptions
{
    public class CommonExceptionFilter: IExceptionFilter
    {
        /// <summary>
        /// 1、实现通用异常包装
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnException(ExceptionContext context)
        {
            // 1、获取异常
            Exception exception = context.Exception;

            // 1.1、异常判断
            if (exception is CommonException common)
            {
                // 1.1.1、通用异常结果包装
                CommonExceptionResult commonExceptionResult = new CommonExceptionResult();
                commonExceptionResult.ErrorNo = common.ErrorNo;
                commonExceptionResult.ErrorInfo = common.ErrorInfo;
                commonExceptionResult.ErrorReason = common.StackTrace;

                // 1.1.2、通用异常结果返回
                context.Result = new JsonResult(commonExceptionResult);
            }
            else
            {
                // 2、异常包装
                CommonException commonException = new CommonException();
                commonException.ErrorNo = "-1"; // 异常状态
                commonException.ErrorInfo = exception.Message;
                //commonException.ErrorReason = exception.StackTrace;

                // 3、异常包装返回
                context.Exception = commonException;

                // 4、通用异常结果包装
                CommonExceptionResult commonExceptionResult = new CommonExceptionResult();
                commonExceptionResult.ErrorNo = commonException.ErrorNo;
                commonExceptionResult.ErrorInfo = commonException.ErrorInfo;
                commonExceptionResult.ErrorReason = commonException.StackTrace;

                // 5、通用异常结果返回
                context.Result = new JsonResult(commonExceptionResult);
            }

        }
    }
}
