using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Annot;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Colorspace;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Renderer;

namespace PDFWrapper
{
    public class VampirePDFBuilder : PDFBase
    {
        private const string BackgroundImage = "PDFWrapper.Resources.VampirePDF_Background.png";
        private const string SmallHeaderImage = "PDFWrapper.Resources.VampirePDF_SmallHeader.jpg";
        private const string SeparatorImage = "PDFWrapper.Resources.VampirePDF_Separator.jpg";
        private const string HeaderFont = "PDFWrapper.Resources.MrsEavesRoman.ttf";
        private const float HeaderFontSize = 14f;
        
        private const float InputLeft = 60f;
        private const float InputLabelWidth = 60f;
        private const float InputWidth = 162f;
        private const float InputFontSize = 10f;
        private const float InputBottom = 3f;
        private const float InputUnderlineWidth = 0.5f;

        // PDFWrapper.Resources.VampirePDF_Background.png
        // PDFWrapper.Resources.VampirePDF_Separator.jpg
        // PDFWrapper.Resources.VampirePDF_SmallHeader.jpg

        public VampirePDFBuilder()
        {

        }

        public override void OnCreate()
        {
            Document.SetMargins(0, 0, 0, 0);
            AddPage(1);

            float bottom = 660f;
            AddBasicInputSection("Name", "Nature", "Clan", bottom);

            bottom -= (InputFontSize * 1.5f);
            bottom -= InputBottom;
            AddBasicInputSection("Player", "Demeanor", "Generation", bottom);

            bottom -= (InputFontSize * 1.5f);
            bottom -= InputBottom;
            AddBasicInputSection("Chronicle", "Concept", "Sire", bottom);

            bottom -= (HeaderFontSize * 1.5f);
            AddSectionHeader("Attributes", 70f, bottom);

            // TODO: Add Physical, Social, Physical Headers here

            bottom -= (HeaderFontSize * 1.5f);
            bottom -= InputBottom;
            AddBasicInputSection("Strength", "Charisma", "Perception", bottom);

            bottom -= (InputFontSize * 1.5f);
            bottom -= InputBottom;
            AddBasicInputSection("Dexterity", "Manipulation", "Intelligence", bottom);

            bottom -= (InputFontSize * 1.5f);
            bottom -= InputBottom;
            AddBasicInputSection("Stamina", "Appearance", "Wits", bottom);

            bottom -= (HeaderFontSize * 1.5f);
            AddSectionHeader("Abilities", 70f, bottom);

            // Add Talents, Skills, Knowledges headers here
        }

        private void AddPage(int pageNumber)
        {
            if (pageNumber > 1)
            {
                var pdf = Document.GetPdfDocument();
                pdf.AddNewPage();
            }
            else
            {
                var pdf = Document.GetPdfDocument();
                pdf.AddFont(this.GetResourceFont(HeaderFont));
            }

            AddPageBackground();
            AddPageHeader();
        }


        private void AddPageBackground()
        {
            var pdf = Document.GetPdfDocument();
            var pageSize = pdf.GetDefaultPageSize();

            var background = this.GetResourceImage(BackgroundImage);
            background.SetFixedPosition(0f, 0f);
            background.ScaleToFit(pageSize.GetWidth(), pageSize.GetHeight());
            Document.Add(background);
        }

        private void AddPageHeader()
        {
            const float left = 203f;
            const float width = 200f;
            const float fontHeight = HeaderFontSize;
            const float bottom = 756f;

            var h2 = this.GetResourceImage(SmallHeaderImage);
            h2.ScaleToFit(width, width * h2.GetImageHeight() / h2.GetImageWidth());
            h2.SetFixedPosition(left, bottom - h2.GetImageScaledHeight());
            Document.Add(h2);

            var h1 = new Paragraph("20th Anniversary Edition");
            h1.SetFixedPosition(left, bottom - (fontHeight / 2), width);
            h1.SetTextAlignment(TextAlignment.CENTER);
            h1.SetFont(this.GetResourceFont(HeaderFont));
            h1.SetFontSize(fontHeight);
            h1.SetBold();
            Document.Add(h1);

            var h3 = new Paragraph("The Masquerade");
            h3.SetFixedPosition(left, bottom - h2.GetImageScaledHeight() - fontHeight, width);
            h3.SetTextAlignment(TextAlignment.CENTER);
            h3.SetFont(this.GetResourceFont(HeaderFont));
            h3.SetFontSize(fontHeight);
            h3.SetBold();
            Document.Add(h3);
        }

        private void AddSectionHeader(string headerText, float headerWidth, float bottom)
        {
            var pdf = Document.GetPdfDocument();
            var pageSize = pdf.GetDefaultPageSize();

            const float width = 489f;
            const float fontHeight = HeaderFontSize;

            var h2 = this.GetResourceImage(SeparatorImage);
            h2.SetFixedPosition(58f, bottom);
            h2.ScaleToFit(width, width * h2.GetImageHeight() / h2.GetImageWidth());
            Document.Add(h2);

            var h1 = new Paragraph(headerText);
            h1.SetTextAlignment(TextAlignment.CENTER);
            h1.SetFont(this.GetResourceFont(HeaderFont));
            h1.SetBackgroundColor(ColorConstants.WHITE);
            h1.SetFontSize(fontHeight);
            h1.SetBold();
            h1.SetFixedPosition((pageSize.GetWidth() - headerWidth) / 2, bottom - (HeaderFontSize / 4f), headerWidth);
            Document.Add(h1);
        }

        private void AddBasicInputSectionHeader(string leftName, string middleName, string rightName, float bottom)
        {

        }

        private void AddBasicInputSection(string leftName, string middleName, string rightName, float bottom)
        {
            AddTextEdit($"txt{leftName}", leftName, InputLabelWidth, InputLeft, InputWidth, bottom);
            AddTextEdit($"txt{middleName}", middleName, InputLabelWidth, InputLeft + InputWidth, InputWidth, bottom);
            AddTextEdit($"txt{rightName}", rightName, InputLabelWidth, InputLeft + 2 * InputWidth, InputWidth, bottom);
        }

        private void AddTextEdit(string inputName, string labelText, float labelWidth, float left, float width, float bottom)
        {
            var pdf = Document.GetPdfDocument();
            var form = PdfAcroForm.GetAcroForm(pdf, true);

            var h1 = new Paragraph($"{labelText}:");
            h1.SetTextAlignment(TextAlignment.LEFT);
            h1.SetFont(this.GetResourceFont(HeaderFont));
            h1.SetFontSize(InputFontSize);
            h1.SetBold();
            h1.SetFixedPosition(left, bottom, labelWidth);
            Document.Add(h1);

            var position = new Rectangle(left + labelWidth, bottom, width - labelWidth, InputFontSize * 1.5f);

            var input = PdfFormField.CreateText(pdf, position, inputName);
            input.SetFont(this.GetResourceFont(HeaderFont));
            input.SetFontSize(InputFontSize);
            input.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 0));
            input.SetMaxLen(50);
            form.AddField(input);

            var canvas = new PdfCanvas(pdf.GetPage(pdf.GetNumberOfPages()));
            canvas.SaveState();
            canvas.SetStrokeColor(ColorConstants.BLACK);
            canvas.SetLineWidth(InputUnderlineWidth);
            canvas.MoveTo(left + labelWidth, bottom - InputUnderlineWidth);
            canvas.LineTo(left + width, bottom - InputUnderlineWidth);
            canvas.ClosePathStroke();
            canvas.RestoreState();
        }
    }
}
