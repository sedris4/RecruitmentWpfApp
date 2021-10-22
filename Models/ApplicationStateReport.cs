using RecruitmentWpfApp.Enums;

namespace RecruitmentWpfApp.Models
{
    public class ApplicationStateReport
    {
        public static ApplicationStateReport Idle => new ApplicationStateReport(ApplicationState.Idle, string.Empty);

        /// <summary>
        /// Current state
        /// </summary>
        public ApplicationState State { get; }
        /// <summary>
        /// Message to user to let him know about application current state
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Initializes new instance of <see cref="ApplicationStateReport"/>
        /// </summary>
        /// <param name="state"></param>
        /// <param name="message"></param>
        public ApplicationStateReport(ApplicationState state, string message)
        {
            State = state;
            Message = message;
        }
    }
}
