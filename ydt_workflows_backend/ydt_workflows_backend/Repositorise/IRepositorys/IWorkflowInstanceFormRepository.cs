using JadeFramework.Dapper;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 流程实例表单关联模型仓储接口
    /// </summary>
    public interface IWorkflowInstanceFormRepository : IDapperRepository<WorkflowInstanceForm>
    {
         /// <summary>
        /// 流程实例表单关联模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowInstanceFormPage> WorkflowInstanceFormGetListPageAsync(WorkflowInstanceFormGetListPage WorkflowInstanceFormGetListPage);
    }
}
