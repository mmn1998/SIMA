using AutoBPM.Bpmn.Abstractions;
using AutoBPM.Bpmn.Models;
using AutoMapper;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using SIMA.Application.Contract.Features.WorkFlowEngine.BPMSes;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Create;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Xml;
using System.Xml.Serialization;
using workFlow = SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
namespace SIMA.Application.Feaatures.WorkFlowEngine.BPMSes.Mappers;

public class BpmsMapper : Profile
{
    private ISimaIdentity _simaIdentity;
    public BpmsMapper(ISimaIdentity simaIdentity)
    {
        _simaIdentity = simaIdentity;
        CreateMap<TDefinitions, CreateWorkFlowArg>()
            .ForMember(x => x.Id, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()))
            .ForMember(x => x.BpmnId, opt => opt.MapFrom(src => src.Id));
    }
    public static ModifyFileContentArg Map(ModifyBpmsCommand request, workFlow.WorkFlow workFlow)
    {
        try
        {
            var content = JsonConvert.DeserializeObject<string>(request.Data);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(content);
            XmlSerializer serializer = new XmlSerializer(typeof(TDefinitions));
            using (XmlReader reader = XmlReader.Create(new StringReader(content)))
            {
                var data = (TDefinitions)serializer.Deserialize(reader);
                var result = Map(data, workFlow);
                result.FileContent = content;
                return result;
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    public static ModifyFileContentArg Map(TDefinitions definition, workFlow.WorkFlow workFlow)
    {
        var progress = new List<ProgressArg>();
        var actors = new List<WorkFlowActorArg>();
        var steps = new Dictionary<string, StepArg>();
        var result = new ModifyFileContentArg
        {
            BpmnId = definition.Id,
            ModifyAt = DateTime.Now
            // ModifyBy= _simaIdentity.UserId
        };

        foreach (var process in definition.Processes)
        {
            var startNodes = process.StartNodes.ToList();
            foreach (var node in startNodes)
            {
                var startProgress = ModifyProgres(node, definition, workFlow, ref steps);
                progress.AddRange(startProgress);
                var nextNodes = node.GetNextNodes(definition);
                var progresses = ModifyNextFlows(nextNodes, workFlow, process.Id, definition, ref steps);
                progress.AddRange(progresses);
            }
            foreach (var laneSet in process.LaneSets)
            {
                foreach (ILane lane in laneSet.Children)
                {
                    var actorId = IdHelper.GenerateUniqueId();
                    var actor = new WorkFlowActorArg
                    {
                        BpmnId = lane.Id,
                        ActiveStatusId = (long)ActiveStatusEnum.Active,
                        Code = actorId.ToString(),
                        CreatedAt = DateTime.Now,
                        Id = actorId,
                        Name = lane.Name
                    };
                    var existActor = workFlow.WorkFlowActors.FirstOrDefault(x => x.BpmnId == lane.Id);
                    if (existActor != null)
                    {
                        actor.Code = existActor.Code;
                        actor.Id = existActor.Id.Value;
                    }
                    actors.Add(actor);
                    foreach (var flowId in lane.FlowNodeRefs)
                    {
                        var step = steps.GetValueOrDefault(flowId);
                        var workflowStepArg = new CreateWorkFlowActorStepArg
                        {
                            BpmnId = flowId,
                            StepId = step.Id,
                            ActiveStatusId = (long)ActiveStatusEnum.Active,
                            WorkFlowActorId = actorId,
                            ActorBpmnId = actor.BpmnId,
                            Id = IdHelper.GenerateUniqueId()
                        };
                        if (existActor != null)
                        {
                            var existActorStep = existActor.WorkFlowActorSteps.FirstOrDefault(x => x.BpmnId == flowId);
                            if (existActorStep != null)
                            {
                                workflowStepArg.Id = existActorStep.Id.Value;
                            }
                        }

                        step.ActorStepArgs.Add(workflowStepArg);
                    }
                }
            }
        }
        result.WorkFlowActors = actors;
        result.Progresses = progress;
        foreach (var step in steps.Values)
        {
            if (string.IsNullOrEmpty(step.Name) && step.ActionTypeId == (int)ActionTypeEnum.startEvent)
            {
                step.Name = "شروع فرایند";
            }
            if (string.IsNullOrEmpty(step.Name) && step.ActionTypeId == (int)ActionTypeEnum.endEvent)
            {
                step.Name = "پایان فرایند";
            }
        }
        result.Steps = steps.Values.ToList();
        return result;
    }
    public static ModifyFileContentArg Map(CreateBpmsCommand request)
    {
        var content = JsonConvert.DeserializeObject<string>(request.Data);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(content);
        XmlSerializer serializer = new XmlSerializer(typeof(TDefinitions));
        using (XmlReader reader = XmlReader.Create(new StringReader(content)))
        {
            var data = (TDefinitions)serializer.Deserialize(reader);

            var result = Map(data);
            result.FileContent = content;
            return result;
        }
    }
    public static ModifyFileContentArg Map(TDefinitions definition)
    {
        var progress = new List<ProgressArg>();
        var actors = new List<WorkFlowActorArg>();
        var steps = new Dictionary<string, StepArg>();
        var result = new ModifyFileContentArg
        {
            BpmnId = definition.Id,
            ModifyAt = DateTime.Now
            // ModifyBy= _simaIdentity.UserId
        };

        foreach (var process in definition.Processes)
        {
            var startNodes = process.StartNodes.ToList();
            foreach (var node in startNodes)
            {
                var startProgress = ToProgres(node, definition, ref steps);
                progress.AddRange(startProgress);
                var nextNodes = node.GetNextNodes(definition);
                var progresses = NextFlows(nextNodes, process.Id, definition, ref steps);
                progress.AddRange(progresses);
            }
            int counter = 1;
            foreach (var laneSet in process.LaneSets)
            {
                foreach (TLane lane in laneSet.Children)
                {
                    if (lane.ChildLaneSet is not null)
                    {
                        foreach (var item in lane.ChildLaneSet.Lane)
                        {
                            if (item.ChildLaneSet != null)
                            {
                                foreach (var child in item.ChildLaneSet.Lane)
                                {
                                    var actor = AddActor(lane.Id, child.Name);
                                    actors.Add(actor);
                                    foreach (var flowId in child.FlowNodeRef)
                                    {
                                        var step = steps.GetValueOrDefault(flowId);
                                        if (step.ActionTypeId == (int)ActionTypeEnum.task)
                                        {
                                            step.CompleteName = child.Name + " - " + step.Name + " - " + counter;
                                            counter++;
                                        }
                                        var workflowStepArg = AddActorStep(flowId, step.Id, actor.Id);
                                        step.ActorStepArgs.Add(workflowStepArg);
                                    }
                                }
                            }
                            else
                            {
                                var actor = AddActor(lane.Id, item.Name);
                                actors.Add(actor);
                                foreach (var flowId in item.FlowNodeRef)
                                {
                                    var step = steps.GetValueOrDefault(flowId);
                                    if (step.ActionTypeId == (int)ActionTypeEnum.task)
                                    {
                                        step.CompleteName = item.Name + " - " + step.Name + " - " + counter;
                                        counter++;
                                    }
                                    var workflowStepArg = AddActorStep(flowId, step.Id, actor.Id);
                                    step.ActorStepArgs.Add(workflowStepArg);
                                }
                            }

                        }

                    }
                    else
                    {
                        var actor = AddActor(lane.Id, lane.Name);
                        actors.Add(actor);
                        foreach (var flowId in lane.FlowNodeRef)
                        {
                            var step = steps.GetValueOrDefault(flowId);
                            if (step.ActionTypeId == (int)ActionTypeEnum.task)
                            {
                                step.CompleteName = lane.Name + " - " + step.Name + " - " + counter;
                                counter++;
                            }
                            var workflowStepArg = AddActorStep(flowId, step.Id, actor.Id);
                            step.ActorStepArgs.Add(workflowStepArg);
                        }
                    }
                }
            }
        }
        result.WorkFlowActors = actors;
        result.Progresses = progress;
        foreach (var step in steps.Values)
        {
            if (string.IsNullOrEmpty(step.Name) && step.ActionTypeId == (int)ActionTypeEnum.startEvent)
            {
                step.Name = "شروع فرایند";
            }
            if (string.IsNullOrEmpty(step.Name) && step.ActionTypeId == (int)ActionTypeEnum.endEvent)
            {
                step.Name = "پایان فرایند";
            }
        }
        result.Steps = steps.Values.ToList();
        return result;
    }
    private static List<ProgressArg> NextFlows(IEnumerable<IFlowNode> flows, string processId, TDefinitions definition, ref Dictionary<string, StepArg> steps)
    {
        var result = new List<ProgressArg>();
        foreach (var flow in flows)
        {
            var progress = ToProgres(flow, definition, ref steps);
            result.AddRange(progress);
            definition.Processes.FirstOrDefault(x => x.Id == processId)?.Remove(flow);
            var nextResult = NextFlows(flow.GetNextNodes(definition), processId, definition, ref steps);
            result.AddRange(nextResult);
        }

        return result;
    }
    private static List<ProgressArg> ModifyNextFlows(IEnumerable<IFlowNode> flows, workFlow.WorkFlow workFlow, string processId, TDefinitions definition, ref Dictionary<string, StepArg> steps)
    {
        var result = new List<ProgressArg>();
        foreach (var flow in flows)
        {
            var progress = ModifyProgres(flow, definition, workFlow, ref steps);
            result.AddRange(progress);
            definition.Processes.FirstOrDefault(x => x.Id == processId).Remove(flow);
            var nextResult = ModifyNextFlows(flow.GetNextNodes(definition), workFlow, processId, definition, ref steps);
            result.AddRange(nextResult);
        }

        return result;
    }
    private static List<ProgressArg> ToProgres(IFlowNode flow, TDefinitions definition, ref Dictionary<string, StepArg> steps)
    {
        var actionType = flow.GetType().Name.Remove(0, 1);
        long? actionTypeValue = null;
        ActionTypeDictionary actionTypeDic = new ActionTypeDictionary();

        var ListActionTypeDic = actionTypeDic.GetactionTypeDic();

        if (ListActionTypeDic.Any(p => p.Key.ToLower() == actionType.ToLower()))
            actionTypeValue = ListActionTypeDic.FirstOrDefault(p => p.Key.ToLower() == actionType.ToLower()).Value;

        var result = new List<ProgressArg>();

        var flowSequences = flow.GetOutgoingFlows(definition);
        if (!steps.TryGetValue(flow.Id, out var step))
        {
            var flowArg = new StepArg
            {
                BpmnId = flow.Id,
                Id = IdHelper.GenerateUniqueId(),
                Name = flow.Name,
                ActiveStatusId = (long)ActiveStatusEnum.Active,
                ActionTypeId = actionTypeValue,
                //NormalizedName = 
            };
            if (string.IsNullOrEmpty(flow.Name) && actionTypeValue == (int)ActionTypeEnum.startEvent)
            {
                flowArg.Name = "شروع فرایند";
            }
            steps.Add(flow.Id, flowArg);
        }


        foreach (var sequence in flowSequences)
        {
            ProgressArg progressArg = new ProgressArg
            {
                Id = IdHelper.GenerateUniqueId()
            };
            var nextFlow = flow.GetNextNodes(definition).FirstOrDefault(x => x.Id == sequence.TargetRef || x.Id == sequence.SourceRef);

            if (!steps.TryGetValue(sequence.TargetRef, out step))
            {
                var actionTypenextFlow = nextFlow.GetType().Name.Remove(0, 1);

                if (ListActionTypeDic.Any(p => p.Key.ToLower() == actionTypenextFlow.ToLower()))
                    actionTypeValue = ListActionTypeDic.FirstOrDefault(p => p.Key.ToLower() == actionTypenextFlow.ToLower()).Value;


                step = new StepArg
                {
                    BpmnId = nextFlow.Id,
                    Id = IdHelper.GenerateUniqueId(),
                    Name = nextFlow.Name,
                    ActiveStatusId = (long)ActiveStatusEnum.Active,
                    ActionTypeId = actionTypeValue,
                };
                if (string.IsNullOrEmpty(step.Name) && actionTypeValue == (int)ActionTypeEnum.endEvent)
                {
                    step.Name = "پایان فرایند";
                }
                steps.Add(nextFlow.Id, step);
            }
            if (step != null)
            {

                var isNextSource = step.BpmnId == sequence.SourceRef;
                if (isNextSource)
                {
                    progressArg.SourceId = step.Id;
                    progressArg.Source = step;
                }
                else
                {
                    progressArg.TargetId = step.Id;
                    progressArg.Target = step;
                }
            }
            var flowArg = steps.GetValueOrDefault(flow.Id);
            var isSource = flow.Id == sequence.SourceRef;
            if (isSource)
            {
                progressArg.SourceId = flowArg.Id;
                progressArg.Source = flowArg;
            }
            else
            {
                progressArg.TargetId = flowArg.Id;
                progressArg.Target = flowArg;
            }
            var id = IdHelper.GenerateUniqueId();
            progressArg.CreatedAt = DateTime.Now;
            progressArg.Description = sequence.Name;

            if (step.ActionTypeId != 6 && string.IsNullOrEmpty(sequence.Name))
                progressArg.Name = "تایید";

            progressArg.Name = sequence.Name;
            progressArg.BpmnId = sequence.Id;
            progressArg.ActiveStatusId = (long)ActiveStatusEnum.Active;
            flowArg.CreateProgresses.Add(progressArg);

            result.Add(progressArg);
        }

        return result;
    }

    private static List<ProgressArg> ModifyProgres(IFlowNode flow, TDefinitions definition, workFlow.WorkFlow workFlow, ref Dictionary<string, StepArg> steps)
    {
        var actionType = flow.GetType().Name.Remove(0, 1);
        long? actionTypeValue = null;
        ActionTypeDictionary actionTypeDic = new ActionTypeDictionary();

        var ListActionTypeDic = actionTypeDic.GetactionTypeDic();

        if (ListActionTypeDic.Any(p => p.Key.ToLower() == actionType.ToLower()))
            actionTypeValue = ListActionTypeDic.FirstOrDefault(p => p.Key.ToLower() == actionType.ToLower()).Value;

        var result = new List<ProgressArg>();

        var flowSequences = flow.GetOutgoingFlows(definition);
        var existStep = workFlow.Steps.FirstOrDefault(x => x.BpmnId == flow.Id);
        if (!steps.TryGetValue(flow.Id, out var step))
        {
            var flowArg = new StepArg
            {
                BpmnId = flow.Id,
                Id = IdHelper.GenerateUniqueId(),
                Name = flow.Name,
                ActiveStatusId = (long)ActiveStatusEnum.Active,
                ActionTypeId = actionTypeValue,
            };
            if (existStep != null)
            {
                flowArg.Id = existStep.Id.Value;
            }
            steps.Add(flow.Id, flowArg);
        }



        foreach (var sequence in flowSequences)
        {
            var existProgress = workFlow.Progresses.FirstOrDefault(x => x.BpmnId == sequence.Id);

            ProgressArg progressArg = new ProgressArg
            {
                Id = IdHelper.GenerateUniqueId()
            };
            if (existProgress != null)
            {
                progressArg.Id = existProgress.Id.Value;
            }
            var nextFlow = flow.GetNextNodes(definition).FirstOrDefault(x => x.Id == sequence.TargetRef || x.Id == sequence.SourceRef);

            if (!steps.TryGetValue(sequence.TargetRef, out step))
            {
                var actionTypenextFlow = nextFlow.GetType().Name.Remove(0, 1);

                if (ListActionTypeDic.Any(p => p.Key.ToLower() == actionTypenextFlow.ToLower()))
                    actionTypeValue = ListActionTypeDic.FirstOrDefault(p => p.Key.ToLower() == actionTypenextFlow.ToLower()).Value;

                var targetExistStep = workFlow.Steps.FirstOrDefault(x => x.BpmnId == sequence.TargetRef);
                step = new StepArg
                {
                    BpmnId = nextFlow.Id,
                    Id = IdHelper.GenerateUniqueId(),
                    Name = nextFlow.Name,
                    ActiveStatusId = (long)ActiveStatusEnum.Active,
                    ActionTypeId = actionTypeValue,
                };
                if (targetExistStep != null)
                {
                    step.Id = targetExistStep.Id.Value;
                }
                steps.Add(nextFlow.Id, step);
            }
            if (step != null)
            {

                var isNextSource = step.BpmnId == sequence.SourceRef;
                if (isNextSource)
                {
                    progressArg.SourceId = step.Id;
                    progressArg.Source = step;
                }
                else
                {
                    progressArg.TargetId = step.Id;
                    progressArg.Target = step;
                }
            }
            var flowArg = steps.GetValueOrDefault(flow.Id);
            var isSource = flow.Id == sequence.SourceRef;
            if (isSource)
            {
                progressArg.SourceId = flowArg.Id;
                progressArg.Source = flowArg;
            }
            else
            {
                progressArg.TargetId = flowArg.Id;
                progressArg.Target = flowArg;
            }
            var id = IdHelper.GenerateUniqueId();
            progressArg.CreatedAt = DateTime.Now;
            progressArg.Description = sequence.Name;
            progressArg.Name = sequence.Name;

            if (step.ActionTypeId != 6 && string.IsNullOrEmpty(sequence.Name))
                progressArg.Name = "تایید";

            progressArg.BpmnId = sequence.Id;
            progressArg.ActiveStatusId = (long)ActiveStatusEnum.Active;
            flowArg.CreateProgresses.Add(progressArg);

            result.Add(progressArg);
        }

        return result;
    }

    private static CreateWorkFlowActorStepArg AddActorStep(string flowId, long stepId, long actorId)
    {
        var workflowStepArg = new CreateWorkFlowActorStepArg
        {
            BpmnId = flowId,
            StepId = stepId,
            ActiveStatusId = (long)ActiveStatusEnum.Active,
            WorkFlowActorId = actorId,
            Id = IdHelper.GenerateUniqueId()
        };
        return workflowStepArg;
    }

    private static WorkFlowActorArg AddActor(string laneId, string name)
    {
        long actorId = IdHelper.GenerateUniqueId();
        var actor = new WorkFlowActorArg
        {
            BpmnId = laneId,
            ActiveStatusId = (long)ActiveStatusEnum.Active,
            Code = actorId.ToString(),
            CreatedAt = DateTime.Now,
            Id = actorId,
            Name = name
        };
        return actor;
    }
}
