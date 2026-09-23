using AutoMapper;
using JadeFramework.Core.Extensions;
using JadeFramework.Core.Security;
using ydt_workflows_backend.CommonExceptions;
using ydt_workflows_backend.Contexts;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Fixtrues;
using ydt_workflows_backend.Models;
using ydt_workflows_backend.Services.IServices;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 登录接口
    /// </summary>
    public class LoginService : ILoginService
    {
        /// <summary>
        /// 工作流固定类
        /// </summary>
        public WorkflowFixtrue _workflowFixtrue { get; set; }
        private readonly IMapper _mapper;

        public LoginService(WorkflowFixtrue workflowFixtrue, IMapper mapper)
        {
            _workflowFixtrue = workflowFixtrue;
            _mapper = mapper;
        }

        
        public async Task<UserLoginResultDto> UserLoginAsync(UserLoginDto userLoginDto)
        {
            // 1、用户名和密码校验
            if (userLoginDto.UserName.IsNullOrEmpty() || userLoginDto.Password.IsNullOrEmpty())
            {
                throw new CommonException("请输入账号或密码");
            }
            //2、判断用户
            // 2.1、用户密码加密
            string pwd = EncryptProvider.CreateSha1Code(userLoginDto.Password);
            User user = await _workflowFixtrue.db.Users.FindAsync(
                m=>m.IsDel==false
                && m.UserName==userLoginDto.UserName.TrimBlank()
                && m.Password==userLoginDto.Password.TrimBlank());
            if (user==null)
            {
                throw new CommonException("用户名或密码错误！");
            }

            UserLoginResultDto result = _mapper.Map<UserLoginResultDto>(user);
            return result;
        }
    }
}