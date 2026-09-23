using JadeFramework.Dapper;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 用户角色关联模型仓储接口
    /// </summary>
    public interface IUserRoleRepository : IDapperRepository<UserRole>
    {
         /// <summary>
        /// 用户角色关联模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<UserRolePage> UserRoleGetListPageAsync(UserRoleGetListPage UserRoleGetListPage);
    }
}
