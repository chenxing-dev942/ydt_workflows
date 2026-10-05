using AutoMapper;
using AutoMapper;
using JadeFramework.Core.Domain.Enum;
using JadeFramework.Core.Extensions;
using ydt_workflows_backend.CommonExceptions;
using ydt_workflows_backend.Contexts;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Fixtrues;
using ydt_workflows_backend.Models;

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
            bool flag=await _workflowFixtrue.db.Resources.InsertAsync(Resource);
            List<ResourceButtonDto> buttonDtos = ResourceCreateDto.ResourceButtonsDtos;
            List<Resource> list=new List<Resource>();
            foreach (var button in buttonDtos)
            {
                // 3.1.1、创建按钮资源
                Resource res = new Resource()
                {
                    IsButton = true, // 为按钮
                    CreateTime = DateTime.Now.ToTimeStamp(),
                    SystemId = ResourceCreateDto.SystemId,
                    ResourceName = button.Name,
                    ButtonType = ResourceCreateDto.ButtonType,
                    ParentId = ResourceCreateDto.ResourceId,
                    ButtonClass = ((ButtonType)button.ButtonModel).ToClass()
                };
                list.Add(res);
            }
            //3.2、批量保存按钮
            await _workflowFixtrue.db.Resources.BulkInsertAsync(list);
            return true;    
        }

        public async Task<List<ResourceDto>> ResourceGetListAsync(ResourceGetListDto ResourceGetListDto)
        {
            
            //1、查询所有资源【菜单】模型
            IEnumerable<Resource> Resources = await _workflowFixtrue.db.Resources.FindAllAsync(u => u.IsDel == ResourceGetListDto.IsDel);
            // 2、资源【菜单】模型映射
            List<ResourceDto> ResourceDtos = _mapper.Map<List<ResourceDto>>(Resources);

            List<ResourceDto> RootResourceDtos=ResourceDtos.Where(r=>r.ParentId==0).ToList();
            GetChildResources(RootResourceDtos, ResourceDtos);
            // 3、返回资源【菜单】模型
            return ResourceDtos;
        }

        private void GetChildResources(List<ResourceDto> RootResources,List<ResourceDto> resourceDtos)
        {
            foreach(var resourceDto in RootResources)
            {
                List<ResourceDto> resources=resourceDtos.Where(r=>r.ParentId==resourceDto.ResourceId).ToList();
                //赋值子资源
                resourceDto.ChildResouceDto = resources;
                GetChildResources(resourceDto.ChildResouceDto,resourceDtos);
            }
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
        public async Task<ResourceSelectResultDto> ResourceGetAsync(long ResourceId)
        {
            // 1、查询资源【菜单】模型
            Resource Resource = await _workflowFixtrue.db.Resources.FindAsync(m => m.ResourceId == ResourceId);

            // 2、映射资源【菜单】模型
            ResourceDto ResourceDto = _mapper.Map<ResourceDto>(Resource);
            IEnumerable<Resource> resources = await _workflowFixtrue.db.Resources.FindAllAsync(u => u.IsDel == ResourceDto.IsDel 
                                                                                            && u.ResourceId!=ResourceId);
            List<ResourceDto> ResourceDtos = _mapper.Map<List<ResourceDto>>(resources);
            List<ResourceDto> RootResourceDtos= ResourceDtos.Where(r=>r.ParentId==0).ToList();
            GetChildResources(RootResourceDtos, ResourceDtos);
            ResourceDto.ChildResouceDto = RootResourceDtos;

            ResourceSelectResultDto resourceSelectResultDto = new ResourceSelectResultDto();
            resourceSelectResultDto.ButtonClass = ResourceDto.ButtonClass;
            resourceSelectResultDto.ButtonType = ResourceDto.ButtonType;
            resourceSelectResultDto.IsButton = ResourceDto.IsButton;
            resourceSelectResultDto.IsDel = ResourceDto.IsDel;
            resourceSelectResultDto.ResourceName = ResourceDto.ResourceName;
            resourceSelectResultDto.ResourceId = ResourceId;
            resourceSelectResultDto.IsShow = ResourceDto.IsShow;
            resourceSelectResultDto.Memo = ResourceDto.Memo;
            resourceSelectResultDto.ParentResourceDtoDto = ResourceDto;

            IEnumerable<Resource> ButtonDtos = await _workflowFixtrue.db.Resources.FindAllAsync(
                                        u => u.IsDel == ResourceDto.IsDel
                                        && u.ParentId == ResourceId
                                        && u.IsButton == true);

            List<ResourceButtonDto> ResourceButtonDtos = ButtonDtos.Select(r => new ResourceButtonDto
            {
                Id=r.ResourceId,
                Name=r.ResourceName,
            }).ToList();
            resourceSelectResultDto.ResourceButtonDtos = ResourceButtonDtos;    

            return resourceSelectResultDto;
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