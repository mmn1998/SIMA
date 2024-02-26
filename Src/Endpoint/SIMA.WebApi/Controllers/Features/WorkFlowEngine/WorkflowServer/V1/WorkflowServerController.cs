//using Grpc.Core;
//using MediatR;
//using SIMA.WorkFlowEngine.Application.Query.Contract.WorkFlow.grpc;
//using SIMA.WorkFlowEngine.Services;

//namespace SIMA.WorkFlowEngine.WebApi.Controllers.WorkflowServer.V1
//{
//    public class WorkflowServerController : SIMA.WorkFlowEngine.Services.WorkflowServer.WorkflowServerBase
//    {
//        private readonly IMediator _mediator;

//        public WorkflowServerController(IMediator mediator)
//        {
//            _mediator = mediator;
//        }
//        public override async Task<GetWorkflowInfoByIdResponse> GetWorkflowInfoById(GetWorkflowInfoByIdRequest request, ServerCallContext context)
//        {
//            try
//            {
//                var query = new GetWorkflowInfoByIdResponseQuery { Id = request.Id };
//                var result = await _mediator.Send(query);
//                return new GetWorkflowInfoByIdResponse { Ok = true,Id = result.Data.Id, CurrentStateId = result.Data.CurrentStateId, CurrentStepId = result.Data.CurrentStepId, Message = ""};
//            }
//            catch (Exception ex)
//            {

//                return new GetWorkflowInfoByIdResponse { Ok = false, Message = ex.Message };
//            }
//        }
//    }
//}
