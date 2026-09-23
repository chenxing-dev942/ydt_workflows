namespace ydt_workflows_backend.Dtos
{
    /// <summary>
    /// 用户角色关联模型分页查询结果Dto
    /// </summary>
    public class UserRolePageDto
    {
        /// <summary>
        /// 1、用户角色关联模型集合
        /// </summary>
        public List<UserRoleDto> UserRoles { get; set; }


        public int TotalPages
        {
            get
            {
                if (TotalItems <= 0 || PageSize <= 0)
                    return 0;
                var totalPage = TotalItems / PageSize;
                if (TotalItems % PageSize != 0)
                {
                    totalPage++;
                }
                return totalPage;
            }
        }
     
        /// <summary>
        /// 3、总条数
        /// </summary>
        public int TotalItems { set; get; }

        /// <summary>
        /// 当前页【1 2 3】
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 5、每页显示的条数【默认显示10条】
        /// </summary>
        public int PageSize { set; get; }

        public UserRolePageDto()
        {
            UserRoles = new List<UserRoleDto>();
        }
    }
}
