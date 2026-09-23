using JadeFramework.Core.Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ydt_workflows_backend.Models
{
    /// <summary>
    /// 催办记录模型
    /// </summary>
    [Table("ydt_workflow_urge")]
    public class WorkflowUrge
    {
        
        /// <summary>
        /// 当前节点ID
        /// </summary>
        public string UrgeId { get; set; }
        
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
        public string Sender { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string UrgeUser { get; set; }
        
        /// <summary>
        /// 当前节点名称
        /// </summary>
        public int UrgeType { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string UrgeContent { get; set; }
    }

    /// <summary>
    /// 催办记录模型-催办记录模型表映射
    /// </summary>
    public sealed class WorkflowUrgeMapper : ClassMapper<WorkflowUrge>
    {
        public WorkflowUrgeMapper()
        {
            // 1、映射到ydt_workflow_urge
            Table("ydt_workflow_urge");

            // 2、自动映射【字段和属性】
            AutoMap();
        }
    }
}