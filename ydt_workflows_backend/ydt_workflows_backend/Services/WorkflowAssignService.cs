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
    /// 流程委托模型Service接口
    /// </summary>
    public class WorkflowAssignService : IWorkflowAssignService
    {
        /// <summary>
        /// 工作流固定类
        /// </summary>
        public WorkflowFixtrue _workflowFixtrue { get; set; }

        public IMapper _mapper { get; set; }

        public WorkflowAssignService(WorkflowFixtrue workflowFixtrue,
                               IMapper mapper)
        {
            _workflowFixtrue = workflowFixtrue;
            _mapper = mapper;
        }

        /// <summary>
        /// 流程委托模型创建实现
        /// </summary>
        /// <param name="WorkflowAssignCreateDto"></param>
        /// <returns></returns>
        public async Task<bool> WorkflowAssignCreateAsync(WorkflowAssignCreateDto WorkflowAssignCreateDto)
        {
            // 1、WorkflowAssignCreateDto模型映射
            WorkflowAssign WorkflowAssign = _mapper.Map<WorkflowAssign>(WorkflowAssignCreateDto);

            //2、实现流程委托模型创建
            return await  _workflowFixtrue.db.WorkflowAssigns.InsertAsync(WorkflowAssign);
        }

        public async Task<List<WorkflowAssignDto>> WorkflowAssignGetListAsync(WorkflowAssignGetListDto WorkflowAssignGetListDto)
        {
            
            //1、查询所有流程委托模型
            IEnumerable<WorkflowAssign> WorkflowAssigns = await _workflowFixtrue.db.WorkflowAssigns.FindAllAsync();
            // 2、流程委托模型映射
            List<WorkflowAssignDto> WorkflowAssignDtos = _mapper.Map<List<WorkflowAssignDto>>(WorkflowAssigns);

            // 3、返回流程委托模型
            return WorkflowAssignDtos;
        }

        public async Task<WorkflowAssignPageDto> WorkflowAssignGetListPageAsync(WorkflowAssignGetListPageDto WorkflowAssignGetListPageDto)
        {
            // 1、流程委托模型分页Dto映射
            WorkflowAssignGetListPage WorkflowAssignGetListPage = _mapper.Map<WorkflowAssignGetListPage>(WorkflowAssignGetListPageDto);

            // 2、查询分页流程委托模型
            WorkflowAssignPage WorkflowAssignPage = await _workflowFixtrue.db.WorkflowAssigns.WorkflowAssignGetListPageAsync(WorkflowAssignGetListPage);

            // 3、流程委托模型分页模型映射
            WorkflowAssignPageDto WorkflowAssignPageDto = _mapper.Map<WorkflowAssignPageDto>(WorkflowAssignPage);
            return WorkflowAssignPageDto;
        }
        public async Task<WorkflowAssignDto> WorkflowAssignGetAsync(string AssignId)
        {
            // 1、查询流程委托模型
            WorkflowAssign WorkflowAssign = await _workflowFixtrue.db.WorkflowAssigns.FindAsync(m => m.AssignId == AssignId);

            // 2、映射流程委托模型
            WorkflowAssignDto WorkflowAssignDto = _mapper.Map<WorkflowAssignDto>(WorkflowAssign);

            return WorkflowAssignDto;
        }
        public async Task<bool> WorkflowAssignUpdateAsync(WorkflowAssignUpdateDto WorkflowAssignUpdateDto, string AssignId)
        {
            // 1、查询流程委托模型
            WorkflowAssign WorkflowAssign = await _workflowFixtrue.db.WorkflowAssigns.FindAsync(m => m.AssignId == AssignId);
            if (WorkflowAssign == null)
            {
                throw new CommonException("WorkflowAssign不存在");
            }

            // 2、流程委托模型模型映射
            WorkflowAssign = _mapper.Map<WorkflowAssignUpdateDto, WorkflowAssign>(WorkflowAssignUpdateDto, WorkflowAssign);

            // 3、流程委托模型更新实现
            return await _workflowFixtrue.db.WorkflowAssigns.UpdateAsync(WorkflowAssign);
        }
        public async Task<bool> WorkflowAssignDeleteAsync(List<string> AssignIds)
        {
            // 1、开启事务
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                
                // 2、查询流程委托模型
                var WorkflowAssigns = await _workflowFixtrue.db.WorkflowAssigns.FindAllAsync(m => AssignIds.Contains(m.AssignId));
                foreach (var WorkflowAssign in WorkflowAssigns)
                {
                    // 3、删除流程委托模型【真实删除】
                    await _workflowFixtrue.db.WorkflowAssigns.DeleteAsync(WorkflowAssign);
                }
                tran.Commit();
                return true;
            }
        }
    }
}