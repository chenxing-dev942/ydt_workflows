namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 流程实例模型【根据流程运行流程】分页查询结果Dto
    /// </summary>
    public class WorkflowInstancePageDto
    {
        /// <summary>
        /// 1、流程实例模型【根据流程运行流程】集合
        /// </summary>
        public List<WorkflowInstanceDto> WorkflowInstances { get; set; }


        public int TotalPages
        {
            get
            {
                if (TotalItems <= 0 || PageSize <= 0)
                    return 0;
                var totalPage = TotalItems / PageSize;
                if (TotalItems % PageSize != 0)
                {
                    totalPage++;
                }
                return totalPage;
            }
        }
     
        /// <summary>
        /// 3、总条数
        /// </summary>
        public int TotalItems { set; get; }

        /// <summary>
        /// 当前页【1 2 3】
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 5、每页显示的条数【默认显示10条】
        /// </summary>
        public int PageSize { set; get; }

        public WorkflowInstancePageDto()
        {
            WorkflowInstances = new List<WorkflowInstanceDto>();
        }
    }
}
