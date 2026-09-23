using Microsoft.AspNetCore.Mvc;
using ydt_workflows_backend.CommonControllers;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Services;

namespace ydt_workflows_backend.Controllers
{
    /// <summary>
    /// 流程实例表单关联模型控制器
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class WorkflowInstanceFormPageController : CommonController<WorkflowInstanceFormPageController>
    {
        /// <summary>
        /// 流程实例表单关联模型模型Service
        /// </summary>
        private IWorkflowInstanceFormService _WorkflowInstanceFormService;   

        public WorkflowInstanceFormPageController(ILogger<WorkflowInstanceFormPageController> logger,
                                IWorkflowInstanceFormService WorkflowInstanceFormService) : 
            base(logger)
        {
            _WorkflowInstanceFormService = WorkflowInstanceFormService;
        }

        /// <summary>
        /// 1、流程实例表单关联模型创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> WorkflowInstanceFormCreateAsync(WorkflowInstanceFormCreateDto WorkflowInstanceFormCreateDto)
        {
            return await _WorkflowInstanceFormService.WorkflowInstanceFormCreateAsync(WorkflowInstanceFormCreateDto);
        }

        /// <summary>
        /// 2、流程实例表单关联模型集合查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<WorkflowInstanceFormDto>> WorkflowInstanceFormGetListAsync([FromQuery]WorkflowInstanceFormGetListDto WorkflowInstanceFormGetListDto)
        {
            return await _WorkflowInstanceFormService.WorkflowInstanceFormGetListAsync(WorkflowInstanceFormGetListDto);
        }

        /// <summary>
        /// 2.1、流程实例表单关联模型集合分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<WorkflowInstanceFormPageDto> WorkflowInstanceFormGetListPageAsync([FromQuery]WorkflowInstanceFormGetListPageDto WorkflowInstanceFormGetListPageDto)
        {
            return  await _WorkflowInstanceFormService.WorkflowInstanceFormGetListPageAsync(WorkflowInstanceFormGetListPageDto);
        }
        /// <summary>
        /// 3、流程实例表单关联模型查询【根据流程实例表单关联模型Id查询】
        /// </summary>
        /// <returns></returns>
        [HttpGet("{InstanceFormId}")]
        public async Task<WorkflowInstanceFormDto> WorkflowInstanceFormGetAsync(string InstanceFormId)
        {
            return await _WorkflowInstanceFormService.WorkflowInstanceFormGetAsync(InstanceFormId);
        }
        /// <summary>
        /// 4、流程实例表单关联模型更新
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> WorkflowInstanceFormUpdateAsync(WorkflowInstanceFormUpdateDto WorkflowInstanceFormUpdateDto,string InstanceFormId)
        {
            return await _WorkflowInstanceFormService.WorkflowInstanceFormUpdateAsync(WorkflowInstanceFormUpdateDto, InstanceFormId);
        }
        /// <summary>
        /// 5、流程实例表单关联模型删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> WorkflowInstanceFormDeleteAsync(List<string> InstanceFormIds)
        {
            return await _WorkflowInstanceFormService.WorkflowInstanceFormDeleteAsync(InstanceFormIds) ;
        }
    }
}