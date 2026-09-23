using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using System.Data;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 用户角色关联模型仓储实现
    /// </summary>
    public class UserRoleRepository : DapperRepository<UserRole>, IUserRoleRepository
    {
        /// <summary>
        /// 1、IDbConnection : Dapper实现数据库操作，连接对象
        /// 2、SqlGeneratorConfig ：配置数据库类型。MySQL SqlServer Oracle
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="config"></param>
        public UserRoleRepository(IDbConnection connection, 
            SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<UserRolePage> UserRoleGetListPageAsync(UserRoleGetListPage UserRoleGetListPage)
        {
            //1、创建分页结果
            UserRolePage page = new UserRolePage()
            {
                PageIndex = UserRoleGetListPage.PageIndex,
                PageSize = UserRoleGetListPage.PageSize
            };

            // 2、设置用户角色关联模型名查询参数
            string sqlWhere = " WHERE 1=1 ";
            
            //if (!UserRoleGetListPage.UserRoleName.IsNullOrEmpty())
            //{
            //    sqlWhere += $@" AND UserRoleName LIKE '%{UserRoleGetListPage.UserRoleName.TrimBlank()}%' ";
            //}
            
            // 4、实现分页查询
            string sql = $"SELECT * FROM ydt_user_role {sqlWhere} LIMIT {UserRoleGetListPage.OffSet()},{UserRoleGetListPage.PageSize}";
            page.UserRoles = await this.QueryAsync(sql);

            // 5、查询所有条数
            page.TotalItems = await this.ExecuteScalarAsync<int>($"SELECT COUNT(1) FROM ydt_user_role {sqlWhere} ");
            
            return page;
        }
    }
}
