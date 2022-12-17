namespace bug_questpdf.Models;

public class InvoiceModel
{
    public string InvoiceId { get; init; } = default!;

    public List<OrderItem> Items { get; init; } = default!;
}
