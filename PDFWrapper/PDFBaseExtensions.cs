using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace PDFWrapper
{
    public static class PDFBaseExtensions
    {
        public static byte[] GetManifestData(this PDFBase document, string resourceName)
        {
            using (var ms = new MemoryStream())
            {
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    resource.CopyTo(ms);
                    ms.Flush();
                }

                ms.Seek(0, SeekOrigin.Begin);
                return ms.ToArray();

            }
        }

        public static Image GetResourceImage(this PDFBase document, string resourceName)
        {
            var data = document.GetManifestData(resourceName);
            var img = ImageDataFactory.Create(data);
            return new Image(img);
        }

        public static PdfFont GetResourceFont(this PDFBase document, string resourceName)
        {
            var data = document.GetManifestData(resourceName); 
            var fontProgram = FontProgramFactory.CreateFont(data);
            return PdfFontFactory.CreateFont(fontProgram);
        }
    }
}
