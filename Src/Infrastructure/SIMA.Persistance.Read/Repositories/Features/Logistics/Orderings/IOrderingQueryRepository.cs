using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Logistics.Orderings;
using SIMA.Framework.Core.Repository;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.Logistics.Orderings;

public interface IOrderingQueryRepository : IQueryRepository
{
    Task<IEnumerable<GetAllOrderingssByLogesticSupplyIdQueryResult>> GetAllByLogisticsSupplyId(long logisticsSupplyId);
    Task<IEnumerable<GetAllOrderingItemsssByOrderingIdQueryResult>> GetAllOrderingItemsByOrderingId(long orderingId);
}
public class OrderingQueryRepository : IOrderingQueryRepository
{
    private readonly string _connectionString;
    public OrderingQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }
    public async Task<IEnumerable<GetAllOrderingssByLogesticSupplyIdQueryResult>> GetAllByLogisticsSupplyId(long logisticsSupplyId)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        string query = @"
                    select 
                    O.Id,
                    O.ReceiptNumber Name
                    from
                    Logistics.Ordering O
                    inner join Logistics.LogisticsSupply LS on LS.Id = O.LogisticsSupplyId and LS.ActiveStatusId<>3
                    where LS.Id = @logisticsSupplyId and O.ActiveStatusId<>3
";
        return await connection.QueryAsync<GetAllOrderingssByLogesticSupplyIdQueryResult>(query, new { logisticsSupplyId });
    }

    public async Task<IEnumerable<GetAllOrderingItemsssByOrderingIdQueryResult>> GetAllOrderingItemsByOrderingId(long orderingId)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        string query = @"
select 
OI.Id,
G.Name 
from
Logistics.Ordering O
join Logistics.OrderingItem OI on OI.OrderingId = O.Id and OI.ActiveStatusId<>3
join Logistics.LogisticsSupplyGoods LSG on LSG.Id = OI.LogisticsSupplyGoodsId
join Logistics.LogisticsRequestGoods LRG on LRG.Id = LSG.LogisticsRequestGoodsId
join Logistics.Goods G on LRG.GoodsId = G.Id
where O.Id = @Id and O.ActiveStatusId<>3
";
        return await connection.QueryAsync<GetAllOrderingItemsssByOrderingIdQueryResult>(query, new { Id = orderingId });
    }
}
