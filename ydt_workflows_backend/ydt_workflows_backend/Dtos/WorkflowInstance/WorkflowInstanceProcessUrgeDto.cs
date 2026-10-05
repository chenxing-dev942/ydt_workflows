using JadeFramework.WorkFlow;

namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 流程实例执行-催办Dto
    /// </summary>
    public class WorkflowInstanceProcessUrgeDto
    {
        public string InstanceId { set; get; } // 工作流实例Id
        public string UrgeConent { set; get; }// 催办内容
        public int UrgeType { set; get; } // 催办类型
        public string UserId { set; get; } // 用户Id
        public string UserName { set; get; } // 用户名
        public string Sender { set; get; } // 给谁催办。

    }
}
