using AutoMapper;
using AutoMapper;
using System.Collections.Generic;
using ydt_workflows_backend.CommonExceptions;
using ydt_workflows_backend.Contexts;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Fixtrues;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 流程操作历史模型Service接口
    /// </summary>
    public class WorkflowOperationHistoryService : IWorkflowOperationHistoryService
    {
        /// <summary>
        /// 工作流固定类
        /// </summary>
        public WorkflowFixtrue _workflowFixtrue { get; set; }

        public IMapper _mapper { get; set; }

        public WorkflowOperationHistoryService(WorkflowFixtrue workflowFixtrue,
                               IMapper mapper)
        {
            _workflowFixtrue = workflowFixtrue;
            _mapper = mapper;
        }

        /// <summary>
        /// 流程操作历史模型创建实现
        /// </summary>
        /// <param name="WorkflowOperationHistoryCreateDto"></param>
        /// <returns></returns>
        public async Task<bool> WorkflowOperationHistoryCreateAsync(WorkflowOperationHistoryCreateDto WorkflowOperationHistoryCreateDto)
        {
            // 1、WorkflowOperationHistoryCreateDto模型映射
            WorkflowOperationHistory WorkflowOperationHistory = _mapper.Map<WorkflowOperationHistory>(WorkflowOperationHistoryCreateDto);

            //2、实现流程操作历史模型创建
            return await  _workflowFixtrue.db.WorkflowOperationHistorys.InsertAsync(WorkflowOperationHistory);
        }

        public async Task<List<WorkflowOperationHistoryDto>> WorkflowOperationHistoryGetListAsync(WorkflowOperationHistoryGetListDto WorkflowOperationHistoryGetListDto)
        {
            
            //1、查询所有流程操作历史模型
            IEnumerable<WorkflowOperationHistory> WorkflowOperationHistorys = await _workflowFixtrue.db.WorkflowOperationHistorys.FindAllAsync();
            // 2、流程操作历史模型映射
            List<WorkflowOperationHistoryDto> WorkflowOperationHistoryDtos = _mapper.Map<List<WorkflowOperationHistoryDto>>(WorkflowOperationHistorys);

            // 3、返回流程操作历史模型
            return WorkflowOperationHistoryDtos;
        }

        /// <summary>
        /// 审批历史分页查询 
        /// </summary>
        /// <param name="WorkflowOperationHistoryGetListPageDto"></param>
        /// <returns></returns>
        public async Task<ApprovalHistoryPageDto> WorkflowOperationHistoryGetListPageAsync(ApprovalHistoryGetListPageDto WorkflowOperationHistoryGetListPageDto)
        {
            // 1、流程操作历史模型分页Dto映射
            WorkflowOperationHistoryGetListPage WorkflowOperationHistoryGetListPage = _mapper.Map<WorkflowOperationHistoryGetListPage>(WorkflowOperationHistoryGetListPageDto);

            // 2、查询分页流程操作历史模型
            WorkflowOperationHistoryPage WorkflowOperationHistoryPage = await _workflowFixtrue.db.WorkflowOperationHistorys.WorkflowOperationHistoryGetListPageAsync(WorkflowOperationHistoryGetListPage);

            // 3、流程操作历史模型分页模型映射
            ApprovalHistoryPageDto approvalHistoryPageDto = _mapper.Map<ApprovalHistoryPageDto>(WorkflowOperationHistoryPage);
            List<WorkflowOperationHistoryDto> historyDtos= approvalHistoryPageDto.WorkflowOperationHistorys;

            foreach (var historyDto in historyDtos)
            {
                var workclowInstance=await _workflowFixtrue.db.WorkflowInstances.FindByIdAsync(historyDto.InstanceId);
                historyDto.InstanceCode = workclowInstance.Code;
                historyDto.Status=workclowInstance.Status;
                historyDto.CreateUserName=workclowInstance.CreateUserName;
                historyDto.FlowId=workclowInstance.FlowId;
                historyDto.CreateTime=workclowInstance.UpdateTime;
            }


            foreach (var historyDto in historyDtos)
            {
                Workflow workflow = await _workflowFixtrue.db.Workflows.FindByIdAsync(historyDto.FlowId);
                historyDto.FlowName=workflow.FlowName;
                historyDto.FormId=workflow.FormId;
            }

            foreach (var historyDto in historyDtos)
            {
                WorkflowForm workflowForm = await _workflowFixtrue.db.WorkflowForms.FindByIdAsync(historyDto.FormId);
                historyDto.FormType=workflowForm.FormType;
                historyDto.FormName=workflowForm.FormName;
            }

            return approvalHistoryPageDto;
        }
        public async Task<WorkflowOperationHistoryDto> WorkflowOperationHistoryGetAsync(string OperationId)
        {
            // 1、查询流程操作历史模型
            WorkflowOperationHistory WorkflowOperationHistory = await _workflowFixtrue.db.WorkflowOperationHistorys.FindAsync(m => m.OperationId == OperationId);

            // 2、映射流程操作历史模型
            WorkflowOperationHistoryDto WorkflowOperationHistoryDto = _mapper.Map<WorkflowOperationHistoryDto>(WorkflowOperationHistory);

            return WorkflowOperationHistoryDto;
        }
        public async Task<bool> WorkflowOperationHistoryUpdateAsync(WorkflowOperationHistoryUpdateDto WorkflowOperationHistoryUpdateDto, string OperationId)
        {
            // 1、查询流程操作历史模型
            WorkflowOperationHistory WorkflowOperationHistory = await _workflowFixtrue.db.WorkflowOperationHistorys.FindAsync(m => m.OperationId == OperationId);
            if (WorkflowOperationHistory == null)
            {
                throw new CommonException("WorkflowOperationHistory不存在");
            }

            // 2、流程操作历史模型模型映射
            WorkflowOperationHistory = _mapper.Map<WorkflowOperationHistoryUpdateDto, WorkflowOperationHistory>(WorkflowOperationHistoryUpdateDto, WorkflowOperationHistory);

            // 3、流程操作历史模型更新实现
            return await _workflowFixtrue.db.WorkflowOperationHistorys.UpdateAsync(WorkflowOperationHistory);
        }
        public async Task<bool> WorkflowOperationHistoryDeleteAsync(List<string> OperationIds)
        {
            // 1、开启事务
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                
                // 2、查询流程操作历史模型
                var WorkflowOperationHistorys = await _workflowFixtrue.db.WorkflowOperationHistorys.FindAllAsync(m => OperationIds.Contains(m.OperationId));
                foreach (var WorkflowOperationHistory in WorkflowOperationHistorys)
                {
                    // 3、删除流程操作历史模型【真实删除】
                    await _workflowFixtrue.db.WorkflowOperationHistorys.DeleteAsync(WorkflowOperationHistory);
                }
                tran.Commit();
                return true;
            }
        }
    }
}