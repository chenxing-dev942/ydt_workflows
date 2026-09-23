using JadeFramework.Dapper;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 用户部门关联模型仓储接口
    /// </summary>
    public interface IUserDeptRepository : IDapperRepository<UserDept>
    {
         /// <summary>
        /// 用户部门关联模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<UserDeptPage> UserDeptGetListPageAsync(UserDeptGetListPage UserDeptGetListPage);
    }
}
