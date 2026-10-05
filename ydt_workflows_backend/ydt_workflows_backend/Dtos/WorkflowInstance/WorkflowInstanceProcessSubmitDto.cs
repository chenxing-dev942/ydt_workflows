namespace ydt_workflows_backend.Dtos
{

    /// <summary>
    /// 流程实例执行-提交Dto
    /// </summary>
    public class WorkflowInstanceProcessSubmitDto
    {
        public string FlowId { get; set; } // 工作流Id
        public string InstanceId { set; get; } // 工作流实例Id

        public string UserId { set; get; } // 用户Id
        public string UserName { set; get; } // 用户名

       
    }
}
