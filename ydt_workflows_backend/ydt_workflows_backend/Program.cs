using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using ydt_workflows_backend.CommonControllersExtensions;
using ydt_workflows_backend.CommonExceptions;
using ydt_workflows_backend.CommonResults;
using ydt_workflows_backend.Contexts;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Fixtrues;
using ydt_workflows_backend.Services;
using ydt_workflows_backend.Services.IServices;

namespace ydt_workflows_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1、使用CommonController模块
            builder.Services.AddCommonControllers();

            // 2、实现配置 Services 和 Fixtrues
            builder.Services.AddScoped(typeof(WorkflowFixtrue));
            builder.Services.AddScoped<IDeptService, DeptService>();
            builder.Services.AddScoped<IResourceService, ResourceService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IRoleResourceService, RoleResourceService>();
            builder.Services.AddScoped<ISystemsService, SystemsService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserDeptService, UserDeptService>();
            builder.Services.AddScoped<IUserRoleService, UserRoleService>();
            builder.Services.AddScoped<IWorkflowService, WorkflowService>();
            builder.Services.AddScoped<IWorkflowAssignService, WorkflowAssignService>();
            builder.Services.AddScoped<IWorkflowCategoryService, WorkflowCategoryService>();
            builder.Services.AddScoped<IWorkflowFormService, WorkflowFormService>();
            builder.Services.AddScoped<IWorkflowInstanceService, WorkflowInstanceService>();
            builder.Services.AddScoped<IWorkflowInstanceFormService, WorkflowInstanceFormService>();
            builder.Services.AddScoped<IWorkflowNoticeService, WorkflowNoticeService>();
            builder.Services.AddScoped<IWorkflowOperationHistoryService, WorkflowOperationHistoryService>();
            builder.Services.AddScoped<IWorkflowTransitionHistoryService, WorkflowTransitionHistoryService>();
            builder.Services.AddScoped<IWorkflowUrgeService, WorkflowUrgeService>();
            builder.Services.AddScoped<IWorkflowsqlService, WorkflowsqlService>();

            builder.Services.AddScoped<ILoginService, LoginService>();

            // 3、加载MappingProfile
            // 传入MappingProfile所在程序集，自动找到所有继承Profile的类
            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

            // 4、配置身份认证Authentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromHours(2); // 2小时过期【固定】
            });

            // 5、配置Authorization
            builder.Services.AddAuthorization();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            // 4.1、配置身份认证Authentication中间件
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
