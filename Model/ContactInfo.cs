using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ContactApp.Model
{
    internal class ContactInfo
    {
        private static int id = 1; 

        public int Id { get; set; }
        public string TypeOfContact { get; set; }
        public object ValueOfContact { get; set; }

        public ContactInfo(string typeOfContact, object valueOfContact)
        {
            Id = id++;    
            TypeOfContact = typeOfContact;
            ValueOfContact = valueOfContact;
        }
        public static ContactInfo CreateContactInfo(string typeOfContact, object valueOfContact)
        {
            if (string.IsNullOrWhiteSpace(typeOfContact) || !(valueOfContact is string || valueOfContact is int))
            {
                throw new Exception("Invalid input");
            }

            return new ContactInfo(typeOfContact, valueOfContact);
        }
        public override string ToString()
        {
            return $"ContactInfo Id: {Id}\n" +
                $"Type of Contact: {TypeOfContact}\n" +
                $"Value Of Contact:{ValueOfContact}\n" +
                $"============================";
        }
        public void UpdateTypeofContactInfo(string parameter, string newValue)
        {
            if (string.IsNullOrWhiteSpace(parameter) || string.IsNullOrWhiteSpace(newValue))
            {
                throw new ArgumentException("Invalid parameter");
            }

            TypeOfContact = newValue;
        }

        public void UpdateValueofContactInfo(string parameter, object newValue)
        {
            if (string.IsNullOrWhiteSpace(parameter))
            {
                throw new ArgumentException("Invalid parameter");
            }

            ValueOfContact = newValue;
        }

    }
}
