using CsvHelper.Configuration.Attributes;

using RecruitmentWpfApp.Models.Base;

namespace RecruitmentWpfApp.Models
{
    public class PersonData : ObservableModel
    {
        //================================================================
        private int id;
        /// <summary>
        /// Identificator of a record
        /// </summary>
        [Name("id")]
        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }
        //================================================================

        //================================================================
        private string name;
        /// <summary>
        /// Person first name 
        /// </summary>
        [Name("name")]
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        //================================================================

        //================================================================
        private string lastName;
        /// <summary>
        /// Person last name 
        /// </summary>
        [Name("surename")]
        public string LastName
        {
            get { return lastName; }
            set { SetProperty(ref lastName, value); }
        }
        //================================================================

        //================================================================
        private string email;
        /// <summary>
        /// Contact email
        /// </summary>
        [Name("email")]
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }
        //================================================================

        //================================================================
        private string telephoneNumber;
        /// <summary>
        /// Contact telephone number
        /// </summary>
        [Name("phone")]
        public string TelephoneNumber
        {
            get { return telephoneNumber; }
            set { SetProperty(ref telephoneNumber, value); }
        }
        //================================================================

        public PersonData Clone()
        {
            return new PersonData()
            {
                Id = id,
                Name = name,
                LastName = lastName,
                Email = email,
                TelephoneNumber = telephoneNumber
            };
        }

        public void CopyTo(PersonData target)
        {
            target.Id = id;
            target.Name = name;
            target.LastName = lastName;
            target.Email = email;
            target.TelephoneNumber = telephoneNumber;
        }
    }
}
