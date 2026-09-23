using JadeFramework.Dapper;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 工作流获取权限系统数据模型仓储接口
    /// </summary>
    public interface IWorkflowsqlRepository : IDapperRepository<Workflowsql>
    {
         /// <summary>
        /// 工作流获取权限系统数据模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowsqlPage> WorkflowsqlGetListPageAsync(WorkflowsqlGetListPage WorkflowsqlGetListPage);
    }
}
