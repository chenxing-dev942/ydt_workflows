namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 工作流获取权限系统数据模型集合查询Dto
    /// </summary>
    public class WorkflowsqlGetListDto
    {
       public bool IsDel { get; set; } // 状态【1：删除 0：未删除】
    }
}
