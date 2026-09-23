using ydt_workflows_backend.Dtos;

namespace ydt_workflows_backend.Models
{
    /// <summary>
    /// 工作流获取权限系统数据模型分页模型
    /// </summary>
    public class WorkflowsqlPage
    {
        /// <summary>
        /// 1、工作流获取权限系统数据模型集合
        /// </summary>
        public IEnumerable<Workflowsql> Workflowsqls { get; set; }

        /// <summary>
        /// 2、总页数
        /// </summary>
        public int TotalPages { set; get; }

        /// <summary>
        /// 3、总条数
        /// </summary>
        public int TotalItems { set; get; }

        /// <summary>
        /// 4、当前页【1 2 3】
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 5、每页显示的条数【默认显示10条】
        /// </summary>
        public int PageSize { set; get; }

        public WorkflowsqlPage()
        {
            Workflowsqls = new List<Workflowsql>();
        }
    }
}
