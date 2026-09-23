using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using System.Data;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 催办记录模型仓储实现
    /// </summary>
    public class WorkflowUrgeRepository : DapperRepository<WorkflowUrge>, IWorkflowUrgeRepository
    {
        /// <summary>
        /// 1、IDbConnection : Dapper实现数据库操作，连接对象
        /// 2、SqlGeneratorConfig ：配置数据库类型。MySQL SqlServer Oracle
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="config"></param>
        public WorkflowUrgeRepository(IDbConnection connection, 
            SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<WorkflowUrgePage> WorkflowUrgeGetListPageAsync(WorkflowUrgeGetListPage WorkflowUrgeGetListPage)
        {
            //1、创建分页结果
            WorkflowUrgePage page = new WorkflowUrgePage()
            {
                PageIndex = WorkflowUrgeGetListPage.PageIndex,
                PageSize = WorkflowUrgeGetListPage.PageSize
            };

            // 2、设置催办记录模型名查询参数
            string sqlWhere = " WHERE 1=1 ";
            
            //if (!WorkflowUrgeGetListPage.WorkflowUrgeName.IsNullOrEmpty())
            //{
            //    sqlWhere += $@" AND WorkflowUrgeName LIKE '%{WorkflowUrgeGetListPage.WorkflowUrgeName.TrimBlank()}%' ";
            //}
            
            // 4、实现分页查询
            string sql = $"SELECT * FROM ydt_workflow_urge {sqlWhere} LIMIT {WorkflowUrgeGetListPage.OffSet()},{WorkflowUrgeGetListPage.PageSize}";
            page.WorkflowUrges = await this.QueryAsync(sql);

            // 5、查询所有条数
            page.TotalItems = await this.ExecuteScalarAsync<int>($"SELECT COUNT(1) FROM ydt_workflow_urge {sqlWhere} ");
            
            return page;
        }
    }
}
