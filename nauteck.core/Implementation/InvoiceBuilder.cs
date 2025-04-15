using System.Threading;

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

using Mysqlx.Expr;

using nauteck.data.Dto.Invoice;
using nauteck.data.Dto.Quotation;

namespace nauteck.core.Implementation;

public static class InvoiceBuilder
{
    private static decimal _amount;
    private static readonly CultureInfo CInfo = new("nl-NL");
    private static readonly DottedBorder DottedBorderLightGray = new(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY, 0.8f);
    private static Table? _invoiceTable;
    private static IEnumerable<IGrouping<decimal, InvoiceLineDto>>? _taxes;

    public static BinaryData BuildInvoice(ClientDto clientDto, InvoiceDto invoice, InvoiceLineDto[] lines)
    {
        _amount = 0;
        var stream = new MemoryStream();
        _taxes = lines.GroupBy(r => r.Tax);

        var document = CreateDocument(stream)
            .CreateHeader()

            .Add(CommonBuilder.LoremIpsum())
            .Add(CommonBuilder.LoremIpsum())

            .CreateAddress(clientDto)

            .Add(CommonBuilder.LoremIpsum())
            .Add(CommonBuilder.LoremIpsum())

            .CreateInvoiceSection(invoice.Date, invoice.Reference)

            .Add(CommonBuilder.LoremIpsum())
            .Add(CommonBuilder.LoremIpsum())

            .CreateInvoiceTable();


        document = lines.Aggregate(document, (current, line) => current.CreateInvoiceDetails(line.Quantity, line.Amount, line.Tax / 100.0m, line.Description));

        document
            .CreateInvoiceTotals()
            .CreatePaymentDetails(invoice.Reference)
            .CreateFooter()
            .Write();

        return new BinaryData(stream.ToArray());
    }
    private static Document CreateDocument(Stream stream)
    {
        _amount = 0;
        _invoiceTable = null;
        var document = new Document(new PdfDocument(new PdfWriter(stream)), iText.Kernel.Geom.PageSize.A4);
        document.SetLeftMargin(70);
        document.SetFont(CommonBuilder.GetDefaultFont());

        return document;
    }
    public static Document CreateFooter(this Document document)
    {
        var p = new Paragraph()
            .Add("Wij verzoeken u vriendelijk het bovenstaande bedrag binnen 14 dagen over te maken onder vermelding van het factuurnummer. Op alle diensten zijn onze algemene voorwaarden van toepassing. Deze kunt u downloaden van onze website.")
            .SetFixedPosition(1, 70, 100, 450)
            .SetFontSize(10);

        return document.Add(p);
    }
    public static Document CreateInvoiceDetails(this Document document, decimal aantal, decimal amount, decimal tax, string? omschrijving)
    {
        if (_invoiceTable is null) return document;
        _amount += aantal * amount;

        AddCell(CommonBuilder.CreateBorderlessCell().Add(new Paragraph().Add(omschrijving)));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(new Paragraph().Add($"{aantal}").SetTextAlignment(TextAlignment.RIGHT)));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(new Paragraph().Add(CommonBuilder.GetNumberFormat(CInfo, amount)).SetTextAlignment(TextAlignment.RIGHT)));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(new Paragraph().Add(CommonBuilder.GetPercentageFormat(CInfo, tax * 100.0m)).SetTextAlignment(TextAlignment.RIGHT)));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(new Paragraph().Add(CommonBuilder.GetNumberFormat(CInfo, aantal * amount)).SetTextAlignment(TextAlignment.RIGHT)));

        return document;
    }    
    public static Document CreateInvoiceSection(this Document document, DateTime factuurDatum, string? referentie)
    {
        var p = new Paragraph()
        .SetFontSize(18)
        .Add("Factuur")
        .SetBorderBottom(DottedBorderLightGray)
        .SetPaddingBottom(5);
        document.Add(p);


        var table = new Table([UnitValue.CreatePercentValue(20), UnitValue.CreatePercentValue(2.5f), UnitValue.CreatePercentValue(77.5f)])
       .SetFontSize(10)
       .UseAllAvailableWidth();

        CommonBuilder.AddRow(table, "Factuurnummer", referentie);
        CommonBuilder.AddRow(table, "Factuurdatum", string.Format(CInfo, "{0:dd-MM-yyyy}", factuurDatum));
        CommonBuilder.AddRow(table, "Vervaldatum", string.Format(CInfo, "{0:dd-MM-yyyy}", factuurDatum.AddMonths(1)));

        return document.Add(table);
    }
    public static Document CreateInvoiceTable(this Document document)
    {
        _invoiceTable = new Table(
         [
             UnitValue.CreatePercentValue(53),
            UnitValue.CreatePercentValue(13),
            UnitValue.CreatePercentValue(15),
            UnitValue.CreatePercentValue(7),
            UnitValue.CreatePercentValue(12)
         ])
         .UseAllAvailableWidth()
         .SetFontSize(10);

        AddCellWithSolidBorderBottom(CommonBuilder.CreateBorderlessCell().Add(CommonBuilder.CreateBoldParagraph().Add("Omschrijving")), CommonBuilder.SolidBorderBlack);
        AddCellWithSolidBorderBottom(CommonBuilder.CreateBorderlessCell().Add(CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add("Aantal")), CommonBuilder.SolidBorderBlack);
        AddCellWithSolidBorderBottom(CommonBuilder.CreateBorderlessCell().Add(CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add("Bedrag")), CommonBuilder.SolidBorderBlack);
        AddCellWithSolidBorderBottom(CommonBuilder.CreateBorderlessCell().Add(CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add("BTW")), CommonBuilder.SolidBorderBlack);
        AddCellWithSolidBorderBottom(CommonBuilder.CreateBorderlessCell().Add(CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add("Totaal")), CommonBuilder.SolidBorderBlack);

        return document;
    }
    public static Document CreateInvoiceTotals(this Document document)
    {
        if (_invoiceTable is null) return document;

        decimal tax = 0.0m;

        AddCell(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()));
        AddCellWithSolidBorderBottom(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()), CommonBuilder.SolidBorderBlack);
        AddCellWithSolidBorderBottom(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()), CommonBuilder.SolidBorderBlack);
        AddCellWithSolidBorderBottom(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()), CommonBuilder.SolidBorderBlack);

        AddCell(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add("Subtotaal")));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add(CommonBuilder.GetNumberFormat(CInfo, _amount))));


        foreach (var t in _taxes ?? [])
        {
            var taxTotal = t.Sum(r => r.Quantity * r.Amount * (r.Tax / 100));
            if (taxTotal == 0) continue;
            AddCell(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()));
            AddCell(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()));
            AddCell(CommonBuilder.CreateBorderlessCell().Add(CommonBuilder.CreateParagraph(TextAlignment.RIGHT).Add("BTW")));
            AddCell(CommonBuilder.CreateBorderlessCell().Add(CommonBuilder.CreateParagraph(TextAlignment.RIGHT).Add(CommonBuilder.GetPercentageFormat(CInfo, t.Key))));
            AddCell(CommonBuilder.CreateBorderlessCell().Add(CommonBuilder.CreateParagraph(TextAlignment.RIGHT).Add(CommonBuilder.GetNumberFormat(CInfo, taxTotal))));
            tax += taxTotal;
        }


        AddCell(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()));
        AddCell(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()));
        AddCellWithSolidBorderTop(CommonBuilder.CreateBorderlessCell().Add(CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add("Totaal")), CommonBuilder.SolidBorderBlack);
        AddCellWithSolidBorderTop(CommonBuilder.CreateBorderlessCell().Add(EmptyParagraph()), CommonBuilder.SolidBorderBlack);
        AddCellWithSolidBorderTop(CommonBuilder.CreateBorderlessCell().Add(CommonBuilder.CreateBoldParagraph(TextAlignment.RIGHT).Add(CommonBuilder.GetMoneyFormat(CInfo, _amount + tax))), CommonBuilder.SolidBorderBlack);
        return document.Add(_invoiceTable);
    }
    public static Document CreatePaymentDetails(this Document document, string? reference)
    {
        var table = new Table(UnitValue.CreatePercentArray([30, 5, 30, 5, 30]))
            .UseAllAvailableWidth()
            .SetFixedPosition(1, 70, 150, 490)
            .SetFontSize(10);


        var p1 = new Paragraph().Add("IBAN");
        var p2 = CommonBuilder.CreateBoldParagraph(TextAlignment.CENTER).Add("NL01 ABNA 012 34 56 789");

        var p3 = new Paragraph().Add("FACTUUR");
        var p4 = CommonBuilder.CreateBoldParagraph(TextAlignment.CENTER).Add(reference);

        var p5 = new Paragraph().Add("BEDRAG");
        var p6 = CommonBuilder.CreateBoldParagraph(TextAlignment.CENTER).Add(CommonBuilder.GetMoneyFormat(CInfo, _amount));

        var c1 = new Cell().SetPadding(10).SetTextAlignment(TextAlignment.CENTER).Add(p1).Add(p2);
        var c2 = CommonBuilder.CreateBorderlessCell();
        var c3 = new Cell().SetPadding(10).SetTextAlignment(TextAlignment.CENTER).Add(p3).Add(p4);
        var c4 = CommonBuilder.CreateBorderlessCell();
        var c5 = new Cell().SetPadding(10).SetTextAlignment(TextAlignment.CENTER).Add(p5).Add(p6);

        table.AddCell(c1).AddCell(c2).AddCell(c3).AddCell(c4).AddCell(c5);

        return document.Add(table);
    }
    #region Private Methods
    private static void AddCell(Cell tableCell, int paddingTop = 2, int paddingBottom = 2)
    {
        _invoiceTable?.AddCell(tableCell.SetPaddingTop(paddingTop).SetPaddingBottom(paddingBottom));
    }
    private static void AddCellWithSolidBorderBottom(Cell tableCell, SolidBorder solidBorder, int paddingTop = 2, int paddingBottom = 2)
    {
        _invoiceTable?.AddCell(tableCell.SetPaddingTop(paddingTop).SetPaddingBottom(paddingBottom).SetBorderBottom(solidBorder));
    }
    private static void AddCellWithSolidBorderTop(Cell tableCell, SolidBorder solidBorder, int paddingTop = 2, int paddingBottom = 2)
    {
        _invoiceTable?.AddCell(tableCell.SetPaddingTop(paddingTop).SetPaddingBottom(paddingBottom).SetBorderTop(solidBorder));
    }
    private static Paragraph EmptyParagraph()
    {
        return new Paragraph().Add("");
    }
    #endregion
}