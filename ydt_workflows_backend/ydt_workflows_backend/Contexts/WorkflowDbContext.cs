using JadeFramework.Dapper.DbContext;
using JadeFramework.Dapper.SqlGenerator;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;
using ydt_workflows_backend.Repositorise ;

namespace ydt_workflows_backend.Contexts
{
    /// <summary>
    /// 工作流上下文实现
    /// </summary>
    public class WorkflowDbContext : DapperDbContext, IWorkflowDbContext
    {
        /// <summary>
        /// 1、数据库选择配置
        /// </summary>
        private readonly SqlGeneratorConfig sqlGeneratorConfig = new SqlGeneratorConfig
        {
            SqlConnector = ESqlConnector.MySQL, // 使用MySQL
            UseQuotationMarks = true // 表和列名使用引号
        };

        public WorkflowDbContext(string ConnectionString) : 
            base(new MySqlConnection(ConnectionString))
        {
        }
        /// <summary>
        /// 实现部门模型仓储
        /// </summary>
         public IDeptRepository Depts => new DeptRepository(Connection, sqlGeneratorConfig);
        /// <summary>
        /// 实现资源【菜单】模型仓储
        /// </summary>
         public IResourceRepository Resources => new ResourceRepository(Connection, sqlGeneratorConfig);
        /// <summary>
        /// 实现角色模型仓储
        /// </summary>
         public IRoleRepository Roles => new RoleRepository(Connection, sqlGeneratorConfig);
        /// <summary>
        /// 实现角色资源关联模型仓储
        /// </summary>
         public IRoleResourceRepository RoleResources => new RoleResourceRepository(Connection, sqlGeneratorConfig);
        /// <summary>
        /// 实现子系统模型仓储
        /// </summary>
         public ISystemsRepository Systemss => new SystemsRepository(Connection, sqlGeneratorConfig);
        /// <summary>
        /// 实现用户模型仓储
        /// </summary>
         public IUserRepository Users => new UserRepository(Connection, sqlGeneratorConfig);
        /// <summary>
        /// 实现用户部门关联模型仓储
        /// </summary>
         public IUserDeptRepository UserDepts => new UserDeptRepository(Connection, sqlGeneratorConfig);
        /// <summary>
        /// 实现用户角色关联模型仓储
        /// </summary>
         public IUserRoleRepository UserRoles => new UserRoleRepository(Connection, sqlGeneratorConfig);
        /// <summary>
        /// 实现工作流模型仓储
        /// </summary>
         public IWorkflowRepository Workflows => new WorkflowRepository(Connection, sqlGeneratorConfig);
        /// <summary>
        /// 实现流程委托模型仓储
        /// </summary>
         public IWorkflowAssignRepository WorkflowAssigns => new WorkflowAssignRepository(Connection, sqlGeneratorConfig);
        /// <summary>
        /// 实现流程分类模型仓储
        /// </summary>
         public IWorkflowCategoryRepository WorkflowCategorys => new WorkflowCategoryRepository(Connection, sqlGeneratorConfig);
        /// <summary>
        /// 实现流程表单模型仓储
        /// </summary>
         public IWorkflowFormRepository WorkflowForms => new WorkflowFormRepository(Connection, sqlGeneratorConfig);
        /// <summary>
        /// 实现流程实例模型【根据流程运行流程】仓储
        /// </summary>
         public IWorkflowInstanceRepository WorkflowInstances => new WorkflowInstanceRepository(Connection, sqlGeneratorConfig);
        /// <summary>
        /// 实现流程实例表单关联模型仓储
        /// </summary>
         public IWorkflowInstanceFormRepository WorkflowInstanceForms => new WorkflowInstanceFormRepository(Connection, sqlGeneratorConfig);
        /// <summary>
        /// 实现流程通知节点模型仓储
        /// </summary>
         public IWorkflowNoticeRepository WorkflowNotices => new WorkflowNoticeRepository(Connection, sqlGeneratorConfig);
        /// <summary>
        /// 实现流程操作历史模型仓储
        /// </summary>
         public IWorkflowOperationHistoryRepository WorkflowOperationHistorys => new WorkflowOperationHistoryRepository(Connection, sqlGeneratorConfig);
        /// <summary>
        /// 实现流程流转历史模型仓储
        /// </summary>
         public IWorkflowTransitionHistoryRepository WorkflowTransitionHistorys => new WorkflowTransitionHistoryRepository(Connection, sqlGeneratorConfig);
        /// <summary>
        /// 实现催办记录模型仓储
        /// </summary>
         public IWorkflowUrgeRepository WorkflowUrges => new WorkflowUrgeRepository(Connection, sqlGeneratorConfig);
        /// <summary>
        /// 实现工作流获取权限系统数据模型仓储
        /// </summary>
         public IWorkflowsqlRepository Workflowsqls => new WorkflowsqlRepository(Connection, sqlGeneratorConfig);
    }
}
