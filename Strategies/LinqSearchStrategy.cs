using System.Xml.Linq;
using ScheduleXmlApp.Models;

namespace ScheduleXmlApp.Strategies
{
    public class LinqSearchStrategy : IXmlSearchStrategy
    {
        public List<ScheduleEntry> Search(string xmlPath, SearchFilter filter)
        {
            var doc = XDocument.Load(xmlPath);
            var results = new List<ScheduleEntry>();

            var entries = doc.Descendants("Entry");
            if (!string.IsNullOrEmpty(filter.AttributeName) && !string.IsNullOrEmpty(filter.Keyword))
            {
                entries = entries.Where(e => e.Element(filter.AttributeName)?.Value.Contains(filter.Keyword, StringComparison.OrdinalIgnoreCase) == true);
            }

            foreach (var e in entries)
            {
                results.Add(new ScheduleEntry
                {
                    TeacherName = e.Element("TeacherName")?.Value,
                    Faculty = e.Element("Faculty")?.Value,
                    Department = e.Element("Department")?.Value,
                    Auditorium = e.Element("Auditorium")?.Value,
                    Curriculum = e.Element("Curriculum")?.Value,
                    StudentGroup = e.Element("StudentGroup")?.Value
                });
            }
            return results;
        }
    }
}