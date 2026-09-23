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
    /// 工作流模型Service接口
    /// </summary>
    public class WorkflowService : IWorkflowService
    {
        /// <summary>
        /// 工作流固定类
        /// </summary>
        public WorkflowFixtrue _workflowFixtrue { get; set; }

        public IMapper _mapper { get; set; }

        public WorkflowService(WorkflowFixtrue workflowFixtrue,
                               IMapper mapper)
        {
            _workflowFixtrue = workflowFixtrue;
            _mapper = mapper;
        }

        /// <summary>
        /// 工作流模型创建实现
        /// </summary>
        /// <param name="WorkflowCreateDto"></param>
        /// <returns></returns>
        public async Task<bool> WorkflowCreateAsync(WorkflowCreateDto WorkflowCreateDto)
        {
            // 1、WorkflowCreateDto模型映射
            Workflow Workflow = _mapper.Map<Workflow>(WorkflowCreateDto);

            //2、实现工作流模型创建
            return await  _workflowFixtrue.db.Workflows.InsertAsync(Workflow);
        }

        public async Task<List<WorkflowDto>> WorkflowGetListAsync(WorkflowGetListDto WorkflowGetListDto)
        {
            
            //1、查询所有工作流模型
            IEnumerable<Workflow> Workflows = await _workflowFixtrue.db.Workflows.FindAllAsync();
            // 2、工作流模型映射
            List<WorkflowDto> WorkflowDtos = _mapper.Map<List<WorkflowDto>>(Workflows);

            // 3、返回工作流模型
            return WorkflowDtos;
        }

        public async Task<WorkflowPageDto> WorkflowGetListPageAsync(WorkflowGetListPageDto WorkflowGetListPageDto)
        {
            // 1、工作流模型分页Dto映射
            WorkflowGetListPage WorkflowGetListPage = _mapper.Map<WorkflowGetListPage>(WorkflowGetListPageDto);

            // 2、查询分页工作流模型
            WorkflowPage WorkflowPage = await _workflowFixtrue.db.Workflows.WorkflowGetListPageAsync(WorkflowGetListPage);

            // 3、工作流模型分页模型映射
            WorkflowPageDto WorkflowPageDto = _mapper.Map<WorkflowPageDto>(WorkflowPage);
            return WorkflowPageDto;
        }
        public async Task<WorkflowDto> WorkflowGetAsync(string FlowId)
        {
            // 1、查询工作流模型
            Workflow Workflow = await _workflowFixtrue.db.Workflows.FindAsync(m => m.FlowId == FlowId);

            // 2、映射工作流模型
            WorkflowDto WorkflowDto = _mapper.Map<WorkflowDto>(Workflow);

            return WorkflowDto;
        }
        public async Task<bool> WorkflowUpdateAsync(WorkflowUpdateDto WorkflowUpdateDto, string FlowId)
        {
            // 1、查询工作流模型
            Workflow Workflow = await _workflowFixtrue.db.Workflows.FindAsync(m => m.FlowId == FlowId);
            if (Workflow == null)
            {
                throw new CommonException("Workflow不存在");
            }

            // 2、工作流模型模型映射
            Workflow = _mapper.Map<WorkflowUpdateDto, Workflow>(WorkflowUpdateDto, Workflow);

            // 3、工作流模型更新实现
            return await _workflowFixtrue.db.Workflows.UpdateAsync(Workflow);
        }
        public async Task<bool> WorkflowDeleteAsync(List<string> FlowIds)
        {
            // 1、开启事务
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                
                // 2、查询工作流模型
                var Workflows = await _workflowFixtrue.db.Workflows.FindAllAsync(m => FlowIds.Contains(m.FlowId));
                foreach (var Workflow in Workflows)
                {
                    // 3、删除工作流模型【真实删除】
                    await _workflowFixtrue.db.Workflows.DeleteAsync(Workflow);
                }
                tran.Commit();
                return true;
            }
        }
    }
}