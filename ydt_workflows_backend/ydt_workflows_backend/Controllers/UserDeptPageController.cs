using Microsoft.AspNetCore.Mvc;
using ydt_workflows_backend.CommonControllers;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Services;

namespace ydt_workflows_backend.Controllers
{
    /// <summary>
    /// 用户部门关联模型控制器
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class UserDeptPageController : CommonController<UserDeptPageController>
    {
        /// <summary>
        /// 用户部门关联模型模型Service
        /// </summary>
        private IUserDeptService _UserDeptService;   

        public UserDeptPageController(ILogger<UserDeptPageController> logger,
                                IUserDeptService UserDeptService) : 
            base(logger)
        {
            _UserDeptService = UserDeptService;
        }

        /// <summary>
        /// 1、用户部门关联模型创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UserDeptCreateAsync(UserDeptCreateDto UserDeptCreateDto)
        {
            return await _UserDeptService.UserDeptCreateAsync(UserDeptCreateDto);
        }

        /// <summary>
        /// 2、用户部门关联模型集合查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<UserDeptDto>> UserDeptGetListAsync([FromQuery]UserDeptGetListDto UserDeptGetListDto)
        {
            return await _UserDeptService.UserDeptGetListAsync(UserDeptGetListDto);
        }

        /// <summary>
        /// 2.1、用户部门关联模型集合分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<UserDeptPageDto> UserDeptGetListPageAsync([FromQuery]UserDeptGetListPageDto UserDeptGetListPageDto)
        {
            return  await _UserDeptService.UserDeptGetListPageAsync(UserDeptGetListPageDto);
        }
        /// <summary>
        /// 3、用户部门关联模型查询【根据用户部门关联模型Id查询】
        /// </summary>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<UserDeptDto> UserDeptGetAsync(long Id)
        {
            return await _UserDeptService.UserDeptGetAsync(Id);
        }
        /// <summary>
        /// 4、用户部门关联模型更新
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> UserDeptUpdateAsync(UserDeptUpdateDto UserDeptUpdateDto,long Id)
        {
            return await _UserDeptService.UserDeptUpdateAsync(UserDeptUpdateDto, Id);
        }
        /// <summary>
        /// 5、用户部门关联模型删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> UserDeptDeleteAsync(List<long> Ids)
        {
            return await _UserDeptService.UserDeptDeleteAsync(Ids) ;
        }
    }
}