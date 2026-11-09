using ScheduleXmlApp.Models;
namespace ScheduleXmlApp.Strategies
{
    public interface IXmlSearchStrategy
    {
        List<ScheduleEntry> Search(string xmlPath, SearchFilter filter);
    }
}