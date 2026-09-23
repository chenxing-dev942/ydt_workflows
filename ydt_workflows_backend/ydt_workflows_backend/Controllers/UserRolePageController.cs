using Microsoft.AspNetCore.Mvc;
using ydt_workflows_backend.CommonControllers;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Services;

namespace ydt_workflows_backend.Controllers
{
    /// <summary>
    /// 用户角色关联模型控制器
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class UserRolePageController : CommonController<UserRolePageController>
    {
        /// <summary>
        /// 用户角色关联模型模型Service
        /// </summary>
        private IUserRoleService _UserRoleService;   

        public UserRolePageController(ILogger<UserRolePageController> logger,
                                IUserRoleService UserRoleService) : 
            base(logger)
        {
            _UserRoleService = UserRoleService;
        }

        /// <summary>
        /// 1、用户角色关联模型创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UserRoleCreateAsync(UserRoleCreateDto UserRoleCreateDto)
        {
            return await _UserRoleService.UserRoleCreateAsync(UserRoleCreateDto);
        }

        /// <summary>
        /// 2、用户角色关联模型集合查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<UserRoleDto>> UserRoleGetListAsync([FromQuery]UserRoleGetListDto UserRoleGetListDto)
        {
            return await _UserRoleService.UserRoleGetListAsync(UserRoleGetListDto);
        }

        /// <summary>
        /// 2.1、用户角色关联模型集合分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<UserRolePageDto> UserRoleGetListPageAsync([FromQuery]UserRoleGetListPageDto UserRoleGetListPageDto)
        {
            return  await _UserRoleService.UserRoleGetListPageAsync(UserRoleGetListPageDto);
        }
        /// <summary>
        /// 3、用户角色关联模型查询【根据用户角色关联模型Id查询】
        /// </summary>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<UserRoleDto> UserRoleGetAsync(long Id)
        {
            return await _UserRoleService.UserRoleGetAsync(Id);
        }
        /// <summary>
        /// 4、用户角色关联模型更新
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> UserRoleUpdateAsync(UserRoleUpdateDto UserRoleUpdateDto,long Id)
        {
            return await _UserRoleService.UserRoleUpdateAsync(UserRoleUpdateDto, Id);
        }
        /// <summary>
        /// 5、用户角色关联模型删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> UserRoleDeleteAsync(List<long> Ids)
        {
            return await _UserRoleService.UserRoleDeleteAsync(Ids) ;
        }
    }
}