using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ydt_workflows_CommonController.CommonControllers;

namespace ydt_workflows_backend.CommonControllers
{
    public class CommonController<T>: ControllerBase
    {
        protected ILogger<T> _logger;

        public SysUser _sysUser => base.User.ToSysUser(); // 每次获取最新出的SysUser
        public CommonController(ILogger<T> logger)
        {
            _logger = logger;
        }
    }
}
