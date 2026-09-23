using ydt_workflows_backend.Dtos;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 角色资源关联模型Service接口
    /// </summary>
    public interface IRoleResourceService
    {
        /// <summary>
        /// 1、角色资源关联模型创建
        /// </summary>
        /// <param name="RoleResourceCreateDto"></param>
        /// <returns></returns>
        public Task<bool> RoleResourceCreateAsync(RoleResourceCreateDto RoleResourceCreateDto);

        /// <summary>
        /// 2、角色资源关联模型集合查询
        /// </summary>
        /// <returns></returns>
        public Task<List<RoleResourceDto>> RoleResourceGetListAsync(RoleResourceGetListDto RoleResourceGetListDto);

        /// <summary>
        /// 2.1、角色资源关联模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<RoleResourcePageDto> RoleResourceGetListPageAsync(RoleResourceGetListPageDto RoleResourceGetListPageDto);
        /// <summary>
        /// 3、角色资源关联模型查询【根据Id查询】
        /// </summary>
        /// <returns></returns>
        public Task<RoleResourceDto> RoleResourceGetAsync(long Id);
        /// <summary>
        /// 4、角色资源关联模型更新
        /// </summary>
        /// <returns></returns>
        public Task<bool> RoleResourceUpdateAsync(RoleResourceUpdateDto RoleResourceUpdateDto, long Id);
        /// <summary>
        /// 5、角色资源关联模型删除
        /// </summary>
        /// <returns></returns>
        public Task<bool> RoleResourceDeleteAsync(List<long> Ids);
    }
}
