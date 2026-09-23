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
    /// 资源【菜单】模型Service接口
    /// </summary>
    public class ResourceService : IResourceService
    {
        /// <summary>
        /// 工作流固定类
        /// </summary>
        public WorkflowFixtrue _workflowFixtrue { get; set; }

        public IMapper _mapper { get; set; }

        public ResourceService(WorkflowFixtrue workflowFixtrue,
                               IMapper mapper)
        {
            _workflowFixtrue = workflowFixtrue;
            _mapper = mapper;
        }

        /// <summary>
        /// 资源【菜单】模型创建实现
        /// </summary>
        /// <param name="ResourceCreateDto"></param>
        /// <returns></returns>
        public async Task<bool> ResourceCreateAsync(ResourceCreateDto ResourceCreateDto)
        {
            // 1、ResourceCreateDto模型映射
            Resource Resource = _mapper.Map<Resource>(ResourceCreateDto);

            //2、实现资源【菜单】模型创建
            return await  _workflowFixtrue.db.Resources.InsertAsync(Resource);
        }

        public async Task<List<ResourceDto>> ResourceGetListAsync(ResourceGetListDto ResourceGetListDto)
        {
            
            //1、查询所有资源【菜单】模型
            IEnumerable<Resource> Resources = await _workflowFixtrue.db.Resources.FindAllAsync(u => u.IsDel == ResourceGetListDto.IsDel);
            // 2、资源【菜单】模型映射
            List<ResourceDto> ResourceDtos = _mapper.Map<List<ResourceDto>>(Resources);

            // 3、返回资源【菜单】模型
            return ResourceDtos;
        }

        public async Task<ResourcePageDto> ResourceGetListPageAsync(ResourceGetListPageDto ResourceGetListPageDto)
        {
            // 1、资源【菜单】模型分页Dto映射
            ResourceGetListPage ResourceGetListPage = _mapper.Map<ResourceGetListPage>(ResourceGetListPageDto);

            // 2、查询分页资源【菜单】模型
            ResourcePage ResourcePage = await _workflowFixtrue.db.Resources.ResourceGetListPageAsync(ResourceGetListPage);

            // 3、资源【菜单】模型分页模型映射
            ResourcePageDto ResourcePageDto = _mapper.Map<ResourcePageDto>(ResourcePage);
            return ResourcePageDto;
        }
        public async Task<ResourceDto> ResourceGetAsync(long ResourceId)
        {
            // 1、查询资源【菜单】模型
            Resource Resource = await _workflowFixtrue.db.Resources.FindAsync(m => m.ResourceId == ResourceId);

            // 2、映射资源【菜单】模型
            ResourceDto ResourceDto = _mapper.Map<ResourceDto>(Resource);

            return ResourceDto;
        }
        public async Task<bool> ResourceUpdateAsync(ResourceUpdateDto ResourceUpdateDto, long ResourceId)
        {
            // 1、查询资源【菜单】模型
            Resource Resource = await _workflowFixtrue.db.Resources.FindAsync(m => m.ResourceId == ResourceId);
            if (Resource == null)
            {
                throw new CommonException("Resource不存在");
            }

            // 2、资源【菜单】模型模型映射
            Resource = _mapper.Map<ResourceUpdateDto, Resource>(ResourceUpdateDto, Resource);

            // 3、资源【菜单】模型更新实现
            return await _workflowFixtrue.db.Resources.UpdateAsync(Resource);
        }
        public async Task<bool> ResourceDeleteAsync(List<long> ResourceIds)
        {
            // 1、开启事务
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                
                // 2、查询资源【菜单】模型
                var Resources = await _workflowFixtrue.db.Resources.FindAllAsync(m => m.IsDel == false && ResourceIds.Contains(m.ResourceId));
                foreach (var Resource in Resources)
                {
                    // 3、删除资源【菜单】模型【逻辑删除】
                    Resource.IsDel = true; // 1:删除状态
                    await _workflowFixtrue.db.Resources.UpdateAsync(Resource, tran);
                }
                tran.Commit();
                return true;
            }
        }
    }
}