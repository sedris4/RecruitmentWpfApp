using RecruitmentWpfApp.Models;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentWpfApp.Interfaces
{
    public interface IPeopleRepository 
    {
        ObservableCollection<PersonData> Data { get; }

        Task<bool> InsertAsync(IAsyncEnumerable<PersonData> dataCollection);
        Task<bool> UpdateAsync(int id, PersonData data);
        Task<PersonData[]> ReadAsync();
    }
}
