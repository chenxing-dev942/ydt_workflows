using ydt_workflows_backend.Dtos;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 用户部门关联模型Service接口
    /// </summary>
    public interface IUserDeptService
    {
        /// <summary>
        /// 1、用户部门关联模型创建
        /// </summary>
        /// <param name="UserDeptCreateDto"></param>
        /// <returns></returns>
        public Task<bool> UserDeptCreateAsync(UserDeptCreateDto UserDeptCreateDto);

        /// <summary>
        /// 2、用户部门关联模型集合查询
        /// </summary>
        /// <returns></returns>
        public Task<List<UserDeptDto>> UserDeptGetListAsync(UserDeptGetListDto UserDeptGetListDto);

        /// <summary>
        /// 2.1、用户部门关联模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<UserDeptPageDto> UserDeptGetListPageAsync(UserDeptGetListPageDto UserDeptGetListPageDto);
        /// <summary>
        /// 3、用户部门关联模型查询【根据Id查询】
        /// </summary>
        /// <returns></returns>
        public Task<UserDeptDto> UserDeptGetAsync(long Id);
        /// <summary>
        /// 4、用户部门关联模型更新
        /// </summary>
        /// <returns></returns>
        public Task<bool> UserDeptUpdateAsync(UserDeptUpdateDto UserDeptUpdateDto, long Id);
        /// <summary>
        /// 5、用户部门关联模型删除
        /// </summary>
        /// <returns></returns>
        public Task<bool> UserDeptDeleteAsync(List<long> Ids);
    }
}
