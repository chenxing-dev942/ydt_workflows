using Microsoft.AspNetCore.Mvc;
using ydt_workflows_backend.CommonControllers;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Services;

namespace ydt_workflows_backend.Controllers
{
    /// <summary>
    /// 子系统模型控制器
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class SystemsPageController : CommonController<SystemsPageController>
    {
        /// <summary>
        /// 子系统模型模型Service
        /// </summary>
        private ISystemsService _SystemsService;   

        public SystemsPageController(ILogger<SystemsPageController> logger,
                                ISystemsService SystemsService) : 
            base(logger)
        {
            _SystemsService = SystemsService;
        }

        /// <summary>
        /// 1、子系统模型创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> SystemsCreateAsync(SystemsCreateDto SystemsCreateDto)
        {
            return await _SystemsService.SystemsCreateAsync(SystemsCreateDto);
        }

        /// <summary>
        /// 2、子系统模型集合查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<SystemsDto>> SystemsGetListAsync([FromQuery]SystemsGetListDto SystemsGetListDto)
        {
            return await _SystemsService.SystemsGetListAsync(SystemsGetListDto);
        }

        /// <summary>
        /// 2.1、子系统模型集合分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<SystemsPageDto> SystemsGetListPageAsync([FromQuery]SystemsGetListPageDto SystemsGetListPageDto)
        {
            return  await _SystemsService.SystemsGetListPageAsync(SystemsGetListPageDto);
        }
        /// <summary>
        /// 3、子系统模型查询【根据子系统模型Id查询】
        /// </summary>
        /// <returns></returns>
        [HttpGet("{SystemId}")]
        public async Task<SystemsDto> SystemsGetAsync(long SystemId)
        {
            return await _SystemsService.SystemsGetAsync(SystemId);
        }
        /// <summary>
        /// 4、子系统模型更新
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> SystemsUpdateAsync(SystemsUpdateDto SystemsUpdateDto,long SystemId)
        {
            return await _SystemsService.SystemsUpdateAsync(SystemsUpdateDto, SystemId);
        }
        /// <summary>
        /// 5、子系统模型删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> SystemsDeleteAsync(List<long> SystemIds)
        {
            return await _SystemsService.SystemsDeleteAsync(SystemIds) ;
        }
    }
}