using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels
{
    /// <summary>
    /// manage cloud view model
    /// </summary>
    public class MangeCloudViewModel
    {
        /// <summary>
        /// constructor for MangeCloudViewModel class
        /// </summary>
        /// <param name="name">name</param>
        public MangeCloudViewModel(string name)
        {
            Login = name;
        }
        /// <summary>
        /// constructor for MangeCloudViewModel class
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="login">login</param>
        public MangeCloudViewModel(int id, string login)
        {
            Id = id;
            Login = login;
        }
        #region properties
        /// <summary>
        /// id of cloud to be managed
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// custom name for cloud to be displayed
        /// </summary>
        public String Login { get; set; }
        #endregion properties
    }
}