using ydt_workflows_backend.Dtos;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 角色模型Service接口
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// 1、角色模型创建
        /// </summary>
        /// <param name="RoleCreateDto"></param>
        /// <returns></returns>
        public Task<bool> RoleCreateAsync(RoleCreateDto RoleCreateDto);

        /// <summary>
        /// 2、角色模型集合查询
        /// </summary>
        /// <returns></returns>
        public Task<List<RoleDto>> RoleGetListAsync(RoleGetListDto RoleGetListDto);

        /// <summary>
        /// 2.1、角色模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<RolePageDto> RoleGetListPageAsync(RoleGetListPageDto RoleGetListPageDto);
        /// <summary>
        /// 3、角色模型查询【根据RoleId查询】
        /// </summary>
        /// <returns></returns>
        public Task<RoleDto> RoleGetAsync(long RoleId);
        /// <summary>
        /// 4、角色模型更新
        /// </summary>
        /// <returns></returns>
        public Task<bool> RoleUpdateAsync(RoleUpdateDto RoleUpdateDto, long RoleId);
        /// <summary>
        /// 5、角色模型删除
        /// </summary>
        /// <returns></returns>
        public Task<bool> RoleDeleteAsync(List<long> RoleIds);
    }
}
