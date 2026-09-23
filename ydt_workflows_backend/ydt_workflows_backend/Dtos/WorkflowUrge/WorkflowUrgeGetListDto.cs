namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 催办记录模型集合查询Dto
    /// </summary>
    public class WorkflowUrgeGetListDto
    {
       public bool IsDel { get; set; } // 状态【1：删除 0：未删除】
    }
}
