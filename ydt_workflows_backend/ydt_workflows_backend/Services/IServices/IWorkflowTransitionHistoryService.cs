using ydt_workflows_backend.Dtos;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 流程流转历史模型Service接口
    /// </summary>
    public interface IWorkflowTransitionHistoryService
    {
        /// <summary>
        /// 1、流程流转历史模型创建
        /// </summary>
        /// <param name="WorkflowTransitionHistoryCreateDto"></param>
        /// <returns></returns>
        public Task<bool> WorkflowTransitionHistoryCreateAsync(WorkflowTransitionHistoryCreateDto WorkflowTransitionHistoryCreateDto);

        /// <summary>
        /// 2、流程流转历史模型集合查询
        /// </summary>
        /// <returns></returns>
        public Task<List<WorkflowTransitionHistoryDto>> WorkflowTransitionHistoryGetListAsync(WorkflowTransitionHistoryGetListDto WorkflowTransitionHistoryGetListDto);

        /// <summary>
        /// 2.1、流程流转历史模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowTransitionHistoryPageDto> WorkflowTransitionHistoryGetListPageAsync(WorkflowTransitionHistoryGetListPageDto WorkflowTransitionHistoryGetListPageDto);
        /// <summary>
        /// 3、流程流转历史模型查询【根据TransitionId查询】
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowTransitionHistoryDto> WorkflowTransitionHistoryGetAsync(string TransitionId);
        /// <summary>
        /// 4、流程流转历史模型更新
        /// </summary>
        /// <returns></returns>
        public Task<bool> WorkflowTransitionHistoryUpdateAsync(WorkflowTransitionHistoryUpdateDto WorkflowTransitionHistoryUpdateDto, string TransitionId);
        /// <summary>
        /// 5、流程流转历史模型删除
        /// </summary>
        /// <returns></returns>
        public Task<bool> WorkflowTransitionHistoryDeleteAsync(List<string> TransitionIds);
    }
}
