namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 流程操作历史模型集合查询结果Dto
    /// </summary>
    public class WorkflowOperationHistoryDto
    {
        public string OperationId { get; set; }
        public string InstanceId { get; set; }
        public string NodeId { get; set; }
        public string NodeName { get; set; }
        public int TransitionType { get; set; }
        public string Content { get; set; }
        public string CreateUserName { get; set; }}
}
