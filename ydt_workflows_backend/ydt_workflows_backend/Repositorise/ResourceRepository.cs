using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using System.Data;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 资源【菜单】模型仓储实现
    /// </summary>
    public class ResourceRepository : DapperRepository<Resource>, IResourceRepository
    {
        /// <summary>
        /// 1、IDbConnection : Dapper实现数据库操作，连接对象
        /// 2、SqlGeneratorConfig ：配置数据库类型。MySQL SqlServer Oracle
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="config"></param>
        public ResourceRepository(IDbConnection connection, 
            SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<ResourcePage> ResourceGetListPageAsync(ResourceGetListPage ResourceGetListPage)
        {
            //1、创建分页结果
            ResourcePage page = new ResourcePage()
            {
                PageIndex = ResourceGetListPage.PageIndex,
                PageSize = ResourceGetListPage.PageSize
            };

            // 2、设置资源【菜单】模型名查询参数
            string sqlWhere = " WHERE 1=1 ";
            
            //if (!ResourceGetListPage.ResourceName.IsNullOrEmpty())
            //{
            //    sqlWhere += $@" AND ResourceName LIKE '%{ResourceGetListPage.ResourceName.TrimBlank()}%' ";
            //}
            
            // 4、实现分页查询
            string sql = $"SELECT * FROM ydt_resource {sqlWhere} LIMIT {ResourceGetListPage.OffSet()},{ResourceGetListPage.PageSize}";
            page.Resources = await this.QueryAsync(sql);

            // 5、查询所有条数
            page.TotalItems = await this.ExecuteScalarAsync<int>($"SELECT COUNT(1) FROM ydt_resource {sqlWhere} ");
            
            return page;
        }
    }
}
