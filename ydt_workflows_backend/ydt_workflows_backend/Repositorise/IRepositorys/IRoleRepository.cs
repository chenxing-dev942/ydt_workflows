using JadeFramework.Dapper;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 角色模型仓储接口
    /// </summary>
    public interface IRoleRepository : IDapperRepository<Role>
    {
         /// <summary>
        /// 角色模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<RolePage> RoleGetListPageAsync(RoleGetListPage RoleGetListPage);
    }
}
