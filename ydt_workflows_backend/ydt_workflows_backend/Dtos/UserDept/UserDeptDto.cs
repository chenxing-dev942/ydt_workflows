namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 用户部门关联模型集合查询结果Dto
    /// </summary>
    public class UserDeptDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long DeptId { get; set; }
        public long CreateTime { get; set; }}
}
