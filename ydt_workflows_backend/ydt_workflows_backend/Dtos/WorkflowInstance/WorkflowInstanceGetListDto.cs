namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 流程实例模型【根据流程运行流程】集合查询Dto
    /// </summary>
    public class WorkflowInstanceGetListDto
    {
       public bool IsDel { get; set; } // 状态【1：删除 0：未删除】
    }
}
