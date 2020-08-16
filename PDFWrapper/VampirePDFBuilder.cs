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
        private const float SmallHeaderWidth = 180f;
        private const float SmallHeaderBottomOffset = 83f;

        private const string SeparatorImage = "PDFWrapper.Resources.VampirePDF_Separator.jpg";

        private const string HeaderFont = "PDFWrapper.Resources.Goudy-Bold.ttf";
        private const string InputFont = "PDFWrapper.Resources.MrsEavesRoman.ttf";

        private const float InputLeft = 60f;
        private const float InputLabelWidth = 90f;
        private const float InputWidth = 162f;
        private const float InputFontSize = 10f;
        private const float InputBottom = 1f;
        private const float InputUnderlineWidth = 0.5f;
        private const float CharacterStoryLabelWidth = 60f;
        private const float BackgroundLabelWidth = 120f;

        private const float InputSectionHeaderFontSize = InputFontSize;
        private const float HeaderFontSize = InputFontSize + 2f;


        private long InputCounter { get; set; } = 0;
        private bool UnderlineInputLabels { get; set; } = false;

        public VampirePDFBuilder()
        {

        }

        public override void OnCreate()
        {
            Document.SetMargins(0, 0, 0, 0);
            var bottom = AddPage(1);

            var headerSpacing = (HeaderFontSize * 1.5f);
            var inputSpacing = (InputFontSize * 1.25f) + InputBottom;
            var backgroundLabelWidth = BackgroundLabelWidth;

            bottom -= headerSpacing;
            AddBasicInputSection("Name", "Nature", "Clan", bottom, CharacterStoryLabelWidth);

            bottom -= inputSpacing;
            AddBasicInputSection("Player", "Demeanor", "Generation", bottom, CharacterStoryLabelWidth);

            bottom -= inputSpacing;
            AddBasicInputSection("Chronicle", "Concept", "Sire", bottom, CharacterStoryLabelWidth);

            bottom -= headerSpacing;
            AddSectionHeader("Attributes", 70f, bottom);

            bottom -= inputSpacing;
            AddBasicInputSectionHeader("Physical", "Social", "Mental", bottom);

            bottom -= inputSpacing;
            AddBasicInputSection("Strength", "Charisma", "Perception", bottom, CharacterStoryLabelWidth);

            bottom -= inputSpacing;
            AddBasicInputSection("Dexterity", "Manipulation", "Intelligence", bottom, CharacterStoryLabelWidth);

            bottom -= inputSpacing;
            AddBasicInputSection("Stamina", "Appearance", "Wits", bottom, CharacterStoryLabelWidth);

            UnderlineInputLabels = true;

            bottom -= headerSpacing;
            AddSectionHeader("Abilities", 70f, bottom);

            bottom -= inputSpacing;
            AddBasicInputSectionHeader("Talents", "Skills", "Knowledges", bottom);

            bottom -= inputSpacing;
            AddBasicInputSection("Alertness", "Animal Ken", "Academics", bottom);

            bottom -= inputSpacing;
            AddBasicInputSection("Athletics", "Crafts", "Computer", bottom);

            bottom -= inputSpacing;
            AddBasicInputSection("Awareness", "Drive", "Finance", bottom);

            bottom -= inputSpacing;
            AddBasicInputSection("Brawl", "Etiquette", "Investigation", bottom);

            bottom -= inputSpacing;
            AddBasicInputSection("Empathy", "Firearms", "Law", bottom);

            bottom -= inputSpacing;
            AddBasicInputSection("Expression", "Larceny", "Medicine", bottom);

            bottom -= inputSpacing;
            AddBasicInputSection("Intimidation", "Melee", "Occult", bottom);

            bottom -= inputSpacing;
            AddBasicInputSection("Leadership", "Performance", "Politics", bottom);

            bottom -= inputSpacing;
            AddBasicInputSection("Streetwise", "Stealth", "Science", bottom);

            bottom -= inputSpacing;
            AddBasicInputSection("Subterfuge", "Survival", "Technology", bottom);

            bottom -= inputSpacing;
            AddBasicInputSection(string.Empty, string.Empty, string.Empty, bottom);

            bottom -= inputSpacing;
            AddBasicInputSection(string.Empty, string.Empty, string.Empty, bottom);

            bottom -= headerSpacing;
            AddSectionHeader("Advantages", 70f, bottom);

            bottom -= inputSpacing;
            AddBasicInputSectionHeader("Disciplines", "Backgrounds", "Virtues", bottom);

            bottom -= inputSpacing;
            AddBasicInputSection(string.Empty, string.Empty, "Conscious/Conviction", bottom, backgroundLabelWidth);

            bottom -= inputSpacing;
            AddBasicInputSection(string.Empty, string.Empty, "Self Control/Instinct", bottom, backgroundLabelWidth);

            bottom -= inputSpacing;
            AddBasicInputSection(string.Empty, string.Empty, "Courage", bottom, backgroundLabelWidth);

            bottom -= inputSpacing;
            AddBasicInputSection(string.Empty, string.Empty, string.Empty, bottom, backgroundLabelWidth);

            bottom -= inputSpacing;
            AddBasicInputSection(string.Empty, string.Empty, string.Empty, bottom, backgroundLabelWidth);

            bottom -= inputSpacing;
            AddBasicInputSection(string.Empty, string.Empty, string.Empty, bottom, backgroundLabelWidth);

            bottom -= inputSpacing;
            AddBasicInputSection(string.Empty, string.Empty, string.Empty, bottom, backgroundLabelWidth);

            bottom -= headerSpacing;
            AddSectionHeader(string.Empty, 70f, bottom);

            bottom -= inputSpacing;
            AddBasicInputSectionHeader("Notes", "Status", "Lifetime", bottom);

            bottom -= inputSpacing;
            AddBasicInputSection(string.Empty, "Health", "Maximum Health", bottom, backgroundLabelWidth, true);

            bottom -= inputSpacing;
            AddBasicInputSection(string.Empty, "Blood Pool (        /turn)", "Maximum Blood Pool", bottom, backgroundLabelWidth, true);

            bottom -= inputSpacing;
            AddBasicInputSection(string.Empty, "Willpower", "Maximum Willpower", bottom, backgroundLabelWidth, true);

            bottom -= inputSpacing;
            AddBasicInputSection(string.Empty, "Humanity/Path Bearing", "Humanity/Path", bottom, backgroundLabelWidth, true);

            bottom -= inputSpacing;
            AddBasicInputSection(string.Empty, "Experience", "Spent Experience", bottom, backgroundLabelWidth, true);

            bottom -= inputSpacing;
            AddBasicInputSection(string.Empty, string.Empty, string.Empty, bottom, backgroundLabelWidth, true);

            bottom -= inputSpacing;
            AddBasicInputSection(string.Empty, string.Empty, string.Empty, bottom, backgroundLabelWidth, true);

            bottom -= inputSpacing;
            AddBasicInputSection(string.Empty, string.Empty, string.Empty, bottom, backgroundLabelWidth, true);

            bottom -= inputSpacing;
            AddBasicInputSection(string.Empty, string.Empty, string.Empty, bottom, backgroundLabelWidth, true);

            bottom -= inputSpacing;
            AddBasicInputSection(string.Empty, string.Empty, string.Empty, bottom, backgroundLabelWidth, true);

            bottom -= (inputSpacing * 1.5f);
            AddInfoLabel("Attributes: 7/5/3 • Abilities:13/9/5 • Disciplines:3 • Backgrounds:5 • Virtues:7 • Freebie Points:15 (7/5/2/1)", InputLeft, 3f * InputWidth, bottom);
        }

        private float AddPage(int pageNumber)
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
            return AddPageHeader();
        }


        private void AddPageBackground()
        {
            var pdf = Document.GetPdfDocument();
            var pageSize = pdf.GetDefaultPageSize();

            var background = this.GetResourceImage(BackgroundImage);
            background.ScaleToFit(pageSize.GetWidth(), pageSize.GetHeight());
            background.SetFixedPosition(0f, pageSize.GetHeight() - background.GetImageScaledHeight());
            Document.Add(background);
        }

        private float AddPageHeader()
        {
            var pdf = Document.GetPdfDocument();
            var pageSize = pdf.GetDefaultPageSize();

            const float width = SmallHeaderWidth;
            var left = (pageSize.GetWidth() - width) / 2f;
          
            const float fontHeight = HeaderFontSize;

            var bottom = pageSize.GetHeight() - SmallHeaderBottomOffset;
            
            var h2 = this.GetResourceImage(SmallHeaderImage);
            h2.ScaleToFit(width, width * h2.GetImageHeight() / h2.GetImageWidth());
            h2.SetFixedPosition(left, bottom);
            Document.Add(h2);

            bottom += h2.GetImageScaledHeight();

            var h1 = new Paragraph("20th Anniversary Edition");
            h1.SetFixedPosition(left, bottom - 0.5f * fontHeight, width);
            h1.SetTextAlignment(TextAlignment.CENTER);
            h1.SetFont(this.GetResourceFont(InputFont));
            h1.SetFontSize(fontHeight);
            h1.SetBold();
            Document.Add(h1);

            bottom -= h2.GetImageScaledHeight();
            bottom -= fontHeight;

            var h3 = new Paragraph("The Masquerade");
            h3.SetFixedPosition(left, bottom, width);
            h3.SetTextAlignment(TextAlignment.CENTER);
            h3.SetFont(this.GetResourceFont(InputFont));
            h3.SetFontSize(fontHeight);
            h3.SetBold();
            Document.Add(h3);

            return bottom;
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

            if (!string.IsNullOrWhiteSpace(headerText))
            {
                var h1 = new Paragraph(headerText);
                h1.SetTextAlignment(TextAlignment.CENTER);
                h1.SetFont(this.GetResourceFont(HeaderFont));
                h1.SetBackgroundColor(ColorConstants.WHITE);
                h1.SetFontSize(fontHeight);
                h1.SetBold();
                h1.SetFixedPosition((pageSize.GetWidth() - headerWidth) / 2, bottom - (HeaderFontSize / 4f), headerWidth);
                Document.Add(h1);
            }
        }

        private void AddBasicInputSectionHeader(string leftName, string middleName, string rightName, float bottom)
        {
            var left = InputLeft;

            var h1 = new Paragraph(leftName);
            h1.SetTextAlignment(TextAlignment.CENTER);
            h1.SetFont(this.GetResourceFont(InputFont));
            h1.SetFontSize(InputSectionHeaderFontSize);
            h1.SetBold();
            h1.SetFixedPosition(left, bottom, InputWidth);
            Document.Add(h1);

            var h2 = new Paragraph(middleName);
            h2.SetTextAlignment(TextAlignment.CENTER);
            h2.SetFont(this.GetResourceFont(InputFont));
            h2.SetFontSize(InputSectionHeaderFontSize);
            h2.SetBold();
            h2.SetFixedPosition(left + InputWidth, bottom, InputWidth);
            Document.Add(h2);

            var h3 = new Paragraph(rightName);
            h3.SetTextAlignment(TextAlignment.CENTER);
            h3.SetFont(this.GetResourceFont(InputFont));
            h3.SetFontSize(InputSectionHeaderFontSize);
            h3.SetBold();
            h3.SetFixedPosition(left + 2 * InputWidth, bottom, InputWidth);
            Document.Add(h3);
        }

        private void AddBasicInputSection(string leftName, string middleName, string rightName, float bottom, float inputLabelWidth = InputLabelWidth, bool evenlyDivideInputs = false)
        {
            if (evenlyDivideInputs)
            {
                AddTextEdit(leftName, InputWidth, InputLeft, InputWidth, bottom);
            }
            else
            {
                AddTextEdit(leftName, inputLabelWidth, InputLeft, InputWidth, bottom);
            }

            if (evenlyDivideInputs && string.IsNullOrWhiteSpace(middleName))
            {
                AddTextEdit(middleName, InputWidth, InputLeft + InputWidth, InputWidth, bottom);
            }
            else
            {
                AddTextEdit(middleName, inputLabelWidth, InputLeft + InputWidth, InputWidth, bottom);
            }

            if (evenlyDivideInputs && string.IsNullOrWhiteSpace(rightName))
            {
                AddTextEdit(rightName, InputWidth, InputLeft + 2 * InputWidth, InputWidth, bottom);
            }
            else
            {
                AddTextEdit(rightName, inputLabelWidth, InputLeft + 2 * InputWidth, InputWidth, bottom);
            }    
        }

        private void AddTextEdit(string labelText, float labelWidth, float left, float width, float bottom)
        {
            var pdf = Document.GetPdfDocument();
            var form = PdfAcroForm.GetAcroForm(pdf, true);

            var inputName = $"txt{InputCounter++}";
            var position = new Rectangle(left, bottom, labelWidth, InputFontSize * 1.25f);

            var h1Text = labelText;
            if (!string.IsNullOrWhiteSpace(h1Text) && !UnderlineInputLabels)
            {
                h1Text += ':';
            }

            var h1 = PdfFormField.CreateText(pdf, position, inputName, h1Text);
            h1.SetFont(this.GetResourceFont(InputFont));
            h1.SetFontSize(InputFontSize);
            h1.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 0));
            h1.SetMaxLen(50);
            form.AddField(h1);

            var underlineSpacing = InputFontSize;

            if (string.IsNullOrWhiteSpace(h1Text) || UnderlineInputLabels)
            {
                var canvas = new PdfCanvas(pdf.GetPage(pdf.GetNumberOfPages()));
                canvas.SaveState();
                canvas.SetStrokeColor(ColorConstants.BLACK);
                canvas.SetLineWidth(InputUnderlineWidth);
                canvas.MoveTo(left, bottom - InputUnderlineWidth);
                canvas.LineTo(left + labelWidth - underlineSpacing, bottom - InputUnderlineWidth);
                canvas.ClosePathStroke();
                canvas.RestoreState();
            }

            if (labelWidth != width)
            {
                inputName = $"txt{InputCounter++}";
                position = new Rectangle(left + labelWidth, bottom, width - labelWidth, InputFontSize * 1.25f);

                var input = PdfFormField.CreateText(pdf, position, inputName);
                input.SetFont(this.GetResourceFont(InputFont));
                input.SetFontSize(InputFontSize);
                input.SetBackgroundColor(new DeviceCmyk(0, 0, 0, 0));
                input.SetMaxLen(50);
                form.AddField(input);

                var canvas = new PdfCanvas(pdf.GetPage(pdf.GetNumberOfPages()));
                canvas.SaveState();
                canvas.SetStrokeColor(ColorConstants.BLACK);
                canvas.SetLineWidth(InputUnderlineWidth);
                canvas.MoveTo(left + labelWidth, bottom - InputUnderlineWidth);
                canvas.LineTo(left + width - underlineSpacing, bottom - InputUnderlineWidth);
                canvas.ClosePathStroke();
                canvas.RestoreState();
            }
        }

        private void AddInfoLabel(string labelText, float left, float width, float bottom)
        {
            var h1 = new Paragraph(labelText);
            h1.SetTextAlignment(TextAlignment.CENTER);
            h1.SetFont(this.GetResourceFont(InputFont));
            h1.SetFontSize(InputSectionHeaderFontSize);
            h1.SetBold();
            h1.SetFixedPosition(left, bottom, width);
            Document.Add(h1);
        }
    }
}
