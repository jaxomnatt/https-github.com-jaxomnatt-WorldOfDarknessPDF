using Microsoft.VisualStudio.TestTools.UnitTesting;
using PDFWrapper;

namespace WorldOfDarknessPDF
{
    [TestClass]
    public class Test_VampirePDFBuilder : Test_Harness
    {
        public Test_VampirePDFBuilder() : base()
        {

        }

        [TestMethod]
        public void CreateDocument()
        {
            using(var document = CreateDocument<VampirePDFBuilder>())
            {
                var content = document.Create();
                Preview(content);
            }
        }
    }
}
