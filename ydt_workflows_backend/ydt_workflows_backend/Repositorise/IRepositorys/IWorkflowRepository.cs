using JadeFramework.Dapper;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 工作流模型仓储接口
    /// </summary>
    public interface IWorkflowRepository : IDapperRepository<Workflow>
    {
         /// <summary>
        /// 工作流模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowPage> WorkflowGetListPageAsync(WorkflowGetListPage WorkflowGetListPage);
    }
}
