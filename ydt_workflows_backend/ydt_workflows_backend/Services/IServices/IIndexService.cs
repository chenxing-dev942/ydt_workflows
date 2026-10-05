using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Dtos.Index;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 首页Service接口
    /// </summary>
    public interface IIndexService
    {
        public Task<IndexDto> IndexGetAsync(IndexGetDto indexGetDto);
    }
}
