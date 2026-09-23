using ydt_workflows_backend.Dtos;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 流程表单模型Service接口
    /// </summary>
    public interface IWorkflowFormService
    {
        /// <summary>
        /// 1、流程表单模型创建
        /// </summary>
        /// <param name="WorkflowFormCreateDto"></param>
        /// <returns></returns>
        public Task<bool> WorkflowFormCreateAsync(WorkflowFormCreateDto WorkflowFormCreateDto);

        /// <summary>
        /// 2、流程表单模型集合查询
        /// </summary>
        /// <returns></returns>
        public Task<List<WorkflowFormDto>> WorkflowFormGetListAsync(WorkflowFormGetListDto WorkflowFormGetListDto);

        /// <summary>
        /// 2.1、流程表单模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowFormPageDto> WorkflowFormGetListPageAsync(WorkflowFormGetListPageDto WorkflowFormGetListPageDto);
        /// <summary>
        /// 3、流程表单模型查询【根据FormId查询】
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowFormDto> WorkflowFormGetAsync(string FormId);
        /// <summary>
        /// 4、流程表单模型更新
        /// </summary>
        /// <returns></returns>
        public Task<bool> WorkflowFormUpdateAsync(WorkflowFormUpdateDto WorkflowFormUpdateDto, string FormId);
        /// <summary>
        /// 5、流程表单模型删除
        /// </summary>
        /// <returns></returns>
        public Task<bool> WorkflowFormDeleteAsync(List<string> FormIds);
    }
}
