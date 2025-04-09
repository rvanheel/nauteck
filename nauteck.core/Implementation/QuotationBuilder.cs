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
    private static decimal _amount = 0;
    private static decimal _tax = 0;
    private static readonly CultureInfo cInfo = new("nl-NL");
    private static readonly DottedBorder dottedBorderLightGray = new(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY, 0.8f);
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


        foreach (var line in lines)
        {
            document = document.CreateQuotationDetails(line.Quantity, line.Amount, line.Description);
        }

        document
            .CreateQuotationTotals()
            .Write();


        return new BinaryData(stream.ToArray());
    }

    private static Document CreateDocument(Stream stream)
    {
        _amount = 0;
        _quotationTable = default;
        var document = new Document(new PdfDocument(new PdfWriter(stream)), iText.Kernel.Geom.PageSize.A4);
        document.SetLeftMargin(70);
        document.SetFont(CommonBuilder.GetDefaultFont());

        return document;
    }
    public static Document CreateQuotationDetails(this Document document, decimal aantal, decimal amount, string? omschrijving)
    {
        if (_quotationTable is null) return document;
        _amount += aantal * amount;
        _tax += (aantal * amount * 0.21m);

        var p06 = new Paragraph().Add(omschrijving);
        var p07 = new Paragraph().Add($"{aantal}").SetTextAlignment(TextAlignment.RIGHT);
        var p08 = new Paragraph().Add(CommonBuilder.GetMoneyFormat(cInfo, amount)).SetTextAlignment(TextAlignment.RIGHT);
        var p09 = new Paragraph().Add(CommonBuilder.GetMoneyFormat(cInfo, aantal * amount * 0.21m)).SetTextAlignment(TextAlignment.RIGHT);
        var p10 = new Paragraph().Add(CommonBuilder.GetMoneyFormat(cInfo, aantal * amount)).SetTextAlignment(TextAlignment.RIGHT);

        var c06 = CommonBuilder.CreateBorderlessCell().Add(p06);
        var c07 = CommonBuilder.CreateBorderlessCell().Add(p07);
        var c08 = CommonBuilder.CreateBorderlessCell().Add(p08);
        var c09 = CommonBuilder.CreateBorderlessCell().Add(p09);
        var c10 = CommonBuilder.CreateBorderlessCell().Add(p10);

        _quotationTable.AddCell(c06).AddCell(c07).AddCell(c08).AddCell(c09).AddCell(c10);

        return document;
    }
    public static Document CreateQuotationSection(this Document document, string? quotationNumber, DateTime quotationDate)
    {
        var p = new Paragraph()
        .SetFontSize(18)
        .Add("Offerte")
        .SetBorderBottom(dottedBorderLightGray)
        .SetPaddingBottom(5);
        document.Add(p);

        var h = p.GetHeight();

        Table table = new Table([130, 10, 350])
        .SetFontSize(10);

        CommonBuilder.AddRow(table, "Offertenummer", quotationNumber);
        CommonBuilder.AddRow(table, "Offertedatum", string.Format(cInfo, "{0:dd-MM-yyyy}", quotationDate));

        return document.Add(table);
    }
    public static Document CreateQuotationTable(this Document document)
    {
        _quotationTable = new Table([250, 50, 100, 70, 75])
                 .UseAllAvailableWidth()
                 .SetFontSize(10);
        var p1 = CommonBuilder.CreateBoldParagraph().Add("Omschrijving");
        var p2 = CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add("Aantal");
        var p3 = CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add("Bedrag ex BTW");
        var p4 = CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add("BTW");
        var p5 = CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add("Totaal");

        _quotationTable.AddCell(CommonBuilder.CreateBorderlessCell().Add(p1).SetPaddingBottom(5).SetBorderBottom(CommonBuilder.SolidBorderBlack));
        _quotationTable.AddCell(CommonBuilder.CreateBorderlessCell().Add(p2).SetPaddingBottom(5).SetBorderBottom(CommonBuilder.SolidBorderBlack));
        _quotationTable.AddCell(CommonBuilder.CreateBorderlessCell().Add(p3).SetPaddingBottom(5).SetBorderBottom(CommonBuilder.SolidBorderBlack));
        _quotationTable.AddCell(CommonBuilder.CreateBorderlessCell().Add(p4).SetPaddingBottom(5).SetBorderBottom(CommonBuilder.SolidBorderBlack));
        _quotationTable.AddCell(CommonBuilder.CreateBorderlessCell().Add(p5).SetPaddingBottom(5).SetBorderBottom(CommonBuilder.SolidBorderBlack));
        return document;
    }
    public static Document CreateQuotationTotals(this Document document)
    {
        if (_quotationTable is null) return document;
        var p11 = new Paragraph().Add("");
        var p12 = new Paragraph().Add("");
        var p13 = CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add("Subtotaal");
        var p14 = new Paragraph().Add("");
        var p15 = CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add(CommonBuilder.GetMoneyFormat(cInfo, _amount));

        var c11 = CommonBuilder.CreateBorderlessCell().Add(p11).SetPaddingTop(5).SetPaddingBottom(5);
        var c12 = CommonBuilder.CreateBorderlessCell().Add(p12).SetPaddingTop(5).SetPaddingBottom(5);
        var c13 = CommonBuilder.CreateBorderlessCell().Add(p13).SetPaddingTop(5).SetPaddingBottom(5);
        var c14 = CommonBuilder.CreateBorderlessCell().Add(p14).SetPaddingTop(5).SetPaddingBottom(5);
        var c15 = CommonBuilder.CreateBorderlessCell().Add(p15).SetPaddingTop(5).SetPaddingBottom(5);


        _quotationTable.AddCell(c11).AddCell(c12).AddCell(c13).AddCell(c14).AddCell(c15);

        var p21 = new Paragraph().Add("");
        var p22 = new Paragraph().Add("");
        var p23 = CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add("BTW");
        var p24 = new Paragraph().Add("");
        var p25 = CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add(CommonBuilder.GetMoneyFormat(cInfo, _tax));
        

        var c21 = CommonBuilder.CreateBorderlessCell().Add(p21).SetPaddingTop(5).SetPaddingBottom(5);
        var c22 = CommonBuilder.CreateBorderlessCell().Add(p22).SetPaddingTop(5).SetPaddingBottom(5);
        var c23 = CommonBuilder.CreateBorderlessCell().Add(p23).SetPaddingTop(5).SetPaddingBottom(5).SetBorderBottom(CommonBuilder.SolidBorderBlack);
        var c24 = CommonBuilder.CreateBorderlessCell().Add(p24).SetPaddingTop(5).SetPaddingBottom(5).SetBorderBottom(CommonBuilder.SolidBorderBlack);
        var c25 = CommonBuilder.CreateBorderlessCell().Add(p25).SetPaddingTop(5).SetPaddingBottom(5).SetBorderBottom(CommonBuilder.SolidBorderBlack);

        _quotationTable.AddCell(c21).AddCell(c22).AddCell(c23).AddCell(c24).AddCell(c25);

        var p16 = new Paragraph().Add("");
        var p17 = new Paragraph().Add("");
        var p18 = CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add("Totaal");
        var p19 = new Paragraph().Add("");
        var p20 = CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add(CommonBuilder.GetMoneyFormat(cInfo, _amount + _tax));

        var c16 = CommonBuilder.CreateBorderlessCell().Add(p16).SetPaddingTop(5).SetPaddingBottom(5);
        var c17 = CommonBuilder.CreateBorderlessCell().Add(p17).SetPaddingTop(5).SetPaddingBottom(5);
        var c18 = CommonBuilder.CreateBorderlessCell().Add(p18).SetPaddingTop(5).SetPaddingBottom(5);
        var c19 = CommonBuilder.CreateBorderlessCell().Add(p19).SetPaddingTop(5).SetPaddingBottom(5);
        var c20 = CommonBuilder.CreateBorderlessCell().Add(p20).SetPaddingTop(5).SetPaddingBottom(5);

        _quotationTable.AddCell(c16).AddCell(c17).AddCell(c18).AddCell(c19).AddCell(c20);
        return document.Add(_quotationTable);
    }
}
