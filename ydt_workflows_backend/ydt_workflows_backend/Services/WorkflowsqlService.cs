using ydt_workflows_backend.Contexts;
using ydt_workflows_backend.Fixtrues;
using ydt_workflows_backend.Dtos;
using AutoMapper;
using AutoMapper;
using ydt_workflows_backend.Models;
using ydt_workflows_backend.CommonExceptions;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 工作流获取权限系统数据模型Service接口
    /// </summary>
    public class WorkflowsqlService : IWorkflowsqlService
    {
        /// <summary>
        /// 工作流固定类
        /// </summary>
        public WorkflowFixtrue _workflowFixtrue { get; set; }

        public IMapper _mapper { get; set; }

        public WorkflowsqlService(WorkflowFixtrue workflowFixtrue,
                               IMapper mapper)
        {
            _workflowFixtrue = workflowFixtrue;
            _mapper = mapper;
        }

        /// <summary>
        /// 工作流获取权限系统数据模型创建实现
        /// </summary>
        /// <param name="WorkflowsqlCreateDto"></param>
        /// <returns></returns>
        public async Task<bool> WorkflowsqlCreateAsync(WorkflowsqlCreateDto WorkflowsqlCreateDto)
        {
            // 1、WorkflowsqlCreateDto模型映射
            Workflowsql Workflowsql = _mapper.Map<Workflowsql>(WorkflowsqlCreateDto);

            //2、实现工作流获取权限系统数据模型创建
            return await  _workflowFixtrue.db.Workflowsqls.InsertAsync(Workflowsql);
        }

        public async Task<List<WorkflowsqlDto>> WorkflowsqlGetListAsync(WorkflowsqlGetListDto WorkflowsqlGetListDto)
        {
            
            //1、查询所有工作流获取权限系统数据模型
            IEnumerable<Workflowsql> Workflowsqls = await _workflowFixtrue.db.Workflowsqls.FindAllAsync();
            // 2、工作流获取权限系统数据模型映射
            List<WorkflowsqlDto> WorkflowsqlDtos = _mapper.Map<List<WorkflowsqlDto>>(Workflowsqls);

            // 3、返回工作流获取权限系统数据模型
            return WorkflowsqlDtos;
        }

        public async Task<WorkflowsqlPageDto> WorkflowsqlGetListPageAsync(WorkflowsqlGetListPageDto WorkflowsqlGetListPageDto)
        {
            // 1、工作流获取权限系统数据模型分页Dto映射
            WorkflowsqlGetListPage WorkflowsqlGetListPage = _mapper.Map<WorkflowsqlGetListPage>(WorkflowsqlGetListPageDto);

            // 2、查询分页工作流获取权限系统数据模型
            WorkflowsqlPage WorkflowsqlPage = await _workflowFixtrue.db.Workflowsqls.WorkflowsqlGetListPageAsync(WorkflowsqlGetListPage);

            // 3、工作流获取权限系统数据模型分页模型映射
            WorkflowsqlPageDto WorkflowsqlPageDto = _mapper.Map<WorkflowsqlPageDto>(WorkflowsqlPage);
            return WorkflowsqlPageDto;
        }
        public async Task<WorkflowsqlDto> WorkflowsqlGetAsync(string Name)
        {
            // 1、查询工作流获取权限系统数据模型
            Workflowsql Workflowsql = await _workflowFixtrue.db.Workflowsqls.FindAsync(m => m.Name == Name);

            // 2、映射工作流获取权限系统数据模型
            WorkflowsqlDto WorkflowsqlDto = _mapper.Map<WorkflowsqlDto>(Workflowsql);

            return WorkflowsqlDto;
        }
        public async Task<bool> WorkflowsqlUpdateAsync(WorkflowsqlUpdateDto WorkflowsqlUpdateDto, string Name)
        {
            // 1、查询工作流获取权限系统数据模型
            Workflowsql Workflowsql = await _workflowFixtrue.db.Workflowsqls.FindAsync(m => m.Name == Name);
            if (Workflowsql == null)
            {
                throw new CommonException("Workflowsql不存在");
            }

            // 2、工作流获取权限系统数据模型模型映射
            Workflowsql = _mapper.Map<WorkflowsqlUpdateDto, Workflowsql>(WorkflowsqlUpdateDto, Workflowsql);

            // 3、工作流获取权限系统数据模型更新实现
            return await _workflowFixtrue.db.Workflowsqls.UpdateAsync(Workflowsql);
        }
        public async Task<bool> WorkflowsqlDeleteAsync(List<string> Names)
        {
            // 1、开启事务
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                
                // 2、查询工作流获取权限系统数据模型
                var Workflowsqls = await _workflowFixtrue.db.Workflowsqls.FindAllAsync(m => Names.Contains(m.Name));
                foreach (var Workflowsql in Workflowsqls)
                {
                    // 3、删除工作流获取权限系统数据模型【真实删除】
                    await _workflowFixtrue.db.Workflowsqls.DeleteAsync(Workflowsql);
                }
                tran.Commit();
                return true;
            }
        }
    }
}