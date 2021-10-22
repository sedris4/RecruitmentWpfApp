using RecruitmentWpfApp.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RecruitmentWpfApp.Views
{
    /// <summary>
    /// Interaction logic for DataEditionWindow.xaml
    /// </summary>
    public partial class DataEditionWindow : Window
    {
        private IRequestsClose _requester;

        public DataEditionWindow()
        {
            InitializeComponent();

            DataContextChanged += DataEditionWindow_DataContextChanged;
        }

        private void DataEditionWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(e.NewValue is IRequestsClose requester)
            {
                _requester = requester;
                _requester.CloseRequest += Requester_CloseRequest;
            }
        }

        private void Requester_CloseRequest(object sender, EventArgs e)
        {
            _requester.CloseRequest -= Requester_CloseRequest;

            Dispatcher.BeginInvoke(Close);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}
