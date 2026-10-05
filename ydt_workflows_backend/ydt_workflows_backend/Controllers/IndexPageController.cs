using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ydt_workflows_backend.CommonControllers;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Models;
using ydt_workflows_backend.Services;
using ydt_workflows_CommonController.CommonControllers;
using Microsoft.AspNetCore.Authorization;
using ydt_workflows_backend.Services.IServices;
using ydt_workflows_backend.Dtos.Index;

namespace ydt_workflows_backend.Controllers
{
    /// <summary>
    /// 首页控制器
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class IndexPageController : CommonController<IndexPageController>
    {
        private readonly IIndexService _indexService;
        public IndexPageController(ILogger<IndexPageController> logger,
                            IIndexService indexService) :
            base(logger)
        { 
            _indexService = indexService;
        }

        /// <summary>
        /// 1、首页查询
        /// </summary>
        [HttpGet]
        public async Task<IndexDto> IndexGetAsync(IndexGetDto indexGetDto)
        {
            return await _indexService.IndexGetAsync(indexGetDto);
        }
    }
}