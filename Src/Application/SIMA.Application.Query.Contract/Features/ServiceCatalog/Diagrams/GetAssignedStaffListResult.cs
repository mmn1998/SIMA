namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;

public class GetAssignedStaffListResult
{
    public long? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? StaffNumber { get; set; }
    public Department? Department { get; set; } // تغییر به شیء
    public Compnay? Company { get; set; } // تغییر به شیء
    public string? ActiveStatus { get; set; }
    public string? CreatedAt { get; set; }
}

public class GetAssignedStaffListResultWrapper
{
    public List<GetAssignedStaffListResult>? Data { get; set; }
}

public class Department
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}

public class Branch
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}

public class Compnay
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}

public class ResponsibleType
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}