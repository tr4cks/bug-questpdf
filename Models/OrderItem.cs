using QuestPDF.Infrastructure;

namespace bug_questpdf.Models;

public class OrderItem
{
    public Action<IContainer> ComposeDescription { get; init; } = default!;
    
    public long Quantity { get; init; }
    
    public long UnitPriceExcludingTax { get; init; }
    
    // ReSharper disable once InconsistentNaming
    public long? VATRate { get; init; }
}
