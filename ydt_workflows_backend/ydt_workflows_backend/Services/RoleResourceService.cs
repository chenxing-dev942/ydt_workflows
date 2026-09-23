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
    /// 角色资源关联模型Service接口
    /// </summary>
    public class RoleResourceService : IRoleResourceService
    {
        /// <summary>
        /// 工作流固定类
        /// </summary>
        public WorkflowFixtrue _workflowFixtrue { get; set; }

        public IMapper _mapper { get; set; }

        public RoleResourceService(WorkflowFixtrue workflowFixtrue,
                               IMapper mapper)
        {
            _workflowFixtrue = workflowFixtrue;
            _mapper = mapper;
        }

        /// <summary>
        /// 角色资源关联模型创建实现
        /// </summary>
        /// <param name="RoleResourceCreateDto"></param>
        /// <returns></returns>
        public async Task<bool> RoleResourceCreateAsync(RoleResourceCreateDto RoleResourceCreateDto)
        {
            // 1、RoleResourceCreateDto模型映射
            RoleResource RoleResource = _mapper.Map<RoleResource>(RoleResourceCreateDto);

            //2、实现角色资源关联模型创建
            return await  _workflowFixtrue.db.RoleResources.InsertAsync(RoleResource);
        }

        public async Task<List<RoleResourceDto>> RoleResourceGetListAsync(RoleResourceGetListDto RoleResourceGetListDto)
        {
            
            //1、查询所有角色资源关联模型
            IEnumerable<RoleResource> RoleResources = await _workflowFixtrue.db.RoleResources.FindAllAsync();
            // 2、角色资源关联模型映射
            List<RoleResourceDto> RoleResourceDtos = _mapper.Map<List<RoleResourceDto>>(RoleResources);

            // 3、返回角色资源关联模型
            return RoleResourceDtos;
        }

        public async Task<RoleResourcePageDto> RoleResourceGetListPageAsync(RoleResourceGetListPageDto RoleResourceGetListPageDto)
        {
            // 1、角色资源关联模型分页Dto映射
            RoleResourceGetListPage RoleResourceGetListPage = _mapper.Map<RoleResourceGetListPage>(RoleResourceGetListPageDto);

            // 2、查询分页角色资源关联模型
            RoleResourcePage RoleResourcePage = await _workflowFixtrue.db.RoleResources.RoleResourceGetListPageAsync(RoleResourceGetListPage);

            // 3、角色资源关联模型分页模型映射
            RoleResourcePageDto RoleResourcePageDto = _mapper.Map<RoleResourcePageDto>(RoleResourcePage);
            return RoleResourcePageDto;
        }
        public async Task<RoleResourceDto> RoleResourceGetAsync(long Id)
        {
            // 1、查询角色资源关联模型
            RoleResource RoleResource = await _workflowFixtrue.db.RoleResources.FindAsync(m => m.Id == Id);

            // 2、映射角色资源关联模型
            RoleResourceDto RoleResourceDto = _mapper.Map<RoleResourceDto>(RoleResource);

            return RoleResourceDto;
        }
        public async Task<bool> RoleResourceUpdateAsync(RoleResourceUpdateDto RoleResourceUpdateDto, long Id)
        {
            // 1、查询角色资源关联模型
            RoleResource RoleResource = await _workflowFixtrue.db.RoleResources.FindAsync(m => m.Id == Id);
            if (RoleResource == null)
            {
                throw new CommonException("RoleResource不存在");
            }

            // 2、角色资源关联模型模型映射
            RoleResource = _mapper.Map<RoleResourceUpdateDto, RoleResource>(RoleResourceUpdateDto, RoleResource);

            // 3、角色资源关联模型更新实现
            return await _workflowFixtrue.db.RoleResources.UpdateAsync(RoleResource);
        }
        public async Task<bool> RoleResourceDeleteAsync(List<long> Ids)
        {
            // 1、开启事务
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                
                // 2、查询角色资源关联模型
                var RoleResources = await _workflowFixtrue.db.RoleResources.FindAllAsync(m => Ids.Contains(m.Id));
                foreach (var RoleResource in RoleResources)
                {
                    // 3、删除角色资源关联模型【真实删除】
                    await _workflowFixtrue.db.RoleResources.DeleteAsync(RoleResource);
                }
                tran.Commit();
                return true;
            }
        }
    }
}