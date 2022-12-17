using bug_questpdf.Models;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace bug_questpdf;

public class InvoiceDocument : IDocument
{
    private readonly InvoiceModel _model;
    
    public InvoiceDocument(InvoiceModel model)
    {
        _model = model;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.DefaultTextStyle(x => x.FontFamily(Fonts.Arial).FontSize(11));
            
            page.Header().Height(2, Unit.Centimetre).Element(ComposeHeader);
            page.Content().PaddingHorizontal(2, Unit.Centimetre)
                .Column(column => column.Item().Element(ComposeContent));
            page.Footer().Height(2, Unit.Centimetre)
                .PaddingHorizontal(2, Unit.Centimetre).Element(ComposeFooter);
        });
    }

    void ComposeHeader(IContainer container)
    {
        container.Column(column =>
        {
            column.Item().Row(row =>
            {
                row.RelativeItem(3).Height(0.3f, Unit.Centimetre).Background(Colors.Grey.Lighten2);
                row.RelativeItem(6).Height(0.3f, Unit.Centimetre).Background(Colors.Grey.Lighten3);
                row.RelativeItem(12).Height(0.3f, Unit.Centimetre).Background(Colors.Grey.Lighten4);
            });
        });
    }

    void ComposeContent(IContainer container)
    {
        container.Column(column =>
        {
            column.Item().Height(9, Unit.Centimetre);
            
            column.Item().PaddingTop(1, Unit.Centimetre).Element(ComposeItemsTable);
            // todo (bug): What causes the bug (ShowEntire)
            column.Item().ShowEntire().Column(x =>
            {
                x.Item().Element(ComposeTotalTable);
            });
        });
    }

    void ComposeItemsTable(IContainer container)
    {
        var headerStyle = TextStyle.Default.SemiBold();
            
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(7);
                columns.RelativeColumn(1.5f);
                columns.RelativeColumn(2.5f);
                columns.RelativeColumn(2);
                columns.RelativeColumn(2);
                columns.RelativeColumn(2);
            });
                
            table.Header(header =>
            {
                header.Cell().Text("-").Style(headerStyle);
                header.Cell().AlignRight().Text("-").Style(headerStyle);
                header.Cell().AlignRight().Text(text =>
                {
                    text.DefaultTextStyle(headerStyle);
                    text.Line("-");
                    text.Span("-");
                });
                header.Cell().AlignRight().Text("-").Style(headerStyle);
                header.Cell().AlignRight().Text(text =>
                {
                    text.DefaultTextStyle(headerStyle);
                    text.Line("-");
                    text.Span("-");
                });
                header.Cell().AlignRight().Text(text =>
                {
                    text.DefaultTextStyle(headerStyle);
                    text.Line("-");
                    text.Span("-");
                });
                header.Cell().ColumnSpan(6).PaddingTop(4).BorderBottom(0.75f).BorderColor(Colors.Black);
            });
            
            var lastItem = _model.Items.LastOrDefault();
            _model.Items.ForEach(item =>
            {
                table.Cell().Element(CellStyle).Element(item.ComposeDescription);
                table.Cell().Element(CellStyle).AlignRight().Text(item.Quantity);
                table.Cell().Element(CellStyle).AlignRight().AlignRight()
                    .Text("-");
                table.Cell().Element(CellStyle).AlignRight().AlignRight().Text("-");
                table.Cell().Element(CellStyle).AlignRight().AlignRight()
                    .Text("-");
                table.Cell().Element(CellStyle).AlignRight().AlignRight().Text("-");
                table.Cell().ColumnSpan(6).ShowIf(item != lastItem).BorderBottom(0.75f).BorderColor(Colors.Grey.Lighten2);
                
                static IContainer CellStyle(IContainer container) => container.PaddingVertical(5);
            });
        });
    }

    void ComposeTotalTable(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem();
            row.RelativeItem().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(6);
                    columns.RelativeColumn(2.5f);
                });

                table.Cell().Element(CellStyle).Text("-");
                table.Cell().Element(CellStyle).AlignRight().Text("-");
                table.Cell().ColumnSpan(2).BorderBottom(0.75f).BorderColor(Colors.Grey.Lighten2);
                
                table.Cell().Element(CellStyle).Text("-");
                table.Cell().Element(CellStyle).AlignRight().Text("-");
                table.Cell().ColumnSpan(2).BorderBottom(0.75f).BorderColor(Colors.Grey.Lighten2);
                
                table.Cell().Element(CellStyle).Text("-");
                table.Cell().Element(CellStyle).AlignRight().Text("-");
                table.Cell().ColumnSpan(2).BorderBottom(0.75f).BorderColor(Colors.Grey.Lighten2);
                
                table.Cell().Element(CellStyle).Text("-");
                table.Cell().Element(CellStyle).AlignRight().Text("-");
                table.Cell().ColumnSpan(2).BorderBottom(0.75f).BorderColor(Colors.Grey.Lighten2);
                
                table.Cell().Element(CellStyle).Text("-");
                table.Cell().Element(CellStyle).AlignRight().Text("-");
                table.Cell().ColumnSpan(2).BorderBottom(0.75f).BorderColor(Colors.Grey.Lighten2);
                
                table.Cell().Element(CellStyle).Text(text =>
                {
                    text.Line("-");
                    text.Span("-");
                });
                table.Cell().Element(CellStyle).AlignRight().Text("-");
                table.Cell().ColumnSpan(2).BorderBottom(0.75f).BorderColor(Colors.Grey.Lighten2);
                
                table.Cell().Element(CellStyle).Text("-").SemiBold();
                table.Cell().Element(CellStyle).AlignRight().Text("-").SemiBold();
                table.Cell().ColumnSpan(2).BorderBottom(0.75f).BorderColor(Colors.Grey.Lighten2);

                static IContainer CellStyle(IContainer container) => container.PaddingVertical(5);
            });
        });
    }

    void ComposeFooter(IContainer container)
    {
        container.Column(column =>
        {
            column.Item().PaddingVertical(0.2f, Unit.Centimetre).LineHorizontal(0.75f).LineColor(Colors.Black);
            column.Item().Row(row =>
            {
                row.RelativeItem().Text(_model.InvoiceId).FontSize(8);
                row.RelativeItem().Text(text =>
                {
                    text.DefaultTextStyle(x => x.FontSize(8));
                    text.AlignRight();
                    text.Span("Page ");
                    text.CurrentPageNumber();
                    text.Span(" / ");
                    text.TotalPages();
                });
            });
        });
    }
}
