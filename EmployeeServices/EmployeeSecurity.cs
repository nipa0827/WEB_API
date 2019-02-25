using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeDataAccess;

namespace EmployeeServices
{
    public class EmployeeSecurity
    {
        public static bool Login(string username, string password){
            using (AIDummyDBEntities entitis = new AIDummyDBEntities()){
                return entitis.Users.Any(user => user.username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                user.password.Equals(password, StringComparison.OrdinalIgnoreCase));

            }
        }
    }
}