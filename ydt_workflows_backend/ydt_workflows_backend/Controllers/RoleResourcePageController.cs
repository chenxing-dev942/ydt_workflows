using Microsoft.AspNetCore.Mvc;
using ydt_workflows_backend.CommonControllers;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Services;

namespace ydt_workflows_backend.Controllers
{
    /// <summary>
    /// 角色资源关联模型控制器
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class RoleResourcePageController : CommonController<RoleResourcePageController>
    {
        /// <summary>
        /// 角色资源关联模型模型Service
        /// </summary>
        private IRoleResourceService _RoleResourceService;   

        public RoleResourcePageController(ILogger<RoleResourcePageController> logger,
                                IRoleResourceService RoleResourceService) : 
            base(logger)
        {
            _RoleResourceService = RoleResourceService;
        }

        /// <summary>
        /// 1、角色资源关联模型创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> RoleResourceCreateAsync(RoleResourceCreateDto RoleResourceCreateDto)
        {
            return await _RoleResourceService.RoleResourceCreateAsync(RoleResourceCreateDto);
        }

        /// <summary>
        /// 2、角色资源关联模型集合查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<RoleResourceDto>> RoleResourceGetListAsync([FromQuery]RoleResourceGetListDto RoleResourceGetListDto)
        {
            return await _RoleResourceService.RoleResourceGetListAsync(RoleResourceGetListDto);
        }

        /// <summary>
        /// 2.1、角色资源关联模型集合分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<RoleResourcePageDto> RoleResourceGetListPageAsync([FromQuery]RoleResourceGetListPageDto RoleResourceGetListPageDto)
        {
            return  await _RoleResourceService.RoleResourceGetListPageAsync(RoleResourceGetListPageDto);
        }
        /// <summary>
        /// 3、角色资源关联模型查询【根据角色资源关联模型Id查询】
        /// </summary>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<RoleResourceDto> RoleResourceGetAsync(long Id)
        {
            return await _RoleResourceService.RoleResourceGetAsync(Id);
        }
        /// <summary>
        /// 4、角色资源关联模型更新
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> RoleResourceUpdateAsync(RoleResourceUpdateDto RoleResourceUpdateDto,long Id)
        {
            return await _RoleResourceService.RoleResourceUpdateAsync(RoleResourceUpdateDto, Id);
        }
        /// <summary>
        /// 5、角色资源关联模型删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> RoleResourceDeleteAsync(List<long> Ids)
        {
            return await _RoleResourceService.RoleResourceDeleteAsync(Ids) ;
        }
    }
}