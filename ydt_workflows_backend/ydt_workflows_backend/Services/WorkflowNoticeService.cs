using ydt_workflows_backend.Contexts;
using ydt_workflows_backend.Fixtrues;
using ydt_workflows_backend.Dtos;
using AutoMapper;
using AutoMapper;
using ydt_workflows_backend.Models;
using ydt_workflows_backend.CommonExceptions;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 流程通知节点模型Service接口
    /// </summary>
    public class WorkflowNoticeService : IWorkflowNoticeService
    {
        /// <summary>
        /// 工作流固定类
        /// </summary>
        public WorkflowFixtrue _workflowFixtrue { get; set; }

        public IMapper _mapper { get; set; }

        public WorkflowNoticeService(WorkflowFixtrue workflowFixtrue,
                               IMapper mapper)
        {
            _workflowFixtrue = workflowFixtrue;
            _mapper = mapper;
        }

        /// <summary>
        /// 流程通知节点模型创建实现
        /// </summary>
        /// <param name="WorkflowNoticeCreateDto"></param>
        /// <returns></returns>
        public async Task<bool> WorkflowNoticeCreateAsync(WorkflowNoticeCreateDto WorkflowNoticeCreateDto)
        {
            // 1、WorkflowNoticeCreateDto模型映射
            WorkflowNotice WorkflowNotice = _mapper.Map<WorkflowNotice>(WorkflowNoticeCreateDto);

            //2、实现流程通知节点模型创建
            return await  _workflowFixtrue.db.WorkflowNotices.InsertAsync(WorkflowNotice);
        }

        public async Task<List<WorkflowNoticeDto>> WorkflowNoticeGetListAsync(WorkflowNoticeGetListDto WorkflowNoticeGetListDto)
        {
            
            //1、查询所有流程通知节点模型
            IEnumerable<WorkflowNotice> WorkflowNotices = await _workflowFixtrue.db.WorkflowNotices.FindAllAsync();
            // 2、流程通知节点模型映射
            List<WorkflowNoticeDto> WorkflowNoticeDtos = _mapper.Map<List<WorkflowNoticeDto>>(WorkflowNotices);

            // 3、返回流程通知节点模型
            return WorkflowNoticeDtos;
        }

        public async Task<WorkflowNoticePageDto> WorkflowNoticeGetListPageAsync(WorkflowNoticeGetListPageDto WorkflowNoticeGetListPageDto)
        {
            // 1、流程通知节点模型分页Dto映射
            WorkflowNoticeGetListPage WorkflowNoticeGetListPage = _mapper.Map<WorkflowNoticeGetListPage>(WorkflowNoticeGetListPageDto);

            // 2、查询分页流程通知节点模型
            WorkflowNoticePage WorkflowNoticePage = await _workflowFixtrue.db.WorkflowNotices.WorkflowNoticeGetListPageAsync(WorkflowNoticeGetListPage);

            // 3、流程通知节点模型分页模型映射
            WorkflowNoticePageDto WorkflowNoticePageDto = _mapper.Map<WorkflowNoticePageDto>(WorkflowNoticePage);
            return WorkflowNoticePageDto;
        }
        public async Task<WorkflowNoticeDto> WorkflowNoticeGetAsync(string NoticeId)
        {
            // 1、查询流程通知节点模型
            WorkflowNotice WorkflowNotice = await _workflowFixtrue.db.WorkflowNotices.FindAsync(m => m.NoticeId == NoticeId);

            // 2、映射流程通知节点模型
            WorkflowNoticeDto WorkflowNoticeDto = _mapper.Map<WorkflowNoticeDto>(WorkflowNotice);

            return WorkflowNoticeDto;
        }
        public async Task<bool> WorkflowNoticeUpdateAsync(WorkflowNoticeUpdateDto WorkflowNoticeUpdateDto, string NoticeId)
        {
            // 1、查询流程通知节点模型
            WorkflowNotice WorkflowNotice = await _workflowFixtrue.db.WorkflowNotices.FindAsync(m => m.NoticeId == NoticeId);
            if (WorkflowNotice == null)
            {
                throw new CommonException("WorkflowNotice不存在");
            }

            // 2、流程通知节点模型模型映射
            WorkflowNotice = _mapper.Map<WorkflowNoticeUpdateDto, WorkflowNotice>(WorkflowNoticeUpdateDto, WorkflowNotice);

            // 3、流程通知节点模型更新实现
            return await _workflowFixtrue.db.WorkflowNotices.UpdateAsync(WorkflowNotice);
        }
        public async Task<bool> WorkflowNoticeDeleteAsync(List<string> NoticeIds)
        {
            // 1、开启事务
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                
                // 2、查询流程通知节点模型
                var WorkflowNotices = await _workflowFixtrue.db.WorkflowNotices.FindAllAsync(m => NoticeIds.Contains(m.NoticeId));
                foreach (var WorkflowNotice in WorkflowNotices)
                {
                    // 3、删除流程通知节点模型【真实删除】
                    await _workflowFixtrue.db.WorkflowNotices.DeleteAsync(WorkflowNotice);
                }
                tran.Commit();
                return true;
            }
        }
    }
}