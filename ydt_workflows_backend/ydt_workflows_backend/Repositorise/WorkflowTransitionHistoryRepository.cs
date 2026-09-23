using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using System.Data;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 流程流转历史模型仓储实现
    /// </summary>
    public class WorkflowTransitionHistoryRepository : DapperRepository<WorkflowTransitionHistory>, IWorkflowTransitionHistoryRepository
    {
        /// <summary>
        /// 1、IDbConnection : Dapper实现数据库操作，连接对象
        /// 2、SqlGeneratorConfig ：配置数据库类型。MySQL SqlServer Oracle
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="config"></param>
        public WorkflowTransitionHistoryRepository(IDbConnection connection, 
            SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<WorkflowTransitionHistoryPage> WorkflowTransitionHistoryGetListPageAsync(WorkflowTransitionHistoryGetListPage WorkflowTransitionHistoryGetListPage)
        {
            //1、创建分页结果
            WorkflowTransitionHistoryPage page = new WorkflowTransitionHistoryPage()
            {
                PageIndex = WorkflowTransitionHistoryGetListPage.PageIndex,
                PageSize = WorkflowTransitionHistoryGetListPage.PageSize
            };

            // 2、设置流程流转历史模型名查询参数
            string sqlWhere = " WHERE 1=1 ";
            
            //if (!WorkflowTransitionHistoryGetListPage.WorkflowTransitionHistoryName.IsNullOrEmpty())
            //{
            //    sqlWhere += $@" AND WorkflowTransitionHistoryName LIKE '%{WorkflowTransitionHistoryGetListPage.WorkflowTransitionHistoryName.TrimBlank()}%' ";
            //}
            
            // 4、实现分页查询
            string sql = $"SELECT * FROM ydt_workflow_transition_history {sqlWhere} LIMIT {WorkflowTransitionHistoryGetListPage.OffSet()},{WorkflowTransitionHistoryGetListPage.PageSize}";
            page.WorkflowTransitionHistorys = await this.QueryAsync(sql);

            // 5、查询所有条数
            page.TotalItems = await this.ExecuteScalarAsync<int>($"SELECT COUNT(1) FROM ydt_workflow_transition_history {sqlWhere} ");
            
            return page;
        }
    }
}
