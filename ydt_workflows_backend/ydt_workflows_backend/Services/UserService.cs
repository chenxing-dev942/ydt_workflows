using ydt_workflows_backend.Contexts;
using ydt_workflows_backend.Fixtrues;
using ydt_workflows_backend.Dtos;
using AutoMapper;
using AutoMapper;
using ydt_workflows_backend.Models;
using ydt_workflows_backend.CommonExceptions;
using JadeFramework.Core.Extensions;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 用户模型Service接口
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// 工作流固定类
        /// </summary>
        public WorkflowFixtrue _workflowFixtrue { get; set; }

        public IMapper _mapper { get; set; }

        public UserService(WorkflowFixtrue workflowFixtrue,
                               IMapper mapper)
        {
            _workflowFixtrue = workflowFixtrue;
            _mapper = mapper;
        }

        /// <summary>
        /// 用户模型创建实现
        /// </summary>
        /// <param name="UserCreateDto"></param>
        /// <returns></returns>
        public async Task<bool> UserCreateAsync(UserCreateDto UserCreateDto)
        {
            // 1、UserCreateDto模型映射
            User User = _mapper.Map<User>(UserCreateDto);

            //2、实现用户模型创建
            return await  _workflowFixtrue.db.Users.InsertAsync(User);
        }

        public async Task<List<UserDto>> UserGetListAsync(UserGetListDto UserGetListDto)
        {
            
            //1、查询所有用户模型
            IEnumerable<User> Users = await _workflowFixtrue.db.Users.FindAllAsync(u => u.IsDel == UserGetListDto.IsDel);
            // 2、用户模型映射
            List<UserDto> UserDtos = _mapper.Map<List<UserDto>>(Users);

            // 3、返回用户模型
            return UserDtos;
        }

        public async Task<UserPageDto> UserGetListPageAsync(UserGetListPageDto UserGetListPageDto)
        {
            // 1、用户模型分页Dto映射
            UserGetListPage UserGetListPage = _mapper.Map<UserGetListPage>(UserGetListPageDto);

            // 2、查询分页用户模型
            UserPage UserPage = await _workflowFixtrue.db.Users.UserGetListPageAsync(UserGetListPage);

            // 3、用户模型分页模型映射
            UserPageDto UserPageDto = _mapper.Map<UserPageDto>(UserPage);
            return UserPageDto;
        }
        public async Task<UserDto> UserGetAsync(long UserId)
        {
            // 1、查询用户模型
            User User = await _workflowFixtrue.db.Users.FindAsync(m => m.UserId == UserId);

            // 2、映射用户模型
            UserDto UserDto = _mapper.Map<UserDto>(User);

            return UserDto;
        }
        public async Task<bool> UserUpdateAsync(UserUpdateDto UserUpdateDto, long UserId)
        {
            // 1、查询用户模型
            User User = await _workflowFixtrue.db.Users.FindAsync(m => m.UserId == UserId);
            if (User == null)
            {
                throw new CommonException("User不存在");
            }

            // 2、用户模型模型映射
            User = _mapper.Map<UserUpdateDto, User>(UserUpdateDto, User);

            // 3、用户模型更新实现
            return await _workflowFixtrue.db.Users.UpdateAsync(User);
        }
        public async Task<bool> UserDeleteAsync(List<long> UserIds)
        {
            // 1、开启事务
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                
                // 2、查询用户模型
                var Users = await _workflowFixtrue.db.Users.FindAllAsync(m => m.IsDel == false && UserIds.Contains(m.UserId));
                foreach (var User in Users)
                {
                    // 3、删除用户模型【逻辑删除】
                    User.IsDel = true; // 1:删除状态
                    await _workflowFixtrue.db.Users.UpdateAsync(User, tran);
                }
                tran.Commit();
                return true;
            }
        }

        public async Task<UserDeptDto> UserDeptGetAsync(long userId)
        {
            var user = await _workflowFixtrue.db.Users.FindByIdAsync(userId);
            var userDept= await _workflowFixtrue.db.UserDepts.FindAsync(m=>m.UserId==userId);
            var depts = await _workflowFixtrue.db.Depts.FindAllAsync(m=>m.IsDel==false);
            UserDeptDto userDeptDto = new UserDeptDto();
            userDeptDto.UserId = userId;
            userDeptDto.UserName = user.UserName;
            userDeptDto.DeptId = userDept.DeptId;
            userDeptDto.userDeptLists = depts.Select(m=>new UserDeptList()
            {
                DeptId = m.DeptId,
                DeptName = m.DeptName,
                Selected=false
            }).ToList();

            if (userDept != null)
            {
                foreach(var item in userDeptDto.userDeptLists)
                {
                    if(item.DeptId== userDept.DeptId)
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
            return userDeptDto;
        }

        public async Task<bool> UserDeptAssignAsync(UserDeptAssignDto userDeptAssignDto)
        {
            using (var tran=_workflowFixtrue.db.BeginTransaction())
            {
                try
                {
                    var dbUserDept = await _workflowFixtrue.db.UserDepts.FindByIdAsync(userDeptAssignDto.UserId);
                    if (dbUserDept != null)
                    {
                        await _workflowFixtrue.db.UserDepts.DeleteAsync(dbUserDept, tran);
                    }
                    UserDept userDept = _mapper.Map<UserDept>(userDeptAssignDto);
                    userDept.CreateTime=DateTime.Now.ToTimeStamp();
                    await _workflowFixtrue.db.UserDepts.InsertAsync(userDept,tran);
                    tran.Commit();
                    return true;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        public async Task<List<UserRoleDto>> UserRoleGetAsync(long userId)
        {
            // 1、查询系统
            var systems = await _workflowFixtrue.db.Systemss.FindAllAsync(m => m.IsDel == false);

            // 2、查询角色
            var rolelist = await _workflowFixtrue.db.Roles.FindAllAsync(m => m.IsDel == false);

            // 3、查询用户角色
            var userRoles = await _workflowFixtrue.db.UserRoles.FindAllAsync(m => m.UserId == userId);

            // 4、配置系统
            List<UserRoleDto> userRoleDtos = systems.Select(system =>
                                        new UserRoleDto()
                                        {
                                            SystemId = system.SystemId,
                                            SystemCode = system.SystemCode,
                                            SystemName = system.SystemName,
                                        }).ToList();

            // 4.1、配置角色集合
            foreach (var userRoleDto in userRoleDtos)
            {
                //4.1.1、查询角色【根据系统Id】
                var roles = rolelist.Where(r => r.SystemId == userRoleDto.SystemId);

                //4.1.2、转换角色
                List<UserRoleList> userRoleLists = roles.Select(role => new UserRoleList()
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleName,
                    Selected = false,// 未选中
                }).ToList();

                //4.1.3、赋值角色
                userRoleDto.userRoleLists = userRoleLists;

                // 4.2、配置角色是否选中
                if (userRoles != null)
                {
                    // 4.2.1、遍历userRoleLists
                    foreach (var userRoleList in userRoleLists)
                    {
                        //4.2.2、遍历userRoles
                        foreach (var userRole in userRoles)
                        {
                            //4.2.3、判断角色是否选中
                            if (userRoleList.RoleId.Equals(userRole.RoleId))
                            {
                                //4.2.4、角色选中【角色之前已分配】
                                userRoleList.Selected = true;
                            }
                        }
                    }
                }
                
            }
            return userRoleDtos;
        }

        public async Task<bool> UserRoleAssignAsync(UserRoleAssignDto userRoleAssignDto)
        {
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
               
                try
                {
                    // 1、删除
                    // 1.1、查询用户角色
                    var userRoles = await _workflowFixtrue.db.UserRoles.
                    FindAllAsync(m => m.UserId == userRoleAssignDto.UserId);

                    // 1.2、删除用户角色
                    if (userRoles != null && userRoles.HasItems())
                    {
                        foreach (var item in userRoles)
                        {
                            await _workflowFixtrue.db.UserRoles.DeleteAsync(item, tran);
                        }
                    }
                    if (userRoleAssignDto.RoleIds.HasItems())
                    {
                        foreach (var roleId in userRoleAssignDto.RoleIds)
                        {
                            UserRole userRole = new UserRole()
                            {
                                UserId = userRoleAssignDto.UserId,
                                RoleId = roleId
                            };
                            //执行分配
                            await _workflowFixtrue.db.UserRoles.InsertAsync(userRole, tran);
                        }
                    }

                    tran.Commit();
                    return true;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    return false;
                }
            }

        }
    }
}