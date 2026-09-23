using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using System.Data;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 流程表单模型仓储实现
    /// </summary>
    public class WorkflowFormRepository : DapperRepository<WorkflowForm>, IWorkflowFormRepository
    {
        /// <summary>
        /// 1、IDbConnection : Dapper实现数据库操作，连接对象
        /// 2、SqlGeneratorConfig ：配置数据库类型。MySQL SqlServer Oracle
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="config"></param>
        public WorkflowFormRepository(IDbConnection connection, 
            SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<WorkflowFormPage> WorkflowFormGetListPageAsync(WorkflowFormGetListPage WorkflowFormGetListPage)
        {
            //1、创建分页结果
            WorkflowFormPage page = new WorkflowFormPage()
            {
                PageIndex = WorkflowFormGetListPage.PageIndex,
                PageSize = WorkflowFormGetListPage.PageSize
            };

            // 2、设置流程表单模型名查询参数
            string sqlWhere = " WHERE 1=1 ";
            
            //if (!WorkflowFormGetListPage.WorkflowFormName.IsNullOrEmpty())
            //{
            //    sqlWhere += $@" AND WorkflowFormName LIKE '%{WorkflowFormGetListPage.WorkflowFormName.TrimBlank()}%' ";
            //}
            
            // 4、实现分页查询
            string sql = $"SELECT * FROM ydt_workflow_form {sqlWhere} LIMIT {WorkflowFormGetListPage.OffSet()},{WorkflowFormGetListPage.PageSize}";
            page.WorkflowForms = await this.QueryAsync(sql);

            // 5、查询所有条数
            page.TotalItems = await this.ExecuteScalarAsync<int>($"SELECT COUNT(1) FROM ydt_workflow_form {sqlWhere} ");
            
            return page;
        }
    }
}
