namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 用户角色关联模型集合查询结果Dto
    /// </summary>
    public class UserRoleDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public long CreateTime { get; set; }}
}
