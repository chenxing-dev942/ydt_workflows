namespace ydt_workflows_backend.Dtos.MyWorkflowWait
{
    /// <summary>
    /// 出参Dto
    /// </summary>
    public class MyWorkflowWaitPageDto
    {
        /// <summary>
        /// 1、待办集合
        /// </summary>
        public List<MyWorkflowWaitListDto> myWorkflowWaitListDtos { get; set; }


        public int TotalPages
        {
            get
            {
                return _TotalPages();
            }
        }
        /// <summary>
        /// 2、总页数
        /// </summary>
        private int _TotalPages()
        {
            // 1、总条数 / 每页的条数=总页数
            var TotalPage = TotalItems / PageSize;

            // 1.1、取模运算
            var Count = TotalItems % PageSize;
            if (Count > 0)
            {
                TotalPage++;
            }
            return TotalPage;
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

        public MyWorkflowWaitPageDto()
        {
            myWorkflowWaitListDtos = new List<MyWorkflowWaitListDto>();
        }
    }

    /// <summary>
    /// 待办模型
    /// </summary>
    public class MyWorkflowWaitListDto
    {
        public string InstanceId { get; set; }
        public string Code { get; set; } // 实例编号
        public int? Status { set; get; } // 实例状态
        public string CreateUserName { get; set; } // 发起人

        public string FlowId { get; set; } // 工作流Id
        public string FlowName { get; set; } // 流程名称

        public string FormId { get; set; } // 工作流表单Id
        public string FormName { get; set; } // 表单名称
        public int FormType { get; set; } // 表单类型

        public long CreateTime { set; get; } // 创建时间
    }
}
