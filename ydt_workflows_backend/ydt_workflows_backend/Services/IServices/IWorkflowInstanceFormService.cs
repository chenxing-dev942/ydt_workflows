using ydt_workflows_backend.Dtos;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 流程实例表单关联模型Service接口
    /// </summary>
    public interface IWorkflowInstanceFormService
    {
        /// <summary>
        /// 1、流程实例表单关联模型创建
        /// </summary>
        /// <param name="WorkflowInstanceFormCreateDto"></param>
        /// <returns></returns>
        public Task<bool> WorkflowInstanceFormCreateAsync(WorkflowInstanceFormCreateDto WorkflowInstanceFormCreateDto);

        /// <summary>
        /// 2、流程实例表单关联模型集合查询
        /// </summary>
        /// <returns></returns>
        public Task<List<WorkflowInstanceFormDto>> WorkflowInstanceFormGetListAsync(WorkflowInstanceFormGetListDto WorkflowInstanceFormGetListDto);

        /// <summary>
        /// 2.1、流程实例表单关联模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowInstanceFormPageDto> WorkflowInstanceFormGetListPageAsync(WorkflowInstanceFormGetListPageDto WorkflowInstanceFormGetListPageDto);
        /// <summary>
        /// 3、流程实例表单关联模型查询【根据InstanceFormId查询】
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowInstanceFormDto> WorkflowInstanceFormGetAsync(string InstanceFormId);
        /// <summary>
        /// 4、流程实例表单关联模型更新
        /// </summary>
        /// <returns></returns>
        public Task<bool> WorkflowInstanceFormUpdateAsync(WorkflowInstanceFormUpdateDto WorkflowInstanceFormUpdateDto, string InstanceFormId);
        /// <summary>
        /// 5、流程实例表单关联模型删除
        /// </summary>
        /// <returns></returns>
        public Task<bool> WorkflowInstanceFormDeleteAsync(List<string> InstanceFormIds);
    }
}
