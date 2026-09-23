using Microsoft.AspNetCore.Mvc;
using ydt_workflows_backend.CommonControllers;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Services;

namespace ydt_workflows_backend.Controllers
{
    /// <summary>
    /// 流程表单模型控制器
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class WorkflowFormPageController : CommonController<WorkflowFormPageController>
    {
        /// <summary>
        /// 流程表单模型模型Service
        /// </summary>
        private IWorkflowFormService _WorkflowFormService;   

        public WorkflowFormPageController(ILogger<WorkflowFormPageController> logger,
                                IWorkflowFormService WorkflowFormService) : 
            base(logger)
        {
            _WorkflowFormService = WorkflowFormService;
        }

        /// <summary>
        /// 1、流程表单模型创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> WorkflowFormCreateAsync(WorkflowFormCreateDto WorkflowFormCreateDto)
        {
            return await _WorkflowFormService.WorkflowFormCreateAsync(WorkflowFormCreateDto);
        }

        /// <summary>
        /// 2、流程表单模型集合查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<WorkflowFormDto>> WorkflowFormGetListAsync([FromQuery]WorkflowFormGetListDto WorkflowFormGetListDto)
        {
            return await _WorkflowFormService.WorkflowFormGetListAsync(WorkflowFormGetListDto);
        }

        /// <summary>
        /// 2.1、流程表单模型集合分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<WorkflowFormPageDto> WorkflowFormGetListPageAsync([FromQuery]WorkflowFormGetListPageDto WorkflowFormGetListPageDto)
        {
            return  await _WorkflowFormService.WorkflowFormGetListPageAsync(WorkflowFormGetListPageDto);
        }
        /// <summary>
        /// 3、流程表单模型查询【根据流程表单模型Id查询】
        /// </summary>
        /// <returns></returns>
        [HttpGet("{FormId}")]
        public async Task<WorkflowFormDto> WorkflowFormGetAsync(string FormId)
        {
            return await _WorkflowFormService.WorkflowFormGetAsync(FormId);
        }
        /// <summary>
        /// 4、流程表单模型更新
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> WorkflowFormUpdateAsync(WorkflowFormUpdateDto WorkflowFormUpdateDto,string FormId)
        {
            return await _WorkflowFormService.WorkflowFormUpdateAsync(WorkflowFormUpdateDto, FormId);
        }
        /// <summary>
        /// 5、流程表单模型删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> WorkflowFormDeleteAsync(List<string> FormIds)
        {
            return await _WorkflowFormService.WorkflowFormDeleteAsync(FormIds) ;
        }
    }
}