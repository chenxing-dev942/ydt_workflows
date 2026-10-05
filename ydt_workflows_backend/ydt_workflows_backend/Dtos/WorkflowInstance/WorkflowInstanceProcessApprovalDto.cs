using JadeFramework.WorkFlow;

namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 流程实例执行-审批意见Dto
    /// </summary>
    public class WorkflowInstanceProcessApprovalDto
    {
        public string InstanceId { set; get; } // 工作流实例Id
        public string UserId { set; get; } // 用户Id
        public string UserName { set; get; } // 用户名
    }
}
