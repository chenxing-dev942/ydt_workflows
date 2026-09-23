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
    /// 流程表单模型Service接口
    /// </summary>
    public class WorkflowFormService : IWorkflowFormService
    {
        /// <summary>
        /// 工作流固定类
        /// </summary>
        public WorkflowFixtrue _workflowFixtrue { get; set; }

        public IMapper _mapper { get; set; }

        public WorkflowFormService(WorkflowFixtrue workflowFixtrue,
                               IMapper mapper)
        {
            _workflowFixtrue = workflowFixtrue;
            _mapper = mapper;
        }

        /// <summary>
        /// 流程表单模型创建实现
        /// </summary>
        /// <param name="WorkflowFormCreateDto"></param>
        /// <returns></returns>
        public async Task<bool> WorkflowFormCreateAsync(WorkflowFormCreateDto WorkflowFormCreateDto)
        {
            // 1、WorkflowFormCreateDto模型映射
            WorkflowForm WorkflowForm = _mapper.Map<WorkflowForm>(WorkflowFormCreateDto);

            //2、实现流程表单模型创建
            return await  _workflowFixtrue.db.WorkflowForms.InsertAsync(WorkflowForm);
        }

        public async Task<List<WorkflowFormDto>> WorkflowFormGetListAsync(WorkflowFormGetListDto WorkflowFormGetListDto)
        {
            
            //1、查询所有流程表单模型
            IEnumerable<WorkflowForm> WorkflowForms = await _workflowFixtrue.db.WorkflowForms.FindAllAsync();
            // 2、流程表单模型映射
            List<WorkflowFormDto> WorkflowFormDtos = _mapper.Map<List<WorkflowFormDto>>(WorkflowForms);

            // 3、返回流程表单模型
            return WorkflowFormDtos;
        }

        public async Task<WorkflowFormPageDto> WorkflowFormGetListPageAsync(WorkflowFormGetListPageDto WorkflowFormGetListPageDto)
        {
            // 1、流程表单模型分页Dto映射
            WorkflowFormGetListPage WorkflowFormGetListPage = _mapper.Map<WorkflowFormGetListPage>(WorkflowFormGetListPageDto);

            // 2、查询分页流程表单模型
            WorkflowFormPage WorkflowFormPage = await _workflowFixtrue.db.WorkflowForms.WorkflowFormGetListPageAsync(WorkflowFormGetListPage);

            // 3、流程表单模型分页模型映射
            WorkflowFormPageDto WorkflowFormPageDto = _mapper.Map<WorkflowFormPageDto>(WorkflowFormPage);
            return WorkflowFormPageDto;
        }
        public async Task<WorkflowFormDto> WorkflowFormGetAsync(string FormId)
        {
            // 1、查询流程表单模型
            WorkflowForm WorkflowForm = await _workflowFixtrue.db.WorkflowForms.FindAsync(m => m.FormId == FormId);

            // 2、映射流程表单模型
            WorkflowFormDto WorkflowFormDto = _mapper.Map<WorkflowFormDto>(WorkflowForm);

            return WorkflowFormDto;
        }
        public async Task<bool> WorkflowFormUpdateAsync(WorkflowFormUpdateDto WorkflowFormUpdateDto, string FormId)
        {
            // 1、查询流程表单模型
            WorkflowForm WorkflowForm = await _workflowFixtrue.db.WorkflowForms.FindAsync(m => m.FormId == FormId);
            if (WorkflowForm == null)
            {
                throw new CommonException("WorkflowForm不存在");
            }

            // 2、流程表单模型模型映射
            WorkflowForm = _mapper.Map<WorkflowFormUpdateDto, WorkflowForm>(WorkflowFormUpdateDto, WorkflowForm);

            // 3、流程表单模型更新实现
            return await _workflowFixtrue.db.WorkflowForms.UpdateAsync(WorkflowForm);
        }
        public async Task<bool> WorkflowFormDeleteAsync(List<string> FormIds)
        {
            // 1、开启事务
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                
                // 2、查询流程表单模型
                var WorkflowForms = await _workflowFixtrue.db.WorkflowForms.FindAllAsync(m => FormIds.Contains(m.FormId));
                foreach (var WorkflowForm in WorkflowForms)
                {
                    // 3、删除流程表单模型【真实删除】
                    await _workflowFixtrue.db.WorkflowForms.DeleteAsync(WorkflowForm);
                }
                tran.Commit();
                return true;
            }
        }
    }
}