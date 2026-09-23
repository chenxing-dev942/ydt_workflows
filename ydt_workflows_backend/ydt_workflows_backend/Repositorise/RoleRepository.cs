using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using System.Data;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 角色模型仓储实现
    /// </summary>
    public class RoleRepository : DapperRepository<Role>, IRoleRepository
    {
        /// <summary>
        /// 1、IDbConnection : Dapper实现数据库操作，连接对象
        /// 2、SqlGeneratorConfig ：配置数据库类型。MySQL SqlServer Oracle
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="config"></param>
        public RoleRepository(IDbConnection connection, 
            SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<RolePage> RoleGetListPageAsync(RoleGetListPage RoleGetListPage)
        {
            //1、创建分页结果
            RolePage page = new RolePage()
            {
                PageIndex = RoleGetListPage.PageIndex,
                PageSize = RoleGetListPage.PageSize
            };

            // 2、设置角色模型名查询参数
            string sqlWhere = " WHERE 1=1 ";
            
            //if (!RoleGetListPage.RoleName.IsNullOrEmpty())
            //{
            //    sqlWhere += $@" AND RoleName LIKE '%{RoleGetListPage.RoleName.TrimBlank()}%' ";
            //}
            
            // 4、实现分页查询
            string sql = $"SELECT * FROM ydt_role {sqlWhere} LIMIT {RoleGetListPage.OffSet()},{RoleGetListPage.PageSize}";
            page.Roles = await this.QueryAsync(sql);

            // 5、查询所有条数
            page.TotalItems = await this.ExecuteScalarAsync<int>($"SELECT COUNT(1) FROM ydt_role {sqlWhere} ");
            
            return page;
        }
    }
}
