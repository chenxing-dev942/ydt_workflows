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
    /// 催办记录模型Service接口
    /// </summary>
    public class WorkflowUrgeService : IWorkflowUrgeService
    {
        /// <summary>
        /// 工作流固定类
        /// </summary>
        public WorkflowFixtrue _workflowFixtrue { get; set; }

        public IMapper _mapper { get; set; }

        public WorkflowUrgeService(WorkflowFixtrue workflowFixtrue,
                               IMapper mapper)
        {
            _workflowFixtrue = workflowFixtrue;
            _mapper = mapper;
        }

        /// <summary>
        /// 催办记录模型创建实现
        /// </summary>
        /// <param name="WorkflowUrgeCreateDto"></param>
        /// <returns></returns>
        public async Task<bool> WorkflowUrgeCreateAsync(WorkflowUrgeCreateDto WorkflowUrgeCreateDto)
        {
            // 1、WorkflowUrgeCreateDto模型映射
            WorkflowUrge WorkflowUrge = _mapper.Map<WorkflowUrge>(WorkflowUrgeCreateDto);

            //2、实现催办记录模型创建
            return await  _workflowFixtrue.db.WorkflowUrges.InsertAsync(WorkflowUrge);
        }

        public async Task<List<WorkflowUrgeDto>> WorkflowUrgeGetListAsync(WorkflowUrgeGetListDto WorkflowUrgeGetListDto)
        {
            
            //1、查询所有催办记录模型
            IEnumerable<WorkflowUrge> WorkflowUrges = await _workflowFixtrue.db.WorkflowUrges.FindAllAsync();
            // 2、催办记录模型映射
            List<WorkflowUrgeDto> WorkflowUrgeDtos = _mapper.Map<List<WorkflowUrgeDto>>(WorkflowUrges);

            // 3、返回催办记录模型
            return WorkflowUrgeDtos;
        }

        public async Task<WorkflowUrgePageDto> WorkflowUrgeGetListPageAsync(WorkflowUrgeGetListPageDto WorkflowUrgeGetListPageDto)
        {
            // 1、催办记录模型分页Dto映射
            WorkflowUrgeGetListPage WorkflowUrgeGetListPage = _mapper.Map<WorkflowUrgeGetListPage>(WorkflowUrgeGetListPageDto);

            // 2、查询分页催办记录模型
            WorkflowUrgePage WorkflowUrgePage = await _workflowFixtrue.db.WorkflowUrges.WorkflowUrgeGetListPageAsync(WorkflowUrgeGetListPage);

            // 3、催办记录模型分页模型映射
            WorkflowUrgePageDto WorkflowUrgePageDto = _mapper.Map<WorkflowUrgePageDto>(WorkflowUrgePage);
            return WorkflowUrgePageDto;
        }
        public async Task<WorkflowUrgeDto> WorkflowUrgeGetAsync(string UrgeId)
        {
            // 1、查询催办记录模型
            WorkflowUrge WorkflowUrge = await _workflowFixtrue.db.WorkflowUrges.FindAsync(m => m.UrgeId == UrgeId);

            // 2、映射催办记录模型
            WorkflowUrgeDto WorkflowUrgeDto = _mapper.Map<WorkflowUrgeDto>(WorkflowUrge);

            return WorkflowUrgeDto;
        }
        public async Task<bool> WorkflowUrgeUpdateAsync(WorkflowUrgeUpdateDto WorkflowUrgeUpdateDto, string UrgeId)
        {
            // 1、查询催办记录模型
            WorkflowUrge WorkflowUrge = await _workflowFixtrue.db.WorkflowUrges.FindAsync(m => m.UrgeId == UrgeId);
            if (WorkflowUrge == null)
            {
                throw new CommonException("WorkflowUrge不存在");
            }

            // 2、催办记录模型模型映射
            WorkflowUrge = _mapper.Map<WorkflowUrgeUpdateDto, WorkflowUrge>(WorkflowUrgeUpdateDto, WorkflowUrge);

            // 3、催办记录模型更新实现
            return await _workflowFixtrue.db.WorkflowUrges.UpdateAsync(WorkflowUrge);
        }
        public async Task<bool> WorkflowUrgeDeleteAsync(List<string> UrgeIds)
        {
            // 1、开启事务
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                
                // 2、查询催办记录模型
                var WorkflowUrges = await _workflowFixtrue.db.WorkflowUrges.FindAllAsync(m => UrgeIds.Contains(m.UrgeId));
                foreach (var WorkflowUrge in WorkflowUrges)
                {
                    // 3、删除催办记录模型【真实删除】
                    await _workflowFixtrue.db.WorkflowUrges.DeleteAsync(WorkflowUrge);
                }
                tran.Commit();
                return true;
            }
        }
    }
}