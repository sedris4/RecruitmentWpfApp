using RecruitmentWpfApp.Interfaces;
using RecruitmentWpfApp.Models;
using RecruitmentWpfApp.Services;
using RecruitmentWpfApp.ViewModels;
using RecruitmentWpfApp.Views;

using System;
using System.Windows;

namespace RecruitmentWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string TargetFileExtension = "csv";

        private IMainViewModel _vm;

        private readonly IFilepathSelector _filepathSelector;
        private readonly IServiceLocator _locator;

        public MainWindow(IFilepathSelector filepathSelector, IServiceLocator locator)
        {
            InitializeComponent();

            _filepathSelector = filepathSelector;
            _locator = locator;

            DataContextChanged += MainWindow_DataContextChanged;
        }

        private void MainWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _vm = e.NewValue as IMainViewModel ?? throw new ArgumentException("Invalid view model type.");
        }

        private void LoadFileButton_Click(object sender, RoutedEventArgs e)
        {
            if(_filepathSelector.GetFilepath(out string path, extension: TargetFileExtension))
            {
                _vm.LoadDataFromFileCommand.ExecuteAsync(path);
            }
        }

        private void RecordsDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(e.OriginalSource is FrameworkElement element && element.DataContext is PersonData data)
            {
                DataEditionWindow dataEditorView = _locator.Resolve<DataEditionWindow>();
                DataEditorViewModel dataEditorViewModel = _locator.Resolve<DataEditorViewModel>();

                dataEditorViewModel.OriginalData = data;

                dataEditorView.DataContext = dataEditorViewModel;
                dataEditorView.ShowDialog();
            }
        }
    }
}
