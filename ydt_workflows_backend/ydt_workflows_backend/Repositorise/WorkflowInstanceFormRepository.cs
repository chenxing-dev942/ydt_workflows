using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using System.Data;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 流程实例表单关联模型仓储实现
    /// </summary>
    public class WorkflowInstanceFormRepository : DapperRepository<WorkflowInstanceForm>, IWorkflowInstanceFormRepository
    {
        /// <summary>
        /// 1、IDbConnection : Dapper实现数据库操作，连接对象
        /// 2、SqlGeneratorConfig ：配置数据库类型。MySQL SqlServer Oracle
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="config"></param>
        public WorkflowInstanceFormRepository(IDbConnection connection, 
            SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<WorkflowInstanceFormPage> WorkflowInstanceFormGetListPageAsync(WorkflowInstanceFormGetListPage WorkflowInstanceFormGetListPage)
        {
            //1、创建分页结果
            WorkflowInstanceFormPage page = new WorkflowInstanceFormPage()
            {
                PageIndex = WorkflowInstanceFormGetListPage.PageIndex,
                PageSize = WorkflowInstanceFormGetListPage.PageSize
            };

            // 2、设置流程实例表单关联模型名查询参数
            string sqlWhere = " WHERE 1=1 ";
            
            //if (!WorkflowInstanceFormGetListPage.WorkflowInstanceFormName.IsNullOrEmpty())
            //{
            //    sqlWhere += $@" AND WorkflowInstanceFormName LIKE '%{WorkflowInstanceFormGetListPage.WorkflowInstanceFormName.TrimBlank()}%' ";
            //}
            
            // 4、实现分页查询
            string sql = $"SELECT * FROM ydt_workflow_instance_form {sqlWhere} LIMIT {WorkflowInstanceFormGetListPage.OffSet()},{WorkflowInstanceFormGetListPage.PageSize}";
            page.WorkflowInstanceForms = await this.QueryAsync(sql);

            // 5、查询所有条数
            page.TotalItems = await this.ExecuteScalarAsync<int>($"SELECT COUNT(1) FROM ydt_workflow_instance_form {sqlWhere} ");
            
            return page;
        }
    }
}
