using Microsoft.AspNetCore.Mvc;
using ydt_workflows_backend.CommonControllers;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Services;

namespace ydt_workflows_backend.Controllers
{
    /// <summary>
    /// 流程实例模型【根据流程运行流程】控制器
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class WorkflowInstancePageController : CommonController<WorkflowInstancePageController>
    {
        /// <summary>
        /// 流程实例模型【根据流程运行流程】模型Service
        /// </summary>
        private IWorkflowInstanceService _WorkflowInstanceService;   

        public WorkflowInstancePageController(ILogger<WorkflowInstancePageController> logger,
                                IWorkflowInstanceService WorkflowInstanceService) : 
            base(logger)
        {
            _WorkflowInstanceService = WorkflowInstanceService;
        }

        /// <summary>
        /// 1、流程实例模型【根据流程运行流程】创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> WorkflowInstanceCreateAsync(WorkflowInstanceCreateDto WorkflowInstanceCreateDto)
        {
            return await _WorkflowInstanceService.WorkflowInstanceCreateAsync(WorkflowInstanceCreateDto);
        }

        /// <summary>
        /// 2、流程实例模型【根据流程运行流程】集合查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<WorkflowInstanceDto>> WorkflowInstanceGetListAsync([FromQuery]WorkflowInstanceGetListDto WorkflowInstanceGetListDto)
        {
            return await _WorkflowInstanceService.WorkflowInstanceGetListAsync(WorkflowInstanceGetListDto);
        }

        /// <summary>
        /// 2.1、流程实例模型【根据流程运行流程】集合分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<WorkflowInstancePageDto> WorkflowInstanceGetListPageAsync([FromQuery]WorkflowInstanceGetListPageDto WorkflowInstanceGetListPageDto)
        {
            return  await _WorkflowInstanceService.WorkflowInstanceGetListPageAsync(WorkflowInstanceGetListPageDto);
        }
        /// <summary>
        /// 3、流程实例模型【根据流程运行流程】查询【根据流程实例模型【根据流程运行流程】Id查询】
        /// </summary>
        /// <returns></returns>
        [HttpGet("{InstanceId}")]
        public async Task<WorkflowInstanceDto> WorkflowInstanceGetAsync(string InstanceId)
        {
            return await _WorkflowInstanceService.WorkflowInstanceGetAsync(InstanceId);
        }
        /// <summary>
        /// 4、流程实例模型【根据流程运行流程】更新
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> WorkflowInstanceUpdateAsync(WorkflowInstanceUpdateDto WorkflowInstanceUpdateDto,string InstanceId)
        {
            return await _WorkflowInstanceService.WorkflowInstanceUpdateAsync(WorkflowInstanceUpdateDto, InstanceId);
        }
        /// <summary>
        /// 5、流程实例模型【根据流程运行流程】删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> WorkflowInstanceDeleteAsync(List<string> InstanceIds)
        {
            return await _WorkflowInstanceService.WorkflowInstanceDeleteAsync(InstanceIds) ;
        }
    }
}