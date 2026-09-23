using Microsoft.AspNetCore.Mvc;
using ydt_workflows_backend.CommonControllers;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Services;

namespace ydt_workflows_backend.Controllers
{
    /// <summary>
    /// 用户模型控制器
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class UserPageController : CommonController<UserPageController>
    {
        /// <summary>
        /// 用户模型模型Service
        /// </summary>
        private IUserService _UserService;   

        public UserPageController(ILogger<UserPageController> logger,
                                IUserService UserService) : 
            base(logger)
        {
            _UserService = UserService;
        }

        /// <summary>
        /// 1、用户模型创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UserCreateAsync(UserCreateDto UserCreateDto)
        {
            return await _UserService.UserCreateAsync(UserCreateDto);
        }

        /// <summary>
        /// 2、用户模型集合查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<UserDto>> UserGetListAsync([FromQuery]UserGetListDto UserGetListDto)
        {
            return await _UserService.UserGetListAsync(UserGetListDto);
        }

        /// <summary>
        /// 2.1、用户模型集合分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<UserPageDto> UserGetListPageAsync([FromQuery]UserGetListPageDto UserGetListPageDto)
        {
            return  await _UserService.UserGetListPageAsync(UserGetListPageDto);
        }
        /// <summary>
        /// 3、用户模型查询【根据用户模型Id查询】
        /// </summary>
        /// <returns></returns>
        [HttpGet("{UserId}")]
        public async Task<UserDto> UserGetAsync(long UserId)
        {
            return await _UserService.UserGetAsync(UserId);
        }
        /// <summary>
        /// 4、用户模型更新
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> UserUpdateAsync(UserUpdateDto UserUpdateDto,long UserId)
        {
            return await _UserService.UserUpdateAsync(UserUpdateDto, UserId);
        }
        /// <summary>
        /// 5、用户模型删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> UserDeleteAsync(List<long> UserIds)
        {
            return await _UserService.UserDeleteAsync(UserIds) ;
        }
    }
}