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
    }
}
