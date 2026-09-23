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
    /// 角色模型Service接口
    /// </summary>
    public class RoleService : IRoleService
    {
        /// <summary>
        /// 工作流固定类
        /// </summary>
        public WorkflowFixtrue _workflowFixtrue { get; set; }

        public IMapper _mapper { get; set; }

        public RoleService(WorkflowFixtrue workflowFixtrue,
                               IMapper mapper)
        {
            _workflowFixtrue = workflowFixtrue;
            _mapper = mapper;
        }

        /// <summary>
        /// 角色模型创建实现
        /// </summary>
        /// <param name="RoleCreateDto"></param>
        /// <returns></returns>
        public async Task<bool> RoleCreateAsync(RoleCreateDto RoleCreateDto)
        {
            // 1、RoleCreateDto模型映射
            Role Role = _mapper.Map<Role>(RoleCreateDto);

            //2、实现角色模型创建
            return await  _workflowFixtrue.db.Roles.InsertAsync(Role);
        }

        public async Task<List<RoleDto>> RoleGetListAsync(RoleGetListDto RoleGetListDto)
        {
            
            //1、查询所有角色模型
            IEnumerable<Role> Roles = await _workflowFixtrue.db.Roles.FindAllAsync(u => u.IsDel == RoleGetListDto.IsDel);
            // 2、角色模型映射
            List<RoleDto> RoleDtos = _mapper.Map<List<RoleDto>>(Roles);

            // 3、返回角色模型
            return RoleDtos;
        }

        public async Task<RolePageDto> RoleGetListPageAsync(RoleGetListPageDto RoleGetListPageDto)
        {
            // 1、角色模型分页Dto映射
            RoleGetListPage RoleGetListPage = _mapper.Map<RoleGetListPage>(RoleGetListPageDto);

            // 2、查询分页角色模型
            RolePage RolePage = await _workflowFixtrue.db.Roles.RoleGetListPageAsync(RoleGetListPage);

            // 3、角色模型分页模型映射
            RolePageDto RolePageDto = _mapper.Map<RolePageDto>(RolePage);
            return RolePageDto;
        }
        public async Task<RoleDto> RoleGetAsync(long RoleId)
        {
            // 1、查询角色模型
            Role Role = await _workflowFixtrue.db.Roles.FindAsync(m => m.RoleId == RoleId);

            // 2、映射角色模型
            RoleDto RoleDto = _mapper.Map<RoleDto>(Role);

            return RoleDto;
        }
        public async Task<bool> RoleUpdateAsync(RoleUpdateDto RoleUpdateDto, long RoleId)
        {
            // 1、查询角色模型
            Role Role = await _workflowFixtrue.db.Roles.FindAsync(m => m.RoleId == RoleId);
            if (Role == null)
            {
                throw new CommonException("Role不存在");
            }

            // 2、角色模型模型映射
            Role = _mapper.Map<RoleUpdateDto, Role>(RoleUpdateDto, Role);

            // 3、角色模型更新实现
            return await _workflowFixtrue.db.Roles.UpdateAsync(Role);
        }
        public async Task<bool> RoleDeleteAsync(List<long> RoleIds)
        {
            // 1、开启事务
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                
                // 2、查询角色模型
                var Roles = await _workflowFixtrue.db.Roles.FindAllAsync(m => m.IsDel == false && RoleIds.Contains(m.RoleId));
                foreach (var Role in Roles)
                {
                    // 3、删除角色模型【逻辑删除】
                    Role.IsDel = true; // 1:删除状态
                    await _workflowFixtrue.db.Roles.UpdateAsync(Role, tran);
                }
                tran.Commit();
                return true;
            }
        }
    }
}