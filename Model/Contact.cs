using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Model
{
    internal class Contact
    {
        private static int id = 1;

        public int Id { get; set; }
        public string Name { get; set; }
        public List<ContactInfo> ContactInfo { get; set; }

        public Contact(string name)
        {
            Id = id++;
            Name = name;
            ContactInfo = new List<ContactInfo>();
        }
        public static Contact CreateNewContact(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Invalid Name");
            }

            return new Contact(name);
        }
        public override string ToString()
        {
            return $"Contact Id: {Id}\n" +
                $"Contact Name: {Name}\n" +
                $"============================";
        }
        public void UpdateName(string newValue)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(newValue))
                {
                    throw new ArgumentException("Invalid Parameter");
                }

                Name = newValue;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateContact(string parameter, string newValue)
        {
            try
            {
                switch (parameter)
                {
                    case "name":
                        UpdateName(newValue);
                        break;
                    default:
                        throw new ArgumentException("Invalid Parameter");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public ContactInfo CreateContactInfo(string typeOfContact, object valueOfContact)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(typeOfContact) || valueOfContact == null)
                {
                    throw new ArgumentException("Invalid input contact class");
                }

                ContactInfo contactInfo = new ContactInfo(typeOfContact, valueOfContact);
                ContactInfo.Add(contactInfo);
                return contactInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<ContactInfo> GetAllContactInfo()
        {
            return ContactInfo;
        }
        public (ContactInfo, int) FindContactInfo(int contactInfoId)
        {
            if (contactInfoId < 0)
            {
                throw new Exception("Invalid Id");
            }

            for (int index = 0; index < ContactInfo.Count; index++)
            {
                if (contactInfoId == ContactInfo[index].Id)
                {
                    return (ContactInfo[index], index);
                }
            }

            return (null, -1);
        }

        public void UpdateContactInfo(int contactInfoId, string parameter, string newValue)
        {
            try
            {
                (ContactInfo contactInfoToBeUpdated, int index) = FindContactInfo(contactInfoId);

                if (contactInfoToBeUpdated == null)
                {
                    throw new Exception("Invalid Id");
                }

                switch (parameter)
                {
                    case "typeOfContact":
                        contactInfoToBeUpdated.UpdateTypeofContactInfo(parameter, newValue);
                        break;
                    case "valueOfContact":
                        contactInfoToBeUpdated.UpdateValueofContactInfo(parameter, newValue);
                        break;
                    default:
                        throw new Exception("Invalid parameter");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteContactInfo(int contactInfoId)
        {
            try
            {
                (ContactInfo contactInfoToBeDeleted, int index) = FindContactInfo(contactInfoId);

                if (index == -1)
                {
                    throw new Exception("Index not found");
                }

                ContactInfo.RemoveAt(index);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
