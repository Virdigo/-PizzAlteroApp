using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzAlteroApp.Resourses.DataBase;

namespace PizzAlteroApp.Resourses.Controllers
{
    public class UserController
    {
        Connection connection = new Connection();
        public List<Users> GetUsers()
        {
            try
            {
                var users = connection.PizzAltero_DataBase.Users.ToList();
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
        public Users CreateNewUser(
            string username,
            string useraddress,
            string card_number,
            string password,
            string mail,
            string login)
        {
            try
            {
                Users users = new Users
                {
                    UserName = username,
                    UserAddress = useraddress,
                    Card_Number = card_number,
                    Password = password,
                    Mail = mail,
                    Login = login,
                };
                connection.PizzAltero_DataBase.Users.Add(users);
                connection.PizzAltero_DataBase.SaveChanges();
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
        public Users SingIn(string login, string password)
        {
            try
            {
                var user = connection.PizzAltero_DataBase.Users.Where(x=>x.Login == login && x.Password == password).First();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
