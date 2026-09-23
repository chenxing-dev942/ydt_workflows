using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using System.Data;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 角色资源关联模型仓储实现
    /// </summary>
    public class RoleResourceRepository : DapperRepository<RoleResource>, IRoleResourceRepository
    {
        /// <summary>
        /// 1、IDbConnection : Dapper实现数据库操作，连接对象
        /// 2、SqlGeneratorConfig ：配置数据库类型。MySQL SqlServer Oracle
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="config"></param>
        public RoleResourceRepository(IDbConnection connection, 
            SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<RoleResourcePage> RoleResourceGetListPageAsync(RoleResourceGetListPage RoleResourceGetListPage)
        {
            //1、创建分页结果
            RoleResourcePage page = new RoleResourcePage()
            {
                PageIndex = RoleResourceGetListPage.PageIndex,
                PageSize = RoleResourceGetListPage.PageSize
            };

            // 2、设置角色资源关联模型名查询参数
            string sqlWhere = " WHERE 1=1 ";
            
            //if (!RoleResourceGetListPage.RoleResourceName.IsNullOrEmpty())
            //{
            //    sqlWhere += $@" AND RoleResourceName LIKE '%{RoleResourceGetListPage.RoleResourceName.TrimBlank()}%' ";
            //}
            
            // 4、实现分页查询
            string sql = $"SELECT * FROM ydt_role_resource {sqlWhere} LIMIT {RoleResourceGetListPage.OffSet()},{RoleResourceGetListPage.PageSize}";
            page.RoleResources = await this.QueryAsync(sql);

            // 5、查询所有条数
            page.TotalItems = await this.ExecuteScalarAsync<int>($"SELECT COUNT(1) FROM ydt_role_resource {sqlWhere} ");
            
            return page;
        }
    }
}
