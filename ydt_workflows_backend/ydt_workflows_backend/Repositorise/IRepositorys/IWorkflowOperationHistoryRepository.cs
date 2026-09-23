using JadeFramework.Dapper;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 流程操作历史模型仓储接口
    /// </summary>
    public interface IWorkflowOperationHistoryRepository : IDapperRepository<WorkflowOperationHistory>
    {
         /// <summary>
        /// 流程操作历史模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowOperationHistoryPage> WorkflowOperationHistoryGetListPageAsync(WorkflowOperationHistoryGetListPage WorkflowOperationHistoryGetListPage);
    }
}
