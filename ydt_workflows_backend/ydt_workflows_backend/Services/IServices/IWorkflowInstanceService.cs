using ydt_workflows_backend.Dtos;

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
        public Task<WorkflowInstancePageDto> WorkflowInstanceGetListPageAsync(WorkflowInstanceGetListPageDto WorkflowInstanceGetListPageDto);
        /// <summary>
        /// 3、流程实例模型【根据流程运行流程】查询【根据InstanceId查询】
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowInstanceDto> WorkflowInstanceGetAsync(string InstanceId);
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
    }
}
