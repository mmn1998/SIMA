using SIMA.Domain.Models.Features.AccessManagement.AccessRequests.Args;
using SIMA.Domain.Models.Features.AccessManagement.AccessRequests.Exceptions;
using SIMA.Domain.Models.Features.AccessManagement.AccessRequests.ValueObjects;
using SIMA.Domain.Models.Features.Auths.AccessTypes.Entities;
using SIMA.Domain.Models.Features.Auths.AccessTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.NetworkProtocols.Entities;
using SIMA.Domain.Models.Features.Auths.NetworkProtocols.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System;
using System.Text;

namespace SIMA.Domain.Models.Features.AccessManagement.AccessRequests.Entities;
 
public class AccessRequest : Entity, IAggregateRoot
{
    private AccessRequest() { }
    private AccessRequest(CreateAccessRequestArg arg)
    {
        Id = new(arg.Id);
        IpDestinationTo = arg.IpDestinationTo;
        IpDestinationFrom = arg.IpDestinationFrom;
        IpSourceFrom = arg.IpSourceFrom;
        IpSourceTo = arg.IpSourceTo;
        PortDestinationFrom = arg.PortDestinationFrom;
        PortDestinationTo = arg.PortDestinationTo;
        NetworkProtocolId = new(arg.NetworkProtocolId);
        IssueId = new(arg.IssueId);
        AccessTypeId = new(arg.AccessTypeId);
        StartDate = arg.StartDate;
        EndDate = arg.EndDate;
        AccessDurationEndTime = arg.AccessDurationEndTime;
        AccessDurationStartTime = arg.AccessDurationStartTime;
        Description = arg.Description;
        HasAutoRenew = arg.HasAutoRenew;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static AccessRequest Create(CreateAccessRequestArg arg)
    {
        CreateGuards(arg);
        return new AccessRequest(arg);
    }
    public void Modify(ModifyAccessRequestArg arg)
    {
        ModifyGuards(arg);
        IpDestinationTo = arg.IpDestinationTo;
        IpDestinationFrom = arg.IpDestinationFrom;
        IpSourceFrom = arg.IpSourceFrom;
        IpSourceTo = arg.IpSourceTo;
        PortDestinationFrom = arg.PortDestinationFrom;
        PortDestinationTo = arg.PortDestinationTo;
        NetworkProtocolId = new(arg.NetworkProtocolId);
        IssueId = new(arg.IssueId);
        AccessTypeId = new(arg.AccessTypeId);
        StartDate = arg.StartDate;
        EndDate = arg.EndDate;
        AccessDurationEndTime = arg.AccessDurationEndTime;
        AccessDurationStartTime = arg.AccessDurationStartTime;
        Description = arg.Description;
        HasAutoRenew = arg.HasAutoRenew;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static void CreateGuards(CreateAccessRequestArg arg)
    {
        arg.NullCheck();
        arg.IpSourceFrom.NullCheck();
        arg.IpDestinationFrom.NullCheck();
        arg.PortDestinationFrom.NullCheck();
        arg.AccessTypeId.NullCheck();
        arg.IssueId.NullCheck();
        arg.ActiveStatusId.NullCheck();
        arg.Id.NullCheck();
        arg.NetworkProtocolId.NullCheck();
        if (arg.StartDate < arg.EndDate) throw AccessRequestExceptions.DateOnlyException;
        if (arg.AccessDurationStartTime < arg.AccessDurationEndTime) throw AccessRequestExceptions.TimeOnlyException;
    }
    private void ModifyGuards(ModifyAccessRequestArg arg)
    {
        arg.NullCheck();
        arg.IpSourceFrom.NullCheck();
        arg.IpDestinationFrom.NullCheck();
        arg.PortDestinationFrom.NullCheck();
        arg.AccessTypeId.NullCheck();
        arg.IssueId.NullCheck();
        arg.ActiveStatusId.NullCheck();
        arg.Id.NullCheck();
        arg.NetworkProtocolId.NullCheck();
        if (arg.StartDate < arg.EndDate) throw AccessRequestExceptions.DateOnlyException;
        if (arg.AccessDurationStartTime < arg.AccessDurationEndTime) throw AccessRequestExceptions.TimeOnlyException;
    }
    #endregion
    public AccessRequestId Id { get; private set; }
    public string? IpSourceFrom { get; private set; }
    public string? IpSourceTo { get; private set; }
    public string? IpDestinationFrom { get; private set; }
    public string? IpDestinationTo { get; private set; }
    public string? PortDestinationFrom { get; private set; }
    public string? PortDestinationTo { get; private set; }
    public NetworkProtocolId NetworkProtocolId { get; private set; }
    public virtual NetworkProtocol NetworkProtocol { get; private set; }
    public AccessTypeId AccessTypeId { get; private set; }
    public virtual AccessType AccessType { get; private set; }
    public IssueId IssueId { get; private set; }
    public virtual Issue Issue { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    public TimeOnly AccessDurationStartTime { get; private set; }
    public TimeOnly AccessDurationEndTime { get; private set; }
    public string? Description { get; private set; }
    public string? HasAutoRenew { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    private List<AccessRequestDocument> _accessRequestDocuments = new();
    public ICollection<AccessRequestDocument> AccessRequestDocuments => _accessRequestDocuments;
}
 