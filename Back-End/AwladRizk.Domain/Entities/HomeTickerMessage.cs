using AwladRizk.Domain.Common;

namespace AwladRizk.Domain.Entities;

public class HomeTickerMessage : BaseEntity
{
    public string TextAr { get; set; } = string.Empty;
    public string TextEn { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
