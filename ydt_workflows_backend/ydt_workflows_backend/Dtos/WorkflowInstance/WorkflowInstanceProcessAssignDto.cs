using JadeFramework.WorkFlow;

namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 流程实例执行-委托Dto
    /// </summary>
    public class WorkflowInstanceProcessAssignDto
    {
        public string FlowId { get; set; } // 工作流Id
        public string InstanceId { set; get; } // 工作流实例Id

        public string UserId { set; get; } // 用户Id
        public string UserName { set; get; } // 用户名

        /// <summary>
        /// 委托信息
        /// </summary>
        public FlowAssign flowAssign { get; set; }

    }
}
