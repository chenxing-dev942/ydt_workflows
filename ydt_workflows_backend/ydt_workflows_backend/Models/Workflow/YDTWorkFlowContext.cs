using JadeFramework.WorkFlow;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ydt_workflows_backend.Models
{
    /// <summary>
    /// 工作流上下文
    /// </summary>  
    public class YDTWorkFlowContext: WorkFlowContext
    {
        public YDTWorkFlowContext(WorkFlow workFlow)
        {
            this .WorkFlow = workFlow;
            //FlowJson反序列化
            dynamic jsonObject=JsonConvert.DeserializeObject(workFlow.FlowJSON);
            //获取节点
            workFlow.Nodes=GetNodes(jsonObject.nodes);
            //获取连线
            workFlow.Lines=GetLines(jsonObject.lines);
            //获取当前节点
            workFlow.ActivityNodeId=
                workFlow.ActivityNodeId==default(Guid)?
                workFlow.StartNodeId:workFlow.ActivityNodeId;
            //获取当前节点类型
            workFlow.ActivityNodeType = GetNodeType(workFlow.ActivityNodeId);
            //获取下一个节点
            workFlow.NextNodeId = GetNextNodeIds(workFlow.ActivityNodeId)[0];
            //获取下一个节点类型
            workFlow.NextNodeType = GetNodeType(workFlow.NextNodeId);
        }

        /// <summary>
        /// 1、获取节点集合（nodes）
        /// </summary>
        /// <param name="nodesobj"></param>
        /// <returns></returns>
        private Dictionary<Guid, FlowNode> GetNodes(dynamic nodesobj)
        {
            Dictionary<Guid,FlowNode> nodes=new Dictionary<Guid, FlowNode>();
            foreach (JObject item in nodesobj)
            {
                FlowNode node=item.ToObject<FlowNode>();
                if (!nodes.ContainsKey(node.Id))
                {
                    nodes.Add(node.Id,node);
                }
                if (node.Type==FlowNode.START)
                {
                    this.WorkFlow.StartNodeId=node.Id;
                }
            }
            return nodes;
        }

        /// <summary>
        /// 2、获取工作流节点及以节点为出发点的流程
        /// </summary>
        /// <param name="linesobj"></param>
        /// <returns></returns>
        private Dictionary<Guid, List<FlowLine>> GetLines(dynamic linesobj)
        {
            Dictionary<Guid, List<FlowLine>> lines = new Dictionary<Guid, List<FlowLine>>();
            foreach (JObject item in linesobj)
            {
                FlowLine line = item.ToObject<FlowLine>();
                if (!lines.ContainsKey(line.From))
                {
                    lines.Add(line.From, new List<FlowLine> { line });
                }
                else
                {
                    lines[line.From].Add(line);
                }
            }
            return lines;
        }

        /// <summary>
        /// 根据节点ID获取节点类型
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns> 
        private WorkFlowInstanceNodeType GetNodeType(Guid nodeId)
        {
            var _thisnode = this.WorkFlow.Nodes[nodeId];
            return _thisnode.NodeType();
        }

        /// <summary>
        /// 根据节点id获取下个节点id
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public List<Guid> GetNextNodeIds(Guid nodeId)
        {
            List<FlowLine> lines = this.WorkFlow.Lines[nodeId]; 
            return lines.Select(x => x.To).ToList();    
        }

        public Guid RejectNode(NodeRejectType rejectType, Guid? rejectNodeId)
        {
            switch (rejectType)
            {
                case NodeRejectType.PreviousStep:
                    return this.WorkFlow.PreviousId;
                case NodeRejectType.FirstStep:
                    return GetNextNodeIds(this.WorkFlow.StartNodeId).First();
                case NodeRejectType.ForOneStep:
                    if (!rejectNodeId.HasValue || rejectNodeId.Value == Guid.Empty)
                    {
                        throw new Exception("驳回节点没有值！");
                    }
                    var fornode = this.WorkFlow.Nodes[rejectNodeId.Value];
                    return fornode.Id;
                case NodeRejectType.UnHandled:
                default:
                    return this.WorkFlow.PreviousId;
            }
        }
    }
}
