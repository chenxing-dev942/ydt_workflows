using ydt_workflows_backend.Dtos;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 流程分类模型Service接口
    /// </summary>
    public interface IWorkflowCategoryService
    {
        /// <summary>
        /// 1、流程分类模型创建
        /// </summary>
        /// <param name="WorkflowCategoryCreateDto"></param>
        /// <returns></returns>
        public Task<bool> WorkflowCategoryCreateAsync(WorkflowCategoryCreateDto WorkflowCategoryCreateDto);

        /// <summary>
        /// 2、流程分类模型集合查询
        /// </summary>
        /// <returns></returns>
        public Task<List<WorkflowCategoryDto>> WorkflowCategoryGetListAsync(WorkflowCategoryGetListDto WorkflowCategoryGetListDto);

        /// <summary>
        /// 2.1、流程分类模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowCategoryPageDto> WorkflowCategoryGetListPageAsync(WorkflowCategoryGetListPageDto WorkflowCategoryGetListPageDto);
        /// <summary>
        /// 3、流程分类模型查询【根据CategoryId查询】
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowCategoryDto> WorkflowCategoryGetAsync(string CategoryId);
        /// <summary>
        /// 4、流程分类模型更新
        /// </summary>
        /// <returns></returns>
        public Task<bool> WorkflowCategoryUpdateAsync(WorkflowCategoryUpdateDto WorkflowCategoryUpdateDto, string CategoryId);
        /// <summary>
        /// 5、流程分类模型删除
        /// </summary>
        /// <returns></returns>
        public Task<bool> WorkflowCategoryDeleteAsync(List<string> CategoryIds);
    }
}
