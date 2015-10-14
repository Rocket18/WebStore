using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebStore.Models.DAL;
using WebStore.Models.Entities;

namespace WebStore.UI.Infrastructure.Concrete
{
    public class CustomMembershipUser:MembershipUser
    {  
        public string Username { get; set; }
        public string UserRoleName { get; set; }
        public CustomMembershipUser(Users user)
            : base("CustomMembershipProvider", user.Username, user.UserID, string.Empty, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        { 
            this.Username = user.Username;
            UserRoleName = user.UsersRole.Name;
        }
      
    }
}