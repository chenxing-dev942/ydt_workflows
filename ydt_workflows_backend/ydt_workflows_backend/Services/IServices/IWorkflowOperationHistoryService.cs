using ydt_workflows_backend.Dtos;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 流程操作历史模型Service接口
    /// </summary>
    public interface IWorkflowOperationHistoryService
    {
        /// <summary>
        /// 1、流程操作历史模型创建
        /// </summary>
        /// <param name="WorkflowOperationHistoryCreateDto"></param>
        /// <returns></returns>
        public Task<bool> WorkflowOperationHistoryCreateAsync(WorkflowOperationHistoryCreateDto WorkflowOperationHistoryCreateDto);

        /// <summary>
        /// 2、流程操作历史模型集合查询
        /// </summary>
        /// <returns></returns>
        public Task<List<WorkflowOperationHistoryDto>> WorkflowOperationHistoryGetListAsync(WorkflowOperationHistoryGetListDto WorkflowOperationHistoryGetListDto);

        /// <summary>
        /// 2.1、流程操作历史模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowOperationHistoryPageDto> WorkflowOperationHistoryGetListPageAsync(WorkflowOperationHistoryGetListPageDto WorkflowOperationHistoryGetListPageDto);
        /// <summary>
        /// 3、流程操作历史模型查询【根据OperationId查询】
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowOperationHistoryDto> WorkflowOperationHistoryGetAsync(string OperationId);
        /// <summary>
        /// 4、流程操作历史模型更新
        /// </summary>
        /// <returns></returns>
        public Task<bool> WorkflowOperationHistoryUpdateAsync(WorkflowOperationHistoryUpdateDto WorkflowOperationHistoryUpdateDto, string OperationId);
        /// <summary>
        /// 5、流程操作历史模型删除
        /// </summary>
        /// <returns></returns>
        public Task<bool> WorkflowOperationHistoryDeleteAsync(List<string> OperationIds);
    }
}
