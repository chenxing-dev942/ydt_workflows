namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 流程实例模型【根据流程运行流程】创建Dto
    /// </summary>
    public class WorkflowInstanceCreateDto
    {
        public string InstanceId { get; set; }
        public string FlowId { get; set; }
        public string Code { get; set; }
        public string ActivityId { get; set; }
        public int ActivityType { get; set; }
        public string ActivityName { get; set; }
        public string PreviousId { get; set; }
        public string MakerList { get; set; }
        public string FlowContent { get; set; }
        public int FlowVersion { get; set; }
        public string CreateUserName { get; set; }
        public long UpdateTime { get; set; }}
}
