using ydt_workflows_backend.Dtos;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 流程委托模型Service接口
    /// </summary>
    public interface IWorkflowAssignService
    {
        /// <summary>
        /// 1、流程委托模型创建
        /// </summary>
        /// <param name="WorkflowAssignCreateDto"></param>
        /// <returns></returns>
        public Task<bool> WorkflowAssignCreateAsync(WorkflowAssignCreateDto WorkflowAssignCreateDto);

        /// <summary>
        /// 2、流程委托模型集合查询
        /// </summary>
        /// <returns></returns>
        public Task<List<WorkflowAssignDto>> WorkflowAssignGetListAsync(WorkflowAssignGetListDto WorkflowAssignGetListDto);

        /// <summary>
        /// 2.1、流程委托模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowAssignPageDto> WorkflowAssignGetListPageAsync(WorkflowAssignGetListPageDto WorkflowAssignGetListPageDto);
        /// <summary>
        /// 3、流程委托模型查询【根据AssignId查询】
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowAssignDto> WorkflowAssignGetAsync(string AssignId);
        /// <summary>
        /// 4、流程委托模型更新
        /// </summary>
        /// <returns></returns>
        public Task<bool> WorkflowAssignUpdateAsync(WorkflowAssignUpdateDto WorkflowAssignUpdateDto, string AssignId);
        /// <summary>
        /// 5、流程委托模型删除
        /// </summary>
        /// <returns></returns>
        public Task<bool> WorkflowAssignDeleteAsync(List<string> AssignIds);
    }
}
