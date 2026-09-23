using JadeFramework.Core.Domain.Entities;

namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 用户登录结果Dto
    /// </summary>
    public class UserLoginResultDto
    {
        //
        // 摘要:
        //     用户id
        public long UserId { get; set; }

        //
        // 摘要:
        //     用户名
        public string UserName { get; set; }

        //
        // 摘要:
        //     头像
        public string HeadImg { get; set; }

        //
        // 摘要:
        //     用户性别
        public UserSex Sex { get; set; }

        //
        // 摘要:
        //     创建时间
        public long CreateTime { get; set; }

        //
        // 摘要:
        //     其他属性
        public object Other { get; set; }
    }
}
