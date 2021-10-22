using Dawn;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using RecruitmentWpfApp.Interfaces;
using RecruitmentWpfApp.Models;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RecruitmentWpfApp.Services
{
    public class PeopleRepository : IPeopleRepository
    {
        public ObservableCollection<PersonData> Data { get; }

        private readonly IDatabaseContext _context;
        private readonly IApplicationLogger _logger;

        private readonly object _lock = new();

        public PeopleRepository(IDatabaseContext context, IApplicationLogger logger)
        {
            _context = Guard.Argument(context, nameof(context)).NotNull().Value;
            _logger = Guard.Argument(logger, nameof(logger)).NotNull().Value;

            Data = _context.People.Local.ToObservableCollection();
            BindingOperations.EnableCollectionSynchronization(Data, _lock);
        }

        public async Task<bool> InsertAsync(IAsyncEnumerable<PersonData> dataCollection)
        {
            _ = Guard.Argument(dataCollection, nameof(dataCollection)).NotNull();

            try
            {
                PersonData[] data = await dataCollection.ToArrayAsync();

                await _context.People.AddRangeAsync(data);

                _ = await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("Could not insert data into database's table. ", ex);

                return false;
            }
        }

        public async Task<bool> UpdateAsync(int id, PersonData newDataContainer)
        {
            _ = Guard.Argument(id, nameof(id)).NotNegative();
            _ = Guard.Argument(newDataContainer, nameof(newDataContainer)).NotNull();

            try
            {
                PersonData originalEntity = _context.People.Single(x => x.Id == id);

                newDataContainer.CopyTo(originalEntity);

                _ = await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("Could not update data in database's table. ", ex);

                return false;
            }
        }

        public async Task<PersonData[]> ReadAsync()
        {
            try
            {
                return await EntityFrameworkQueryableExtensions.ToArrayAsync(_context.People);
            }
            catch (Exception ex)
            {
                _logger.Error("Could not read data from database's table. ", ex);

                return null;
            }
        }
    }
}
