using ydt_workflows_backend.Dtos;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 催办记录模型Service接口
    /// </summary>
    public interface IWorkflowUrgeService
    {
        /// <summary>
        /// 1、催办记录模型创建
        /// </summary>
        /// <param name="WorkflowUrgeCreateDto"></param>
        /// <returns></returns>
        public Task<bool> WorkflowUrgeCreateAsync(WorkflowUrgeCreateDto WorkflowUrgeCreateDto);

        /// <summary>
        /// 2、催办记录模型集合查询
        /// </summary>
        /// <returns></returns>
        public Task<List<WorkflowUrgeDto>> WorkflowUrgeGetListAsync(WorkflowUrgeGetListDto WorkflowUrgeGetListDto);

        /// <summary>
        /// 2.1、催办记录模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowUrgePageDto> WorkflowUrgeGetListPageAsync(WorkflowUrgeGetListPageDto WorkflowUrgeGetListPageDto);
        /// <summary>
        /// 3、催办记录模型查询【根据UrgeId查询】
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowUrgeDto> WorkflowUrgeGetAsync(string UrgeId);
        /// <summary>
        /// 4、催办记录模型更新
        /// </summary>
        /// <returns></returns>
        public Task<bool> WorkflowUrgeUpdateAsync(WorkflowUrgeUpdateDto WorkflowUrgeUpdateDto, string UrgeId);
        /// <summary>
        /// 5、催办记录模型删除
        /// </summary>
        /// <returns></returns>
        public Task<bool> WorkflowUrgeDeleteAsync(List<string> UrgeIds);
    }
}
