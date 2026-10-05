using AutoMapper;
using System;
using System.Linq;
using ydt_workflows_backend.Dtos.Index;
using ydt_workflows_backend.Fixtrues;
using ydt_workflows_backend.Models;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 首页Service实现
    /// </summary>
    public class IndexService : IIndexService
    {
        /// <summary>
        /// 工作流固定类
        /// </summary>
        public WorkflowFixtrue _workflowFixtrue { get; set; }
        private readonly IMapper _mapper;  // 映射接口

        public IndexService(WorkflowFixtrue workflowFixtrue, IMapper mapper)
        {
            _workflowFixtrue = workflowFixtrue;
            _mapper = mapper;
        }

        public async Task<IndexDto> IndexGetAsync(IndexGetDto indexGetDto)
        {
            IndexDto indexDto = new IndexDto();
            // 1、查询工作流实例表【根据执行人，来查询】
            List<WorkflowInstance> workflowInstances =  _workflowFixtrue.db.WorkflowInstances.FindAll(x => x.MakerList.Contains(indexGetDto.UserId.ToString())).ToList();
            foreach (var workflowInstance in workflowInstances)
            {
                Workflow workflow=await _workflowFixtrue.db.Workflows.FindByIdAsync(workflowInstance.FlowId);
                ApprovaDto approvaDto = new ApprovaDto
                {
                    FlowName = workflow.FlowName,
                    CreateTime = workflowInstance.UpdateTime,
                    CreateUserName = workflowInstance.CreateUserName
                };
                indexDto.ApprovaItem.approvaDtos.Add(approvaDto);
            }
            indexDto.ApprovaItem.ApprovaCount=workflowInstances.Count;
            return indexDto;
        }
    }
}
