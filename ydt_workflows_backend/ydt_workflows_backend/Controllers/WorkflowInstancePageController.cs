using Microsoft.AspNetCore.Mvc;
using ydt_workflows_backend.CommonControllers;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Services;

namespace ydt_workflows_backend.Controllers
{
    /// <summary>
    /// 【流程发起】流程实例模型【根据流程运行流程】控制器
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class WorkflowInstancePageController : CommonController<WorkflowInstancePageController>
    {
        /// <summary>
        /// 流程实例模型【根据流程运行流程】模型Service
        /// </summary>
        private IWorkflowInstanceService _WorkflowInstanceService;
        private IWorkflowService _WorkflowService;
        public WorkflowInstancePageController(ILogger<WorkflowInstancePageController> logger,
                                IWorkflowInstanceService WorkflowInstanceService,
                                IWorkflowService workflowService) :
            base(logger)
        {
            _WorkflowInstanceService = WorkflowInstanceService;
            _WorkflowService = workflowService;
        }

        /// <summary>
        /// 1、流程分类+工作流查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("WorkflowAndWorkflowCategory")]
        public async Task<WorkflowAndWorkflowCategoryDto> WorkflowAndWorkflowCategoryGetListAsync()
        {
            return await _WorkflowInstanceService.WorkflowAndWorkflowCategoryGetListAsync();
        }


        /// <summary>
        /// 2、根据分类查询工作流
        /// </summary>
        /// <returns></returns>
        [HttpGet("Workflow/{CategoryId}")]
        public async Task<List<WorkflowDto>> WorkflowGetListByCategoryIdAsync(string CategoryId)
        {
            return await _WorkflowService.WorkflowGetListByCategoryIdAsync(CategoryId);
        }


        /// <summary>
        /// 3、打开流程【设计好的流程】
        /// </summary>
        /// <returns></returns>
        [HttpGet("Workflow/Open")]
        public async Task<WorkflowOpenDto> WorkflowOpenAsync(string flowId)
        {
            return await _WorkflowInstanceService.WorkflowOpenAsync(flowId);
        }

        /// 4、创建流程实例-保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> WorkflowInstanceCreateAsync(WorkflowInstanceCreateDto WorkflowInstanceCreateDto)
        {
            return await _WorkflowInstanceService.WorkflowInstanceCreateAsync(WorkflowInstanceCreateDto);
        }

        /// 5、查看流程图
        /// </summary>
        /// <returns></returns>
        [HttpGet("Workflow/Image")]
        public async Task<WorkflowImageDto> WorkflowImageAsync(string flowId)
        {
            return await _WorkflowInstanceService.WorkflowImageAsync(flowId);
        }


        /// 6、流程实例执行-提交【触发开始节点】
        /// </summary>
        /// <returns></returns>
        [HttpGet("Process/Submit")]
        public async Task<bool> WorkflowInstanceProcessSubmitAsync(WorkflowInstanceProcessSubmitDto
                                        workflowInstanceProcessSubmitDto)
        {
            return await _WorkflowInstanceService.WorkflowInstanceProcessSubmitAsync(workflowInstanceProcessSubmitDto);
        }

        /// 7、流程实例执行-同意【触发审核节点】
        /// </summary>
        /// <returns></returns>
        [HttpGet("Process/Agree")]
        public async Task<bool> WorkflowInstanceProcessAgreeAsync(
                                        WorkflowInstanceProcessAgreeDto
                                        workflowInstanceProcessAgreeDto)
        {
            return await _WorkflowInstanceService.WorkflowInstanceProcessAgreeAsync(workflowInstanceProcessAgreeDto);
        }

        /// 8、流程实例执行-不同意【流程直接终止】
        /// </summary>
        /// <returns></returns>
        [HttpPost("Process/Deprecate")]
        public async Task<bool> WorkflowInstanceProcessDeprecateAsync(
                                        WorkflowInstanceProcessDeprecateDto
                                        workflowInstanceProcessDeprecateDto)
        {
            return await _WorkflowInstanceService.WorkflowInstanceProcessDeprecateAsync(workflowInstanceProcessDeprecateDto);
        }

        /// 9、流程实例执行-退回【退回到某一个节点】
        /// </summary>
        /// <returns></returns>
        [HttpPost("Process/Back")]
        public async Task<bool> WorkflowInstanceProcessBackAsync(
                                        WorkflowInstanceProcessBackDto
                                        workflowInstanceProcessBackDto)
        {
            return await _WorkflowInstanceService.WorkflowInstanceProcessBackAsync(workflowInstanceProcessBackDto);
        }
        /// 10、流程实例执行-再次提交【重新开始执行】
        /// </summary>
        /// <returns></returns>
        [HttpPost("Process/ReSubmit")]
        public async Task<bool> WorkflowInstanceProcessReSubmitAsync(
                                        WorkflowInstanceProcessSubmitDto
                                        workflowInstanceProcessSubmitDto)
        {
            return await _WorkflowInstanceService.WorkflowInstanceProcessReSubmitAsync(workflowInstanceProcessSubmitDto);
        }

        /// 11、流程实例执行-委托【给系统其他人】
        /// </summary>
        /// <returns></returns>
        [HttpPost("Process/Assign")]
        public async Task<bool> WorkflowInstanceProcessAssignAsync(
                                        WorkflowInstanceProcessAssignDto
                                        workflowInstanceProcessAssignDto)
        {
            return await _WorkflowInstanceService.WorkflowInstanceProcessAssignAsync(workflowInstanceProcessAssignDto);
        }
        /// 12、流程实例执行-审批意见【查询审批记录】
        /// </summary>
        /// <returns></returns>
        [HttpPost("Process/Approval")]
        public async Task<List<WorkflowOperationHistoryDto>> WorkflowInstanceProcessApprovalAsync(
                                        WorkflowInstanceProcessApprovalDto
                                        workflowInstanceProcessApprovalDto)
        {
            return await _WorkflowInstanceService.WorkflowInstanceProcessApprovalAsync(workflowInstanceProcessApprovalDto);
        }

        /// 13、流程实例执行-撤回【删除工作流实例信息】
        /// </summary>
        /// <returns></returns>
        [HttpPost("Process/Withdraw")]
        public async Task<bool> WorkflowInstanceProcessWithdrawAsync(
                                        WorkflowInstanceProcessWithdrawDto
                                        workflowInstanceProcessWithdrawDto)
        {
            return await _WorkflowInstanceService.WorkflowInstanceProcessWithdrawAsync(workflowInstanceProcessWithdrawDto);
        }

        /// 14、流程实例执行-催办【提醒审核人快速执行】
        /// </summary>
        /// <returns></returns>
        [HttpPost("Process/Urge")]
        public async Task<bool> WorkflowInstanceProcessUrgeAsync(
                                        WorkflowInstanceProcessUrgeDto
                                        workflowInstanceProcessUrgeDto)
        {
            return await _WorkflowInstanceService.WorkflowInstanceProcessUrgeAsync(workflowInstanceProcessUrgeDto);
        }
    }
}