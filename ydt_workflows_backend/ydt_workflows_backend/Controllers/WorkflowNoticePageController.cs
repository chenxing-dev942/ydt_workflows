using Microsoft.AspNetCore.Mvc;
using ydt_workflows_backend.CommonControllers;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Services;

namespace ydt_workflows_backend.Controllers
{
    /// <summary>
    /// 流程通知节点模型控制器
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class WorkflowNoticePageController : CommonController<WorkflowNoticePageController>
    {
        /// <summary>
        /// 流程通知节点模型模型Service
        /// </summary>
        private IWorkflowNoticeService _WorkflowNoticeService;   

        public WorkflowNoticePageController(ILogger<WorkflowNoticePageController> logger,
                                IWorkflowNoticeService WorkflowNoticeService) : 
            base(logger)
        {
            _WorkflowNoticeService = WorkflowNoticeService;
        }

        /// <summary>
        /// 1、流程通知节点模型创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> WorkflowNoticeCreateAsync(WorkflowNoticeCreateDto WorkflowNoticeCreateDto)
        {
            return await _WorkflowNoticeService.WorkflowNoticeCreateAsync(WorkflowNoticeCreateDto);
        }

        /// <summary>
        /// 2、流程通知节点模型集合查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<WorkflowNoticeDto>> WorkflowNoticeGetListAsync([FromQuery]WorkflowNoticeGetListDto WorkflowNoticeGetListDto)
        {
            return await _WorkflowNoticeService.WorkflowNoticeGetListAsync(WorkflowNoticeGetListDto);
        }

        /// <summary>
        /// 2.1、流程通知节点模型集合分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<WorkflowNoticePageDto> WorkflowNoticeGetListPageAsync([FromQuery]WorkflowNoticeGetListPageDto WorkflowNoticeGetListPageDto)
        {
            return  await _WorkflowNoticeService.WorkflowNoticeGetListPageAsync(WorkflowNoticeGetListPageDto);
        }
        /// <summary>
        /// 3、流程通知节点模型查询【根据流程通知节点模型Id查询】
        /// </summary>
        /// <returns></returns>
        [HttpGet("{NoticeId}")]
        public async Task<WorkflowNoticeDto> WorkflowNoticeGetAsync(string NoticeId)
        {
            return await _WorkflowNoticeService.WorkflowNoticeGetAsync(NoticeId);
        }
        /// <summary>
        /// 4、流程通知节点模型更新
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> WorkflowNoticeUpdateAsync(WorkflowNoticeUpdateDto WorkflowNoticeUpdateDto,string NoticeId)
        {
            return await _WorkflowNoticeService.WorkflowNoticeUpdateAsync(WorkflowNoticeUpdateDto, NoticeId);
        }
        /// <summary>
        /// 5、流程通知节点模型删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> WorkflowNoticeDeleteAsync(List<string> NoticeIds)
        {
            return await _WorkflowNoticeService.WorkflowNoticeDeleteAsync(NoticeIds) ;
        }
    }
}