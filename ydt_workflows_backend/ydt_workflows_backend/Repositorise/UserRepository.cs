using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using System.Data;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 用户模型仓储实现
    /// </summary>
    public class UserRepository : DapperRepository<User>, IUserRepository
    {
        /// <summary>
        /// 1、IDbConnection : Dapper实现数据库操作，连接对象
        /// 2、SqlGeneratorConfig ：配置数据库类型。MySQL SqlServer Oracle
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="config"></param>
        public UserRepository(IDbConnection connection, 
            SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<UserPage> UserGetListPageAsync(UserGetListPage UserGetListPage)
        {
            //1、创建分页结果
            UserPage page = new UserPage()
            {
                PageIndex = UserGetListPage.PageIndex,
                PageSize = UserGetListPage.PageSize
            };

            // 2、设置用户模型名查询参数
            string sqlWhere = " WHERE 1=1 ";
            
            //if (!UserGetListPage.UserName.IsNullOrEmpty())
            //{
            //    sqlWhere += $@" AND UserName LIKE '%{UserGetListPage.UserName.TrimBlank()}%' ";
            //}
            
            // 4、实现分页查询
            string sql = $"SELECT * FROM ydt_user {sqlWhere} LIMIT {UserGetListPage.OffSet()},{UserGetListPage.PageSize}";
            page.Users = await this.QueryAsync(sql);

            // 5、查询所有条数
            page.TotalItems = await this.ExecuteScalarAsync<int>($"SELECT COUNT(1) FROM ydt_user {sqlWhere} ");
            
            return page;
        }
    }
}
