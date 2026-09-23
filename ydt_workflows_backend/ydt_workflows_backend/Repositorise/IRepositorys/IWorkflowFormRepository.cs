using JadeFramework.Dapper;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 流程表单模型仓储接口
    /// </summary>
    public interface IWorkflowFormRepository : IDapperRepository<WorkflowForm>
    {
         /// <summary>
        /// 流程表单模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowFormPage> WorkflowFormGetListPageAsync(WorkflowFormGetListPage WorkflowFormGetListPage);
    }
}
