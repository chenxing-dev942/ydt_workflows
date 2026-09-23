using Microsoft.AspNetCore.Mvc;
using ydt_workflows_backend.CommonControllers;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Services;

namespace ydt_workflows_backend.Controllers
{
    /// <summary>
    /// 流程操作历史模型控制器
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class WorkflowOperationHistoryPageController : CommonController<WorkflowOperationHistoryPageController>
    {
        /// <summary>
        /// 流程操作历史模型模型Service
        /// </summary>
        private IWorkflowOperationHistoryService _WorkflowOperationHistoryService;   

        public WorkflowOperationHistoryPageController(ILogger<WorkflowOperationHistoryPageController> logger,
                                IWorkflowOperationHistoryService WorkflowOperationHistoryService) : 
            base(logger)
        {
            _WorkflowOperationHistoryService = WorkflowOperationHistoryService;
        }

        /// <summary>
        /// 1、流程操作历史模型创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> WorkflowOperationHistoryCreateAsync(WorkflowOperationHistoryCreateDto WorkflowOperationHistoryCreateDto)
        {
            return await _WorkflowOperationHistoryService.WorkflowOperationHistoryCreateAsync(WorkflowOperationHistoryCreateDto);
        }

        /// <summary>
        /// 2、流程操作历史模型集合查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<WorkflowOperationHistoryDto>> WorkflowOperationHistoryGetListAsync([FromQuery]WorkflowOperationHistoryGetListDto WorkflowOperationHistoryGetListDto)
        {
            return await _WorkflowOperationHistoryService.WorkflowOperationHistoryGetListAsync(WorkflowOperationHistoryGetListDto);
        }

        /// <summary>
        /// 2.1、流程操作历史模型集合分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<WorkflowOperationHistoryPageDto> WorkflowOperationHistoryGetListPageAsync([FromQuery]WorkflowOperationHistoryGetListPageDto WorkflowOperationHistoryGetListPageDto)
        {
            return  await _WorkflowOperationHistoryService.WorkflowOperationHistoryGetListPageAsync(WorkflowOperationHistoryGetListPageDto);
        }
        /// <summary>
        /// 3、流程操作历史模型查询【根据流程操作历史模型Id查询】
        /// </summary>
        /// <returns></returns>
        [HttpGet("{OperationId}")]
        public async Task<WorkflowOperationHistoryDto> WorkflowOperationHistoryGetAsync(string OperationId)
        {
            return await _WorkflowOperationHistoryService.WorkflowOperationHistoryGetAsync(OperationId);
        }
        /// <summary>
        /// 4、流程操作历史模型更新
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> WorkflowOperationHistoryUpdateAsync(WorkflowOperationHistoryUpdateDto WorkflowOperationHistoryUpdateDto,string OperationId)
        {
            return await _WorkflowOperationHistoryService.WorkflowOperationHistoryUpdateAsync(WorkflowOperationHistoryUpdateDto, OperationId);
        }
        /// <summary>
        /// 5、流程操作历史模型删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> WorkflowOperationHistoryDeleteAsync(List<string> OperationIds)
        {
            return await _WorkflowOperationHistoryService.WorkflowOperationHistoryDeleteAsync(OperationIds) ;
        }
    }
}