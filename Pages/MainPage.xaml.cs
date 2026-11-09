using ScheduleXmlApp.Models;
using ScheduleXmlApp.Services;
using ScheduleXmlApp.Strategies;

namespace ScheduleXmlApp.Pages
{
    public partial class MainPage : ContentPage
    {
        private ScheduleManager _scheduleManager = new ScheduleManager();
        private string _currentXmlPath;
        private string _currentXslPath;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnLoadXml_Click(object sender, EventArgs e)
        {
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Оберіть XML файл"
            });

            if (result != null)
            {
                _currentXmlPath = result.FullPath;
                _scheduleManager.SetXmlPath(_currentXmlPath);
                lblFilePath.Text = $"XML: {result.FileName}";
                PerformSearch(); // Одразу показуємо весь файл
            }
        }

        private async void btnLoadXsl_Click(object sender, EventArgs e)
        {
            var result = await FilePicker.Default.PickAsync();
            if (result != null)
            {
                _currentXslPath = result.FullPath;
                lblFilePath.Text += $" | XSL: {result.FileName}";
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void PerformSearch()
        {
            if (string.IsNullOrEmpty(_currentXmlPath)) return;

            string selectedStrategy = strategyPicker.SelectedItem?.ToString();

            // Встановлюємо стратегію залежно від вибору
            if (selectedStrategy == "LINQ to XML")
                _scheduleManager.SetStrategy(new LinqSearchStrategy());
            // else if (selectedStrategy == "DOM") _scheduleManager.SetStrategy(new DomSearchStrategy());
            else
                _scheduleManager.SetStrategy(new LinqSearchStrategy()); // Заглушка

            var filter = new SearchFilter
            {
                AttributeName = attributePicker.SelectedItem?.ToString(),
                Keyword = searchEntry.Text
            };

            var results = _scheduleManager.ExecuteSearch(filter);
            scheduleCollectionView.ItemsSource = results;
        }

        private async void btnTransform_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentXmlPath) || string.IsNullOrEmpty(_currentXslPath))
            {
                await DisplayAlert("Помилка", "Завантажте XML та XSL файли", "ОК");
                return;
            }

            try
            {
                var transformer = new HtmlTransformer();
                string outPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ScheduleOutput.html");
                transformer.TransformToHtml(_currentXmlPath, _currentXslPath, outPath);
                await DisplayAlert("Успіх", $"Файл збережено:\n{outPath}", "ОК");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Помилка", ex.Message, "ОК");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            searchEntry.Text = string.Empty;
            scheduleCollectionView.ItemsSource = null;
        }

        private async void btnExit_Click(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Вихід", "Ви дійсно хочете вийти?", "Так", "Ні");
            if (answer)
            {
                Application.Current.Quit();
            }
        }
    }
}