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
    }
}