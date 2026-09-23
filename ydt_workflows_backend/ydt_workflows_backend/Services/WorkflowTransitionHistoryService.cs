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
    /// 流程流转历史模型Service接口
    /// </summary>
    public class WorkflowTransitionHistoryService : IWorkflowTransitionHistoryService
    {
        /// <summary>
        /// 工作流固定类
        /// </summary>
        public WorkflowFixtrue _workflowFixtrue { get; set; }

        public IMapper _mapper { get; set; }

        public WorkflowTransitionHistoryService(WorkflowFixtrue workflowFixtrue,
                               IMapper mapper)
        {
            _workflowFixtrue = workflowFixtrue;
            _mapper = mapper;
        }

        /// <summary>
        /// 流程流转历史模型创建实现
        /// </summary>
        /// <param name="WorkflowTransitionHistoryCreateDto"></param>
        /// <returns></returns>
        public async Task<bool> WorkflowTransitionHistoryCreateAsync(WorkflowTransitionHistoryCreateDto WorkflowTransitionHistoryCreateDto)
        {
            // 1、WorkflowTransitionHistoryCreateDto模型映射
            WorkflowTransitionHistory WorkflowTransitionHistory = _mapper.Map<WorkflowTransitionHistory>(WorkflowTransitionHistoryCreateDto);

            //2、实现流程流转历史模型创建
            return await  _workflowFixtrue.db.WorkflowTransitionHistorys.InsertAsync(WorkflowTransitionHistory);
        }

        public async Task<List<WorkflowTransitionHistoryDto>> WorkflowTransitionHistoryGetListAsync(WorkflowTransitionHistoryGetListDto WorkflowTransitionHistoryGetListDto)
        {
            
            //1、查询所有流程流转历史模型
            IEnumerable<WorkflowTransitionHistory> WorkflowTransitionHistorys = await _workflowFixtrue.db.WorkflowTransitionHistorys.FindAllAsync();
            // 2、流程流转历史模型映射
            List<WorkflowTransitionHistoryDto> WorkflowTransitionHistoryDtos = _mapper.Map<List<WorkflowTransitionHistoryDto>>(WorkflowTransitionHistorys);

            // 3、返回流程流转历史模型
            return WorkflowTransitionHistoryDtos;
        }

        public async Task<WorkflowTransitionHistoryPageDto> WorkflowTransitionHistoryGetListPageAsync(WorkflowTransitionHistoryGetListPageDto WorkflowTransitionHistoryGetListPageDto)
        {
            // 1、流程流转历史模型分页Dto映射
            WorkflowTransitionHistoryGetListPage WorkflowTransitionHistoryGetListPage = _mapper.Map<WorkflowTransitionHistoryGetListPage>(WorkflowTransitionHistoryGetListPageDto);

            // 2、查询分页流程流转历史模型
            WorkflowTransitionHistoryPage WorkflowTransitionHistoryPage = await _workflowFixtrue.db.WorkflowTransitionHistorys.WorkflowTransitionHistoryGetListPageAsync(WorkflowTransitionHistoryGetListPage);

            // 3、流程流转历史模型分页模型映射
            WorkflowTransitionHistoryPageDto WorkflowTransitionHistoryPageDto = _mapper.Map<WorkflowTransitionHistoryPageDto>(WorkflowTransitionHistoryPage);
            return WorkflowTransitionHistoryPageDto;
        }
        public async Task<WorkflowTransitionHistoryDto> WorkflowTransitionHistoryGetAsync(string TransitionId)
        {
            // 1、查询流程流转历史模型
            WorkflowTransitionHistory WorkflowTransitionHistory = await _workflowFixtrue.db.WorkflowTransitionHistorys.FindAsync(m => m.TransitionId == TransitionId);

            // 2、映射流程流转历史模型
            WorkflowTransitionHistoryDto WorkflowTransitionHistoryDto = _mapper.Map<WorkflowTransitionHistoryDto>(WorkflowTransitionHistory);

            return WorkflowTransitionHistoryDto;
        }
        public async Task<bool> WorkflowTransitionHistoryUpdateAsync(WorkflowTransitionHistoryUpdateDto WorkflowTransitionHistoryUpdateDto, string TransitionId)
        {
            // 1、查询流程流转历史模型
            WorkflowTransitionHistory WorkflowTransitionHistory = await _workflowFixtrue.db.WorkflowTransitionHistorys.FindAsync(m => m.TransitionId == TransitionId);
            if (WorkflowTransitionHistory == null)
            {
                throw new CommonException("WorkflowTransitionHistory不存在");
            }

            // 2、流程流转历史模型模型映射
            WorkflowTransitionHistory = _mapper.Map<WorkflowTransitionHistoryUpdateDto, WorkflowTransitionHistory>(WorkflowTransitionHistoryUpdateDto, WorkflowTransitionHistory);

            // 3、流程流转历史模型更新实现
            return await _workflowFixtrue.db.WorkflowTransitionHistorys.UpdateAsync(WorkflowTransitionHistory);
        }
        public async Task<bool> WorkflowTransitionHistoryDeleteAsync(List<string> TransitionIds)
        {
            // 1、开启事务
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                
                // 2、查询流程流转历史模型
                var WorkflowTransitionHistorys = await _workflowFixtrue.db.WorkflowTransitionHistorys.FindAllAsync(m => TransitionIds.Contains(m.TransitionId));
                foreach (var WorkflowTransitionHistory in WorkflowTransitionHistorys)
                {
                    // 3、删除流程流转历史模型【真实删除】
                    await _workflowFixtrue.db.WorkflowTransitionHistorys.DeleteAsync(WorkflowTransitionHistory);
                }
                tran.Commit();
                return true;
            }
        }
    }
}