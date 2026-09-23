using JadeFramework.Dapper;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 催办记录模型仓储接口
    /// </summary>
    public interface IWorkflowUrgeRepository : IDapperRepository<WorkflowUrge>
    {
         /// <summary>
        /// 催办记录模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowUrgePage> WorkflowUrgeGetListPageAsync(WorkflowUrgeGetListPage WorkflowUrgeGetListPage);
    }
}
