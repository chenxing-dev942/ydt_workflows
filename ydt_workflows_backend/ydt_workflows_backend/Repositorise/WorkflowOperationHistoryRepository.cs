using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using System.Data;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 流程操作历史模型仓储实现
    /// </summary>
    public class WorkflowOperationHistoryRepository : DapperRepository<WorkflowOperationHistory>, IWorkflowOperationHistoryRepository
    {
        /// <summary>
        /// 1、IDbConnection : Dapper实现数据库操作，连接对象
        /// 2、SqlGeneratorConfig ：配置数据库类型。MySQL SqlServer Oracle
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="config"></param>
        public WorkflowOperationHistoryRepository(IDbConnection connection, 
            SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<WorkflowOperationHistoryPage> WorkflowOperationHistoryGetListPageAsync(WorkflowOperationHistoryGetListPage WorkflowOperationHistoryGetListPage)
        {
            //1、创建分页结果
            WorkflowOperationHistoryPage page = new WorkflowOperationHistoryPage()
            {
                PageIndex = WorkflowOperationHistoryGetListPage.PageIndex,
                PageSize = WorkflowOperationHistoryGetListPage.PageSize
            };

            // 2、设置流程操作历史模型名查询参数
            string sqlWhere = " WHERE 1=1 ";
            
            //if (!WorkflowOperationHistoryGetListPage.WorkflowOperationHistoryName.IsNullOrEmpty())
            //{
            //    sqlWhere += $@" AND WorkflowOperationHistoryName LIKE '%{WorkflowOperationHistoryGetListPage.WorkflowOperationHistoryName.TrimBlank()}%' ";
            //}
            
            // 4、实现分页查询
            string sql = $"SELECT * FROM ydt_workflow_operation_history {sqlWhere} LIMIT {WorkflowOperationHistoryGetListPage.OffSet()},{WorkflowOperationHistoryGetListPage.PageSize}";
            page.WorkflowOperationHistorys = await this.QueryAsync(sql);

            // 5、查询所有条数
            page.TotalItems = await this.ExecuteScalarAsync<int>($"SELECT COUNT(1) FROM ydt_workflow_operation_history {sqlWhere} ");
            
            return page;
        }
    }
}
