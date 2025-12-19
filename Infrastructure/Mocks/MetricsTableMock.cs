using Client.Models.Models.Entities;
using Client.Models.Models.Enums;
using Infrastructure.Interfaces;

namespace Infrastructure.Mocks;

public class MetricsTableMock :IMetricsTable
{
    public Task<bool> AddRecord(string username, MetricType type)
    {
        throw new NotImplementedException();
    }

    public Task<List<MetricModel>> ReadAllRecord()
    {
        throw new NotImplementedException();
    }
}