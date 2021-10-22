using Dawn;

using Microsoft.Win32;

using RecruitmentWpfApp.Interfaces;

using System;
using System.IO;
using System.Text.RegularExpressions;

namespace RecruitmentWpfApp.Services
{
    public class FileSelectionService : IFilepathSelector
    {
        public bool GetFilepath(out string path, string extension = "", string initialDirectory = "")
        {
            if (!string.IsNullOrWhiteSpace(extension) && !IsExtension(extension))
            {
                throw new ArgumentNullException("Extension value invalid.");
            }

            if (!string.IsNullOrWhiteSpace(initialDirectory) && IsExistingDirectory(initialDirectory))
            {
                throw new ArgumentNullException("Initial directory value invalid.");
            }

            OpenFileDialog dialog = new OpenFileDialog()
            {
                DefaultExt = extension,
                Filter = $"Plik *.{extension} | *.{extension}",
                AddExtension = true,
                InitialDirectory = initialDirectory,
            };

            return ShowDialogAndGetResult(dialog, out path);
        }

        private bool ShowDialogAndGetResult(FileDialog dialog, out string path)
        {
            bool success = dialog.ShowDialog() is true;

            path = success ? dialog.FileName : default;

            return success;
        }

        private bool IsExistingDirectory(string baseDirectory)
        {
            return Directory.Exists(baseDirectory);
        }

        private bool IsExtension(string extension)
        {
            return Regex.IsMatch(extension, "[a-zA-Z0-9]");
        }
    }
}
