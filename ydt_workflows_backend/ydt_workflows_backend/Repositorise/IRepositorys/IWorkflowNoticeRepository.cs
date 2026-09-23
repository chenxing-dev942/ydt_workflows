using JadeFramework.Dapper;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 流程通知节点模型仓储接口
    /// </summary>
    public interface IWorkflowNoticeRepository : IDapperRepository<WorkflowNotice>
    {
         /// <summary>
        /// 流程通知节点模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowNoticePage> WorkflowNoticeGetListPageAsync(WorkflowNoticeGetListPage WorkflowNoticeGetListPage);
    }
}
