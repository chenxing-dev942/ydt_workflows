using JadeFramework.Dapper;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 用户模型仓储接口
    /// </summary>
    public interface IUserRepository : IDapperRepository<User>
    {
         /// <summary>
        /// 用户模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<UserPage> UserGetListPageAsync(UserGetListPage UserGetListPage);
    }
}
