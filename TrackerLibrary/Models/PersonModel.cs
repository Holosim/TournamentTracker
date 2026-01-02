using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// Represents a person with contact information such as first name, last name, email address, and cell phone
    /// number.
    /// </summary>
    public class PersonModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the prize.
        /// </summary>
        public int PersonID { get; set; }
        /// <summary>
        /// Gets or sets the first name associated with the entity.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of the person
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// The primary email address.
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// The primary cell phone number of the person.
        /// </summary>
        public string CellPhoneNumber { get; set; }
        /// <summary>
        /// Test datapoint for the person's number of direct descendants.
        /// </summary>
        public string NumberOfKids { get; set; }
        /// <summary>
        /// Creation datefor the person in the database. (Not the creation date of the person, AKA birthdate)
        /// </summary>
        public string CreateDate { get; set; }

       public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }


        public PersonModel()
        {
            FirstName = "";
            LastName = "";
            EmailAddress = "";
            CellPhoneNumber = "";
            NumberOfKids = "0";
            CreateDate = DateTime.Now.ToString();
        }
        public PersonModel( string first, string last, string email, string cell)
        {
            FirstName = first;
            LastName = last;
            EmailAddress = email;
            CellPhoneNumber = cell;
            NumberOfKids = "0";
            CreateDate = DateTime.Now.ToString();
        }
    }
}
