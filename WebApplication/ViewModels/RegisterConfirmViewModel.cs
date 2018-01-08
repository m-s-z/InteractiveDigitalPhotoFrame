using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels
{
    /// <summary>
    /// register confirm view model
    /// </summary>
    public class RegisterConfirmViewModel
    {
        /// <summary>
        /// constructor for RegisterConfirmViewModel
        /// </summary>
        /// <param name="message">message</param>
        public RegisterConfirmViewModel(string message)
        {
            Message = message;
        }
        #region properties
        /// <summary>
        /// message to be displayed
        /// </summary>
        public string Message { get; set; }
        #endregion properties
    }
}