using CsvHelper;

using Dawn;

using RecruitmentWpfApp.Models;

using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace RecruitmentWpfApp.Services
{
    public class CsvPersonDataLoader : IPersonDataLoader
    {
        /// <summary>
        /// Container required for enumeration operation in CsvHelper lib
        /// </summary>
        private static readonly PersonData _container = new PersonData();

        public IEnumerable<PersonData> Load(string path)
        {
            _ = Guard.Argument(path, nameof(path)).NotNull().NotEmpty().NotWhiteSpace();

            using StreamReader streamReader = new(path);
            using CsvReader csvReader = new(streamReader, CultureInfo.InvariantCulture);

            foreach(PersonData loadedRecord in csvReader.EnumerateRecords(_container))
            {
                // Loaded record is same reference as _container, after reading new PersonData object has to instantiated
                yield return loadedRecord.Clone(); 
            }
        }

        public async IAsyncEnumerable<PersonData> LoadAsync(string path)
        {
            _ = Guard.Argument(path, nameof(path)).NotNull().NotEmpty().NotWhiteSpace();

            using StreamReader streamReader = new(path);
            using CsvReader csvReader = new(streamReader, CultureInfo.InvariantCulture);

            await foreach (PersonData loadedRecord in csvReader.EnumerateRecordsAsync(_container))
            {
                // Loaded record is same reference as _container, after reading new PersonData object has to instantiated
                yield return loadedRecord.Clone();
            }
        }
    }
}