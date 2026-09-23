using JadeFramework.Dapper;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 角色资源关联模型仓储接口
    /// </summary>
    public interface IRoleResourceRepository : IDapperRepository<RoleResource>
    {
         /// <summary>
        /// 角色资源关联模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<RoleResourcePage> RoleResourceGetListPageAsync(RoleResourceGetListPage RoleResourceGetListPage);
    }
}
