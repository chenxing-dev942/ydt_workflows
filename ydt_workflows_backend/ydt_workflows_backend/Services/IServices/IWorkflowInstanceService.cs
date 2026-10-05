using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Dtos.MyWorkflowWait;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 流程实例模型【根据流程运行流程】Service接口
    /// </summary>
    public interface IWorkflowInstanceService
    {
        /// <summary>
        /// 1、流程实例模型【根据流程运行流程】创建
        /// </summary>
        /// <param name="WorkflowInstanceCreateDto"></param>
        /// <returns></returns>
        public Task<bool> WorkflowInstanceCreateAsync(WorkflowInstanceCreateDto WorkflowInstanceCreateDto);

        /// <summary>
        /// 2、流程实例模型【根据流程运行流程】集合查询
        /// </summary>
        /// <returns></returns>
        public Task<List<WorkflowInstanceDto>> WorkflowInstanceGetListAsync(WorkflowInstanceGetListDto WorkflowInstanceGetListDto);

        /// <summary>
        /// 2.1、流程实例模型【根据流程运行流程】集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<MyWorkflowPageDto> WorkflowInstanceGetListPageAsync(MyWorkflowGetListPageDto WorkflowInstanceGetListPageDto);
        /// <summary>
        /// 3、流程实例模型【根据流程运行流程】查询【根据InstanceId查询】
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowInstanceGetResultDto> WorkflowInstanceGetAsync(string InstanceId);
        /// <summary>
        /// 4、流程实例模型【根据流程运行流程】更新
        /// </summary>
        /// <returns></returns>
        public Task<bool> WorkflowInstanceUpdateAsync(WorkflowInstanceUpdateDto WorkflowInstanceUpdateDto, string InstanceId);
        /// <summary>
        /// 5、流程实例模型【根据流程运行流程】删除
        /// </summary>
        /// <returns></returns>
        public Task<bool> WorkflowInstanceDeleteAsync(List<string> InstanceIds);
        /// <summary>
        /// 6/工作流和工作流分类集合查询
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowAndWorkflowCategoryDto> WorkflowAndWorkflowCategoryGetListAsync();

        /// <summary>
        /// 打开流程
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        public Task<WorkflowOpenDto> WorkflowOpenAsync(string flowId);

        /// <summary>
        /// 查看流程图
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        public Task<WorkflowImageDto> WorkflowImageAsync(string flowId);

        /// <summary>
        /// 工作流实例执行-提交
        /// </summary>
        /// <param name="workflowInstanceProcessSubmitDto"></param>
        /// <returns></returns>
        public Task<bool> WorkflowInstanceProcessSubmitAsync(WorkflowInstanceProcessSubmitDto workflowInstanceProcessSubmitDto);

        /// <summary>
        /// 工作流实例执行-同意
        /// </summary>
        /// <param name="workflowInstanceProcessAgreeDto"></param>
        /// <returns></returns>
        public Task<bool> WorkflowInstanceProcessAgreeAsync(WorkflowInstanceProcessAgreeDto workflowInstanceProcessAgreeDto);

        /// <summary>
        /// 工作流实例执行-不同意
        /// </summary>
        /// <param name="workflowInstanceProcessDeprecateDto"></param>
        /// <returns></returns>
        public Task<bool> WorkflowInstanceProcessDeprecateAsync(WorkflowInstanceProcessDeprecateDto workflowInstanceProcessDeprecateDto);

        /// <summary>
        /// 工作流实例执行-退回
        /// </summary>
        /// <param name="workflowInstanceProcessBackDto"></param>
        /// <returns></returns>
        public Task<bool> WorkflowInstanceProcessBackAsync(WorkflowInstanceProcessBackDto workflowInstanceProcessBackDto);

        /// <summary>
        /// 工作流实例执行-再次提交
        /// </summary>
        /// <param name="workflowInstanceProcessSubmitDto"></param>
        /// <returns></returns>
        public Task<bool> WorkflowInstanceProcessReSubmitAsync(WorkflowInstanceProcessSubmitDto workflowInstanceProcessSubmitDto);

        /// <summary>
        /// 工作流实例执行-委托
        /// </summary>
        /// <param name="workflowInstanceProcessAssignDto"></param>
        /// <returns></returns>
        public Task<bool> WorkflowInstanceProcessAssignAsync(WorkflowInstanceProcessAssignDto workflowInstanceProcessAssignDto);

        /// <summary>
        /// 工作流实例执行-审批意见
        /// </summary>
        /// <param name="workflowInstanceProcessApprovalDto"></param>
        /// <returns></returns>
        public Task<List<WorkflowOperationHistoryDto>> WorkflowInstanceProcessApprovalAsync(WorkflowInstanceProcessApprovalDto workflowInstanceProcessApprovalDto);

        /// <summary>
        /// 工作流实例执行-撤回
        /// </summary>
        /// <param name="workflowInstanceProcessWithdrawDto"></param>
        /// <returns></returns>
        public Task<bool> WorkflowInstanceProcessWithdrawAsync(WorkflowInstanceProcessWithdrawDto workflowInstanceProcessWithdrawDto);

        /// <summary>
        /// 工作流实例执行-催办
        /// </summary>
        /// <param name="workflowInstanceProcessUrgeDto"></param>
        /// <returns></returns>
        public Task<bool> WorkflowInstanceProcessUrgeAsync(WorkflowInstanceProcessUrgeDto workflowInstanceProcessUrgeDto);

        /// <summary>
        /// 我的待办
        /// </summary>
        /// <param name="myWorkflowWaitGetListPageDto"></param>
        /// <returns></returns>
        public Task<MyWorkflowWaitPageDto> MyWorkflowWaitGetListPageAsync(MyWorkflowWaitGetListPageDto myWorkflowWaitGetListPageDto);
    }
}
