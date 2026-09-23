namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 用户部门关联模型集合查询Dto
    /// </summary>
    public class UserDeptGetListDto
    {
       public bool IsDel { get; set; } // 状态【1：删除 0：未删除】
    }
}
