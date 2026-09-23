using ydt_workflows_backend.Dtos;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 资源【菜单】模型Service接口
    /// </summary>
    public interface IResourceService
    {
        /// <summary>
        /// 1、资源【菜单】模型创建
        /// </summary>
        /// <param name="ResourceCreateDto"></param>
        /// <returns></returns>
        public Task<bool> ResourceCreateAsync(ResourceCreateDto ResourceCreateDto);

        /// <summary>
        /// 2、资源【菜单】模型集合查询
        /// </summary>
        /// <returns></returns>
        public Task<List<ResourceDto>> ResourceGetListAsync(ResourceGetListDto ResourceGetListDto);

        /// <summary>
        /// 2.1、资源【菜单】模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<ResourcePageDto> ResourceGetListPageAsync(ResourceGetListPageDto ResourceGetListPageDto);
        /// <summary>
        /// 3、资源【菜单】模型查询【根据ResourceId查询】
        /// </summary>
        /// <returns></returns>
        public Task<ResourceDto> ResourceGetAsync(long ResourceId);
        /// <summary>
        /// 4、资源【菜单】模型更新
        /// </summary>
        /// <returns></returns>
        public Task<bool> ResourceUpdateAsync(ResourceUpdateDto ResourceUpdateDto, long ResourceId);
        /// <summary>
        /// 5、资源【菜单】模型删除
        /// </summary>
        /// <returns></returns>
        public Task<bool> ResourceDeleteAsync(List<long> ResourceIds);
    }
}
