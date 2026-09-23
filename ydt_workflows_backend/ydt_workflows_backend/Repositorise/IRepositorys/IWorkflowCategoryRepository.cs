using JadeFramework.Dapper;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 流程分类模型仓储接口
    /// </summary>
    public interface IWorkflowCategoryRepository : IDapperRepository<WorkflowCategory>
    {
         /// <summary>
        /// 流程分类模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowCategoryPage> WorkflowCategoryGetListPageAsync(WorkflowCategoryGetListPage WorkflowCategoryGetListPage);
    }
}
