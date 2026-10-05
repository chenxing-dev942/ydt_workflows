using JadeFramework.WorkFlow;

namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 流程实例执行-提交Dto
    /// </summary>
    public class WorkflowInstanceProcessAgreeDto
    {
        public string InstanceId { set; get; } // 工作流实例Id
        public string AgreeContent { set; get; } // 同意内容
        public string UserId { set; get; } // 用户Id
        public string UserName { set; get; } // 用户名

    }
}
