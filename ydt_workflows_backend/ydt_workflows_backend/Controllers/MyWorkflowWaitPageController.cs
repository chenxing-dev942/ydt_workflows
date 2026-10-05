using Microsoft.AspNetCore.Mvc;
using ydt_workflows_backend.CommonControllers;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Dtos.MyWorkflowWait;
using ydt_workflows_backend.Services;

namespace ydt_workflows_backend.Controllers
{
    /// <summary>
    /// 工作流模型控制器【我的待办】
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class MyWorkflowWaitPageController : CommonController<MyWorkflowWaitPageController>
    {
        /// <summary>
        /// 工作流模型模型Service
        /// </summary>
        private IWorkflowInstanceService _WorkflowInstanceService;

        public MyWorkflowWaitPageController(
                                ILogger<MyWorkflowWaitPageController> logger,
                                IWorkflowInstanceService WorkflowInstanceService) :
            base(logger)
        {
            _WorkflowInstanceService = WorkflowInstanceService;
        }

        /// <summary>
        /// 1、待办数据分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<MyWorkflowWaitPageDto> MyWorkflowWaitGetListPageAsync(
            [FromQuery] MyWorkflowWaitGetListPageDto myWorkflowWaitGetListPageDto)
        {
            return await _WorkflowInstanceService.MyWorkflowWaitGetListPageAsync(
                myWorkflowWaitGetListPageDto);
        }
    }
}