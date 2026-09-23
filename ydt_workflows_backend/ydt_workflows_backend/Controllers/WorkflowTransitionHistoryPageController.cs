using Microsoft.AspNetCore.Mvc;
using ydt_workflows_backend.CommonControllers;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Services;

namespace ydt_workflows_backend.Controllers
{
    /// <summary>
    /// 流程流转历史模型控制器
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class WorkflowTransitionHistoryPageController : CommonController<WorkflowTransitionHistoryPageController>
    {
        /// <summary>
        /// 流程流转历史模型模型Service
        /// </summary>
        private IWorkflowTransitionHistoryService _WorkflowTransitionHistoryService;   

        public WorkflowTransitionHistoryPageController(ILogger<WorkflowTransitionHistoryPageController> logger,
                                IWorkflowTransitionHistoryService WorkflowTransitionHistoryService) : 
            base(logger)
        {
            _WorkflowTransitionHistoryService = WorkflowTransitionHistoryService;
        }

        /// <summary>
        /// 1、流程流转历史模型创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> WorkflowTransitionHistoryCreateAsync(WorkflowTransitionHistoryCreateDto WorkflowTransitionHistoryCreateDto)
        {
            return await _WorkflowTransitionHistoryService.WorkflowTransitionHistoryCreateAsync(WorkflowTransitionHistoryCreateDto);
        }

        /// <summary>
        /// 2、流程流转历史模型集合查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<WorkflowTransitionHistoryDto>> WorkflowTransitionHistoryGetListAsync([FromQuery]WorkflowTransitionHistoryGetListDto WorkflowTransitionHistoryGetListDto)
        {
            return await _WorkflowTransitionHistoryService.WorkflowTransitionHistoryGetListAsync(WorkflowTransitionHistoryGetListDto);
        }

        /// <summary>
        /// 2.1、流程流转历史模型集合分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<WorkflowTransitionHistoryPageDto> WorkflowTransitionHistoryGetListPageAsync([FromQuery]WorkflowTransitionHistoryGetListPageDto WorkflowTransitionHistoryGetListPageDto)
        {
            return  await _WorkflowTransitionHistoryService.WorkflowTransitionHistoryGetListPageAsync(WorkflowTransitionHistoryGetListPageDto);
        }
        /// <summary>
        /// 3、流程流转历史模型查询【根据流程流转历史模型Id查询】
        /// </summary>
        /// <returns></returns>
        [HttpGet("{TransitionId}")]
        public async Task<WorkflowTransitionHistoryDto> WorkflowTransitionHistoryGetAsync(string TransitionId)
        {
            return await _WorkflowTransitionHistoryService.WorkflowTransitionHistoryGetAsync(TransitionId);
        }
        /// <summary>
        /// 4、流程流转历史模型更新
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> WorkflowTransitionHistoryUpdateAsync(WorkflowTransitionHistoryUpdateDto WorkflowTransitionHistoryUpdateDto,string TransitionId)
        {
            return await _WorkflowTransitionHistoryService.WorkflowTransitionHistoryUpdateAsync(WorkflowTransitionHistoryUpdateDto, TransitionId);
        }
        /// <summary>
        /// 5、流程流转历史模型删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> WorkflowTransitionHistoryDeleteAsync(List<string> TransitionIds)
        {
            return await _WorkflowTransitionHistoryService.WorkflowTransitionHistoryDeleteAsync(TransitionIds) ;
        }
    }
}