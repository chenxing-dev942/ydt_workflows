using ydt_workflows_backend.Dtos;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 子系统模型Service接口
    /// </summary>
    public interface ISystemsService
    {
        /// <summary>
        /// 1、子系统模型创建
        /// </summary>
        /// <param name="SystemsCreateDto"></param>
        /// <returns></returns>
        public Task<bool> SystemsCreateAsync(SystemsCreateDto SystemsCreateDto);

        /// <summary>
        /// 2、子系统模型集合查询
        /// </summary>
        /// <returns></returns>
        public Task<List<SystemsDto>> SystemsGetListAsync(SystemsGetListDto SystemsGetListDto);

        /// <summary>
        /// 2.1、子系统模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<SystemsPageDto> SystemsGetListPageAsync(SystemsGetListPageDto SystemsGetListPageDto);
        /// <summary>
        /// 3、子系统模型查询【根据SystemId查询】
        /// </summary>
        /// <returns></returns>
        public Task<SystemsDto> SystemsGetAsync(long SystemId);
        /// <summary>
        /// 4、子系统模型更新
        /// </summary>
        /// <returns></returns>
        public Task<bool> SystemsUpdateAsync(SystemsUpdateDto SystemsUpdateDto, long SystemId);
        /// <summary>
        /// 5、子系统模型删除
        /// </summary>
        /// <returns></returns>
        public Task<bool> SystemsDeleteAsync(List<long> SystemIds);
    }
}
