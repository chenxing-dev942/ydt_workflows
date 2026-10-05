using JadeFramework.WorkFlow;

namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 查看工作流实例Dto
    /// </summary>
    public class WorkflowInstanceGetResultDto
    {
        public string FlowId { get; set; }
        public string FlowName { get; set; }
        public string InstanceId { get; set; } // 工作流实例Id
        public string FormId { get; set; }
        public WorkFlowFormType FormType { get; set; }
        public string FormContent { get; set; } // json格式
        public string FormUrl { get; set; }
        public string FormData { get; set; }

        //
        // 摘要:
        //     可操作按钮集合 JadeFramework.WorkFlow.WorkFlowMenu集合
        public List<int> Menus { get; set; }

        //
        // 摘要:
        //     流程信息
        public WorkFlowProcessFlowData FlowData { get; set; }

    }
}
