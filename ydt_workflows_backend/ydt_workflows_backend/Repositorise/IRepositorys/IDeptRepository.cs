using JadeFramework.Dapper;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 部门模型仓储接口
    /// </summary>
    public interface IDeptRepository : IDapperRepository<Dept>
    {
         /// <summary>
        /// 部门模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<DeptPage> DeptGetListPageAsync(DeptGetListPage DeptGetListPage);
    }
}
