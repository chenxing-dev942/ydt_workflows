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
    /// 流程实例表单关联模型Service接口
    /// </summary>
    public class WorkflowInstanceFormService : IWorkflowInstanceFormService
    {
        /// <summary>
        /// 工作流固定类
        /// </summary>
        public WorkflowFixtrue _workflowFixtrue { get; set; }

        public IMapper _mapper { get; set; }

        public WorkflowInstanceFormService(WorkflowFixtrue workflowFixtrue,
                               IMapper mapper)
        {
            _workflowFixtrue = workflowFixtrue;
            _mapper = mapper;
        }

        /// <summary>
        /// 流程实例表单关联模型创建实现
        /// </summary>
        /// <param name="WorkflowInstanceFormCreateDto"></param>
        /// <returns></returns>
        public async Task<bool> WorkflowInstanceFormCreateAsync(WorkflowInstanceFormCreateDto WorkflowInstanceFormCreateDto)
        {
            // 1、WorkflowInstanceFormCreateDto模型映射
            WorkflowInstanceForm WorkflowInstanceForm = _mapper.Map<WorkflowInstanceForm>(WorkflowInstanceFormCreateDto);

            //2、实现流程实例表单关联模型创建
            return await  _workflowFixtrue.db.WorkflowInstanceForms.InsertAsync(WorkflowInstanceForm);
        }

        public async Task<List<WorkflowInstanceFormDto>> WorkflowInstanceFormGetListAsync(WorkflowInstanceFormGetListDto WorkflowInstanceFormGetListDto)
        {
            
            //1、查询所有流程实例表单关联模型
            IEnumerable<WorkflowInstanceForm> WorkflowInstanceForms = await _workflowFixtrue.db.WorkflowInstanceForms.FindAllAsync();
            // 2、流程实例表单关联模型映射
            List<WorkflowInstanceFormDto> WorkflowInstanceFormDtos = _mapper.Map<List<WorkflowInstanceFormDto>>(WorkflowInstanceForms);

            // 3、返回流程实例表单关联模型
            return WorkflowInstanceFormDtos;
        }

        public async Task<WorkflowInstanceFormPageDto> WorkflowInstanceFormGetListPageAsync(WorkflowInstanceFormGetListPageDto WorkflowInstanceFormGetListPageDto)
        {
            // 1、流程实例表单关联模型分页Dto映射
            WorkflowInstanceFormGetListPage WorkflowInstanceFormGetListPage = _mapper.Map<WorkflowInstanceFormGetListPage>(WorkflowInstanceFormGetListPageDto);

            // 2、查询分页流程实例表单关联模型
            WorkflowInstanceFormPage WorkflowInstanceFormPage = await _workflowFixtrue.db.WorkflowInstanceForms.WorkflowInstanceFormGetListPageAsync(WorkflowInstanceFormGetListPage);

            // 3、流程实例表单关联模型分页模型映射
            WorkflowInstanceFormPageDto WorkflowInstanceFormPageDto = _mapper.Map<WorkflowInstanceFormPageDto>(WorkflowInstanceFormPage);
            return WorkflowInstanceFormPageDto;
        }
        public async Task<WorkflowInstanceFormDto> WorkflowInstanceFormGetAsync(string InstanceFormId)
        {
            // 1、查询流程实例表单关联模型
            WorkflowInstanceForm WorkflowInstanceForm = await _workflowFixtrue.db.WorkflowInstanceForms.FindAsync(m => m.InstanceFormId == InstanceFormId);

            // 2、映射流程实例表单关联模型
            WorkflowInstanceFormDto WorkflowInstanceFormDto = _mapper.Map<WorkflowInstanceFormDto>(WorkflowInstanceForm);

            return WorkflowInstanceFormDto;
        }
        public async Task<bool> WorkflowInstanceFormUpdateAsync(WorkflowInstanceFormUpdateDto WorkflowInstanceFormUpdateDto, string InstanceFormId)
        {
            // 1、查询流程实例表单关联模型
            WorkflowInstanceForm WorkflowInstanceForm = await _workflowFixtrue.db.WorkflowInstanceForms.FindAsync(m => m.InstanceFormId == InstanceFormId);
            if (WorkflowInstanceForm == null)
            {
                throw new CommonException("WorkflowInstanceForm不存在");
            }

            // 2、流程实例表单关联模型模型映射
            WorkflowInstanceForm = _mapper.Map<WorkflowInstanceFormUpdateDto, WorkflowInstanceForm>(WorkflowInstanceFormUpdateDto, WorkflowInstanceForm);

            // 3、流程实例表单关联模型更新实现
            return await _workflowFixtrue.db.WorkflowInstanceForms.UpdateAsync(WorkflowInstanceForm);
        }
        public async Task<bool> WorkflowInstanceFormDeleteAsync(List<string> InstanceFormIds)
        {
            // 1、开启事务
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                
                // 2、查询流程实例表单关联模型
                var WorkflowInstanceForms = await _workflowFixtrue.db.WorkflowInstanceForms.FindAllAsync(m => InstanceFormIds.Contains(m.InstanceFormId));
                foreach (var WorkflowInstanceForm in WorkflowInstanceForms)
                {
                    // 3、删除流程实例表单关联模型【真实删除】
                    await _workflowFixtrue.db.WorkflowInstanceForms.DeleteAsync(WorkflowInstanceForm);
                }
                tran.Commit();
                return true;
            }
        }
    }
}