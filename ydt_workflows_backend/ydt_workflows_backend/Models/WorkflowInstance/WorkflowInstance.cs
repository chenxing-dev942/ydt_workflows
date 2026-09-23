using JadeFramework.Core.Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ydt_workflows_backend.Models
{
    /// <summary>
    /// 流程实例模型【根据流程运行流程】
    /// </summary>
    [Table("ydt_workflow_instance")]
    public class WorkflowInstance
    {
        
        /// <summary>
        /// 主键
        /// </summary>
        public string InstanceId { get; set; }
        
        /// <summary>
        /// 流程ID
        /// </summary>
        public string FlowId { get; set; }
        
        /// <summary>
        /// 实例编号
        /// </summary>
        public string Code { get; set; }
        
        /// <summary>
        /// 当前节点ID
        /// </summary>
        public string ActivityId { get; set; }
        
        /// <summary>
        /// 当前节点类型
        /// </summary>
        public int ActivityType { get; set; }
        
        /// <summary>
        /// 当前节点名称
        /// </summary>
        public string ActivityName { get; set; }
        
        /// <summary>
        /// 上一个节点ID
        /// </summary>
        public string PreviousId { get; set; }
        
        /// <summary>
        /// 执行人
        ///为0表示全部人员
        /// </summary>
        public string MakerList { get; set; }
        
        /// <summary>
        /// 流程JSON内容
        /// </summary>
        public string FlowContent { get; set; }
        
        /// <summary>
        /// 流程版本
        /// </summary>
        public int FlowVersion { get; set; }
        
        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string CreateUserName { get; set; }
        
        /// <summary>
        /// 更新时间
        /// </summary>
        public long UpdateTime { get; set; }
    }

    /// <summary>
    /// 流程实例模型【根据流程运行流程】-流程实例模型【根据流程运行流程】表映射
    /// </summary>
    public sealed class WorkflowInstanceMapper : ClassMapper<WorkflowInstance>
    {
        public WorkflowInstanceMapper()
        {
            // 1、映射到ydt_workflow_instance
            Table("ydt_workflow_instance");

            // 2、自动映射【字段和属性】
            AutoMap();
        }
    }
}