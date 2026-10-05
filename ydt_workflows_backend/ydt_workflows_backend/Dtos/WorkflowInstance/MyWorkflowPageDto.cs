namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 流程实例模型【根据流程运行流程】分页查询结果Dto
    /// </summary>
    public class MyWorkflowPageDto
    {
        /// <summary>
        /// 1、流程实例模型【根据流程运行流程】集合
        /// </summary>
        public List<MyWorkflowDto> MyWorkflowDtos { get; set; }


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

        public MyWorkflowPageDto()
        {
            MyWorkflowDtos = new List<MyWorkflowDto>();
        }

        /// <summary>
        ///  我的流程模型
        /// </summary>

        public class MyWorkflowDto
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
}
