using ContactApp.Model;

namespace ContactApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Admin
            User admin1 = User.NewAdmin("Simran", 22, 'F');
            Console.WriteLine(admin1);

            //CRUD on User
            User user1 = admin1.NewUser("Madhura", 22, 'F');
            User user2 = admin1.NewUser("Rahul", 21, 'M');

            //user1.UpdateUser(1, "name", "Rohit");
            //admin1.DeleteUser(1);
            //Console.WriteLine("==============All Users List:=================");
            admin1.GetAllUsers();


            //CRUD on Contact
            user1.CreateContact("Sanika");
            user1.CreateContact("Deepanshu");
            user1.CreateContact("Richa");

            //user1.UpdateContact(1, "name", "Tanaya");
            //user1.DeleteContact(2);
            user1.GetAllContact();

            //CRUD on ContactInfo
            user1.CreateContactInfo(1, "mobileNo", "2345678909");
            user1.CreateContactInfo(1, "Company", "Aurionpro");

            //user1.UpdateContactInfo(1, 1,"valueOfContact","987654312");
            //user1.DeleteContactInfo(1, 1);
            //Console.WriteLine("==============All ContactInfo List:=================");
            user1.GetAllContactInfo(1);
        }
    }
}
