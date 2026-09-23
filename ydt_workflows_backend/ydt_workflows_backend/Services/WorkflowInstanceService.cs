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
    /// 流程实例模型【根据流程运行流程】Service接口
    /// </summary>
    public class WorkflowInstanceService : IWorkflowInstanceService
    {
        /// <summary>
        /// 工作流固定类
        /// </summary>
        public WorkflowFixtrue _workflowFixtrue { get; set; }

        public IMapper _mapper { get; set; }

        public WorkflowInstanceService(WorkflowFixtrue workflowFixtrue,
                               IMapper mapper)
        {
            _workflowFixtrue = workflowFixtrue;
            _mapper = mapper;
        }

        /// <summary>
        /// 流程实例模型【根据流程运行流程】创建实现
        /// </summary>
        /// <param name="WorkflowInstanceCreateDto"></param>
        /// <returns></returns>
        public async Task<bool> WorkflowInstanceCreateAsync(WorkflowInstanceCreateDto WorkflowInstanceCreateDto)
        {
            // 1、WorkflowInstanceCreateDto模型映射
            WorkflowInstance WorkflowInstance = _mapper.Map<WorkflowInstance>(WorkflowInstanceCreateDto);

            //2、实现流程实例模型【根据流程运行流程】创建
            return await  _workflowFixtrue.db.WorkflowInstances.InsertAsync(WorkflowInstance);
        }

        public async Task<List<WorkflowInstanceDto>> WorkflowInstanceGetListAsync(WorkflowInstanceGetListDto WorkflowInstanceGetListDto)
        {
            
            //1、查询所有流程实例模型【根据流程运行流程】
            IEnumerable<WorkflowInstance> WorkflowInstances = await _workflowFixtrue.db.WorkflowInstances.FindAllAsync();
            // 2、流程实例模型【根据流程运行流程】映射
            List<WorkflowInstanceDto> WorkflowInstanceDtos = _mapper.Map<List<WorkflowInstanceDto>>(WorkflowInstances);

            // 3、返回流程实例模型【根据流程运行流程】
            return WorkflowInstanceDtos;
        }

        public async Task<WorkflowInstancePageDto> WorkflowInstanceGetListPageAsync(WorkflowInstanceGetListPageDto WorkflowInstanceGetListPageDto)
        {
            // 1、流程实例模型【根据流程运行流程】分页Dto映射
            WorkflowInstanceGetListPage WorkflowInstanceGetListPage = _mapper.Map<WorkflowInstanceGetListPage>(WorkflowInstanceGetListPageDto);

            // 2、查询分页流程实例模型【根据流程运行流程】
            WorkflowInstancePage WorkflowInstancePage = await _workflowFixtrue.db.WorkflowInstances.WorkflowInstanceGetListPageAsync(WorkflowInstanceGetListPage);

            // 3、流程实例模型【根据流程运行流程】分页模型映射
            WorkflowInstancePageDto WorkflowInstancePageDto = _mapper.Map<WorkflowInstancePageDto>(WorkflowInstancePage);
            return WorkflowInstancePageDto;
        }
        public async Task<WorkflowInstanceDto> WorkflowInstanceGetAsync(string InstanceId)
        {
            // 1、查询流程实例模型【根据流程运行流程】
            WorkflowInstance WorkflowInstance = await _workflowFixtrue.db.WorkflowInstances.FindAsync(m => m.InstanceId == InstanceId);

            // 2、映射流程实例模型【根据流程运行流程】
            WorkflowInstanceDto WorkflowInstanceDto = _mapper.Map<WorkflowInstanceDto>(WorkflowInstance);

            return WorkflowInstanceDto;
        }
        public async Task<bool> WorkflowInstanceUpdateAsync(WorkflowInstanceUpdateDto WorkflowInstanceUpdateDto, string InstanceId)
        {
            // 1、查询流程实例模型【根据流程运行流程】
            WorkflowInstance WorkflowInstance = await _workflowFixtrue.db.WorkflowInstances.FindAsync(m => m.InstanceId == InstanceId);
            if (WorkflowInstance == null)
            {
                throw new CommonException("WorkflowInstance不存在");
            }

            // 2、流程实例模型【根据流程运行流程】模型映射
            WorkflowInstance = _mapper.Map<WorkflowInstanceUpdateDto, WorkflowInstance>(WorkflowInstanceUpdateDto, WorkflowInstance);

            // 3、流程实例模型【根据流程运行流程】更新实现
            return await _workflowFixtrue.db.WorkflowInstances.UpdateAsync(WorkflowInstance);
        }
        public async Task<bool> WorkflowInstanceDeleteAsync(List<string> InstanceIds)
        {
            // 1、开启事务
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                
                // 2、查询流程实例模型【根据流程运行流程】
                var WorkflowInstances = await _workflowFixtrue.db.WorkflowInstances.FindAllAsync(m => InstanceIds.Contains(m.InstanceId));
                foreach (var WorkflowInstance in WorkflowInstances)
                {
                    // 3、删除流程实例模型【根据流程运行流程】【真实删除】
                    await _workflowFixtrue.db.WorkflowInstances.DeleteAsync(WorkflowInstance);
                }
                tran.Commit();
                return true;
            }
        }
    }
}