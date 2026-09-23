using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using System.Data;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 用户部门关联模型仓储实现
    /// </summary>
    public class UserDeptRepository : DapperRepository<UserDept>, IUserDeptRepository
    {
        /// <summary>
        /// 1、IDbConnection : Dapper实现数据库操作，连接对象
        /// 2、SqlGeneratorConfig ：配置数据库类型。MySQL SqlServer Oracle
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="config"></param>
        public UserDeptRepository(IDbConnection connection, 
            SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<UserDeptPage> UserDeptGetListPageAsync(UserDeptGetListPage UserDeptGetListPage)
        {
            //1、创建分页结果
            UserDeptPage page = new UserDeptPage()
            {
                PageIndex = UserDeptGetListPage.PageIndex,
                PageSize = UserDeptGetListPage.PageSize
            };

            // 2、设置用户部门关联模型名查询参数
            string sqlWhere = " WHERE 1=1 ";
            
            //if (!UserDeptGetListPage.UserDeptName.IsNullOrEmpty())
            //{
            //    sqlWhere += $@" AND UserDeptName LIKE '%{UserDeptGetListPage.UserDeptName.TrimBlank()}%' ";
            //}
            
            // 4、实现分页查询
            string sql = $"SELECT * FROM ydt_user_dept {sqlWhere} LIMIT {UserDeptGetListPage.OffSet()},{UserDeptGetListPage.PageSize}";
            page.UserDepts = await this.QueryAsync(sql);

            // 5、查询所有条数
            page.TotalItems = await this.ExecuteScalarAsync<int>($"SELECT COUNT(1) FROM ydt_user_dept {sqlWhere} ");
            
            return page;
        }
    }
}
