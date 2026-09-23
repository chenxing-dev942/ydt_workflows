using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using System.Data;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 流程实例模型【根据流程运行流程】仓储实现
    /// </summary>
    public class WorkflowInstanceRepository : DapperRepository<WorkflowInstance>, IWorkflowInstanceRepository
    {
        /// <summary>
        /// 1、IDbConnection : Dapper实现数据库操作，连接对象
        /// 2、SqlGeneratorConfig ：配置数据库类型。MySQL SqlServer Oracle
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="config"></param>
        public WorkflowInstanceRepository(IDbConnection connection, 
            SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<WorkflowInstancePage> WorkflowInstanceGetListPageAsync(WorkflowInstanceGetListPage WorkflowInstanceGetListPage)
        {
            //1、创建分页结果
            WorkflowInstancePage page = new WorkflowInstancePage()
            {
                PageIndex = WorkflowInstanceGetListPage.PageIndex,
                PageSize = WorkflowInstanceGetListPage.PageSize
            };

            // 2、设置流程实例模型【根据流程运行流程】名查询参数
            string sqlWhere = " WHERE 1=1 ";
            
            //if (!WorkflowInstanceGetListPage.WorkflowInstanceName.IsNullOrEmpty())
            //{
            //    sqlWhere += $@" AND WorkflowInstanceName LIKE '%{WorkflowInstanceGetListPage.WorkflowInstanceName.TrimBlank()}%' ";
            //}
            
            // 4、实现分页查询
            string sql = $"SELECT * FROM ydt_workflow_instance {sqlWhere} LIMIT {WorkflowInstanceGetListPage.OffSet()},{WorkflowInstanceGetListPage.PageSize}";
            page.WorkflowInstances = await this.QueryAsync(sql);

            // 5、查询所有条数
            page.TotalItems = await this.ExecuteScalarAsync<int>($"SELECT COUNT(1) FROM ydt_workflow_instance {sqlWhere} ");
            
            return page;
        }
    }
}
