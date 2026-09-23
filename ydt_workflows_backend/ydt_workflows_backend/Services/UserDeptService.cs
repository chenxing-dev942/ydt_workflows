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
    /// 用户部门关联模型Service接口
    /// </summary>
    public class UserDeptService : IUserDeptService
    {
        /// <summary>
        /// 工作流固定类
        /// </summary>
        public WorkflowFixtrue _workflowFixtrue { get; set; }

        public IMapper _mapper { get; set; }

        public UserDeptService(WorkflowFixtrue workflowFixtrue,
                               IMapper mapper)
        {
            _workflowFixtrue = workflowFixtrue;
            _mapper = mapper;
        }

        /// <summary>
        /// 用户部门关联模型创建实现
        /// </summary>
        /// <param name="UserDeptCreateDto"></param>
        /// <returns></returns>
        public async Task<bool> UserDeptCreateAsync(UserDeptCreateDto UserDeptCreateDto)
        {
            // 1、UserDeptCreateDto模型映射
            UserDept UserDept = _mapper.Map<UserDept>(UserDeptCreateDto);

            //2、实现用户部门关联模型创建
            return await  _workflowFixtrue.db.UserDepts.InsertAsync(UserDept);
        }

        public async Task<List<UserDeptDto>> UserDeptGetListAsync(UserDeptGetListDto UserDeptGetListDto)
        {
            
            //1、查询所有用户部门关联模型
            IEnumerable<UserDept> UserDepts = await _workflowFixtrue.db.UserDepts.FindAllAsync();
            // 2、用户部门关联模型映射
            List<UserDeptDto> UserDeptDtos = _mapper.Map<List<UserDeptDto>>(UserDepts);

            // 3、返回用户部门关联模型
            return UserDeptDtos;
        }

        public async Task<UserDeptPageDto> UserDeptGetListPageAsync(UserDeptGetListPageDto UserDeptGetListPageDto)
        {
            // 1、用户部门关联模型分页Dto映射
            UserDeptGetListPage UserDeptGetListPage = _mapper.Map<UserDeptGetListPage>(UserDeptGetListPageDto);

            // 2、查询分页用户部门关联模型
            UserDeptPage UserDeptPage = await _workflowFixtrue.db.UserDepts.UserDeptGetListPageAsync(UserDeptGetListPage);

            // 3、用户部门关联模型分页模型映射
            UserDeptPageDto UserDeptPageDto = _mapper.Map<UserDeptPageDto>(UserDeptPage);
            return UserDeptPageDto;
        }
        public async Task<UserDeptDto> UserDeptGetAsync(long Id)
        {
            // 1、查询用户部门关联模型
            UserDept UserDept = await _workflowFixtrue.db.UserDepts.FindAsync(m => m.Id == Id);

            // 2、映射用户部门关联模型
            UserDeptDto UserDeptDto = _mapper.Map<UserDeptDto>(UserDept);

            return UserDeptDto;
        }
        public async Task<bool> UserDeptUpdateAsync(UserDeptUpdateDto UserDeptUpdateDto, long Id)
        {
            // 1、查询用户部门关联模型
            UserDept UserDept = await _workflowFixtrue.db.UserDepts.FindAsync(m => m.Id == Id);
            if (UserDept == null)
            {
                throw new CommonException("UserDept不存在");
            }

            // 2、用户部门关联模型模型映射
            UserDept = _mapper.Map<UserDeptUpdateDto, UserDept>(UserDeptUpdateDto, UserDept);

            // 3、用户部门关联模型更新实现
            return await _workflowFixtrue.db.UserDepts.UpdateAsync(UserDept);
        }
        public async Task<bool> UserDeptDeleteAsync(List<long> Ids)
        {
            // 1、开启事务
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                
                // 2、查询用户部门关联模型
                var UserDepts = await _workflowFixtrue.db.UserDepts.FindAllAsync(m => Ids.Contains(m.Id));
                foreach (var UserDept in UserDepts)
                {
                    // 3、删除用户部门关联模型【真实删除】
                    await _workflowFixtrue.db.UserDepts.DeleteAsync(UserDept);
                }
                tran.Commit();
                return true;
            }
        }
    }
}