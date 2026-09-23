namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 子系统模型集合查询Dto
    /// </summary>
    public class SystemsGetListDto
    {
       public bool IsDel { get; set; } // 状态【1：删除 0：未删除】
    }
}
