using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentWpfApp.Interfaces
{
    public interface IDataFileLoader<T> 
    {
        /// <summary>
        /// Synchrously loads all instances of generic type from source file on provided path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IEnumerable<T> Load(string path);

        /// <summary>
        /// Asynchrously loads all instances of generic type from source file on provided path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IAsyncEnumerable<T> LoadAsync(string path);
    }
}
