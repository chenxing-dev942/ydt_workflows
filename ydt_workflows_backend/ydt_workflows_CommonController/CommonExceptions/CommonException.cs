namespace ydt_workflows_backend.CommonExceptions
{
    /// <summary>
    /// 通用异常
    /// </summary>
    public class CommonException:Exception
    {
        public CommonException()
        {
        }

        public CommonException(string? message) : base(message)
        {
            ErrorInfo = message;
        }

        public string ErrorNo { get; set; }
        public string ErrorInfo {  get; set; }//抽象message
    }
}
