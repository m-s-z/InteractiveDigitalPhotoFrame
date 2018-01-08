using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels
{
    /// <summary>
    /// change cloud password view model
    /// </summary>
    public class ChangeCloudPasswordViewModel
    {
        /// <summary>
        /// ChangeCloudPasswordViewModel class
        /// </summary>
        /// <param name="result">result</param>
        public ChangeCloudPasswordViewModel(string result)
        {
            Result = result;
        }
        #region properties
        /// <summary>
        /// string with result ot be displayed
        /// </summary>
        public string Result { get; set; }
        #endregion properties
    }
}