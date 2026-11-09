using ScheduleXmlApp.Models;
using ScheduleXmlApp.Strategies;

namespace ScheduleXmlApp.Services
{
    public class ScheduleManager
    {
        private IXmlSearchStrategy _strategy;
        private string _xmlPath;

        public void SetStrategy(IXmlSearchStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetXmlPath(string path)
        {
            _xmlPath = path;
        }

        public List<ScheduleEntry> ExecuteSearch(SearchFilter filter)
        {
            if (_strategy == null || string.IsNullOrEmpty(_xmlPath))
                return new List<ScheduleEntry>();

            return _strategy.Search(_xmlPath, filter);
        }
    }
}