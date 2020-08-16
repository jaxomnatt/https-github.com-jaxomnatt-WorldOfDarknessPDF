using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace WorldOfDarknessPDF
{
    public class Test_Harness
    {
        public Test_Harness()
        {
        }

        public T CreateDocument<T>() where T : class => Activator.CreateInstance<T>();
        public void Preview(Stream document)
        {
            if (!Directory.Exists("C:\\PDFs"))
            {
                Directory.CreateDirectory("C:\\PDFs");
            }

            if (document.CanSeek)
            {
                document.Seek(0, SeekOrigin.Begin);
            }

            var filePath = Path.Combine("C:\\PDFs", string.Format("{0}.pdf", Guid.NewGuid().ToString()));

            using(var fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write))
            {
                document.CopyTo(fs);
                fs.Flush();
            }

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo(filePath)
                {
                    UseShellExecute = true,
                    LoadUserProfile = true
                }
            };

            process.Start();
            process.Dispose();
        }
    }
}
