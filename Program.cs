using System.Diagnostics;
using bug_questpdf;
using bug_questpdf.Models;
using QuestPDF.Fluent;

InvoiceModel model = new()
{
    InvoiceId = "-",
    Items = new()
    {
        new()
        {
            ComposeDescription = x =>
            {
                x.Text(text =>
                {
                    text.Line("-").SemiBold();
                    text.Span("-");
                });
            },
            Quantity = 1,
            UnitPriceExcludingTax = 990
        },
        new()
        {
            ComposeDescription = x =>
            {
                x.Text(text =>
                {
                    text.Line("-").SemiBold();
                    text.Span("-");
                });
            },
            Quantity = 2,
            UnitPriceExcludingTax = 990
        },
        new()
        {
            ComposeDescription = x =>
            {
                x.Text(text =>
                {
                    text.Line("-").SemiBold();
                    text.Span("-");
                });
            },
            Quantity = 3,
            UnitPriceExcludingTax = 990
        },
        new()
        {
            ComposeDescription = x =>
            {
                x.Text(text =>
                {
                    text.Line("-").SemiBold();
                    text.Span("-");
                });
            },
            Quantity = 3,
            UnitPriceExcludingTax = 990
        },
        new()
        {
            ComposeDescription = x =>
            {
                x.Text(text =>
                {
                    text.Line("-").SemiBold();
                    text.Span("-");
                });
            },
            Quantity = 3,
            UnitPriceExcludingTax = 990
        },
        new()
        {
            ComposeDescription = x =>
            {
                x.Text(text =>
                {
                    text.Line("-").SemiBold();
                    text.Span("-");
                });
            },
            Quantity = 3,
            UnitPriceExcludingTax = 990
        }
    }
};

InvoiceDocument document = new(model);

const string filePath = "invoice.pdf";

document.GeneratePdf(filePath);

var process = new Process
{
    StartInfo = new ProcessStartInfo(filePath)
    {
        UseShellExecute = true
    }
};

process.Start();
