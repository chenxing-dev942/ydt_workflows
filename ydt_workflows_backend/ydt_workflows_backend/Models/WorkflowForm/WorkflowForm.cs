using JadeFramework.Core.Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ydt_workflows_backend.Models
{
    /// <summary>
    /// 流程表单模型
    /// </summary>
    [Table("ydt_workflow_form")]
    public class WorkflowForm
    {
        
        /// <summary>
        /// 工作流编码
        /// </summary>
        public string FormId { get; set; }
        
        /// <summary>
        /// 分类ID
        /// </summary>
        public string FormName { get; set; }
        
        /// <summary>
        /// 表单ID
        /// </summary>
        public int FormType { get; set; }
        
        /// <summary>
        /// 是否是旧版本
        /// </summary>
        public string Content { get; set; }
        
        /// <summary>
        /// 流程名称
        /// </summary>
        public string OriginalContent { get; set; }
        
        /// <summary>
        /// 流程JSON内容
        /// </summary>
        public string FormUrl { get; set; }
    }

    /// <summary>
    /// 流程表单模型-流程表单模型表映射
    /// </summary>
    public sealed class WorkflowFormMapper : ClassMapper<WorkflowForm>
    {
        public WorkflowFormMapper()
        {
            // 1、映射到ydt_workflow_form
            Table("ydt_workflow_form");

            // 2、自动映射【字段和属性】
            AutoMap();
        }
    }
}