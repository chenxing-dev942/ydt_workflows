using JadeFramework.Core.Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ydt_workflows_backend.Models
{
    /// <summary>
    /// 工作流模型
    /// </summary>
    [Table("ydt_workflow")]
    public class Workflow
    {
        
        /// <summary>
        /// 工作流ID
        /// </summary>
        public string FlowId { get; set; }
        
        /// <summary>
        /// 工作流编码
        /// </summary>
        public string FlowCode { get; set; }
        
        /// <summary>
        /// 分类ID
        /// </summary>
        public string CategoryId { get; set; }
        
        /// <summary>
        /// 表单ID
        /// </summary>
        public string FormId { get; set; }
        
        /// <summary>
        /// 流程名称
        /// </summary>
        public string FlowName { get; set; }
        
        /// <summary>
        /// 流程JSON内容
        /// </summary>
        public string FlowContent { get; set; }
        
        /// <summary>
        /// 流程版本 默认值为1
        /// </summary>
        public int FlowVersion { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        
        /// <summary>
        /// 是否启用 1：是 0: 否
        /// </summary>
        public int Enable { get; set; }
        
        /// <summary>
        /// 是否是旧版本
        /// </summary>
        public int IsOld { get; set; }
    }

    /// <summary>
    /// 工作流模型-工作流模型表映射
    /// </summary>
    public sealed class WorkflowMapper : ClassMapper<Workflow>
    {
        public WorkflowMapper()
        {
            // 1、映射到ydt_workflow
            Table("ydt_workflow");

            // 2、自动映射【字段和属性】
            AutoMap();
        }
    }
}