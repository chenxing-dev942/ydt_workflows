using JadeFramework.WorkFlow;

namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 流程实例模型【根据流程运行流程】创建Dto
    /// </summary>
    public class WorkflowInstanceCreateDto
    {
        public string FlowId { get; set; }
        public string FlowName { get; set; }
        public string FormId { get; set; }
        public WorkFlowFormType FormType { get; set; }
        //public string FormContent { get; set; }
        public string FormUrl { get; set; }
        public string FormData { get; set; }

        public string UserId { set; get; } // 用户Id
        public string UserName { set; get; } // 用户名
    }
}
