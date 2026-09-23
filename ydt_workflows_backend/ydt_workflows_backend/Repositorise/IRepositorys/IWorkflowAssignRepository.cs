using JadeFramework.Dapper;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 流程委托模型仓储接口
    /// </summary>
    public interface IWorkflowAssignRepository : IDapperRepository<WorkflowAssign>
    {
         /// <summary>
        /// 流程委托模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowAssignPage> WorkflowAssignGetListPageAsync(WorkflowAssignGetListPage WorkflowAssignGetListPage);
    }
}
