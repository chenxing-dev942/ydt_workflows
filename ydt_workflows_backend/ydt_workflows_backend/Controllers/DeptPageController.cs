using Microsoft.AspNetCore.Mvc;
using ydt_workflows_backend.CommonControllers;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Services;

namespace ydt_workflows_backend.Controllers
{
    /// <summary>
    /// 部门模型控制器
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class DeptPageController : CommonController<DeptPageController>
    {
        /// <summary>
        /// 部门模型模型Service
        /// </summary>
        private IDeptService _DeptService;   

        public DeptPageController(ILogger<DeptPageController> logger,
                                IDeptService DeptService) : 
            base(logger)
        {
            _DeptService = DeptService;
        }

        /// <summary>
        /// 1、部门模型创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> DeptCreateAsync(DeptCreateDto DeptCreateDto)
        {
            return await _DeptService.DeptCreateAsync(DeptCreateDto);
        }

        /// <summary>
        /// 2、部门模型集合查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<DeptDto>> DeptGetListAsync([FromQuery]DeptGetListDto DeptGetListDto)
        {
            return await _DeptService.DeptGetListAsync(DeptGetListDto);
        }

        /// <summary>
        /// 2.1、部门模型集合分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<DeptPageDto> DeptGetListPageAsync([FromQuery]DeptGetListPageDto DeptGetListPageDto)
        {
            return  await _DeptService.DeptGetListPageAsync(DeptGetListPageDto);
        }
        /// <summary>
        /// 3、部门模型查询【根据部门模型Id查询】
        /// </summary>
        /// <returns></returns>
        [HttpGet("{DeptId}")]
        public async Task<DeptDto> DeptGetAsync(long DeptId)
        {
            return await _DeptService.DeptGetAsync(DeptId);
        }
        /// <summary>
        /// 4、部门模型更新
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> DeptUpdateAsync(DeptUpdateDto DeptUpdateDto,long DeptId)
        {
            return await _DeptService.DeptUpdateAsync(DeptUpdateDto, DeptId);
        }
        /// <summary>
        /// 5、部门模型删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> DeptDeleteAsync(List<long> DeptIds)
        {
            return await _DeptService.DeptDeleteAsync(DeptIds) ;
        }
    }
}