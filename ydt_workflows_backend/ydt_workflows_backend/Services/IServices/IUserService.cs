using ydt_workflows_backend.Dtos;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 用户模型Service接口
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 1、用户模型创建
        /// </summary>
        /// <param name="UserCreateDto"></param>
        /// <returns></returns>
        public Task<bool> UserCreateAsync(UserCreateDto UserCreateDto);

        /// <summary>
        /// 2、用户模型集合查询
        /// </summary>
        /// <returns></returns>
        public Task<List<UserDto>> UserGetListAsync(UserGetListDto UserGetListDto);

        /// <summary>
        /// 2.1、用户模型集合分页查询
        /// </summary>
        /// <returns></returns>
        public Task<UserPageDto> UserGetListPageAsync(UserGetListPageDto UserGetListPageDto);
        /// <summary>
        /// 3、用户模型查询【根据UserId查询】
        /// </summary>
        /// <returns></returns>
        public Task<UserDto> UserGetAsync(long UserId);
        /// <summary>
        /// 4、用户模型更新
        /// </summary>
        /// <returns></returns>
        public Task<bool> UserUpdateAsync(UserUpdateDto UserUpdateDto, long UserId);
        /// <summary>
        /// 5、用户模型删除
        /// </summary>
        /// <returns></returns>
        public Task<bool> UserDeleteAsync(List<long> UserIds);
    }
}
