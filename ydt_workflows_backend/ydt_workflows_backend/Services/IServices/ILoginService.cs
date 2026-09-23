using ydt_workflows_backend.Dtos;

namespace ydt_workflows_backend.Services.IServices
{
    /// <summary>
    /// 登录接口
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns></returns>
        public Task<UserLoginResultDto> UserLoginAsync(UserLoginDto userLoginDto);
    }
}
