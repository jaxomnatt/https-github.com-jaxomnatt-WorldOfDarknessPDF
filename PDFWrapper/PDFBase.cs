using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PDFWrapper
{
    public class PDFBase : IDisposable
    {
        protected MemoryStream Content { get; set; }
        protected Document Document { get; set; }

        public MemoryStream Create()
        {
            Flush();
            Content = new MemoryStream();

            try
            {
                using (var writer = new PdfWriter(Content))
                using (var pdf = new PdfDocument(writer))
                using (var document = new Document(pdf, PageSize.LETTER))              
                {
                    writer.SetCloseStream(false);
                    Document = document;
                    OnCreate();
                } 
            }
            finally
            {
                Document = null;
                Content.Seek(0, SeekOrigin.Begin);
            }

            return Content;
        }

        public virtual void OnCreate()
        {

        }

        public void Flush()
        {
            if (Content != null)
            {
                try
                {
                    Content.Dispose();
                }
                catch
                {
                    // Do nothing
                }
                finally
                {
                    Content = null;
                }
            }
        }

        public void Dispose()
        {
            Flush();
            if (Content != null)
            {
                Content.Dispose();
                Content = null;
            }
        }
    }
}
