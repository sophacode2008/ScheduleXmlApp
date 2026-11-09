using System.Xml.Xsl;

namespace ScheduleXmlApp.Services
{
    public class HtmlTransformer
    {
        public void TransformToHtml(string xmlPath, string xslPath, string htmlPath)
        {
            if (string.IsNullOrEmpty(xmlPath) || string.IsNullOrEmpty(xslPath)) return;
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xslPath);
            xslt.Transform(xmlPath, htmlPath);
        }
    }
}