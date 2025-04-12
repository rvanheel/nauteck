using System.Globalization;

using iText.IO.Image;
using iText.Kernel.Font;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

using nauteck.data.Dto.Client;

namespace nauteck.core.Implementation;

internal static class CommonBuilder
{
    private static readonly DottedBorder DottedBorderBlack = new(iText.Kernel.Colors.ColorConstants.BLACK, 0.8f);
    public static readonly iText.Kernel.Colors.DeviceRgb RgbColorBlue = iText.Kernel.Colors.WebColors.GetRGBColor("#009FE3");
    public static readonly SolidBorder SolidBorderBlack = new(iText.Kernel.Colors.ColorConstants.BLACK, 1);

    public static void AddRow(Table table, string? t1, string? t2)
    {
        table.AddCell(CreateBorderlessCell().SetMargin(0).SetPadding(-1).Add(new Paragraph().Add(t1 ?? "")));
        table.AddCell(CreateBorderlessCell().SetMargin(0).SetPadding(-1).Add(new Paragraph().Add(":")));
        table.AddCell(CreateBorderlessCell().SetMargin(0).SetPadding(-1).Add(new Paragraph().Add(t2 ?? "")));
    }
    public static Document CreateAddress(this Document document, ClientDto clientDto)
    {
        // adressering
        var a = new Paragraph()
        .SetFontSize(10)
        .Add($"{clientDto.Preamble} {clientDto.FirstName} {clientDto.Infix} {clientDto.LastName}".Replace("  " , " "))
        .Add(Environment.NewLine)
        .Add(GetCompanyStreet(clientDto))
        .Add(Environment.NewLine)
        .Add(GetCompanyZipAndCity(clientDto))
        .Add(Environment.NewLine)
        .Add(clientDto.Country)
        .SetBorderBottom(DottedBorderBlack)
        .SetPaddingBottom(15)
        ;

        return document.Add(a);
    }
    public static Cell CreateBorderlessCell()
    {
        return new Cell().SetBorder(Border.NO_BORDER);
    }

    public static Paragraph CreateParagraph(TextAlignment textAlignment = TextAlignment.LEFT)
    {
        return new Paragraph().SetTextAlignment(textAlignment);
    }
    public static Paragraph CreateBoldParagraph(TextAlignment textAlignment = TextAlignment.LEFT)
    {
        return CreateParagraph(textAlignment).SetBold();
    }
    public static Document CreateHeader(this Document document)
    {
        // header table
        var header = new Table(3).SetFontSize(9).UseAllAvailableWidth();

        header.AddCell(CreateLogoCell());
        header.AddCell(CreateBorderlessCell().Add(CreateParagraph().Add("Spijksedijk 3-186").Add(Environment.NewLine).Add("6917 AB Spijk (GLD)")));
        header.AddCell(CreateBorderlessCell().Add(CreateParagraph().Add("E: info@nauteckflooring.com").Add(Environment.NewLine).Add("T: +31 (0)6 51 52 24 24")));

        return document.Add(header);
    }

    private static Cell CreateLogoCell()
    {
        var logo = new Image(ImageDataFactory.Create(Resources.Logo)).SetWidth(150);
        var pLogo = CreateParagraph().SetWidth(logo.GetWidth()).Add(logo);

        return CreateBorderlessCell().Add(pLogo)
            .SetBorder(Border.NO_BORDER)
            .SetWidth(logo.GetWidth());
    }
    public static PdfFont GetDefaultFont()
    {
        var collection = PdfFontFactory.GetRegisteredFonts();
        return PdfFontFactory.CreateRegisteredFont(collection.First(t => t == "helvetica"));
    }

    private static string GetCompanyStreet(ClientDto clientDto)
    {
        var extension = "";
        if (int.TryParse(clientDto.Extension, out _)) extension = "-";
        return $"{clientDto.Address} {clientDto.Number}{extension}{clientDto.Extension}".Trim();
    }

    private static string GetCompanyZipAndCity(ClientDto clientDto)
    {
        return $"{clientDto.Zipcode} {clientDto.City}".Trim();
    }
    public static Paragraph LoremIpsum()
    {
        return CreateParagraph().Add(Environment.NewLine).SetFontColor(iText.Kernel.Colors.ColorConstants.WHITE);
    }
    public static string GetPercentageFormat(CultureInfo cInfo, decimal amount)
    {
        return string.Format(cInfo, "{0:n0}%", amount);
    }
    public static string GetMoneyFormat(CultureInfo cInfo, decimal amount)
    {
        return string.Format(cInfo, "{0:c2}", amount);
    }
    public static string GetNumberFormat(CultureInfo cInfo, decimal amount)
    {
        return string.Format(cInfo, "{0:n2}", amount);
    }
    public static void Write(this Document document)
    {
        document.Flush();
        document.Close();
    }
}