using Microsoft.Extensions.DependencyInjection;
using ydt_workflows_backend.CommonExceptions;
using ydt_workflows_backend.CommonResults;

namespace ydt_workflows_backend.CommonControllersExtensions
{
    /// <summary>
    /// CommonCtroller模块封装
    /// </summary>
    public static class CommonControllersServiceCollectionExtensions
    {
       public static IServiceCollection AddCommonControllers(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<CommonResultFilter>();
                options.Filters.Add<CommonExceptionFilter>();
            }).AddJsonOptions(options => {
                // 3、配置通用Json格式
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            return services;
        }
    }
}
