using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Model
{
    internal class User
    {
        private static int id = 0;
        private static  List<User> allUsers = new List<User>();

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public char Gender { get; set; }
        public bool IsAdmin { get; set; }
        public List<Contact> Contacts { get; set; }

        public User(string name, int age, char gender, bool isAdmin)
        {
            Id = id++;
            Name = name;
            Age = age;
            Gender = gender;
            IsAdmin = isAdmin;
            Contacts = new List<Contact>();
 
        }

        public static User NewAdmin(string name, int age, char gender)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException("Invalid name");
                }
                if (age <= 0)
                {
                    throw new ArgumentException("Invalid age");
                }
                if (gender != 'M' && gender != 'F' && gender != 'O')
                {
                    throw new ArgumentException("Invalid gender");
                }

                return new User(name, age, gender, true);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        private static bool CurrentUserIsAdmin()
        {
            return true;
        }
        public  User NewUser(string name, int age, char gender)
        {
            try
            {
                if (!CurrentUserIsAdmin())
                {
                    throw new InvalidOperationException("Only admin can access");
                }

                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException("Invalid name");
                }

                if (age <= 0)
                {
                    throw new ArgumentException("Invalid age");
                }

                if (gender != 'M' && gender != 'F' && gender != 'O')
                {
                    throw new ArgumentException("Invalid gender");
                }

                var newUser = new User(name, age, gender, false);
                allUsers.Add(newUser);
                return newUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                if (!CurrentUserIsAdmin())
                {
                    throw new InvalidOperationException("Only admin can access");
                }
                Console.WriteLine("==============All Users List:=================");
                foreach (var user in allUsers)
                {
                    Console.WriteLine(user.ToString());
                }

                return User.allUsers;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static int FindUser(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    throw new ArgumentException("Invalid User Id");
                }

                for (int index = 0; index < allUsers.Count; index++)
                {
                    if (userId == allUsers[index].Id)
                    {
                        return index;
                    }
                }

                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
        public void UpdateName(string newValue, int index)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(newValue))
                {
                    throw new ArgumentException("Invalid Name");
                }

                if (index < 0 || index >= allUsers.Count)
                {
                    throw new IndexOutOfRangeException("Invalid index");
                }

                allUsers[index].Name = newValue;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateAge(int newValue, int index)
        {
            try
            {
                if (newValue <= 0)
                {
                    throw new ArgumentException("Invalid Age");
                }

                if (index < 0 || index >= allUsers.Count)
                {
                    throw new IndexOutOfRangeException("Invalid index");
                }

                allUsers[index].Age = newValue;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateGender(char newValue, int index)
        {
            try
            {
                if (newValue != 'M' && newValue != 'F' && newValue != 'O')
                {
                    throw new ArgumentException("Invalid Gender");
                }

                if (index < 0 || index >= allUsers.Count)
                {
                    throw new IndexOutOfRangeException("Invalid index");
                }

                allUsers[index].Gender = newValue;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateUser(int userId, string parameter, object newValue)
        {
            try
            {
                if (!CurrentUserIsAdmin())
                {
                    throw new InvalidOperationException("Only Admin can access");
                }

                int index = FindUser(userId);
                if (index == -1)
                {
                    throw new Exception("Index not found");
                }

                switch (parameter)
                {
                    case "name":
                        UpdateName((string)newValue, index);
                        break;
                    case "age":
                        UpdateAge((int)newValue, index);
                        break;
                    case "gender":
                        UpdateGender((char)newValue, index);
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

        public void DeleteUser(int userId)
        {
            try
            {
                if (!CurrentUserIsAdmin())
                {
                    throw new InvalidOperationException("Only Admin Can Access");
                }

                int index = FindUser(userId);
                if (index == -1)
                {
                    throw new Exception("Index not found");
                }

                allUsers.RemoveAt(index);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public Contact CreateContact(string name)
        {
            try
            {
                if (IsAdmin)
                {
                    throw new InvalidOperationException("Admin Access Prohibited");
                }

                Contact newContact = Contact.CreateNewContact(name);
                Contacts.Add(newContact);
                return newContact;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public List<Contact> GetAllContact()
        {
            try
            {
                if (IsAdmin)
                {
                    throw new InvalidOperationException("Admin Access Prohibited");
                }
                Console.WriteLine("==============All Contact List:=================");
                foreach (var contact in Contacts)
                {
                    Console.WriteLine(contact.ToString());
                }
                return Contacts;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null; 
            }
        }

        public (Contact, int) FindContact(int contactId)
        {
            try
            {
                if (contactId <= 0)
                {
                    throw new ArgumentException("Invalid Contact Id");
                }

                for (int index = 0; index < Contacts.Count; index++)
                {
                    if (contactId == Contacts[index].Id)
                    {
                        return (Contacts[index], index);
                    }
                }

                return (null,-1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (null, -1); 
            }
        }

        public void UpdateContact(int contactId, string parameter, string newValue)
        {
            try
            {
                if (IsAdmin)
                {
                    throw new InvalidOperationException("Admin Access Prohibited");
                }

                if (contactId < 0 )
                {
                    throw new ArgumentException("Invalid input");
                }

                (Contact contactToBeUpdated, int index) = FindContact(contactId);

                if (contactToBeUpdated == null)
                {
                    throw new Exception("Invalid Id");
                }

                contactToBeUpdated.UpdateContact(parameter, newValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DeleteContact(int contactId)
        {
            try
            {
                if (IsAdmin)
                {
                    throw new InvalidOperationException("Admin Access Prohibited");
                }

                if (contactId < 0)
                {
                    throw new ArgumentException("Invalid input");
                }

                (Contact contactToBeDeleted, int index) = FindContact(contactId);

                if (index == -1)
                {
                    throw new Exception("Index not found");
                }

                Contacts.RemoveAt(index);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public ContactInfo CreateContactInfo(int contactId, string typeOfContact, object valueOfContact)
        {
            try
            {
                if (IsAdmin)
                {
                    throw new InvalidOperationException("Admin Access Prohibited");
                }

                if (contactId < 0)
                {
                    throw new ArgumentException("Invalid input");
                }

                (Contact contactInfoToBeCreated, int index) = FindContact(contactId);

                if (index == -1 || contactInfoToBeCreated == null)
                {
                    throw new Exception("Contact not found");
                }

                ContactInfo contactInfo = contactInfoToBeCreated.CreateContactInfo(typeOfContact, valueOfContact);
                return contactInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public List<ContactInfo> GetAllContactInfo(int contactId)
        {
            try
            {
                if (IsAdmin)
                {
                    throw new Exception("Admin Access Prohibited");
                }

                if (contactId < 0)
                {
                    throw new Exception("Invalid input");
                }

                (Contact contactToBeDisplayed, int index) = FindContact(contactId);

                if (contactToBeDisplayed == null)
                {
                    throw new Exception("Invalid Id");
                }
                Console.WriteLine("==============All ContactInfo List:=================");
                foreach (var contactInfo in contactToBeDisplayed.GetAllContactInfo())
                {
                    Console.WriteLine(contactInfo.ToString());
                }
                List<ContactInfo> allContactInfo = contactToBeDisplayed.GetAllContactInfo();
                return allContactInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public void UpdateContactInfo(int contactId, int contactInfoId, string parameter, string newValue)
        {
            try
            {
                if (IsAdmin)
                {
                    throw new Exception("Admin Access Prohibited");
                }

                if (contactId < 0 || contactInfoId < 0)
                {
                    throw new Exception("Invalid input");
                }

                (Contact contactToBeUpdated, int index) = FindContact(contactId);

                if (contactToBeUpdated == null)
                {
                    throw new Exception("Invalid Id");
                }

                contactToBeUpdated.UpdateContactInfo(contactInfoId, parameter, newValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteContactInfo(int contactId, int contactInfoId)
        {
            try
            {
                if (IsAdmin)
                {
                    throw new Exception("Admin Access Prohibited");
                }

                if (contactId<0 || contactInfoId <0)
                {
                    throw new Exception("Invalid Id");
                }

                (Contact contactToBeDeleted, int index) = FindContact(contactId);

                if (contactToBeDeleted == null)
                {
                    throw new Exception("Invalid Id");
                }

                contactToBeDeleted.DeleteContactInfo(contactInfoId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public override string ToString()
        {
            return $"User Id: {Id}\n" +
                $"Name: {Name}\n" +
                $"Age: {Age}\n" +
                $"Gender: {Gender}\n" +
                $"IsAdmin:{IsAdmin}\n" +
                $"==============";

        }

    }
}
