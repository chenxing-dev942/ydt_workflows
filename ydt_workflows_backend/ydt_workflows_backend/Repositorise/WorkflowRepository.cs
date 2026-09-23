using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using System.Data;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 工作流模型仓储实现
    /// </summary>
    public class WorkflowRepository : DapperRepository<Workflow>, IWorkflowRepository
    {
        /// <summary>
        /// 1、IDbConnection : Dapper实现数据库操作，连接对象
        /// 2、SqlGeneratorConfig ：配置数据库类型。MySQL SqlServer Oracle
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="config"></param>
        public WorkflowRepository(IDbConnection connection, 
            SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<WorkflowPage> WorkflowGetListPageAsync(WorkflowGetListPage WorkflowGetListPage)
        {
            //1、创建分页结果
            WorkflowPage page = new WorkflowPage()
            {
                PageIndex = WorkflowGetListPage.PageIndex,
                PageSize = WorkflowGetListPage.PageSize
            };

            // 2、设置工作流模型名查询参数
            string sqlWhere = " WHERE 1=1 ";
            
            //if (!WorkflowGetListPage.WorkflowName.IsNullOrEmpty())
            //{
            //    sqlWhere += $@" AND WorkflowName LIKE '%{WorkflowGetListPage.WorkflowName.TrimBlank()}%' ";
            //}
            
            // 4、实现分页查询
            string sql = $"SELECT * FROM ydt_workflow {sqlWhere} LIMIT {WorkflowGetListPage.OffSet()},{WorkflowGetListPage.PageSize}";
            page.Workflows = await this.QueryAsync(sql);

            // 5、查询所有条数
            page.TotalItems = await this.ExecuteScalarAsync<int>($"SELECT COUNT(1) FROM ydt_workflow {sqlWhere} ");
            
            return page;
        }
    }
}
