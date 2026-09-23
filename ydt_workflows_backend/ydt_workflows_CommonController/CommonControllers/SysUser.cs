using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ydt_workflows_CommonController.CommonControllers
{
    public class SysUser
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
        //public UserSex Sex { get; set; }

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
