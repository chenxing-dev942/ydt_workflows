namespace ydt_workflows_backend.CommonExceptions
{
    public class CommonExceptionResult
    {
        public string ErrorNo { get; set; } // 异常编号
        public string ErrorInfo { get; set; }  // 抽象 message
        public string ErrorReason { set; get; } // 抽象 StackTrace
    }
}
