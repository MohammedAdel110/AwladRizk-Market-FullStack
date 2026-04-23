using AwladRizk.Domain.Common;

namespace AwladRizk.Domain.Entities;

public class Offer : BaseEntity
{
    public string TitleAr { get; set; } = string.Empty;
    public string TitleEn { get; set; } = string.Empty;
    public string DescriptionAr { get; set; } = string.Empty;
    public string DescriptionEn { get; set; } = string.Empty;
    public int DiscountPercent { get; set; }
    public string BadgeText { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public DateTime ValidUntil { get; set; }
    public bool IsActive { get; set; } = true;
}
