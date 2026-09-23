using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using System.Data;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 流程通知节点模型仓储实现
    /// </summary>
    public class WorkflowNoticeRepository : DapperRepository<WorkflowNotice>, IWorkflowNoticeRepository
    {
        /// <summary>
        /// 1、IDbConnection : Dapper实现数据库操作，连接对象
        /// 2、SqlGeneratorConfig ：配置数据库类型。MySQL SqlServer Oracle
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="config"></param>
        public WorkflowNoticeRepository(IDbConnection connection, 
            SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<WorkflowNoticePage> WorkflowNoticeGetListPageAsync(WorkflowNoticeGetListPage WorkflowNoticeGetListPage)
        {
            //1、创建分页结果
            WorkflowNoticePage page = new WorkflowNoticePage()
            {
                PageIndex = WorkflowNoticeGetListPage.PageIndex,
                PageSize = WorkflowNoticeGetListPage.PageSize
            };

            // 2、设置流程通知节点模型名查询参数
            string sqlWhere = " WHERE 1=1 ";
            
            //if (!WorkflowNoticeGetListPage.WorkflowNoticeName.IsNullOrEmpty())
            //{
            //    sqlWhere += $@" AND WorkflowNoticeName LIKE '%{WorkflowNoticeGetListPage.WorkflowNoticeName.TrimBlank()}%' ";
            //}
            
            // 4、实现分页查询
            string sql = $"SELECT * FROM ydt_workflow_notice {sqlWhere} LIMIT {WorkflowNoticeGetListPage.OffSet()},{WorkflowNoticeGetListPage.PageSize}";
            page.WorkflowNotices = await this.QueryAsync(sql);

            // 5、查询所有条数
            page.TotalItems = await this.ExecuteScalarAsync<int>($"SELECT COUNT(1) FROM ydt_workflow_notice {sqlWhere} ");
            
            return page;
        }
    }
}
