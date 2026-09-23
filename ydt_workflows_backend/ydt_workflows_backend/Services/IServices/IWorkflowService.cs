using ydt_workflows_backend.Dtos;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 工作流模型Service接口
    /// </summary>
    public interface IWorkflowService
    {
        /// <summary>
        /// 1、工作流模型创建
        /// </summary>
        /// <param name="WorkflowCreateDto"></param>
        /// <returns></returns>
        public Task<bool> WorkflowCreateAsync(WorkflowCreateDto WorkflowCreateDto);

        /// <summary>
        /// 2、工作流模型集合查询
        /// </summary>
        /// <returns></returns>
        public Task<List<WorkflowDto>> WorkflowGetListAsync(WorkflowGetListDto WorkflowGetListDto);

        /// <summary>
        /// 2.1、工作流模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowPageDto> WorkflowGetListPageAsync(WorkflowGetListPageDto WorkflowGetListPageDto);
        /// <summary>
        /// 3、工作流模型查询【根据FlowId查询】
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowDto> WorkflowGetAsync(string FlowId);
        /// <summary>
        /// 4、工作流模型更新
        /// </summary>
        /// <returns></returns>
        public Task<bool> WorkflowUpdateAsync(WorkflowUpdateDto WorkflowUpdateDto, string FlowId);
        /// <summary>
        /// 5、工作流模型删除
        /// </summary>
        /// <returns></returns>
        public Task<bool> WorkflowDeleteAsync(List<string> FlowIds);
    }
}
