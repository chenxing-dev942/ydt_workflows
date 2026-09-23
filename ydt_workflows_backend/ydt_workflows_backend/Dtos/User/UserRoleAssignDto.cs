namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 用户角色分配Dto
    /// </summary>
    public class UserRoleAssignDto
    {
        public List<long> RoleIds { set; get; }
        public long UserId { set; get; }
    }
}
