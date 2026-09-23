using Microsoft.AspNetCore.Mvc;
using ydt_workflows_backend.CommonControllers;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Services;

namespace ydt_workflows_backend.Controllers
{
    /// <summary>
    /// 工作流获取权限系统数据模型控制器
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class WorkflowsqlPageController : CommonController<WorkflowsqlPageController>
    {
        /// <summary>
        /// 工作流获取权限系统数据模型模型Service
        /// </summary>
        private IWorkflowsqlService _WorkflowsqlService;   

        public WorkflowsqlPageController(ILogger<WorkflowsqlPageController> logger,
                                IWorkflowsqlService WorkflowsqlService) : 
            base(logger)
        {
            _WorkflowsqlService = WorkflowsqlService;
        }

        /// <summary>
        /// 1、工作流获取权限系统数据模型创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> WorkflowsqlCreateAsync(WorkflowsqlCreateDto WorkflowsqlCreateDto)
        {
            return await _WorkflowsqlService.WorkflowsqlCreateAsync(WorkflowsqlCreateDto);
        }

        /// <summary>
        /// 2、工作流获取权限系统数据模型集合查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<WorkflowsqlDto>> WorkflowsqlGetListAsync([FromQuery]WorkflowsqlGetListDto WorkflowsqlGetListDto)
        {
            return await _WorkflowsqlService.WorkflowsqlGetListAsync(WorkflowsqlGetListDto);
        }

        /// <summary>
        /// 2.1、工作流获取权限系统数据模型集合分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<WorkflowsqlPageDto> WorkflowsqlGetListPageAsync([FromQuery]WorkflowsqlGetListPageDto WorkflowsqlGetListPageDto)
        {
            return  await _WorkflowsqlService.WorkflowsqlGetListPageAsync(WorkflowsqlGetListPageDto);
        }
        /// <summary>
        /// 3、工作流获取权限系统数据模型查询【根据工作流获取权限系统数据模型Id查询】
        /// </summary>
        /// <returns></returns>
        [HttpGet("{Name}")]
        public async Task<WorkflowsqlDto> WorkflowsqlGetAsync(string Name)
        {
            return await _WorkflowsqlService.WorkflowsqlGetAsync(Name);
        }
        /// <summary>
        /// 4、工作流获取权限系统数据模型更新
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> WorkflowsqlUpdateAsync(WorkflowsqlUpdateDto WorkflowsqlUpdateDto,string Name)
        {
            return await _WorkflowsqlService.WorkflowsqlUpdateAsync(WorkflowsqlUpdateDto, Name);
        }
        /// <summary>
        /// 5、工作流获取权限系统数据模型删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> WorkflowsqlDeleteAsync(List<string> Names)
        {
            return await _WorkflowsqlService.WorkflowsqlDeleteAsync(Names) ;
        }
    }
}