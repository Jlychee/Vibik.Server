using Client.Models.Models.Entities;
using MediatR;

namespace Api.Application.Features.Metrics.GetMetrics;

public record GetMetricsQuery : IRequest<List<MetricModel>>;