namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 流程操作历史模型集合查询结果Dto
    /// </summary>
    public class WorkflowOperationHistoryDto
    {
        public string InstanceId { get; set; }
        public string InstanceCode { get; set; } // 实例编号
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
