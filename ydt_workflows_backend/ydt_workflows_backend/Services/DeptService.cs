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
    /// 部门模型Service接口
    /// </summary>
    public class DeptService : IDeptService
    {
        /// <summary>
        /// 工作流固定类
        /// </summary>
        public WorkflowFixtrue _workflowFixtrue { get; set; }

        public IMapper _mapper { get; set; }

        public DeptService(WorkflowFixtrue workflowFixtrue,
                               IMapper mapper)
        {
            _workflowFixtrue = workflowFixtrue;
            _mapper = mapper;
        }

        /// <summary>
        /// 部门模型创建实现
        /// </summary>
        /// <param name="DeptCreateDto"></param>
        /// <returns></returns>
        public async Task<bool> DeptCreateAsync(DeptCreateDto DeptCreateDto)
        {
            // 1、DeptCreateDto模型映射
            Dept Dept = _mapper.Map<Dept>(DeptCreateDto);

            //2、实现部门模型创建
            return await  _workflowFixtrue.db.Depts.InsertAsync(Dept);
        }

        public async Task<List<DeptDto>> DeptGetListAsync(DeptGetListDto DeptGetListDto)
        {
            
            //1、查询所有部门模型
            IEnumerable<Dept> Depts = await _workflowFixtrue.db.Depts.FindAllAsync(u => u.IsDel == DeptGetListDto.IsDel);
            // 2、部门模型映射
            List<DeptDto> DeptDtos = _mapper.Map<List<DeptDto>>(Depts);

            // 3、返回部门模型
            return DeptDtos;
        }

        public async Task<DeptPageDto> DeptGetListPageAsync(DeptGetListPageDto DeptGetListPageDto)
        {
            // 1、部门模型分页Dto映射
            DeptGetListPage DeptGetListPage = _mapper.Map<DeptGetListPage>(DeptGetListPageDto);

            // 2、查询分页部门模型
            DeptPage DeptPage = await _workflowFixtrue.db.Depts.DeptGetListPageAsync(DeptGetListPage);

            // 3、部门模型分页模型映射
            DeptPageDto DeptPageDto = _mapper.Map<DeptPageDto>(DeptPage);
            return DeptPageDto;
        }
        public async Task<DeptDto> DeptGetAsync(long DeptId)
        {
            // 1、查询部门模型
            Dept Dept = await _workflowFixtrue.db.Depts.FindAsync(m => m.DeptId == DeptId);

            // 2、映射部门模型
            DeptDto DeptDto = _mapper.Map<DeptDto>(Dept);

            return DeptDto;
        }
        public async Task<bool> DeptUpdateAsync(DeptUpdateDto DeptUpdateDto, long DeptId)
        {
            // 1、查询部门模型
            Dept Dept = await _workflowFixtrue.db.Depts.FindAsync(m => m.DeptId == DeptId);
            if (Dept == null)
            {
                throw new CommonException("Dept不存在");
            }

            // 2、部门模型模型映射
            Dept = _mapper.Map<DeptUpdateDto, Dept>(DeptUpdateDto, Dept);

            // 3、部门模型更新实现
            return await _workflowFixtrue.db.Depts.UpdateAsync(Dept);
        }
        public async Task<bool> DeptDeleteAsync(List<long> DeptIds)
        {
            // 1、开启事务
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                
                // 2、查询部门模型
                var Depts = await _workflowFixtrue.db.Depts.FindAllAsync(m => m.IsDel == false && DeptIds.Contains(m.DeptId));
                foreach (var Dept in Depts)
                {
                    // 3、删除部门模型【逻辑删除】
                    Dept.IsDel = true; // 1:删除状态
                    await _workflowFixtrue.db.Depts.UpdateAsync(Dept, tran);
                }
                tran.Commit();
                return true;
            }
        }
    }
}