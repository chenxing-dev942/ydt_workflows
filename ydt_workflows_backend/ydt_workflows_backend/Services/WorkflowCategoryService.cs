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
    /// 流程分类模型Service接口
    /// </summary>
    public class WorkflowCategoryService : IWorkflowCategoryService
    {
        /// <summary>
        /// 工作流固定类
        /// </summary>
        public WorkflowFixtrue _workflowFixtrue { get; set; }

        public IMapper _mapper { get; set; }

        public WorkflowCategoryService(WorkflowFixtrue workflowFixtrue,
                               IMapper mapper)
        {
            _workflowFixtrue = workflowFixtrue;
            _mapper = mapper;
        }

        /// <summary>
        /// 流程分类模型创建实现
        /// </summary>
        /// <param name="WorkflowCategoryCreateDto"></param>
        /// <returns></returns>
        public async Task<bool> WorkflowCategoryCreateAsync(WorkflowCategoryCreateDto WorkflowCategoryCreateDto)
        {
            // 1、WorkflowCategoryCreateDto模型映射
            WorkflowCategory WorkflowCategory = _mapper.Map<WorkflowCategory>(WorkflowCategoryCreateDto);

            //2、实现流程分类模型创建
            return await  _workflowFixtrue.db.WorkflowCategorys.InsertAsync(WorkflowCategory);
        }

        public async Task<List<WorkflowCategoryDto>> WorkflowCategoryGetListAsync(WorkflowCategoryGetListDto WorkflowCategoryGetListDto)
        {
            
            //1、查询所有流程分类模型
            IEnumerable<WorkflowCategory> WorkflowCategorys = await _workflowFixtrue.db.WorkflowCategorys.FindAllAsync();
            // 2、流程分类模型映射
            List<WorkflowCategoryDto> WorkflowCategoryDtos = _mapper.Map<List<WorkflowCategoryDto>>(WorkflowCategorys);

            // 3、返回流程分类模型
            return WorkflowCategoryDtos;
        }

        public async Task<WorkflowCategoryPageDto> WorkflowCategoryGetListPageAsync(WorkflowCategoryGetListPageDto WorkflowCategoryGetListPageDto)
        {
            // 1、流程分类模型分页Dto映射
            WorkflowCategoryGetListPage WorkflowCategoryGetListPage = _mapper.Map<WorkflowCategoryGetListPage>(WorkflowCategoryGetListPageDto);

            // 2、查询分页流程分类模型
            WorkflowCategoryPage WorkflowCategoryPage = await _workflowFixtrue.db.WorkflowCategorys.WorkflowCategoryGetListPageAsync(WorkflowCategoryGetListPage);

            // 3、流程分类模型分页模型映射
            WorkflowCategoryPageDto WorkflowCategoryPageDto = _mapper.Map<WorkflowCategoryPageDto>(WorkflowCategoryPage);
            return WorkflowCategoryPageDto;
        }
        public async Task<WorkflowCategoryDto> WorkflowCategoryGetAsync(string CategoryId)
        {
            // 1、查询流程分类模型
            WorkflowCategory WorkflowCategory = await _workflowFixtrue.db.WorkflowCategorys.FindAsync(m => m.CategoryId == CategoryId);

            // 2、映射流程分类模型
            WorkflowCategoryDto WorkflowCategoryDto = _mapper.Map<WorkflowCategoryDto>(WorkflowCategory);

            return WorkflowCategoryDto;
        }
        public async Task<bool> WorkflowCategoryUpdateAsync(WorkflowCategoryUpdateDto WorkflowCategoryUpdateDto, string CategoryId)
        {
            // 1、查询流程分类模型
            WorkflowCategory WorkflowCategory = await _workflowFixtrue.db.WorkflowCategorys.FindAsync(m => m.CategoryId == CategoryId);
            if (WorkflowCategory == null)
            {
                throw new CommonException("WorkflowCategory不存在");
            }

            // 2、流程分类模型模型映射
            WorkflowCategory = _mapper.Map<WorkflowCategoryUpdateDto, WorkflowCategory>(WorkflowCategoryUpdateDto, WorkflowCategory);

            // 3、流程分类模型更新实现
            return await _workflowFixtrue.db.WorkflowCategorys.UpdateAsync(WorkflowCategory);
        }
        public async Task<bool> WorkflowCategoryDeleteAsync(List<string> CategoryIds)
        {
            // 1、开启事务
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                
                // 2、查询流程分类模型
                var WorkflowCategorys = await _workflowFixtrue.db.WorkflowCategorys.FindAllAsync(m => CategoryIds.Contains(m.CategoryId));
                foreach (var WorkflowCategory in WorkflowCategorys)
                {
                    // 3、删除流程分类模型【真实删除】
                    await _workflowFixtrue.db.WorkflowCategorys.DeleteAsync(WorkflowCategory);
                }
                tran.Commit();
                return true;
            }
        }
    }
}