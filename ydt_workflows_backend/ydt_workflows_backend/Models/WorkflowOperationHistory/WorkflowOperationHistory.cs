using JadeFramework.Core.Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ydt_workflows_backend.Models
{
    /// <summary>
    /// 流程操作历史模型
    /// </summary>
    [Table("ydt_workflow_operation_history")]
    public class WorkflowOperationHistory
    {
        
        /// <summary>
        /// 当前节点ID
        /// </summary>
        public string OperationId { get; set; }
        
        /// <summary>
        /// 主键
        /// </summary>
        public string InstanceId { get; set; }
        
        /// <summary>
        /// 流程ID
        /// </summary>
        public string NodeId { get; set; }
        
        /// <summary>
        /// 实例编号
        /// </summary>
        public string NodeName { get; set; }
        
        /// <summary>
        /// 当前节点类型
        /// </summary>
        public int TransitionType { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; }
        
        /// <summary>
        /// 当前节点名称
        /// </summary>
        public string CreateUserName { get; set; }
    }

    /// <summary>
    /// 流程操作历史模型-流程操作历史模型表映射
    /// </summary>
    public sealed class WorkflowOperationHistoryMapper : ClassMapper<WorkflowOperationHistory>
    {
        public WorkflowOperationHistoryMapper()
        {
            // 1、映射到ydt_workflow_operation_history
            Table("ydt_workflow_operation_history");

            // 2、自动映射【字段和属性】
            AutoMap();
        }
    }
}