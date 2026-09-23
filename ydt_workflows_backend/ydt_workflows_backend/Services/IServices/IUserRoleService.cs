using ydt_workflows_backend.Dtos;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 用户角色关联模型Service接口
    /// </summary>
    public interface IUserRoleService
    {
        /// <summary>
        /// 1、用户角色关联模型创建
        /// </summary>
        /// <param name="UserRoleCreateDto"></param>
        /// <returns></returns>
        public Task<bool> UserRoleCreateAsync(UserRoleCreateDto UserRoleCreateDto);

        /// <summary>
        /// 2、用户角色关联模型集合查询
        /// </summary>
        /// <returns></returns>
        public Task<List<UserRoleDto>> UserRoleGetListAsync(UserRoleGetListDto UserRoleGetListDto);

        /// <summary>
        /// 2.1、用户角色关联模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<UserRolePageDto> UserRoleGetListPageAsync(UserRoleGetListPageDto UserRoleGetListPageDto);
        /// <summary>
        /// 3、用户角色关联模型查询【根据Id查询】
        /// </summary>
        /// <returns></returns>
        public Task<UserRoleDto> UserRoleGetAsync(long Id);
        /// <summary>
        /// 4、用户角色关联模型更新
        /// </summary>
        /// <returns></returns>
        public Task<bool> UserRoleUpdateAsync(UserRoleUpdateDto UserRoleUpdateDto, long Id);
        /// <summary>
        /// 5、用户角色关联模型删除
        /// </summary>
        /// <returns></returns>
        public Task<bool> UserRoleDeleteAsync(List<long> Ids);
    }
}
