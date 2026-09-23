using AutoMapper;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 1、配置映射关系
    ///  Dto和Model之间的映射
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Dept模型的映射
            CreateMap<DeptCreateDto,Dept>();
            
            CreateMap<Dept, DeptDto>();
            CreateMap<DeptGetListPageDto, DeptGetListPage>();
            CreateMap<DeptPage,DeptPageDto>();
            CreateMap<DeptUpdateDto, Dept>();
            // Resource模型的映射
            CreateMap<ResourceCreateDto,Resource>();
            
            CreateMap<Resource, ResourceDto>();
            CreateMap<ResourceGetListPageDto, ResourceGetListPage>();
            CreateMap<ResourcePage,ResourcePageDto>();
            CreateMap<ResourceUpdateDto, Resource>();
            // Role模型的映射
            CreateMap<RoleCreateDto,Role>();
            
            CreateMap<Role, RoleDto>();
            CreateMap<RoleGetListPageDto, RoleGetListPage>();
            CreateMap<RolePage,RolePageDto>();
            CreateMap<RoleUpdateDto, Role>();
            // RoleResource模型的映射
            CreateMap<RoleResourceCreateDto,RoleResource>();
            
            CreateMap<RoleResource, RoleResourceDto>();
            CreateMap<RoleResourceGetListPageDto, RoleResourceGetListPage>();
            CreateMap<RoleResourcePage,RoleResourcePageDto>();
            CreateMap<RoleResourceUpdateDto, RoleResource>();
            // Systems模型的映射
            CreateMap<SystemsCreateDto,Systems>();
            
            CreateMap<Systems, SystemsDto>();
            CreateMap<SystemsGetListPageDto, SystemsGetListPage>();
            CreateMap<SystemsPage,SystemsPageDto>();
            CreateMap<SystemsUpdateDto, Systems>();
            // User模型的映射
            CreateMap<UserCreateDto,User>();
            
            CreateMap<User, UserLoginResultDto>();
            CreateMap<User, UserDto>();
            CreateMap<UserGetListPageDto, UserGetListPage>();
            CreateMap<UserPage,UserPageDto>();
            CreateMap<UserUpdateDto, User>();
            // UserDept模型的映射
            CreateMap<UserDeptCreateDto,UserDept>();
            
            CreateMap<UserDept, UserDeptDto>();
            CreateMap<UserDeptGetListPageDto, UserDeptGetListPage>();
            CreateMap<UserDeptPage,UserDeptPageDto>();
            CreateMap<UserDeptUpdateDto, UserDept>();
            // UserRole模型的映射
            CreateMap<UserRoleCreateDto,UserRole>();
            
            CreateMap<UserRole, UserRoleDto>();
            CreateMap<UserRoleGetListPageDto, UserRoleGetListPage>();
            CreateMap<UserRolePage,UserRolePageDto>();
            CreateMap<UserRoleUpdateDto, UserRole>();
            // Workflow模型的映射
            CreateMap<WorkflowCreateDto,Workflow>();
            
            CreateMap<Workflow, WorkflowDto>();
            CreateMap<WorkflowGetListPageDto, WorkflowGetListPage>();
            CreateMap<WorkflowPage,WorkflowPageDto>();
            CreateMap<WorkflowUpdateDto, Workflow>();
            // WorkflowAssign模型的映射
            CreateMap<WorkflowAssignCreateDto,WorkflowAssign>();
            
            CreateMap<WorkflowAssign, WorkflowAssignDto>();
            CreateMap<WorkflowAssignGetListPageDto, WorkflowAssignGetListPage>();
            CreateMap<WorkflowAssignPage,WorkflowAssignPageDto>();
            CreateMap<WorkflowAssignUpdateDto, WorkflowAssign>();
            // WorkflowCategory模型的映射
            CreateMap<WorkflowCategoryCreateDto,WorkflowCategory>();
            
            CreateMap<WorkflowCategory, WorkflowCategoryDto>();
            CreateMap<WorkflowCategoryGetListPageDto, WorkflowCategoryGetListPage>();
            CreateMap<WorkflowCategoryPage,WorkflowCategoryPageDto>();
            CreateMap<WorkflowCategoryUpdateDto, WorkflowCategory>();
            // WorkflowForm模型的映射
            CreateMap<WorkflowFormCreateDto,WorkflowForm>();
            
            CreateMap<WorkflowForm, WorkflowFormDto>();
            CreateMap<WorkflowFormGetListPageDto, WorkflowFormGetListPage>();
            CreateMap<WorkflowFormPage,WorkflowFormPageDto>();
            CreateMap<WorkflowFormUpdateDto, WorkflowForm>();
            // WorkflowInstance模型的映射
            CreateMap<WorkflowInstanceCreateDto,WorkflowInstance>();
            
            CreateMap<WorkflowInstance, WorkflowInstanceDto>();
            CreateMap<WorkflowInstanceGetListPageDto, WorkflowInstanceGetListPage>();
            CreateMap<WorkflowInstancePage,WorkflowInstancePageDto>();
            CreateMap<WorkflowInstanceUpdateDto, WorkflowInstance>();
            // WorkflowInstanceForm模型的映射
            CreateMap<WorkflowInstanceFormCreateDto,WorkflowInstanceForm>();
            
            CreateMap<WorkflowInstanceForm, WorkflowInstanceFormDto>();
            CreateMap<WorkflowInstanceFormGetListPageDto, WorkflowInstanceFormGetListPage>();
            CreateMap<WorkflowInstanceFormPage,WorkflowInstanceFormPageDto>();
            CreateMap<WorkflowInstanceFormUpdateDto, WorkflowInstanceForm>();
            // WorkflowNotice模型的映射
            CreateMap<WorkflowNoticeCreateDto,WorkflowNotice>();
            
            CreateMap<WorkflowNotice, WorkflowNoticeDto>();
            CreateMap<WorkflowNoticeGetListPageDto, WorkflowNoticeGetListPage>();
            CreateMap<WorkflowNoticePage,WorkflowNoticePageDto>();
            CreateMap<WorkflowNoticeUpdateDto, WorkflowNotice>();
            // WorkflowOperationHistory模型的映射
            CreateMap<WorkflowOperationHistoryCreateDto,WorkflowOperationHistory>();
            
            CreateMap<WorkflowOperationHistory, WorkflowOperationHistoryDto>();
            CreateMap<WorkflowOperationHistoryGetListPageDto, WorkflowOperationHistoryGetListPage>();
            CreateMap<WorkflowOperationHistoryPage,WorkflowOperationHistoryPageDto>();
            CreateMap<WorkflowOperationHistoryUpdateDto, WorkflowOperationHistory>();
            // WorkflowTransitionHistory模型的映射
            CreateMap<WorkflowTransitionHistoryCreateDto,WorkflowTransitionHistory>();
            
            CreateMap<WorkflowTransitionHistory, WorkflowTransitionHistoryDto>();
            CreateMap<WorkflowTransitionHistoryGetListPageDto, WorkflowTransitionHistoryGetListPage>();
            CreateMap<WorkflowTransitionHistoryPage,WorkflowTransitionHistoryPageDto>();
            CreateMap<WorkflowTransitionHistoryUpdateDto, WorkflowTransitionHistory>();
            // WorkflowUrge模型的映射
            CreateMap<WorkflowUrgeCreateDto,WorkflowUrge>();
            
            CreateMap<WorkflowUrge, WorkflowUrgeDto>();
            CreateMap<WorkflowUrgeGetListPageDto, WorkflowUrgeGetListPage>();
            CreateMap<WorkflowUrgePage,WorkflowUrgePageDto>();
            CreateMap<WorkflowUrgeUpdateDto, WorkflowUrge>();
            // Workflowsql模型的映射
            CreateMap<WorkflowsqlCreateDto,Workflowsql>();
            
            CreateMap<Workflowsql, WorkflowsqlDto>();
            CreateMap<WorkflowsqlGetListPageDto, WorkflowsqlGetListPage>();
            CreateMap<WorkflowsqlPage,WorkflowsqlPageDto>();
            CreateMap<WorkflowsqlUpdateDto, Workflowsql>();

        }
    }
}
