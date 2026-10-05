namespace ydt_workflows_backend.Dtos.MyWorkflowWait
{
    /// <summary>
    /// 入参Dto
    /// </summary>
    public class MyWorkflowWaitGetListPageDto
    {
        public int UserId { set; get; }

        public string UserName { set; get; }

        /// <summary>
        /// 当前页【1 2 3】
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页大小【10 20 30】
        /// </summary>
        public int PageSize { set; get; }

        /// <summary>
        /// 分页偏移量
        /// </summary>
        /// <returns></returns>
        public int OffSet()
        {
            return (PageIndex - 1) * PageSize; // 0 10 20 30 
        }
    }
}
