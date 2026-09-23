using JadeFramework.Dapper;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 子系统模型仓储接口
    /// </summary>
    public interface ISystemsRepository : IDapperRepository<Systems>
    {
         /// <summary>
        /// 子系统模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<SystemsPage> SystemsGetListPageAsync(SystemsGetListPage SystemsGetListPage);
    }
}
