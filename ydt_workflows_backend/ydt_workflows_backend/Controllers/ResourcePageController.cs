using Microsoft.AspNetCore.Mvc;
using ydt_workflows_backend.CommonControllers;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Services;

namespace ydt_workflows_backend.Controllers
{
    /// <summary>
    /// 资源【菜单】模型控制器
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ResourcePageController : CommonController<ResourcePageController>
    {
        /// <summary>
        /// 资源【菜单】模型模型Service
        /// </summary>
        private IResourceService _ResourceService;   

        public ResourcePageController(ILogger<ResourcePageController> logger,
                                IResourceService ResourceService) : 
            base(logger)
        {
            _ResourceService = ResourceService;
        }

        /// <summary>
        /// 1、资源【菜单】模型创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> ResourceCreateAsync(ResourceCreateDto ResourceCreateDto)
        {
            return await _ResourceService.ResourceCreateAsync(ResourceCreateDto);
        }

        /// <summary>
        /// 2、资源【菜单】模型集合查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<ResourceDto>> ResourceGetListAsync([FromQuery]ResourceGetListDto ResourceGetListDto)
        {
            return await _ResourceService.ResourceGetListAsync(ResourceGetListDto);
        }

        /// <summary>
        /// 2.1、资源【菜单】模型集合分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<ResourcePageDto> ResourceGetListPageAsync([FromQuery]ResourceGetListPageDto ResourceGetListPageDto)
        {
            return  await _ResourceService.ResourceGetListPageAsync(ResourceGetListPageDto);
        }
        /// <summary>
        /// 3、资源【菜单】模型查询【根据资源【菜单】模型Id查询】
        /// </summary>
        /// <returns></returns>
        [HttpGet("{ResourceId}")]
        public async Task<ResourceDto> ResourceGetAsync(long ResourceId)
        {
            return await _ResourceService.ResourceGetAsync(ResourceId);
        }
        /// <summary>
        /// 4、资源【菜单】模型更新
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> ResourceUpdateAsync(ResourceUpdateDto ResourceUpdateDto,long ResourceId)
        {
            return await _ResourceService.ResourceUpdateAsync(ResourceUpdateDto, ResourceId);
        }
        /// <summary>
        /// 5、资源【菜单】模型删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> ResourceDeleteAsync(List<long> ResourceIds)
        {
            return await _ResourceService.ResourceDeleteAsync(ResourceIds) ;
        }
    }
}