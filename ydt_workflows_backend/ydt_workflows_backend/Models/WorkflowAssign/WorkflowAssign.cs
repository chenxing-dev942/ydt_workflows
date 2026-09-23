using JadeFramework.Core.Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ydt_workflows_backend.Models
{
    /// <summary>
    /// 流程委托模型
    /// </summary>
    [Table("ydt_workflow_assign")]
    public class WorkflowAssign
    {
        
        /// <summary>
        /// 主键
        /// </summary>
        public string AssignId { get; set; }
        
        /// <summary>
        /// 工作流编码
        /// </summary>
        public string FlowId { get; set; }
        
        /// <summary>
        /// 分类ID
        /// </summary>
        public string InstanceId { get; set; }
        
        /// <summary>
        /// 表单ID
        /// </summary>
        public string NodeId { get; set; }
        
        /// <summary>
        /// 流程名称
        /// </summary>
        public string NodeName { get; set; }
        
        /// <summary>
        /// 流程JSON内容
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// 流程版本 默认值为1
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        public string AssignUserId { get; set; }
        
        /// <summary>
        /// 是否启用 1：是 0: 否
        /// </summary>
        public string AssignUserName { get; set; }
        
        /// <summary>
        /// 是否是旧版本
        /// </summary>
        public string Content { get; set; }
    }

    /// <summary>
    /// 流程委托模型-流程委托模型表映射
    /// </summary>
    public sealed class WorkflowAssignMapper : ClassMapper<WorkflowAssign>
    {
        public WorkflowAssignMapper()
        {
            // 1、映射到ydt_workflow_assign
            Table("ydt_workflow_assign");

            // 2、自动映射【字段和属性】
            AutoMap();
        }
    }
}