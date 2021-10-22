using RecruitmentWpfApp.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentWpfApp.Services
{
    public class PersonDataLoadersBank : IPersonDataLoadersBank
    {
        private readonly Dictionary<string, IPersonDataLoader> _availableFileLoaders = new()
        {
            [".csv"] = new CsvPersonDataLoader()
        };

        public IPersonDataLoader Get(string fileExtension)
        {
            if (string.IsNullOrWhiteSpace(fileExtension))
            {
                throw new ArgumentNullException("Extension value not provided.");
            }

            if (!_availableFileLoaders.ContainsKey(fileExtension))
            {
                throw new NotSupportedException($"Loading data from file with extension '{fileExtension}' is not supported.");
            }

            return _availableFileLoaders[fileExtension];
        }
    }
}
