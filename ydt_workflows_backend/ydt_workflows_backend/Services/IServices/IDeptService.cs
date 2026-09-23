using ydt_workflows_backend.Dtos;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 部门模型Service接口
    /// </summary>
    public interface IDeptService
    {
        /// <summary>
        /// 1、部门模型创建
        /// </summary>
        /// <param name="DeptCreateDto"></param>
        /// <returns></returns>
        public Task<bool> DeptCreateAsync(DeptCreateDto DeptCreateDto);

        /// <summary>
        /// 2、部门模型集合查询
        /// </summary>
        /// <returns></returns>
        public Task<List<DeptDto>> DeptGetListAsync(DeptGetListDto DeptGetListDto);

        /// <summary>
        /// 2.1、部门模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<DeptPageDto> DeptGetListPageAsync(DeptGetListPageDto DeptGetListPageDto);
        /// <summary>
        /// 3、部门模型查询【根据DeptId查询】
        /// </summary>
        /// <returns></returns>
        public Task<DeptDto> DeptGetAsync(long DeptId);
        /// <summary>
        /// 4、部门模型更新
        /// </summary>
        /// <returns></returns>
        public Task<bool> DeptUpdateAsync(DeptUpdateDto DeptUpdateDto, long DeptId);
        /// <summary>
        /// 5、部门模型删除
        /// </summary>
        /// <returns></returns>
        public Task<bool> DeptDeleteAsync(List<long> DeptIds);
    }
}
