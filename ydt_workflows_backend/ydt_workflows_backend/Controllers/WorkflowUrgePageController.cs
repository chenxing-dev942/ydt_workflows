using Microsoft.AspNetCore.Mvc;
using ydt_workflows_backend.CommonControllers;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Services;

namespace ydt_workflows_backend.Controllers
{
    /// <summary>
    /// 催办记录模型控制器
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class WorkflowUrgePageController : CommonController<WorkflowUrgePageController>
    {
        /// <summary>
        /// 催办记录模型模型Service
        /// </summary>
        private IWorkflowUrgeService _WorkflowUrgeService;   

        public WorkflowUrgePageController(ILogger<WorkflowUrgePageController> logger,
                                IWorkflowUrgeService WorkflowUrgeService) : 
            base(logger)
        {
            _WorkflowUrgeService = WorkflowUrgeService;
        }

        /// <summary>
        /// 1、催办记录模型创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> WorkflowUrgeCreateAsync(WorkflowUrgeCreateDto WorkflowUrgeCreateDto)
        {
            return await _WorkflowUrgeService.WorkflowUrgeCreateAsync(WorkflowUrgeCreateDto);
        }

        /// <summary>
        /// 2、催办记录模型集合查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<WorkflowUrgeDto>> WorkflowUrgeGetListAsync([FromQuery]WorkflowUrgeGetListDto WorkflowUrgeGetListDto)
        {
            return await _WorkflowUrgeService.WorkflowUrgeGetListAsync(WorkflowUrgeGetListDto);
        }

        /// <summary>
        /// 2.1、催办记录模型集合分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<WorkflowUrgePageDto> WorkflowUrgeGetListPageAsync([FromQuery]WorkflowUrgeGetListPageDto WorkflowUrgeGetListPageDto)
        {
            return  await _WorkflowUrgeService.WorkflowUrgeGetListPageAsync(WorkflowUrgeGetListPageDto);
        }
        /// <summary>
        /// 3、催办记录模型查询【根据催办记录模型Id查询】
        /// </summary>
        /// <returns></returns>
        [HttpGet("{UrgeId}")]
        public async Task<WorkflowUrgeDto> WorkflowUrgeGetAsync(string UrgeId)
        {
            return await _WorkflowUrgeService.WorkflowUrgeGetAsync(UrgeId);
        }
        /// <summary>
        /// 4、催办记录模型更新
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> WorkflowUrgeUpdateAsync(WorkflowUrgeUpdateDto WorkflowUrgeUpdateDto,string UrgeId)
        {
            return await _WorkflowUrgeService.WorkflowUrgeUpdateAsync(WorkflowUrgeUpdateDto, UrgeId);
        }
        /// <summary>
        /// 5、催办记录模型删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> WorkflowUrgeDeleteAsync(List<string> UrgeIds)
        {
            return await _WorkflowUrgeService.WorkflowUrgeDeleteAsync(UrgeIds) ;
        }
    }
}