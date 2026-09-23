using JadeFramework.Dapper.DbContext;
using ydt_workflows_backend.Repositorise;

namespace ydt_workflows_backend.Contexts
{
    /// <summary>
    /// 工作流上下文接口
    /// </summary>
    public interface IWorkflowDbContext : IDapperDbContext
    {
        //依赖IDeptRepository
        public IDeptRepository Depts { get; }
        //依赖IResourceRepository
        public IResourceRepository Resources { get; }
        //依赖IRoleRepository
        public IRoleRepository Roles { get; }
        //依赖IRoleResourceRepository
        public IRoleResourceRepository RoleResources { get; }
        //依赖ISystemsRepository
        public ISystemsRepository Systemss { get; }
        //依赖IUserRepository
        public IUserRepository Users { get; }
        //依赖IUserDeptRepository
        public IUserDeptRepository UserDepts { get; }
        //依赖IUserRoleRepository
        public IUserRoleRepository UserRoles { get; }
        //依赖IWorkflowRepository
        public IWorkflowRepository Workflows { get; }
        //依赖IWorkflowAssignRepository
        public IWorkflowAssignRepository WorkflowAssigns { get; }
        //依赖IWorkflowCategoryRepository
        public IWorkflowCategoryRepository WorkflowCategorys { get; }
        //依赖IWorkflowFormRepository
        public IWorkflowFormRepository WorkflowForms { get; }
        //依赖IWorkflowInstanceRepository
        public IWorkflowInstanceRepository WorkflowInstances { get; }
        //依赖IWorkflowInstanceFormRepository
        public IWorkflowInstanceFormRepository WorkflowInstanceForms { get; }
        //依赖IWorkflowNoticeRepository
        public IWorkflowNoticeRepository WorkflowNotices { get; }
        //依赖IWorkflowOperationHistoryRepository
        public IWorkflowOperationHistoryRepository WorkflowOperationHistorys { get; }
        //依赖IWorkflowTransitionHistoryRepository
        public IWorkflowTransitionHistoryRepository WorkflowTransitionHistorys { get; }
        //依赖IWorkflowUrgeRepository
        public IWorkflowUrgeRepository WorkflowUrges { get; }
        //依赖IWorkflowsqlRepository
        public IWorkflowsqlRepository Workflowsqls { get; }
    }
}
