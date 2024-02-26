using AutoBPM.Bpmn.Abstractions;
using AutoBPM.Bpmn.Models;
using AutoMapper;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Exeptions;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Create;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;

namespace SIMA.Application.Feaatures.WorkFlowEngine.BPMSes.Mappers
{
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
        public static ModifyFileContentArg Map(TDefinitions definition)
        {
            var progress = new List<CreateProgressArg>();
            var actors = new List<CreateWorkFlowActorArg>();
            var steps = new Dictionary<string, CreateStepArg>();
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
                foreach (var laneSet in process.LaneSets)
                {
                    foreach (ILane lane in laneSet.Children)
                    {
                        var actorId = IdHelper.GenerateUniqueId();
                        var actor = new CreateWorkFlowActorArg
                        {
                            ActiveStatusId = (long)ActiveStatusEnum.Active,
                            Code = actorId.ToString(),
                            CreatedAt = DateTime.Now,
                            Id = actorId,
                            Name = lane.Name
                        };
                        actors.Add(actor);
                        foreach (var flowId in lane.FlowNodeRefs)
                        {
                            var step = steps.GetValueOrDefault(flowId);
                            var workflowStepArg = new CreateWorkFlowActorStepArg
                            {
                                StepId = step.Id,
                                ActiveStatusId = (long)ActiveStatusEnum.Active,
                                WorkFlowActorId = actorId,
                                Id = IdHelper.GenerateUniqueId()
                            };
                            step.ActorStepArgs.Add(workflowStepArg);
                        }
                    }
                }
            }
            result.WorkFlowActors = actors;
            result.Progresses = progress;
            result.Steps = steps.Values.ToList();
            return result;
        }
        private static List<CreateProgressArg> NextFlows(IEnumerable<IFlowNode> flows, string processId, TDefinitions definition, ref Dictionary<string, CreateStepArg> steps)
        {
            var result = new List<CreateProgressArg>();
            foreach (var flow in flows)
            {
                var progress = ToProgres(flow, definition, ref steps);
                result.AddRange(progress);
                definition.Processes.FirstOrDefault(x => x.Id == processId).Remove(flow);
                var nextResult = NextFlows(flow.GetNextNodes(definition), processId, definition, ref steps);
                result.AddRange(nextResult);
            }

            return result;
        }
        private static List<CreateProgressArg> ToProgres(IFlowNode flow, TDefinitions definition, ref Dictionary<string, CreateStepArg> steps)
        {
            var actionType = flow.GetType().Name.Remove(0, 1);
            long? actionTypeValue = null;
            ActionTypeDic actionTypeDic = new ActionTypeDic();

            var ListActionTypeDic = actionTypeDic.GetactionTypeDic();

            if (ListActionTypeDic.Any(p => p.Key.ToLower() == actionType.ToLower()))
                actionTypeValue = ListActionTypeDic.FirstOrDefault(p => p.Key.ToLower() == actionType.ToLower()).Value;

            var result = new List<CreateProgressArg>();

            var flowSequences = flow.GetOutgoingFlows(definition);
            if (!steps.TryGetValue(flow.Id, out var step))
            {
                var flowArg = new CreateStepArg
                {
                    BpmnId = flow.Id,
                    Id = IdHelper.GenerateUniqueId(),
                    Name = flow.Name,
                    ActiveStatusId = (long)ActiveStatusEnum.Active,
                    ActionTypeId = actionTypeValue,
                    // IsLastStep = flow.GetNextNodes(definition) is null ? "1" : "0",
                };
                steps.Add(flow.Id, flowArg);
            }


            foreach (var sequence in flowSequences)
            {
                CreateProgressArg progressArg = new CreateProgressArg
                {
                    Id = IdHelper.GenerateUniqueId()
                };
                var nextFlow = flow.GetNextNodes(definition).FirstOrDefault(x => x.Id == sequence.TargetRef || x.Id == sequence.SourceRef);

                if (!steps.TryGetValue(sequence.TargetRef, out step))
                {
                    var actionTypenextFlow = nextFlow.GetType().Name.Remove(0, 1);

                    if (ListActionTypeDic.Any(p => p.Key.ToLower() == actionTypenextFlow.ToLower()))
                        actionTypeValue = ListActionTypeDic.FirstOrDefault(p => p.Key.ToLower() == actionTypenextFlow.ToLower()).Value;


                    step = new CreateStepArg
                    {
                        BpmnId = nextFlow.Id,
                        Id = IdHelper.GenerateUniqueId(),
                        Name = nextFlow.Name,
                        ActiveStatusId = (long)ActiveStatusEnum.Active,
                        ActionTypeId = actionTypeValue,
                        //  IsLastStep = nextFlow.GetNextNodes(definition) is null ? "1" : "0",
                    };
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
    }
}
