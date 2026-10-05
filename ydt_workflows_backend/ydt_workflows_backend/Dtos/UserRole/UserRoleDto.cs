namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 用户角色关联模型集合查询结果Dto
    /// </summary>
    public class UserRoleDto
    {
        public long UserId { get; set; }
        public long SystemId { get; set; } // 系统Id
        public string SystemCode { get; set; }// 系统编码
        public string SystemName { get; set; } // 系统Name

        public List<UserRoleList> userRoleLists { get; set; }  // 用户角色集合
    }

    /// <summary>
    /// 用户角色集合
    /// </summary>
    public class UserRoleList
    {
        public long RoleId { get; set; } // 角色Id
        public string RoleName { get; set; } // 角色名称
        public bool Selected { set; get; } // 是否选中。true 代表选中。false 未选中。
    }
}
