using Client.Models.Models.Enums;

namespace Client.Models.Models.Entities;

public class MetricModel
{
    public int Id { get; set; }
    public string Username { get; set; }
    public MetricType Type { get; set; }
    public DateTime Time { get; set; }
}