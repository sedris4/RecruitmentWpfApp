using Autofac.Core;

namespace RecruitmentWpfApp.Interfaces
{
    /// <summary>
    /// IoC container implementation
    /// </summary>
    public interface IServiceLocator
    {
        /// <summary>
        /// Gets instance of a generic type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Resolve<T>();
        /// <summary>
        /// Gets instance of a generic type with parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        T Resolve<T>(params Parameter[] parameters);
    }
}