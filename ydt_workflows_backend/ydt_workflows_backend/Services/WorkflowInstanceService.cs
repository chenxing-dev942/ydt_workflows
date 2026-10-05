using AutoMapper;
using AutoMapper;
using JadeFramework.Core.Extensions;
using JadeFramework.WorkFlow;
using Microsoft.AspNetCore.JsonPatch.Operations;
using ydt_workflows_backend.CommonExceptions;
using ydt_workflows_backend.Contexts;
using ydt_workflows_backend.Dtos;
using ydt_workflows_backend.Dtos.MyWorkflowWait;
using ydt_workflows_backend.Fixtrues;
using ydt_workflows_backend.Models;
using static ydt_workflows_backend.Dtos.MyWorkflowPageDto;

namespace ydt_workflows_backend.Services
{
    /// <summary>
    /// 流程实例模型【根据流程运行流程】Service接口
    /// </summary>
    public class WorkflowInstanceService : IWorkflowInstanceService
    {
        /// <summary>
        /// 工作流固定类
        /// </summary>
        public WorkflowFixtrue _workflowFixtrue { get; set; }
        /// <summary>
        /// 工作流service
        /// </summary>
        public IWorkflowService _WorkflowService { get; set; }
        /// <summary>
        /// 工作流分类Service
        /// </summary>
        private IWorkflowCategoryService _WorkflowCategoryService;
        public IMapper _mapper { get; set; }

        public WorkflowInstanceService(WorkflowFixtrue workflowFixtrue,
                               IMapper mapper,
                               IWorkflowService workflowService,
                               IWorkflowCategoryService workflowCategoryService)
        {
            _workflowFixtrue = workflowFixtrue;
            _mapper = mapper;
            _WorkflowService = workflowService;
            _WorkflowCategoryService = workflowCategoryService;
        }

        #region 1、创建流程
        /// <summary>
        /// 流程实例模型【根据流程运行流程】创建实现
        /// </summary>
        /// <param name="WorkflowInstanceCreateDto"></param>
        /// <returns></returns>
        public async Task<bool> WorkflowInstanceCreateAsync(WorkflowInstanceCreateDto WorkflowInstanceCreateDto)
        {
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                try
                {
                    // 1、查询工作流表
                    Workflow workflow = await _workflowFixtrue.db.Workflows.
                                FindByIdAsync(WorkflowInstanceCreateDto.FlowId);

                    // 2、创建工作流实例
                    WorkflowInstance workflowInstance = new WorkflowInstance()
                    {
                        InstanceId = Guid.NewGuid().ToString(), // 创建主键
                        FlowId = workflow.FlowId, // 工作流Id
                        Code = DateTime.Now.ToTimeStamp() + string.Empty.CreateNumberNonce(),
                        //CreateUserId = addProcess.UserId,
                        CreateUserName = WorkflowInstanceCreateDto.UserName,
                        FlowContent = workflow.FlowContent, // 工作流内容
                        IsFinish = null, // 完成状态
                        Status = (int)WorkFlowStatus.UnSubmit, // 工作流实例状态
                        UpdateTime = DateTime.Now.ToTimeStamp()
                    };

                    // 2.2、创建工作流实例
                    await _workflowFixtrue.db.WorkflowInstances.InsertAsync(workflowInstance, tran);

                    // 3、创建工作流实例表单
                    // 3.1、查询工作流表单表
                    var workflowForm = await _workflowFixtrue.db.WorkflowForms.FindByIdAsync(WorkflowInstanceCreateDto.FormId);
                    // 3.2、创建工作流实例表单模型
                    WorkflowInstanceForm workflowInstanceForm = new WorkflowInstanceForm()
                    {
                        InstanceFormId = Guid.NewGuid().ToString(), // 创建实例表单Id
                                                                    // CreateUserId = addProcess.UserId,
                        FlowContent = workflowForm.Content,// 表单内容
                        FormData = WorkflowInstanceCreateDto.FormData, // 表单数据
                        InstanceId = workflowInstance.InstanceId, // 工作流实例主键
                        FormId = workflowForm.FormId,
                        FormType = workflowForm.FormType,
                        FormUrl = null,
                        //CreateTime = DateTime.Now.ToTimeStamp(),
                    };
                    // 3.2、创建工作流实例
                    await _workflowFixtrue.db.WorkflowInstanceForms.InsertAsync(workflowInstanceForm, tran);

                    // 4、提交
                    tran.Commit();

                    return true;
                }
                catch (Exception e)
                {
                    // 5、回滚
                    tran.Rollback();
                    throw;
                }
            }
        }
        #endregion

        #region 2、流程实例模型【根据流程运行流程】集合查询

        public async Task<List<WorkflowInstanceDto>> WorkflowInstanceGetListAsync(WorkflowInstanceGetListDto WorkflowInstanceGetListDto)
        {

            //1、查询所有流程实例模型【根据流程运行流程】
            IEnumerable<WorkflowInstance> WorkflowInstances = await _workflowFixtrue.db.WorkflowInstances.FindAllAsync();
            // 2、流程实例模型【根据流程运行流程】映射
            List<WorkflowInstanceDto> WorkflowInstanceDtos = _mapper.Map<List<WorkflowInstanceDto>>(WorkflowInstances);

            // 3、返回流程实例模型【根据流程运行流程】
            return WorkflowInstanceDtos;
        }
        #endregion

        #region  2.1、流程实例模型【根据流程运行流程】集合分页查询
        public async Task<MyWorkflowPageDto> WorkflowInstanceGetListPageAsync(MyWorkflowGetListPageDto WorkflowInstanceGetListPageDto)
        {
            // 1、流程实例模型【根据流程运行流程】分页Dto映射
            WorkflowInstanceGetListPage WorkflowInstanceGetListPage =
                _mapper.Map<WorkflowInstanceGetListPage>(WorkflowInstanceGetListPageDto);

            // 2、查询分页流程实例模型【根据流程运行流程】
            WorkflowInstancePage WorkflowInstancePage = 
                await _workflowFixtrue.db.WorkflowInstances.WorkflowInstanceGetListPageAsync(WorkflowInstanceGetListPage);

            // 3、流程实例模型【根据流程运行流程】分页模型映射
            MyWorkflowPageDto myWorkflowPageDto = _mapper.Map<MyWorkflowPageDto>(WorkflowInstancePage);

            List<MyWorkflowDto> myWorkflowDtos= myWorkflowPageDto.MyWorkflowDtos;

            foreach (var myWorkflowDto in myWorkflowDtos)
            {
                Workflow workflow = await _workflowFixtrue.db.Workflows.FindByIdAsync(myWorkflowDto.FlowId);
                myWorkflowDto.FlowName = workflow.FlowName;
                myWorkflowDto.FormId=workflow.FormId;
            }

            foreach (var myWorkflowDto in myWorkflowDtos)
            {
                WorkflowForm workflowForm=await _workflowFixtrue.db.WorkflowForms
                    .FindByIdAsync(myWorkflowDto.FormId);
                myWorkflowDto.FormName = workflowForm.FormName;
                myWorkflowDto.FormType = workflowForm.FormType;
            }
            return myWorkflowPageDto;
        }
        #endregion

        #region 3、流程实例模型【根据流程运行流程】查询【根据InstanceId查询】
        public async Task<WorkflowInstanceGetResultDto> WorkflowInstanceGetAsync(string InstanceId)
        {
            // 1、创建WorkflowInstanceGetResultDto
            WorkflowInstanceGetResultDto instanceGetResultDto = new WorkflowInstanceGetResultDto();
            // 2、查询工作流实例
            WorkflowInstance WorkflowInstance = await _workflowFixtrue.db.
                WorkflowInstances.FindAsync(m => m.InstanceId == InstanceId);
            // 2.1、设置WorkflowInstanceGetResultDto
            instanceGetResultDto.FlowId = WorkflowInstance.FlowId;
            instanceGetResultDto.InstanceId = WorkflowInstance.InstanceId;
            instanceGetResultDto.FlowName = null;

            // 3、查询工作流实例表单
            WorkflowInstanceForm instanceForm = await _workflowFixtrue.db.
                 WorkflowInstanceForms.FindAsync(m => m.InstanceId == InstanceId);
            // 3.1、设置WorkflowInstanceGetResultDto
            instanceGetResultDto.FormType = (WorkFlowFormType)instanceForm.FormType;
            instanceGetResultDto.FormContent = instanceForm.FlowContent; // 表单内容
            instanceGetResultDto.FormUrl = instanceForm.FormUrl;
            instanceGetResultDto.FormData = instanceForm.FormData; // 表单数据

            // 4、设置菜单
            instanceGetResultDto.Menus = new List<int>
            {
                (int)WorkFlowMenu.Submit, // 提交
                (int)WorkFlowMenu.FlowImage, // 流程图
                (int)WorkFlowMenu.Save, // 保存
                (int)WorkFlowMenu.Return, // 返回
            };

            // 5、设置实例状态
            instanceGetResultDto.FlowData = new WorkFlowProcessFlowData
            {
                IsFinish = WorkflowInstance.IsFinish, // 流程是否完成
                Status = WorkflowInstance.Status // 流程实例状态
            };

            return instanceGetResultDto;
        }
        #endregion

        #region 4、流程实例模型【根据流程运行流程】更新
        public async Task<bool> WorkflowInstanceUpdateAsync(WorkflowInstanceUpdateDto WorkflowInstanceUpdateDto, string InstanceId)
        {
            WorkflowInstanceForm workflowInstanceForm = await _workflowFixtrue.db.WorkflowInstanceForms.FindAsync(m => m.InstanceId == InstanceId);
            workflowInstanceForm.FormData = workflowInstanceForm.FormData;
            return await _workflowFixtrue.db.WorkflowInstanceForms.UpdateAsync(workflowInstanceForm);
        }
        #endregion

        #region 5、流程实例模型【根据流程运行流程】删除
        public async Task<bool> WorkflowInstanceDeleteAsync(List<string> InstanceIds)
        {
            // 1、开启事务
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {

                // 2、查询流程实例模型【根据流程运行流程】
                var WorkflowInstances = await _workflowFixtrue.db.WorkflowInstances.FindAllAsync(m => InstanceIds.Contains(m.InstanceId));
                foreach (var WorkflowInstance in WorkflowInstances)
                {
                    // 3、删除流程实例模型【根据流程运行流程】【真实删除】
                    await _workflowFixtrue.db.WorkflowInstances.DeleteAsync(WorkflowInstance);
                }
                tran.Commit();
                return true;
            }
        }
        #endregion

        #region 6、流程实例模型【根据流程运行流程】查询工作流和工作流分类
        public async Task<WorkflowAndWorkflowCategoryDto> WorkflowAndWorkflowCategoryGetListAsync()
        {
            WorkflowAndWorkflowCategoryDto WorkflowAndWorkflowCategoryDto = new WorkflowAndWorkflowCategoryDto();
            List<WorkflowDto> workflowDtos = await _WorkflowService.WorkflowGetListAsync(new WorkflowGetListDto());
            List<WorkflowCategoryDto> workflowCategoryDtos =
                    await _WorkflowCategoryService.WorkflowCategoryGetListAsync(new WorkflowCategoryGetListDto());
            WorkflowAndWorkflowCategoryDto.workflowDtos = workflowDtos;
            WorkflowAndWorkflowCategoryDto.workflowCategories = workflowCategoryDtos;
            return WorkflowAndWorkflowCategoryDto;
        }
        #endregion

        #region 7、流程实例模型【根据流程运行流程】打开
        public async Task<WorkflowOpenDto> WorkflowOpenAsync(string flowId)
        {
            ///核心是查询工作流表和工作流表单表，返回WorkflowOpenDto
            // 1、创建WorkflowOpenDto
            WorkflowOpenDto workflowOpenDto = new WorkflowOpenDto();
            // 2、查询工作流表
            Workflow workflow = await _workflowFixtrue.db.Workflows.FindByIdAsync(flowId);
            workflowOpenDto.FlowId = workflow.FlowId;
            workflowOpenDto.FlowName = workflow.FlowName;
            workflowOpenDto.FormId = workflow.FormId;

            // 3、查询工作流表单表
            WorkflowForm workflowForm = await _workflowFixtrue.db.WorkflowForms.FindByIdAsync(workflow.FormId);
            workflowOpenDto.FormType = (WorkFlowFormType)workflowForm.FormType;
            workflowOpenDto.FormContent = workflowForm.Content;
            workflowOpenDto.FormUrl = workflowForm.FormUrl;
            workflowOpenDto.FormData = null;
            //4、设置菜单
            workflowOpenDto.Menus = new List<int>
            {
                (int)WorkFlowMenu.Submit,
                (int)WorkFlowMenu.FlowImage,
                (int)WorkFlowMenu.Save,
                (int)WorkFlowMenu.Return,
            };

            // 5、设置流程实例状态
            workflowOpenDto.FlowData = new WorkFlowProcessFlowData
            {
                IsFinish = null,
                Status = (int)WorkFlowStatus.UnSubmit
            };

            return workflowOpenDto;
        }
        #endregion

        #region 查看流程图
        public async Task<WorkflowImageDto> WorkflowImageAsync(string flowId)
        {
            WorkflowImageDto workflowImageDto = new WorkflowImageDto();
            Workflow workflow = await _workflowFixtrue.db.Workflows.FindByIdAsync(flowId);
            workflowImageDto.FlowId = flowId;
            workflowImageDto.FlowContent = workflow.FlowContent;
            return workflowImageDto;
        }
        #endregion

        #region 执行(提交)
        public async Task<bool> WorkflowInstanceProcessSubmitAsync(WorkflowInstanceProcessSubmitDto workflowInstanceProcessSubmitDto)
        {
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                try
                {
                    // 1、工作流数据转换
                    //1.1、查询工作流数据 : FlowContent
                    Workflow workflow = await _workflowFixtrue.db.Workflows.FindByIdAsync(workflowInstanceProcessSubmitDto.FlowId);
                    //1.2 转换工作流数据
                    WorkFlow workFlow = new WorkFlow();
                    workFlow.FlowJSON = workflow.FlowContent;
                    workFlow.FlowId = Guid.Parse(workflow.FlowId);
                    workFlow.InstanceId = Guid.Parse(workflowInstanceProcessSubmitDto.InstanceId);
                    //1.2.1、创建工作流上下文
                    YDTWorkFlowContext context = new YDTWorkFlowContext(workFlow);
                    // 2、更新ydt_workflow_instance【设置下一个节点信息】
                    // 2.1、查询工作流实例
                    WorkflowInstance workflowInstance = await _workflowFixtrue.db.WorkflowInstances
                                .FindByIdAsync(workflowInstanceProcessSubmitDto.InstanceId);
                    //2.1、设置新工作流实例值
                    workflowInstance.ActivityId = context.WorkFlow.NextNodeId.ToString(); // 下一个节点做为当前节点
                    workflowInstance.ActivityName = context.WorkFlow.NextNode.Name;// 当前节点名称
                    workflowInstance.ActivityType = (int)context.WorkFlow.NextNodeType; // 当前节点类型
                    workflowInstance.PreviousId = context.WorkFlow.ActivityNodeId.ToString(); // 当前节点做为上一个节点

                    // 获取下一个节点执行人员【该谁开始执行流程】
                    workflowInstance.MakerList = await this.GetMakerListAsync
                                    (context.WorkFlow.Nodes[Guid.Parse(workflowInstance.ActivityId)]);

                    workflowInstance.FlowContent = workflow.FlowContent;
                    // 设置工作流状态0,1
                    workflowInstance.IsFinish = context.WorkFlow.NextNodeType.ToIsFinish();
                    // 设置工作流操作状态
                    workflowInstance.Status = (int)WorkFlowStatus.Running; // 审核中（实例发起之后变成运行中状态）
                    workflowInstance.UpdateTime = DateTime.Now.ToTimeStamp();
                    await _workflowFixtrue.db.WorkflowInstances.UpdateAsync(workflowInstance, tran);

                    // 3、创建ydt_workflow_operation_history【记录操作历史】
                    WorkflowOperationHistory operationHistory = new WorkflowOperationHistory
                    {
                        OperationId = Guid.NewGuid().ToString(),
                        InstanceId = workflowInstance.InstanceId,
                        NodeId = context.WorkFlow.ActivityNodeId.ToString(),
                        NodeName = context.WorkFlow.ActivityNode.Name,
                        TransitionType = (int)WorkFlowMenu.Submit,// 提交【运行】
                        Content = "流程提交",
                        CreateUserName = workflowInstanceProcessSubmitDto.UserName
                    };
                    await _workflowFixtrue.db.WorkflowOperationHistorys.InsertAsync(operationHistory, tran);

                    // 4、创建ydt_workflow_transition_history【进度条，从哪个节点到哪一个节点】
                    WorkflowTransitionHistory transitionHistory = new WorkflowTransitionHistory
                    {
                        TransitionId = Guid.NewGuid().ToString(), // 主键
                        InstanceId = workflowInstance.InstanceId, // 工作实例主键
                        FromNodeId = context.WorkFlow.ActivityNodeId.ToString(), // 当前节点
                        FromNodeType = (int)context.WorkFlow.ActivityNodeType,
                        FromNodName = context.WorkFlow.ActivityNode.Name,
                        ToNodeId = context.WorkFlow.NextNodeId.ToString(),// 下一个节点
                        ToNodeType = (int)context.WorkFlow.NextNodeType,
                        ToNodeName = context.WorkFlow.NextNode.Name,
                        //CreateUserId = model.UserId,
                        CreateUserName = workflowInstanceProcessSubmitDto.UserName,
                        TransitionState = (int)WorkFlowTransitionStateType.Normal, // 正常状态【通过】
                        IsFinish = context.WorkFlow.NextNodeType.ToIsFinish(), // 是否完成状态，0：执行中
                    };
                    await _workflowFixtrue.db.WorkflowTransitionHistorys.InsertAsync(transitionHistory, tran);

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }
        #endregion

        #region 获取执行人员
        /// <summary>
        ///  获取执行人员
        /// </summary>
        /// <param name="flowNode"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task<string> GetMakerListAsync(FlowNode flowNode)
        {
            string makerList = "";
            FlowNodeSetInfo flowNodeSetInfo = flowNode.SetInfo;
            if (flowNodeSetInfo.NodeDesignate.Equals("SPECIAL_USER"))
            {
                string[] users = flowNodeSetInfo.Nodedesignatedata.Users;
                makerList = string.Join(",", users);
            }
            return makerList;
        }
        #endregion

        #region 同意流程
        public async Task<bool> WorkflowInstanceProcessAgreeAsync(WorkflowInstanceProcessAgreeDto workflowInstanceProcessAgreeDto)
        {
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                try
                {
                    // 1、工作流数据转换
                    //1.1、查询工作流数据 : FlowContent
                    WorkflowInstance workflowInstance = await _workflowFixtrue.db.WorkflowInstances.FindByIdAsync(workflowInstanceProcessAgreeDto.InstanceId);
                    //1.2 转换工作流数据
                    WorkFlow workFlow = new WorkFlow();
                    workFlow.FlowJSON = workflowInstance.FlowContent;// 工作流数据
                    workFlow.FlowId = Guid.Parse(workflowInstance.FlowId);
                    workFlow.InstanceId = Guid.Parse(workflowInstance.InstanceId);
                    workFlow.ActivityNodeId = Guid.Parse(workflowInstance.ActivityId); // 设置当前节点
                    workFlow.ActivityNodeType = (WorkFlowInstanceNodeType)workflowInstance.ActivityType; //设置当前节点类型 
                    workFlow.PreviousId = Guid.Parse(workflowInstance.PreviousId); // 设置上一个节点
                                                                                   // 1.2.1、创建工作流上下文
                    YDTWorkFlowContext context = new YDTWorkFlowContext(workFlow);

                    // 2、修改工作流实例【设置下一个节点】
                    workflowInstance.PreviousId = workflowInstance.ActivityId;
                    workflowInstance.ActivityId = context.WorkFlow.NextNodeId.ToString();
                    workflowInstance.ActivityName = context.WorkFlow.NextNode.Name;
                    workflowInstance.ActivityType = (int)context.WorkFlow.NextNodeType;
                    workflowInstance.UpdateTime = DateTime.Now.ToTimeStamp();
                    // 执行人
                    workflowInstance.MakerList = context.WorkFlow.NextNodeType
                                            == WorkFlowInstanceNodeType.EndRound ? ""
                                            : await this.GetMakerListAsync(context.WorkFlow.NextNode);

                    // 工作流实例状态
                    workflowInstance.IsFinish = context.WorkFlow.NextNodeType.ToIsFinish();

                    // 判断是否结束【最后一次审核】
                    if (context.WorkFlow.NextNodeType == WorkFlowInstanceNodeType.EndRound)
                    {
                        workflowInstance.Status = (int)WorkFlowStatus.IsFinish; // 已结束
                    }
                    else
                    {
                        //工作操作状态
                        workflowInstance.Status = (int)WorkFlowStatus.Running; // 审核中
                    }
                    await _workflowFixtrue.db.WorkflowInstances.UpdateAsync(workflowInstance, tran);

                    // 3、创建ydt_workflow_operation_history【记录操作历史】
                    WorkflowOperationHistory operationHistory = new WorkflowOperationHistory
                    {
                        OperationId = Guid.NewGuid().ToString(),
                        InstanceId = workflowInstance.InstanceId,
                        NodeId = context.WorkFlow.ActivityNodeId.ToString(),
                        NodeName = context.WorkFlow.ActivityNode.Name,
                        TransitionType = (int)WorkFlowMenu.Agree,// 提交【同意】
                        Content = workflowInstanceProcessAgreeDto.AgreeContent,
                        CreateUserName = workflowInstanceProcessAgreeDto.UserName
                    };
                    await _workflowFixtrue.db.WorkflowOperationHistorys.InsertAsync(operationHistory, tran);

                    // 4、创建ydt_workflow_transition_history【进度条，从哪个节点到哪一个节点】
                    WorkflowTransitionHistory transitionHistory = new WorkflowTransitionHistory
                    {
                        TransitionId = Guid.NewGuid().ToString(), // 主键
                        InstanceId = workflowInstance.InstanceId, // 工作实例主键
                        FromNodeId = context.WorkFlow.ActivityNodeId.ToString(), // 当前节点
                        FromNodeType = (int)context.WorkFlow.ActivityNodeType,
                        FromNodName = context.WorkFlow.ActivityNode.Name,
                        ToNodeId = context.WorkFlow.NextNodeId.ToString(),// 下一个节点
                        ToNodeType = (int)context.WorkFlow.NextNodeType,
                        ToNodeName = context.WorkFlow.NextNode.Name,
                        //CreateUserId = model.UserId,
                        CreateUserName = workflowInstanceProcessAgreeDto.UserName,
                        TransitionState = (int)WorkFlowTransitionStateType.Normal, // 正常状态【通过】
                        IsFinish = context.WorkFlow.NextNodeType.ToIsFinish(), // 是否完成状态，0：执行中
                    };
                    await _workflowFixtrue.db.WorkflowTransitionHistorys.InsertAsync(transitionHistory, tran);

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }
        #endregion

        #region 不同意流程
        public async Task<bool> WorkflowInstanceProcessDeprecateAsync(WorkflowInstanceProcessDeprecateDto workflowInstanceProcessDeprecateDto)
        {
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                try
                {
                    WorkflowInstance workflowInstance = await _workflowFixtrue.db.WorkflowInstances.FindByIdAsync(workflowInstanceProcessDeprecateDto.InstanceId);
                    // 2、工作流数据转换【工作流模型】
                    WorkFlow workFlow = new WorkFlow();
                    workFlow.FlowJSON = workflowInstance.FlowContent;
                    workFlow.FlowId = Guid.Parse(workflowInstance.FlowId);
                    workFlow.InstanceId = Guid.Parse(workflowInstance.InstanceId);
                    workFlow.ActivityNodeId = Guid.Parse(workflowInstance.ActivityId); // 设置当前节点
                    workFlow.ActivityNodeType = (WorkFlowInstanceNodeType)workflowInstance.ActivityType; //设置当前节点类型 
                    workFlow.PreviousId = Guid.Parse(workflowInstance.PreviousId); // 设置上一个节点

                    // 1.2.1、创建工作流上下文
                    YDTWorkFlowContext context = new YDTWorkFlowContext(workFlow);
                    // 3、更新工作流实例【更新状态、更新当前节点】
                    workflowInstance.PreviousId = workflowInstance.ActivityId;
                    workflowInstance.ActivityId = context.WorkFlow.NextNodeId.ToString();
                    workflowInstance.ActivityName = context.WorkFlow.NextNode.Name;
                    workflowInstance.ActivityType = (int)context.WorkFlow.NextNodeType;
                    workflowInstance.UpdateTime = DateTime.Now.ToTimeStamp();
                    // 工作流实例状态
                    workflowInstance.IsFinish = 0; // 不同意
                                                   // 工作操作状态
                    workflowInstance.Status = (int)WorkFlowStatus.Deprecate; // 不同意状态
                    await _workflowFixtrue.db.WorkflowInstances.UpdateAsync(workflowInstance, tran);

                    // 4、创建工作流操作记录 ydt_workflow_operation_history
                    WorkflowOperationHistory operationHistory = new WorkflowOperationHistory
                    {
                        OperationId = Guid.NewGuid().ToString(),
                        InstanceId = workflowInstance.InstanceId, // 设置工作流实例Id
                                                                  //CreateUserId = model.UserId,
                        CreateUserName = workflowInstanceProcessDeprecateDto.UserName, // 操做用户
                        Content = workflowInstanceProcessDeprecateDto.DisagreeContent, // 操作内容
                        NodeName = context.WorkFlow.ActivityNode.Name, // 当前节点名称
                        NodeId = context.WorkFlow.ActivityNodeId.ToString(), // 当前节点Id
                        TransitionType = (int)WorkFlowMenu.Deprecate // 【不同意菜单】
                    };
                    await _workflowFixtrue.db.WorkflowOperationHistorys.InsertAsync(operationHistory, tran);

                    // 4、创建ydt_workflow_transition_history【进度条，从哪个节点到哪一个节点】
                    WorkflowTransitionHistory transitionHistory = new WorkflowTransitionHistory
                    {
                        TransitionId = Guid.NewGuid().ToString(), // 主键
                        InstanceId = workflowInstance.InstanceId, // 工作实例主键
                        FromNodeId = context.WorkFlow.ActivityNodeId.ToString(), // 当前节点
                        FromNodeType = (int)context.WorkFlow.ActivityNodeType,
                        FromNodName = context.WorkFlow.ActivityNode.Name,
                        ToNodeId = context.WorkFlow.NextNodeId.ToString(),// 下一个节点
                        ToNodeType = (int)context.WorkFlow.NextNodeType,
                        ToNodeName = context.WorkFlow.NextNode.Name,
                        //CreateUserId = model.UserId,
                        CreateUserName = workflowInstanceProcessDeprecateDto.UserName,
                        TransitionState = (int)WorkFlowTransitionStateType.Reject, // 节点拒绝[不允许往下执行]
                        IsFinish = (int)WorkFlowInstanceStatus.Running // 执行中 
                    };
                    await _workflowFixtrue.db.WorkflowTransitionHistorys.InsertAsync(transitionHistory, tran);

                    tran.Commit();
                    return true;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }
#endregion

        #region 退回流程
        public async Task<bool> WorkflowInstanceProcessBackAsync(WorkflowInstanceProcessBackDto workflowInstanceProcessBackDto)
        {
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                try
                {
                    // 1、查询工作流实例
                    WorkflowInstance workflowInstance = await _workflowFixtrue.db.WorkflowInstances.FindByIdAsync(workflowInstanceProcessBackDto.InstanceId);
                    // 2、工作流数据转换【工作流模型】【确定上一个、当前、下一个节点】
                    WorkFlow workFlow = new WorkFlow();
                    workFlow.FlowJSON = workflowInstance.FlowContent;// 工作流数据
                    workFlow.FlowId = Guid.Parse(workflowInstance.FlowId);
                    workFlow.InstanceId = Guid.Parse(workflowInstance.InstanceId);
                    workFlow.ActivityNodeId = Guid.Parse(workflowInstance.ActivityId); // 设置当前节点
                    workFlow.PreviousId = Guid.Parse(workflowInstance.PreviousId); // 设置上一个节点
                    // 1.2.1、创建工作流上下文
                    YDTWorkFlowContext context = new YDTWorkFlowContext(workFlow);

                    // 3、更新工作流实例【更新状态、更新当前节点】
                    // 3.1、获取回退的节点
                    Guid rejectNodeId = context.RejectNode(workflowInstanceProcessBackDto.NodeRejectType.Value,
                       workflowInstanceProcessBackDto.RejectNodeId);
                    FlowNode rejectNode = context.WorkFlow.Nodes[rejectNodeId];
                    workflowInstance.PreviousId = workflowInstance.ActivityId; // 当前节点，作为上一个节点
                    workflowInstance.ActivityId = rejectNodeId.ToString();// 退回的节点，作为当前节点
                    workflowInstance.ActivityName = rejectNode.Name;
                    workflowInstance.ActivityType = (int)rejectNode.NodeType();
                    workflowInstance.UpdateTime = DateTime.Now.ToTimeStamp();

                    // 3.2、设置状态
                    workflowInstance.IsFinish = rejectNode.NodeType().ToIsFinish();// 是否完成
                    workflowInstance.Status = (int)WorkFlowStatus.Back; // 退回

                    // 3.3、修改节点执行人
                    workflowInstance.MakerList = rejectNode.NodeType()
                        == WorkFlowInstanceNodeType.EndRound
                        ? ""
                        : await this.GetMakerListAsync(rejectNode);
                    await _workflowFixtrue.db.WorkflowInstances.UpdateAsync(workflowInstance, tran);
                    // 4、创建工作流操作记录 ydt_workflow_operation_history
                    WorkflowOperationHistory operationHistory = new WorkflowOperationHistory
                    {
                        OperationId = Guid.NewGuid().ToString(),
                        InstanceId = workflowInstance.InstanceId, // 设置工作流实例Id
                                                                  //CreateUserId = model.UserId,
                        CreateUserName = workflowInstanceProcessBackDto.UserName,
                        Content = workflowInstanceProcessBackDto.BackContent, // 操作内容
                        NodeName = context.WorkFlow.ActivityNode.Name, // 当前节点名称
                        NodeId = context.WorkFlow.ActivityNodeId.ToString(), // 当前节点Id
                        TransitionType = (int)WorkFlowMenu.Back // 流转状态【退回】
                    };
                    await _workflowFixtrue.db.WorkflowOperationHistorys.InsertAsync(operationHistory, tran);

                    // 5、创建工作流流转记录 ydt_workflow_transition_history【从哪一个节点到一个节点】
                    WorkflowTransitionHistory transitionHistory = new WorkflowTransitionHistory
                    {
                        TransitionId = Guid.NewGuid().ToString(), // 主键
                        InstanceId = workflowInstance.InstanceId, // 工作实例主键
                        FromNodeId = context.WorkFlow.ActivityNodeId.ToString(), // 当前节点
                        FromNodeType = (int)context.WorkFlow.ActivityNodeType,
                        FromNodName = context.WorkFlow.ActivityNode.Name,
                        ToNodeId = rejectNodeId.ToString(),// 到退回的节点
                        ToNodeType = (int)rejectNode.NodeType(),
                        ToNodeName = rejectNode.Name,
                        //CreateUserId = model.UserId,
                        CreateUserName = workflowInstanceProcessBackDto.UserName,
                        TransitionState = (int)WorkFlowTransitionStateType.Reject, // 拒绝状态【拒绝】
                        IsFinish = (int)WorkFlowInstanceStatus.Running, // 是否完成状态，0：审核中
                    };
                    await _workflowFixtrue.db.WorkflowTransitionHistorys.InsertAsync(transitionHistory, tran);

                    tran.Commit();
                    return true;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        #endregion

        #region 重新提交
        public async Task<bool> WorkflowInstanceProcessReSubmitAsync(WorkflowInstanceProcessSubmitDto workflowInstanceProcessSubmitDto)
        {
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                try
                {
                    // 1、查询工作流实例
                    WorkflowInstance workflowInstance = await _workflowFixtrue.db.WorkflowInstances
                   .FindByIdAsync(workflowInstanceProcessSubmitDto.InstanceId);

                    // 2、工作流数据转换【工作流模型】【确定上一个、当前、下一个节点】
                    WorkFlow workFlow = new WorkFlow();
                    workFlow.FlowJSON = workflowInstance.FlowContent;// 工作流数据
                    workFlow.FlowId = Guid.Parse(workflowInstance.FlowId);
                    workFlow.ActivityNodeId = default(Guid); // 设置当前节点
                    // 1.2.1、创建工作流上下文
                    YDTWorkFlowContext context = new YDTWorkFlowContext(workFlow);

                    // 3、更新工作流实例【更新状态、更新当前节点】
                    workflowInstance.ActivityId = context.WorkFlow.NextNodeId.ToString();
                    workflowInstance.ActivityName = context.WorkFlow.NextNode.Name;
                    workflowInstance.ActivityType = (int)context.WorkFlow.NextNodeType;
                    workflowInstance.PreviousId = context.WorkFlow.ActivityNodeId.ToString();
                    // 3.1、设置执行人
                    workflowInstance.MakerList = await this.GetMakerListAsync(
                           context.WorkFlow.Nodes[context.WorkFlow.NextNodeId]);
                    // 3.2、设置工作流状态
                    workflowInstance.IsFinish = context.WorkFlow.NextNodeType.ToIsFinish();
                    workflowInstance.Status = (int)WorkFlowStatus.Running; //审核中
                    workflowInstance.UpdateTime = DateTime.Now.ToTimeStamp();
                    await _workflowFixtrue.db.WorkflowInstances.UpdateAsync(workflowInstance, tran);

                    // 4、创建工作流操作记录 ydt_workflow_operation_history
                    WorkflowOperationHistory operationHistory = new WorkflowOperationHistory
                    {
                        OperationId = Guid.NewGuid().ToString(),
                        InstanceId = workflowInstance.InstanceId, // 设置工作流实例Id
                                                                  //CreateUserId = model.UserId,
                        CreateUserName = workflowInstanceProcessSubmitDto.UserName,
                        Content = "流程重新提交", // 操作内容
                        NodeName = context.WorkFlow.ActivityNode.Name, // 当前节点名称
                        NodeId = context.WorkFlow.ActivityNodeId.ToString(), // 当前节点Id
                        TransitionType = (int)WorkFlowMenu.Submit // 流转状态，退回
                    };
                    await _workflowFixtrue.db.WorkflowOperationHistorys.InsertAsync(operationHistory, tran);

                    // 5、创建工作流流转记录 ydt_workflow_transition_history【从哪一个节点到一个节点】
                    WorkflowTransitionHistory transitionHistory = new WorkflowTransitionHistory
                    {
                        TransitionId = Guid.NewGuid().ToString(), // 主键
                        InstanceId = workflowInstance.InstanceId, // 工作实例主键
                        FromNodeId = context.WorkFlow.ActivityNodeId.ToString(), // 当前节点
                        FromNodeType = (int)context.WorkFlow.ActivityNodeType,
                        FromNodName = context.WorkFlow.ActivityNode.Name,
                        ToNodeId = context.WorkFlow.NextNodeId.ToString(),// 到指定的节点
                        ToNodeType = (int)context.WorkFlow.NextNodeType,
                        ToNodeName = context.WorkFlow.NextNode.Name,
                        //CreateUserId = model.UserId,
                        CreateUserName = workflowInstanceProcessSubmitDto.UserName,
                        TransitionState = (int)WorkFlowTransitionStateType.Normal, // 正常状态
                        IsFinish = context.WorkFlow.NextNodeType.ToIsFinish(), // 是否完成状态，0：执行中
                    };
                    await _workflowFixtrue.db.WorkflowTransitionHistorys.InsertAsync(transitionHistory, tran);

                    tran.Commit();
                    return true;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
        #endregion

        #region 工作流实例执行-委托
        public async Task<bool> WorkflowInstanceProcessAssignAsync(WorkflowInstanceProcessAssignDto workflowInstanceProcessAssignDto)
        {
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                try
                {
                    // 1、查询工作流实例
                    WorkflowInstance workflowInstance = await _workflowFixtrue.db.WorkflowInstances
               .FindByIdAsync(workflowInstanceProcessAssignDto.InstanceId);

                    // 2、工作流数据转换【工作流模型】【确定上一个、当前、下一个节点】
                    WorkFlow workFlow = new WorkFlow();
                    workFlow.FlowJSON = workflowInstance.FlowContent;// 工作流数据
                    workFlow.FlowId = Guid.Parse(workflowInstance.FlowId);
                    workFlow.ActivityNodeId = Guid.Parse(workflowInstance.ActivityId); // 设置当前节点
                    workFlow.PreviousId = Guid.Parse(workflowInstance.PreviousId);
                    // 1.2.1、创建工作流上下文
                    YDTWorkFlowContext context = new YDTWorkFlowContext(workFlow);

                    // 3、更新工作流实例【更新执行人】
                    string OldMakerList = workflowInstance.MakerList != null
                                            ? workflowInstance.MakerList :
                                            workflowInstanceProcessAssignDto.UserId + ",";
                    string NewMakerList = workflowInstanceProcessAssignDto.UserId + ","; 
                    // 3.1、替换执行人【把委托人换成执行人】
                    workflowInstance.MakerList = workflowInstance.MakerList.Replace(OldMakerList, NewMakerList);
                    await _workflowFixtrue.db.WorkflowInstances.UpdateAsync(workflowInstance, tran);
                    // 4、创建工作流操作记录 ydt_workflow_operation_history
                    WorkflowOperationHistory workflowOperationHistory = new WorkflowOperationHistory
                    {
                        OperationId=new Guid().ToString(),
                        InstanceId=workflowInstance.InstanceId,
                        NodeId=context.WorkFlow.ActivityNodeId.ToString(),
                        NodeName=context.WorkFlow.ActivityNode.Name,
                        TransitionType=(int)WorkFlowMenu.Assign,
                        Content=workflowInstanceProcessAssignDto.flowAssign.AssignContent,
                        CreateUserName=workflowInstanceProcessAssignDto.UserName
                    };
                    await _workflowFixtrue.db.WorkflowOperationHistorys.InsertAsync(workflowOperationHistory, tran);
                    // 5、创建委托记录ydt_workflow_assign
                    WorkflowAssign workflowAssign = new WorkflowAssign
                    {
                        AssignId = new Guid().ToString(),
                        FlowId= workflowInstance.FlowId,
                        InstanceId=workflowInstance.InstanceId,
                        NodeId= context.WorkFlow.ActivityNodeId.ToString(),
                        NodeName=context.WorkFlow.ActivityNode.Name,
                        UserId=workflowInstanceProcessAssignDto.UserId,
                        UserName= workflowInstanceProcessAssignDto.UserName,
                        AssignUserId=workflowInstanceProcessAssignDto.flowAssign.AssignUserId,
                        AssignUserName=workflowInstanceProcessAssignDto.flowAssign.AssignUserName,
                        Content=workflowInstanceProcessAssignDto.flowAssign.AssignContent
                    };
                    await _workflowFixtrue.db.WorkflowAssigns.InsertAsync(workflowAssign, tran);
                    tran.Commit();
                    return true;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
        #endregion

        #region 工作流实例执行-审批意见
        public async Task<List<WorkflowOperationHistoryDto>> WorkflowInstanceProcessApprovalAsync(WorkflowInstanceProcessApprovalDto workflowInstanceProcessApprovalDto)
        {
            var operationHistories = await _workflowFixtrue.db.WorkflowOperationHistorys.FindAllAsync(x=>x.InstanceId== workflowInstanceProcessApprovalDto.InstanceId);
            List<WorkflowOperationHistory> workflowOperationHistories = operationHistories.ToList();
            WorkflowInstance workflowInstance = await _workflowFixtrue.db.WorkflowInstances.FindByIdAsync(workflowInstanceProcessApprovalDto.InstanceId);
            if (workflowInstance.IsFinish==1)
            {
                WorkflowOperationHistory workflowOperationHistory = new WorkflowOperationHistory();
                workflowOperationHistory.NodeName = "结束";
                workflowOperationHistory.TransitionType = 0;
                workflowOperationHistory.CreateUserName = "";
                workflowOperationHistory.Content = "系统自动结束";
                workflowOperationHistories.Add(workflowOperationHistory);
            }
            List<WorkflowOperationHistoryDto> historyDtos = _mapper.Map<List<WorkflowOperationHistoryDto>>(workflowOperationHistories);
            return historyDtos;
        }
        #endregion

        #region 流程实例执行-撤回【删除工作流实例信息】
        public async Task<bool> WorkflowInstanceProcessWithdrawAsync(WorkflowInstanceProcessWithdrawDto workflowInstanceProcessWithdrawDto)
        {
            using (var tran = _workflowFixtrue.db.BeginTransaction())
            {
                try
                {
                    // 1、查询工作流实例
                    var dbflowinstance = await _workflowFixtrue.db.WorkflowInstances.FindByIdAsync(
                        workflowInstanceProcessWithdrawDto.InstanceId);

                    // 2、删除工作流实例操作记录
                    var dboperationHistory = await _workflowFixtrue.db.WorkflowOperationHistorys.
                            FindAllAsync(m => m.InstanceId == workflowInstanceProcessWithdrawDto.InstanceId);
                    foreach (var item in dboperationHistory)
                    {
                        await _workflowFixtrue.db.WorkflowOperationHistorys.DeleteAsync(item, tran);
                    }

                    // 3、删除工作流实例流转记录
                    var dbtransitionHistory = await _workflowFixtrue.db.WorkflowTransitionHistorys
                       .FindAllAsync(m => m.InstanceId == workflowInstanceProcessWithdrawDto.InstanceId);
                    foreach (var item in dbtransitionHistory)
                    {
                        await _workflowFixtrue.db.WorkflowTransitionHistorys.DeleteAsync(item, tran);
                    }

                    // 4、删除工作流实例委托记录
                    var dbassigns = await _workflowFixtrue.db.WorkflowAssigns
                        .FindAllAsync(m => m.InstanceId == workflowInstanceProcessWithdrawDto.InstanceId);
                    foreach (var item in dbassigns)
                    {
                        await _workflowFixtrue.db.WorkflowAssigns.DeleteAsync(item, tran);
                    }

                    dbflowinstance.IsFinish = null;
                    dbflowinstance.Status = (int)WorkFlowStatus.UnSubmit;
                    dbflowinstance.MakerList = null;
                    dbflowinstance.UpdateTime=DateTime.Now.ToTimeStamp();
                    await _workflowFixtrue.db.WorkflowInstances.UpdateAsync(dbflowinstance,tran);

                    tran.Commit();
                    return true;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }
        #endregion

        #region 流程实例执行-催办【提醒审核人快速执行】
        public async Task<bool> WorkflowInstanceProcessUrgeAsync(WorkflowInstanceProcessUrgeDto workflowInstanceProcessUrgeDto)
        {
            // 1、查询工作流实例
            var dbflowinstance = await _workflowFixtrue.db.WorkflowInstances.FindByIdAsync(
                workflowInstanceProcessUrgeDto.InstanceId);

            // 2、创建工作流实例催办记录
            WorkflowUrge workflowUrge = new WorkflowUrge
            {
                // CreateUserId = workflowInstanceProcessUrgeDto.Sender,
                Sender = workflowInstanceProcessUrgeDto.Sender,
                InstanceId = workflowInstanceProcessUrgeDto.InstanceId,
                UrgeContent = workflowInstanceProcessUrgeDto.UrgeConent,
                UrgeType = workflowInstanceProcessUrgeDto.UrgeType,
                UrgeId = Guid.NewGuid().ToString(),

                // 给谁催办
                UrgeUser = dbflowinstance.MakerList,
                NodeId = dbflowinstance.ActivityId, // 当前节点
                NodeName = dbflowinstance.ActivityName,// 当前节点名称
            };
            await _workflowFixtrue.db.WorkflowUrges.InsertAsync(workflowUrge);

            // 3、发送短信【通知】【根据用户发送短信】

            return true;
        }
        #endregion

        public async Task<MyWorkflowWaitPageDto> MyWorkflowWaitGetListPageAsync(MyWorkflowWaitGetListPageDto myWorkflowWaitGetListPageDto)
        {
            WorkflowInstanceGetListPage workflowInstanceGetListPage=_mapper.Map<WorkflowInstanceGetListPage>(myWorkflowWaitGetListPageDto);
            WorkflowInstancePage workflowInstancePage = await _workflowFixtrue.db.WorkflowInstances.WorkflowInstanceGetListPageAsync(workflowInstanceGetListPage);
            MyWorkflowWaitPageDto myWorkflowWaitPageDto = _mapper.Map<MyWorkflowWaitPageDto>(workflowInstancePage);

            List<MyWorkflowWaitListDto> workflowWaitListDtos = myWorkflowWaitPageDto.myWorkflowWaitListDtos;
            // 2、查询工作流表
            foreach (var workflowWaitListDto in workflowWaitListDtos)
            {
                // 2.1、查询工作流表
                Workflow workflow = await _workflowFixtrue.db.Workflows.FindByIdAsync(workflowWaitListDto.FlowId);
                workflowWaitListDto.FlowName = workflow.FlowName;
                workflowWaitListDto.FormId = workflow.FormId;
            }
            // 3、查询工作流表单表
            foreach (var workflowWaitListDto in workflowWaitListDtos)
            {
                // 3.1、查询工作流表单表 根据表单Id
                WorkflowForm workflowForm = await _workflowFixtrue.db.WorkflowForms.FindByIdAsync(workflowWaitListDto.FormId);
                workflowWaitListDto.FormName = workflowForm.FormName;
                workflowWaitListDto.FormType = workflowForm.FormType;
            }

            return myWorkflowWaitPageDto;
        }
    }
}