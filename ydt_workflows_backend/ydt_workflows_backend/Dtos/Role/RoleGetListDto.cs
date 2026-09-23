namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 角色模型集合查询Dto
    /// </summary>
    public class RoleGetListDto
    {
       public bool IsDel { get; set; } // 状态【1：删除 0：未删除】
    }
}
