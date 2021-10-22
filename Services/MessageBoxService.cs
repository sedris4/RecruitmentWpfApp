using RecruitmentWpfApp.Interfaces;

using System;
using System.Windows;

namespace RecruitmentWpfApp.Services
{
    public class MessageBoxService : IMessageBoxService
    {
        public void ReportError(string title, string message)
        {
            _ = MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ReportSuccess(string title, string message)
        {
            _ = MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
