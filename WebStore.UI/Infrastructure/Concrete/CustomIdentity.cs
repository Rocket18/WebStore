using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Web.Security;

namespace WebStore.UI.Infrastructure.Concrete
{
    [Serializable]
    public class CustomIdentity: IIdentity
    {
        public IIdentity Identity { get; set; }

        public string Username { get; set; }
        public string UserRoleName { get; set; }
        #region Implementation of IIdentity

        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        /// <returns>
        /// The name of the user on whose behalf the code is running.
        /// </returns>
        public string Name
        {
            get { return Identity.Name; }
        }

        /// <summary>
        /// Gets the type of authentication used.
        /// </summary>
        /// <returns>
        /// The type of authentication used to identify the user.
        /// </returns>
        public string AuthenticationType
        {
            get { return Identity.AuthenticationType; }
        }

        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated.
        /// </summary>
        /// <returns>
        /// true if the user was authenticated; otherwise, false.
        /// </returns>
        public bool IsAuthenticated { get { return Identity.IsAuthenticated; } }

        #endregion

        #region Constructor

        public CustomIdentity(IIdentity identity)
        {
            Identity = identity;

            var customMembershipUser = (CustomMembershipUser)Membership.GetUser(identity.Name);
            if (customMembershipUser != null)
            {
                this.Username = customMembershipUser.Username;
                UserRoleName = customMembershipUser.UserRoleName;
            }
        }

        #endregion
    }
    
      
}