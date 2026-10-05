using Microsoft.AspNetCore.Mvc;
using ydt_workflows_backend.CommonControllers;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Services;

namespace ydt_workflows_backend.Controllers
{
    /// <summary>
    /// 工作流模型控制器【我的流程】
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class MyWorkflowPageController : CommonController<MyWorkflowPageController>
    {
        /// <summary>
        /// 工作流模型模型Service
        /// </summary>
        private IWorkflowService _WorkflowService;
        private IWorkflowInstanceService _WorkflowInstanceService;// 依赖注入

        public MyWorkflowPageController(ILogger<MyWorkflowPageController> logger,
                                IWorkflowService WorkflowService,
                                IWorkflowInstanceService workflowInstanceService) :
            base(logger)
        {
            _WorkflowService = WorkflowService;
            _WorkflowInstanceService = workflowInstanceService;
        }

        /// <summary>
        ///1、我的流程-工作流实例集合查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<WorkflowInstanceDto>> WorkflowInstanceGetListAsync([FromQuery] WorkflowInstanceGetListDto WorkflowInstanceGetListDto)
        {
            return await _WorkflowInstanceService.WorkflowInstanceGetListAsync(WorkflowInstanceGetListDto);
        }
        /// <summary>
        /// 2、我的流程-工作流实例分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<MyWorkflowPageDto> MyWorkflowGetListPageAsync([FromQuery] MyWorkflowGetListPageDto myWorkflowGetListPageDto)
        {
            return await _WorkflowInstanceService.WorkflowInstanceGetListPageAsync(myWorkflowGetListPageDto);
        }

        /// <summary>
        /// 3、我的流程-工作流实例查看-根据Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{InstanceId}")]
        public async Task<WorkflowInstanceGetResultDto> WorkflowInstanceGetAsync(string InstanceId)
        {
            return await _WorkflowInstanceService.WorkflowInstanceGetAsync(InstanceId);
        }

       
        /// <summary>
        /// 4、我的流程-工作流实例删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> WorkflowInstanceDeleteAsync(List<string> InstanceIds)
        {
            return await _WorkflowInstanceService.WorkflowInstanceDeleteAsync(InstanceIds);
        }


        /// <summary>
        /// 6、打开流程【设计好的流程】
        /// </summary>
        /// <returns></returns>
        [HttpGet("Workflow/Open")]
        public async Task<WorkflowOpenDto> WorkflowOpenAsync(string flowId)
        {
            return await _WorkflowInstanceService.WorkflowOpenAsync(flowId);
        }

        /// <summary>
        /// 5、我的流程-工作流实例更新
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> WorkflowInstanceUpdateAsync(WorkflowInstanceUpdateDto WorkflowInstanceUpdateDto, string InstanceId)
        {
            return await _WorkflowInstanceService.WorkflowInstanceUpdateAsync(WorkflowInstanceUpdateDto, InstanceId);
        }

        /// 6、查看流程图
        /// </summary>
        /// <returns></returns>
        [HttpGet("Workflow/Image")]
        public async Task<WorkflowImageDto> WorkflowImageAsync(string flowId)
        {
            return await _WorkflowInstanceService.WorkflowImageAsync(flowId);
        }

        /// 7、修改流程实例【保存】
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> WorkflowInstanceUpateAsync(WorkflowInstanceUpdateDto workflowInstanceUpdateDto)
        {
            return await _WorkflowInstanceService.WorkflowInstanceUpdateAsync(workflowInstanceUpdateDto,
                workflowInstanceUpdateDto.InstanceId);
        }
    }
}