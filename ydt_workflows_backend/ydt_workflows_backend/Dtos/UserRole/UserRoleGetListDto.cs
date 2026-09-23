namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 用户角色关联模型集合查询Dto
    /// </summary>
    public class UserRoleGetListDto
    {
       public bool IsDel { get; set; } // 状态【1：删除 0：未删除】
    }
}
