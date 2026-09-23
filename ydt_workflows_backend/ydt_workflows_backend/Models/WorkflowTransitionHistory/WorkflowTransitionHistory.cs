using JadeFramework.Core.Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ydt_workflows_backend.Models
{
    /// <summary>
    /// 流程流转历史模型
    /// </summary>
    [Table("ydt_workflow_transition_history")]
    public class WorkflowTransitionHistory
    {
        
        /// <summary>
        /// 当前节点ID
        /// </summary>
        public string TransitionId { get; set; }
        
        /// <summary>
        /// 主键
        /// </summary>
        public string InstanceId { get; set; }
        
        /// <summary>
        /// 流程ID
        /// </summary>
        public string FromNodeId { get; set; }
        
        /// <summary>
        /// 实例编号
        /// </summary>
        public int FromNodeType { get; set; }
        
        /// <summary>
        /// 当前节点类型
        /// </summary>
        public string FromNodName { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string ToNodeId { get; set; }
        
        /// <summary>
        /// 当前节点名称
        /// </summary>
        public int ToNodeType { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string ToNodeName { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int TransitionState { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int IsFinish { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string CreateUserName { get; set; }
    }

    /// <summary>
    /// 流程流转历史模型-流程流转历史模型表映射
    /// </summary>
    public sealed class WorkflowTransitionHistoryMapper : ClassMapper<WorkflowTransitionHistory>
    {
        public WorkflowTransitionHistoryMapper()
        {
            // 1、映射到ydt_workflow_transition_history
            Table("ydt_workflow_transition_history");

            // 2、自动映射【字段和属性】
            AutoMap();
        }
    }
}