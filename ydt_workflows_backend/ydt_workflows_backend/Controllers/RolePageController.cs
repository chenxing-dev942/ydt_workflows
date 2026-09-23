using Microsoft.AspNetCore.Mvc;
using ydt_workflows_backend.CommonControllers;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Services;

namespace ydt_workflows_backend.Controllers
{
    /// <summary>
    /// 角色模型控制器
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class RolePageController : CommonController<RolePageController>
    {
        /// <summary>
        /// 角色模型模型Service
        /// </summary>
        private IRoleService _RoleService;   

        public RolePageController(ILogger<RolePageController> logger,
                                IRoleService RoleService) : 
            base(logger)
        {
            _RoleService = RoleService;
        }

        /// <summary>
        /// 1、角色模型创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> RoleCreateAsync(RoleCreateDto RoleCreateDto)
        {
            return await _RoleService.RoleCreateAsync(RoleCreateDto);
        }

        /// <summary>
        /// 2、角色模型集合查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<RoleDto>> RoleGetListAsync([FromQuery]RoleGetListDto RoleGetListDto)
        {
            return await _RoleService.RoleGetListAsync(RoleGetListDto);
        }

        /// <summary>
        /// 2.1、角色模型集合分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<RolePageDto> RoleGetListPageAsync([FromQuery]RoleGetListPageDto RoleGetListPageDto)
        {
            return  await _RoleService.RoleGetListPageAsync(RoleGetListPageDto);
        }
        /// <summary>
        /// 3、角色模型查询【根据角色模型Id查询】
        /// </summary>
        /// <returns></returns>
        [HttpGet("{RoleId}")]
        public async Task<RoleDto> RoleGetAsync(long RoleId)
        {
            return await _RoleService.RoleGetAsync(RoleId);
        }
        /// <summary>
        /// 4、角色模型更新
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> RoleUpdateAsync(RoleUpdateDto RoleUpdateDto,long RoleId)
        {
            return await _RoleService.RoleUpdateAsync(RoleUpdateDto, RoleId);
        }
        /// <summary>
        /// 5、角色模型删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> RoleDeleteAsync(List<long> RoleIds)
        {
            return await _RoleService.RoleDeleteAsync(RoleIds) ;
        }
    }
}