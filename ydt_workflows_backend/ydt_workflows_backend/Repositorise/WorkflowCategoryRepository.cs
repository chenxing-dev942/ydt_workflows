using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using System.Data;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 流程分类模型仓储实现
    /// </summary>
    public class WorkflowCategoryRepository : DapperRepository<WorkflowCategory>, IWorkflowCategoryRepository
    {
        /// <summary>
        /// 1、IDbConnection : Dapper实现数据库操作，连接对象
        /// 2、SqlGeneratorConfig ：配置数据库类型。MySQL SqlServer Oracle
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="config"></param>
        public WorkflowCategoryRepository(IDbConnection connection, 
            SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<WorkflowCategoryPage> WorkflowCategoryGetListPageAsync(WorkflowCategoryGetListPage WorkflowCategoryGetListPage)
        {
            //1、创建分页结果
            WorkflowCategoryPage page = new WorkflowCategoryPage()
            {
                PageIndex = WorkflowCategoryGetListPage.PageIndex,
                PageSize = WorkflowCategoryGetListPage.PageSize
            };

            // 2、设置流程分类模型名查询参数
            string sqlWhere = " WHERE 1=1 ";
            
            //if (!WorkflowCategoryGetListPage.WorkflowCategoryName.IsNullOrEmpty())
            //{
            //    sqlWhere += $@" AND WorkflowCategoryName LIKE '%{WorkflowCategoryGetListPage.WorkflowCategoryName.TrimBlank()}%' ";
            //}
            
            // 4、实现分页查询
            string sql = $"SELECT * FROM ydt_workflow_category {sqlWhere} LIMIT {WorkflowCategoryGetListPage.OffSet()},{WorkflowCategoryGetListPage.PageSize}";
            page.WorkflowCategorys = await this.QueryAsync(sql);

            // 5、查询所有条数
            page.TotalItems = await this.ExecuteScalarAsync<int>($"SELECT COUNT(1) FROM ydt_workflow_category {sqlWhere} ");
            
            return page;
        }
    }
}
