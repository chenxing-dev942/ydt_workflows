using JadeFramework.Dapper;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 资源【菜单】模型仓储接口
    /// </summary>
    public interface IResourceRepository : IDapperRepository<Resource>
    {
         /// <summary>
        /// 资源【菜单】模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<ResourcePage> ResourceGetListPageAsync(ResourceGetListPage ResourceGetListPage);
    }
}
