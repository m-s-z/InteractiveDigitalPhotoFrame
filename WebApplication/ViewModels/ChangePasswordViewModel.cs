using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels
{
    /// <summary>
    /// Change Password view model class
    /// </summary>
    public class ChangePasswordViewModel
    {
        #region properties
        /// <summary>
        /// message to be displayed
        /// </summary>
        public String Message { get; set; }
        #endregion properties
        /// <summary>
        /// constructor for ChangePasswordViewModel class
        /// </summary>
        public ChangePasswordViewModel()
        {
                
        }
        /// <summary>
        /// constructor for ChangePasswordViewModel class
        /// </summary>
        /// <param name="message">message</param>
        public ChangePasswordViewModel(string message)
        {
            Message = message;
        }
    }
}