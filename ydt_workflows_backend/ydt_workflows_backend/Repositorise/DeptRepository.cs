using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using System.Data;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Repositorise
{
    /// <summary>
    /// 部门模型仓储实现
    /// </summary>
    public class DeptRepository : DapperRepository<Dept>, IDeptRepository
    {
        /// <summary>
        /// 1、IDbConnection : Dapper实现数据库操作，连接对象
        /// 2、SqlGeneratorConfig ：配置数据库类型。MySQL SqlServer Oracle
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="config"></param>
        public DeptRepository(IDbConnection connection, 
            SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<DeptPage> DeptGetListPageAsync(DeptGetListPage DeptGetListPage)
        {
            //1、创建分页结果
            DeptPage page = new DeptPage()
            {
                PageIndex = DeptGetListPage.PageIndex,
                PageSize = DeptGetListPage.PageSize
            };

            // 2、设置部门模型名查询参数
            string sqlWhere = " WHERE 1=1 ";
            
            //if (!DeptGetListPage.DeptName.IsNullOrEmpty())
            //{
            //    sqlWhere += $@" AND DeptName LIKE '%{DeptGetListPage.DeptName.TrimBlank()}%' ";
            //}
            
            // 4、实现分页查询
            string sql = $"SELECT * FROM ydt_dept {sqlWhere} LIMIT {DeptGetListPage.OffSet()},{DeptGetListPage.PageSize}";
            page.Depts = await this.QueryAsync(sql);

            // 5、查询所有条数
            page.TotalItems = await this.ExecuteScalarAsync<int>($"SELECT COUNT(1) FROM ydt_dept {sqlWhere} ");
            
            return page;
        }
    }
}
