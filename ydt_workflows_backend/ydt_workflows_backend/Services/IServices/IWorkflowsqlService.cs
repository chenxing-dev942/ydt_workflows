using ydt_workflows_backend.Dtos;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 工作流获取权限系统数据模型Service接口
    /// </summary>
    public interface IWorkflowsqlService
    {
        /// <summary>
        /// 1、工作流获取权限系统数据模型创建
        /// </summary>
        /// <param name="WorkflowsqlCreateDto"></param>
        /// <returns></returns>
        public Task<bool> WorkflowsqlCreateAsync(WorkflowsqlCreateDto WorkflowsqlCreateDto);

        /// <summary>
        /// 2、工作流获取权限系统数据模型集合查询
        /// </summary>
        /// <returns></returns>
        public Task<List<WorkflowsqlDto>> WorkflowsqlGetListAsync(WorkflowsqlGetListDto WorkflowsqlGetListDto);

        /// <summary>
        /// 2.1、工作流获取权限系统数据模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowsqlPageDto> WorkflowsqlGetListPageAsync(WorkflowsqlGetListPageDto WorkflowsqlGetListPageDto);
        /// <summary>
        /// 3、工作流获取权限系统数据模型查询【根据Name查询】
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowsqlDto> WorkflowsqlGetAsync(string Name);
        /// <summary>
        /// 4、工作流获取权限系统数据模型更新
        /// </summary>
        /// <returns></returns>
        public Task<bool> WorkflowsqlUpdateAsync(WorkflowsqlUpdateDto WorkflowsqlUpdateDto, string Name);
        /// <summary>
        /// 5、工作流获取权限系统数据模型删除
        /// </summary>
        /// <returns></returns>
        public Task<bool> WorkflowsqlDeleteAsync(List<string> Names);
    }
}
