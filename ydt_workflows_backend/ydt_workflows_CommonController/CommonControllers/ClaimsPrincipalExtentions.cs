using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ydt_workflows_CommonController.CommonControllers
{
    /// <summary>
    ///  ClaimsPrincipal 扩展 转换成为SysUser
    /// </summary>
    public  static class ClaimsPrincipalExtentions
    {
        public static SysUser ToSysUser(this ClaimsPrincipal principal)
        {
            //1、创建SysUser
            SysUser sysUser = new SysUser();
            List<Claim> source = principal.Claims.ToList();
            sysUser.UserId = Int64.Parse(source.Single((Claim m) => m.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value);
            sysUser.UserName = source.Single((Claim m) => m.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
            if (source.SingleOrDefault((Claim m) => m.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri") != null)
            {
                sysUser.HeadImg = source.Single((Claim m) => m.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri").Value;
            }

            // userIdentity.Sex = (UserSex)source.Single((Claim m) => m.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender").Value.ToInt32();
            Claim claim = source.SingleOrDefault((Claim m) => m.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata");
            if (claim != null)
            {
                sysUser.Other = JsonConvert.DeserializeObject(claim.Value);
            }

            return sysUser;
        }
    }
}
