using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels
{
    /// <summary>
    /// pair device view model class
    /// </summary>
    public class PairDeviceViewModel
    {
        /// <summary>
        /// constructor for PairDeviceViewModel
        /// </summary>
        /// <param name="result">result</param>
        public PairDeviceViewModel(string result)
        {
            Result = result;
        }
        #region properties
        /// <summary>
        /// result string to be displayed
        /// </summary>
        public String Result { get; set; }
        #endregion properties
    }
}