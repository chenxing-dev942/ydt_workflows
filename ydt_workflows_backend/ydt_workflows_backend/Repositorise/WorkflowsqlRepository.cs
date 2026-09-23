using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using System.Data;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 工作流获取权限系统数据模型仓储实现
    /// </summary>
    public class WorkflowsqlRepository : DapperRepository<Workflowsql>, IWorkflowsqlRepository
    {
        /// <summary>
        /// 1、IDbConnection : Dapper实现数据库操作，连接对象
        /// 2、SqlGeneratorConfig ：配置数据库类型。MySQL SqlServer Oracle
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="config"></param>
        public WorkflowsqlRepository(IDbConnection connection, 
            SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<WorkflowsqlPage> WorkflowsqlGetListPageAsync(WorkflowsqlGetListPage WorkflowsqlGetListPage)
        {
            //1、创建分页结果
            WorkflowsqlPage page = new WorkflowsqlPage()
            {
                PageIndex = WorkflowsqlGetListPage.PageIndex,
                PageSize = WorkflowsqlGetListPage.PageSize
            };

            // 2、设置工作流获取权限系统数据模型名查询参数
            string sqlWhere = " WHERE 1=1 ";
            
            //if (!WorkflowsqlGetListPage.WorkflowsqlName.IsNullOrEmpty())
            //{
            //    sqlWhere += $@" AND WorkflowsqlName LIKE '%{WorkflowsqlGetListPage.WorkflowsqlName.TrimBlank()}%' ";
            //}
            
            // 4、实现分页查询
            string sql = $"SELECT * FROM ydt_workflowsql {sqlWhere} LIMIT {WorkflowsqlGetListPage.OffSet()},{WorkflowsqlGetListPage.PageSize}";
            page.Workflowsqls = await this.QueryAsync(sql);

            // 5、查询所有条数
            page.TotalItems = await this.ExecuteScalarAsync<int>($"SELECT COUNT(1) FROM ydt_workflowsql {sqlWhere} ");
            
            return page;
        }
    }
}
