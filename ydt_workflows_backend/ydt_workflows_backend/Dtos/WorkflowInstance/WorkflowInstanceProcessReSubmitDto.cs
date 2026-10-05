using JadeFramework.WorkFlow;

namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 流程实例执行-退回Dto
    /// </summary>
    public class WorkflowInstanceProcessReSubmitDto
    {
        public string InstanceId { set; get; } // 工作流实例Id
        public string FlowId { set; get; } // 工作流Id
        public string UserId { set; get; } // 用户Id
        public string UserName { set; get; } // 用户名

    }
}
