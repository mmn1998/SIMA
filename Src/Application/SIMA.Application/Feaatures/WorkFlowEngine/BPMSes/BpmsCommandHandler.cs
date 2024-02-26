using AutoBPM.Bpmn.Models;
using AutoMapper;
using Newtonsoft.Json;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.WorkFlowEngine.BPMSes;
using SIMA.Application.Feaatures.WorkFlowEngine.BPMSes.Mappers;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Interface;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SIMA.Application.Feaatures.WorkFlowEngine.BPMSes;

public class BpmsCommandHandler : ICommandHandler<BpmsCommand, Result<long>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWorkFlowRepository _workflowRepository;
    private readonly IProgressRepository _progressRepository;
    private readonly IWorkFlowActorRepository _actorRepository;

    public BpmsCommandHandler(IMapper mapper,
        IUnitOfWork unitOfWork,
        IWorkFlowRepository workflowRepository,
        IProgressRepository progressRepository,
        IWorkFlowActorRepository actorRepository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _workflowRepository = workflowRepository;
        _progressRepository = progressRepository;
        _actorRepository = actorRepository;
    }
    public async Task<Result<long>> Handle(BpmsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var workflow = await _workflowRepository.GetById(request.WorkFlowId);  
            var content = JsonConvert.DeserializeObject<string>(request.Data);
            long workFlowId = 0;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(content);
            XmlSerializer serializer = new XmlSerializer(typeof(TDefinitions));
            using (XmlReader reader = XmlReader.Create(new StringReader(content)))
            {
                var data = (TDefinitions)serializer.Deserialize(reader);
                #region --workflow --
                var arg = BpmsMapper.Map(data);
                arg.FileContent = content;
                workflow.Modify(arg);
                await _unitOfWork.SaveChangesAsync();
                workFlowId = workflow.Id.Value;
            }
            return Result.Ok(workFlowId);

        }
        catch (Exception ex)
        {
            throw SimaResultException.FileUploadError;
        }

        #endregion

    }


    public string ReadTextFile(string fileName)
    {

        string result = string.Empty;
        var textFilesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "textfiles");

        var filePath = Path.Combine(textFilesDirectory, fileName);

        if (!File.Exists(filePath))
        {
            result = "فایل مورد نظر یافت نشد.";
        }

        if (result == string.Empty)
        {
            var fileContent = File.ReadAllText(filePath, Encoding.UTF8);
            result = fileContent;
        }

        return result;


    }
}
