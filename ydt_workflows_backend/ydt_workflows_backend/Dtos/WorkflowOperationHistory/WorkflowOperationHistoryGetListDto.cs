namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 流程操作历史模型集合查询Dto
    /// </summary>
    public class WorkflowOperationHistoryGetListDto
    {
       public bool IsDel { get; set; } // 状态【1：删除 0：未删除】
    }
}
