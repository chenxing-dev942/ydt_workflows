using Microsoft.AspNetCore.Mvc;
using ydt_workflows_backend.CommonControllers;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Services;

namespace ydt_workflows_backend.Controllers
{
    /// <summary>
    /// 流程分类模型控制器
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class WorkflowCategoryPageController : CommonController<WorkflowCategoryPageController>
    {
        /// <summary>
        /// 流程分类模型模型Service
        /// </summary>
        private IWorkflowCategoryService _WorkflowCategoryService;   

        public WorkflowCategoryPageController(ILogger<WorkflowCategoryPageController> logger,
                                IWorkflowCategoryService WorkflowCategoryService) : 
            base(logger)
        {
            _WorkflowCategoryService = WorkflowCategoryService;
        }

        /// <summary>
        /// 1、流程分类模型创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> WorkflowCategoryCreateAsync(WorkflowCategoryCreateDto WorkflowCategoryCreateDto)
        {
            return await _WorkflowCategoryService.WorkflowCategoryCreateAsync(WorkflowCategoryCreateDto);
        }

        /// <summary>
        /// 2、流程分类模型集合查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<WorkflowCategoryDto>> WorkflowCategoryGetListAsync([FromQuery]WorkflowCategoryGetListDto WorkflowCategoryGetListDto)
        {
            return await _WorkflowCategoryService.WorkflowCategoryGetListAsync(WorkflowCategoryGetListDto);
        }

        /// <summary>
        /// 2.1、流程分类模型集合分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<WorkflowCategoryPageDto> WorkflowCategoryGetListPageAsync([FromQuery]WorkflowCategoryGetListPageDto WorkflowCategoryGetListPageDto)
        {
            return  await _WorkflowCategoryService.WorkflowCategoryGetListPageAsync(WorkflowCategoryGetListPageDto);
        }
        /// <summary>
        /// 3、流程分类模型查询【根据流程分类模型Id查询】
        /// </summary>
        /// <returns></returns>
        [HttpGet("{CategoryId}")]
        public async Task<WorkflowCategoryDto> WorkflowCategoryGetAsync(string CategoryId)
        {
            return await _WorkflowCategoryService.WorkflowCategoryGetAsync(CategoryId);
        }
        /// <summary>
        /// 4、流程分类模型更新
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> WorkflowCategoryUpdateAsync(WorkflowCategoryUpdateDto WorkflowCategoryUpdateDto,string CategoryId)
        {
            return await _WorkflowCategoryService.WorkflowCategoryUpdateAsync(WorkflowCategoryUpdateDto, CategoryId);
        }
        /// <summary>
        /// 5、流程分类模型删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> WorkflowCategoryDeleteAsync(List<string> CategoryIds)
        {
            return await _WorkflowCategoryService.WorkflowCategoryDeleteAsync(CategoryIds) ;
        }
    }
}