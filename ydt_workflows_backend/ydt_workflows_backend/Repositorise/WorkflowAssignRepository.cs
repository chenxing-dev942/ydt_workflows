using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using System.Data;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 流程委托模型仓储实现
    /// </summary>
    public class WorkflowAssignRepository : DapperRepository<WorkflowAssign>, IWorkflowAssignRepository
    {
        /// <summary>
        /// 1、IDbConnection : Dapper实现数据库操作，连接对象
        /// 2、SqlGeneratorConfig ：配置数据库类型。MySQL SqlServer Oracle
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="config"></param>
        public WorkflowAssignRepository(IDbConnection connection, 
            SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<WorkflowAssignPage> WorkflowAssignGetListPageAsync(WorkflowAssignGetListPage WorkflowAssignGetListPage)
        {
            //1、创建分页结果
            WorkflowAssignPage page = new WorkflowAssignPage()
            {
                PageIndex = WorkflowAssignGetListPage.PageIndex,
                PageSize = WorkflowAssignGetListPage.PageSize
            };

            // 2、设置流程委托模型名查询参数
            string sqlWhere = " WHERE 1=1 ";
            
            //if (!WorkflowAssignGetListPage.WorkflowAssignName.IsNullOrEmpty())
            //{
            //    sqlWhere += $@" AND WorkflowAssignName LIKE '%{WorkflowAssignGetListPage.WorkflowAssignName.TrimBlank()}%' ";
            //}
            
            // 4、实现分页查询
            string sql = $"SELECT * FROM ydt_workflow_assign {sqlWhere} LIMIT {WorkflowAssignGetListPage.OffSet()},{WorkflowAssignGetListPage.PageSize}";
            page.WorkflowAssigns = await this.QueryAsync(sql);

            // 5、查询所有条数
            page.TotalItems = await this.ExecuteScalarAsync<int>($"SELECT COUNT(1) FROM ydt_workflow_assign {sqlWhere} ");
            
            return page;
        }
    }
}
