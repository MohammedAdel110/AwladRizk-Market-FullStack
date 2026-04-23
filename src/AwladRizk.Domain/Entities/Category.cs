using AwladRizk.Domain.Common;

namespace AwladRizk.Domain.Entities;

public class Category : BaseEntity
{
    public string NameAr { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public int SortOrder { get; set; }

    // Navigation
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
