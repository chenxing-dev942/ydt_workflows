using Microsoft.AspNetCore.Mvc;
using ydt_workflows_backend.CommonControllers;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Services;

namespace ydt_workflows_backend.Controllers
{
    /// <summary>
    /// 流程委托模型控制器
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class WorkflowAssignPageController : CommonController<WorkflowAssignPageController>
    {
        /// <summary>
        /// 流程委托模型模型Service
        /// </summary>
        private IWorkflowAssignService _WorkflowAssignService;   

        public WorkflowAssignPageController(ILogger<WorkflowAssignPageController> logger,
                                IWorkflowAssignService WorkflowAssignService) : 
            base(logger)
        {
            _WorkflowAssignService = WorkflowAssignService;
        }

        /// <summary>
        /// 1、流程委托模型创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> WorkflowAssignCreateAsync(WorkflowAssignCreateDto WorkflowAssignCreateDto)
        {
            return await _WorkflowAssignService.WorkflowAssignCreateAsync(WorkflowAssignCreateDto);
        }

        /// <summary>
        /// 2、流程委托模型集合查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<WorkflowAssignDto>> WorkflowAssignGetListAsync([FromQuery]WorkflowAssignGetListDto WorkflowAssignGetListDto)
        {
            return await _WorkflowAssignService.WorkflowAssignGetListAsync(WorkflowAssignGetListDto);
        }

        /// <summary>
        /// 2.1、流程委托模型集合分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<WorkflowAssignPageDto> WorkflowAssignGetListPageAsync([FromQuery]WorkflowAssignGetListPageDto WorkflowAssignGetListPageDto)
        {
            return  await _WorkflowAssignService.WorkflowAssignGetListPageAsync(WorkflowAssignGetListPageDto);
        }
        /// <summary>
        /// 3、流程委托模型查询【根据流程委托模型Id查询】
        /// </summary>
        /// <returns></returns>
        [HttpGet("{AssignId}")]
        public async Task<WorkflowAssignDto> WorkflowAssignGetAsync(string AssignId)
        {
            return await _WorkflowAssignService.WorkflowAssignGetAsync(AssignId);
        }
        /// <summary>
        /// 4、流程委托模型更新
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> WorkflowAssignUpdateAsync(WorkflowAssignUpdateDto WorkflowAssignUpdateDto,string AssignId)
        {
            return await _WorkflowAssignService.WorkflowAssignUpdateAsync(WorkflowAssignUpdateDto, AssignId);
        }
        /// <summary>
        /// 5、流程委托模型删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> WorkflowAssignDeleteAsync(List<string> AssignIds)
        {
            return await _WorkflowAssignService.WorkflowAssignDeleteAsync(AssignIds) ;
        }
    }
}