using System.Globalization;
using System.Resources;

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

using nauteck.data.Dto.Client;
using nauteck.data.Dto.Quotation;

namespace nauteck.core.Implementation;

public static class QuotationBuilder
{
    private static decimal _amount;
    private static decimal _tax;
    private static readonly CultureInfo CInfo = new("nl-NL");
    private static readonly DottedBorder DottedBorderLightGray = new(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY, 0.8f);
    private static Table? _quotationTable;

    public static BinaryData BuildQuotation(ClientDto clientDto, QuotationDto quotation, QuotationLineDto[] lines)
    {
        _amount = 0;
        _tax = 0;
        var stream = new MemoryStream();

        var document = CreateDocument(stream)
            .CreateHeader()

            .Add(CommonBuilder.LoremIpsum())
            .Add(CommonBuilder.LoremIpsum())

            .CreateAddress(clientDto)

            .Add(CommonBuilder.LoremIpsum())
            .Add(CommonBuilder.LoremIpsum())

            .CreateQuotationSection(quotation.Reference, quotation.Date)

            .Add(CommonBuilder.LoremIpsum())
            .Add(CommonBuilder.LoremIpsum())

            .CreateQuotationTable();


        document = lines.Aggregate(document, (current, line) => current.CreateQuotationDetails(line.Quantity, line.Amount,line.Tax / 100.0m, line.Description));

        document
            .CreateQuotationTotals()
            .Write();


        return new BinaryData(stream.ToArray());
    }

    private static Document CreateDocument(Stream stream)
    {
        _amount = 0;
        _tax = 0;
        _quotationTable = null;
        var document = new Document(new PdfDocument(new PdfWriter(stream)), iText.Kernel.Geom.PageSize.A4);
        document.SetLeftMargin(70);
        document.SetFont(CommonBuilder.GetDefaultFont());

        return document;
    }

    private static Document CreateQuotationDetails(this Document document, decimal aantal, decimal amount, decimal tax, string? omschrijving)
    {
        if (_quotationTable is null) return document;
        _amount += aantal * amount;
        _tax += aantal * amount * tax;
        
        AddCell(CommonBuilder.CreateBorderlessCell().Add(new Paragraph().Add(omschrijving)));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(new Paragraph().Add($"{aantal}").SetTextAlignment(TextAlignment.RIGHT)));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(new Paragraph().Add(CommonBuilder.GetNumberFormat(CInfo, amount)).SetTextAlignment(TextAlignment.RIGHT)));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(new Paragraph().Add(CommonBuilder.GetNumberFormat(CInfo, aantal * amount * tax)).SetTextAlignment(TextAlignment.RIGHT)));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(new Paragraph().Add(CommonBuilder.GetDecimalFormat(CInfo, tax * 100.0m)).SetTextAlignment(TextAlignment.RIGHT)));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(new Paragraph().Add(CommonBuilder.GetNumberFormat(CInfo, aantal * amount)).SetTextAlignment(TextAlignment.RIGHT)));

        return document;
    }

    private static Document CreateQuotationSection(this Document document, string? quotationNumber, DateTime quotationDate)
    {
        var p = new Paragraph()
        .SetFontSize(18)
        .Add("Offerte")
        .SetBorderBottom(DottedBorderLightGray)
        .SetPaddingBottom(5);
        document.Add(p);

        var table = new Table([130, 10, 350])
        .SetFontSize(10);

        CommonBuilder.AddRow(table, "Offertenummer", quotationNumber);
        CommonBuilder.AddRow(table, "Offertedatum", string.Format(CInfo, "{0:dd-MM-yyyy}", quotationDate));

        return document.Add(table);
    }
    private static Document CreateQuotationTable(this Document document)
    {
        _quotationTable = new Table([230, 50, 100, 60, 30, 75])
                 .UseAllAvailableWidth()
                 .SetFontSize(10);
        
        AddCellWithSolidBorderBottom(CommonBuilder.CreateBorderlessCell().Add(CommonBuilder.CreateBoldParagraph().Add("Omschrijving")),CommonBuilder.SolidBorderBlack);
        AddCellWithSolidBorderBottom(CommonBuilder.CreateBorderlessCell().Add(CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add("Aantal")),CommonBuilder.SolidBorderBlack);
        AddCellWithSolidBorderBottom(CommonBuilder.CreateBorderlessCell().Add(CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add("Bedrag ex BTW")),CommonBuilder.SolidBorderBlack);
        AddCellWithSolidBorderBottom(CommonBuilder.CreateBorderlessCell().Add(CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add("BTW")),CommonBuilder.SolidBorderBlack);
        AddCellWithSolidBorderBottom(CommonBuilder.CreateBorderlessCell().Add(CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add("")),CommonBuilder.SolidBorderBlack);
        AddCellWithSolidBorderBottom(CommonBuilder.CreateBorderlessCell().Add(CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add("Totaal")),CommonBuilder.SolidBorderBlack);

        return document;
    }
    private static void AddCell(Cell tableCell, int paddingTop = 5, int paddingBottom = 5)
    {
        _quotationTable?.AddCell(tableCell.SetPaddingTop(paddingTop).SetPaddingBottom(paddingBottom));
    }
    private static void AddCellWithSolidBorderBottom(Cell tableCell, SolidBorder solidBorder, int paddingTop = 5, int paddingBottom = 5)
    {
        _quotationTable?.AddCell(tableCell.SetPaddingTop(paddingTop).SetPaddingBottom(paddingBottom).SetBorderBottom(solidBorder));
    }
    private static Paragraph EmptyParagraph()
    {
        return new Paragraph().Add("");
    }
    private static Document CreateQuotationTotals(this Document document)
    {
        if (_quotationTable is null) return document;
        
        AddCell(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()));
        AddCellWithSolidBorderBottom(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()), CommonBuilder.SolidBorderBlack);
        AddCellWithSolidBorderBottom(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()), CommonBuilder.SolidBorderBlack);
        AddCellWithSolidBorderBottom(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()), CommonBuilder.SolidBorderBlack);
        AddCellWithSolidBorderBottom(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()), CommonBuilder.SolidBorderBlack);

        AddCell(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add("Subtotaal")));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add(CommonBuilder.GetNumberFormat(CInfo, _amount))));

        AddCell(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()));
        AddCellWithSolidBorderBottom(CommonBuilder.CreateBorderlessCell().Add(CommonBuilder.CreateParagraph(TextAlignment.RIGHT).Add("BTW")), CommonBuilder.SolidBorderBlack);
        AddCellWithSolidBorderBottom(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()), CommonBuilder.SolidBorderBlack);
        AddCellWithSolidBorderBottom(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()), CommonBuilder.SolidBorderBlack);
        AddCellWithSolidBorderBottom(CommonBuilder.CreateBorderlessCell().Add(CommonBuilder.CreateParagraph(TextAlignment.RIGHT).Add(CommonBuilder.GetNumberFormat(CInfo, _tax))), CommonBuilder.SolidBorderBlack);

        
        AddCell(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add("Totaal")));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add(CommonBuilder.GetMoneyFormat(CInfo, _amount + _tax))));
        return document.Add(_quotationTable);
    }
}
