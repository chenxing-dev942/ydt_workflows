using Microsoft.AspNetCore.Mvc;
using ydt_workflows_backend.CommonControllers;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Services;

namespace ydt_workflows_backend.Controllers
{
    /// <summary>
    /// 工作流模型控制器
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class WorkflowPageController : CommonController<WorkflowPageController>
    {
        /// <summary>
        /// 工作流模型模型Service
        /// </summary>
        private IWorkflowService _WorkflowService;   

        public WorkflowPageController(ILogger<WorkflowPageController> logger,
                                IWorkflowService WorkflowService) : 
            base(logger)
        {
            _WorkflowService = WorkflowService;
        }

        /// <summary>
        /// 1、工作流模型创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> WorkflowCreateAsync(WorkflowCreateDto WorkflowCreateDto)
        {
            return await _WorkflowService.WorkflowCreateAsync(WorkflowCreateDto);
        }

        /// <summary>
        /// 2、工作流模型集合查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<WorkflowDto>> WorkflowGetListAsync([FromQuery]WorkflowGetListDto WorkflowGetListDto)
        {
            return await _WorkflowService.WorkflowGetListAsync(WorkflowGetListDto);
        }

        /// <summary>
        /// 2.1、工作流模型集合分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<WorkflowPageDto> WorkflowGetListPageAsync([FromQuery]WorkflowGetListPageDto WorkflowGetListPageDto)
        {
            return  await _WorkflowService.WorkflowGetListPageAsync(WorkflowGetListPageDto);
        }
        /// <summary>
        /// 3、工作流模型查询【根据工作流模型Id查询】
        /// </summary>
        /// <returns></returns>
        [HttpGet("{FlowId}")]
        public async Task<WorkflowDto> WorkflowGetAsync(string FlowId)
        {
            return await _WorkflowService.WorkflowGetAsync(FlowId);
        }
        /// <summary>
        /// 4、工作流模型更新
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> WorkflowUpdateAsync(WorkflowUpdateDto WorkflowUpdateDto,string FlowId)
        {
            return await _WorkflowService.WorkflowUpdateAsync(WorkflowUpdateDto, FlowId);
        }
        /// <summary>
        /// 5、工作流模型删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> WorkflowDeleteAsync(List<string> FlowIds)
        {
            return await _WorkflowService.WorkflowDeleteAsync(FlowIds) ;
        }
    }
}