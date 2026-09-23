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
    /// 用户角色关联模型Service接口
    /// </summary>
    public class UserRoleService : IUserRoleService
    {
        /// <summary>
        /// 工作流固定类
        /// </summary>
        public WorkflowFixtrue _workflowFixtrue { get; set; }

        public IMapper _mapper { get; set; }

        public UserRoleService(WorkflowFixtrue workflowFixtrue,
                               IMapper mapper)
        {
            _workflowFixtrue = workflowFixtrue;
            _mapper = mapper;
        }

        /// <summary>
        /// 用户角色关联模型创建实现
        /// </summary>
        /// <param name="UserRoleCreateDto"></param>
        /// <returns></returns>
        public async Task<bool> UserRoleCreateAsync(UserRoleCreateDto UserRoleCreateDto)
        {
            // 1、UserRoleCreateDto模型映射
            UserRole UserRole = _mapper.Map<UserRole>(UserRoleCreateDto);

            //2、实现用户角色关联模型创建
            return await  _workflowFixtrue.db.UserRoles.InsertAsync(UserRole);
        }

        public async Task<List<UserRoleDto>> UserRoleGetListAsync(UserRoleGetListDto UserRoleGetListDto)
        {
            
            //1、查询所有用户角色关联模型
            IEnumerable<UserRole> UserRoles = await _workflowFixtrue.db.UserRoles.FindAllAsync();
            // 2、用户角色关联模型映射
            List<UserRoleDto> UserRoleDtos = _mapper.Map<List<UserRoleDto>>(UserRoles);

            // 3、返回用户角色关联模型
            return UserRoleDtos;
        }

        public async Task<UserRolePageDto> UserRoleGetListPageAsync(UserRoleGetListPageDto UserRoleGetListPageDto)
        {
            // 1、用户角色关联模型分页Dto映射
            UserRoleGetListPage UserRoleGetListPage = _mapper.Map<UserRoleGetListPage>(UserRoleGetListPageDto);

            // 2、查询分页用户角色关联模型
            UserRolePage UserRolePage = await _workflowFixtrue.db.UserRoles.UserRoleGetListPageAsync(UserRoleGetListPage);

            // 3、用户角色关联模型分页模型映射
            UserRolePageDto UserRolePageDto = _mapper.Map<UserRolePageDto>(UserRolePage);
            return UserRolePageDto;
        }
        public async Task<UserRoleDto> UserRoleGetAsync(long Id)
        {
            // 1、查询用户角色关联模型
            UserRole UserRole = await _workflowFixtrue.db.UserRoles.FindAsync(m => m.Id == Id);

            // 2、映射用户角色关联模型
            UserRoleDto UserRoleDto = _mapper.Map<UserRoleDto>(UserRole);

            return UserRoleDto;
        }
        public async Task<bool> UserRoleUpdateAsync(UserRoleUpdateDto UserRoleUpdateDto, long Id)
        {
            // 1、查询用户角色关联模型
            UserRole UserRole = await _workflowFixtrue.db.UserRoles.FindAsync(m => m.Id == Id);
            if (UserRole == null)
            {
                throw new CommonException("UserRole不存在");
            }

            // 2、用户角色关联模型模型映射
            UserRole = _mapper.Map<UserRoleUpdateDto, UserRole>(UserRoleUpdateDto, UserRole);

            // 3、用户角色关联模型更新实现
            return await _workflowFixtrue.db.UserRoles.UpdateAsync(UserRole);
        }
        public async Task<bool> UserRoleDeleteAsync(List<long> Ids)
        {
            // 1、开启事务
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                
                // 2、查询用户角色关联模型
                var UserRoles = await _workflowFixtrue.db.UserRoles.FindAllAsync(m => Ids.Contains(m.Id));
                foreach (var UserRole in UserRoles)
                {
                    // 3、删除用户角色关联模型【真实删除】
                    await _workflowFixtrue.db.UserRoles.DeleteAsync(UserRole);
                }
                tran.Commit();
                return true;
            }
        }
    }
}