namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 流程实例模型【根据流程运行流程】分页查询入参Dto
    /// </summary>
    public class MyWorkflowGetListPageDto
    {
        public int UserId { get; set; } // 用户Id
        public string UserName { get; set; } // 用户名称

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
            return (PageIndex-1) * PageSize; // 0 10 20 30 
        }
    }
}
