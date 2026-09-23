using ydt_workflows_backend.Contexts;
using ydt_workflows_backend.Fixtrues;
using ydt_workflows_backend.Dtos;
using AutoMapper;
using AutoMapper;
using ydt_workflows_backend.Models;
using ydt_workflows_backend.CommonExceptions;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 子系统模型Service接口
    /// </summary>
    public class SystemsService : ISystemsService
    {
        /// <summary>
        /// 工作流固定类
        /// </summary>
        public WorkflowFixtrue _workflowFixtrue { get; set; }

        public IMapper _mapper { get; set; }

        public SystemsService(WorkflowFixtrue workflowFixtrue,
                               IMapper mapper)
        {
            _workflowFixtrue = workflowFixtrue;
            _mapper = mapper;
        }

        /// <summary>
        /// 子系统模型创建实现
        /// </summary>
        /// <param name="SystemsCreateDto"></param>
        /// <returns></returns>
        public async Task<bool> SystemsCreateAsync(SystemsCreateDto SystemsCreateDto)
        {
            // 1、SystemsCreateDto模型映射
            Systems Systems = _mapper.Map<Systems>(SystemsCreateDto);

            //2、实现子系统模型创建
            return await  _workflowFixtrue.db.Systemss.InsertAsync(Systems);
        }

        public async Task<List<SystemsDto>> SystemsGetListAsync(SystemsGetListDto SystemsGetListDto)
        {
            
            //1、查询所有子系统模型
            IEnumerable<Systems> Systemss = await _workflowFixtrue.db.Systemss.FindAllAsync(u => u.IsDel == SystemsGetListDto.IsDel);
            // 2、子系统模型映射
            List<SystemsDto> SystemsDtos = _mapper.Map<List<SystemsDto>>(Systemss);

            // 3、返回子系统模型
            return SystemsDtos;
        }

        public async Task<SystemsPageDto> SystemsGetListPageAsync(SystemsGetListPageDto SystemsGetListPageDto)
        {
            // 1、子系统模型分页Dto映射
            SystemsGetListPage SystemsGetListPage = _mapper.Map<SystemsGetListPage>(SystemsGetListPageDto);

            // 2、查询分页子系统模型
            SystemsPage SystemsPage = await _workflowFixtrue.db.Systemss.SystemsGetListPageAsync(SystemsGetListPage);

            // 3、子系统模型分页模型映射
            SystemsPageDto SystemsPageDto = _mapper.Map<SystemsPageDto>(SystemsPage);
            return SystemsPageDto;
        }
        public async Task<SystemsDto> SystemsGetAsync(long SystemId)
        {
            // 1、查询子系统模型
            Systems Systems = await _workflowFixtrue.db.Systemss.FindAsync(m => m.SystemId == SystemId);

            // 2、映射子系统模型
            SystemsDto SystemsDto = _mapper.Map<SystemsDto>(Systems);

            return SystemsDto;
        }
        public async Task<bool> SystemsUpdateAsync(SystemsUpdateDto SystemsUpdateDto, long SystemId)
        {
            // 1、查询子系统模型
            Systems Systems = await _workflowFixtrue.db.Systemss.FindAsync(m => m.SystemId == SystemId);
            if (Systems == null)
            {
                throw new CommonException("Systems不存在");
            }

            // 2、子系统模型模型映射
            Systems = _mapper.Map<SystemsUpdateDto, Systems>(SystemsUpdateDto, Systems);

            // 3、子系统模型更新实现
            return await _workflowFixtrue.db.Systemss.UpdateAsync(Systems);
        }
        public async Task<bool> SystemsDeleteAsync(List<long> SystemIds)
        {
            // 1、开启事务
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                
                // 2、查询子系统模型
                var Systemss = await _workflowFixtrue.db.Systemss.FindAllAsync(m => m.IsDel == false && SystemIds.Contains(m.SystemId));
                foreach (var Systems in Systemss)
                {
                    // 3、删除子系统模型【逻辑删除】
                    Systems.IsDel = true; // 1:删除状态
                    await _workflowFixtrue.db.Systemss.UpdateAsync(Systems, tran);
                }
                tran.Commit();
                return true;
            }
        }
    }
}