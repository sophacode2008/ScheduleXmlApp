using CommunityToolkit.Mvvm.Input;
using ScheduleXmlApp.Models;

namespace ScheduleXmlApp.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}