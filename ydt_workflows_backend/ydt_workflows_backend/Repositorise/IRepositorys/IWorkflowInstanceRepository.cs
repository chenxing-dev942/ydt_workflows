using JadeFramework.Dapper;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 流程实例模型【根据流程运行流程】仓储接口
    /// </summary>
    public interface IWorkflowInstanceRepository : IDapperRepository<WorkflowInstance>
    {
         /// <summary>
        /// 流程实例模型【根据流程运行流程】集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowInstancePage> WorkflowInstanceGetListPageAsync(WorkflowInstanceGetListPage WorkflowInstanceGetListPage);
    }
}
