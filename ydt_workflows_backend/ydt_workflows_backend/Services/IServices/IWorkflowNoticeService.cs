using ydt_workflows_backend.Dtos;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 流程通知节点模型Service接口
    /// </summary>
    public interface IWorkflowNoticeService
    {
        /// <summary>
        /// 1、流程通知节点模型创建
        /// </summary>
        /// <param name="WorkflowNoticeCreateDto"></param>
        /// <returns></returns>
        public Task<bool> WorkflowNoticeCreateAsync(WorkflowNoticeCreateDto WorkflowNoticeCreateDto);

        /// <summary>
        /// 2、流程通知节点模型集合查询
        /// </summary>
        /// <returns></returns>
        public Task<List<WorkflowNoticeDto>> WorkflowNoticeGetListAsync(WorkflowNoticeGetListDto WorkflowNoticeGetListDto);

        /// <summary>
        /// 2.1、流程通知节点模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowNoticePageDto> WorkflowNoticeGetListPageAsync(WorkflowNoticeGetListPageDto WorkflowNoticeGetListPageDto);
        /// <summary>
        /// 3、流程通知节点模型查询【根据NoticeId查询】
        /// </summary>
        /// <returns></returns>
        public Task<WorkflowNoticeDto> WorkflowNoticeGetAsync(string NoticeId);
        /// <summary>
        /// 4、流程通知节点模型更新
        /// </summary>
        /// <returns></returns>
        public Task<bool> WorkflowNoticeUpdateAsync(WorkflowNoticeUpdateDto WorkflowNoticeUpdateDto, string NoticeId);
        /// <summary>
        /// 5、流程通知节点模型删除
        /// </summary>
        /// <returns></returns>
        public Task<bool> WorkflowNoticeDeleteAsync(List<string> NoticeIds);
    }
}
