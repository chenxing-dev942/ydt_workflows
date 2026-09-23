using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using System.Data;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 子系统模型仓储实现
    /// </summary>
    public class SystemsRepository : DapperRepository<Systems>, ISystemsRepository
    {
        /// <summary>
        /// 1、IDbConnection : Dapper实现数据库操作，连接对象
        /// 2、SqlGeneratorConfig ：配置数据库类型。MySQL SqlServer Oracle
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="config"></param>
        public SystemsRepository(IDbConnection connection, 
            SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<SystemsPage> SystemsGetListPageAsync(SystemsGetListPage SystemsGetListPage)
        {
            //1、创建分页结果
            SystemsPage page = new SystemsPage()
            {
                PageIndex = SystemsGetListPage.PageIndex,
                PageSize = SystemsGetListPage.PageSize
            };

            // 2、设置子系统模型名查询参数
            string sqlWhere = " WHERE 1=1 ";
            
            //if (!SystemsGetListPage.SystemsName.IsNullOrEmpty())
            //{
            //    sqlWhere += $@" AND SystemsName LIKE '%{SystemsGetListPage.SystemsName.TrimBlank()}%' ";
            //}
            
            // 4、实现分页查询
            string sql = $"SELECT * FROM ydt_systems {sqlWhere} LIMIT {SystemsGetListPage.OffSet()},{SystemsGetListPage.PageSize}";
            page.Systemss = await this.QueryAsync(sql);

            // 5、查询所有条数
            page.TotalItems = await this.ExecuteScalarAsync<int>($"SELECT COUNT(1) FROM ydt_systems {sqlWhere} ");
            
            return page;
        }
    }
}
