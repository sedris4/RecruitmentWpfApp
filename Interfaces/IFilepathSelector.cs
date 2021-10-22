using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentWpfApp.Interfaces
{
    /// <summary>
    /// Interface for service used for allowing user to select specific file
    /// </summary>
    public interface IFilepathSelector
    {
        /// <summary>
        /// Gets filepath 
        /// </summary>
        /// <param name="path">Selected filepath</param>
        /// <param name="extension"></param>
        /// <param name="initialDirectory"></param>
        /// <returns>True if successfully got valid filepath otherwise false</returns>
        bool GetFilepath(out string path, string extension = "", string initialDirectory = "");
    }
}
