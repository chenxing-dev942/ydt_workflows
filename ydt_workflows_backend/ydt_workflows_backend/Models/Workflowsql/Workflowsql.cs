using JadeFramework.Core.Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ydt_workflows_backend.Models
{
    /// <summary>
    /// 工作流获取权限系统数据模型
    /// </summary>
    [Table("ydt_workflowsql")]
    public class Workflowsql
    {
        
        /// <summary>
        /// 当前节点ID
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 工作流ID
        /// </summary>
        public string FlowId { get; set; }
        
        /// <summary>
        /// 主键
        /// </summary>
        public string FlowSQL { get; set; }
        
        /// <summary>
        /// 流程ID
        /// </summary>
        public string Param { get; set; }
        
        /// <summary>
        /// 实例编号
        /// </summary>
        public bool SQLType { get; set; }
        
        /// <summary>
        /// 当前节点类型
        /// </summary>
        public int Status { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }
        
        /// <summary>
        /// 当前节点名称
        /// </summary>
        public long CreateUserId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public long CreateTime { get; set; }
    }

    /// <summary>
    /// 工作流获取权限系统数据模型-工作流获取权限系统数据模型表映射
    /// </summary>
    public sealed class WorkflowsqlMapper : ClassMapper<Workflowsql>
    {
        public WorkflowsqlMapper()
        {
            // 1、映射到ydt_workflowsql
            Table("ydt_workflowsql");

            // 2、自动映射【字段和属性】
            AutoMap();
        }
    }
}