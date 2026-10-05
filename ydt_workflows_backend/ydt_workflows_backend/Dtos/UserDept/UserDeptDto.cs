namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 用户部门关联模型集合查询结果Dto
    /// </summary>
    public class UserDeptDto
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public long DeptId { get; set; }
        public List<UserDeptList> userDeptLists { get; set; }   
    }

    /// <summary>
    /// 用户部门接口
    /// </summary>
    public class UserDeptList
    {
        public long DeptId { get; set; }
        public string DeptName { get; set; }
        public bool Selected { set; get; } // 是否选中
    }
}
